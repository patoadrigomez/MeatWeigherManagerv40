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
using ListViewExtendedItem;
using Db.Entities;
using Extensions;

namespace MeatWeigherManager
{
    public partial class Form_IngresoAPlanta : Form
    {
        public volatile int vueltas = 0;
        CDatScale m_DatPesaje;
        COi m_datOIActiva;
        CPesada m_datPesadaActiva;
        List<ScaleForm> m_listScales;
        bool m_isManualRegistrationRequest = false;

        private enum ESTADOS_APP
        {
            INICIADA,
            PARADA,
            OI_NUEVA,
            OI_ABIERTA,
            OI_EDICION,
            INABILITADA
        };

        /// <summary>
        /// Indica que balanza esta visible en el dialogo
        /// </summary>
        private ScaleForm m_activeScaleForm;

        private ESTADOS_APP m_Estado;
        /// <summary>
        /// Flag que indica que el pedido de registracion de pesada fue manual y no
        /// por balanza.
        /// </summary>
        public bool IsManualRegistrationRequest { get => m_isManualRegistrationRequest; set => m_isManualRegistrationRequest = value; }
        public bool EnableActionsClosing { get; private set; } = true;

        #region INICIALIZACION

        public Form_IngresoAPlanta()
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
                            CountDecimals=sf.Decimales
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
        private async void Form_IngresoAPlanta_FormClosing(object sender, FormClosingEventArgs e)
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
            else if (keyData == (Keys.F3))
            {
                if (toolStripButton_NuevaOI.Enabled)
                    toolStripButton_NuevaOI.PerformClick();
            }
            else if (keyData == (Keys.F4))
            {
                if (toolStripButton_SalvarNuevaOI.Enabled)
                    toolStripButton_SalvarNuevaOI.PerformClick();
            }
            else if (keyData == (Keys.F5))
            {
                if (toolStripButton_EditarOI.Enabled)
                    toolStripButton_EditarOI.PerformClick();
            }
            else if (keyData == (Keys.F6))
            {
                if (toolStripButton_AbrirOI.Enabled)
                    toolStripButton_AbrirOI.PerformClick();
            }
            else if (keyData == (Keys.F7))
            {
                if (toolStripButton_CerrarOI.Enabled)
                    toolStripButton_CerrarOI.PerformClick();
            }
            else if (keyData == (Keys.F8))
            {
                if (toolStripButton_EliminarOI.Enabled)
                    toolStripButton_EliminarOI.PerformClick();
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
        private void textBox_certificadoSanitario_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Certificado Sanitario", "Numero",textBox_certificadoSanitario.Text,15);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_certificadoSanitario.Text = dlg.VALUE;
            }
        }
        private void textBox_pesoRemitido_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoRemitidoTotal.Text, "Peso remitido", "Editar el Peso Remitido del Producto", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoRemitidoTotal.Text = dlg.VALUE;
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
        private void textBox_unidadesTotalesRemitidas_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_unidadesTotalesRemitidas.Text, "Unidades Totales Remitidas", "Editar la Cantidad de Unidades Totales Remitidas", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_unidadesTotalesRemitidas.Text = dlg.VALUE;
            }
        }


        private void textBox_pesoNetoPredefinido_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoNetoPredefinido.Text, "Peso Neto", "Editar Peso Neto ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoNetoPredefinido.Text = dlg.VALUE;
            }
        }

        private void textBox_bultos_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_bultos.Text, "Bultos", "Editar la Cantidad de Bultos ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_bultos.Text = dlg.VALUE;
            }
        }
        private void textBox_tropa_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_tropa.Text, "Tropa", "Editar el numero de Tropa ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_tropa.Text = dlg.VALUE;
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
        private void button_EdicionRemitos_MouseHover(object sender, EventArgs e)
        {
            if(m_datOIActiva != null && m_datOIActiva.m_remitos != null && m_datOIActiva.m_remitos.Count > 0)
            {
                string listText = "Remitos: ";
                foreach(string str in m_datOIActiva.m_remitos)
                {
                    listText += str + " ";
                }
                ToolTip tt = new ToolTip();
                tt.Show(listText, (Button)sender, 0, 0, 1000);
            }
        }
        private void button_EdicionFacturas_MouseHover(object sender, EventArgs e)
        {
            if (m_datOIActiva != null && m_datOIActiva.m_facturas != null && m_datOIActiva.m_facturas.Count > 0)
            {
                string listText = "Facturas: ";
                foreach (string str in m_datOIActiva.m_facturas)
                {
                    listText += str + " ";
                }
                ToolTip tt = new ToolTip();
                tt.Show(listText, (Button)sender, 0, 0, 1000);
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

        private void textBox_unidadesTotalesRemitidas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_unidadesTotalesRemitidas.Text, "Unidades Totales Remitidas", "Editar la Cantidad de Unidades Totales Remitidas", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_unidadesTotalesRemitidas.Text = dlg.VALUE;
            }
        }

        private void textBox_diasVencimiento_DoubleClick_1(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_diasVencimiento.Text, "Días de Vencimiento", "Editar los días de vencimiento", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_diasVencimiento.Text = dlg.VALUE;
            }
        }

        private void textBox_pesoRemitidoTotal_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoRemitidoTotal.Text, "Peso remitido", "Editar el Peso Remitido del Producto", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoRemitidoTotal.Text = dlg.VALUE;
            }
        }

        private void textBox_bultos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_bultos.Text, "Bultos", "Editar la Cantidad de Bultos ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_bultos.Text = dlg.VALUE;
            }
        }

        private void textBox_pesoTaraPredefinida_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoTaraPredefinida.Text, "Tara", "Editar la Tara ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoTaraPredefinida.Text = dlg.VALUE;
            }
        }

        private void textBox_pesoNetoPredefinido_MouseClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoNetoPredefinido.Text, "Peso Neto", "Editar Peso Neto ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoNetoPredefinido.Text = dlg.VALUE;
            }
        }

        private void textBox_unidadesPredefinidas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_unidadesTotalesRemitidas.Text, "Unidades Totales Remitidas", "Editar la Cantidad de Unidades Totales Remitidas", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_unidadesTotalesRemitidas.Text = dlg.VALUE;
            }
        }

        #endregion

        #region EVENTOS MENU

        private void consultaPesajes_OIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaOIPesadasDlg dlg = new CViewConsultaOIPesadasDlg();
            dlg.ShowDialog();
        }

        private void toolStripContextMenuDataGridViewPesadas_ItemBorrar_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                if (BorrarRegistrosSeleccionadosDataGridPesadasOI())
                    CargarControlesConAcumuladosProducto();
            }
        }
        
        private void reimprimirEtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CPesada> listPesadas = CDb.GetWeightingFromSelectedDGVPesadas(dataGridView_detallePesajeEnOI,"IDPESADA");

                if (listPesadas != null && listPesadas.Count > 0)
                {
                    foreach(CPesada pesada in listPesadas)
                    {
                        CLabel.PrintProduct(pesada, ConfigApp.CConfigApp.m_ingresoAPlanta_WeightLabelEnable,true, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 1);
                    }
                }
            }
        }

        #endregion
        
        #region EVENTOS TOOLSTRIPBUTTONS
        private void toolStripButton_Iniciar_Click(object sender, EventArgs e)
        {
            SetEstado(ESTADOS_APP.INICIADA);
        }
        private void toolStripButton_Parar_Click(object sender, EventArgs e)
        {
            ClrControlesOI();
            SetEstado(ESTADOS_APP.PARADA);
        }

        private void toolStripButton_NuevaOI_Click(object sender, EventArgs e)
        {
            ClrControlesOI();
            SetEstado(ESTADOS_APP.OI_NUEVA);
        }

        private void toolStripButton_SalvarNuevaOI_Click(object sender, EventArgs e)
        {
            if (m_Estado == ESTADOS_APP.OI_NUEVA)
            {
                if (VerificarControlesNuevaOI())
                {
                    GetDatosControlesNuevaOI();
                    if (CDb.RegistrarOI(ref m_datOIActiva))
                    {
                        RefrescarControlesOI();
                        SetEstado(ESTADOS_APP.OI_ABIERTA);
                        MessageBox.Show("Se realizo con Exito la creación de una nueva Orden de Ingreso, en este momento queda activa para continuar con el proceso de pesaje.", "CONTROL DE NUEVA ORDEN DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar la nueva Orden de Ingreso en la Base de Datos. Verifique la conectividad con la misma.", "CONTROL DE NUEVA ORDEN DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (m_Estado == ESTADOS_APP.OI_EDICION)
            {
                if (VerificarControlesNuevaOI())
                {
                    GetDatosControlesEdicionOI();
                    if (CDb.UpdateOI(ref m_datOIActiva))
                    {
                        SetEstado(ESTADOS_APP.OI_ABIERTA);
                        MessageBox.Show("Se modificaron con Exito los datos de la Orden de Ingreso Activa.", "CONTROL DE MODIFICACÍON DE ORDEN DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar la Orden de Ingreso en la Base de Datos. Verifique la conectividad con la misma.", "CONTROL DE MODIFICACÍON DE ORDEN DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButton_AbrirOI_Click(object sender, EventArgs e)
        {
            CViewOICreadasDlg dlg = new CViewOICreadasDlg("MeatWeigherManager - SELECCION DE ORDEN DE INGRESO", CViewOICreadasDlg.MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI);
            dlg.ShowDialog();
            ClrControlesOI();
            if (dlg.OISelected.m_id != 0)
            {
                m_datOIActiva = dlg.OISelected;
                CargarControlesConOIActiva();
                SetEstado(ESTADOS_APP.OI_ABIERTA);
            }
            else
            {
                SetEstado(ESTADOS_APP.INICIADA);
            }
        }

        private void toolStripButton_EliminarOI_Click(object sender, EventArgs e)
        {
            if (BorrarOIActiva())
            {
                CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionOrdenDeIngreso, TYPE_CONTEXT_DBLOG.IngresoAPlanta,
                    string.Format("Se Elimino la Orden de Ingreso: {0}", m_datOIActiva.m_id));
            }
            ClrControlesOI();
            SetEstado(ESTADOS_APP.INICIADA);
        }

        private void toolStripButton_EditarOI_Click(object sender, EventArgs e)
        {
            SetEstado(ESTADOS_APP.OI_EDICION);
        }

        private void toolStripButton_CerrarOI_Click(object sender, EventArgs e)
        {
            if (m_Estado == ESTADOS_APP.OI_ABIERTA)
            {
                if (CerrarOIActiva())
                {
                    CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.CierreDeOrdenDeIngreso,
                               TYPE_CONTEXT_DBLOG.IngresoAPlanta,
                               string.Format("Se Cerró la Orden de Ingreso: {0}", m_datOIActiva.m_id));
                }
                ClrControlesOI();
                SetEstado(ESTADOS_APP.INICIADA);
            }
        }

        #endregion

        #region EVENTOS BUTTONS

        private void button_seleccionarProducto_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_producto.Tag != null ? ((CProducto)textBox_producto.Tag).Id : 0;
            CSelProductoDlg abmDlg = new CSelProductoDlg(idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.ProductoSelected.Id != 0)
            {
                textBox_producto.Tag = abmDlg.ProductoSelected;
                CargarControlesConProductoActivo();
            }
            else
            {
                ClrControlesProducto();
            }
        }
        private void button_seleccionarDestino_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_destino.Tag != null ? ((CDestino)textBox_destino.Tag).Id : 0;
            CABM_DestinosDlg abmDlg = new CABM_DestinosDlg(CDb.m_oleDbConnection,CABM_DestinosDlg.BEHAVIOR_MODE.SELECTION,idSelected);
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
        private void button_selectTipificacion_Click(object sender, EventArgs e)
        {
            int idSelected = textBox_tipificacion.Tag != null ? ((Tipificacion)textBox_tipificacion.Tag).Id : 0;
            CABM_TipificacionesDlg abmDlg = new CABM_TipificacionesDlg(CDb.m_oleDbConnection, CABM_TipificacionesDlg.BEHAVIOR_MODE.SELECTION, idSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK && abmDlg.TipificacionSelected.Id != 0)
            {
                textBox_tipificacion.Tag = abmDlg.TipificacionSelected;
                CargarControlesConTipificacionActiva();
            }
            else
            {
                ClrControlesTipificacion();
            }
        }

        private void button_EdicionRemitos_Click(object sender, EventArgs e)
        {
            CAB_RemitosDlg abRemitosDlg = new CAB_RemitosDlg(ref m_datOIActiva.m_remitos);
            abRemitosDlg.ShowDialog();
        }

        private void button_EdicionFacturas_Click(object sender, EventArgs e)
        {
            CAB_FacturasDlg abFacturasDlg = new CAB_FacturasDlg(ref m_datOIActiva.m_facturas);
            abFacturasDlg.ShowDialog();
        }

        private void button_seleccionarProveedor_Click(object sender, EventArgs e)
        {
            string codSelected = textBox_proveedor.Tag != null ? ((CProveedorSAC)textBox_proveedor.Tag).Codigo : "";
            CSelProveedorSACDlg abmDlg = new CSelProveedorSACDlg(codSelected);
            if (abmDlg.ShowDialog() == DialogResult.OK &&  abmDlg.ProveedorSelected.Codigo != "")
            {
                textBox_proveedor.Tag = abmDlg.ProveedorSelected;
                textBox_proveedor.Text = abmDlg.ProveedorSelected.Nombre;
            }
            else
            {
                textBox_proveedor.Tag = null;
                textBox_proveedor.Text = "";
            }
        }

        private void RegistrarPesada()
        {
            if (m_Estado == ESTADOS_APP.OI_ABIERTA)
            {
                if (VerificarControlesNuevaPesada())
                {
                    SetDatosPesadaActiva();
                    m_datPesadaActiva.Manual = false;
                    
                    if (m_datPesadaActiva.Bultos == 1)
                        RegistrarPesadaSimple();
                    else
                        RegistrarPesadaGrupal();
                    
                    ResetTropaSelected();
                }
            }
        }

        private void ResetTropaSelected()
        {
            if(!ConfigApp.CConfigApp.m_mantenerUltimaTropaEntrePesajes)
            {
                ClrControlesTropa();
            }
            else if(!ConfigApp.CConfigApp.m_mantenerUltimaTipificacionEntrePesajes)
            {
                ClrControlesTipificacion();
            }
        }

        private void RegistrarPesadaSimple()
        {
            try
            {
                if (CDb.RegistrarPesada(ref m_datPesadaActiva))
                {
                    CLabel.PrintProduct(m_datPesadaActiva, ConfigApp.CConfigApp.m_ingresoAPlanta_WeightLabelEnable,true, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 1);
                    CargarControlesConAcumuladosProducto();
                    LoadDataGridPesadasOI();
                    ClrStatusManualRegistrationRequest();
                    if(m_datPesadaActiva.Producto.EsInsumo)
                    {
                        if (!RegistrarInsumo())
                        {
                            MessageBox.Show("Se ha producido un error al registrar los Insumos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
        private void RegistrarPesadaGrupal()
        {
            try
            {
                int bultos = m_datPesadaActiva.Bultos;
                bool registrarInsumosOk = true;

                m_datPesadaActiva.IdGrupo = CDb.GetNewIdGrupoPesada(m_datOIActiva.m_id);
                if(!IsManualRegistrationRequest)
                {
                    m_datPesadaActiva.PesoNeto /= m_datPesadaActiva.Bultos;
                    m_datPesadaActiva.PesoTara /= m_datPesadaActiva.Bultos;
                    m_datPesadaActiva.PesoRemitido /= (m_datPesadaActiva.Bultos * m_datPesadaActiva.Unidades);
                }
                do
                {
                    if (CDb.RegistrarPesada(ref m_datPesadaActiva))
                    {
                        CLabel.PrintProduct(m_datPesadaActiva, ConfigApp.CConfigApp.m_ingresoAPlanta_WeightLabelEnable,true, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 1);
                        CargarControlesConAcumuladosProducto();
                        if (m_datPesadaActiva.Producto.EsInsumo)
                        {
                            registrarInsumosOk &= RegistrarInsumo();
                        }
                    }
                } while (--bultos > 0);
                LoadDataGridPesadasOI();
                ClrStatusManualRegistrationRequest();
                ResetBultos();
                if (!registrarInsumosOk)
                {
                    MessageBox.Show("Se ha producido un error al registrar los Insumos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException cdbe)
            {
                MessageBox.Show("Se ha producido un error al intentar registrar la nueva pesada en base de datos.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_registrarSinPesar_Click(object sender, EventArgs e)
        {
            SetStatusManualRegistrationRequest();
            RegistrarPesada();
        }

        private bool RegistrarInsumo()
        {
            bool registradoOK = false;
            registradoOK = CDb.RegistrarMovimientoInsumo(TYPE_INSUMO_MOV.Ingreso, TYPE_INSUMO_PROC.IngresoPlanta, m_datPesadaActiva.Id, m_datPesadaActiva.Producto.Id, m_datPesadaActiva.Unidades);
            return registradoOK;
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


       
        private void button_declararIngresosConPesoEditado_Click(object sender, EventArgs e)
        {
            RegistrarIngresosConPesoEditado();
        }

        private void RegistrarIngresosConPesoEditado()
        {
            if (m_Estado == ESTADOS_APP.OI_ABIERTA)
            {
                if (VerificarControlesParaRegistracionIngresosPorPesoEditado())
                {
                    var producto = ((CProducto)textBox_producto.Tag);
                    if (producto != null)
                    {
                        Form_DeclaraIngresosConPesoEditado dlg = new Form_DeclaraIngresosConPesoEditado(producto.Nombre, producto.EsTropa);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            SetDatosPesadaActiva();
                            try
                            {
                                foreach(IngresoConPesoEditado ingreso in dlg.Ingresos)
                                {
                                    m_datPesadaActiva.Tropa = new CTropa()
                                    {
                                        Numero=ingreso.Tropa,
                                        Tipificacion = ingreso.Tipificacion
                                    };
                                    m_datPesadaActiva.PesoTara = 0.0f;
                                    m_datPesadaActiva.Bultos = 1;
                                    m_datPesadaActiva.PesoNeto = ingreso.PesoNeto;
                                    m_datPesadaActiva.PesoTara = ingreso.PesoTara;
                                    m_datPesadaActiva.Unidades = ingreso.Unidades;
                                    m_datPesadaActiva.Manual = true;
                                    
                                    if (CDb.RegistrarPesada(ref m_datPesadaActiva))
                                    {
                                        CLabel.PrintProduct(m_datPesadaActiva, ConfigApp.CConfigApp.m_ingresoAPlanta_WeightLabelEnable, true, m_activeScaleForm?.ScaleSerialCtrl?.DatScale.CantDecimales ?? 1);
                                        CargarControlesConAcumuladosProducto();
                                        LoadDataGridPesadasOI();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se ha producido un error al intentar registrar la nueva pesada en base de datos.verifique la conexion con la base de datos.", "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                            }
                            catch (CDbException cdbe)
                            {
                                MessageBox.Show("Se ha producido un error al intentar registrar la nueva pesada en base de datos.verifique la conexion con la base de datos. " + cdbe.Message, "REGISTRACION DE PESADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ClrStatusManualRegistrationRequest()
        {
            IsManualRegistrationRequest = false;
        }
        private void SetStatusManualRegistrationRequest()
        {
            IsManualRegistrationRequest = true;
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
                        SetEnableToolStripButtomsOperaciones(false);
                        tabControl_ProcesoPesaje.Visible = false;
                        groupBox_datOI.Enabled = false;
                        groupBox_datOI.Visible = false;
                        scaleSerialCtrl1.Enabled = false;
                        scaleSerialCtrl1.Visible = false;
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
                        SetEnableToolStripButtomsOperaciones(false);
                        tabControl_ProcesoPesaje.Visible = false;
                        groupBox_datOI.Enabled = false;
                        groupBox_datOI.Visible = false;
                        groupBox_datPesaje.Enabled = false;
                        groupBox_datPesaje.Visible = false;
                        groupBox_detallesPesadasOI.Visible = false;
                        scaleSerialCtrl1.Enabled = false;
                        scaleSerialCtrl1.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        button_registrarSinPesar.Enabled = false;
                        button_declararIngresosConPesoEditado.Enabled = false;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "DETENIDO", Color.Red);

                        break;
                    }

                case ESTADOS_APP.INICIADA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoPesaje.Visible = false;
                        groupBox_datOI.Enabled = false;
                        groupBox_datOI.Visible = false;
                        scaleSerialCtrl1.Enabled = false;
                        scaleSerialCtrl1.Visible = true;
                        groupBox_datPesaje.Enabled = false;
                        groupBox_datPesaje.Visible = false;
                        groupBox_detallesPesadasOI.Visible = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_AbrirOI.Enabled = true;
                        toolStripButton_SalvarNuevaOI.Enabled = false;
                        toolStripButton_NuevaOI.Enabled = true;
                        toolStripButton_EliminarOI.Enabled = false;
                        toolStripButton_CerrarOI.Enabled = false;
                        toolStripButton_EditarOI.Enabled = false;
                        button_registrarSinPesar.Enabled = false;
                        button_declararIngresosConPesoEditado.Enabled = false;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "INICIADO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.OI_NUEVA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoPesaje.Visible = true;
                        tabControl_ProcesoPesaje.SelectTab("tabPage_ProcesoPesaje");
                        groupBox_datOI.Enabled = true;
                        groupBox_datOI.Visible = true;
                        scaleSerialCtrl1.Enabled = false;
                        groupBox_datPesaje.Enabled = false;
                        groupBox_datPesaje.Visible = false;
                        groupBox_detallesPesadasOI.Visible = false;
                        toolStripButton_AbrirOI.Enabled = false;
                        toolStripButton_SalvarNuevaOI.Enabled = true;
                        toolStripButton_NuevaOI.Enabled = false;
                        toolStripButton_EliminarOI.Enabled = false;
                        toolStripButton_CerrarOI.Enabled = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_EditarOI.Enabled = false;
                        button_registrarSinPesar.Enabled = false;
                        button_declararIngresosConPesoEditado.Enabled = false;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "CREAR NUEVO INGRESO", Color.Red);
                        break;
                    }
                case ESTADOS_APP.OI_ABIERTA:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        groupBox_datOI.Enabled = false;
                        tabControl_ProcesoPesaje.Visible = true;
                        tabControl_ProcesoPesaje.SelectTab("tabPage_ProcesoPesaje");
                        groupBox_datOI.Visible = true;
                        scaleSerialCtrl1.Enabled = true;
                        groupBox_datPesaje.Enabled = true;
                        groupBox_datPesaje.Visible = true;
                        groupBox_detallesPesadasOI.Visible = true;
                        toolStripButton_AbrirOI.Enabled = true;
                        toolStripButton_SalvarNuevaOI.Enabled = false;
                        toolStripButton_NuevaOI.Enabled = false;
                        toolStripButton_EliminarOI.Enabled = true;
                        toolStripButton_EditarOI.Enabled = true;
                        toolStripButton_CerrarOI.Enabled = true;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        button_registrarSinPesar.Enabled = false;
                        button_declararIngresosConPesoEditado.Enabled = false;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "ORDEN DE INGRESO EN PROCESO DE PESAJE", Color.Red);
                        break;
                    }
                case ESTADOS_APP.OI_EDICION:
                    {
                        toolStripButton_Iniciar.Enabled = false;
                        toolStripButton_Parar.Enabled = true;
                        tabControl_ProcesoPesaje.Visible = true;
                        tabControl_ProcesoPesaje.SelectTab("tabPage_ProcesoPesaje");
                        groupBox_datOI.Enabled = true;
                        groupBox_datOI.Visible = true;
                        scaleSerialCtrl1.Enabled = false;
                        groupBox_datPesaje.Enabled = false;
                        groupBox_datPesaje.Visible = false;
                        groupBox_detallesPesadasOI.Visible = false;
                        toolStripButton_AbrirOI.Enabled = false;
                        toolStripButton_SalvarNuevaOI.Enabled = true;
                        toolStripButton_NuevaOI.Enabled = false;
                        toolStripButton_EliminarOI.Enabled = false;
                        ToolStripMenuItem_Consultas.Enabled = true;
                        toolStripButton_EditarOI.Enabled = false;
                        toolStripButton_CerrarOI.Enabled = false;
                        button_registrarSinPesar.Enabled = false;
                        button_declararIngresosConPesoEditado.Enabled = false;
                        button_imprimirEtiquetaContenedor.Enabled = false;
                        CCommons.SetToolStripStatusLabel(toolStripStatusProcessValue, "EDITAR ORDEN DE INGRESO", Color.Red);
                        break;
                    }
            }
        }
#endregion
        
        #region FUNCIONES GENERALES

        //obtiene una referencia al control toolstrip activo del form
        public ToolStrip GetActiveToolStrip()
        {
            return toolStrip_buttons;
        }

        private void SetEnableToolStripButtomsOperaciones(bool enable)
        {
            toolStripButton_AbrirOI.Enabled = enable;
            toolStripButton_SalvarNuevaOI.Enabled = enable;
            toolStripButton_NuevaOI.Enabled = enable;
            toolStripButton_EliminarOI.Enabled = enable;
            toolStripButton_EditarOI.Enabled = enable;
            toolStripButton_CerrarOI.Enabled = enable;
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
            m_datPesadaActiva.Oi = m_datOIActiva;
            m_datPesadaActiva.Producto = (CProducto)textBox_producto.Tag;
            m_datPesadaActiva.Unidades = GetUndsPredefinidasProductoDlg();
            m_datPesadaActiva.PesoTara = GetPesoTara();
            m_datPesadaActiva.PesoNeto = GetPesoNeto();
            m_datPesadaActiva.PesoRemitido = GetPesoRemitidoDlg();
            m_datPesadaActiva.Bultos = GetBultosPesadaDlg();
            m_datPesadaActiva.Destino= (CDestino)textBox_destino.Tag;
            m_datPesadaActiva.Tropa = GetTropaDlg();
            m_datPesadaActiva.FechaVencimiento = DateTime.Now.AddDays(GetDiasVencimiento());
        }

        private CTropa GetTropaDlg()
        {
            CTropa tropaSelect = new CTropa();
            CProducto selectedProducto = (CProducto)textBox_producto.Tag;
            
            if (selectedProducto != null && selectedProducto.EsTropa)
            {
                tropaSelect.Numero = textBox_tropa.GetValue<int>(0);
                //tropaSelect.Tipificacion = (Tipificacion)textBox_tipificacion.Tag ?? new Tipificacion();
                tropaSelect.Tipificacion = textBox_tipificacion.CastTag<Tipificacion>();
            }
            return tropaSelect;
        }

        private int GetUndsPredefinidasProductoDlg()
        {
            return String.IsNullOrEmpty(textBox_unidadesPredefinidas.Text) ?  0 : Convert.ToInt32(textBox_unidadesPredefinidas.Text);
        }

        private int GetBultosPesadaDlg()
        {
            return String.IsNullOrEmpty(textBox_bultos.Text) ? 0 : Convert.ToInt32(textBox_bultos.Text);
        }

        private float GetPesoRemitidoDlg()
        {
            float pesoRemitidoProducto = textBox_pesoRemitidoTotal.GetValue<float>(0.0f);
            pesoRemitidoProducto /= textBox_unidadesTotalesRemitidas.GetValue<float>(1.0f)==0.0f ? 1.0f : textBox_unidadesTotalesRemitidas.GetValue<float>(1.0f);
            pesoRemitidoProducto *= GetUndsPredefinidasProductoDlg();
            return pesoRemitidoProducto ;
        }

        private void ResetBultos()
        {
            textBox_bultos.Text = "1";
        }

        private float GetPesoNeto()
        {
            float peso = 0.0f;
            bool productoPesable = (textBox_producto.Tag as CProducto).EsPesable;
            if (productoPesable || !IsManualRegistrationRequest)
            {
                peso = m_DatPesaje.PesoNeto - (m_DatPesaje.PesoTara != 0.0f ? 0.0f : Convert.ToSingle(textBox_pesoTaraPredefinida.Text));
            }
            else
            {
                peso = Convert.ToSingle(textBox_pesoNetoPredefinido.Text);
            }
            return peso;
        }
        private float GetPesoTara()
        {
            float peso = 0.0f;
            if (m_datPesadaActiva.Producto.EsPesable || !IsManualRegistrationRequest)
            {
                peso = m_DatPesaje.PesoTara != 0.0f ? m_DatPesaje.PesoTara: Convert.ToSingle(textBox_pesoTaraPredefinida.Text);
            }
            else
            {
                peso = Convert.ToSingle(textBox_pesoTaraPredefinida.Text);
            }
            return peso;
        }

        private int GetDiasVencimiento()
        {
            int diasVcto = Convert.ToInt32(textBox_diasVencimiento.Text);

            return diasVcto;
        }

        private void ClrControlesOI()
        {
            textBox_certificadoSanitario.Text = "";
            label_numOI.Text = "-----";
            label_fechaOI.Text = "-----";
            textBox_proveedor.Text = "";
            textBox_proveedor.Tag = null;
            m_datOIActiva = new COi();
            ClrControlesPesada();
        }
        private void ClrControlesPesada()
        {
            ClrControlesProducto();
            ClrControlesDetallePesadasOI();
            ClrControlesDestino();
            ClrControlesTropa();
            ClrPesoRemitido();
        }
        private void ClrControlesProducto()
        {
            textBox_producto.Text = "";
            textBox_producto.Tag = null;
            tableLayoutPanel_infoProducto.Visible = false;
            button_imprimirEtiquetaContenedor.Enabled = false;
            ClrControlesAcumuladosProducto();
        }
        private void ClrControlesTropa()
        {
            textBox_tropa.Text = "";
            ClrControlesTipificacion();
        }
        private void ClrControlesDestino()
        {
            textBox_destino.Text = "";
            textBox_destino.Tag = null;
        }
        private void ClrControlesTipificacion()
        {
            textBox_tipificacion.Text = "";
            textBox_tipificacion.Tag = null;
        }
        private void ClrControlesAcumuladosProducto()
        {
            textBox_totalPesadasProducto.Text = "0";
            textBox_totalUnidadesProducto.Text = "0";
            textBox_totalBrutoProducto.Text = "0";
            textBox_totalNetoProducto.Text = "0";
        }
        private void RefrescarControlesOI()
        {
            label_numOI.Text = m_datOIActiva.m_id.ToString();
            label_fechaOI.Text = m_datOIActiva.m_fechaHoraCreacion.ToShortDateString();
        }
        private void CargarControlesConOIActiva()
        {
            if (m_datOIActiva != null)
            {
                label_numOI.Text = m_datOIActiva.m_id.ToString();
                label_fechaOI.Text = m_datOIActiva.m_fechaHoraCreacion.ToShortDateString();
                textBox_proveedor.Text = m_datOIActiva.m_proveedor.Nombre;
                textBox_proveedor.Tag = m_datOIActiva.m_proveedor;
                textBox_certificadoSanitario.Text = m_datOIActiva.m_idCertSanitario;

                LoadDataGridPesadasOI();
            }
        }
        private void ClrPesoRemitido()
        {
            textBox_pesoRemitidoTotal.Text = "";
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

                textBox_codigoProducto.Text = datProducto.ProductoSAC.Codigo.ToString().Trim();
                textBox_unidadesPredefinidas.Text = datProducto.UnidadesPredefinidas.ToString();
                textBox_pesoNetoPredefinido.Text = datProducto.PesoNetoPredefinido.ToString();
                textBox_pesoTaraPredefinida.Text = datProducto.PesoTaraPredefinida.ToString();
                textBox_diasVencimiento.Text = datProducto.DiasVencimientoPredefinido.ToString();
                textBox_bultos.Text = "1";
                textBox_unidadesTotalesRemitidas.Text = "1";
                CargarControlesConAcumuladosProducto();
                tableLayoutPanel_infoProducto.Visible = true;
                SetEnableCtrlSecure(button_registrarSinPesar,!datProducto.EsPesable);
                SetEnableCtrlSecure(button_declararIngresosConPesoEditado, datProducto.EsPesable);
                SetEnableCtrlSecure(button_imprimirEtiquetaContenedor,true);
                ClrPesoRemitido();
                SetVisibleCtrlSecure(groupBox_tropa, datProducto.EsTropa);
                ClrControlesTropa();
            }
        }
        private void CargarControlesConDestinoActivo()
        {
            if (textBox_destino.Tag != null)
            {
                textBox_destino.Text = ((CDestino)textBox_destino.Tag).Nombre;
            }
        }
        private void CargarControlesConTipificacionActiva()
        {
            if (textBox_tipificacion.Tag != null)
            {
                textBox_tipificacion.Text = ((Tipificacion)textBox_tipificacion.Tag).Nombre;
            }
        }

        private void CargarControlesConAcumuladosProducto()
        {
            if (textBox_producto.Tag != null)
            {
                CAcumulado acumulado = CDb.GetAcumuladosOIPorProducto(m_datOIActiva.m_id,((CProducto)textBox_producto.Tag).Id);
                SetText_ControlSecure(textBox_totalPesadasProducto, acumulado.m_pesadas.ToString());
                SetText_ControlSecure(textBox_totalUnidadesProducto, acumulado.m_unidades.ToString());
                SetText_ControlSecure(textBox_totalBrutoProducto, acumulado.m_bruto.ToString());
                SetText_ControlSecure(textBox_totalNetoProducto, acumulado.m_neto.ToString());
            }
        }

        private void GetDatosControlesNuevaOI()
        {
            m_datOIActiva.m_fechaHoraCreacion = DateTime.Now;
            m_datOIActiva.m_activo = true;
            m_datOIActiva.m_Operador = CDb.m_OperadorActivo;
            m_datOIActiva.m_idEstacion = CDb.m_OperadorActivo.m_idEstacion;
            m_datOIActiva.m_proveedor = (CProveedorSAC)textBox_proveedor.Tag;
            m_datOIActiva.m_idCertSanitario = textBox_certificadoSanitario.Text;
        }
        private void GetDatosControlesEdicionOI()
        {
            m_datOIActiva.m_proveedor = (CProveedorSAC)textBox_proveedor.Tag;
            m_datOIActiva.m_idCertSanitario = textBox_certificadoSanitario.Text;
        }

        private bool VerificarControlesNuevaOI()
        {
            bool verificacionOk = false;
            if (textBox_certificadoSanitario.Text == "")
            {
                MessageBox.Show("No ha editado el numero de certificado sanitario.", "VALIDACION DE DATOS PARA NUEVA ORDEN DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_proveedor.Text == "")
            {
                MessageBox.Show("No ha editado el Proveedor", "VALIDACION DE DATOS PARA NUEVA ORDEN DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                verificacionOk = true;
            return verificacionOk;
        }

        private bool VerificarControlesNuevaPesada()
        {
            bool verificacionOk = false;
            CProducto selectProducto = textBox_producto.Tag as CProducto;

            if (textBox_producto.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Producto", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_destino.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Destino", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_unidadesPredefinidas.Text == "" || Convert.ToInt32(textBox_unidadesPredefinidas.Text) == 0)
            {
                MessageBox.Show("No ha editado la cantidad de unidades", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CConfigApp.m_pesoUnidadesRemitidoObligatorio && textBox_unidadesTotalesRemitidas.GetValue<int>() == 0)
            {
                MessageBox.Show("No ha editado la cantidad de unidades totales remitidas", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CConfigApp.m_pesoUnidadesRemitidoObligatorio && textBox_unidadesTotalesRemitidas.GetValue<int>() < textBox_unidadesPredefinidas.GetValue<int>())
            {
                MessageBox.Show("La cantidad total de unidades remitidas no puede ser menor a la cantidad de unidades del producto.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (selectProducto.EsPesable && ((m_DatPesaje.PesoNeto <= m_activeScaleForm.MaximoRangoZero) && (textBox_pesoNetoPredefinido.Text == "" || Convert.ToSingle(textBox_pesoNetoPredefinido.Text) == 0.0f)))
            {
                MessageBox.Show("En un Producto pesable debe existir sobre la balanza un peso valido o tener editado o predefinido un valor de peso.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_bultos.Text == "" || Convert.ToInt32(textBox_bultos.Text) == 0)
            {
                MessageBox.Show("No ha editado la cantidad de bultos o la misma se encuentra en cero", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!CPrintEtiZpl2_Win.IsPrinterOnLine(CConfigApp.m_nombreImpresora))
            {
                MessageBox.Show("La impresora no esta ON-LINE , puede ser que se encuentre apagada o en un estado de error.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((!selectProducto.EsInsumo || (selectProducto.EsInsumo && selectProducto.EsPesable))
                            && CConfigApp.m_pesoUnidadesRemitidoObligatorio
                            && (textBox_pesoRemitidoTotal.Text == "" || Convert.ToSingle(textBox_pesoRemitidoTotal.Text) == 0.0f))
            {
                MessageBox.Show("No ha editado el valor de Peso Total Remitido del Producto o la Partida.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((!selectProducto.EsInsumo || (selectProducto.EsInsumo && selectProducto.EsPesable))
                            && !EsPesoValido())
            {
                MessageBox.Show("El Peso Neto del Producto no es Valido. Se encuentra fuera de tolerancia con respecto al Peso Predefinido.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (selectProducto.EsTropa && (String.IsNullOrEmpty(textBox_tropa.Text) || Convert.ToInt32(textBox_tropa.Text) == 0 || (textBox_tipificacion.Tag == null)))
            {
                MessageBox.Show("El producto requiere la edición de un numero de Tropa y la selección de una Tipificacion.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                verificacionOk = true;

            return verificacionOk;
        }
        private bool VerificarControlesParaRegistracionIngresosPorPesoEditado()
        {
            bool verificacionOk = false;
            CProducto selectProducto = textBox_producto.Tag as CProducto;

            if (textBox_producto.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Producto", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox_destino.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Destino", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!selectProducto.EsPesable)
            {
                MessageBox.Show("El producto debe ser del tipo Pesable para poder realizar una declaración de ingresos con pedo Editado.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!CPrintEtiZpl2_Win.IsPrinterOnLine(CConfigApp.m_nombreImpresora))
            {
                MessageBox.Show("La impresora no esta ON-LINE , puede ser que se encuentre apagada o en un estado de error.", "VALIDACION DE DATOS PARA NUEVA PESADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                verificacionOk = true;

            return verificacionOk;
        }

        private bool EsPesoValido()
        {
            float pesoUnitarioBalanza = GetPesoNeto() / (float)(GetBultosPesadaDlg() * GetUndsPredefinidasProductoDlg());
            float pesoNetoPredefinido = textBox_pesoNetoPredefinido.GetValue<float>();
            float desvioPesoPredefinidoPesoBalanza = ((pesoNetoPredefinido - pesoUnitarioBalanza) / pesoNetoPredefinido) * 100;
            return (Math.Abs(desvioPesoPredefinidoPesoBalanza) <= CConfigApp.m_toleranciaPesoPredefPesoBalanza_IngresoAPlanta);
        }

        private bool BorrarOIActiva()
        {
            bool registroBorradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {
                    string aviso = String.Format("Usted esta a punto de Eliminar una Orden de Ingreso Numero: {0} . Se borraran tambien todos los registros de pesaje que posea esta orden de ingreso ! , confirma la eliminacion ", m_datOIActiva.m_id);
                    if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //verifico que ninguna pieza de la OI se encuentre en produccion o en egresos es decir que
                        //todas esten en stock o que no haya pesadas efectuadas.
                        if (CDb.IsAllInStockOI(m_datOIActiva.m_id))
                        {
                            if (CDb.BorrarOI(m_datOIActiva.m_id))
                            {
                                registroBorradoOk = true;
                                MessageBox.Show("La Orden de Ingreso ha sido borrada !!!.", "Confirmación de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                                MessageBox.Show("No se pudo eliminar la Orden de Ingreso.", "Error Borrando Registro en base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("No es posible eliminar una Orden de Ingreso que posea piezas que hayan sido producidas o Egresadas.", "Validación de Borrado de Orden de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return registroBorradoOk;
        }

        private bool CerrarOIActiva()
        {
            bool registroCerradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {
                    string aviso = String.Format("Usted esta a punto de Cerrar una Orden de Ingreso Numero: {0} . Una vez cerrada no podra volverla a abrir para continuar realizando pesajes para esta Orden de Ingreso ! , confirma el cierre ?? ", m_datOIActiva.m_id);
                    if (MessageBox.Show(aviso, "CONFIRMACION DE CERRAR LA ORDEN DE INGRESO",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //verifico que ninguna pieza de la OI se encuentre en produccion o en egresos es decir que
                        //todas esten en stock o que no haya pesadas efectuadas.
                        if (CDb.CerrarOI(m_datOIActiva.m_id))
                        {
                            registroCerradoOk = true;
                            MessageBox.Show("La Orden de Ingreso ha sido cerrada !!!.", "Confirmación de Cierre de Orden de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                            MessageBox.Show("No se pudo cerrar la Orden de Ingreso.", "Error Cerrando la Orden de Ingreso en base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return registroCerradoOk;
        }

        #endregion

        #region DATAGRID PESADAS_OI
        private void ClrControlesDetallePesadasOI()
        {
            dataGridView_detallePesajeEnOI.DataSource = null;
        }

        private void LoadDataGridPesadasOI()
        {
            DataSet dsPesadasOI = new DataSet();
            int idOI = m_datOIActiva.m_id;

            try
            {
                //IDPESADA,GRUPO,IDOI,FECHA_HORA,OPERADOR,DESTINO,CODIGO,PRODUCTO,TROPA,TIPIF,UNDS,NETO,TARA,REMITIDO
                if (CDb.GetDatSet_PesadasOI(idOI, out dsPesadasOI))
                {
                    if (dsPesadasOI.Tables.Contains("PESADAS"))
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePesajeEnOI,dsPesadasOI);
                        SetDataMember_DataGridViewSecure(dataGridView_detallePesajeEnOI,"PESADAS");
                        SetFormatDGVDetallePesajeEnOISecure();

                    }
                    else
                    {
                        SetDataSource_DataGridViewSecure(dataGridView_detallePesajeEnOI, null);
                    }
                }
                else
                {
                    MessageBox.Show("Error en base de datos al intentar cargar la grilla de pesadas registradas en la Orden de Ingreso seleccionada", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetFormatDGVDetallePesajeEnOISecure()
        {
            if (dataGridView_detallePesajeEnOI.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetFormatDGVDetallePesajeEnOISecure(); }));
            }
            else
            {
                dataGridView_detallePesajeEnOI.Columns["IDOI"].Visible = false;

                dataGridView_detallePesajeEnOI.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["IDPESADA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnOI.Columns["IDPESADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["IDPESADA"].HeaderText="PIEZA";

                dataGridView_detallePesajeEnOI.Columns["GRUPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnOI.Columns["GRUPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["FECHA_HORA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePesajeEnOI.Columns["FECHA_HORA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnOI.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnOI.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnOI.Columns["CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnOI.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnOI.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnOI.Columns["TROPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnOI.Columns["TROPA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["TIPIF"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView_detallePesajeEnOI.Columns["TIPIF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_detallePesajeEnOI.Columns["UNDS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnOI.Columns["UNDS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnOI.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["TARA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnOI.Columns["TARA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["REMITIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView_detallePesajeEnOI.Columns["REMITIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_detallePesajeEnOI.Columns["REMITIDO"].DefaultCellStyle.Format = "0.##";
                dataGridView_detallePesajeEnOI.Columns["VENCIMIENTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_detallePesajeEnOI.Columns["VENCIMIENTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void dataGridView_detallePesajeEnOI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_detallePesajeEnOI.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDataGridPesadasOI();
                }
            }
        }

        private bool BorrarRegistrosSeleccionadosDataGridPesadasOI()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_detallePesajeEnOI.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_detallePesajeEnOI.SelectedRows.Count;
                        int countDeletedRegisters = 0;

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de pesaje de la Orden de Ingreso: {1}  , confirma la eliminación ",countSelectRegisters, m_datOIActiva.m_id);
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPesada;
                            foreach (DataGridViewRow dgvr in dataGridView_detallePesajeEnOI.SelectedRows)
                            {
                                idPesada = Convert.ToInt32(dgvr.Cells["IDPESADA"].Value);
                                if (CDb.IPartInStock(idPesada))
                                {
                                    if (CDb.BorrarPieza(idPesada))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPieza, TYPE_CONTEXT_DBLOG.IngresoAPlanta,
                                            string.Format("Se Elimino la Pieza: {0} de la Orden de Ingreso: {1}", idPesada,m_datOIActiva.m_id));
                                        /// por mas que la pesadas sea o no insumo llama al metodo de borrar insumo.
                                        /// si la pesada no es un insumo el metodo no realizara ninguna accion dado que la
                                        /// pesada no existira en la tabla por no pertenecer a un producto de tipo insumo.
                                        CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.IngresoPlanta, idPesada);
                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);

                            if(countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar "+countDeletedRegisters+" registros de "+ countSelectRegisters + " seleccionados , dado que los registros de pesaje que han sido parte de produccion o fueron egresados no podran ser eliminados.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if(countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("Ningun registro fue eliminado , los mismos pertenecen a piezas que han sido parte de produccion o ya fueron egresadas.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            LoadDataGridPesadasOI();
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
