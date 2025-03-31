using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ConfigApp;
using Db;
using BalanzaSerialPort;
using SerializerClass;
using EditValueTouchDlg;
using EditStringTouchDlg;
using System.Data.OleDb;
using System.Globalization;
using PrintEtiZpl2_Serie;
using Logger;
using Commons;
using ScaleSerialCtrl;
using System.Drawing.Printing;
using PrintEtiZpl2_Win;
using System.Threading.Tasks;
using System.Threading;
using ZebraScannerLib;
using HidKeyboardScannerLib;

namespace MeatWeigherManager
{
    public partial class Form_TransferenciaDeposito : Form
    {
        CPIP  DataReadPiece;
        CContenedor DataReadCaja;
        BultosTransferidos ListBultosTransferidos;

        CZebraScannerLib m_scannerDRV;
        HidScannerLib m_scannerHID;

        /// <summary>
        /// Evento de simulacion de lectura de scanner por edicion manual touch.
        /// </summary>
        public delegate void NewDataEditScanner(string canData);
        public event NewDataEditScanner OnNewDataEditScanner;


        private enum ESTADOS_APP
        {
            INICIADA,
            PARADA,
            INABILITADA
        };

        private enum TYPE_READ_VALUE_SCANNER
        {
            PIECE,
            BOX
        }

        private ESTADOS_APP m_Estado;
        private const int CODBAR_MAX_LENGTH = 7;

        private TYPE_READ_VALUE_SCANNER TypeReadValueScanner { get; set; } = TYPE_READ_VALUE_SCANNER.PIECE;

        #region INICIALIZACION

        public Form_TransferenciaDeposito()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {
            if (CDb.isOpen)
            {
                ListBultosTransferidos = new BultosTransferidos();
                LoadDataGridListBultoTransferidos();
                SetEstado(ESTADOS_APP.PARADA);
            }
            else
            {
                SetEstado(ESTADOS_APP.INABILITADA);
            }
        }

        private void InitializeScanner()
        {
            if (ConfigApp.CConfigApp.m_hostInterfaceScaneer == HostInterfaceScanner.SNAPI_CoreScanner)
            {
                m_scannerDRV = new CZebraScannerLib(CConfigApp.m_modeloScannerZebra);
                m_scannerDRV.ListCodBarEnables.Add(CODBAR_TYPE.QR);
                m_scannerDRV.OnNewDataScanner += M_scannerDRV_OnNewDataScanner;
                m_scannerDRV.OnAttachedScanner += M_scannerDRV_OnAttachedScanner;
                m_scannerDRV.OnDetachedScanner += M_scannerDRV_OnDetachedScanner;
                m_scannerDRV.OnScannerDataException += M_scannerDRV_OnScannerDataException;
                //actualiza ToolStripStatus de escaner
                if (m_scannerDRV.IsOpen) M_scannerDRV_OnAttachedScanner();
                else M_scannerDRV_OnDetachedScanner();
            }

            if (ConfigApp.CConfigApp.m_hostInterfaceScaneer == HostInterfaceScanner.HID_KeyboarEmulation)
            {
                m_scannerHID = new HidScannerLib(this, textBox_valueReadCodBar);
                m_scannerHID.OnNewDataScanner += M_scannerDRV_OnNewDataScanner;
            }
            //inicializa evento de simulacion de lectura de scanner
            OnNewDataEditScanner += M_scannerDRV_OnNewDataScanner;
        }
        private void CloseScanner()
        {
            m_scannerDRV?.Close();
            m_scannerHID?.Close();
        }

        #endregion

        #region EVENTOS DEL FORM Y DE SUS CONTROLES
        private void Form_TransferenciaDeposito_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            InitializeScanner();
            WindowState = FormWindowState.Maximized;
        }

