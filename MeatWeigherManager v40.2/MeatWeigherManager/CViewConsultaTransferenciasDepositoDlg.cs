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
    public partial class CViewConsultaTransferenciasDepositoDlg : Form
    {

        public CViewConsultaTransferenciasDepositoDlg()
        {
            InitializeComponent();
            SetDGVDoubleBuffer(dataGridView_FECHAS);
            SetDGVDoubleBuffer(dataGridView_MOVIMIENTOS);
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
                if (CDb.GetConsultaMovimientosTransladosEntreDepositos(out ds))
                {
                    if (ds.Tables["FECHAS"].Rows.Count > 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_FECHAS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_FECHAS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_FECHAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_FECHAS.DataSource = ds;
                        dataGridView_FECHAS.DataMember = "FECHAS";

                        if (ds.Relations.Contains("FECHAS_MOVIMIENTOS"))
                        {
                            /*
                            Antes de realizar el binding con el datagrid es conveniente desabilitar el
                            AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                            para ganar velocidad en el proceso de binding.
                            */

                            dataGridView_MOVIMIENTOS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                            dataGridView_MOVIMIENTOS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            dataGridView_MOVIMIENTOS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                            dataGridView_MOVIMIENTOS.DataSource = ds;
                            dataGridView_MOVIMIENTOS.DataMember = "FECHAS.FECHAS_MOVIMIENTOS";
                        }
                        else
                        {
                            dataGridView_MOVIMIENTOS.DataSource = null;
                            dataGridView_MOVIMIENTOS.DataMember = null;
                        }

                        dataGridView_FECHAS.Columns["FECHA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_FECHAS.Columns["FECHA"].DefaultCellStyle.Format = "dd-MM-yyyy";
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_FECHAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        dataGridView_MOVIMIENTOS.AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                        dataGridView_MOVIMIENTOS.Columns["FECHA"].Visible = false;
                        dataGridView_MOVIMIENTOS.Columns["DETALLE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_MOVIMIENTOS.Columns["DETALLE"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView_MOVIMIENTOS.Columns["FECHA_HORA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_MOVIMIENTOS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    }
                    else
                    {
                        MessageBox.Show("NO HAY REGISTROS A MOSTRAR", "CONSULTA DE MOVIMIENTOS ENTRE DEPÓSITOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    dataGridView_FECHAS.DataSource = null;
                    dataGridView_FECHAS.DataMember = null;
                    dataGridView_MOVIMIENTOS.DataSource = null;
                    dataGridView_MOVIMIENTOS.DataMember = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla de Movimientos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CViewConsultaTransferenciasDepositoDlg_Load(object sender, EventArgs e)
        {
            LoadDataGrid_Operaciones();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
