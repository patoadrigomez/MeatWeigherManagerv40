using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Db;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CAjusteStockInsumosDlg : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;
        int idPrdInsumoSelected = 0;

        DataTable dtInsumos =null;

        public CAjusteStockInsumosDlg()
        {
            InitializeComponent();
        }

        private void CABM_CAjusteStockInsumosDlg_Load(object sender, EventArgs e)
        {
            CargarDataGrid();
        }

        private void CargarDataGrid()
        {
            try
            {
                dtInsumos = CDb.GetUnidadesEnStockInsumos();

                if (dtInsumos != null && dtInsumos.Rows.Count >0)
                {
                    dataGridView_DetalleExistenciaStockInsumos.DataSource = dtInsumos;
                    dataGridView_DetalleExistenciaStockInsumos.Columns["ID"].Visible = false;
                    dataGridView_DetalleExistenciaStockInsumos.Columns["INSUMO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView_DetalleExistenciaStockInsumos.Columns["UNDS"].DefaultCellStyle.Format = "0.0";

                    int dgv_width = dataGridView_DetalleExistenciaStockInsumos.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                    if (dgv_width > this.Width) this.Width = dgv_width + 100;
                }
                else
                    dataGridView_DetalleExistenciaStockInsumos.DataSource = null;

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

        private void ClrControlesInsumo()
        {
            textBox_unidadesExistencia.Text = "";
            textBox_unidadesAjustar.Text = "";
            textBox_insumo.Text = "";
        }

        private bool ValidarParaActualizar()
        {
            bool validadoOk = false;
            if (textBox_insumo.Text == "")
            {
                MessageBox.Show("No ha seleccionado un Insumo", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox_unidadesAjustar.Text == "" || !Commons.CCommons.CheckIfTextBoxFloat(textBox_unidadesAjustar))
            {
                MessageBox.Show("No ha editado un valor para el ajuste o el mismo no es una valor numerico valido.", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                validadoOk = true;

            return validadoOk;
        }

        private void button_Actualizar_Click(object sender, EventArgs e)
        {
            if (ValidarParaActualizar())
            {
                if (ActualizarRegistro())
                {
                    int idxSelectRow = dataGridView_DetalleExistenciaStockInsumos.SelectedRows[0].Index;
                    CargarDataGrid();
                    SelectDataGridView_Registro(idxSelectRow);
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_DetalleExistenciaStockInsumos.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_DetalleExistenciaStockInsumos.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_DetalleExistenciaStockInsumos.SelectedRows.Count > 0)
            {
                //mover cursor.
                dataGridView_DetalleExistenciaStockInsumos.CurrentCell = dataGridView_DetalleExistenciaStockInsumos[1, idxRow];
                //seleccionar registro
                dataGridView_DetalleExistenciaStockInsumos.Rows[idxRow].Selected = true;
            }
        }

        private bool ActualizarRegistro()
        {

            bool actualizadoOk = false;
            try
            {
                float unidadesAjustar =  Convert.ToSingle(textBox_unidadesAjustar.Text);
                actualizadoOk= CDb.AjustarStockInsumo(idPrdInsumoSelected, unidadesAjustar);
                CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.AjusteManualDeStockDeInsumo, TYPE_CONTEXT_DBLOG.AjusteStockInsumos,
                                        string.Format("Se Ajustó el stock del insumo: {0} a la cantidad de unidades: {1}", textBox_insumo.Text,unidadesAjustar));
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error en Base de Datos al Intentar Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return actualizadoOk;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            LoadControlsForRowSelect();
        }

        private void LoadControlsForRowSelect()
        {
            try
            {
                if (dataGridView_DetalleExistenciaStockInsumos.SelectedRows.Count > 0)
                {
                    idPrdInsumoSelected = CDb.GetCellDGVInt(dataGridView_DetalleExistenciaStockInsumos.SelectedRows[0],"ID");
                    textBox_unidadesExistencia.Text = CDb.GetCellDGVFloat(dataGridView_DetalleExistenciaStockInsumos.SelectedRows[0], "UNDS").ToString();
                    textBox_unidadesAjustar.Text = textBox_unidadesExistencia.Text;
                    textBox_insumo.Text = CDb.GetCellDGVString(dataGridView_DetalleExistenciaStockInsumos.SelectedRows[0], "INSUMO");
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_unidadesAjustar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Peso", "Editar nuevo valor de Unidades en Stock", CEditValueTouchDlg.TYPE_VALUE.FLOAT, ((TextBox)sender).MaxLength);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = dlg.VALUE;
            }
        }

        private void textBox_unidadesAjustar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9' ) || e.KeyChar == '-' || e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void textBox_filtroDataGrid_TextChanged(object sender, EventArgs e)
        {
            if(dtInsumos != null && dtInsumos.Rows.Count>0)
            {
                dtInsumos.DefaultView.RowFilter= string.Format("insumo LIKE '%{0}%'", textBox_filtroDataGrid.Text);
            }
        }
    }
}
