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
using EditStringTouchDlg;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CABM_DeclaracionInsumosPedidoDlg : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;
        private CPedido m_pedido;


        public CABM_DeclaracionInsumosPedidoDlg(CPedido pedido)
        {
            InitializeComponent();
            m_pedido = pedido;
        }

        private void LoadComboInsumos(string filterNameAprox = "")
        {
            var list = CDb.GetProductos().Where(s => s.EsInsumo == true && s.Nombre.ToUpper().Contains(filterNameAprox.ToUpper())).Select(s => s).ToList();
            if (list != null)
            {
                comboBox_Insumos.DataSource = list;
                comboBox_Insumos.DisplayMember = "NOMBRE";
                comboBox_Insumos.ValueMember = null;
            }
        }

        private void CABM_ProductoInsumosManager_Load(object sender, EventArgs e)
        {
            LoadCtrlsPedido();
            LoadComboInsumos();
            CargarDataGridInsumos();
        }


        private void LoadCtrlsPedido()
        {
            label_nroPedido.Text = m_pedido.Id.ToString();
            label_comprobante.Text = m_pedido.ComprobantePedidoSAC;
            label_cliente.Text = m_pedido.Cliente.Nombre;
        }
        private void CargarDataGridInsumos()
        {
            try
            {
                List<CItemInsumoPedido> listPrd = CDb.GetInsumosPedido(m_pedido.Id);

                if (listPrd.Count > 0)
                {
                    dataGridView_DetalleInsumos.DataSource = listPrd;

                    dataGridView_DetalleInsumos.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);

                    dataGridView_DetalleInsumos.Columns["NOMBRE"].Visible = true;
                    dataGridView_DetalleInsumos.Columns["NOMBRE"].HeaderText = "PRODUCTO";
                    dataGridView_DetalleInsumos.Columns["NOMBRE"].DisplayIndex = 2;
                    dataGridView_DetalleInsumos.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dataGridView_DetalleInsumos.Columns["UNIDADES"].Visible = true;
                    dataGridView_DetalleInsumos.Columns["UNIDADES"].HeaderText = "UNIDADES";
                    dataGridView_DetalleInsumos.Columns["UNIDADES"].DisplayIndex = 3;
                    dataGridView_DetalleInsumos.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    int dgv_width = dataGridView_DetalleInsumos.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                    if (dgv_width > this.Width) this.Width = dgv_width + 100;
                }
                else
                {
                    dataGridView_DetalleInsumos.DataSource = null;
                    dataGridView_DetalleInsumos.Columns.Clear();
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


        /// <summary>
        /// Elimina un insumo de la tabla de movimientos de insumos , Dado su pedido y codigo de insumo. 
        /// </summary>
        private bool BorrarRegistroInsumo(int idPedido,int idInsumo)
        {
            bool borrado = false;
            int regAfectados;
 
            string strCmd = String.Format(" DELETE MOVINSUMOS WHERE idTipoMov = 'EGR' and idTipoProc = 'PED' and idProc = {0} and idPrdInsumo ={1}",idPedido,idInsumo);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, CDb.m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                borrado = (regAfectados >= 1);
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return borrado;
        }

        private bool BorrarRegistroInsumoSeleccionado()
        {
            bool registroBorradoOk = false;
            try
            {
                if (dataGridView_DetalleInsumos.SelectedRows.Count > 0)
                {
                    string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de Insumos Primarios , confirma la eliminación ?", dataGridView_DetalleInsumos.SelectedRows.Count);
                    if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dgvr in dataGridView_DetalleInsumos.SelectedRows)
                        {
                            CItemInsumoPedido prditem = dgvr.DataBoundItem as CItemInsumoPedido;
                            if (!BorrarRegistroInsumo(prditem.IdPedido,prditem.Id))
                            {
                                MessageBox.Show("No se pudieron eliminar los registros desde la base de datos.", "Error Borrando Registro en base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return registroBorradoOk;
                            }
                        }
                        registroBorradoOk = true;
                    }
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando un Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registroBorradoOk;
        }

        private void ClrControlesInsumo()
        {
            textBox_unidades.Text = "";
            comboBox_Insumos.SelectedIndex = -1;
            textBox_searchInsumo.Text = "";
        }

        private void dataGridView_DetalleInsumos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = !BorrarRegistroInsumoSeleccionado();
        }

        private bool ValidarParaAgregar()
        {
            bool validadoOk = false;
            if (comboBox_Insumos.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado el insumo a registrar para el Pedido", "Validación de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox_unidades.Text == "" || Convert.ToSingle(textBox_unidades.Text) == 0.0f)
            {
                MessageBox.Show("No ha editado la cantidad de unidades del insumo primario.", "Validación de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (EsInsumoIncluidoEnPedido())
            {
                MessageBox.Show("El Insumo ya se encuentra incluido en el Pedido.", "Validación de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                validadoOk = true;
           
            return validadoOk;
        }

        private bool ValidarParaActualizar()
        {
            bool validadoOk = false;
            if (comboBox_Insumos.SelectedIndex == -1)
            {
                MessageBox.Show("No tiene un Insumo seleccionado", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox_unidades.Text == "" || Convert.ToSingle(textBox_unidades.Text) == 0.0f)
            {
                MessageBox.Show("No ha editado la cantidad de unidades del insumo.", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!EsInsumoIncluidoEnPedido())
            {
                MessageBox.Show("El Insumo que instenta actualizar no se encuentra dentro del Pedido.", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                validadoOk = true;

            return validadoOk;
        }

        private bool EsInsumoIncluidoEnPedido()
        {
            bool incluidoOK = false;
            if (dataGridView_DetalleInsumos.Rows.Count > 0)
            {
                int idInsumoPrimario = (comboBox_Insumos.SelectedItem as CProducto).Id;
                List<CItemInsumoPedido> listInsumos = dataGridView_DetalleInsumos.DataSource as List<CItemInsumoPedido>;
                incluidoOK = listInsumos.Any(x => x.Id == idInsumoPrimario);
            }
            return incluidoOK;
        }

        private void Button_Agregar_Click(object sender, EventArgs e)
        {
            if(ValidarParaAgregar())
            {
                int countRowsBefore = GetCountRowsDataGridView(dataGridView_DetalleInsumos);
                int idxSelectRowDGV = GetIdxSelectRegisterDataGridView(dataGridView_DetalleInsumos);

                InsertNuevoRegistro();
                CargarDataGridInsumos();

                if(GetCountRowsDataGridView(dataGridView_DetalleInsumos) > countRowsBefore)
                    SelectDataGridView_UltimoRegistro(dataGridView_DetalleInsumos);
                else
                    SelectDataGridView_Registro(dataGridView_DetalleInsumos, idxSelectRowDGV);
            }
        }

        private void button_Actualizar_Click(object sender, EventArgs e)
        {
            if (ValidarParaActualizar())
            {
                if (ActualizarRegistro())
                {
                    int idxSelectRowDGV = GetIdxSelectRegisterDataGridView(dataGridView_DetalleInsumos);

                    CargarDataGridInsumos();

                    SelectDataGridView_Registro(dataGridView_DetalleInsumos,idxSelectRowDGV);
                }
            }
        }

        private int GetCountRowsDataGridView(DataGridView dgv)
        {
            int count = 0;
            if (dgv != null)
            {
                count = dgv.Rows.Count;
            }
            return count;
        }
        private void SelectDataGridView_UltimoRegistro(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dgv,dgv.Rows.Count - 1);
            }
        }
        private int GetIdxSelectRegisterDataGridView(DataGridView dgv)
        {
            int idx = 0;

            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                idx = dgv.SelectedRows[0].Index;
            }
            return idx;
        }

        private void SelectDataGridView_Registro(DataGridView dgv ,int idxRow)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                dgv.CurrentCell = dgv[0, idxRow];
            }
        }

        private bool InsertNuevoRegistro()
        {
            bool registracionOk = false;

            try
            {
                int idPrdInsumo = ((CProducto)comboBox_Insumos.SelectedItem).Id;
                float unidades = Convert.ToSingle(textBox_unidades.Text.Replace(".", ","));

                registracionOk = CDb.RegistrarMovimientoInsumo(TYPE_INSUMO_MOV.Egreso,TYPE_INSUMO_PROC.PreparacionPedido,m_pedido.Id,idPrdInsumo,unidades);
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Insertando nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registracionOk;
        }

        private bool ActualizarRegistro()
        {

            bool actualizadoOk = false;

            try
            {
                int idPrdInsumo = ((CProducto)comboBox_Insumos.SelectedItem).Id;
                float unidades = Convert.ToSingle(textBox_unidades.Text.Replace(".", ","));

                actualizadoOk = CDb.ActualizarUnidadesMovimientoInsumo(TYPE_INSUMO_MOV.Egreso, TYPE_INSUMO_PROC.PreparacionPedido, m_pedido.Id, idPrdInsumo, unidades);

            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Actualizando nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return actualizadoOk;
        }


        private void dataGridView_DetalleInsumosPrimariosManager_SelectionChanged(object sender, EventArgs e)
        {
            LoadControlsInsumosForRowSelect();
        }


        private void LoadControlsInsumosForRowSelect()
        {
            try
            {
                if (dataGridView_DetalleInsumos.SelectedRows.Count > 0)
                {
                    int idProductoInsumo = (dataGridView_DetalleInsumos.SelectedRows[0].DataBoundItem as CItemInsumoPedido).Id;
                    comboBox_Insumos.SelectedItem = comboBox_Insumos.Items.Cast<CProducto>().Where(x => x.Id == idProductoInsumo).FirstOrDefault();
                    textBox_unidades.Text = (dataGridView_DetalleInsumos.SelectedRows[0].DataBoundItem as CItemInsumoPedido).Unidades.ToString();
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectDataGridViewRowInsumo(int idPrdInsumo)
        {
            if (dataGridView_DetalleInsumos.Rows.Count > 0)
            {
                dataGridView_DetalleInsumos.ClearSelection();
                foreach(DataGridViewRow dgrv in dataGridView_DetalleInsumos.Rows)
                {
                    if(((CItemInsumoPedido)dgrv.DataBoundItem).Id == idPrdInsumo)
                    {
                        dgrv.Selected = true;
                        break;
                    }
                }
            }
        }

        private void textBox_unidades_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Unidades", "Editar las Unidades del Insumo ", CEditValueTouchDlg.TYPE_VALUE.FLOAT, ((TextBox)sender).MaxLength);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = dlg.VALUE;
            }
        }

        private void textBox_unidades_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9' )|| e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void comboBox_Insumos_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_unidades.Text = "";
            
            if(comboBox_Insumos.SelectedItem != null)
                SelectDataGridViewRowInsumo(((CProducto)comboBox_Insumos.SelectedItem).Id);
        }

        private void dataGridView_DetalleInsumos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BorrarRegistroInsumoSeleccionado();
                CargarDataGridInsumos();
            }
        }

        private void dataGridView_DetalleInsumosPrimarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadControlsInsumosForRowSelect();
        }

        private void textBox_searchNombreAprox_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Texto de Aproximación ", "Texto", ((TextBox)sender).Text, ((TextBox)sender).MaxLength);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void textBox_searchInsumo_TextChanged(object sender, EventArgs e)
        {
            LoadComboInsumos(textBox_searchInsumo.Text);
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            BorrarRegistroInsumoSeleccionado();
            CargarDataGridInsumos();
        }
    }
}