        private void Form_TransferenciaDeposito_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseScanner();
            ((MainForm)this.ParentForm).closeChild.Set();
        }
        //obtiene una referencia al control toolstrip activo del form
        public ToolStrip GetActiveToolStrip()
        {
            return toolStrip_buttons;
        }

        /**************************************************************************************
         *  Detecta teclas de Funcion y ejecuta los botones que correspondan de la barra de
         *  herramientas. (para que funcione la llamada PrecessCmdKey debe estar habilitado a true
         *  en el form la propiedad KeyPreview=True.
         **************************************************************************************/
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1))
            {
                if (toolStripButton_Iniciar.Enabled)
                    toolStripButton_Iniciar.PerformClick();
            }
            else if (keyData == (Keys.F2))
            {
                if (toolStripButton_Parar.Enabled)
                    toolStripButton_Parar.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button_selectDestino_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_destinos.Tag != null ? ((CDestino)textBox_destinos.Tag).Id : 0;
            CABM_DestinosDlg abmDlg = new CABM_DestinosDlg(CDb.m_oleDbConnection, CABM_DestinosDlg.BEHAVIOR_MODE.SELECTION, idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.DestinoSelected.Id != 0)
            {
                textBox_destinos.Tag = abmDlg.DestinoSelected;
                CargarControlConDestinoActivo();
            }
            else
            {
                ClrControlDestino();
            }
        }
        /// <summary>
        /// Permite al Operador simular la lectura del scanner editando su valor de codigo de barras de la pieza.
        /// Dispara un evento que genera el mismo efecto que lograria la lectura del escanner.
        /// </summary>
        private void textBox_valueReadCodBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CConfigApp.m_permiteSimularLecturaScanner)
            {
                CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Numero de Pieza o Caja", "Numero", "", 10);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox_valueReadCodBar.Text = dlg.VALUE.ToUpper();
                    OnNewDataEditScanner?.Invoke(dlg.VALUE.ToUpper());
                }
            }
        }

        #endregion

        #region SCANNER
        private async void M_scannerDRV_OnScannerDataException(string infoException)
        {
            await ExecuteExceptionToScanner(infoException);
        }

        private void M_scannerDRV_OnDetachedScanner()
        {
            Invoke((MethodInvoker)delegate
            {
                toolStripStatusScannerValue.Text = "OFF";
                toolStripStatusScannerValue.ForeColor = Color.Red;
            });
        }

        private void M_scannerDRV_OnAttachedScanner()
        {
            Invoke((MethodInvoker)delegate
            {
                toolStripStatusScannerValue.Text = "ON";
                toolStripStatusScannerValue.ForeColor = Color.Green;
            });
        }

        private async void M_scannerDRV_OnNewDataScanner(string canData)
        {
            SetText_ControlSecure(textBox_valueReadCodBar,canData);
            await Task.Run(() =>
            {
                RegisterReadPiece(canData);
            });
        }

        private async Task ExecuteExceptionToScanner(string infoException)
        {
            await Task.Run(() =>
            {
                if (infoException != "")
                    SetText_ControlSecure(label_detalleLectura, infoException);

                m_scannerDRV?.Beep(BEEPLED_TYPE.RED_LED_ON);
                Thread.Sleep(500);
                m_scannerDRV?.Beep(BEEPLED_TYPE._4_HIGH_LONG_BEEPS);
                m_scannerDRV?.Beep(BEEPLED_TYPE.RED_LED_OFF);
            });
        }
        #endregion
        
        #region EVENTOS MENU

        private void consultaTransferenciasRealizadas_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaTransferenciasDepositoDlg dlg = new CViewConsultaTransferenciasDepositoDlg();
            dlg.ShowDialog();
        }

        #endregion

        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            ClrControlesCaptura();
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            SetEstado(ESTADOS_APP.PARADA);
        }

        #endregion

        #region METODOS DE ACCESSO A CONTROLES DE FORMA SEGURA
        /// **************************************************************
        /// SetColorButtonSecure
        /// Esta Funcion es utilizada para cambiar el color de fondo de un
        /// boton sin generar excepciones por accederce por varios threads.
        /// **************************************************************
        private void SetColorButtonSecure(Button ctrlButton, Color color)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctrlButton.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetColorButtonSecure(ctrlButton, color); }));
            }
            else
            {
                ctrlButton.BackColor = color;
            }
        }

        /// **************************************************************
        /// SetEnableCtrlSecure
        /// Esta Funcion es utilizada para cambiar el estado de true a false
        /// en la propiedad Enable de un Control de forma segura para que no 
        /// genere excepciones por accederce por varios threads.
        /// **************************************************************
        private void SetEnableCtrlSecure(Control ctrl , bool enable)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetEnableCtrlSecure(ctrl, enable); }));
            }
            else
            {
                ctrl.Enabled = enable;
            }
        }
        /// **************************************************************
        /// SetVisibleCtrlSecure
        /// Esta Funcion es utilizada para cambiar el estado de true a false
        /// en la propiedad Visible de un Control de forma segura para que no 
        /// genere excepciones por accederce por varios threads.
        /// **************************************************************
        private void SetVisibleCtrlSecure(Control ctrl, bool enable)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetVisibleCtrlSecure(ctrl, enable); }));
            }
            else
            {
                ctrl.Visible = enable;
            }
        }
        /// **************************************************************
        /// SetText_ControlSecure
        /// Esta Funcion permite escribir , de forma segura , la propiedad
        /// Text de un objeto Control o de una clase que derive de el
        /// para ser accedida desde otros threads.(eventos)
        /// **************************************************************
        private void SetText_ControlSecure(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetText_ControlSecure(ctrl, text); }));
            }
            else
            {
                ctrl.Text = text;
            }
        }
        /// <summary>
        /// Establece el texto que tendra un control y su color de forma segura.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void SetTextColorCtrlSecure(Control ctrl, string text,Color color)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetTextColorCtrlSecure(ctrl, text,color); }));
            }
            else
            {
                ctrl.Text = text;
                ctrl.ForeColor = color;
            }
        }

        /// **************************************************************
        /// SetDataSource_DataGridViewSecure
        /// **************************************************************
        private void SetDataSource_DataGridViewSecure(DataGridView ctrl, object dataSource)
        {
            if (ctrl.InvokeRequired)
            {

                Invoke(new MethodInvoker(delegate ()
                { SetDataSource_DataGridViewSecure(ctrl, dataSource); }));
            }
            else
            {
                ctrl.DataSource = dataSource;
            }
        }
        /// **************************************************************
        /// SetDataSource_DataGridViewRefresh
        /// **************************************************************
        private void SetDataSource_DataGridViewRefresh(DataGridView ctrl)
        {
            if (ctrl.InvokeRequired)
            {

                Invoke(new MethodInvoker(delegate ()
                { SetDataSource_DataGridViewRefresh(ctrl); }));
            }
            else
            {
                ctrl.Refresh();
            }
        }
        /// **************************************************************
        /// SetDataMember_DataGridViewSecure
        /// **************************************************************
        private void SetDataMember_DataGridViewSecure(DataGridView ctrl, string dataMember)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetDataMember_DataGridViewSecure(ctrl, dataMember); }));
            }
            else
            {
                ctrl.DataMember = dataMember;
            }
        }

        /**********************************************************************************************
        * Funcion:        SetToolStripStatusLabelSecure
        * Descripcion:    Esta Funcion es utilizada para cambiar el texto y color de una 
        *                 etiqueta en un item de un control StatusStrip. Este metodo esta
        *                 preparardo para ser accedido desde varios threads y no generar
        *                 excepciones.
        * Parametro:      StatusStrip ctrl: Control
        * Parametro:      int idxLabel: Indice al item a modificar
        * Parametro:      string valor: Valor de texto a escribir.
        * Parametro:      Color color: Color de texto
        ***********************************************************************************************/
        private void SetToolStripStatusLabelSecure(ToolStripStatusLabel ctrl, string valor, Color color)
        {
            if (ctrl.GetCurrentParent() != null)
            {
                if (ctrl.GetCurrentParent().InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    { SetToolStripStatusLabelSecure(ctrl, valor, color); }));
                }
                else
                {
                    ctrl.Text = valor;
                    ctrl.ForeColor = color;
                }
            }
        }
        #endregion

        #region FUNCIONES MAQUINA DE ESTADOS
        private void SetEstado(ESTADOS_APP estado)
        {
            m_Estado = estado;
            PrepararControlesDlg();
        }
        private ESTADOS_APP GetEstado()
        {
            return m_Estado;
        }
        private void PrepararControlesDlg()
        {
            switch (m_Estado)
            {
                case ESTADOS_APP.INABILITADA:
                    {
                        SetEnableToolStripButtomsStartStop(false);
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        groupBox_datCaptura.Enabled = true;
                        groupBox_datCaptura.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "EN PROCESO DE ESCANEO", Color.Red);
                        break;
                    }
            }
        }
        #endregion

        #region FUNCIONES GENERALES

        private async void RegisterReadPiece(string codBarRead)
        {
            if (m_Estado == ESTADOS_APP.INICIADA)
            {
                var newDestino = textBox_destinos.Tag as CDestino;
                SetDataReadPiece(codBarRead);

                try
                {
                    if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.PIECE)
                    {
                        if (await IsValidUpdateReadBulto(DataReadPiece))
                        {
                            if(!esBultoYaActualizado(DataReadPiece,newDestino))
                            {
                                RemoveBultoToList(DataReadPiece);
                                if (await ActualizarDepositoBulto(DataReadPiece, newDestino))
                                {
                                    AddBultoTransferido(ListBultosTransferidos, DataReadPiece, newDestino);
                                    LoadDataGridListBultoTransferidos();
                                    await RegistrarEventoLogTransferencia(DataReadPiece, newDestino);

                                    SetMessageReadyScanner("Bulto Actualizado con  exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar actualizar el depósito destino de la pieza. Verifique la conexion con la base de datos.", "ACTUALIZACIÓN DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                SetMessageReadyScanner("El Bulto ya esta Actualizado !!.", Color.Black);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                        }
                    }
                    else
                    {
                        if (await IsValidUpdateReadBulto(DataReadCaja))
                        {
                            if (!esBultoYaActualizado(DataReadCaja, newDestino))
                            {
                                RemoveBultoToList(DataReadCaja);
                                if (await ActualizarDepositoBulto(DataReadCaja, newDestino))
                                {
                                    AddBultoTransferido(ListBultosTransferidos, DataReadCaja, newDestino);
                                    await RegistrarEventoLogTransferencia(DataReadCaja, newDestino);

                                    LoadDataGridListBultoTransferidos();
                                    SetMessageReadyScanner("Bulto Registrado con  exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar actualizar el depósito destino de la pieza. Verifique la conexion con la base de datos.", "ACTUALIZACIÓN DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                SetMessageReadyScanner("El Bulto ya esta Actualizado !!.", Color.Black);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                        }
                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al intentar registrar un ingreso de pieza a producción.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

  

        private bool esBultoYaActualizado(CPIP pieza, CDestino newDestino)
        {
            return ListBultosTransferidos.Any(x=>x.Id==pieza.Id && x.Tipo == TipoBulto.PIEZA && x.Destino.Id==newDestino.Id);
        }
        private bool esBultoYaActualizado(CContenedor contenedor, CDestino newDestino)
        {
            return ListBultosTransferidos.Any(x => x.Id == contenedor.Id && x.Tipo != TipoBulto.PIEZA &&x.Destino.Id == newDestino.Id);
        }
        private void RemoveBultoToList(CPIP pieza)
        {
            ListBultosTransferidos.RemoveAll(x => x.Id == pieza.Id && x.Tipo == TipoBulto.PIEZA);
        }
        private void RemoveBultoToList(CContenedor contenedor)
        {
            ListBultosTransferidos.RemoveAll(x => x.Id == contenedor.Id && x.Tipo != TipoBulto.PIEZA);
        }
        
        private async Task RegistrarEventoLogTransferencia(CPIP pieza, CDestino newDestino)
        {
            await Task.Run(() =>
            {
                CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.TransferenciaDeDeposito, TYPE_CONTEXT_DBLOG.TransferenciasDeposito,
                        string.Format("La Pieza Nro: {0} Producto: {1} fue transferida del depósito: {2} al {3}",
                        pieza.Id,
                        pieza.Producto.Nombre.Trim(),
                        pieza.Destino.ToString(),
                        newDestino.ToString()));
            });

        }
        private async Task RegistrarEventoLogTransferencia(CContenedor contenedor, CDestino newDestino)
        {
            await Task.Run(() =>
            {
                CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.TransferenciaDeDeposito, TYPE_CONTEXT_DBLOG.TransferenciasDeposito,
                    string.Format("El contenedor Nro: {0} Tipo: {1} Producto: {2} fue transferido del depósito: {3} al {4}",
                    contenedor.Id,
                    contenedor.IdTipo,
                    contenedor.Producto.Nombre.Trim(),
                    contenedor.Destino.ToString(),
                    newDestino.ToString()));
            });
        }

        private void AddBultoTransferido(BultosTransferidos listBultosTransferidos, CContenedor bulto,CDestino newDestino)
        {
            listBultosTransferidos.Add(new BultoTransferido()
            {
                Id = bulto.Id,
                Nombre = bulto.Producto.Nombre,
                Peso = bulto.PesoNeto,
                Tipo = bulto.IdTipo == "CAJ" ? TipoBulto.CAJA : TipoBulto.COMBO,
                Origen = bulto.Destino,
                Destino = newDestino

            });
        }
        private void AddBultoTransferido(BultosTransferidos listBultosTransferidos, CPIP bulto, CDestino newDestino)
        {
            listBultosTransferidos.Add(new BultoTransferido()
            {
                Id = bulto.Id,
                Nombre = bulto.Producto.Nombre,
                Peso = bulto.PesoNeto,
                Tipo = TipoBulto.PIEZA,
                Origen = bulto.Destino,
                Destino = newDestino

            });
        }


        private async Task<bool> ActualizarDepositoBulto(CPIP pieza, CDestino newDestino)
        {
            return await Task.Run(() =>
            {
                return CDb.ActualizarDepositoDestinoDePieza(pieza.Id, newDestino.Id);
            });
        }
        private async Task<bool> ActualizarDepositoBulto(CContenedor contenedor, CDestino newDestino)
        {
            return await Task.Run(() =>
            {
                return CDb.ActualizarDepositoDestinoDeContenedor(contenedor.Id, newDestino.Id);
            });
        }

        private async Task SetOnOffLedOK()
        {
            await Task.Run(() =>
            {
                ledCtrl_readyOK.LedStatus = true;
                Thread.Sleep(2000);
                ledCtrl_readyOK.LedStatus = false;
            });
        }
        private async Task SetOnOffLedError()
        {
            await Task.Run(() =>
            {
                ledCtrl_readyError.LedStatus = true;
                Thread.Sleep(2000);
                ledCtrl_readyError.LedStatus = false;
            });
        }
        private void SetMessageReadyScanner(string text,Color color)
        {
            SetTextColorCtrlSecure(label_detalleLectura, text, color);
        }

        private void SetEnableToolStripButtomsStartStop(bool enable)
        {
            toolStripButton_Iniciar.Enabled = enable;
            toolStripButton_Parar.Enabled = enable;
        }

        private void ClrControlesCaptura()
        {
            DateTime dt = DateTime.Now;
            textBox_valueReadCodBar.Text = "";
            LoadDataGridListBultoTransferidos();
        }

        private void CargarControlConDestinoActivo()
        {
            if (textBox_destinos.Tag != null)
            {
                textBox_destinos.Text = ((CDestino)textBox_destinos.Tag).Nombre;
            }
        }

        private void ClrControlDestino()
        {
            textBox_destinos.Text = "";
            textBox_destinos.Tag = null;
        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            DataReadPiece = null;
            DataReadCaja = null;
            if (codeBarRead != "" && codeBarRead.Length >= 3 && codeBarRead[0] == 'A' && codeBarRead[codeBarRead.Length - 1] == 'A')
            {
                codeBarRead = codeBarRead.Remove(0, 1);
                codeBarRead = codeBarRead.Remove(codeBarRead.Length - 1, 1);
                Int32.TryParse(codeBarRead, out id);
                DataReadCaja = CDb.GetContenedor(id);
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.BOX;
            }
            else
            {
                Int32.TryParse(codeBarRead, out id);
                CPesada datpieza = CDb.GetPesada(id);
                if (datpieza != null)
                {
                    DataReadPiece = new CPIP(datpieza);
                    DataReadPiece.IdEstacionRegistration = CDb.m_OperadorActivo.m_idEstacion;
                    DataReadPiece.IdOperadorRegistration = CDb.m_OperadorActivo.m_id;
                }
                else
                    DataReadPiece = null;
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.PIECE;
            }

        }
        private async Task<bool> IsValidUpdateReadBulto(CPIP pieza)
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (textBox_destinos.Tag == null)
                {
                    SetMessageReadyScanner("Debe seleccionar un Depósito destino !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (pieza == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (pieza.Producto.EsInsumo)
                {
                    SetMessageReadyScanner("La pieza colectada es un Insumo !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsPartInStock(pieza.Id))
                {
                    SetMessageReadyScanner("La pieza no esta en stock !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        private async Task<bool> IsValidUpdateReadBulto(CContenedor contenedor)
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (textBox_destinos.Tag == null)
                {
                    SetMessageReadyScanner("Debe seleccionar un Depósito destino !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (contenedor == null)
                {
                    SetMessageReadyScanner("El contenedor colectado no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.EsContenedorEnStock(contenedor.Id))
                {
                    SetMessageReadyScanner("El Contenedor no esta en stock !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        #endregion

        #region DATAGRID DETALLE DE PIEZAS TRANSFERIDAS
        private void LoadDataGridListBultoTransferidos()
        {
            try
            {

                var blist = new BindingList<BultoTransferido>(ListBultosTransferidos);
                SetDataSource_DataGridViewSecure(dataGridView_bultosTransferidos,blist);
                SetFormatDGVDetalleBultosTransferidosSecure();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Source + "-" + aex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException aex)
            {
                MessageBox.Show(aex.Source + "-" + aex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetFormatDGVDetalleBultosTransferidosSecure()
        {
            if (dataGridView_bultosTransferidos.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetalleBultosTransferidosSecure(); }));
            }
            else
            {
                dataGridView_bultosTransferidos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Id)].HeaderText = "Nro";
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Id)].DisplayIndex = 0;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Id)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Id)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Tipo)].HeaderText = "Tipo";
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Tipo)].DisplayIndex = 1;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Tipo)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Tipo)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; ;

                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Nombre)].HeaderText = "Producto";
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Nombre)].DisplayIndex = 2;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Nombre)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Peso)].DisplayIndex = 3;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Peso)].DefaultCellStyle.Format = "0.00 kg";
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Peso)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Peso)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; ;

                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Origen)].DisplayIndex = 4;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Origen)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Destino)].DisplayIndex = 5;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Destino)].DefaultCellStyle.ForeColor = Color.Red;
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Destino)].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                dataGridView_bultosTransferidos.Columns[nameof(BultoTransferido.Destino)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                
            }
        }

        #endregion

        
    }
}
