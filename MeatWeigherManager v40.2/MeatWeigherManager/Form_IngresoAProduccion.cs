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
    public partial class Form_IngresoAProduccion : Form
    {
        CPIP  m_datReadPiece;
        CContenedor m_dataReadCaja;
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
        /// <summary>
        /// establece el estado de captura normal o en modo eliminacion de piezas colectadas
        /// </summary>
        public bool InPartRemovalMode { get; private set; } = false;

        private TYPE_READ_VALUE_SCANNER TypeReadValueScanner { get; set; } = TYPE_READ_VALUE_SCANNER.PIECE;

        #region INICIALIZACION

        public Form_IngresoAProduccion()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {
            if (CDb.isOpen)
            {
                LoadDataGridPiezasIngresadasLote();
                SetEstado(ESTADOS_APP.PARADA);
            }
            else
            {
                SetEstado(ESTADOS_APP.INABILITADA);
            }
        }

        private void InitializeScanner()
        {
            if(ConfigApp.CConfigApp.m_hostInterfaceScaneer==HostInterfaceScanner.SNAPI_CoreScanner)
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

            //inicializa evento de simulacion de lectura de scanner por edicion
            OnNewDataEditScanner += M_scannerDRV_OnNewDataScanner;

            if (ConfigApp.CConfigApp.m_hostInterfaceScaneer == HostInterfaceScanner.HID_KeyboarEmulation)
            {
                m_scannerHID = new HidScannerLib(this, textBox_valueReadCodBar);
                m_scannerHID.OnNewDataScanner += M_scannerDRV_OnNewDataScanner;
            }
        }
        private void CloseScanner()
        {
            m_scannerDRV?.Close();
            m_scannerHID?.Close();
        }

        #endregion

        #region EVENTOS DEL FORM Y DE SUS CONTROLES
        private void Form_IngresoAProduccion_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            InitializeScanner();
            WindowState = FormWindowState.Maximized;
        }

        private void Form_IngresoAProduccion_FormClosing(object sender, FormClosingEventArgs e)
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
        /// <summary>
        /// Permite al Operador simular la lectura del scanner editando su valor de codigo de barras de la pieza.
        /// Dispara un evento que genera el mismo efecto que lograria la lectura del escanner.
        /// </summary>
        private void textBox_valueReadCodBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(CConfigApp.m_permiteSimularLecturaScanner)
            {
                CEditValueTouchDlg dlg = new CEditValueTouchDlg("", "Numero", "Editar Numero de Pieza", CEditValueTouchDlg.TYPE_VALUE.NUMERIC, 10);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox_valueReadCodBar.Text = dlg.VALUE;
                    OnNewDataEditScanner?.Invoke(dlg.VALUE);
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
            BorrarRegistrosSeleccionadosDVGDetalleCapturaIP();
        }

        private void button_selectSector_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_sector.Tag != null ? ((CSector)textBox_sector.Tag).Id : 0;
            CABM_SectoresDlg abmDlg = new CABM_SectoresDlg(CDb.m_oleDbConnection, CABM_SectoresDlg.BEHAVIOR_MODE.SELECTION, idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.SectorSelected.Id != 0)
            {
                textBox_sector.Tag = abmDlg.SectorSelected;
                CargarControlesConSectorActivo();
            }
            else
            {
                ClrControlesSector();
            }
        }

        #endregion

        #region EVENTOS MENU

        private void consultaIngresosProduccionLote_ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    " a partir de este momento todas las piezas escaneadas seran eliminadas del registro de Ingreso a Producción , permanece en este modo ?.",
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
                        ToolStripMenuItem_Consultas.Enabled = false;
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
                        ToolStripMenuItem_Consultas.Enabled = true;
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
                        ToolStripMenuItem_Consultas.Enabled = true;
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
            int idSector;
            if (m_Estado == ESTADOS_APP.INICIADA)
            {
                SetDataReadPiece(codBarRead);

                try
                {
                    if (!InPartRemovalMode)
                    {
                        if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.PIECE)
                        {
                            if (await IsValidEnterReadPart())
                            {
                                idSector = (textBox_sector.Tag as CSector).Id;

                                if (CDb.RegisterReadPartIP(m_datReadPiece,idSector))
                                {
                                    LoadDataGridPiezasIngresadasLote();
                                    LoadDataGridTotalesCapturasIP();
                                    SetMessageReadyScanner("La pieza fue registrada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar registrar un ingreso de pieza a producción.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                if (CDb.EliminarPiezaIngresadaProduccion(m_datReadPiece.Id))
                                {
                                    LoadDataGridPiezasIngresadasLote();
                                    LoadDataGridTotalesCapturasIP();
                                    SetMessageReadyScanner("La pieza fue eliminada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar eliminar una pieza ingresada en producción.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DateTime dt = DateTime.Now;
            label_Lote.Text = String.Format("{0:D2}{1:D2}{2:D4}", dt.Day, dt.Month, dt.Year);
            textBox_valueReadCodBar.Text = "";
            LoadDataGridPiezasIngresadasLote();
            LoadDataGridTotalesCapturasIP();
        }

        private void CargarControlesConSectorActivo()
        {
            if (textBox_sector.Tag != null)
            {
                textBox_sector.Text = ((CSector)textBox_sector.Tag).Nombre;
            }
        }

        private void ClrControlesSector()
        {
            textBox_sector.Text = "";
            textBox_sector.Tag = null;
        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            m_datReadPiece = null;
            m_dataReadCaja = null;
            if (codeBarRead != "" && codeBarRead.Length >= 3 && codeBarRead[0] == 'A' && codeBarRead[codeBarRead.Length - 1] == 'A')
            {
                codeBarRead = codeBarRead.Remove(0, 1);
                codeBarRead = codeBarRead.Remove(codeBarRead.Length - 1, 1);
                Int32.TryParse(codeBarRead, out id);
                m_dataReadCaja = CDb.GetCaja(id);
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.BOX;
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

        private async Task<bool> IsValidEnterReadPart()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string detailResult;
                if (textBox_sector.Tag == null)
                {
                    SetMessageReadyScanner("Debe seleccionar un Sector de Producción !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (m_datReadPiece == null)
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
                else if (!CDb.IsValidPartForProductionInput(m_datReadPiece.Id,out detailResult))
                {
                    SetMessageReadyScanner(detailResult, Color.Red);
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
                else if (!CDb.FindFieldTable("DLP", "idPesaje", m_datReadPiece.Id.ToString()))
                {
                    SetMessageReadyScanner("La pieza no esta Ingresada !!.", Color.Red);
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
        private void dataGridView_detallePiezasIngresadasLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_detallePiezasIngresadasLote.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDVGDetalleCapturaIP();
                }
            }
        }

        private void LoadDataGridPiezasIngresadasLote()
        {
            DataSet dsPiezasIngresadasLote = new DataSet();

            try
            {

                if (CDb.GetDatSet_PiezasIngresadasLote(DateTime.Now, out dsPiezasIngresadasLote))
                {
                    if (dsPiezasIngresadasLote.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasIngresadasLote,dsPiezasIngresadasLote);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasIngresadasLote,"PIEZAS");
                        SetFormatDGVDetallePiezasIngresadasLoteSecure();
                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasIngresadasLote, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas ingresadas a produccion en el lote actual.", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void SetFormatDGVDetallePiezasIngresadasLoteSecure()
        {
            if (dataGridView_detallePiezasIngresadasLote.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePiezasIngresadasLoteSecure(); }));
            }
            else
            {
                dataGridView_detallePiezasIngresadasLote.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasIngresadasLote.Columns["IDPIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasIngresadasLote.Columns["IDPIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasIngresadasLote.Columns["FECHA_INGRESO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasIngresadasLote.Columns["FECHA_INGRESO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasIngresadasLote.Columns["LOTE_PIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasIngresadasLote.Columns["LOTE_PIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasIngresadasLote.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePiezasIngresadasLote.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasIngresadasLote.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePiezasIngresadasLote.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasIngresadasLote.Columns["TROPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePiezasIngresadasLote.Columns["TROPA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasIngresadasLote.Columns["TIPIF"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePiezasIngresadasLote.Columns["TIPIF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasIngresadasLote.Columns["SECTOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasIngresadasLote.Columns["SECTOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasIngresadasLote.Columns["OI"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasIngresadasLote.Columns["OI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasIngresadasLote.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasIngresadasLote.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private bool BorrarRegistrosSeleccionadosDVGDetalleCapturaIP()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_detallePiezasIngresadasLote.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_detallePiezasIngresadasLote.SelectedRows.Count;
                        int countDeletedRegisters = 0;

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de piezas ingresadas al Lote: {1}  , confirma la eliminación ", countSelectRegisters, DateTime.Now.ToString("ddMMyyyy"));
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPieza;
                            foreach (DataGridViewRow dgvr in dataGridView_detallePiezasIngresadasLote.SelectedRows)
                            {
                                idPieza = Convert.ToInt32(dgvr.Cells["IDPIEZA"].Value);
                                if (!CDb.IsPartInEgresos(idPieza))
                                {
                                    if (CDb.BorrarRegistroTabla("DLP","idpesaje",idPieza.ToString()))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPiezaEnIngresoAProduccion, TYPE_CONTEXT_DBLOG.IngresoAProduccion,
                                        string.Format("Se Eliminó el Ingreso a Produccion de la pieza : {0}", idPieza));
                                        
                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);

                            if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar " + countDeletedRegisters + " registros de " + countSelectRegisters + " seleccionados , dado que los registros de piezas que ya fueron egresadas no podran ser eliminados.", "Protección de Borrado de Ingresos a Produccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("No se han eliminado registros de piezas colectadas dado que los mismos pertenecen a piezas ya egresadas.", "Protección de Borrado de Ingresos a Produccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            dataGridView_detallePiezasIngresadasLote.Update();
                            LoadDataGridPiezasIngresadasLote();
                            LoadDataGridTotalesCapturasIP();
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
        private void LoadDataGridTotalesCapturasIP()
        {
            DataSet dsTotalPiezas = new DataSet();

            try
            {

                if (CDb.GetDatSet_TotalesPiezasCapturadas(DateTime.Now, out dsTotalPiezas))
                {
                    if (dsTotalPiezas.Tables.Contains("TOTAL_PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_totalesPiezasIngresadasLote, dsTotalPiezas);
                        SetDataMember_DataGridViewSecure(dataGridView_totalesPiezasIngresadasLote, "TOTAL_PIEZAS");
                        SetFormatDGVTotalesCapturaIPSecure();
                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_totalesPiezasIngresadasLote, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de pesadas registradas en el Lote", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void SetFormatDGVTotalesCapturaIPSecure()
        {
            if (dataGridView_totalesPiezasIngresadasLote.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVTotalesCapturaIPSecure(); }));
            }
            else
            {
                dataGridView_totalesPiezasIngresadasLote.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasIngresadasLote.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_totalesPiezasIngresadasLote.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_totalesPiezasIngresadasLote.Columns["SECTOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasIngresadasLote.Columns["SECTOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_totalesPiezasIngresadasLote.Columns["CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasIngresadasLote.Columns["CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasIngresadasLote.Columns["UNIDADES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasIngresadasLote.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasIngresadasLote.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasIngresadasLote.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        #endregion

    }
}
