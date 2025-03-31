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
using StatusProgressBar;
using ZebraScannerLib;
using System.Threading;
using HidKeyboardScannerLib;

namespace MeatWeigherManager
{
    public partial class Form_Fraccionar : Form
    {
        public volatile int vueltas = 0;
        CDatScale m_DatPesaje;
        List<ScaleForm> m_listScales;
        CPesada m_datReadPiece;
        CZebraScannerLib m_scannerDRV;
        HidScannerLib m_scannerHID;


        private enum ESTADOS_APP
        {
            INICIADA,
            PARADA,
            ABIERTA,
            INABILITADA
        };

        enum TYPE_DELETE
        {
            OFF,
            PIEZAS,
        };

        TYPE_DELETE InRemovalMode { get; set; } = TYPE_DELETE.OFF;

        /// <summary>
        /// Indica que balanza esta visible en el dialogo
        /// </summary>
        private ScaleForm m_activeScaleForm;

        private ESTADOS_APP m_Estado;
        public bool EnableActionsClosing { get; private set; } = true;

        #region INICIALIZACION

        public Form_Fraccionar()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {

            Connect_Scales();

            Conectar_Impresora();

            InitializeScanner();

            if (!CDb.isOpen || !scaleSerialCtrl1.IsConnected)
                SetEstado(ESTADOS_APP.INABILITADA);
            else
                SetEstado(ESTADOS_APP.PARADA);
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
        }
        private void CloseScanner()
        {
            m_scannerDRV?.Close();
            m_scannerHID?.Close();
        }

