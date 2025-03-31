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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using HidKeyboardScannerLib;

namespace MeatWeigherManager
{
    public partial class Form_PreparacionCombos : Form
    {
        public volatile int vueltas = 0;
        CContenedor m_datComboActivo;
        CContenedor m_datReadCombo;
        CPesada m_datReadPiece;
        CZebraScannerLib m_scannerDRV;
        CProducto m_productoComboActivo;
        HidScannerLib m_scannerHID;


        private enum ESTADOS_APP
        {
            INICIADA,
            CREAR_COMBO,
            ELIMINAR_PIEZA_COMBO_ABIERTO,
            ELIMINAR_COMBO,
            PARADA,
            INABILITADA
        };

        private ESTADOS_APP m_Estado;
        public bool EnableActionsClosing { get; private set; } = true;

        #region INICIALIZACION

        public Form_PreparacionCombos()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {

            Conectar_Impresora();

            InitializeScanner();

            if (!CDb.isOpen)
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
        #endregion

        #region EVENTOS SCANNER
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
        #endregion

        #region EVENTOS DEL FORM Y DE SUS CONTROLES
        private void Form_MeatWeigherManager_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            WindowState = FormWindowState.Maximized;
        }
        private void Form_PreparacionCombos_FormClosing(object sender, FormClosingEventArgs e)
        {
            // el codigo contenido aqui es justificado solo porque los puertos como demoran en cerrarce y 
            // quedan en proceso de cierre aun cuando se cerro el form. Esto asegura que los eventos de FormClosed
            //disparados se ocacionen cuando las tareas aqui dentro se completen.
            //Este comportamiento solo se utiliza cuando cuando el Form Padre pide el cierre del hijo (Razon USERCLOSING).
            //Cuando el usuario cierra con la X al padre (MDIFROMCLOSING) se efectua normalemnte.
            if (EnableActionsClosing == true)
            {
                e.Cancel = true;
                CloseScanner();
                EnableActionsClosing = false;
                Close();
            }
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
            if (textBox_productoPieza.Tag != null)
            {
                string productoPrimario = String.Format("Producto Primario: {0}", ((CProducto)textBox_productoPieza.Tag).Nombre);
                ToolTip tt = new ToolTip();
                tt.Show(productoPrimario, textBox_productoPieza, 0, 0, 1000);
            }
        }

