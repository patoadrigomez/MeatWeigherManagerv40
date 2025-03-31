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
    public partial class Form_Inventario : Form
    {
        CPIP  m_datReadPiece;
        CContenedor m_dataReadContenedor;
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
            CONTAINER
        }

        private ESTADOS_APP m_Estado;
        private const int CODBAR_MAX_LENGTH = 7;
        /// <summary>
        /// establece el estado de captura normal o en modo eliminacion de piezas colectadas
        /// </summary>
        public bool InPartRemovalMode { get; private set; } = false;

        private TYPE_READ_VALUE_SCANNER TypeReadValueScanner { get; set; } = TYPE_READ_VALUE_SCANNER.PIECE;

        #region INICIALIZACION

        public Form_Inventario()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {
            if (CDb.isOpen)
            {
                LoadDataGridDetallePiezasColectadas();
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
        private void Form_Inventario_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            InitializeScanner();
            WindowState = FormWindowState.Maximized;
        }

        private void Form_Inventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseScanner();
            ((MainForm)this.ParentForm).closeChild.Set();
        }
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
            else if (keyData == (Keys.F3))
            {
                if (toolStripButton_ModoEliminarPiezas.Enabled)
                    toolStripButton_ModoEliminarPiezas.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void textBox_numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }
        private void textBox_float_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b' || e.KeyChar == ',')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }
        private void textBox_valueCapture_TextChanged(object sender, EventArgs e)
        {

        }
        private void borrarToolStripMenuItem_Eliminar_Click(object sender, EventArgs e)
        {
            BorrarRegistrosSeleccionadosDVGDetallePiezasColectadas();
        }

        #endregion

        #region EVENTOS MENU

        private void consultaInventario_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaLoteIngPiezasProduccionDlg dlg = new CViewConsultaLoteIngPiezasProduccionDlg();
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
        private void toolStripButton_ModoEliminarPiezas_Click(object sender, EventArgs e)
        {
            InPartRemovalMode ^= true;
            if (InPartRemovalMode)
            {
                if (DialogResult.Yes != MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Piezas Colectadas ," +
                    " a partir de este momento todas las piezas escaneadas seran eliminadas del registro de Inventario , permanece en este modo ?.",
                    "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    InPartRemovalMode ^= true;
                }
            }
            SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode);
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
                        tabControl_ProcesoEscaneo.Visible = false;
                        toolStripButton_ModoEliminarPiezas.Enabled = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        groupBox_detallesIngresosProduccion.Visible = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        toolStripButton_ModoEliminarPiezas.Enabled = false;
                        tabControl_ProcesoEscaneo.Visible = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        groupBox_detallesIngresosProduccion.Visible = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode = false);
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        toolStripButton_ModoEliminarPiezas.Enabled = true;
                        tabControl_ProcesoEscaneo.Visible = true;
                        tabControl_ProcesoEscaneo.SelectTab("tabPage_ProcesoEscaneo");
                        groupBox_datCaptura.Enabled = true;
                        groupBox_datCaptura.Visible = true;
                        groupBox_detallesIngresosProduccion.Visible = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "EN PROCESO DE ESCANEO", Color.Red);
                        SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode = false);
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
                SetDataReadPiece(codBarRead);

                try
                {
                    if (!InPartRemovalMode)
                    {
                        if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.PIECE)
                        {
                            if (await IsValidRegisterReadPiece())
                            {
                                /*si el usuario declaro el origen de la mercaderia se usa ese y si no se
                                  se usa el que posee registrado la pieza.*/
                                if (textBox_ubicacion.Tag != null)
                                    m_datReadPiece.Destino = textBox_ubicacion.Tag as CDestino;

                                if (CDb.RegisterReadPieceINV(dateTimePicker_fechaInventario.Value, m_datReadPiece))
                                {
                                    LoadDataGridDetallePiezasColectadas();
                                    LoadDataGridTotalesCapturas();
                                    SetMessageReadyScanner("La pieza fue registrada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar registrar una pieza em el inventario.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if(TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.CONTAINER)
                        {
                            if (await IsValidRegisterReadContenedor())
                            {
                                /*si el usuario declaro el origen de la mercaderia se usa ese y si no se
                                  se usa el que posee registrado el contenedor.*/
                                if (textBox_ubicacion.Tag != null)
                                    m_dataReadContenedor.Destino = textBox_ubicacion.Tag as CDestino;

                                if (CDb.RegisterContainerINV(dateTimePicker_fechaInventario.Value, m_dataReadContenedor))
                                {
                                    LoadDataGridDetallePiezasColectadas();
                                    LoadDataGridTotalesCapturas();
                                    SetMessageReadyScanner("Contenedor registrado con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar registrar un contenedor en el inventario. Verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.PIECE)
                        {
                            if (await IsValidDeleteReadPiece())
                            {
                                if (CDb.DeletePiezaINV(dateTimePicker_fechaInventario.Value,m_datReadPiece.Id))
                                {
                                    LoadDataGridDetallePiezasColectadas();
                                    LoadDataGridTotalesCapturas();
                                    SetMessageReadyScanner("La pieza fue eliminadade la captura del inventario !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar eliminar una pieza en el inventario.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.CONTAINER)
                        {
                            if (await IsValidDeleteReadContenedor())
                            {
                                if (CDb.DeleteContenedorINV(dateTimePicker_fechaInventario.Value ,m_dataReadContenedor.Id))
                                {
                                    LoadDataGridDetallePiezasColectadas();
                                    LoadDataGridTotalesCapturas();
                                    SetMessageReadyScanner("El contenedor fue eliminado de la captura del inventario !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar eliminar el contenedor en el inventario.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
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
            textBox_valueReadCodBar.Text = "";
            textBox_ubicacion.Tag = null;
            textBox_ubicacion.Text = null;

            LoadDataGridDetallePiezasColectadas();
            LoadDataGridTotalesCapturas();
        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            m_datReadPiece = null;
            m_dataReadContenedor = null;
            if (codeBarRead != "" && codeBarRead.Length >= 3 && codeBarRead[0] == 'A' && codeBarRead[codeBarRead.Length - 1] == 'A')
            {
                codeBarRead = codeBarRead.Remove(0, 1);
                codeBarRead = codeBarRead.Remove(codeBarRead.Length - 1, 1);
                Int32.TryParse(codeBarRead, out id);
                m_dataReadContenedor = CDb.GetContenedor(id);
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.CONTAINER;
            }
            else
            {
                Int32.TryParse(codeBarRead, out id);
                CPesada datpieza = CDb.GetPesada(id);
                if (datpieza != null)
                {
                    m_datReadPiece = new CPIP(datpieza);
                    m_datReadPiece.IdEstacionRegistration = CDb.m_OperadorActivo.m_idEstacion;
                    m_datReadPiece.IdOperadorRegistration = CDb.m_OperadorActivo.m_id;
                }
                else
                    m_datReadPiece = null;
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.PIECE;
            }

        }

        private async Task<bool> IsValidRegisterReadPiece()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_datReadPiece == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (m_datReadPiece.Producto.EsInsumo)
                {
                    SetMessageReadyScanner("La pieza colectada es un Insumo !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (CDb.EsBultoColectadoEnInventario(dateTimePicker_fechaInventario.Value,m_datReadPiece.Id.ToString()))
                {
                    SetMessageReadyScanner("La pieza ya esta colectada !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        private async Task<bool> IsValidDeleteReadPiece()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_datReadPiece == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (m_datReadPiece.Producto.EsInsumo)
                {
                    SetMessageReadyScanner("La pieza colectada es un Insumo !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        private async Task<bool> IsValidRegisterReadContenedor()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_dataReadContenedor == null)
                {
                    SetMessageReadyScanner("El contenedor colectado no existe  !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (CDb.EsBultoColectadoEnInventario(dateTimePicker_fechaInventario.Value, 'A'+m_dataReadContenedor.Id.ToString()+'A'))
                {
                    SetMessageReadyScanner("El Contenedor ya esta colectado !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        private async Task<bool> IsValidDeleteReadContenedor()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_dataReadContenedor == null)
                {
                    SetMessageReadyScanner("El contenedor colectado no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }

        private void SetVisible_ToolStripLabelModoEliminacion(bool visible)
        {
            Invoke((MethodInvoker)delegate
            {
                toolStripLabel_ModoEliminacion.Visible = visible;
            });
        }

        #endregion

        #region DATAGRID DETALLE DE PIEZAS INGRESADAS
        private void dataGridView_detallePiezasColectadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_detallePiezasColectadas.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDVGDetallePiezasColectadas();
                }
            }
        }

        private void LoadDataGridDetallePiezasColectadas()
        {
            DataTable dtDetallePiezasColectadas = new DataTable();

            try
            {
                //TIPO,NRO,FECHA,PRODUCTO,UBICACION
                dtDetallePiezasColectadas = CDb.GetDatSet_DetalleColeccionInventario(dateTimePicker_fechaInventario.Value);

                if (dtDetallePiezasColectadas != null)
                {
                    SetDataSource_DataGridViewSecure(dataGridView_detallePiezasColectadas,dtDetallePiezasColectadas);
                    SetFormatDGVDetallePiezasColectadasSecure();
                }
                else
                {
                    SetDataSource_DataGridViewSecure(dataGridView_detallePiezasColectadas, null);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Source + "-" + aex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetFormatDGVDetallePiezasColectadasSecure()
        {
            if (dataGridView_detallePiezasColectadas.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePiezasColectadasSecure(); }));
            }
            else
            {
                dataGridView_detallePiezasColectadas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasColectadas.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasColectadas.Columns["TIPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasColectadas.Columns["NRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasColectadas.Columns["NRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasColectadas.Columns["FECHA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasColectadas.Columns["FECHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasColectadas.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_detallePiezasColectadas.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasColectadas.Columns["UBICACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasColectadas.Columns["UBICACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasColectadas.Columns["PESO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasColectadas.Columns["PESO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }
        private bool BorrarRegistrosSeleccionadosDVGDetallePiezasColectadas()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_detallePiezasColectadas.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_detallePiezasColectadas.SelectedRows.Count;
                        int countDeletedRegisters = 0;

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de piezas colectadas para el inventario : {1}  , confirma la eliminación ", countSelectRegisters, DateTime.Now.ToString("dd-MM-yyyy"));
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idbulto;
                            string tipoBulto;
                            foreach (DataGridViewRow dgvr in dataGridView_detallePiezasColectadas.SelectedRows)
                            {
                                idbulto = CDb.GetCellDGVInt(dgvr, "NRO"); 
                                tipoBulto = CDb.GetCellDGVString(dgvr, "TIPO"); 

                                if (tipoBulto=="PIEZA")
                                {
                                    if (CDb.DeletePiezaINV(dateTimePicker_fechaInventario.Value, idbulto))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPiezaColectada, TYPE_CONTEXT_DBLOG.Inventario,
                                        string.Format("Se Eliminó del inventario la pieza colectada : {0}", idbulto));

                                        countDeletedRegisters++;
                                    }
                                }
                                else if(tipoBulto == "CAJA" || tipoBulto == "COMBO")
                                {
                                    if (CDb.DeleteContenedorINV(dateTimePicker_fechaInventario.Value, idbulto))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionContenedorColectado, TYPE_CONTEXT_DBLOG.Inventario,
                                        string.Format("Se Eliminó del inventario el contenedor colectado : {0}", idbulto));

                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);

                            dataGridView_detallePiezasColectadas.Update();
                            LoadDataGridDetallePiezasColectadas();
                            LoadDataGridTotalesCapturas();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Operacion no permitida para un Usuario no Supervisor", "Control de Acceso a usuarios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando un Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return borradoOk;
        }
        private void LoadDataGridTotalesCapturas()
        {
            DataTable dtTotalPiezas = new DataTable();

            try
            {
                //TIPO',PRODUCTO,UBICACION,BULTOS,PESO
                if (CDb.GetDatSet_TotalesColeccionInventario(dateTimePicker_fechaInventario.Value, out dtTotalPiezas))
                {
                    if (dtTotalPiezas != null)
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_totalesPiezasInventario, dtTotalPiezas);
                        SetFormatDGVTotalesPiezasInventario();
                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_totalesPiezasInventario, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de totales de piezas capturadas para el inventario en curso.", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(aex.Source + "-" + aex.Message, "Error de Base de Datos al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetFormatDGVTotalesPiezasInventario()
        {
            if (dataGridView_totalesPiezasInventario.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVTotalesPiezasInventario(); }));
            }
            else
            {  //TIPO',PRODUCTO,UBICACION,BULTOS,PESO
                dataGridView_totalesPiezasInventario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasInventario.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_totalesPiezasInventario.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_totalesPiezasInventario.Columns["UBICACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_totalesPiezasInventario.Columns["UBICACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_totalesPiezasInventario.Columns["BULTOS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_totalesPiezasInventario.Columns["BULTOS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasInventario.Columns["PESO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_totalesPiezasInventario.Columns["PESO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasInventario.Columns["PESO"].DefaultCellStyle.Format = "0.00";
            }
        }

        #endregion

        private void dateTimePicker_fechaEntrega_ValueChanged(object sender, EventArgs e)
        {
            LoadDataGridTotalesCapturas();
            LoadDataGridDetallePiezasColectadas();
        }

        private void button_EditValueFecha_Click(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Fecha de Inventario", "Fecha (dd-MM-yyyy) o (dd/MM/yyyy)", dateTimePicker_fechaInventario.Value.ToString("dd-MM-yyyy"), 15);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (IsValidDateTime(dlg.VALUE))
                {
                    dateTimePicker_fechaInventario.Value = GetDateTimeFromString(dlg.VALUE);
                }
                else
                {
                    MessageBox.Show("Error , edición de fecha con formato no valido . El formato valido puede ser dd-mm-yyyy o dd/mm/yyyy .", "VALIDACIÓN DE EDICIÓN DE FECHA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool IsValidDateTime(string dateTime)
        {
            string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy" };
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("es-ES"),
                                           DateTimeStyles.None, out parsedDateTime);
        }
        public DateTime GetDateTimeFromString(string strDateTime)
        {
            return DateTime.Parse(strDateTime, new CultureInfo("es-ES", true));
        }

        private void button_selecUbicacion_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_ubicacion.Tag != null ? ((CDestino)textBox_ubicacion.Tag).Id : 0;
            CABM_DestinosDlg abmDlg = new CABM_DestinosDlg(CDb.m_oleDbConnection, CABM_DestinosDlg.BEHAVIOR_MODE.SELECTION, idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.DestinoSelected.Id != 0)
            {
                textBox_ubicacion.Tag = abmDlg.DestinoSelected;
                textBox_ubicacion.Text = abmDlg.DestinoSelected.Nombre;
            }
            else
            {
                textBox_ubicacion.Tag = null;
                textBox_ubicacion.Text = "";
            }
        }

    }
}
