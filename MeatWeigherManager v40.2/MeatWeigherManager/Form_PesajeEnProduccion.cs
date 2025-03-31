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

namespace MeatWeigherManager
{
    public partial class Form_PesajeEnProduccion : Form
    {
        public volatile int vueltas = 0;
        CDatScale m_DatPesaje;
        CPesada m_datPesadaActiva;
        List<ScaleForm> m_listScales;

        private enum ESTADOS_APP
        {
            INICIADA,
            PARADA,
            INABILITADA
        };

        /// <summary>
        /// Indica que balanza esta visible en el dialogo
        /// </summary>
        private ScaleForm m_activeScaleForm;

        private ESTADOS_APP m_Estado;
        public bool EnableActionsClosing { get; private set; } = true;

        #region INICIALIZACION

        public Form_PesajeEnProduccion()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {

            Connect_Scales();

            Conectar_Impresora();

            if (!CDb.isOpen || !scaleSerialCtrl1.IsConnected)
                SetEstado(ESTADOS_APP.INABILITADA);
            else
                SetEstado(ESTADOS_APP.PARADA);

            if (CDb.isOpen)
                LoadDataGridPesadasLote();
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

        private async void Form_PesajeEnProduccion_FormClosing(object sender, FormClosingEventArgs e)
        {
            // el codigo contenido aqui es justificado solo porque los puertos como demoran en cerrarce y 
            // quedan en proceso de cierre aun cuando se cerro el form. Esto asegura que los eventos de FormClosed
            //disparados se ocacionen cuando las tareas aqui dentro se completen.
            if (EnableActionsClosing)
            {
                e.Cancel = true;
                await Disconnect_ScalesAsync();
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

        private void textBox_unidadesPredefinidas_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_unidadesPredefinidas.Text, "Unidades", "Editar la Cantidad de Unidades", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_unidadesPredefinidas.Text = dlg.VALUE;
            }
        }

        private void textBox_taraPredefinida_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoTaraPredefinida.Text, "Tara", "Editar la Tara ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoTaraPredefinida.Text = dlg.VALUE;
            }
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
        private void button_imprimirEtiquetaContenedor_EnabledChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region EVENTOS MENU

        private void consultaPesajes_LoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaLotePesadasDlg dlg = new CViewConsultaLotePesadasDlg();
            dlg.ShowDialog();
        }

        private void toolStripContextMenuDataGridViewPesadas_ItemBorrar_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                if (BorrarRegistrosSeleccionadosDataGridPesadasLote())
                    CargarControlesConAcumuladosProducto();
            }
        }
        
        private void reimprimirEtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CPesada> listPesadas = CDb.GetWeightingFromSelectedDGVPesadas(dataGridView_detallePesajeEnLote,"IDPESADA");

                if (listPesadas != null && listPesadas.Count > 0)
                {
                    foreach(CPesada pesada in listPesadas)
                    {
                        CLabel.PrintProduct(pesada, ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 1);
                    }
                }
            }
        }