        private void textBox_taraCombo_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_taraCombo.Text, "Tara", "Editar la Tara del Combo ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_taraCombo.Text = dlg.VALUE;
            }
        }

        private void textBox_taraCombo_KeyPress(object sender, KeyPressEventArgs e)
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
        private void button_registrarCombo_EnabledChanged(object sender, EventArgs e)
        {
            button_registrarCombo.BackgroundImage = button_registrarCombo.Enabled ? global::MeatWeigherManager.Properties.Resources.Registrar_Caja_Enable : global::MeatWeigherManager.Properties.Resources.Registrar_caja_disable;
        }

        private void button_registrarCombo_Click(object sender, EventArgs e)
        {
            RegistrarCombo();
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

        private void button_selectProductoCombo_Click(object sender, EventArgs e)
        {
            int idProductSelected = m_productoComboActivo != null ? m_productoComboActivo.Id : 0;
            int idTipoProductSelected = m_productoComboActivo != null ? m_productoComboActivo.m_tipo.Id : 0;

            CSelProductoDlg abmDlg = new CSelProductoDlg(idProductSelected, idTipoProductSelected, true);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.ProductoSelected.Id != 0)
            {
                if(m_productoComboActivo != null && !m_productoComboActivo.Equals(abmDlg.ProductoSelected) && !isGridPiezasContenidasVoid())
                {
                    if (DialogResult.Yes == MessageBox.Show("Esta intentando seleccionar otro combo cuando tiene pendiente piezas colectadas. Desea eliminar dichas piezas y colocar como activo el combo que acaba de seleccionar ?",
                        "VALIDACIÓN DE CAMBIO DE COMBO", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        CDb.EliminarPiezasComboTemp();
                    }
                    else
                        return;
                }
                m_productoComboActivo = new CProducto(abmDlg.ProductoSelected);
                CargarControlesConProductoComboActivo();
                SetEstado(ESTADOS_APP.CREAR_COMBO);

                productoInsumosCtrl.IdProducto = m_productoComboActivo.Id;
            }
            else
            {
                ClrAllControls();
                SetEstado(ESTADOS_APP.INICIADA);
            }
        }
        private void tabControl_ProcesoRegistracionCombos_Click(object sender, EventArgs e)
        {
            LoadDataGridRegistracionCombosLote();
        }


        #endregion

        #region EVENTOS MENU

        private void consultaPesajes_CombosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaCombosRegistradosDlg dlg = new CViewConsultaCombosRegistradosDlg();
            dlg.ShowDialog();
        }
        
        private void reimprimirEtiquetaCombosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CContenedor> listCombos = CDb.GetCombosFromSelectedDGVCombos(dataGridView_detalleRegistracionesCombosEnLote);

                if (listCombos != null && listCombos.Count > 0)
                {
                    foreach (CContenedor cn in listCombos)
                    {
                        CLabel.PrintCombo(cn);
                    }
                }
            }
        }

        #endregion
        
        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            ClrAllControls();
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            SetEstado(ESTADOS_APP.PARADA);
        }
        

        private void toolStripButton_modoQuitarPiezaContenidaComboAbierto_Click(object sender, EventArgs e)
        {
            if (GetEstado() == ESTADOS_APP.ELIMINAR_PIEZA_COMBO_ABIERTO)
                SetEstado(ESTADOS_APP.CREAR_COMBO);
            else if (!isGridPiezasContenidasVoid())
            {
                if (DialogResult.Yes == MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Piezas Contenidas en la preparación de un Combo," +
                " a partir de este momento todas las piezas que sean escaneadas seran eliminadas de la preparación, permanece en este modo ?.",
                "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SetEstado(ESTADOS_APP.ELIMINAR_PIEZA_COMBO_ABIERTO);
                }
            }
            else
            {
                MessageBox.Show("No puede ingresar al modo Eliminacion de Piezas en Combo Abierto dado que no hay ningun combo en proceso o no hay piezas a eliminar.", "VALIDACIÓN DE INGRESO A MODO ELIMINACIÓN DE PIEZAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton_modoEliminacionCombo_Click(object sender, EventArgs e)
        {
            if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
            {
                SetEstado(ESTADOS_APP.ELIMINAR_COMBO);
            }
            else
            {
                MessageBox.Show("Acción permitida unicamente para Administradores", "ELIMINACIÓN DE COMBOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        tabControl_ProcesoRegistracionCombos.Visible = false;
                        groupBox_datCombo.Enabled = false;
                        groupBox_datCombo.Visible = false;
                        groupBox_lecturaScanner.Enabled = false;
                        groupBox_lecturaScanner.Visible = false;
                        groupBox_datPiezasContenidas.Enabled = false;
                        groupBox_datPiezasContenidas.Visible = false;
                        groupBox_detallesRegistracionCombos.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        toolStripButton_modoEliminarPiezaContenidaComboAbierta.Enabled = false;
                        toolStripButton_modoEliminacionCombo.Enabled = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, false);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        tabControl_ProcesoRegistracionCombos.Visible = false;
                        groupBox_lecturaScanner.Enabled = false;
                        groupBox_lecturaScanner.Visible = false;
                        groupBox_datCombo.Enabled = false;
                        groupBox_datCombo.Visible = false;
                        groupBox_datPiezasContenidas.Enabled = false;
                        groupBox_datPiezasContenidas.Visible = false;
                        groupBox_detallesRegistracionCombos.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_modoEliminarPiezaContenidaComboAbierta.Enabled = false;
                        toolStripButton_modoEliminacionCombo.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        groupBox_selectProductoCombo.Enabled = false;
                        groupBox_selectProductoCombo.Visible = false;
                        button_registrarCombo.Enabled = false;
                        button_registrarCombo.Visible = false;
                        groupBox_selectionDestino.Visible = false;
                        groupBox_edicionTara.Visible = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, false);
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        tabControl_ProcesoRegistracionCombos.SelectTab(tabPage_RegistracionCombo);
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoRegistracionCombos.Visible = true;
                        groupBox_lecturaScanner.Enabled = false;
                        groupBox_lecturaScanner.Visible = false;
                        groupBox_datCombo.Enabled = false;
                        groupBox_datCombo.Visible = false;
                        groupBox_datPiezasContenidas.Enabled = false;
                        groupBox_datPiezasContenidas.Visible = false;
                        groupBox_detallesRegistracionCombos.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_modoEliminarPiezaContenidaComboAbierta.Enabled = false;
                        toolStripButton_modoEliminacionCombo.Enabled = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INICIADO", Color.Black);
                        groupBox_selectProductoCombo.Enabled = true;
                        groupBox_selectProductoCombo.Visible = true;
                        button_registrarCombo.Enabled = false;
                        button_registrarCombo.Visible = false;
                        groupBox_selectionDestino.Visible = false;
                        groupBox_edicionTara.Visible = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, true);
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "INICIADO", Color.Black);

                        break;
                    }
                case ESTADOS_APP.CREAR_COMBO:
                    {
                        tabControl_ProcesoRegistracionCombos.SelectTab(tabPage_RegistracionCombo);
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoRegistracionCombos.Visible = true;
                        groupBox_lecturaScanner.Enabled = true;
                        groupBox_lecturaScanner.Visible = true;
                        groupBox_datCombo.Enabled = false;
                        groupBox_datCombo.Visible = true;
                        groupBox_datPiezasContenidas.Enabled = true;
                        groupBox_datPiezasContenidas.Visible = true;
                        groupBox_detallesRegistracionCombos.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_modoEliminarPiezaContenidaComboAbierta.Enabled = true;
                        toolStripButton_modoEliminacionCombo.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "ACTIVO PARA CREAR COMBOS", Color.Green);
                        button_registrarCombo.Enabled = true;
                        button_registrarCombo.Visible = true;
                        groupBox_selectionDestino.Visible = true;
                        groupBox_selectionDestino.Enabled = true;
                        groupBox_edicionTara.Visible = true;
                        groupBox_edicionTara.Enabled = true;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, true);
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO PREPARAR COMBO", Color.Green);

                        break;
                    }
                case ESTADOS_APP.ELIMINAR_PIEZA_COMBO_ABIERTO:
                    {
                        toolStripButton_modoEliminarPiezaContenidaComboAbierta.Enabled = true;
                        toolStripButton_modoEliminacionCombo.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "MODO ELIMINAR PIEZAS COMBO ABIERTO", Color.Red);
                        button_registrarCombo.Enabled = false;
                        button_registrarCombo.Visible = false;
                        groupBox_selectionDestino.Visible = true;
                        groupBox_selectionDestino.Enabled = false;
                        groupBox_edicionTara.Visible = true;
                        groupBox_edicionTara.Enabled = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, true);
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "ELIMINAR PIEZAS COMBO", Color.Red);

                        break;
                    }
                case ESTADOS_APP.ELIMINAR_COMBO:
                    {
                        tabControl_ProcesoRegistracionCombos.SelectTab(tabPage_RegistracionCombo);
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoRegistracionCombos.Visible = true;
                        groupBox_lecturaScanner.Enabled = true;
                        groupBox_lecturaScanner.Visible = true;
                        groupBox_datCombo.Enabled = false;
                        groupBox_datCombo.Visible = true;
                        groupBox_datPiezasContenidas.Enabled = true;
                        groupBox_datPiezasContenidas.Visible = true;
                        groupBox_detallesRegistracionCombos.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_modoEliminarPiezaContenidaComboAbierta.Enabled = false;
                        toolStripButton_modoEliminacionCombo.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "MODO ELIMINAR COMBOS", Color.Red);
                        button_registrarCombo.Enabled = false;
                        button_registrarCombo.Visible = false;
                        groupBox_selectProductoCombo.Enabled = false;
                        groupBox_selectProductoCombo.Visible = true;
                        groupBox_selectionDestino.Visible = true;
                        groupBox_selectionDestino.Enabled = false;
                        groupBox_edicionTara.Visible = true;
                        groupBox_edicionTara.Enabled = false;
                        SetVisibleCtrlSecure(toolStripLabel_TituloProceso, true);
                        SetTextToolStripLabel(toolStripLabel_TituloProceso, "MODO ELIMINAR COMBOS", Color.Red);

                        break;
                    }
            }
        }
        #endregion

        #region FUNCIONES GENERALES

        private void RegistrarCombo()
        {
            if (m_Estado == ESTADOS_APP.CREAR_COMBO)
            {
                if (EsValidoRegistrarCombo())
                {
                    if (GenerarNuevoCombo())
                    {
                        CargarControlesLoteConComboActivo();
                        LoadDataGridPiezasContenidasComboAbierto();
                        ClrControlesProducto();
                        if (!RegistrarInsumos(productoInsumosCtrl.GetInsumos()))
                        {
                            MessageBox.Show("Se ha producido un error al registrar los Insumos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
        }

        private bool GenerarNuevoCombo()
        {
            bool createdOk = false;
            float pesoTara = 0.0f;
            float.TryParse(textBox_taraCombo.Text, out pesoTara);

            m_datComboActivo = new CContenedor();
            m_datComboActivo.PesoNeto = CDb.GetTotalNetoPartsContainTempCombo();
            m_datComboActivo.PesoTara = pesoTara; 
            m_datComboActivo.Destino = ((CDestino)textBox_destino.Tag);
            m_datComboActivo.m_idEstacion = CDb.m_OperadorActivo.m_idEstacion;
            m_datComboActivo.Producto = m_productoComboActivo;
            m_datComboActivo.m_undsContenidas = CDb.GetTotalUndsPartsContainTempCombo();
            m_datComboActivo.IdTipo = "CMB";
            try
            {
                if (CDb.InsertNewContainer(ref m_datComboActivo))
                {
                    CLabel.PrintCombo(m_datComboActivo);
                    createdOk = true;
                }
                else
                {
                    MessageBox.Show("Error de Base de Datos al intentar generar un nuevo Combo.", "GENERAR UN NUEVO COMBO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show("Error de Base de Datos al intentar generar un nuevo Combo.  " + excep.Message, "GENERAR UN NUEVO COMBO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return createdOk;
        }

        /// <summary>
        /// Verifica que las unidades y peso de cada articulo incluido en el combo sean correctas
        /// en funcion a si el articulo especifica validar unidades o validar peso.
        /// Las unidades se validan por igual cantidad , el peso por un rango de tolerancia que
        /// especifica cada articulo en el combo.
        /// </summary>
        /// <returns></returns>
        private bool EsValidoRegistrarCombo()
        {
            bool isValid = false;

            if (textBox_destino.Tag == null)
            {
                MessageBox.Show("Error , no ha seleccionado un destino", "VALIDACIÓN DE NUEVO COMBO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!EsValidoCantidadesPreparadasCombo())
            {
                MessageBox.Show("Error , las cantidades colectadas para el combo no son correctas , verifique la cantidad de unidades y peso que requiere el combo", "VALIDACIÓN DE NUEVO COMBO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(!productoInsumosCtrl.esTodosInsumosConfirmados())
            {
                MessageBox.Show("Error , No ha confirmado todos los insumos del Combo.", "VALIDACION DE NUEVO COMBO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                isValid = true;
            return isValid;
        }

        /// <summary>
        /// Valida las cantidades colectadas del combo en proceso para verificar si la cantidad de unidades
        /// y peso de cada item es correcto con respecto a lo que define cada item de producto en el combo.
        /// </summary>
        /// <returns></returns>
        private bool EsValidoCantidadesPreparadasCombo()
        {
            bool isValid = false;
            if(dataGridView_detallePiezasContenidasCombo.Rows.Count > 0)
            {
                isValid = !((from DataGridViewRow dgvr in dataGridView_detallePiezasContenidasCombo.Rows
                                        where (CDb.GetCellDGVInt(dgvr, "UNDS_COL") ==0 || (CDb.GetCellDGVBool(dgvr, "VALID_UNDS") == true && CDb.GetCellDGVInt(dgvr, "UNDS_CMB") != CDb.GetCellDGVInt(dgvr, "UNDS_COL")))
                                        || (CDb.GetCellDGVBool(dgvr, "VALID_PESO") == true && !EsPesoEnTolerancia(CDb.GetCellDGVFloat(dgvr, "PESO_CMB"),CDb.GetCellDGVFloat(dgvr, "PESO_COL"), CDb.GetCellDGVFloat(dgvr, "TOL_PESO")))
                                        select dgvr).Count() > 0);
            }
            return isValid;
        }

        /// <summary>
        /// Determina un un valor de peso se encuentra dentro de un margen porcentual de tolerancia 
        /// en comparacion con un valor de peso de referencia.
        /// </summary>
        /// <param name="pesoReferencia"></param>
        /// <param name="peso"></param>
        /// <param name="tolerancia"></param>
        /// <returns></returns>
        private bool EsPesoEnTolerancia(float pesoReferencia,float peso,float tolerancia)
        {
            bool enTolerancia = false;
            try
            {
                enTolerancia = Math.Abs((((pesoReferencia - peso) / pesoReferencia) * 100)) <= tolerancia;
            }
            catch(DivideByZeroException)
            { }
            return enTolerancia;
        }
        private async void RegisterReadPiece(string codBarRead)
        {
            if (m_Estado == ESTADOS_APP.CREAR_COMBO || 
                    m_Estado == ESTADOS_APP.ELIMINAR_COMBO || 
                        m_Estado == ESTADOS_APP.ELIMINAR_PIEZA_COMBO_ABIERTO)
            {
                SetDataReadPiece(codBarRead);
                
                try
                {
                    if (m_Estado == ESTADOS_APP.CREAR_COMBO)
                    {
                        if (await IsValidAddReadPiece())
                        {
                            if (CDb.AgregarPiezaComboTemp(m_datReadPiece.Id))
                            {
                                CargarControlesConProductoActivo();
                                LoadDataGridPiezasContenidasComboAbierto();
                                SetMessageReadyScanner("La pieza fue registrada con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al registrar una pieza para vincularla a un nuevo combo. Verifique la conexión con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if(m_Estado == ESTADOS_APP.ELIMINAR_PIEZA_COMBO_ABIERTO)
                    {
                        if (await IsValidDeleteReadPieceForComboOpen())
                        {
                            if (CDb.EliminarPiezaComboTemp(m_datReadPiece.Id))
                            {
                                LoadDataGridPiezasContenidasComboAbierto();
                                SetMessageReadyScanner("La pieza fue eliminada con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();
                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al intentar eliminar una pieza contenida en el combo. Verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (m_Estado == ESTADOS_APP.ELIMINAR_COMBO)
                    {
                        if (await IsValidDeleteReadCombo())
                        {
                            SetActiveComboFromIdCombo(m_datReadCombo.Id);
                            CargarControlesConComboActivo();
                            LoadDataGridPiezasContenidasComboCerradoPorIdCombo(m_datReadCombo.Id);

                            if (CDb.DesarmarContenedor(m_datReadCombo.Id))
                            {

                                /// elimina los insumos que fueron registrados para el contenedor.
                                CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.ConformadoContenedor, m_datReadCombo.Id);

                                ClrControlesDetallePiezasContenidasCombo();
                                SetMessageReadyScanner("El Combo fue eliminado con exito !!.", Color.Green);
                                m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                await SetOnOffLedOK();

                            }
                            else
                            {
                                MessageBox.Show("Se ha producido un error al eliminar un combo .verifique la conexion con la base de datos.", "ELIMINAR COMBO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al registrar una pieza para vincularla a un nuevo combo.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            m_datReadPiece = null;
            m_datReadCombo = null;
            if (codeBarRead != "" && codeBarRead.Length >= 3 && codeBarRead[0] == 'A' && codeBarRead[codeBarRead.Length - 1] == 'A')
            {
                codeBarRead = codeBarRead.Remove(0, 1);
                codeBarRead = codeBarRead.Remove(codeBarRead.Length - 1, 1);
                Int32.TryParse(codeBarRead, out id);
                m_datReadCombo = CDb.GetCombo(id);
            }
            else
            {
                Int32.TryParse(codeBarRead, out id);
                m_datReadPiece = CDb.GetPesada(id);
            }
        }

        private bool SetActiveComboFromIdCombo(int idCombo)
        {
            m_datComboActivo = CDb.GetCombo(idCombo);
            return m_datComboActivo != null;
        }

        private async Task<bool> IsValidAddReadPiece()
        {
            return await Task.Run(async () =>
            {
                string resultStr;
                bool verificacionOk = false;
                if (m_datReadPiece == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidPartForIncludeToCombo(m_productoComboActivo.Id, m_datReadPiece.Id,out resultStr))
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
        private async Task<bool> IsValidDeleteReadPieceForComboOpen()
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
                else if (!CDb.IsValidPartForDeleteToOpenContainer(m_datReadPiece.Id,out resultStr))
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
        private async Task<bool> IsValidDeleteReadCombo()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                if (m_datReadCombo == null)
                {
                    SetMessageReadyScanner("El Combo colectado no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidDisarmContainer(m_datReadCombo.Id))
                {
                    SetMessageReadyScanner("Este combo no esta en stock !!.", Color.Red);
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
            return dataGridView_detallePiezasContenidasCombo == null || 
                dataGridView_detallePiezasContenidasCombo.Rows.Cast<DataGridViewRow>().Sum(t => CDb.GetCellDGVInt(t,"UNDS_COL")) == 0;
        }

        private void CargarControlesConProductoComboActivo()
        {
            Invoke((MethodInvoker)delegate
            {
                textBox_productoCombo.Text = m_productoComboActivo.Nombre;
                textBox_taraCombo.Text = m_productoComboActivo.PesoTaraPredefinida.ToString();

                ClrControlesDestino();
                ClrControlesReadEscanner();
                ClrControlesProducto();
                LoadDataGridPiezasContenidasComboAbierto();
            });
        }

        private void CargarControlesConComboActivo()
        {
            Invoke((MethodInvoker)delegate
            {
                m_productoComboActivo = m_datComboActivo.Producto;
                textBox_productoCombo.Text = m_productoComboActivo.Nombre;
                textBox_productoCombo.Tag = m_productoComboActivo;

                textBox_taraCombo.Text = m_productoComboActivo.PesoTaraPredefinida.ToString();
                
                textBox_destino.Tag = m_datComboActivo.Destino;
                textBox_destino.Text = m_datComboActivo.Destino.Nombre;

                CargarControlesLoteConComboActivo();
                ClrControlesReadEscanner();
                ClrControlesProducto();
                LoadDataGridPiezasContenidasComboAbierto();
            });
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
                registrados = CDb.RegistrarMovimientoInsumos(TYPE_INSUMO_MOV.Egreso, TYPE_INSUMO_PROC.ConformadoContenedor, m_datComboActivo.Id, listInsumos);
            }
            return registrados;
        }

        /// <summary>
        /// Borra todos los controles del dialogo.
        /// </summary>
        private void ClrAllControls()
        {
            Invoke((MethodInvoker)delegate
            {
                ClrControlesCombo();
                ClrControlesDestino();
                ClrControlesTara();
                ClrControlesReadEscanner();
            });
        }
        /// <summary>
        /// borra los controles que se relacionan con el combo
        /// </summary>
        private void ClrControlesCombo()
        {
            Invoke((MethodInvoker)delegate
            {
                textBox_productoCombo.Tag = null;
                textBox_productoCombo.Text = "";
                CDb.EliminarPiezasComboTemp();
                label_numCOMBO.Text = "-----";
                label_Lote.Text = "-----";
                m_productoComboActivo = null;
                ClrControlesDetallePiezasContenidasCombo();
                ClrControlesDetalleRegistracionesCombosLote();
                productoInsumosCtrl.IdProducto = 0;

            });
        }

        /// <summary>
        /// borra controles de lectura de piezas con el escanner.
        /// </summary>
        private void ClrControlesReadEscanner()
        {
            textBox_valueReadCodBar.Text = "------";
            label_detalleLectura.Text = null;
        }
        /// <summary>
        /// borra controles de seleccion del destino
        /// </summary>
        private void ClrControlesDestino()
        {
            textBox_destino.Text = "";
            textBox_destino.Tag = null;
        }
        private void ClrControlesTara()
        {
            textBox_taraCombo.Text = "";
        }

        private void ClrControlesProducto()
        {
            textBox_productoPieza.Text = "";
        }
        private void CargarControlesLoteConComboActivo()
        {
            Invoke((MethodInvoker)delegate
            {
                if (m_datComboActivo != null)
                {
                    label_numCOMBO.Text = m_datComboActivo.Id.ToString();
                    label_Lote.Text = m_datComboActivo.m_fechaHoraCreacion.ToShortDateString();
                }
            });
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
                textBox_productoPieza.Text = m_datReadPiece.Producto.NombreEtiL1;
                textBox_productoPieza.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL2);
                textBox_productoPieza.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL3);
                textBox_productoPieza.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL4);
                textBox_productoPieza.Text += ("\r\n" + m_datReadPiece.Producto.NombreEtiL5);
            }
        }

        #endregion

        #region DATAGRID REGISTRACIONES DE COMBOS EN LOTE
        private void ClrControlesDetalleRegistracionesCombosLote()
        {
            dataGridView_detalleRegistracionesCombosEnLote.DataSource = null;
        }

        private void LoadDataGridRegistracionCombosLote()
        {
            DataSet dsPesadasCombos = new DataSet();

            try
            {

                if (CDb.GetDatSet_RegistracionesComboLote(DateTime.Now, out dsPesadasCombos))
                {
                    if (dsPesadasCombos.Tables.Contains("REGISTRACIONES"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detalleRegistracionesCombosEnLote,dsPesadasCombos);
                        SetDataMember_DataGridViewSecure(dataGridView_detalleRegistracionesCombosEnLote, "REGISTRACIONES");
                        SetFormatDGVDetalleRegistracionesCombosLoteSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detalleRegistracionesCombosEnLote, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de registraciones de combos en el lote", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetFormatDGVDetalleRegistracionesCombosLoteSecure()
        {
            if (dataGridView_detalleRegistracionesCombosEnLote.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetalleRegistracionesCombosLoteSecure(); }));
            }
            else
            {
                //CMB_NRO,COMBO,DESTINO,CREADO,EST,UNIDADES,BRUTO,TARA,NETO
                dataGridView_detalleRegistracionesCombosEnLote.Columns["FECHA_VENCIMIENTO"].Visible = false;

                dataGridView_detalleRegistracionesCombosEnLote.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["CMB_NRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["CMB_NRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["COMBO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["COMBO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["DESTINO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["DESTINO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["CREADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["CREADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["UNIDADES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["BRUTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["BRUTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["BRUTO"].DefaultCellStyle.Format = "0.##";
                dataGridView_detalleRegistracionesCombosEnLote.Columns["TARA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["TARA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["TARA"].DefaultCellStyle.Format = "0.##";
                dataGridView_detalleRegistracionesCombosEnLote.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detalleRegistracionesCombosEnLote.Columns["NETO"].DefaultCellStyle.Format = "0.##";
            }
        }

        #endregion


        #region GRILLA DE PIEZAS EN COMBO
        private void ClrControlesDetallePiezasContenidasCombo()
        {
            Invoke((MethodInvoker)delegate
            {
                dataGridView_detallePiezasContenidasCombo.DataSource = null;
            });
        }
        private void LoadDataGridPiezasContenidasComboAbierto()
        {
            DataSet dsPiezasContenidas = new DataSet();

            try
            {
                //PIEZA,PRODUCTO,PESO_NETO
                if (CDb.GetDatSet_PiezasContenidasComboAbierto(m_productoComboActivo.Id, out dsPiezasContenidas))
                {
                    if (dsPiezasContenidas.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCombo, dsPiezasContenidas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasContenidasCombo, "PIEZAS");
                        SetFormatDGVDetallePiezasContenidasComboSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCombo, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas contenidas en el combo", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadDataGridPiezasContenidasComboCerradoPorIdCombo(int idCombo)
        {
            DataSet dsPiezasContenidas = new DataSet();

            try
            {
                //PIEZA,PRODUCTO,PESO_NETO
                if (CDb.GetDatSet_PiezasContenidasComboCerradoIdCombo(idCombo, out dsPiezasContenidas))
                {
                    if (dsPiezasContenidas.Tables.Contains("PIEZAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCombo, dsPiezasContenidas);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePiezasContenidasCombo, "PIEZAS");
                        SetFormatDGVDetallePiezasContenidasComboSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasContenidasCombo, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas contenidas en el combo", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetFormatDGVDetallePiezasContenidasComboSecure()
        {
            if (dataGridView_detallePiezasContenidasCombo.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePiezasContenidasComboSecure(); }));
            }
            else
            {
                //IDPRODUCTO,PRODUCTO,UNDS_CMB,PESO_CMB,UND_COL,PESO_COL

                dataGridView_detallePiezasContenidasCombo.EnableHeadersVisualStyles = false;
                dataGridView_detallePiezasContenidasCombo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCombo.Columns["IDPRODUCTO"].Visible = false;
                dataGridView_detallePiezasContenidasCombo.Columns["VALID_UNDS"].Visible = false;
                dataGridView_detallePiezasContenidasCombo.Columns["VALID_PESO"].Visible = false;
                dataGridView_detallePiezasContenidasCombo.Columns["TOL_PESO"].Visible = false;
                dataGridView_detallePiezasContenidasCombo.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_detallePiezasContenidasCombo.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasContenidasCombo.Columns["UNDS_CMB"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasContenidasCombo.Columns["UNDS_CMB"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCombo.Columns["PESO_CMB"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasContenidasCombo.Columns["PESO_CMB"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCombo.Columns["UNDS_COL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasContenidasCombo.Columns["UNDS_COL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCombo.Columns["PESO_COL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasContenidasCombo.Columns["PESO_COL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasContenidasCombo.Columns["PESO_COL"].DefaultCellStyle.Format = "0.00";

                bool havingPartColected = (from DataGridViewRow dgvr in dataGridView_detallePiezasContenidasCombo.Rows
                                           where CDb.GetCellDGVInt(dgvr, "UNDS_COL") > 0 select dgvr).Count() > 0;

                dataGridView_detallePiezasContenidasCombo.Columns["UNDS_COL"].HeaderCell.Style.BackColor = havingPartColected ? Color.Red : Color.White;
                dataGridView_detallePiezasContenidasCombo.Columns["PESO_COL"].HeaderCell.Style.BackColor = havingPartColected ? Color.Red : Color.White;
            }
        }

        #endregion

        private void button_printLabelCombo_Click(object sender, EventArgs e)
        {
            if (m_datComboActivo != null)
            {
                CLabel.PrintCombo(m_datComboActivo);
            }
        }

    }
}
