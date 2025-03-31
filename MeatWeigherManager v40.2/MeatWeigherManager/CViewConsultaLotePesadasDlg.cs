using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Db;
using System.Data.OleDb;
using Logger;
using System.Reflection;

namespace MeatWeigherManager
{
    public partial class CViewConsultaLotePesadasDlg : Form
    {

        public CViewConsultaLotePesadasDlg()
        {
            InitializeComponent();
            SetDGVDoubleBuffer(dataGridView_LOTE);
            SetDGVDoubleBuffer(dataGridView_PESADAS);
        }
        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }

        private void LoadDataGrid_Operaciones()
        {
            try
            {
                DataSet ds;
                if (CDb.GetConsultaCompletaLotes(out ds))
                {
                    if (ds.Tables["LOTES"].Rows.Count > 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_LOTE.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_LOTE.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_LOTE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_LOTE.DataSource = ds;
                        dataGridView_LOTE.DataMember = "LOTES";

                        if (ds.Relations.Contains("LOTES_PESADA"))
                        {
                            dataGridView_PESADAS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                            dataGridView_PESADAS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            dataGridView_PESADAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                            dataGridView_PESADAS.DataSource = ds;
                            dataGridView_PESADAS.DataMember = "LOTES.LOTES_PESADA";
                        }
                        else
                        {
                            dataGridView_PESADAS.DataSource = null;
                            dataGridView_PESADAS.DataMember = null;
                        }

                        dataGridView_LOTE.Columns["LOTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_LOTE.Columns["LOTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_LOTE.Columns["IDLOTE"].Visible = false;
                        /*
                       HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                       */
                        dataGridView_LOTE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        dataGridView_PESADAS.Columns["IDLOTE"].Visible = false;
                        dataGridView_PESADAS.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["TIPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["NRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["NRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["EST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["EST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["FECHA_HORA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["FECHA_HORA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PESADAS.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PESADAS.Columns["COD_PROD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["COD_PROD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["UNDS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        dataGridView_PESADAS.Columns["UNDS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["NETO"].DefaultCellStyle.Format = "0.00";
                        dataGridView_PESADAS.Columns["TARA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["TARA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PESADAS.Columns["TARA"].DefaultCellStyle.Format = "0.00";
                        dataGridView_PESADAS.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PESADAS.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_PESADAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    }
                    else
                    {
                        MessageBox.Show("NO HAY REGISTROS A MOSTRAR", "CONSULTA DE PESADAS EN LOTE DE PRODUCCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    dataGridView_LOTE.DataSource = null;
                    dataGridView_LOTE.DataMember = null;
                    dataGridView_PESADAS.DataSource = null;
                    dataGridView_PESADAS.DataMember = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla de Pesadas en Lote de Producción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CViewConsultaLotePesadasDlg_Load(object sender, EventArgs e)
        {
            LoadDataGrid_Operaciones();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void reimprimirEtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            List<CContenedor> listContenedores;
            List<CPesada> listPesadas;

            if (menuContextItem != null)
            {
                listPesadas = CDb.GetWeightingFromSelectedDGVPesadasTipoPiezas(dataGridView_PESADAS,"NRO");

                if (listPesadas != null && listPesadas.Count > 0)
                {
                    foreach (CPesada pesada in listPesadas)
                    {
                        CLabel.PrintProduct(pesada, ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, 2);
                    }
                }

                listContenedores = CDb.GetWeightingFromSelectedDGVPesadasTipoContenedores(dataGridView_PESADAS, "NRO","TIPO","CAJA");
                if (listContenedores != null && listContenedores.Count > 0)
                {
                    foreach (CContenedor caja in listContenedores)
                    {
                        CLabel.PrintCaja(caja,2);
                    }
                }

                listContenedores = CDb.GetCombosFromSelectedDGVCombos(dataGridView_PESADAS, "NRO","TIPO","COMBO");
                if (listContenedores != null && listContenedores.Count > 0)
                {
                    foreach (CContenedor combo in listContenedores)
                    {
                        CLabel.PrintCombo(combo);
                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BorrarRegistrosSeleccionadosDataGridPesadasOI();
        }
        private bool BorrarRegistrosSeleccionadosDataGridPesadasOI()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_PESADAS.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_PESADAS.SelectedRows.Count;
                        int countDeletedRegisters = 0;
                        string lote = dataGridView_LOTE.SelectedRows[0].Cells["LOTE"].Value.ToString();

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de pesaje del lote: {1}  , confirma la eliminación ", countSelectRegisters, lote);
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPesada;
                            string Tipo;
                            foreach (DataGridViewRow dgvr in dataGridView_PESADAS.SelectedRows)
                            {
                                idPesada = Convert.ToInt32(dgvr.Cells["NRO"].Value);
                                Tipo = dgvr.Cells["TIPO"].Value.ToString();
                                if (Tipo == "PIEZA")
                                {
                                    if (!CDb.IsPartInEgresos(idPesada))
                                    {
                                        if (CDb.BorrarPieza(idPesada))
                                        {
                                            CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPieza,
                                                                        TYPE_CONTEXT_DBLOG.PesajeEnProduccion,
                                                                        String.Format("Se Eliminó la Pieza: {0} perteneciente al Lote: {1}", idPesada, lote));

                                            /// borra los insumos que pudo haber registrado la pesada.
                                            CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.PesajePieza, idPesada);
                                            countDeletedRegisters++;
                                        }

                                    }
                                }
                                else if(Tipo == "CAJA" || Tipo == "COMBO")
                                {
                                    if (CDb.IsValidDisarmContainer(idPesada))
                                    {
                                        if (CDb.DesarmarContenedor(idPesada))
                                        {
                                            CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.DesarmadoDeContenedor,
                                                                        TYPE_CONTEXT_DBLOG.PesajeEnProduccion,
                                                                        String.Format("Se Desarmó el Contenedor: {0} perteneciente al Lote: {1}", idPesada,lote));

                                            /// borra los insumos que pudo haber registrado el conformado del contenedor.
                                            CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.ConformadoContenedor, idPesada);
                                            countDeletedRegisters++;
                                        }
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);


                            if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar " + countDeletedRegisters + " registros de " + countSelectRegisters + " seleccionados , dado que los registros de piezas que ya fueron egresadas no podran ser eliminados.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("No se han eliminado registros de pesaje dado que los mismos pertenecen a piezas ya egresadas.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            LoadDataGrid_Operaciones();
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
    }
}
