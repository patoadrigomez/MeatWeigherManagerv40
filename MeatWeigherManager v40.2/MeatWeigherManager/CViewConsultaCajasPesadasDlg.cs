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
    public partial class CViewConsultaCajasPesadasDlg : Form
    {

        public CViewConsultaCajasPesadasDlg()
        {
            InitializeComponent();
            SetDGVDoubleBuffer(dataGridView_Cajas);
            SetDGVDoubleBuffer(dataGridView_piezasContenidas);
        }

        private void LoadDataGrid_Operaciones()
        {
            try
            {
                DataSet ds;
                if (CDb.GetConsultaCompletaPesajesCajas(out ds))
                {
                    if (ds.Tables["CAJAS"].Rows.Count > 0)
                    {

                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_Cajas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_Cajas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_Cajas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_Cajas.DataSource = ds;
                        dataGridView_Cajas.DataMember = "CAJAS";

                        //CAJA,CREADA,PRODUCTO,UNIDADES,BRUTO,TARA,NETO
                        dataGridView_Cajas.Columns["CREADA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Cajas.Columns["CREADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_Cajas.Columns["EST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Cajas.Columns["EST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Cajas.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Cajas.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_Cajas.Columns["BRUTO"].DefaultCellStyle.Format = "0.00";
                        dataGridView_Cajas.Columns["TARA"].DefaultCellStyle.Format = "0.00";
                        dataGridView_Cajas.Columns["NETO"].DefaultCellStyle.Format = "0.00";

                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_Cajas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        if (ds.Relations.Contains("CAJAS_PIEZAS"))
                        {
                            /*
                            Antes de realizar el binding con el datagrid es conveniente desabilitar el
                            AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                            para ganar velocidad en el proceso de binding.
                            */

                            dataGridView_piezasContenidas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                            dataGridView_piezasContenidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            dataGridView_piezasContenidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                            dataGridView_piezasContenidas.DataSource = ds;
                            dataGridView_piezasContenidas.DataMember = "CAJAS.CAJAS_PIEZAS";
                            //CAJA,PIEZA,NETO
                            dataGridView_piezasContenidas.Columns["CAJA"].Visible = false;
                            dataGridView_piezasContenidas.Columns["PIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_piezasContenidas.Columns["PIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_piezasContenidas.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_piezasContenidas.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            /*
                            HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                            */
                            dataGridView_piezasContenidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                        }
                        else
                        {
                            dataGridView_piezasContenidas.DataSource = null;
                            dataGridView_piezasContenidas.DataMember = null;
                        }

                    }
                    else
                    {
                        MessageBox.Show("NO HAY REGISTROS A MOSTRAR", "CONSULTA DE CAJAS PESADAS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    dataGridView_Cajas.DataSource = null;
                    dataGridView_Cajas.DataMember = null;
                    dataGridView_piezasContenidas.DataSource = null;
                    dataGridView_piezasContenidas.DataMember = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla de Cajas Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CViewConsultaCajasPesadasDlg_Load(object sender, EventArgs e)
        {
            LoadDataGrid_Operaciones();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }

        private void reimprimirEtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CPesada> listPesadas = CDb.GetWeightingFromSelectedDGVPesadas(dataGridView_piezasContenidas,"PIEZA");

                if (listPesadas != null && listPesadas.Count > 0)
                {
                    foreach (CPesada pesada in listPesadas)
                    {
                        CLabel.PrintProduct(pesada,ConfigApp.CConfigApp.m_pesajeEnProduccion_WeightLabelEnable, ConfigApp.CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable, 2);
                    }
                }
            }
        }

        private void reimprimirEtiquetaCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CContenedor> listCajasPesadas = CDb.GetWeightingFromSelectedDGVCajas(dataGridView_Cajas);

                if (listCajasPesadas != null && listCajasPesadas.Count > 0)
                {
                    foreach (CContenedor caja in listCajasPesadas)
                    {
                        CLabel.PrintCaja(caja);
                    }
                }
            }
        }
    }
}
