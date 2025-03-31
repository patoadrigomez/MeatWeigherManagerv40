using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Db;
using System.Data.OleDb;
using EditValueTouchDlg;

namespace ProductoInsumosCtrl
{
    /// <summary>
    /// Control que provee la funcionalidad de visualizar una grilla con todos los
    /// insumos que posee vinculados un articulo.
    /// </summary>
    public partial class ProductoInsumosCtrl: UserControl
    {

        private int m_idProducto = 0;

        [Description("Codigo de Producto"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Datos de Configuracion"), DefaultValue(typeof(int), "0")]
        [Browsable(true)]
        public int IdProducto
        {
            get { return m_idProducto; }
            set
            {
                if(value != m_idProducto)
                {
                    m_idProducto = value;
                    CargarDataGrid();
                }
            }
        }

        public ProductoInsumosCtrl()
        {
            InitializeComponent();
        }
        
        public List<CItemInsumoProductoEnProceso> GetInsumos()
        {
            return dataGridView_DetalleDeInsumos.DataSource as List<CItemInsumoProductoEnProceso>;
        }

        public bool esTodosInsumosConfirmados()
        {
            List<CItemInsumoProductoEnProceso> listInsumos = dataGridView_DetalleDeInsumos.DataSource as List<CItemInsumoProductoEnProceso>;
            
            return listInsumos == null ||
                        !listInsumos.Any(x=>x.EsConfirmado==false);
        }

        private void CargarDataGrid()
        {
            try
            {
                ///obtengo todos los insumos de un producto, primarios y secundarios.
                List<CItemInsumoProducto> listFullInsumos = CDb.GetComposicionInsumoProducto(IdProducto).ToList();

                ///creo lista con solo un registro primarios.
                List<CItemInsumoProducto> listInsumosPrimarios = listFullInsumos.Where(x => x.Id==x.IdInsumoPrimario).ToList();
                
                ///Obtiene la lista de los insumos , cada uno de ellos con su lista de insumos alternativos y cantidad. 
                List<CItemInsumoProductoEnProceso> listInsumosProceso = listInsumosPrimarios.ConvertAll(x => new CItemInsumoProductoEnProceso()
                {
                    EsConfirmado = !x.RequiereConfirmacion,
                    IdInsumoSelected = x.Id,
                    IdProductoSelected = IdProducto,
                    InsumosAlternativos = listFullInsumos.Where(y => y.IdInsumoPrimario == x.Id).ToList(),
                    Unidades = x.Unidades
                }).ToList();

                if (listInsumosProceso.Count > 0)
                {
                    if(!dataGridView_DetalleDeInsumos.Columns.Contains("INSUMO"))
                    {
                        DataGridViewComboBoxColumn dgvCmbCol = new DataGridViewComboBoxColumn();
                        dgvCmbCol.HeaderText = "INSUMO";
                        dgvCmbCol.Name = "INSUMO";
                        dgvCmbCol.DisplayMember = "NOMBRE";
                        dgvCmbCol.ValueMember = "Id";

                        dgvCmbCol.Name = "INSUMO";
                        dgvCmbCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        
                        dataGridView_DetalleDeInsumos.Columns.Add(dgvCmbCol);
                    }

                    dataGridView_DetalleDeInsumos.DataSource = listInsumosProceso;
                    //pongo todas las columnas no visibles para luego poner en visible la que me interesa
                    dataGridView_DetalleDeInsumos.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);
                    dataGridView_DetalleDeInsumos.RowHeadersDefaultCellStyle.Font = new Font("Arial", 9);
                    dataGridView_DetalleDeInsumos.DefaultCellStyle.Font = new Font("Arial", 12);
                    dataGridView_DetalleDeInsumos.EditMode = DataGridViewEditMode.EditOnEnter;
                    foreach (DataGridViewRow dgr in dataGridView_DetalleDeInsumos.Rows)
                    {
                        CItemInsumoProductoEnProceso item = ((CItemInsumoProductoEnProceso)dgr.DataBoundItem);
                        DataGridViewComboBoxCell cbcell = new DataGridViewComboBoxCell();
                        cbcell.DisplayMember = "NOMBRE";
                        cbcell.ValueMember = "Id";
                        cbcell.DataSource = item.InsumosAlternativos;
                        cbcell.MaxDropDownItems = 5;
                        dgr.Cells["INSUMO"] = cbcell;
                        int val = item.InsumosAlternativos.Where(y => y.Id == y.IdInsumoPrimario).Select(y => y.Id).FirstOrDefault();

                        dgr.Cells["INSUMO"].Value = val;
                    }

                    dataGridView_DetalleDeInsumos.Columns["ESCONFIRMADO"].Visible = true;
                    dataGridView_DetalleDeInsumos.Columns["ESCONFIRMADO"].HeaderText = "OK";
                    dataGridView_DetalleDeInsumos.Columns["ESCONFIRMADO"].DisplayIndex = 0;
                    dataGridView_DetalleDeInsumos.Columns["ESCONFIRMADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                    dataGridView_DetalleDeInsumos.Columns["INSUMO"].Visible = true;
                    dataGridView_DetalleDeInsumos.Columns["INSUMO"].DisplayIndex = 1;
                    dataGridView_DetalleDeInsumos.Columns["INSUMO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dataGridView_DetalleDeInsumos.Columns["UNIDADES"].Visible = true;
                    dataGridView_DetalleDeInsumos.Columns["UNIDADES"].HeaderText = "UNDS";
                    dataGridView_DetalleDeInsumos.Columns["UNIDADES"].DisplayIndex = 2;
                    dataGridView_DetalleDeInsumos.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_DetalleDeInsumos.Columns["UNIDADES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                    int dgv_width = dataGridView_DetalleDeInsumos.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                    if (dgv_width > this.Width) this.Width = dgv_width + 100;
                }
                else
                {
                    dataGridView_DetalleDeInsumos.DataSource = null;
                    dataGridView_DetalleDeInsumos.Columns.Clear();
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

        private void dataGridView_DetalleDeInsumos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView_DetalleDeInsumos.Columns[((DataGridView)sender).CurrentCell.ColumnIndex].Name == "INSUMO")
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.SelectionChangeCommitted -= ComboBoxCellInsumo_SelectionChange;
                    cb.SelectionChangeCommitted += ComboBoxCellInsumo_SelectionChange;
                }
            }

        }


        private void ComboBoxCellInsumo_SelectionChange(object sender, EventArgs e)
        {
            CItemInsumoProductoEnProceso insumoPrimario = dataGridView_DetalleDeInsumos.CurrentRow.DataBoundItem as CItemInsumoProductoEnProceso;
            CItemInsumoProducto insumoSecundario = ((ComboBox)sender).SelectedItem as CItemInsumoProducto;
            insumoPrimario.IdInsumoSelected = insumoSecundario.Id;
            insumoPrimario.Unidades = insumoSecundario.Unidades;
            dataGridView_DetalleDeInsumos.EndEdit();
            dataGridView_DetalleDeInsumos.Refresh();
        }

        private void dataGridView_DetalleDeInsumos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_DetalleDeInsumos.Columns[((DataGridView)sender).CurrentCell.ColumnIndex].Name == "Unidades")
            {
                float value = Convert.ToSingle(dataGridView_DetalleDeInsumos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                CEditValueTouchDlg dlg = new CEditValueTouchDlg(value.ToString(), "Unidades", "Editar las Unidades del Insumo ", CEditValueTouchDlg.TYPE_VALUE.FLOAT, 5);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    float input;
                    float.TryParse(dlg.VALUE, out input);
                    dataGridView_DetalleDeInsumos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = input;
                }
            }
        }

        private void dataGridView_DetalleDeInsumos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView_DetalleDeInsumos.Columns[((DataGridView)sender).CurrentCell.ColumnIndex].Name == "Unidades")
            {
                float output;

                if (!float.TryParse(e.FormattedValue.ToString(), out output) || output==0.0f)
                {
                    MessageBox.Show("Por favor edite un valor numerico mayor a cero.","Validar edición de Datos para Insumos",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    e.Cancel = true;
                }

                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    MessageBox.Show("Debe editar un valor.", "Validar edición de Datos para Insumos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }
    }
}