#endregion
        
        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            ClrControlesPesada();
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            SetEstado(ESTADOS_APP.PARADA);
        }

        #endregion

        #region EVENTOS BUTTONS

        private void button_seleccionarProducto_Click(object sender, EventArgs e)
        {
            int idProductSelected = textBox_producto.Tag != null ? ((CProducto)textBox_producto.Tag).Id : 0;
            int idTipoProductSelected = textBox_producto.Tag != null ? ((CProducto)textBox_producto.Tag).m_tipo.Id : 0;

            CSelProductoDlg abmDlg = new CSelProductoDlg(idProductSelected, idTipoProductSelected/*," AND p.espesable=1 AND (p.esinsumo is null or p.esinsumo=0) "*/);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.ProductoSelected.Id != 0)
            {
                textBox_producto.Tag = abmDlg.ProductoSelected;
                CargarControlesConProductoActivo();
                productoInsumosCtrl.IdProducto = abmDlg.ProductoSelected.Id;
            }
            else
            {
                ClrControlesProducto();
            }
            ClrControlesSector();
        }
        private void button_selecDestino_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_destino.Tag != null ? ((CDestino)textBox_destino.Tag).Id : 0;
            CABM_DestinosDlg abmDlg = new CABM_DestinosDlg(CDb.m_oleDbConnection, CABM_DestinosDlg.BEHAVIOR_MODE.SELECTION, idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.DestinoSelected.Id != 0)
            {
                textBox_destino.Tag = abmDlg.DestinoSelected;
                CargarControlesConDestinoActivo();
            }
            else
            {
                ClrControlesDestino();
            }
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

        private void button_imprimirEtiquetaContenedor_Click(object sender, EventArgs e)
        {
            Form_EditCount dlg = new Form_EditCount();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CPesada cp = new CPesada();
                cp.FechaHora = DateTime.Now;
                cp.Producto = (CProducto)textBox_producto.Tag;
                CLabel.PrintContainer(cp,dlg.Cantidad);
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
                    RegistrarPesada();
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
                        groupBox_datPesaje.Enabled = false;
                        groupBox_datPesaje.Visible = false;
                        groupBox_detallesPesadasOI.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        tabControl_ProcesoPesaje.Visible = false;
                        groupBox_datPesaje.Enabled = false;
                        groupBox_datPesaje.Visible = false;
                        groupBox_detallesPesadasOI.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoPesaje.Visible = true;
                        tabControl_ProcesoPesaje.SelectTab("tabPage_ProcesoPesaje");
                        scaleSerialCtrl1.Enabled = true;
                        groupBox_datPesaje.Enabled = true;
                        groupBox_datPesaje.Visible = true;
                        groupBox_detallesPesadasOI.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "EN PROCESO DE PESAJE", Color.Red);
                        break;
                    }
            }
        }
        #endregion

        #region FUNCIONES GENERALES

        private void RegistrarPesada()
        {
            if (m_Estado == ESTADOS_APP.INICIADA)
            {
                if (VerificarControlesNuevaPesada())
                {
                    SetDatosPesadaActiva();
                    try
                    {
                        if (CDb.RegistrarPesada(ref m_datPesadaActiva))
                        {
                            CLabel.PrintProduct(m_datPesadaActiva, ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 1);
                            CargarControlesConAcumuladosProducto();
                            LoadDataGridPesadasLote();
                            if (!RegistrarInsumos(productoInsumosCtrl.GetInsumos()))
                            {
                                MessageBox.Show("Se ha producido un error al registrar los Insumos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Se ha producido un error al intentar registrar la nueva pesada en base de datos.verifique la conexion con la base de datos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (CDbException cdbe)
                    {
                        MessageBox.Show("Se ha producido un error al intentar registrar la nueva pesada en base de datos.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
                registrados = CDb.RegistrarMovimientoInsumos(TYPE_INSUMO_MOV.Egreso, TYPE_INSUMO_PROC.PesajePieza, m_datPesadaActiva.Id,listInsumos);
            }
            return registrados;
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

        private void SetDatosPesadaActiva()
        {
            m_datPesadaActiva = new CPesada();
            m_datPesadaActiva.FechaHora = DateTime.Now;
            m_datPesadaActiva.IdEstacion = CDb.m_OperadorActivo.m_idEstacion;
            m_datPesadaActiva.Operador = CDb.m_OperadorActivo;
            m_datPesadaActiva.Oi = null;
            m_datPesadaActiva.Producto = (CProducto)textBox_producto.Tag;
            m_datPesadaActiva.Unidades = Convert.ToInt32(textBox_unidadesPredefinidas.Text);
            m_datPesadaActiva.PesoTara = GetPesoTara();
            m_datPesadaActiva.PesoNeto = GetPesoNeto();
            m_datPesadaActiva.Destino = (CDestino)textBox_destino.Tag;
            m_datPesadaActiva.Sector = (CSector)textBox_sector.Tag;
            m_datPesadaActiva.FechaVencimiento = DateTime.Today.AddDays(m_datPesadaActiva.Producto.DiasVencimientoPredefinido);
        }

        private float GetPesoNeto()
        {
            return m_DatPesaje.PesoNeto - (m_DatPesaje.PesoTara != 0.0f ? 0.0f : Convert.ToSingle(textBox_pesoTaraPredefinida.Text));
        }
        private float GetPesoTara()
        {
            return m_DatPesaje.PesoTara != 0.0f ? m_DatPesaje.PesoTara: Convert.ToSingle(textBox_pesoTaraPredefinida.Text);
        }

        private void ClrControlesPesada()
        {
            DateTime dt = DateTime.Now;
            label_Lote.Text = String.Format("{0:D2}{1:D2}{2:D4}", dt.Day, dt.Month, dt.Year);
            ClrControlesProducto();
            ClrControlesDestino();
            ClrControlesSector();
        }
        private void ClrControlesProducto()
        {
            textBox_producto.Text = "";
            textBox_producto.Tag = null;
            tableLayoutPanel_infoProducto.Visible = false;
            groupBox_insumos.Visible = false;
            button_imprimirEtiquetaContenedor.Enabled = false;
            ClrControlesAcumuladosProducto();
        }
        private void ClrControlesDestino()
        {
            textBox_destino.Text = "";
            textBox_destino.Tag = null;
        }
        private void ClrControlesSector()
        {
            textBox_sector.Text = "";
            textBox_sector.Tag = null;
        }

        private void ClrControlesAcumuladosProducto()
        {
            textBox_totalPesadasProducto.Text = "0";
            textBox_totalUnidadesProducto.Text = "0";
            textBox_totalBrutoProducto.Text = "0";
            textBox_totalNetoProducto.Text = "0";
        }
        private void CargarControlesConProductoActivo()
        {
            if (textBox_producto.Tag != null)
            {
                CProducto datProducto = (CProducto)textBox_producto.Tag;

                textBox_producto.Text = datProducto.NombreEtiL1;
                textBox_producto.Text += ("\r\n" + datProducto.NombreEtiL2);
                textBox_producto.Text += ("\r\n" + datProducto.NombreEtiL3);
                textBox_producto.Text += ("\r\n" + datProducto.NombreEtiL4);
                textBox_producto.Text += ("\r\n" + datProducto.NombreEtiL5);

                textBox_codigoProducto.Text = datProducto.ProductoSAC.Codigo.ToString();
                textBox_unidadesPredefinidas.Text = datProducto.UnidadesPredefinidas.ToString();
                textBox_pesoTaraPredefinida.Text = datProducto.PesoTaraPredefinida.ToString();
                CargarControlesConAcumuladosProducto();
                tableLayoutPanel_infoProducto.Visible = true;
                groupBox_insumos.Visible = true;
                SetEnableCtrlSecure(button_imprimirEtiquetaContenedor, true);
            }
        }
        private void CargarControlesConDestinoActivo()
        {
            if (textBox_destino.Tag != null)
            {
                textBox_destino.Text = ((CDestino)textBox_destino.Tag).Nombre;
            }
        }
        private void CargarControlesConSectorActivo()
        {
            if (textBox_sector.Tag != null)
            {
                textBox_sector.Text = ((CSector)textBox_sector.Tag).Nombre;
            }
        }

        private void CargarControlesConAcumuladosProducto()
        {
            if (textBox_producto.Tag != null)
            {
                CAcumulado acumulado = CDb.GetAcumuladosLotePorProducto(DateTime.Now,((CProducto)textBox_producto.Tag).Id);
                SetText_ControlSecure(textBox_totalPesadasProducto, acumulado.m_pesadas.ToString());
                SetText_ControlSecure(textBox_totalUnidadesProducto, acumulado.m_unidades.ToString());
                SetText_ControlSecure(textBox_totalBrutoProducto, acumulado.m_bruto.ToString());
                SetText_ControlSecure(textBox_totalNetoProducto, acumulado.m_neto.ToString());
            }
        }

        private bool VerificarControlesNuevaPesada()
        {
            bool verificacionOk = false;
            if (textBox_producto.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Producto", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_destino.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Destino", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_sector.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Sector", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_unidadesPredefinidas.Text == "" || Convert.ToInt32(textBox_unidadesPredefinidas.Text)==0)
            {
                MessageBox.Show("No ha editado la cantidad de unidades ", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!productoInsumosCtrl.esTodosInsumosConfirmados())
            {
                MessageBox.Show("No ha confirmado todos los insumos del producto.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                verificacionOk = true;

            return verificacionOk;
        }

        #endregion

        #region DATAGRID PESADAS_LOTE

        private void LoadDataGridPesadasLote()
        {
            DataSet dsPesadasOI = new DataSet();

            try
            {

                if (CDb.GetDatSet_PesadasLote(DateTime.Now, out dsPesadasOI))
                {
                    if (dsPesadasOI.Tables.Contains("PESADAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePesajeEnLote,dsPesadasOI);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePesajeEnLote,"PESADAS");
                        SetFormatDGVDetallePesajeEnLoteSecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePesajeEnLote, null);
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

        private void SetFormatDGVDetallePesajeEnLoteSecure()
        {
            if (dataGridView_detallePesajeEnLote.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePesajeEnLoteSecure(); }));
            }
            else
            {
                dataGridView_detallePesajeEnLote.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnLote.Columns["IDPESADA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnLote.Columns["IDPESADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnLote.Columns["FECHA_HORA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePesajeEnLote.Columns["FECHA_HORA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnLote.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnLote.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnLote.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnLote.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnLote.Columns["UNIDADES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnLote.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnLote.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnLote.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnLote.Columns["TARA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnLote.Columns["TARA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void dataGridView_detallePesajeEnLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_detallePesajeEnLote.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDataGridPesadasLote();
                }
            }
        }
        private bool BorrarRegistrosSeleccionadosDataGridPesadasLote()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_detallePesajeEnLote.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_detallePesajeEnLote.SelectedRows.Count;
                        int countDeletedRegisters = 0;

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de pesaje en el Lote: {1}  , confirma la eliminación ",countSelectRegisters, DateTime.Now.ToString("ddMMyyyy"));
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPesada;
                            foreach (DataGridViewRow dgvr in dataGridView_detallePesajeEnLote.SelectedRows)
                            {
                                idPesada = Convert.ToInt32(dgvr.Cells["IDPESADA"].Value);
                                if (!CDb.IsPartInEgresos(idPesada))
                                {
                                    if (CDb.BorrarPieza(idPesada))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPieza, TYPE_CONTEXT_DBLOG.PesajeEnProduccion,
                                        string.Format("Se Eliminó la pieza : {0}", idPesada));

                                        /// borra los insumos que pudo haber registrado la pesada.
                                        CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.PesajePieza, idPesada);
                                        
                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);
                            
                            if(countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar "+countDeletedRegisters+" registros de "+ countSelectRegisters + " seleccionados , dado que los registros de piezas que ya fueron egresadas no podran ser eliminados.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if(countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("No se han eliminado registros de pesaje dado que los mismos pertenecen a piezas ya egresadas.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            LoadDataGridPesadasLote();
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
        #endregion

    }
}
