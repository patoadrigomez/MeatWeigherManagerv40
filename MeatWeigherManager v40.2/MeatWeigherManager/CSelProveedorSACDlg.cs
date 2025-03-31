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
    public partial class CSelProveedorSACDlg : Form
    {
        DataTable dtProveedores;
        string m_codSelected = "";
        string m_codSelection = "";
        CProveedorSAC m_ProveedorSeleted;

        public string CodSelected
        {
            get { return m_codSelected;}
            set { m_codSelected = value;}
        }

        public CProveedorSAC ProveedorSelected
        {
            get { return m_ProveedorSeleted; }
            set { m_ProveedorSeleted = value; }
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
        public CSelProveedorSACDlg(string codSelection = "")
        {
            m_codSelection = codSelection;
            m_ProveedorSeleted = new CProveedorSAC();
            InitializeComponent();
        }

        private void CABM_Proveedores_Load(object sender, EventArgs e)
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
                    dtProveedores = CDb.GetProveedoresSAC(nameFilter);
                    
                    if (dtProveedores != null && dtProveedores.Rows.Count >0)
                    {
                        dataGridView_Proveedores.DataSource = dtProveedores;

                        dataGridView_Proveedores.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dataGridView_Proveedores.Columns["CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Proveedores.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Proveedores.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_Proveedores.Columns["NOMBRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        trackBar_dgvProductos.Minimum = 1;
                        trackBar_dgvProductos.Maximum = dataGridView_Proveedores.RowCount;
                        TrackBarValue = 1;
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para esta consulta", "Cargando datos en la grilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView_Proveedores.DataSource = null;
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
                foreach (DataGridViewRow row in dataGridView_Proveedores.Rows)
                {
                    if (row.Cells["CODIGO"].Value != null) // Need to check for null if new row is exposed
                    {
                        if (row.Cells["CODIGO"].Value.ToString().Equals(codSelection))
                        {
                            rowIndex = row.Index;
                            dataGridView_Proveedores.CurrentCell = dataGridView_Proveedores.Rows[rowIndex].Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_Proveedores.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_Proveedores.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Proveedores.SelectedRows.Count > 0)
            {
                dataGridView_Proveedores.CurrentCell = dataGridView_Proveedores[0, idxRow];
            }
        }

        private void dataGridView_Productos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Proveedores.SelectedRows.Count > 0)
                {
                    SelectingRowProveedor(dataGridView_Proveedores.SelectedRows[0]);
                    TrackBarValue = dataGridView_Proveedores.SelectedRows[0].Index + 1;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectingRowProveedor(DataGridViewRow rowSelected)
        {
            if (rowSelected != null)
            {
                CodSelected = rowSelected.Cells["CODIGO"].Value.ToString();

                ProveedorSelected = new CProveedorSAC()
                {
                    Codigo = CDb.GetCellDGVString(rowSelected,"CODIGO"),
                    Nombre = CDb.GetCellDGVString(rowSelected, "NOMBRE"),
                };
            }
        }

        private void dataGridView_Productos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SelectingRowProveedor(dataGridView_Proveedores.CurrentRow);
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
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("EDITAR PROVEEDOR A BUSCAR", "Nombre o Aproximación", textBox_nombreBuscar.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_nombreBuscar.Text = dlg.VALUE;
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_Proveedores.SelectedRows.Count > 0 && dataGridView_Proveedores.SelectedRows[0].Index > 0)
            {
                dataGridView_Proveedores.CurrentCell = dataGridView_Proveedores[0, dataGridView_Proveedores.SelectedRows[0].Index-1];
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_Proveedores.SelectedRows.Count > 0 && (dataGridView_Proveedores.SelectedRows[0].Index < dataGridView_Proveedores.Rows.Count-1))
            {
                dataGridView_Proveedores.CurrentCell = dataGridView_Proveedores[0, dataGridView_Proveedores.SelectedRows[0].Index + 1];
            }
        }

        private void trackBar_dgvProveedores_Scroll(object sender, EventArgs e)
        {
            if (dataGridView_Proveedores.SelectedRows.Count > 0 && (dataGridView_Proveedores.SelectedRows[0].Index < dataGridView_Proveedores.Rows.Count))
            {
                dataGridView_Proveedores.CurrentCell = dataGridView_Proveedores[0, TrackBarValue - 1];
            }
        }
    }
}
