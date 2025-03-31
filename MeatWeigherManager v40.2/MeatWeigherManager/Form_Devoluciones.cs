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
    public partial class Form_Devoluciones : Form
    {
        CPIP  m_datReadPiece;
        CContenedor m_dataReadContenedor;
        
        CZebraScannerLib m_scannerDRV;
        HidScannerLib m_scannerHID;


        DataTable m_dtPiezasReingresadas;

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

        private TYPE_READ_VALUE_SCANNER TypeReadValueScanner { get; set; } = TYPE_READ_VALUE_SCANNER.PIECE;

        #region INICIALIZACION

        public Form_Devoluciones()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {
            if (CDb.isOpen)
            {
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
        private void Form_Devoluciones_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            InitializeScanner();
            WindowState = FormWindowState.Maximized;
        }

        private void Form_Devoluciones_FormClosing(object sender, FormClosingEventArgs e)
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
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        
        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            Crear_DataTablePiezasReingresadas();
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
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        tabControl_ProcesoEscaneo.Visible = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoEscaneo.Visible = true;
                        tabControl_ProcesoEscaneo.SelectTab("tabPage_ProcesoEscaneo");
                        groupBox_datCaptura.Enabled = true;
                        groupBox_datCaptura.Visible = true;
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
                SetDataReadPiece(codBarRead);

                try
                {
                    if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.PIECE)
                    {
                        if (await IsValidDevolutionReadPiece())
                        {
                            if (CDb.RegistrarDevolucionPieza(m_datReadPiece.Id))
                            {
                                SumarPiezaReingresadaA_DataGrid(m_datReadPiece);
                                SetMessageReadyScanner("La pieza fue reingresada a stock con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al intentar reingresar una pieza a stock . verifique la conexion con la base de datos.", "REINGRESO DE PIEZA A STOCK ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if(TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.CONTAINER)
                    {
                        if (await IsValidDevolutionReadCaja())
                        {
                            if (CDb.RegistrarDevolucionContenedor(m_dataReadContenedor.Id))
                            {
                                SumarPiezaReingresadaA_DataGrid(m_dataReadContenedor);
                                SetMessageReadyScanner("El Conteneor ha sido retornado a stock con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al intentar reingresar un contenedor a stock. verifique la conexion con la base de datos.", "REINGRESO DE CONTENEDOR A STOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al intentar reingresar piezas a stock .verifique la conexion con la base de datos. " + cdbe.Message, "REINGRESO DE PIEZA A STOCK ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DateTime dt = DateTime.Now;
            textBox_valueReadCodBar.Text = "";
            Clear_DataTablePiezasReingresadas();
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

        private async Task<bool> IsValidDevolutionReadPiece()
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
                else if (!CDb.IsValidPartForReturn(m_datReadPiece.Id))
                {
                    SetMessageReadyScanner("Esta pieza no fue egresada o esta pendiente de egreso!!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        private async Task<bool> IsValidDevolutionReadCaja()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_dataReadContenedor == null)
                {
                    SetMessageReadyScanner("La caja colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidContainerForReturn(m_dataReadContenedor.Id))
                {
                    SetMessageReadyScanner("Este contenedor no esta egresado o el pedido de Egreso se encuentra abierto !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;
                return verificacionOk;
            });
        }
        #endregion

        #region DATAGRID PIEZAS REINGRESADAS

        private void Clear_DataTablePiezasReingresadas()
        {
            Invoke((MethodInvoker)delegate
            {
                m_dtPiezasReingresadas.Clear();
                dataGridView_PiezasReingresadas.DataSource = null;
            });
        }
        private void Crear_DataTablePiezasReingresadas()
        {
            Invoke((MethodInvoker)delegate
            {
                m_dtPiezasReingresadas = new DataTable();
                m_dtPiezasReingresadas.Columns.Add(new DataColumn("PIEZA", typeof(Int32)));
                m_dtPiezasReingresadas.Columns.Add(new DataColumn("PRODUCTO", typeof(string)));
                m_dtPiezasReingresadas.Columns.Add(new DataColumn("PESO", typeof(float)));
                dataGridView_PiezasReingresadas.DataSource = m_dtPiezasReingresadas;

                dataGridView_PiezasReingresadas.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            });
        }

        private void SumarPiezaReingresadaA_DataGrid(CPIP pieza)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    DataRow dr = m_dtPiezasReingresadas.NewRow();
                    dr["PIEZA"] = pieza.Id;
                    dr["PRODUCTO"] = pieza.Producto.Nombre;
                    dr["PESO"] = pieza.PesoNeto;
                    m_dtPiezasReingresadas.Rows.Add(dr);
                    dataGridView_PiezasReingresadas.Refresh();
                }
                catch (Exception aex)
                {
                    MessageBox.Show(aex.Source + "-" + aex.Message, "Error agregando pieza a la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
        private void SumarPiezaReingresadaA_DataGrid(CContenedor contenedor)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    DataRow dr = m_dtPiezasReingresadas.NewRow();
                    dr["PIEZA"] = contenedor.Id;
                    dr["PRODUCTO"] = contenedor.Producto.Nombre;
                    dr["PESO"] = contenedor.PesoNeto;
                    m_dtPiezasReingresadas.Rows.Add(dr);
                    dataGridView_PiezasReingresadas.Refresh();
                }
                catch (CDbException aexdb)
                {
                    MessageBox.Show(aexdb.Source + "-" + aexdb.Message, "Error de Base de Datos agregando la pieza a la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception aex)
                {
                    MessageBox.Show(aex.Source + "-" + aex.Message, "Error agregando pieza a la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        #endregion

    }
}
