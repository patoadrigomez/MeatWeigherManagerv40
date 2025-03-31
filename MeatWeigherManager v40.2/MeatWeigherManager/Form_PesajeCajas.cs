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
    public partial class Form_PesajeCajas : Form
    {
        public volatile int vueltas = 0;
        CDatScale m_DatPesaje;
        CContenedor m_datCajaActiva;
        CContenedor m_datReadCaja;
        List<ScaleForm> m_listScales;
        CPesada m_datReadPiece;
        CZebraScannerLib m_scannerDRV;
        HidScannerLib m_scannerHID;

        CProducto m_productoCajaActiva;


        private enum MODE_REGISTRATION_BOX
        {
            WHITOUTWEIGHT,
            WEIGHING
        };
        private enum ESTADOS_APP
        {
            INICIADA,
            PARADA,
            INABILITADA
        };

        private enum ESTADOS_INICIAR
        {
            CREAR,
            ELIMINAR_PIEZAS_EN_CAJA_ABIERTA,
            ELIMINAR_PIEZAS_EN_CAJA_CERRADA,
            ELIMINAR_CAJAS,
        };

        MODE_REGISTRATION_BOX ModeRegistration { get; set; } = MODE_REGISTRATION_BOX.WEIGHING;
        ESTADOS_INICIAR EstadoIniciar { get; set; } = ESTADOS_INICIAR.CREAR;

        /// <summary>
        /// Indica que balanza esta visible en el dialogo
        /// </summary>
        private ScaleForm m_activeScaleForm;

        private ESTADOS_APP m_Estado;
        public bool EnableActionsClosing { get; private set; } = true;

        #region INICIALIZACION

        public Form_PesajeCajas()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {

            Connect_Scales();

            Conectar_Impresora();

            InitializeScanner();

            InitializeButtomRegistrar();

            if (!CDb.isOpen)
                SetEstado(ESTADOS_APP.INABILITADA);
            else
                SetEstado(ESTADOS_APP.PARADA);
        }

        private void InitializeButtomRegistrar()
        {
            button_registrarCaja.Visible = CDb.isOpen && CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR;
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
                            Protocol=sf.Protocolo,
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
                if (CPrintEtiZpl2_Win.SendFormatToPrinter(CConfigApp.m_nombreImpresora, CConfigApp.m_pathArchivoFormatosEtiquetas, CConfigApp.m_encodingNameOutputPrinter))
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
        private async void Form_PesajeCajas_FormClosing(object sender, FormClosingEventArgs e)
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
                m_scannerDRV?.Beep(BEEPLED_TYPE.RED_LED_OFF);
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
        private void tabControl_Balanzas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
        private void textBox_taraCaja_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_taraCaja.Text, "Tara", "Editar la Tara de Caja ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_taraCaja.Text = dlg.VALUE;
            }
        }

        private void textBox_taraCaja_KeyPress(object sender, KeyPressEventArgs e)
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
        private void button_registrarCaja_EnabledChanged(object sender, EventArgs e)
        {
            button_registrarCaja.BackgroundImage = button_registrarCaja.Enabled ? global::MeatWeigherManager.Properties.Resources.Registrar_Caja_Enable : global::MeatWeigherManager.Properties.Resources.Registrar_caja_disable;
        }

        private void button_registrarCaja_Click(object sender, EventArgs e)
        {
            RegistrarCajaSinPesar();
        }

        #endregion

        #region EVENTOS MENU

        private void consultaPesajes_CajasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaCajasPesadasDlg dlg = new CViewConsultaCajasPesadasDlg();
            dlg.ShowDialog();
        }
        
        private void reimprimirEtiquetaCajasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CContenedor> listCajasPesadas = CDb.GetWeightingFromSelectedDGVCajas(dataGridView_detallePesajeCajasEnLote);

                if (listCajasPesadas != null && listCajasPesadas.Count > 0)
                {
                    foreach(CContenedor caja in listCajasPesadas)
                    {
                        CLabel.PrintCaja(caja, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 2);
                    }
                }
            }
        }

        #endregion
        
        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            ClrControlesCaja();
            CDb.EliminarPiezasCajaTemp();
            LoadDataGridPiezasContenidasCajaAbierta();
            LoadDataGridPesadasCajasLote();
            EstadoIniciar = ESTADOS_INICIAR.CREAR;
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            ClrControlesCaja();
            SetEstado(ESTADOS_APP.PARADA);
        }

        private void toolStripButton_eliminarPiezaContenida_Click(object sender, EventArgs e)
        {
            if(GetEstado() == ESTADOS_APP.INICIADA && EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_ABIERTA)
            {
                EstadoIniciar = ESTADOS_INICIAR.CREAR;
                SetEstado(ESTADOS_APP.INICIADA);
            }
            else
            {
                if (!isGridPiezasContenidasVoid())
                {
                    if (DialogResult.Yes == MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Piezas Contenidas en caja Abierta," +
                    " a partir de este momento todas las piezas que sean escaneadas seran eliminadas de la caja, permanece en este modo ?.",
                    "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        EnableCtrlsCrearCaja(false);
                        EstadoIniciar = ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_ABIERTA;
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO ELIMINAR PIEZAS CAJA ABIERTA", Color.Red);
                    }
                }
                else
                {
                    MessageBox.Show("No puede ingresar al modo Eliminacion de Piezas en Caja Abierta dado que no hay ninguna caja en proceso", "VALIDACIÓN DE INGRESO A MODO ELIMINACIÓN DE PIEZAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton_modoQuitarPiezaContenidaCajaCerrada_Click(object sender, EventArgs e)
        {
            if (GetEstado() == ESTADOS_APP.INICIADA && EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_CERRADA)
            {
                EstadoIniciar = ESTADOS_INICIAR.CREAR;
                SetEstado(ESTADOS_APP.INICIADA);
                ClrAllControls();
                SetVisibleButtonRePrintLabelCaja(false);
                SetEnableCtrlSecure(groupBox_datCaja, false);
            }
            else
            {
                if (isGridPiezasContenidasVoid())
                {
                    if (DialogResult.Yes == MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Piezas Contenidas en caja Cerrada," +
                        " a partir de este momento todas las piezas que sean escaneadas seran eliminadas de la caja, permanece en este modo ?.",
                        "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        EnableCtrlsCrearCaja(false);
                        EstadoIniciar = ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_CERRADA;
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO ELIMINAR PIEZAS CAJA CERRADA", Color.Red);
                        ClrAllControls();
                        SetVisibleButtonRePrintLabelCaja();
                        SetEnableCtrlSecure(groupBox_datCaja, true);
                    }
                }
                else
                {
                    MessageBox.Show("No puede ingresar al modo Eliminacion de Piezas en Caja Cerrada dado que aún posee piezas colectadas para el armado de una caja", "VALIDACIÓN DE INGRESO A MODO ELIMINACIÓN DE PIEZAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void toolStripButton_modoEliminacionCaja_Click(object sender, EventArgs e)
        {
            if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
            {
                if (GetEstado() == ESTADOS_APP.INICIADA && EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_CAJAS)
                {
                    EstadoIniciar = ESTADOS_INICIAR.CREAR;
                    SetEstado(ESTADOS_APP.INICIADA);
                    ClrAllControls();
                }
                else
                {
                    if (isGridPiezasContenidasVoid() || (!isGridPiezasContenidasVoid() && EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_CERRADA))
                    {
                        if (DialogResult.Yes == MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Cajas ," +
                        " a partir de este momento todas las cajas seran eliminadas como tambien los vinculos con las piezas contenidas, permanece en este modo ?.",
                        "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            EnableCtrlsCrearCaja(false);
                            EstadoIniciar = ESTADOS_INICIAR.ELIMINAR_CAJAS;
                            SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO ELIMINAR CAJAS", Color.Red);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No puede ingresar al modo Eliminacion de Cajas dado que aún posee piezas colectadas para el armado de una caja", "VALIDACIÓN DE INGRESO A MODO ELIMINACIÓN DE CAJAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Acción permitida unicamente para Administradores", "ELIMINACIÓN DE CAJAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ModeRegistration = MODE_REGISTRATION_BOX.WEIGHING;
                    RegistrarCajaPesando();
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
        /// SetVisibleCtrlSecure
        /// Esta Funcion es utilizada para cambiar el estado de true a false
        /// en la propiedad Visible de un Control de forma segura para que no 
        /// genere excepciones por accederce por varios threads.
        /// **************************************************************
        private void SetVisibleCtrlSecure(ToolStripLabel ctrl, bool enable)
        {
            if (ctrl.GetCurrentParent() != null)
            {
                if (ctrl.GetCurrentParent().InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    { SetVisibleCtrlSecure(ctrl, enable); }));
                }
                else
                {
                    ctrl.Visible = enable;
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
                        tabControl_ProcesoPesaje.Visible = false;
                        groupBox_datCaja.Enabled = false;
                        groupBox_datCaja.Visible = false;
                        scaleSerialCtrl1.Enabled = false;
                        scaleSerialCtrl1.Visible = false;
                        groupBox_lecturaScanner.Enabled = false;
                        groupBox_lecturaScanner.Visible = false;
                        groupBox_datPiezasContenidas.Enabled = false;
                        groupBox_datPiezasContenidas.Visible = false;
                        groupBox_detallesPesadasCajas.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        toolStripButton_modoEliminarPiezaContenidaCajaAbierta.Enabled = false;
                        toolStripButton_modoQuitarPiezaContenidaCajaCerrada.Enabled = false;
                        toolStripButton_modoEliminacionCaja.Enabled = false;
                        button_registrarCaja.Enabled = false;
                        button_rePrintLabelBox.Enabled = false;
                        button_rePrintLabelBox.Visible = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, false);
                        groupBox_selectProductoCaja.Visible = false;
                        tabControl_Balanzas.Enabled = false;
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        tabControl_ProcesoPesaje.Visible = false;
                        groupBox_lecturaScanner.Enabled = false;
                        groupBox_lecturaScanner.Visible = false;
                        groupBox_datCaja.Enabled = false;
                        groupBox_datCaja.Visible = false;
                        groupBox_datPiezasContenidas.Enabled = false;
                        groupBox_datPiezasContenidas.Visible = false;
                        groupBox_detallesPesadasCajas.Visible = false;
                        scaleSerialCtrl1.Enabled = false;
                        scaleSerialCtrl1.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_modoEliminarPiezaContenidaCajaAbierta.Enabled = false;
                        toolStripButton_modoQuitarPiezaContenidaCajaCerrada.Enabled = false;
                        toolStripButton_modoEliminacionCaja.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        button_registrarCaja.Enabled = false;
                        button_rePrintLabelBox.Enabled = false;
                        button_rePrintLabelBox.Visible = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, false);
                        groupBox_selectProductoCaja.Visible = false;
                        tabControl_Balanzas.Enabled = false;
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoPesaje.Visible = true;
                        groupBox_lecturaScanner.Enabled = true;
                        groupBox_lecturaScanner.Visible = true;
                        groupBox_datCaja.Enabled = true;
                        groupBox_datCaja.Visible = true;
                        groupBox_selectProductoCaja.Visible = true;
                        groupBox_selectProductoCaja.Enabled = true;
                        groupBox_selectionDestino.Enabled = true;
                        groupBox_taraDeCaja.Enabled = true;
                        scaleSerialCtrl1.Enabled = true;
                        scaleSerialCtrl1.Visible = true;
                        groupBox_datPiezasContenidas.Enabled = false;
                        groupBox_datPiezasContenidas.Visible = true;
                        groupBox_detallesPesadasCajas.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_modoEliminarPiezaContenidaCajaAbierta.Enabled = true;
                        toolStripButton_modoQuitarPiezaContenidaCajaCerrada.Enabled = true;
                        toolStripButton_modoEliminacionCaja.Enabled = true;
                        button_registrarCaja.Enabled = true;
                        button_rePrintLabelBox.Enabled = true;
                        button_rePrintLabelBox.Visible = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, true);
                        tabControl_ProcesoPesaje.SelectTab("tabPage_ProcesoPesaje");
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO CREAR CAJA", Color.Green);
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "ACTIVO PARA COLECTAR Y PESAR", Color.Green);
                        tabControl_Balanzas.Enabled = true;
                        break;
                    }
            }
        }
        #endregion

        #region FUNCIONES GENERALES

        private void RegistrarCajaPesando()
        {
            if (m_Estado == ESTADOS_APP.INICIADA)
            {
                if (EsValidoRegistrarCajaPesando())
                {
                    if (GenerarNuevaCajaPesando())
                    {
                        CargarControlesLoteConCajaActiva();
                        LoadDataGridPesadasCajasLote();
                        ClrControlesDetallePiezasContenidasCaja();
                        ClrControlesProducto();
                        if (!RegistrarInsumos(productoInsumosCtrl.GetInsumos()))
                        {
                            MessageBox.Show("Se ha producido un error al registrar los Insumos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
        }
        private void RegistrarCajaSinPesar()
        {
            if (m_Estado == ESTADOS_APP.INICIADA)
            {
                if (EsValidoRegistrarCajaSinPesar())
                {
                    if (GenerarNuevaCajaSinPesar())
                    {
                        CargarControlesLoteConCajaActiva();
                        LoadDataGridPesadasCajasLote();
                        ClrControlesDetallePiezasContenidasCaja();
                        ClrControlesProducto();
                        if (!RegistrarInsumos(productoInsumosCtrl.GetInsumos()))
                        {
                            MessageBox.Show("Se ha producido un error al registrar los Insumos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private bool GenerarNuevaCajaPesando()
        {
            bool createdOk = false;
            m_datCajaActiva = new CContenedor();
            m_datCajaActiva.PesoTara = m_DatPesaje.isTareActive ? m_DatPesaje.PesoTara: Convert.ToSingle(textBox_taraCaja.Text);
            m_datCajaActiva.PesoNeto = m_DatPesaje.isTareActive ? m_DatPesaje.PesoNeto: m_DatPesaje.PesoNeto - m_datCajaActiva.PesoTara;
            m_datCajaActiva.Destino = ((CDestino)textBox_destino.Tag);
            m_datCajaActiva.m_idEstacion = CDb.m_OperadorActivo.m_idEstacion;
            m_datCajaActiva.Producto = m_productoCajaActiva;
            m_datCajaActiva.m_undsContenidas = CDb.GetTotalUndsPartsContainTempCaja();
            m_datCajaActiva.IdTipo = "CAJ";
            try
            {
                if (CDb.InsertNewContainer(ref m_datCajaActiva))
                {
                    CLabel.PrintCaja(m_datCajaActiva, m_DatPesaje.CantDecimales);
                    createdOk = true;
                }
                else
                {
                    MessageBox.Show("Error de Base de Datos al intentar generar una nueva Caja.", "GENERAR UNA NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception excep)
            {
                MessageBox.Show("Error de Base de Datos al intentar generar una nueva Caja.  "+excep.Message, "GENERAR UNA NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return createdOk;
        }

        private bool GenerarNuevaCajaSinPesar()
        {
            bool createdOk = false;
            m_datCajaActiva = new CContenedor();
            m_datCajaActiva.PesoNeto = CDb.GetTotalNetoPartsContainCaja(); 
            m_datCajaActiva.PesoTara = Convert.ToSingle(textBox_taraCaja.Text);
            m_datCajaActiva.Destino = ((CDestino)textBox_destino.Tag);
            m_datCajaActiva.m_idEstacion = CDb.m_OperadorActivo.m_idEstacion;
            m_datCajaActiva.Producto = m_productoCajaActiva;
            m_datCajaActiva.m_undsContenidas = CDb.GetTotalUndsPartsContainTempCaja();
            m_datCajaActiva.IdTipo = "CAJ";
            try
            {
                if (CDb.InsertNewContainer(ref m_datCajaActiva))
                {
                    CLabel.PrintCaja(m_datCajaActiva);
                    createdOk = true;
                }
                else
                {
                    MessageBox.Show("Error de Base de Datos al intentar generar una nueva Caja.", "GENERAR UNA NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show("Error de Base de Datos al intentar generar una nueva Caja.  " + excep.Message, "GENERAR UNA NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return createdOk;
        }

        /// <summary>
        /// Registra todos los insumos utilizados en la pesada.
        /// </summary>
        /// <param name="listInsumos"></param>
        /// <returns></returns>
        private bool RegistrarInsumos(List<CItemInsumoProductoEnProceso> listInsumos)
        {
            bool registrados = false;
            if (listInsumos == null || listInsumos.Count == 0)
                registrados = true;
            else
            {
                registrados = CDb.RegistrarMovimientoInsumos(TYPE_INSUMO_MOV.Egreso, TYPE_INSUMO_PROC.ConformadoContenedor, m_datCajaActiva.Id, listInsumos);
            }
            return registrados;
        }

        private bool EsValidoRegistrarCajaPesando()
        {
            bool isValid = false;
            float pesoNetoContenido = 0.0f;
            float pesoNetoBalanza = 0.0f;
            float pesoTara = 0.0f;
            float desvioPesoCaja = 0.0f;

            float.TryParse(textBox_taraCaja.Text, out pesoTara);

            if (m_DatPesaje.isTareActive || pesoTara != 0.0f)
            {
                if(m_DatPesaje.isTareActive)
                    pesoTara = 0;
                pesoNetoBalanza = m_DatPesaje.PesoNeto - pesoTara;
                    

                if (m_productoCajaActiva != null)
                {
                    if (textBox_destino.Tag != null)
                    {
                        if (productoInsumosCtrl.esTodosInsumosConfirmados())
                        {
                            //verifico si hay piezas a vincular a la nueva caja y obtengo
                            //la sumatoria de peso neto de su contenido.
                            pesoNetoContenido = CDb.GetTotalNetoPartsContainCaja();
                            if (pesoNetoContenido != 0.0f)
                            {
                                desvioPesoCaja = ((pesoNetoContenido - pesoNetoBalanza) / pesoNetoContenido) * 100;

                                if (Math.Abs(desvioPesoCaja) <= CConfigApp.m_toleranceWeightBox)
                                {
                                    isValid = true;
                                }
                                else
                                {
                                    MessageBox.Show("Error , no coincide el peso de la caja con su contenido declarado.", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error , no hay registros de piezas a vincular a la nueva Caja", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No ha confirmado todos los insumos de la caja.", "VALIDACION DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error , no ha seleccionado un destino", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error , no ha seleccionado el producto Tipo Caja", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error , no ha definido en la balanza una Tara de Caja", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isValid;
        }

        private bool EsValidoRegistrarCajaSinPesar()
        {
            bool isValid = false;
            float pesoNetoContenido = 0.0f;
            float pesoTaraEditado = 0.0f;

            float.TryParse(textBox_taraCaja.Text, out pesoTaraEditado);

            if (pesoTaraEditado != 0.0f)
            {
                if (m_productoCajaActiva != null)
                {
                    if (textBox_destino.Tag != null)
                    {
                        if (productoInsumosCtrl.esTodosInsumosConfirmados())
                        {
                            //verifico si hay piezas a vincular a la nueva caja y obtengo
                            //la sumatoria de peso neto de su contenido.
                            pesoNetoContenido = CDb.GetTotalNetoPartsContainCaja();
                            if (pesoNetoContenido != 0.0f)
                            {
                                isValid = true;
                            }
                            else
                            {
                                MessageBox.Show("Error , no hay registros de piezas a vincular a la nueva Caja", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No ha confirmado todos los insumos de la caja.", "VALIDACION DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error , no ha seleccionado un destino", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error , no ha seleccionado el producto Tipo Caja", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error , no ha definido una Tara de Caja", "VALIDACIÓN DE NUEVA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isValid;
        }

        private async void RegisterReadPiece(string codBarRead)
        {
            if (m_Estado == ESTADOS_APP.INICIADA)
            {
                SetDataReadPiece(codBarRead);
                
                try
                {
                    if (EstadoIniciar == ESTADOS_INICIAR.CREAR)
                    {
                        if (m_productoCajaActiva != null)
                        {
                            if (await IsValidAddReadPiece())
                            {
                                if (CDb.AgregarPiezaCajaTemp(m_datReadPiece.Id))
                                {
                                    CargarControlesConProductoContenidoActivo();
                                    LoadDataGridPiezasContenidasCajaAbierta();
                                    SetMessageReadyScanner("La pieza fue registrada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al registrar una pieza para vincularla a una nueva caja.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No ha seleccionado el producto CAJA a crear.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if(EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_ABIERTA)
                    {
                        if (await IsValidDeteteReadPieceForBoxOpen())
                        {
                            if (CDb.EliminarPiezaCajaTemp(m_datReadPiece.Id))
                            {
                                LoadDataGridPiezasContenidasCajaAbierta();
                                SetMessageReadyScanner("La pieza fue eliminada con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al intentar eliminar una pieza contenida en la caja .verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_PIEZAS_EN_CAJA_CERRADA)
                    {
                        if (await IsValidDeleteReadPieceForBoxClose())
                        {
                            SetActiveBoxFromIdPiece(m_datReadPiece.Id);
                            CargarControlesConCajaActiva();
                            LoadDataGridPiezasContenidasCajaCerradaPorIdPieza(m_datReadPiece.Id);

                            string consulta = String.Format("Desea quitar de la caja Nº: {0} la Pieza contenida Nº: {1} ?",m_datCajaActiva.Id,m_datReadPiece.Id);
                            if (DialogResult.Yes == MessageBox.Show(consulta,"Permiso para Eliminar Pieza Contenida ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                if (CDb.EliminarPiezaContenedor(m_datReadPiece.Id))
                                {
                                    SetActiveBoxFromIdBox(m_datCajaActiva.Id);
                                    LoadDataGridPiezasContenidasCajaCerradaPorIdCaja(m_datCajaActiva.Id);
                                    LoadDataGridPesadasCajasLote();
                                    SetMessageReadyScanner("La pieza fue quitada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar borrar el registro de la pieza contenida en la caja .verifique la conexion con la base de datos.", "ELIMINACIÓN DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else if (EstadoIniciar == ESTADOS_INICIAR.ELIMINAR_CAJAS)
                    {
                        if (await IsValidDeteteReadBox())
                        {
                            if (CDb.DesarmarContenedor(m_datReadCaja.Id))
                            {

                                /// elimina los insumos que fueron registrados para el contenedor.
                                CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.ConformadoContenedor, m_datReadCaja.Id);

                                LoadDataGridPiezasContenidasCajaAbierta();
                                LoadDataGridPesadasCajasLote();
                                SetMessageReadyScanner("La caja fue eliminada con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al eliminar una caja .verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al registrar una pieza para vincularla a una nueva caja.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            m_datReadPiece = null;
            m_datReadCaja = null;
            if (codeBarRead != "" && codeBarRead.Length >= 3 && codeBarRead[0] == 'A' && codeBarRead[codeBarRead.Length - 1] == 'A')
            {
                codeBarRead = codeBarRead.Remove(0, 1);
                codeBarRead = codeBarRead.Remove(codeBarRead.Length - 1, 1);
                Int32.TryParse(codeBarRead, out id);
                m_datReadCaja = CDb.GetCaja(id);
            }
            else
            {
                Int32.TryParse(codeBarRead, out id);
                m_datReadPiece = CDb.GetPesada(id);
            }
        }

        private bool SetActiveBoxFromIdPiece(int idPiece)
        {
            m_datCajaActiva = CDb.GetCajaDesdePieza(idPiece);
            m_productoCajaActiva = m_datCajaActiva.Producto;
            return m_datCajaActiva != null;
        }

        private bool SetActiveBoxFromIdBox(int idCaja)
        {
            m_datCajaActiva = CDb.GetCaja(idCaja);
            return m_datCajaActiva != null;
        }
        
        private void EnableCtrlsCrearCaja(bool enable=true)
        {
            button_registrarCaja.Enabled = enable;
            tabControl_Balanzas.Enabled = enable;
            groupBox_selectProductoCaja.Enabled = enable;
            groupBox_selectionDestino.Enabled = enable;
            groupBox_taraDeCaja.Enabled = enable;
        }

        private async Task<bool> IsValidAddReadPiece()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string resultStr;
                if (m_datReadPiece == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidPartForIncludeToCaja(m_productoCajaActiva.Id, m_datReadPiece.Id, out resultStr))
                {
                    SetMessageReadyScanner(resultStr, Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;

                return verificacionOk;
            });
        }

        private async Task<bool> IsValidDeteteReadPieceForBoxOpen()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string resultStr;
                if (m_datReadPiece == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidPartForDeleteToOpenContainer(m_datReadPiece.Id, out resultStr))
                {
                    SetMessageReadyScanner(resultStr, Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;

                return verificacionOk;
            });
        }
        private async Task<bool> IsValidDeleteReadPieceForBoxClose()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string resultStr;
                if (m_datReadPiece == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidPartForDeleteToClosedContainer(m_datReadPiece.Id, out resultStr))
                {
                    SetMessageReadyScanner(resultStr, Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;

                return verificacionOk;
            });
        }

        
        private async Task<bool> IsValidDeteteReadBox()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_datReadCaja == null)
                {
                    SetMessageReadyScanner("La caja colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidDisarmContainer(m_datReadCaja.Id))
                {
                    SetMessageReadyScanner("Esta caja no esta en stock , ya fue egresada !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else
                    verificacionOk = true;

                return verificacionOk;
            });
        }

        private void SetTextToolStripLabel(ToolStripLabel ctrlTSLabel, string text, Color color)
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

        private bool isGridPiezasContenidasVoid()
        {
            return dataGridView_detallePiezasContenidasCaja.Rows.Count == 0;
        }

        private float GetPesoNeto()
        {
            return m_DatPesaje.PesoNeto - m_DatPesaje.PesoTara;
        }
        private float GetPesoTara()
        {
            return m_DatPesaje.PesoTara;
        }

        private void ClrAllControls()
        {
            Invoke((MethodInvoker)delegate
            {
                ClrControlesCaja();
                ClrControlesDestino();
                ClrControlesTara();
                ClrControlesReadEscanner();
                ClrControlesDetallePiezasContenidasCaja();
                ClrListaInsumos();
            });
        }

        private void ClrListaInsumos()
        {
            productoInsumosCtrl.IdProducto = 0;
        }
        private void ClrControlesCaja()
        {
            label_numCAJA.Text = "-----";
            label_Lote.Text = "-----";
            textBox_producto.Text = "";
            textBox_productoCaja.Text = "";
            label_productoAContener.Text = "-";
            textBox_taraCaja.Text = "";
            textBox_destino.Text = "";
            m_datCajaActiva = null;
            m_productoCajaActiva = null;
        }

        private void ClrControlesReadEscanner()
        {
            textBox_valueReadCodBar.Text = "------";
            label_detalleLectura.Text = null;
        }
        private void ClrControlesDestino()
        {
            textBox_destino.Text = "";
            textBox_destino.Tag = null;
        }
        private void ClrControlesTara()
        {
            textBox_taraCaja.Text = "";
        }

        private void ClrControlesProducto()
        {
            textBox_producto.Text = "";
        }
        private void CargarControlesLoteConCajaActiva()
        {
            if (m_datCajaActiva != null)
            {
                label_numCAJA.Text = m_datCajaActiva.Id.ToString();
                label_Lote.Text = m_datCajaActiva.m_fechaHoraCreacion.ToShortDateString();
            }
        }

        private void CargarControlesConCajaActiva()
        {
            Invoke((MethodInvoker)delegate
            {
                CargarControlesConProductoCajaActiva();
                CargarControlesConTaraActiva();
                CargarControlesConDestinoActivo();
                CargarControlesLoteConCajaActiva();
                CargarControlesConProductoContenidoActivo();
            });
        }
        private void SetVisibleButtonRePrintLabelCaja(bool visible=true)
        {
            Invoke((MethodInvoker)delegate
            {
                button_rePrintLabelBox.Enabled = visible;
                button_rePrintLabelBox.Visible = visible;
            });
        }

        private void CargarControlesConTaraActiva()
        {
            Invoke((MethodInvoker)delegate
            {
                textBox_taraCaja.Text = m_datCajaActiva.Producto.PesoTaraPredefinida.ToString();
            });
        }
        private void CargarControlesConDestinoActivo()
        {
            Invoke((MethodInvoker)delegate
            {
                textBox_destino.Text = m_datCajaActiva.Destino.Nombre;
            });
        }
        private void CargarControlesConProductoContenidoActivo()
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
                textBox_producto.Tag = m_datReadPiece.Producto;
                textBox_producto.Text = m_datReadPiece.Producto.NombreEtiL1;
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL2);
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL3);
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL4);
                textBox_producto.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL5);
            }
        }

        #endregion

        #region DATAGRID PESAJES DE CAJAS EN LOTE
        private void ClrControlesDetallePesadasCajasLote()
        {
            dataGridView_detallePesajeCajasEnLote.DataSource = null;
        }

        private void LoadDataGridPesadasCajasLote()
        {
            DataSet dsPesadasCajas = new DataSet();

            try
            {

                if (CDb.GetDatSet_PesadasCajasLote(DateTime.Now, out dsPesadasCajas))
                {
                    if (dsPesadasCajas.Tables.Contains("PESADAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePesajeCajasEnLote,dsPesadasCajas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePesajeCajasEnLote,"PESADAS");
                        SetFormatDGVDetallePesadasCajasLoteSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePesajeCajasEnLote, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de pesadas de cajas en el lote", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetFormatDGVDetallePesadasCajasLoteSecure()
        {
            if (dataGridView_detallePesajeCajasEnLote.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePesadasCajasLoteSecure(); }));
            }
            else
            {
                //CAJA,CREADA,DESTINO,PRODUCTO,UNIDADES,BRUTO,TARA,NETO

                dataGridView_detallePesajeCajasEnLote.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeCajasEnLote.Columns["CAJA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeCajasEnLote.Columns["CAJA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeCajasEnLote.Columns["CREADA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePesajeCajasEnLote.Columns["CREADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeCajasEnLote.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeCajasEnLote.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeCajasEnLote.Columns["UNIDADES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeCajasEnLote.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeCajasEnLote.Columns["BRUTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeCajasEnLote.Columns["BRUTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeCajasEnLote.Columns["BRUTO"].DefaultCellStyle.Format = "0.##";
                dataGridView_detallePesajeCajasEnLote.Columns["TARA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeCajasEnLote.Columns["TARA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeCajasEnLote.Columns["TARA"].DefaultCellStyle.Format = "0.##";
                dataGridView_detallePesajeCajasEnLote.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeCajasEnLote.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeCajasEnLote.Columns["NETO"].DefaultCellStyle.Format = "0.##";
            }
        }

        private void dataGridView_detallePesajeCajasEnLote_KeyDown(object sender, KeyEventArgs e)
        {
        }

        #endregion


        #region GRILLA DE PIEZAS EN CAJA
        private void ClrControlesDetallePiezasContenidasCaja()
        {
            dataGridView_detallePiezasContenidasCaja.DataSource = null;
        }
        private void LoadDataGridPiezasContenidasCajaAbierta()
        {
            DataSet dsPiezasContenidas = new DataSet();

            try
            {
                //PIEZA,PRODUCTO,PESO_NETO
                if (CDb.GetDatSet_PiezasContenidasCajaAbierta(out dsPiezasContenidas))
                {
                    if (dsPiezasContenidas.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, dsPiezasContenidas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, "PIEZAS");
                        SetFormatDGVDetallePiezasContenidasCajaSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas contenidas en la caja", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadDataGridPiezasContenidasCajaCerradaPorIdPieza(int idPieza)
        {
            DataSet dsPiezasContenidas = new DataSet();

            try
            {
                //PIEZA,PRODUCTO,PESO_NETO
                if (CDb.GetDatSet_PiezasContenidasCajaCerradaPorIdPieza(idPieza,out dsPiezasContenidas))
                {
                    if (dsPiezasContenidas.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, dsPiezasContenidas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, "PIEZAS");
                        SetFormatDGVDetallePiezasContenidasCajaSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas contenidas en la caja", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void LoadDataGridPiezasContenidasCajaCerradaPorIdCaja(int idCaja)
        {
            DataSet dsPiezasContenidas = new DataSet();

            try
            {
                //PIEZA,PRODUCTO,PESO_NETO
                if (CDb.GetDatSet_PiezasContenidasCajaCerradaPorIdCaja(idCaja, out dsPiezasContenidas))
                {
                    if (dsPiezasContenidas.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, dsPiezasContenidas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, "PIEZAS");
                        SetFormatDGVDetallePiezasContenidasCajaSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCaja, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas contenidas en la caja", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridView_detallePiezasContenidasCaja.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePiezasContenidasCajaSecure(); }));
            }
            else
            {
                //PIEZA,PRODUCTO,PESO_NETO

                dataGridView_detallePiezasContenidasCaja.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCaja.Columns["PIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasContenidasCaja.Columns["PIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCaja.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_detallePiezasContenidasCaja.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasContenidasCaja.Columns["PESO_NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasContenidasCaja.Columns["PESO_NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        #endregion

        private void button_printLabelBox_Click(object sender, EventArgs e)
        {
            if (m_datCajaActiva != null)
            {
                CLabel.PrintCaja(m_datCajaActiva);
            }
        }

        private void button_selectProductoCaja_Click(object sender, EventArgs e)
        {
            int idProductSelected = m_productoCajaActiva != null ? m_productoCajaActiva.Id : 0;
            int idTipoProductSelected = m_productoCajaActiva != null ? m_productoCajaActiva.m_tipo.Id : 0;

            CSelProductoDlg abmDlg = new CSelProductoDlg(idProductSelected, idTipoProductSelected, false, true);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.ProductoSelected.Id != 0)
            {
                if (m_productoCajaActiva != null && !m_productoCajaActiva.Equals(abmDlg.ProductoSelected) && !isGridPiezasContenidasVoid())
                {
                    if (DialogResult.Yes == MessageBox.Show("Esta intentando seleccionar otra caja cuando tiene pendiente piezas colectadas. Desea eliminar dichas piezas y colocar como activa la caja que acaba de seleccionar ?",
                        "VALIDACIÓN DE CAMBIO DE PRODUCTO CAJA", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        CDb.EliminarPiezasCajaTemp();
                    }
                    else
                        return;
                }
                m_productoCajaActiva = new CProducto(abmDlg.ProductoSelected);
                productoInsumosCtrl.IdProducto = abmDlg.ProductoSelected.Id;
                CargarControlesConProductoCajaActiva();
                ClrControlesDestino();
                ClrControlesReadEscanner();
                ClrControlesProducto();
                LoadDataGridPiezasContenidasCajaAbierta();
            }
            else
            {
                ClrAllControls();
            }
        }
        
        private void CargarControlesConProductoCajaActiva()
        {
            Invoke((MethodInvoker)delegate
            {
                textBox_productoCaja.Text = m_productoCajaActiva.Nombre;
                textBox_taraCaja.Text = m_productoCajaActiva.PesoTaraPredefinida.ToString();
                CProducto prdContener = CDb.GetProductoAContenerPorLaCaja(m_productoCajaActiva.Id);
                label_productoAContener.Text = prdContener == null ? "-" : prdContener.Nombre;
            });
        }
    }
}