        private bool Connect_Scales()
        {
            bool conectada = false;
            m_listScales = new List<ScaleForm>() { new ScaleForm(scaleSerialCtrl1,tabPageB1,toolStripStatusB1Valor, CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1]),
                                                   new ScaleForm(scaleSerialCtrl2,tabPageB2,toolStripStatusB2Valor, CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2]),
                                                   new ScaleForm(scaleSerialCtrl3,tabPageB3,toolStripStatusB3Valor, CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3])};
            m_DatPesaje = new CDatScale();

            foreach (ScaleForm sf in m_listScales)
            {
                if (!sf.ScaleSerialCtrl.IsConnected && sf.Enable)
                {
                    try
                    {
                        conectada = sf.ScaleSerialCtrl.Connect(new CCnfgScale()
                        {
                            PesoMaximoCero = sf.MaximoRangoZero,
                            PesoMinimoValido = sf.RangoPesoValidoInferior,
                            PesoMaximoValido = sf.RangoPesoValidoSuperior,
                            MaximaDispercionEstable = sf.MaximaDispercionEstable,
                            MsDeteccionEstable = sf.TiempoDeteccionEstable,
                            PortName = sf.ComName,
                            BaudRate = sf.Baudios,
                            DataBits = (short)sf.DataBits,
                            StopBits = (System.IO.Ports.StopBits)sf.StopBits,
                            HandShaque = (System.IO.Ports.Handshake)sf.HandShake,
                            Parity = (System.IO.Ports.Parity)sf.Parity,
                            Protocol= sf.Protocolo,
                            CountDecimals = sf.Decimales

                        });
                        sf.ScaleSerialCtrl.NameScale = sf.Name;

                        sf.ScaleSerialCtrl.OnNewWeight += ScaleSerialCtrl_OnNewWeight;
                        sf.ScaleSerialCtrl.OnException += ScaleSerialCtrl_OnException;

                        if (conectada)
                        {
                            CCommons.SetToolStripStatusLabel(sf.StatusScaleToolStrip, "CONECTADA", Color.Green);
                        }
                        else
                        {
                            CCommons.SetToolStripStatusLabel(sf.StatusScaleToolStrip, "ERROR", Color.Red);
                        }
                    }
                    catch (Exception error)
                    {
                        CCommons.SetToolStripStatusLabel(sf.StatusScaleToolStrip, "ERROR", Color.Red);
                    }
                }
            }
            SetActiveFirstScaleEnable();
            return conectada;
        }
        
        /// <summary>
        /// Establece en la variable m_activeScaleForm la primera balanza que se encuentra
        /// habilitada y conectada. Tambien activa y pone visible el TabPage que visualiza 
        /// a la balanza.
        /// </summary>
        private void SetActiveFirstScaleEnable()
        {
            SCALES scaleSelect = SCALES.SCALE1;

            m_activeScaleForm = null;
            foreach (ScaleForm sf in m_listScales)
            {
                if(sf.Enable && sf.ScaleSerialCtrl.IsConnected)
                {
                    m_activeScaleForm = sf;
                    sf.TabPageContainsScale.Visible = true;
                    sf.TabPageContainsScale.Select();
                    break;
                }
                scaleSelect++;
            }
            //si no se pudo colocar como activa a una balanza se oculta el contenedor TABCONTROL de
            //las balanzas.
            if (m_activeScaleForm == null)
            {
                tabControl_Balanzas.Visible = false;
            }
        }

        private bool Conectar_Impresora()
        {
            bool conectada = false;

            try
            {
                if (CPrintEtiZpl2_Win.SendFormatToPrinter(CConfigApp.m_nombreImpresora, CConfigApp.m_pathArchivoFormatosEtiquetas,CConfigApp.m_encodingNameOutputPrinter))
                {
                    conectada = true;
                    CCommons.SetToolStripStatusLabel(toolStripStatusImpresorValor, "ON LINE", Color.Green);
                }
                else
                {
                    MessageBox.Show("Error enviando el formato de etiquetas a la  impresora, verifique que la misma se encuentre conectada y encendida.", "ERROR IMPRESORA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CCommons.SetToolStripStatusLabel(toolStripStatusImpresorValor, "ERROR ENVIANDO FORMATO", Color.Red);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR DE CONEXION CON EL IMPRESOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CCommons.SetToolStripStatusLabel(toolStripStatusImpresorValor, "ERROR OFFLINE", Color.Red);
            }
            return conectada;
        }
        /// <summary>
        /// Desconecta todas las conecciones rs232 con las balanzas.
        /// Esto requiere de una operativa especial dado que los puertos com 
        /// tienen problemas al realizar acciones de apertura y cierre inmediatas.
        /// </summary>
        /// <returns></returns>
        private async Task Disconnect_ScalesAsync()
        {
            await Task.Run(() =>
            {
                foreach (ScaleForm sf in m_listScales)
                {
                    sf.ScaleSerialCtrl.Disconnect();
                }
            });
        }

        #endregion

        #region EVENTOS DEL FORM Y DE SUS CONTROLES
        private void Form_MeatWeigherManager_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            WindowState = FormWindowState.Maximized;
        }
        private async void Form_Fraccionar_FormClosing(object sender, FormClosingEventArgs e)
        {
            // el codigo contenido aqui es justificado solo porque los puertos como demoran en cerrarce y 
            // quedan en proceso de cierre aun cuando se cerro el form. Esto asegura que los eventos de FormClosed
            //disparados se ocacionen cuando las tareas aqui dentro se completen.
            //Este comportamiento solo se utiliza cuando cuando el Form Padre pide el cierre del hijo (Razon USERCLOSING).
            //Cuando el usuario cierra con la X al padre (MDIFROMCLOSING) se efectua normalemnte.
            if (EnableActionsClosing == true)
            {
                e.Cancel = true;
                await Disconnect_ScalesAsync();
                CloseScanner();
                EnableActionsClosing = false;
                Close();
            }
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
            SetText_ControlSecure(textBox_valueReadCodBar, canData);
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
                m_scannerDRV.Beep(BEEPLED_TYPE.RED_LED_OFF);
            });
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
                if (toolStripButton_modoEliminarPiezaFraccionada.Enabled)
                    toolStripButton_modoEliminarPiezaFraccionada.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textBox_producto_MouseHover(object sender, EventArgs e)
        {
            if (textBox_producto.Tag != null)
            {
                string productoPrimario = String.Format("Producto Primario: {0}", ((CProducto)textBox_producto.Tag).Nombre);
                ToolTip tt = new ToolTip();
                tt.Show(productoPrimario, textBox_producto, 0, 0, 1000);
            }
        }
        private void tabControl_Balanzas_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(!m_listScales[tabControl_Balanzas.SelectedIndex].Enable || !m_listScales[tabControl_Balanzas.SelectedIndex].ScaleSerialCtrl.IsConnected)
            {
                e.Cancel = true;
            }
            else
            {
                m_activeScaleForm = m_listScales[tabControl_Balanzas.SelectedIndex];
            }
        }
        private void textBox_taraPieza_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_taraDeFraccion.Text, "Tara", "Editar la Tara de la Fracción ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_taraDeFraccion.Text = dlg.VALUE;
            }
        }

        private void textBox_taraPieza_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBox_unidadesEnFraccion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox_unidadesEnFraccion_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_unidadesDeFraccion.Text, "Unidades", "Editar las Unidades en la Fracción ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_unidadesDeFraccion.Text = dlg.VALUE;
            }
        }

        #endregion

        #region EVENTOS MENU
        private void reimprimirEtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CPesada> listPesadas = CDb.GetWeightingFromSelectedDGVPesadas(dataGridView_detallePiezasFraccionadas, "PIEZA");

                if (listPesadas != null && listPesadas.Count > 0)
                {
                    foreach (CPesada pesada in listPesadas)
                    {
                        CLabel.PrintProduct(pesada, ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 2);
                    }
                }
            }
        }
        private void eliminarFracciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                if (BorrarRegistrosSeleccionadosDataGridPiezasFraccionadas())
                    CargarControlesConPiezaActiva();
            }
        }
        #endregion

        #region EVENTOS BUTTONS
        private void button_printLabelFather_Click(object sender, EventArgs e)
        {
            if (m_datReadPiece != null)
            {
                CPesada pieza = CDb.GetPesada(m_datReadPiece.Id);
                if (pieza != null)
                {
                    CLabel.PrintProduct(pieza, ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 2);
                }
            }
        }
        #endregion

        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            ClrControlesPieza();
            ClrControlesReadScanner();
            LoadDataGridPiezasFraccionadas();
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            ClrControlesPieza();
            SetEstado(ESTADOS_APP.PARADA);
        }
        private void toolStripButton_eliminarPiezaFraccionada_Click(object sender, EventArgs e)
        {
            if(InRemovalMode == TYPE_DELETE.PIEZAS)
            {
                InRemovalMode = TYPE_DELETE.OFF;
                toolStripButton_modoEliminarPiezaFraccionada.Image = global::MeatWeigherManager.Properties.Resources.eliminar_fraccion;
                SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO FRACCIONAMIENTO", Color.Black);
                SetVisibleCtrlSecure(tabControl_Balanzas, true);
                SetVisibleCtrlSecure(groupBox_datFraccion, true);
            }
            else
            { 
                if (DialogResult.Yes == MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Piezas Fraccionadas ," +
                    " a partir de este momento todas las piezas que sean escaneadas seran eliminadas del fraccionamiento de la pieza Padre, permanece en este modo ?.",
                    "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    InRemovalMode = TYPE_DELETE.PIEZAS;
                    toolStripButton_modoEliminarPiezaFraccionada.Image = global::MeatWeigherManager.Properties.Resources.eliminar_fraccion_activo;
                    SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO ELIMINACIÓN DE PIEZAS FRACCIONADAS", Color.Red);
                    SetVisibleCtrlSecure(tabControl_Balanzas, false);
                    SetVisibleCtrlSecure(groupBox_datFraccion, false);
                }
            }
        }
        private void button_selecDestino_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_destino.Tag != null ? ((CDestino)textBox_destino.Tag).Id : 0;
            CABM_DestinosDlg abmDlg = new CABM_DestinosDlg(CDb.m_oleDbConnection, CABM_DestinosDlg.BEHAVIOR_MODE.SELECTION, idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.DestinoSelected.Id != 0)
            {
                textBox_destino.Tag = abmDlg.DestinoSelected;
                textBox_destino.Text = abmDlg.DestinoSelected.Nombre;
            }
            else
            {
                ClrControlesDestino();
            }
        }
        #endregion

        #region EVENTOS BALANZA
        /// ***************************************************************
        /// CBalanza_OnNewWeight:
        /// Evento disparado por la clase CBalanza cuando hay un 
        /// nuevo peso.
        /// ***************************************************************
        private void ScaleSerialCtrl_OnNewWeight(object sender ,CDatScale datPesaje)
        {
            try
            {
                ScaleSerialCtrl.ScaleSerialCtrl scaleCtrl = sender as ScaleSerialCtrl.ScaleSerialCtrl;
                ScaleForm sf = m_listScales.Find(ctrl => ctrl.ScaleSerialCtrl == scaleCtrl);
                if (m_activeScaleForm == sf)
                {
                    ToolStripStatusLabel tssl = sf.StatusScaleToolStrip;

                    m_DatPesaje = datPesaje;
                    SetToolStripStatusLabelSecure(tssl, "OK", Color.Green);
                    RegistrarFraccion();
                }
            }
            catch (SystemException e)
            {

            }
        }

        /**********************************************************************************************
        * Funcion:        CBalanzaAsy_OnException
        * Descripcion:    Evento generado por la clase CBalanzaAsy cuando hay excepciones
        ***********************************************************************************************/
        private void ScaleSerialCtrl_OnException(object sender,EXCEPTION_CBALANZASERIALPORT exception)
        {
            try
            {
                ScaleSerialCtrl.ScaleSerialCtrl scaleCtrl = sender as ScaleSerialCtrl.ScaleSerialCtrl;
                ScaleForm sf = m_listScales.Find(ctrl => ctrl.ScaleSerialCtrl == scaleCtrl);
                ToolStripStatusLabel tssl = sf.StatusScaleToolStrip; 

                if (exception == EXCEPTION_CBALANZASERIALPORT.NO_RECEPTION)
                {
                    SetToolStripStatusLabelSecure(tssl,"ERROR - NO RECEPCION -", Color.Red);
                }
                else if (exception == EXCEPTION_CBALANZASERIALPORT.PROTOCOL_ERROR)
                {
                    SetToolStripStatusLabelSecure(tssl, "PROTOCOLO ERROR", Color.Red);
                }
                else if (exception == EXCEPTION_CBALANZASERIALPORT.PORT_ERROR)
                {
                    SetToolStripStatusLabelSecure(tssl, "DESCONECTADA", Color.Red);
                }
            }
            catch (SystemException e)
            {
            }
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
        /// SetEnableToolStripMenuItemSecure
        /// Esta Funcion es utilizada para cambiar el estado de true a false
        /// en la propiedad Enable de un Control de forma segura para que no 
        /// genere excepciones por accederce por varios threads.
        /// **************************************************************
        private void SetEnableToolStripMenuItemSecure(ToolStripMenuItem ctrl, bool enable)
        {
            if (ctrl.GetCurrentParent() != null)
            {
                if (ctrl.GetCurrentParent().InvokeRequired)
                {
                    ctrl.GetCurrentParent().Invoke(new MethodInvoker(delegate ()
                    { SetEnableToolStripMenuItemSecure(ctrl, enable); }));
                }
                else
                {
                    ctrl.Enabled = enable;
                }
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
        /// SetTextStatusLabelSecure
        /// Esta Funcion es utilizada para asignar un texto y color a un control
        /// statuslabel de forma segura.
        /// **************************************************************
        private void SetTextStatusLabelSecure(ToolStripStatusLabel ctrl, string text, Color color)
        {
            if (ctrl.GetCurrentParent() != null)
            {
                if (ctrl.GetCurrentParent().InvokeRequired)
                {
                    ctrl.GetCurrentParent().Invoke(new MethodInvoker(delegate ()
                    { SetTextStatusLabelSecure(ctrl, text, color); }));
                }
                else
                {
                    ctrl.Text = text;
                    ctrl.ForeColor = color;
                }
            }
        }
        
        /// **************************************************************
        /// SetEnableToolStripButtonSecure
        /// Esta Funcion es utilizada para cambiar el estado de true a false
        /// en la propiedad Enable de un Control de forma segura para que no 
        /// genere excepciones por accederce por varios threads.
        /// **************************************************************
        private void SetEnableToolStripButtonSecure(ToolStripButton ctrl, bool enable)
        {
            if (ctrl.GetCurrentParent() != null)
            {
                if (ctrl.GetCurrentParent().InvokeRequired)
                {
                    ctrl.GetCurrentParent().Invoke(new MethodInvoker(delegate ()
                    { SetEnableToolStripButtonSecure(ctrl, enable); }));
                }
                else
                {
                    ctrl.Enabled = enable;
                }
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
        private void SetTag_ControlSecure(Control ctrl, object tag)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetTag_ControlSecure(ctrl, tag); }));
            }
            else
            {
                ctrl.Tag = tag;
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
        /// <summary>
        /// Establece el texto que tendra un control y su color de forma segura.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private void SetTextColorCtrlSecure(Control ctrl, string text, Color color)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetTextColorCtrlSecure(ctrl, text, color); }));
            }
            else
            {
                ctrl.Text = text;
                ctrl.ForeColor = color;
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
                        SetVisibleCtrlSecure(tabControl_ProcesoFraccionamiento, false);
                        SetEnableCtrlSecure(groupBox_datPieza,false);
                        SetVisibleCtrlSecure(groupBox_datPieza,false);
                        SetVisibleCtrlSecure(tabControl_Balanzas, false);
                        SetEnableCtrlSecure(groupBox_lecturaScanner,false);
                        SetVisibleCtrlSecure(groupBox_lecturaScanner,false);
                        SetEnableCtrlSecure(groupBox_datPiezasFraccionadas,false);
                        SetVisibleCtrlSecure(groupBox_datPiezasFraccionadas,false);
                        SetTextStatusLabelSecure(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        SetEnableToolStripButtonSecure(toolStripButton_modoEliminarPiezaFraccionada,false);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        SetEnableToolStripButtonSecure(toolStripButton_Iniciar,true);
                        SetEnableToolStripButtonSecure(toolStripButton_Parar, false);
                        SetEnableToolStripButtonSecure(toolStripButton_modoEliminarPiezaFraccionada, false);
                        SetVisibleCtrlSecure(tabControl_ProcesoFraccionamiento, false);

                        SetVisibleCtrlSecure(groupBox_lecturaScanner,false);
                        SetVisibleCtrlSecure(groupBox_datPieza,false);
                        SetVisibleCtrlSecure(groupBox_datPiezasFraccionadas,false);
                        SetVisibleCtrlSecure(groupBox_datFraccion,false);
                        SetVisibleCtrlSecure(tabControl_Balanzas,false);

                        SetEnableCtrlSecure(groupBox_lecturaScanner,false);
                        SetEnableCtrlSecure(groupBox_datPieza,false);
                        SetEnableCtrlSecure(groupBox_datPiezasFraccionadas,false);
                        SetEnableCtrlSecure(groupBox_datFraccion,false);

                        SetTextStatusLabelSecure(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        InRemovalMode = TYPE_DELETE.OFF;
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        SetEnableToolStripButtonSecure(toolStripButton_Iniciar, false);
                        SetEnableToolStripButtonSecure(toolStripButton_Parar, true);
                        SetEnableToolStripButtonSecure(toolStripButton_modoEliminarPiezaFraccionada, false);
                        SetVisibleCtrlSecure(tabControl_ProcesoFraccionamiento, true);

                        SetVisibleCtrlSecure(groupBox_lecturaScanner, true);
                        SetVisibleCtrlSecure(groupBox_datPieza, true);
                        SetVisibleCtrlSecure(groupBox_datPiezasFraccionadas, false);
                        SetVisibleCtrlSecure(groupBox_datFraccion, false);
                        SetVisibleCtrlSecure(tabControl_Balanzas, false);

                        SetEnableCtrlSecure(groupBox_lecturaScanner, true);
                        SetEnableCtrlSecure(groupBox_datPieza, false);
                        SetEnableCtrlSecure(groupBox_datPiezasFraccionadas, false);
                        SetEnableCtrlSecure(groupBox_datFraccion, false);

                        SetTextStatusLabelSecure(toolStripStatusProcessValue, "ACTIVO PARA COLECTAR PIEZA PADRE", Color.Red);
                        InRemovalMode = TYPE_DELETE.OFF;
                        break;
                    }
                case ESTADOS_APP.ABIERTA:
                    {
                        SetEnableToolStripButtonSecure(toolStripButton_Iniciar, false);
                        SetEnableToolStripButtonSecure(toolStripButton_Parar, true);
                        SetEnableToolStripButtonSecure(toolStripButton_modoEliminarPiezaFraccionada, true);
                        SetVisibleCtrlSecure(tabControl_ProcesoFraccionamiento, true);

                        SetVisibleCtrlSecure(groupBox_lecturaScanner, true);
                        SetVisibleCtrlSecure(groupBox_datPieza, true);
                        SetVisibleCtrlSecure(groupBox_datPiezasFraccionadas, true);
                        SetVisibleCtrlSecure(groupBox_datFraccion, true);
                        SetVisibleCtrlSecure(tabControl_Balanzas, true);

                        SetEnableCtrlSecure(groupBox_lecturaScanner, true);
                        SetEnableCtrlSecure(groupBox_datPieza, true);
                        SetEnableCtrlSecure(groupBox_datPiezasFraccionadas, true);
                        SetEnableCtrlSecure(groupBox_datFraccion, true);

                        SetTextStatusLabelSecure(toolStripStatusProcessValue, "ACTIVO PARA CREAR NUEVA PIEZA FRACCIONADA", Color.Red);
                        InRemovalMode = TYPE_DELETE.OFF;
                        break;
                    }
            }
        }
        #endregion

        #region FUNCIONES GENERALES

        private void RegistrarFraccion()
        {
            if (m_Estado == ESTADOS_APP.ABIERTA)
            {
                if (EsValidoFraccionarPieza())
                {
                    if (GenerarNuevaPiezaFraccionada())
                    {
                        CargarControlesConPiezaActiva();
                        LoadDataGridPiezasFraccionadas();
                    }
                }
            }
        }

        private bool GenerarNuevaPiezaFraccionada()
        {
            bool createdOk = false;
            CPesada newPiece = new CPesada(m_datReadPiece);
            newPiece.PesoNeto = GetPesoNetoPiezaFraccionada();
            newPiece.PesoTara = GetPesoTaraPiezaFraccionada();
            newPiece.Destino.Id = ((CDestino)textBox_destino.Tag).Id;
            newPiece.Unidades = GetUnidadesPiezaFraccionada();
            newPiece.IdEstacion = CDb.m_OperadorActivo.m_idEstacion;
            newPiece.IdGrupo = 0;
            newPiece.IdPiezaPadre = m_datReadPiece.Id;
            newPiece.PesoRemitido = (m_datReadPiece.PesoRemitido / m_datReadPiece.Unidades) * newPiece.Unidades;
            newPiece.FechaVencimiento = m_datReadPiece.FechaVencimiento;

            try
            {
                if (CDb.RegistrarPesada(ref newPiece,false))
                {
                    float newPesoNetoPiezaPadre = m_datReadPiece.PesoNeto - newPiece.PesoNeto;
                    int newUnidadesPiezaPadre =  m_datReadPiece.Unidades <= newPiece.Unidades ? 1 : m_datReadPiece.Unidades - newPiece.Unidades;
                    float newPesoRemitidoPiezaPadre = (m_datReadPiece.PesoRemitido / m_datReadPiece.Unidades)*newUnidadesPiezaPadre;

                    if (CDb.ActualizarPesoNetoUnidadesPesada(m_datReadPiece.Id, newPesoNetoPiezaPadre, newUnidadesPiezaPadre,newPesoRemitidoPiezaPadre))
                    {
                        CLabel.PrintProduct(newPiece, ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, m_DatPesaje.CantDecimales);
                        //actualizo el peso neto y unidades de la pieza padre en la clase activa
                        m_datReadPiece.PesoNeto = newPesoNetoPiezaPadre;
                        m_datReadPiece.Unidades = newUnidadesPiezaPadre;
                        m_datReadPiece.PesoRemitido = newPesoRemitidoPiezaPadre;
                        createdOk = true;
                    }
                    else
                    {
                        MessageBox.Show("Error de Base de Datos al intentar actualizar el peso neto y unidades de la pieza padre del fraccionamiento.", "GENERAR UNA NUEVA PIEZA FRACCIONADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error de Base de Datos al intentar generar una nueva pieza fraccionada.", "GENERAR UNA NUEVA PIEZA FRACCIONADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception excep)
            {
                MessageBox.Show("Error de Base de Datos al intentar generar una nueva Pieza Fraccionada.  "+excep.Message, "GENERAR UNA NUEVA PIEZA FRACCIONADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return createdOk;
        }


        private bool EsValidoFraccionarPieza()
        {
            bool isValid = false;

            if (textBox_destino.Tag != null)
            {
                if(textBox_unidadesDeFraccion.Text != "" && Convert.ToInt32(textBox_unidadesDeFraccion.Text) > 0)
                {
                    if(m_datReadPiece.PesoNeto > GetPesoNetoPiezaFraccionada())
                    {
                        isValid = true;
                    }
                    else
                    {
                        MessageBox.Show("Error , la nueva pieza generada en el fraccionamiento no puede tener un peso mayor o igual a la pieza Padre.", "VALIDACIÓN DE NUEVA PIEZA FRACCIONADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error , no ha seleccionado las unidades para la nueva pieza a Fraccionar", "VALIDACIÓN DE NUEVA PIEZA FRACCIONADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error , no ha seleccionado un destino", "VALIDACIÓN DE NUEVA PIEZA FRACCIONADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isValid;
        }

        private float GetPesoNetoPiezaFraccionada()
        {
            float pesoTara= GetPesoTaraPiezaFraccionada();
            return !m_DatPesaje.isTareActive ? m_DatPesaje.PesoNeto - pesoTara: m_DatPesaje.PesoNeto;
        }

        private float GetPesoTaraPiezaFraccionada()
        {
            float pesoTara;
            if (!m_DatPesaje.isTareActive)
            {
                float.TryParse(textBox_taraDeFraccion.Text, out pesoTara);
            }
            else
            {
                pesoTara = m_DatPesaje.PesoTara;
            }
            return pesoTara;
        }

        private int GetUnidadesPiezaFraccionada()
        {
            return Convert.ToInt32(textBox_unidadesDeFraccion.Text);
        }

        private async void RegisterReadPiece(string codBarRead)
        {
            if (m_Estado == ESTADOS_APP.INICIADA || (m_Estado == ESTADOS_APP.ABIERTA && InRemovalMode == TYPE_DELETE.OFF))
            {
                CPesada oldPiece = m_datReadPiece != null ? new CPesada(m_datReadPiece) : null;

                SetDataReadPiece(codBarRead);
                
                try
                {
                    if (await IsValidAddReadPiece())
                    {
                        CargarControlesConProductoActivo();
                        CargarControlesConPiezaActiva();
                        LoadDataGridPiezasFraccionadas();
                        SetMessageReadyScanner("La pieza padre fue seleccionada con exito !!.", Color.Green);
                        ClrControlesPiezaFraccionada();
                        m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                        SetEstado(ESTADOS_APP.ABIERTA);
                        await SetOnOffLedOK();
                    }
                    else
                    {
                        if(oldPiece == null || !oldPiece.Equals(m_datReadPiece))
                        {
                            ClrControlesPiezaFraccionada();
                        }
                        m_datReadPiece = oldPiece != null ? new CPesada(oldPiece) : null;
                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al cargar la pieza padre para iniciar el proceso de fraccionamiento. Verifique la conexion con la base de datos. " + cdbe.Message, "SELECCIÓN DE PIEZA PADRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (m_Estado == ESTADOS_APP.ABIERTA && InRemovalMode == TYPE_DELETE.PIEZAS)
            {
                CPesada piezaReadToDelete = GetDataReadPiece(codBarRead);
                try
                {
                    if (await IsValidDeteteReadPiece(piezaReadToDelete))
                    {
                        if (CDb.BorrarPieza(piezaReadToDelete.Id))
                        {
                            float pesoNetoToUpdate = m_datReadPiece.PesoNeto + piezaReadToDelete.PesoNeto;
                            int unidadesToUpdate = m_datReadPiece.Unidades + piezaReadToDelete.Unidades;
                            float pesoRemitidoToUpdate = m_datReadPiece.PesoRemitido + piezaReadToDelete.PesoRemitido;

                            CDb.ActualizarPesoNetoUnidadesPesada(m_datReadPiece.Id, pesoNetoToUpdate, unidadesToUpdate,pesoRemitidoToUpdate);

                            m_datReadPiece.PesoNeto = pesoNetoToUpdate;
                            m_datReadPiece.Unidades = unidadesToUpdate;
                            m_datReadPiece.PesoRemitido = pesoRemitidoToUpdate;

                            CargarControlesConPiezaActiva();
                            LoadDataGridPiezasFraccionadas();
                            SetMessageReadyScanner("La pieza fue eliminada con exito !!.", Color.Green);
                            m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                            await SetOnOffLedOK();
                        }
                        else
                        {
                            MessageBox.Show("Se ha producido un error al intentar eliminar una pieza fraccionada .verifique la conexion con la base de datos.", "ELIMINACIÓN DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al registrar una pieza fraccionada.verifique la conexion con la base de datos. " + cdbe.Message, "ELIMINACIÓN DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            m_datReadPiece = null;
            Int32.TryParse(codeBarRead, out id);
            m_datReadPiece = CDb.GetPesada(id);
        }
        private CPesada GetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            Int32.TryParse(codeBarRead, out id);
            return CDb.GetPesada(id);
        }
        private async Task<bool> IsValidAddReadPiece()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string detailResult;
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
                else if (!CDb.IsValidPartForDivicion(m_datReadPiece.Id,out detailResult))
                {
                    SetMessageReadyScanner(detailResult , Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;

                return verificacionOk;
            });
        }
        private async Task<bool> IsValidDeteteReadPiece(CPesada pieceToDelete)
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string detailResult;
                if (pieceToDelete == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (pieceToDelete.Id == m_datReadPiece.Id)
                {
                    SetMessageReadyScanner("No puede eliminar la pieza Origen del Fraccionamiento !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (pieceToDelete.Producto.EsInsumo)
                {
                    SetMessageReadyScanner("La pieza colectada es un Insumo !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidPartDelete(pieceToDelete.Id,out detailResult))
                {
                    SetMessageReadyScanner(detailResult, Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (CDb.IsPartFather(pieceToDelete.Id))
                {
                    SetMessageReadyScanner("La pieza colectada es primaria , no puede ser eliminada !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;

                return verificacionOk;
            });
        }


        private void SetTextToolStripLabel(ToolStripLabel ctrlTSLabel,string text,Color color)
        {
            Invoke((MethodInvoker)delegate
            {
                ctrlTSLabel.Text = text;
                ctrlTSLabel.ForeColor = color;
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
        private void SetMessageReadyScanner(string text, Color color)
        {
            SetTextColorCtrlSecure(label_detalleLectura, text, color);
        }

        //obtiene una referencia al control toolstrip activo del form
        public ToolStrip GetActiveToolStrip()
        {
            return toolStrip_buttons;
        }

        private void SetEnableToolStripButtomsStartStop(bool enable)
        {
            toolStripButton_Iniciar.Enabled = enable;
            toolStripButton_Parar.Enabled = enable;
        }

        private void ClrControlesPieza()
        {
            label_numPiezaPadre.Text = "-----";
            label_LotePiezaPadre.Text = "-----";
            label_pesoNetoPiezaPadre.Text = "-----";
            label_unidadesPiezaPadre.Text = "-----";
            textBox_producto.Text = "";
            textBox_taraDeFraccion.Text = "";
            textBox_destino.Tag = null;
            textBox_destino.Text = "";
            textBox_unidadesDeFraccion.Text = "";
        }

        private void ClrControlesPiezaFraccionada()
        {
            Invoke((MethodInvoker)delegate
            {
                textBox_taraDeFraccion.Text= "";
                textBox_destino.Tag=null;
                textBox_destino.Text= "";
                textBox_unidadesDeFraccion.Text="";
            });
        }

        

        private void ClrControlesDestino()
        {
            textBox_destino.Text = "";
            textBox_destino.Tag = null;
        }

        private void ClrControlesProducto()
        {
            textBox_producto.Text = "";
        }
        private void ClrControlesReadScanner()
        {
            SetMessageReadyScanner("",Color.Black);
            SetText_ControlSecure(textBox_valueReadCodBar, "------");
        }
        private void CargarControlesConPiezaActiva()
        {
            if (IsDisposed)
                UpdateCtrlsActivePiece();
            else
            {
                BeginInvoke(new Action(() =>
                {
                    UpdateCtrlsActivePiece();
                }));
            }
        }

        private void UpdateCtrlsActivePiece()
        {
            if (m_datReadPiece != null)
            {
                label_numPiezaPadre.Text = m_datReadPiece.Id.ToString();
                label_LotePiezaPadre.Text = m_datReadPiece.FechaHora.ToShortDateString();
                label_pesoNetoPiezaPadre.Text = m_datReadPiece.PesoNeto.ToString();
                label_unidadesPiezaPadre.Text = m_datReadPiece.Unidades.ToString();
            }
        }

        private void CargarControlesConProductoActivo()
        {
            if (IsDisposed)
                LoadTextBoxProducto();
            else
            {
                BeginInvoke(new Action(()=>
                {
                    LoadTextBoxProducto();
                }));
            }
        }

        private void LoadTextBoxProducto()
        {
            if (m_datReadPiece != null)
            {
                textBox_producto.Text = m_datReadPiece.Producto.NombreEtiL1;
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL2);
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL3);
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL4);
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL5);
            }
        }
        #endregion
        
        #region GRILLA DE PIEZAS FRACCIONADAS
        private void ClrControlesDetallePiezasFraccionadas()
        {
            dataGridView_detallePiezasFraccionadas.DataSource = null;
        }
        private void LoadDataGridPiezasFraccionadas()
        {
            DataSet dsPiezasFraccionadas = new DataSet();

            try
            {
                //PIEZA,PRODUCTO,PESO_NETO
                if (CDb.GetDatSet_PiezasHijas(m_datReadPiece==null ? 0: m_datReadPiece.Id, out dsPiezasFraccionadas))
                {
                    if (dsPiezasFraccionadas.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasFraccionadas, dsPiezasFraccionadas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasFraccionadas, "PIEZAS");
                        SetFormatDGVDetallePiezasContenidasCajaSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasFraccionadas, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas fraccionadas", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetFormatDGVDetallePiezasContenidasCajaSecure()
        {
            if (dataGridView_detallePiezasFraccionadas.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePiezasContenidasCajaSecure(); }));
            }
            else
            {
                //PIEZA,PRODUCTO,PESO_NETO

                dataGridView_detallePiezasFraccionadas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasFraccionadas.Columns["PESO_REMITIDO"].Visible= false;
                dataGridView_detallePiezasFraccionadas.Columns["PIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasFraccionadas.Columns["PIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasFraccionadas.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasFraccionadas.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasFraccionadas.Columns["UNDS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasFraccionadas.Columns["UNDS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private bool BorrarRegistrosSeleccionadosDataGridPiezasFraccionadas()
        {
            bool borradoOk = false;
            float pesoNetoToUpdate;
            int unidadesToUpdate;
            float pesoRemitidoToUpdate;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_detallePiezasFraccionadas.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_detallePiezasFraccionadas.SelectedRows.Count;
                        int countDeletedRegisters = 0;

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de piezas fraccionadas , confirma la eliminación ", countSelectRegisters);
                        if (MessageBox.Show(aviso, "CONFIRMACIÓN DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPieza;
                            foreach (DataGridViewRow dgvr in dataGridView_detallePiezasFraccionadas.SelectedRows)
                            {
                                idPieza = Convert.ToInt32(dgvr.Cells["PIEZA"].Value);
                                string detailResult;
                                if (CDb.IsValidPartDelete(idPieza,out detailResult))
                                {
                                    if (CDb.BorrarPieza(idPieza))
                                    {
                                        pesoNetoToUpdate = m_datReadPiece.PesoNeto + CDb.GetCellDGVFloat(dgvr,"NETO");
                                        unidadesToUpdate = m_datReadPiece.Unidades + CDb.GetCellDGVInt(dgvr, "UNDS");
                                        pesoRemitidoToUpdate = m_datReadPiece.PesoRemitido + CDb.GetCellDGVFloat(dgvr, "PESO_REMITIDO");

                                        CDb.ActualizarPesoNetoUnidadesPesada(m_datReadPiece.Id, pesoNetoToUpdate, unidadesToUpdate,pesoRemitidoToUpdate);

                                        m_datReadPiece.PesoNeto = pesoNetoToUpdate;
                                        m_datReadPiece.Unidades = unidadesToUpdate;
                                        m_datReadPiece.PesoRemitido = pesoRemitidoToUpdate;

                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPieza, TYPE_CONTEXT_DBLOG.Fraccionamiento,
                                        string.Format("Se Eliminó la pieza : {0} que habia sido fraccionada.", idPieza));

                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);

                            if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar " + countDeletedRegisters + " registros de " + countSelectRegisters + " seleccionados.", "Protección de Borrado de Piezas Fraccionadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("No se han eliminado registros de piezas fraccionadas dado que las mismas fueron egresadas.", "Protección de Borrado de Piezas Fraccionadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            LoadDataGridPiezasFraccionadas();
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

        private void dataGridView_detallePiezasFraccionadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_detallePiezasFraccionadas.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDataGridPiezasFraccionadas();
                    CargarControlesConPiezaActiva();
                }
            }
        }

        #endregion

    }
}
