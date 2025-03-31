using Commons;
using ConfigApp;
using Db;
using EditStringTouchDlg;
using EditValueTouchDlg;
using FlexibleMessageBox;
using HidKeyboardScannerLib;
using Logger;
using PdfStampa;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZebraScannerLib;

namespace MeatWeigherManager
{
    public partial class Form_EgresoDlg : Form
    {
        #region PROPERTYES
        const int CODBAR_MAX_LENGTH = 7;

        CPEgreso m_datPedidoActivo;
        CContenedor m_dataReadContenedor;


        CZebraScannerLib m_scannerDRV;
        HidScannerLib m_scannerHID;


        bool m_inPartRemovalMode = false;

        /// <summary>
        /// Evento de simulacion de lectura de scanner por edicion manual touch.
        /// </summary>
        public delegate void NewDataEditScanner(string canData);
        public event NewDataEditScanner OnNewDataEditScanner;

        private enum ESTADOS_APP
        {
            INICIADA,
            PARADA,
            PEDIDO_ABIERTO,
            INABILITADA
        };
        private enum TYPE_READ_VALUE_SCANNER
        {
            PIECE,
            BOX
        }

        private ESTADOS_APP m_Estado;

        /// <summary>
        /// Indica si esta en modo de captura normal o en modo de captura para eliminar piezas
        /// </summary>
        public bool InPartRemovalMode { get => m_inPartRemovalMode; set => m_inPartRemovalMode = value; }
        private TYPE_READ_VALUE_SCANNER TypeReadValueScanner { get; set; } = TYPE_READ_VALUE_SCANNER.PIECE;

        #endregion

        #region INICIALIZACION

        public Form_EgresoDlg()
        {
            InitializeComponent();
        }

        private void InitializeSystem()
        {
            if (CDb.isOpen)
            {
                LoadDataGridPiezasEgresadasPedido();
                SetEstado(ESTADOS_APP.PARADA);
            }
            else
            {
                SetEstado(ESTADOS_APP.INABILITADA);
            }
        }



        #endregion

        #region EVENTOS DEL FORM Y DE SUS CONTROLES
        private void Form_EgresoDlg_Load(object sender, EventArgs e)
        {
            InitializeSystem();
            InitializeScanner();
            WindowState = FormWindowState.Maximized;
        }

        private void Form_EgresoDlg_FormClosing(object sender, FormClosingEventArgs e)
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
            else if (keyData == (Keys.F3))
            {
                if (toolStripButton_AbrirPedido.Enabled)
                    toolStripButton_AbrirPedido.PerformClick();
            }
            else if (keyData == (Keys.F4))
            {
                if (toolStripButton_CerrarPedido.Enabled)
                    toolStripButton_CerrarPedido.PerformClick();
            }
            else if (keyData == (Keys.F5))
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
            BorrarRegistrosSeleccionadosDVGDetallePiezasEgresadas();
        }

        private void button_generarEtiquetasPedido_Click(object sender, EventArgs e)
        {
            Form_PrintLabelsPedido dlg = new Form_PrintLabelsPedido(m_datPedidoActivo,GetBultosColectados());
            dlg.ShowDialog();
        }

        #endregion

        #region EVENTOS MENU

