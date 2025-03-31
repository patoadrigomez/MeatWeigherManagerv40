using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigApp;
using Db;
using Commons;
using System.Threading.Tasks;
using System.Threading;

namespace MeatWeigherManager
{
    public partial class MainForm : Form
    {
        public EventWaitHandle closeChild = new EventWaitHandle(true, EventResetMode.AutoReset);
        public bool EnableActionsClosing { get; private set; } = true;

        #region INICIALIZACION
        public MainForm()
        {
            InitializeComponent();
            InitializeSystem();

        }
        private void InitializeSystem()
        {
            CConfigApp.Importar();
            ConnectDb();
            LogingOperator();
        }


        private void ConnectDb()
        {
            try
            {
                CDb.Open(CConfigApp.m_servidorDB, CConfigApp.m_nombreDB, CConfigApp.m_userDB, CConfigApp.m_passwordDB, CConfigApp.m_tipoSeguridadConexionDB_SSPI ? CDb.TypeSecurity.SSPI : CDb.TypeSecurity.SQL);
                CCommons.SetToolStripStatusLabel(toolStripStatus_Db, "CONECTADA", Color.Green);
            }
            catch(CDbException dbe)
            {
                MessageBox.Show(dbe.Message, "Error en Conexión con la Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CCommons.SetToolStripStatusLabel(toolStripStatus_Db, "DESCONECTADA", Color.Red);
            }
        }

        private void LogingOperator()
        {
            bool validatedOk = false;
            if (CDb.isOpen)
            {
                DialogResult result;
                while (!(validatedOk =ValidarIngresoOperador()))
                {
                    result = MessageBox.Show("Operador no Valido, consulte con el Administrador del Sistema que Usuario y Password posee usted asignado para ingresar a la aplicacion.", "CONTROL DE ACCESO", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                    {
                        Close();
                        break;
                    }
                }
                if (validatedOk)
                {
                    CCommons.SetToolStripStatusLabel(toolStripStatus_Operador, CDb.m_OperadorActivo.m_nombre, Color.Black);
                }
            }
            else
            {
                CCommons.SetToolStripStatusLabel(toolStripStatus_Operador, "PERMITIDO PARA CONFIGURACION", Color.Red);
            }
            CCommons.SetToolStripStatusLabel(toolStripStatus_Estacion, CConfigApp.m_idEstacion.ToString(), Color.Black);
        }

        private bool ValidarIngresoOperador()
        {
            bool ingresoOk = false;
            CLogDlg dlg = new CLogDlg();
            dlg.ShowDialog();
            ingresoOk = CDb.validarOperador(dlg.m_nombre, dlg.m_password, ConfigApp.CConfigApp.m_idEstacion);
            if (ingresoOk)
            {
                configuracionSistemaToolStripMenuItem.Enabled = (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR);
                inventarioToolStripMenuItem.Enabled = (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR);
                colecciónInventarioToolStripMenuItem.Enabled=(CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR);
                backupToolStripMenuItem.Enabled = (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR);
                ajusteDeStockDeInsumosToolStripMenuItem.Enabled = (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR);
            }
            return ingresoOk;
        }
        #endregion

        #region EVENTOS DEL FORM
        private void FormChild_Closed(object sender, FormClosedEventArgs e)
        {
            //señaliza al objeto para notificar que el form hijo realmente se cerro.
            closeChild.Set();
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //todo esto es para que no se cierre el forma padre cuando el hijo aun esta
            //en proceso de cierre cuando esta cerrando los puertos com.
            if (EnableActionsClosing == true)
            {
                e.Cancel = true;
                await WaithClosedChild();
                EnableActionsClosing = false;
                Close();
            }
        }

        #endregion

        #region METODOS GENERALES

        private void CloseAllChildsForm()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            Text = "MeatWeigherManager";
            ToolStripManager.RevertMerge(toolStrip);
        }

        private void CloseChildForm(Form childForm)
        {
            if (childForm != null)
            {
                childForm.Close();
                Text = "MeatWeigherManager";
                ToolStripManager.RevertMerge(toolStrip);
            }
        }

        private ToolStrip GetToolStripActiveFormChild(Form formChild)
        {
            ToolStrip ts = formChild.Controls.OfType<ToolStrip>().FirstOrDefault();
            return ts;
        }

        public async Task WaithClosedChild()
        {
            await Task.Run(() =>
            {
                closeChild.WaitOne();
            });
        }

        public void LoadChild(Form formChild,string nameForm)
        {
            formChild.MdiParent = this;
            formChild.FormClosed += FormChild_Closed;
            ToolStrip toolStripChild = GetToolStripActiveFormChild(formChild);
            if (toolStripChild != null)
                ToolStripManager.Merge(toolStripChild, toolStrip);
            Text = "MeatWeigherManager" + " - " + nameForm;
            formChild.Show();
        }

        #endregion

        #region MENU Y BOTONES DEL TOOLSTRIP
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CConfigAppDlg dlgConfig = new CConfigAppDlg();
            dlgConfig.ShowDialog();
        }

