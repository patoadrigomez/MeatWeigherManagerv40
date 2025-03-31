using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigApp;
using Db;
using EditStringTouchDlg;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CSelProductoSACDlg : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;
        DataTable dtProductos;
        string m_codSelected = "";
        string m_codSelection = "";
        CProductoSAC m_ProductoSeleted;

        public string CodSelected
        {
            get { return m_codSelected;}
            set { m_codSelected = value;}
        }

        public CProductoSAC ProductoSelected
        {
            get { return m_ProductoSeleted; }
            set { m_ProductoSeleted = value; }
        }

        public int TrackBarValue
        {
            get { return trackBar_dgvProductos.Maximum - trackBar_dgvProductos.Value + trackBar_dgvProductos.Minimum; }
            set { trackBar_dgvProductos.Value =  trackBar_dgvProductos.Maximum - value + trackBar_dgvProductos.Minimum; }
        }

        /// <summary>
        /// Clase de Visualizacion y seleccion de un articulo.
        /// </summary>
        /// <param name="codSelection"> id del articulo a seleccionar una vez visualizada la grilla.</param>
        public CSelProductoSACDlg(string codSelection = "")
        {
            m_codSelection = codSelection;
            m_ProductoSeleted = new CProductoSAC();
            InitializeComponent();
        }

        private void CABM_Productos_Load(object sender, EventArgs e)
        {
            CargarDataGrid();
            SelectItemDataGrid(m_codSelection);
        }

        private void CargarDataGrid(string filterForName = "")
        {
            LoadDataGrid(filterForName);
        }        

        private void LoadDataGrid(string nameFilter)
        {
            try
            {

                if (CDb.m_oleDbConnection.State == ConnectionState.Open)
                {
                    //creo el data table que contendra la tabla de productos SAC
                    dtProductos = CDb.GetProductosSAC(nameFilter);
                    
                    if (dtProductos != null && dtProductos.Rows.Count >0)
                    {
                        dataGridView_Productos.DataSource = dtProductos;

                        dataGridView_Productos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dataGridView_Productos.Columns["CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Productos.Columns["ALIAS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["ALIAS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Productos.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_Productos.Columns["NOMBRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        trackBar_dgvProductos.Minimum = 1;
                        trackBar_dgvProductos.Maximum = dataGridView_Productos.RowCount;
                        TrackBarValue = 1;
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para esta consulta", "Cargando datos en la grilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView_Productos.DataSource = null;
                    }
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

        private void SelectItemDataGrid(string codSelection)
        {
            if (codSelection != "")
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView_Productos.Rows)
                {
                    if (row.Cells["CODIGO"].Value != null) // Need to check for null if new row is exposed
                    {
                        if (row.Cells["CODIGO"].Value.ToString().Equals(codSelection))
                        {
                            rowIndex = row.Index;
                            dataGridView_Productos.CurrentCell = dataGridView_Productos.Rows[rowIndex].Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_Productos.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_Productos.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0)
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, idxRow];
            }
        }

        private void dataGridView_Productos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Productos.SelectedRows.Count > 0)
                {
                    SelectingRowProducto(dataGridView_Productos.SelectedRows[0]);
                    TrackBarValue = dataGridView_Productos.SelectedRows[0].Index + 1;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectingRowProducto(DataGridViewRow rowSelected)
        {
            if (rowSelected != null)
            {
                CodSelected = rowSelected.Cells["CODIGO"].Value.ToString();

                ProductoSelected = new CProductoSAC()
                {
                    Codigo = CDb.GetCellDGVString(rowSelected,"CODIGO"),
                    Alias = CDb.GetCellDGVString(rowSelected, "ALIAS"),
                    Nombre = CDb.GetCellDGVString(rowSelected, "NOMBRE"),
                };
            }
        }

        private void dataGridView_Productos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SelectingRowProducto(dataGridView_Productos.CurrentRow);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBox_nombreBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarDataGrid(textBox_nombreBuscar.Text);
        }

        private void textBox_nombreBuscar_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("EDITAR PRODUCTO A BUSCAR", "Nombre o Aproximación", textBox_nombreBuscar.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_nombreBuscar.Text = dlg.VALUE;
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0 && dataGridView_Productos.SelectedRows[0].Index > 0)
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, dataGridView_Productos.SelectedRows[0].Index-1];
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0 && (dataGridView_Productos.SelectedRows[0].Index < dataGridView_Productos.Rows.Count-1))
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, dataGridView_Productos.SelectedRows[0].Index + 1];
            }
        }

        private void trackBar_dgvProductos_Scroll(object sender, EventArgs e)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0 && (dataGridView_Productos.SelectedRows[0].Index < dataGridView_Productos.Rows.Count))
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, TrackBarValue - 1];
            }
        }
    }
}
