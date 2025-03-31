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
    public partial class CViewHistoryPartDlg : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;

        /// <summary>
        /// Clase de Visualizacion y seleccion de un articulo.
        /// </summary>
        /// <param name="codSelection"> id del articulo a seleccionar una vez visualizada la grilla.</param>
        public CViewHistoryPartDlg(string codSelection = "")
        {
            InitializeComponent();
        }

        private void CViewHistoryPartDlg_Load(object sender, EventArgs e)
        {
        }

        private void LoadDataGrid(int id,bool esContenedor=false)
        {
            try
            {
                //MOV,FECHA,PRODUCTO,LOTE,OI,PROVEEDOR,SANITARIO,CONTENEDOR,DESTINO,CLIENTE,COMPROBANTE,SECTOR,OPERADOR,ESTACION,UNIDADES,NETO,TARA,PESO_REMITIDO

                if (CDb.m_oleDbConnection.State == ConnectionState.Open)
                {
                    DataTable dt = CDb.GetMovimientosPieza(id,esContenedor);
                    
                    if (dt != null && dt.Rows.Count >0)
                    {
                        dataGridView_Historico.DataSource = dt;

                        dataGridView_Historico.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Historico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                    else
                    {
                        dataGridView_Historico.DataSource = null;
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

        private void textBox_idBuscar_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_id.Text,"NUMERO DE PIEZA/CONTENEDOR", 
                "Editar numero de pieza o contenedor",CEditValueTouchDlg.TYPE_VALUE.NUMERIC,textBox_id.MaxLength);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_id.Text = dlg.VALUE;
            }
        }

        private void textBox_id_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox_id_TextChanged(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void checkBox_esContenedor_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
        
        private void LoadDataGrid()
        {
            int id = Convert.ToInt32(textBox_id.Text==""?"0": textBox_id.Text);
            LoadDataGrid(id, checkBox_esContenedor.Checked);
        }
    }
}