        private void modoDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CConfigWorkMode dlgConfig = new CConfigWorkMode();
            dlgConfig.ShowDialog();
        }
        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CDb.isOpen)
            {
                CInventarioDlg dlg = new CInventarioDlg();
                dlg.ShowDialog();
            }
        }

        private void crearBackupDBToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void restaurarBackupDBToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void IngresoAPlantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_IngresoAPlanta))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_IngresoAPlanta(), "Ingreso A Planta");
            }
        }
        private async void IngresoAProducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_IngresoAProduccion))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_IngresoAProduccion(), "Ingreso A Producción");
            }
        }

        private async void PesajeEnProducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_PesajeEnProduccion))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_PesajeEnProduccion(), "Pesajes en Producción");
            }
        }
        private async void pesajeDeCajasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_PesajeCajas))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_PesajeCajas(), "Pesajes de Cajas");
            }
        }
        private async void preparaciónDeCombosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_PreparacionCombos))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_PreparacionCombos(), "Preparación de Combos");
            }
        }

        private async void toolStripButton_fraccionamiento_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_Fraccionar))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_Fraccionar(), "Proceso de Fraccionamientos");
            }
        }

        private async void EgresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_EgresoDlg))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_EgresoDlg(), "Egresos");
            }
        }

        private async void toolStripButton_Devoluciones_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_Devoluciones))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_Devoluciones(), "Devoluciones");
            }
        }
        private async void  colecciónInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_Inventario))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_Inventario(), "Colección para Inventario");
            }
        }
        private async void toolStripButton_transferenciasDeDeposito_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_TransferenciaDeposito))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_TransferenciaDeposito(), "Transferencias de Depósito");
            }
        }


        private void toolStripButton_cambioOperador_Click(object sender, EventArgs e)
        {
            LogingOperator();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox abtDlg = new AboutBox();
            abtDlg.Show();
        }

        private async void transferenciasDeDepósitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null || ActiveMdiChild.GetType() != typeof(Form_TransferenciaDeposito))
            {
                CloseChildForm(ActiveMdiChild);
                await WaithClosedChild();
                LoadChild(new Form_TransferenciaDeposito(), "Transferencias de Depósito");
            }
        }

        private void ToolStripMenuItem_TablaTiposProductos_Click(object sender, EventArgs e)
        {
            CABM_TiposProductosDlg abmDlg = new CABM_TiposProductosDlg(CDb.m_oleDbConnection);
            abmDlg.ShowDialog();
        }

        private void destinosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CABM_DestinosDlg abmDlg = new CABM_DestinosDlg(CDb.m_oleDbConnection);
            abmDlg.ShowDialog();
        }
        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CABM_EtiquetasDlg abmDlg = new CABM_EtiquetasDlg(CDb.m_oleDbConnection);
            abmDlg.ShowDialog();
        }
        private void tipificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CABM_TipificacionesDlg abmDlg = new CABM_TipificacionesDlg(CDb.m_oleDbConnection);
            abmDlg.ShowDialog();
        }

        private void sectoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CABM_SectoresDlg abmDlg = new CABM_SectoresDlg(CDb.m_oleDbConnection);
            abmDlg.ShowDialog();
        }
        private void ingresosAPlantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaOIPesadasDlg dlg = new CViewConsultaOIPesadasDlg();
            dlg.ShowDialog();
        }

        private void ingresosAProducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaLoteIngPiezasProduccionDlg dlg = new CViewConsultaLoteIngPiezasProduccionDlg();
            dlg.ShowDialog();
        }

        private void pesajesEnProducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaLotePesadasDlg dlg = new CViewConsultaLotePesadasDlg();
            dlg.ShowDialog();
        }

        private void pesajesDeCajasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaCajasPesadasDlg dlg = new CViewConsultaCajasPesadasDlg();
            dlg.ShowDialog();
        }

        private void preparaciónDeCombosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CViewConsultaCombosRegistradosDlg dlg = new CViewConsultaCombosRegistradosDlg();
            dlg.ShowDialog();
        }

        private void egresosPorPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaEgresosDlg dlg = new CViewConsultaEgresosDlg();
            dlg.ShowDialog();
        }
        
        private void historicoDePiezaOContenedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewHistoryPartDlg dlg = new CViewHistoryPartDlg();
            dlg.ShowDialog();
        }
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CBackupDlg dlg = new CBackupDlg();
            dlg.ShowDialog();
        }
        private void ajusteDeStockParaInsumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void ajusteDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CDb.isOpen)
            {
                CAjusteStockInsumosDlg dlg = new CAjusteStockInsumosDlg();
                dlg.ShowDialog();
            }
        }
        private void parametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CDb.isOpen)
            {
                CParametrosDlg dlg = new CParametrosDlg();
                dlg.ShowDialog();
            }
        }
        private void transferenciasEntreDepósitosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CViewConsultaTransferenciasDepositoDlg dlg = new CViewConsultaTransferenciasDepositoDlg();
            dlg.ShowDialog();
        }

        #endregion

    }
}
