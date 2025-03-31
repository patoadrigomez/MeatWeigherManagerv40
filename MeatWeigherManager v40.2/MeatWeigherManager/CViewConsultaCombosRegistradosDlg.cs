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
    public partial class CViewConsultaCombosRegistradosDlg : Form
    {

        public CViewConsultaCombosRegistradosDlg()
        {
            InitializeComponent();
            SetDGVDoubleBuffer(dataGridView_Combos);
            SetDGVDoubleBuffer(dataGridView_piezasContenidas);
        }

        private void LoadDataGrid_Operaciones()
        {
            try
            {
                DataSet ds;
                if (CDb.GetConsultaCompletaRegistracionesCombos(out ds))
                {
                    if (ds.Tables["COMBOS"].Rows.Count > 0)
                    {

                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_Combos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_Combos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_Combos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_Combos.DataSource = ds;
                        dataGridView_Combos.DataMember = "COMBOS";

                        //CMB_NRO,COMBO,DESTINO,CREADO,UNIDADES,BRUTO,TARA,NETO
                        dataGridView_Combos.Columns["CMB_NRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Combos.Columns["CMB_NRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Combos.Columns["COMBO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Combos.Columns["COMBO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_Combos.Columns["DESTINO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Combos.Columns["DESTINO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_Combos.Columns["CREADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Combos.Columns["CREADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Combos.Columns["EST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Combos.Columns["EST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Combos.Columns["UNIDADES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_Combos.Columns["UNIDADES"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Combos.Columns["BRUTO"].DefaultCellStyle.Format = "0.##";
                        dataGridView_Combos.Columns["TARA"].DefaultCellStyle.Format = "0.##";
                        dataGridView_Combos.Columns["NETO"].DefaultCellStyle.Format = "0.##";

                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_Combos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        if (ds.Relations.Contains("COMBOS_PIEZAS"))
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
                            dataGridView_piezasContenidas.DataMember = "COMBOS.COMBOS_PIEZAS";
                            //CMB_NRO,PRODUCTO,PIEZA,NETO
                            dataGridView_piezasContenidas.Columns["CMB_NRO"].Visible = false;
                            dataGridView_piezasContenidas.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_piezasContenidas.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_piezasContenidas.Columns["PIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_piezasContenidas.Columns["PIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                    dataGridView_Combos.DataSource = null;
                    dataGridView_Combos.DataMember = null;
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

        private void CViewConsultaCombosRegistradosDlg_Load(object sender, EventArgs e)
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

        private void reimprimirEtiquetaComboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CContenedor> listCombos = CDb.GetCombosFromSelectedDGVCombos(dataGridView_Combos);

                if (listCombos != null && listCombos.Count > 0)
                {
                    foreach (CContenedor cn in listCombos)
                    {
                        CLabel.PrintCombo(cn);
                    }
                }
            }
        }
    }
}