        private void consultaEgresosPedidos_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaEgresosDlg dlg = new CViewConsultaEgresosDlg();
            dlg.ShowDialog();
        }

        #endregion

        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            ClrControlesCapturaEscaner();
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            SetEstado(ESTADOS_APP.PARADA);
        }
        private void toolStripButton_AbrirPedido_Click(object sender, EventArgs e)
        {
            CViewPedidosDlg dlg = new CViewPedidosDlg(m_datPedidoActivo?.Id ?? 0);
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                AbrirYCargarPedido(dlg.PedidoSelected);
            }
            else
                SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_ListarDetalleProductosEnPedidos_Click(object sender, EventArgs e)
        {
            CViewArticulosEnPedidosDlg dlg = new CViewArticulosEnPedidosDlg();
            dlg.ShowDialog();
        }

        private void toolStripButton_ModoEliminarPiezas_Click(object sender, EventArgs e)
        {
            InPartRemovalMode ^= true;
            if (InPartRemovalMode)
            {
                if (DialogResult.Yes != MessageBox.Show("Usted ha colocado al sistema en Modo Eliminación de Piezas ," +
                    " a partir de este momento todas las piezas escaneadas seran eliminadas del registro de egreso , permanece en este modo ?.",
                    "Notificación de Estado en Modo Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    InPartRemovalMode ^= true;
                }
            }
            SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode);
        }
        private void SetVisible_ToolStripLabelModoEliminacion(bool visible)
        {
            Invoke((MethodInvoker)delegate
            {
                toolStripLabel_ModoEliminacion.Visible = visible;
            });
        }

        private void toolStripButton_CerrarPedido_Click(object sender, EventArgs e)
        {
            if (m_Estado == ESTADOS_APP.PEDIDO_ABIERTO)
            {
                if (CerrarPedido())
                {
                    ClrControlesPedido();
                    SetEstado(ESTADOS_APP.INICIADA);

                    CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.CierreDePedido, TYPE_CONTEXT_DBLOG.Egresos,
                    string.Format("Se Cerró el Pedido : {0} con Comprobante : {1}", m_datPedidoActivo.Id, m_datPedidoActivo.ComprobantePedidoSAC));
                }
            }
        }

        private void button_declaracionInsumos_Click(object sender, EventArgs e)
        {
            CABM_DeclaracionInsumosPedidoDlg dlg = new CABM_DeclaracionInsumosPedidoDlg(m_datPedidoActivo);
            dlg.ShowDialog();
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
        private void SetEnableCtrlSecure(Control ctrl, bool enable)
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
        * Funcion:        SetTextANDColor_ToolStripStatusLabelSecure
        * Descripcion:    Esta Funcion es utilizada para cambiar el texto y color de una 
        *                 etiqueta en un item de un control StatusStrip. Este metodo esta
        *                 preparardo para ser accedido desde varios threads y no generar
        *                 excepciones.
        * Parametro:      StatusStrip ctrl: Control
        * Parametro:      int idxLabel: Indice al item a modificar
        * Parametro:      string valor: Valor de texto a escribir.
        * Parametro:      Color color: Color de texto
        ***********************************************************************************************/
        private void SetTextANDColor_ToolStripStatusLabelSecure(ToolStripStatusLabel ctrl, string valor, Color color)
        {
            if (ctrl.GetCurrentParent() != null)
            {
                if (ctrl.GetCurrentParent().InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    { SetTextANDColor_ToolStripStatusLabelSecure(ctrl, valor, color); }));
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
                        toolStripButton_AbrirPedido.Enabled = false;
                        toolStripButton_ListarDetalleProductosEnPedidos.Enabled = false;
                        toolStripButton_CerrarPedido.Enabled = false;
                        toolStripButton_ModoEliminarPiezas.Enabled = false;
                        tabControl_ProcesoEscaneo.Visible = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        groupBox_detallesPiezasEgresadas.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INHABILITADO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.PARADA:
                    {
                        toolStripButton_Iniciar.Enabled = true;
                        toolStripButton_Parar.Enabled = false;
                        toolStripButton_AbrirPedido.Enabled = false;
                        toolStripButton_ListarDetalleProductosEnPedidos.Enabled = false;
                        toolStripButton_CerrarPedido.Enabled = false;
                        toolStripButton_ModoEliminarPiezas.Enabled = false;
                        tabControl_ProcesoEscaneo.Visible = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        groupBox_detallesPiezasEgresadas.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);
                        SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode = false);
                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        toolStripButton_AbrirPedido.Enabled = true;
                        toolStripButton_ListarDetalleProductosEnPedidos.Enabled = true;
                        toolStripButton_ModoEliminarPiezas.Enabled = false;
                        toolStripButton_CerrarPedido.Enabled = false;
                        tabControl_ProcesoEscaneo.Visible = false;
                        groupBox_datCaptura.Enabled = false;
                        groupBox_datCaptura.Visible = false;
                        groupBox_detallesPiezasEgresadas.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INICIADO", Color.Red);
                        SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode = false);
                        break;
                    }
                case ESTADOS_APP.PEDIDO_ABIERTO:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        toolStripButton_AbrirPedido.Enabled = true;
                        toolStripButton_ListarDetalleProductosEnPedidos.Enabled = true;
                        toolStripButton_CerrarPedido.Enabled = true;
                        toolStripButton_ModoEliminarPiezas.Enabled = true;
                        tabControl_ProcesoEscaneo.Visible = true;
                        tabControl_ProcesoEscaneo.SelectTab("tabPage_ProcesoEscaneo");
                        groupBox_datCaptura.Enabled = true;
                        groupBox_datCaptura.Visible = true;
                        groupBox_detallesPiezasEgresadas.Visible = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "EN PROCESO DE ESCANEO DE EGRESOS", Color.Red);
                        SetVisible_ToolStripLabelModoEliminacion(InPartRemovalMode = false);
                        break;
                    }
            }
        }
        #endregion

        #region FUNCIONES GENERALES

        private void AbrirYCargarPedido(CPedido pedido)
        {
            ClrControlesPedido();
            ClrControlesCapturaEscaner();
            if (pedido != null)
            {
                //verifico si el pedido del sistema de pesaje estaba creado.
                CPedido pedidoSelected = new CPedido(pedido);

                if (pedidoSelected.Id == 0)
                {
                    CDb.GenerarPedido(ref pedidoSelected);
                }
                m_datPedidoActivo = new CPEgreso(pedidoSelected);
                CargarControlesConPedidoActivo();
                LoadDataGridPiezasEgresadasPedido();
                LoadDataGridTotalesPiezasEgresadasPedido();
                SetEstado(ESTADOS_APP.PEDIDO_ABIERTO);
            }
            else
            {
                SetEstado(ESTADOS_APP.INICIADA);
            }
        }

        private bool CerrarPedido()
        {
            bool registroCerradoOk = false;
            string resultActualizarPedidoSAC = "Error desconocido";
            try
            {
                string aviso = String.Format("Usted esta a punto de Cerrar un Pedido con Numero de Comprobante: {0} . Una vez cerrado no podra volver a abrirlo para continuar realizando egresos !! , confirma el cierre ?? ",
                    m_datPedidoActivo.ComprobantePedidoSAC);
                if (FMB.Show(aviso, "CONFIRMACIÓN DE CIERRE DE PEDIDO",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning, new Font("Tahoma", 16)) == DialogResult.Yes)
                {
                    if (!IsAllProductsCollected())
                    {
                        aviso = "El Pedido no esta completo , faltan colectar articulos. Desea igualmente proceder a cerrar el pedido ?? ";
                        if (FMB.Show(aviso, "AVISO DE PEDIDO INCOMPLETO",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question, new Font("Tahoma", 16)) == DialogResult.No)
                            return false;
                    }
                    if (IsMoreProductsCollectedThanOrders())
                    {
                        if(CConfigApp.m_permitirColectarMasCantidadesQueLasPedidasEnDespachos)
                        {
                            aviso = "El Pedido posee articulos con mas unidades colectadas que la pedida. Desea igualmente proceder a cerrar el pedido ?? ";
                            if (FMB.Show(aviso, "AVISO DE PEDIDO CON MAS UNIDADES COLECTADAS",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, new Font("Tahoma", 16)) == DialogResult.No)
                                return false;
                        }
                        else
                        {
                            aviso = "No podrá Cerrar un pedido que posee articulos con mas unidades colectadas que las pedidas. Rectifique el mismo para poder realizar el cierre !! ";
                            FMB.Show(aviso, "VALIDACIÓN DE PEDIDO CON MAS UNIDADES COLECTADAS",
                                            MessageBoxButtons.OK,MessageBoxIcon.Error, new Font("Tahoma", 16));
                            return false;

                        }
                    }
                    if (!ThereAreInsumosInPedido())
                    {
                        aviso = "No se han declarado insumos para asociar a este Pedido , desea igualmente continuar con el cierre del Pedido ?? ";
                        if (FMB.Show(aviso, "AVISO DE PEDIDO INSUMOS NO DECLARADOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question, new Font("Tahoma", 16)) == DialogResult.No)
                            return false;
                    }
                    if (GenerarRemitoDespachoSAC(out resultActualizarPedidoSAC))
                    {
                        if (CDb.CerrarPedido(m_datPedidoActivo.Id))
                        {
                            registroCerradoOk = true;
                            MessageBox.Show("El Pedido ha sido cerrado !!!.", "Confirmación de Cierre de Pedido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                            MessageBox.Show("No se pudo cerrar el pedido.", "Error Cerrando el pedido en base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("No se pudo cerrar el pedido en la base de datos de SAC. Error: " + resultActualizarPedidoSAC, "Error Cerrando el Pedido en SAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando un Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registroCerradoOk;
        }

        ///****************************************************************************************
        /// <summary>
        /// Genera en el sistema SAC un remito con todos los items del pedido que se han colectado
        /// </summary>
        /// <param 
        /// name="resultUpdateSAC">
        /// retorna el resultado del SP 
        /// </param>
        /// <returns></returns>
        ///****************************************************************************************
        private bool GenerarRemitoDespachoSAC(out string result)
        {
            bool actualizacionok = false;
            result = "Error desconocido";

            try
            {
                actualizacionok = CDb.CrearRemitoDespachoSAC(m_datPedidoActivo.CodigoPedidoSAC, m_datPedidoActivo.Cliente.Codigo, m_datPedidoActivo.Id, CDb.m_OperadorActivo.m_idEstacion, out result) && result.ToUpper().Contains("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cerrando el Pedido en SAC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return actualizacionok;
        }

        private async void RegisterReadPiece(string codBarRead)
        {
            if (m_Estado == ESTADOS_APP.PEDIDO_ABIERTO)
            {
                SetDataReadPiece(codBarRead);
                try
                {
                    if (!InPartRemovalMode)
                    {
                        if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.PIECE)
                        {
                            if (await IsValidEgressReadPiece())
                            {
                                if (CDb.RegisterEgressPart(m_datPedidoActivo))
                                {
                                    LoadDataGridPiezasEgresadasPedido();
                                    LoadDataGridTotalesPiezasEgresadasPedido();
                                    SetMessageReadyScanner("La pieza fue registrada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar registrar un egreso.verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.BOX)
                        {
                            if (await IsValidEgressReadContainer())
                            {
                                if (CDb.RegisterEgressContainer(m_dataReadContenedor, m_datPedidoActivo.Id))
                                {
                                    LoadDataGridPiezasEgresadasPedido();
                                    LoadDataGridTotalesPiezasEgresadasPedido();
                                    SetMessageReadyScanner("El Contenedor fue registrado con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar registrar un egreso de un contenedor .verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                                string detailResult;
                                if (CDb.BorrarPiezaEgresadaDelPedido(m_datPedidoActivo.Pieza.Id, m_datPedidoActivo.IdPedido, out detailResult))
                                {
                                    LoadDataGridPiezasEgresadasPedido();
                                    LoadDataGridTotalesPiezasEgresadasPedido();
                                    SetMessageReadyScanner("La pieza fue Eliminada con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar eliminar una pieza egresada .verifique la conexion con la base de datos.", "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (TypeReadValueScanner == TYPE_READ_VALUE_SCANNER.BOX)
                        {
                            if (await IsValidDeleteReadBox())
                            {
                                string detailResult;
                                if (CDb.BorrarContenedorEgresadoDelPedido(m_dataReadContenedor.Id, m_datPedidoActivo.IdPedido, out detailResult))
                                {
                                    LoadDataGridPiezasEgresadasPedido();
                                    LoadDataGridTotalesPiezasEgresadasPedido();
                                    SetMessageReadyScanner("El contenedor fue Eliminado del egreso con exito !!.", Color.Green);
                                    m_scannerDRV?.Beep(BEEPLED_TYPE.HIGH_LOW_BEEP);
                                    await SetOnOffLedOK();
                                }
                                else
                                {
                                    MessageBox.Show("Se ha producido un error al intentar eliminar un contenedor del egreso .verifique la conexion con la base de datos.", "REGISTRACION DE CONTENEDOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                    }
                }
                catch (CDbException cdbe)
                {
                    MessageBox.Show("Se ha producido un error al intentar registrar o eliminar egresos.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PIEZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetEnableToolStripButtomsStartStop(bool enable)
        {
            toolStripButton_Iniciar.Enabled = enable;
            toolStripButton_Parar.Enabled = enable;
        }
        private void ClrControlesPedido()
        {
            label_comprobantePedido.Text = "";
            label_modoPedido.Text = "";
            label_fechaEntregaPedido.Text = "";
            label_cliente.Text = "";
        }
        private void CargarControlesConPedidoActivo()
        {
            label_comprobantePedido.Text = m_datPedidoActivo.ComprobantePedidoSAC;
            label_modoPedido.Text = m_datPedidoActivo.TipoPedidoSAC;
            label_fechaEntregaPedido.Text = m_datPedidoActivo.FechaHoraPreparacion.ToString("dd-MM-yyyy");
            label_cliente.Text = m_datPedidoActivo.Cliente.Nombre;
        }
        private int GetBultosColectados()
        {
            return dataGridView_detallePiezasEgresadasPedido.Rows.Count;
        }
        #endregion

        #region FUNCIONES DE CONTROL DEL ESCANER
        private void InitializeScanner()
        {
            if (ConfigApp.CConfigApp.m_hostInterfaceScaneer == HostInterfaceScanner.SNAPI_CoreScanner)
            {
                m_scannerDRV = new CZebraScannerLib(CConfigApp.m_modeloScannerZebra);
                m_scannerDRV.ListCodBarEnables.Add(CODBAR_TYPE.QR);
                m_scannerDRV.ListCodBarEnables.Add(CODBAR_TYPE.COD128);
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
        private void ClrControlesCapturaEscaner()
        {
            SetText_ControlSecure(textBox_valueReadCodBar, "");
        }

        /// <summary>
        /// Permite al Operador simular la lectura del scanner editando su valor de codigo de barras de la pieza.
        /// Dispara un evento que genera el mismo efecto que lograria la lectura del escanner.
        /// </summary>
        private void textBox_valueReadCodBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CConfigApp.m_permiteSimularLecturaScanner)
            {
                CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Numero de Pieza o Caja", "Numero","",10);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox_valueReadCodBar.Text = dlg.VALUE.ToUpper();
                    OnNewDataEditScanner?.Invoke(dlg.VALUE.ToUpper());
                }
            }
        }

        private void SetDataReadPiece(string codeBarRead)
        {
            int id = 0;
            m_dataReadContenedor = null;
            if (codeBarRead != "" && codeBarRead.Length >= 3 && codeBarRead[0] == 'A' && codeBarRead[codeBarRead.Length - 1] == 'A')
            {
                codeBarRead = codeBarRead.Remove(0, 1);
                codeBarRead = codeBarRead.Remove(codeBarRead.Length - 1, 1);
                Int32.TryParse(codeBarRead, out id);
                m_dataReadContenedor = CDb.GetContenedor(id);
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.BOX;
            }
            else
            {
                Int32.TryParse(codeBarRead, out id);
                CPesada datpieza = CDb.GetPesada(id);
                if (datpieza != null)
                {
                    m_datPedidoActivo.Pieza = new CPesada(datpieza);
                    m_datPedidoActivo.IdEstacionRegistration = CDb.m_OperadorActivo.m_idEstacion;
                    m_datPedidoActivo.IdOperadorRegistration = CDb.m_OperadorActivo.m_id;
                }
                else
                    m_datPedidoActivo.Pieza = null;
                TypeReadValueScanner = TYPE_READ_VALUE_SCANNER.PIECE;
            }
        }

        private async Task<bool> IsValidEgressReadPiece()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string detailResult;
                if (m_datPedidoActivo == null || m_datPedidoActivo.Pieza == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidPartForEgress(m_datPedidoActivo.Pieza.Id, m_datPedidoActivo.IdPedido, out detailResult))
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
        private async Task<bool> IsValidEgressReadContainer()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string detailResult;
                if (m_dataReadContenedor == null)
                {
                    SetMessageReadyScanner("La caja colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidContainerForEgress(m_dataReadContenedor.Id, m_datPedidoActivo.IdPedido, out detailResult))
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
                string detailResult;

                if (m_datPedidoActivo == null)
                {
                    SetMessageReadyScanner("La pieza colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidDeleteEgressPart(m_datPedidoActivo.Pieza.Id, m_datPedidoActivo.IdPedido, out detailResult))
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
        private async Task<bool> IsValidDeleteReadBox()
        {
            return await Task.Run(async () =>
            {
                bool verificacionOk = false;
                string detailResult;
                if (m_dataReadContenedor == null)
                {
                    SetMessageReadyScanner("La caja colectada no existe !!.", Color.Red);
                    await ExecuteExceptionToScanner("");
                    await SetOnOffLedError();
                }
                else if (!CDb.IsValidDeleteEgressContainer(m_dataReadContenedor.Id, m_datPedidoActivo.IdPedido, out detailResult))
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

        #endregion

        #region DATAGRID DETALLE PIEZAS EGRESADAS
        private void dataGridView_detallePiezasEgresadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_detallePiezasEgresadasPedido.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDVGDetallePiezasEgresadas();
                }
            }
        }

        private void LoadDataGridPiezasEgresadasPedido()
        {
            DataTable dtPiezasEgresadasPedido = new DataTable();

            try
            {
                //TIPO,NRO,PRODUCTO,LOTE,UNDS,NETO,FECHA_EGRESO,OPERADOR
                if (CDb.GetDatSet_BultosEgresadosPorPedido(m_datPedidoActivo?.Id ?? 0, out dtPiezasEgresadasPedido))
                {
                    if (dtPiezasEgresadasPedido != null && dtPiezasEgresadasPedido.Rows.Count > 0)
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasEgresadasPedido, dtPiezasEgresadasPedido);
                        SetFormatDGVDetallePiezasEgresadasPedidoSecure();
                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePiezasEgresadasPedido, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas Egresadas del pedido.", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void SetFormatDGVDetallePiezasEgresadasPedidoSecure()
        {
            if (dataGridView_detallePiezasEgresadasPedido.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePiezasEgresadasPedidoSecure(); }));
            }
            else
            {
                //TIPO,NRO,PRODUCTO,LOTE,UNDS,NETO,FECHA_EGRESO,OPERADOR
                dataGridView_detallePiezasEgresadasPedido.Columns["FECHA_EGRESO"].Visible = false;

                dataGridView_detallePiezasEgresadasPedido.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasEgresadasPedido.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasEgresadasPedido.Columns["TIPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasEgresadasPedido.Columns["NRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasEgresadasPedido.Columns["NRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasEgresadasPedido.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_detallePiezasEgresadasPedido.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasEgresadasPedido.Columns["LOTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePiezasEgresadasPedido.Columns["LOTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePiezasEgresadasPedido.Columns["UNDS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasEgresadasPedido.Columns["UNDS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasEgresadasPedido.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePiezasEgresadasPedido.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePiezasEgresadasPedido.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePiezasEgresadasPedido.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }
        private bool BorrarRegistrosSeleccionadosDVGDetallePiezasEgresadas()
        {
            bool borradoOk = false;
            string detailResult;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_detallePiezasEgresadasPedido.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_detallePiezasEgresadasPedido.SelectedRows.Count;
                        int countDeletedRegisters = 0;

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de piezas , cajas o combos egresados en el Pedido: {1}  , confirma la eliminación ", countSelectRegisters, m_datPedidoActivo.ComprobantePedidoSAC);
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPieza;
                            string Tipo;

                            foreach (DataGridViewRow dgvr in dataGridView_detallePiezasEgresadasPedido.SelectedRows)
                            {
                                idPieza = Convert.ToInt32(dgvr.Cells["NRO"].Value);
                                Tipo = dgvr.Cells["TIPO"].Value.ToString();

                                if (Tipo == "PIEZA")
                                {
                                    if (CDb.BorrarPiezaEgresadaDelPedido(idPieza, m_datPedidoActivo.IdPedido, out detailResult))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionEgresoPieza, TYPE_CONTEXT_DBLOG.Egresos,
                                        string.Format("Se elimino el Egreso de la pieza: {0} en el Pedido: {1} Comprobante: {2} ", idPieza,m_datPedidoActivo.Id, m_datPedidoActivo.ComprobantePedidoSAC));

                                        countDeletedRegisters++;
                                    }
                                }
                                if (Tipo == "CAJA" || Tipo == "COMBO")
                                {
                                    if (CDb.BorrarContenedorEgresadoDelPedido(idPieza, m_datPedidoActivo.IdPedido, out detailResult))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionEgresoContenedor, TYPE_CONTEXT_DBLOG.Egresos,
                                        string.Format("Se elimino el Egreso del Contenedor: {0} en el Pedido: {1} Comprobante: {2} ", idPieza, m_datPedidoActivo.Id, m_datPedidoActivo.ComprobantePedidoSAC));

                                        countDeletedRegisters++;

                                    }
                                }

                            }
                            borradoOk = (countDeletedRegisters > 0);

                            dataGridView_detallePiezasEgresadasPedido.Update();
                            LoadDataGridPiezasEgresadasPedido();
                            LoadDataGridTotalesPiezasEgresadasPedido();
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

        #region DATAGRID TOTAL PIEZAS EGRESADAS
        private void LoadDataGridTotalesPiezasEgresadasPedido()
        {
            DataTable dtTotalPiezas = new DataTable();

            try
            {
                //ITEM_PRD_SAC,CODIGO_SAC,PRODUCTO_SAC,OBSERVACION,UNDS_PED,PESO_PED,UNDS_COL,PESO_COL
                if (CDb.GetDataTable_TotalesPorProductoDePiezasEgresadasPedido(m_datPedidoActivo.Id,out dtTotalPiezas))
                {
                    if (dtTotalPiezas != null && dtTotalPiezas.Rows.Count > 0)
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_totalesPiezasEgresadasPedido, dtTotalPiezas);
                        SetFormatDGVTotalesPiezasEgresadasPedidoSecure();
                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_totalesPiezasEgresadasPedido, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de piezas totales egresadas del pedido", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool IsAllProductsCollected()
        {
            bool allCollectd = (from DataGridViewRow row in dataGridView_totalesPiezasEgresadasPedido.Rows
                                where Convert.ToInt32(row.Cells["UNDS_COL"].Value) == 0
                                select row).Count() == 0;
            return allCollectd;
        }

        /// <summary>
        /// verifica si hay mas productos colectados que los pedidos. 
        /// </summary>
        private bool IsMoreProductsCollectedThanOrders()
        {
            bool allMore = (from DataGridViewRow row in dataGridView_totalesPiezasEgresadasPedido.Rows
                                where Convert.ToInt32(row.Cells["UNDS_COL"].Value) > Convert.ToInt32(row.Cells["UNDS_PED"].Value)
                            select row).Count() > 0;
            return allMore;
        }

        private bool ThereAreInsumosInPedido()
        {
            return CDb.GetInsumosPedido(m_datPedidoActivo.Id).Count() > 0;
        }

        private void SetFormatDGVTotalesPiezasEgresadasPedidoSecure()
        {
            if (dataGridView_totalesPiezasEgresadasPedido.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVTotalesPiezasEgresadasPedidoSecure(); }));
            }
            else
            {
                //ITEM_PRD_SAC,CODIGO_SAC,PRODUCTO_SAC,OBSERVACION,UNDS_PED,PESO_PED,UNDS_COL,PESO_COL
                dataGridView_totalesPiezasEgresadasPedido.Columns["ITEM_PRD_SAC"].Visible = false;
                dataGridView_totalesPiezasEgresadasPedido.Columns["CODIGO_SAC"].Visible = false;

                dataGridView_totalesPiezasEgresadasPedido.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PRODUCTO_SAC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PRODUCTO_SAC"].Resizable = DataGridViewTriState.True;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PRODUCTO_SAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_totalesPiezasEgresadasPedido.Columns["OBSERVACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView_totalesPiezasEgresadasPedido.Columns["OBSERVACION"].Resizable =DataGridViewTriState.True;
                dataGridView_totalesPiezasEgresadasPedido.Columns["OBSERVACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_totalesPiezasEgresadasPedido.Columns["UNDS_PED"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasEgresadasPedido.Columns["UNDS_PED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasEgresadasPedido.Columns["UNDS_PED"].DefaultCellStyle.Format = "0";
                dataGridView_totalesPiezasEgresadasPedido.Columns["UNDS_COL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasEgresadasPedido.Columns["UNDS_COL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PESO_PED"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PESO_PED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PESO_COL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PESO_COL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_totalesPiezasEgresadasPedido.Columns["PESO_COL"].DefaultCellStyle.Format = "0.##";
            }
        }
        #endregion

        #region PRINT Egreso
        private void button_print_Click(object sender, EventArgs e)
        {
            if (dataGridView_totalesPiezasEgresadasPedido != null && dataGridView_totalesPiezasEgresadasPedido.Rows.Count > 0)
            {
                DateTime dateFirstPartOut = CDb.GetFechaHoraPrimerPiezaEgresada(m_datPedidoActivo.Id);

                DataTable dt = ((DataTable)dataGridView_totalesPiezasEgresadasPedido.DataSource).Copy();

                CPdfStampaGridReport pdf = new CPdfStampaGridReport()
                {
                    PathDestinationReport = ConfigApp.CConfigApp.m_pathDirectorioReportes,
                    FullPathLogoCompany = ConfigApp.CConfigApp.m_pathLogoEmpresa,
                    DataSource = dt,
                    CompanyDescription = ConfigApp.CConfigApp.m_razonSocialEmpresa,
                    NameFileFormatXML = "FormatoDetalleTotalEgresos",
                    NameFilePDF = String.Format("EGRESOS PEDIDO COMPROBANTE({0})", m_datPedidoActivo.ComprobantePedidoSAC),
                    ReportDescriptionLine1 = String.Format("PEDIDO Nº: {0} Fecha Egreso: {1}", m_datPedidoActivo.ComprobantePedidoSAC, dateFirstPartOut),
                    ReportDescriptionLine2 = String.Format("CLIENTE: {0} ", m_datPedidoActivo.Cliente.Nombre),
                    TitleDescription = "DETALLE DE EGRESOS DE PEDIDO"
                };
                pdf.Create();
            }
        }
        #endregion

    }
}
