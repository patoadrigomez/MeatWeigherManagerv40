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
    public partial class CViewConsultaLoteIngPiezasProduccionDlg : Form
    {

        public CViewConsultaLoteIngPiezasProduccionDlg()
        {
            InitializeComponent();
            SetDGVDoubleBuffer(dataGridView_LOTE);
            SetDGVDoubleBuffer(dataGridView_PIEZAS);
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
                if (CDb.GetConsultaCompletaIngresosProduccionLotes(out ds))
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

                        if (ds.Relations.Contains("LOTES_PIEZAS"))
                        {
                            /*
                            Antes de realizar el binding con el datagrid es conveniente desabilitar el
                            AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                            para ganar velocidad en el proceso de binding.
                            */

                            dataGridView_PIEZAS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                            dataGridView_PIEZAS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            dataGridView_PIEZAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                            dataGridView_PIEZAS.DataSource = ds;
                            dataGridView_PIEZAS.DataMember = "LOTES.LOTES_PIEZAS";
                        }
                        else
                        {
                            dataGridView_PIEZAS.DataSource = null;
                            dataGridView_PIEZAS.DataMember = null;
                        }

                        dataGridView_LOTE.Columns["LOTE_PRODUCCION"].Visible = false;
                        dataGridView_LOTE.Columns["LOTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_LOTE.Columns["LOTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_LOTE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;


                        dataGridView_PIEZAS.Columns["LOTE_PRODUCCION"].Visible = false;
                        dataGridView_PIEZAS.Columns["IDPIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["IDPIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PIEZAS.Columns["OI"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["OI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PIEZAS.Columns["SECTOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["SECTOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PIEZAS.Columns["LOTE_PIEZA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["LOTE_PIEZA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PIEZAS.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PIEZAS.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        dataGridView_PIEZAS.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PIEZAS.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PIEZAS.Columns["TROPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["TROPA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PIEZAS.Columns["TIPIF"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PIEZAS.Columns["TIPIF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_PIEZAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    }
                    else
                    {
                        MessageBox.Show("NO HAY REGISTROS A MOSTRAR", "CONSULTA DE INGRESO DE PIEZAS PARA LOTE DE PRODUCCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    dataGridView_LOTE.DataSource = null;
                    dataGridView_LOTE.DataMember = null;
                    dataGridView_PIEZAS.DataSource = null;
                    dataGridView_PIEZAS.DataMember = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla de Piezas en Lote de Producción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CViewConsultaLoteIngPiezasProduccionDlg_Load(object sender, EventArgs e)
        {
            LoadDataGrid_Operaciones();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BorrarRegistrosSeleccionadosDataGridPesadasOI();
        }

        private void dataGridView_PIEZAS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView_PIEZAS.SelectedRows.Count > 0)
                {
                    BorrarRegistrosSeleccionadosDataGridPesadasOI();
                }
            }
        }

        private bool BorrarRegistrosSeleccionadosDataGridPesadasOI()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_PIEZAS.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_PIEZAS.SelectedRows.Count;
                        int countDeletedRegisters = 0;
                        string lote = dataGridView_LOTE.SelectedRows[0].Cells["LOTE"].Value.ToString();

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de piezas que ingresarón a producción para el lote: {1}  , confirma la eliminación ", countSelectRegisters, lote);
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPieza;
                            foreach (DataGridViewRow dgvr in dataGridView_PIEZAS.SelectedRows)
                            {
                                idPieza = Convert.ToInt32(dgvr.Cells["IDPIEZA"].Value);
                                if (!CDb.IsPartInEgresos(idPieza))
                                {
                                    if (CDb.BorrarRegistroTabla("DLP", "idpesaje", idPieza.ToString()))
                                    {
                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPiezaEnIngresoAProduccion, TYPE_CONTEXT_DBLOG.IngresoAProduccion,
                                        string.Format("Se Eliminó el ingreso a Producción de la pieza : {0}", idPieza));
                                        
                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);

                            if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar " + countDeletedRegisters + " registros de " + countSelectRegisters + " seleccionados , dado que los registros de piezas que ya fueron egresadas no podran ser eliminados.", "Protección de Borrado de Ingresos a Produccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("No se han eliminado registros de piezas colectadas dado que los mismos pertenecen a piezas ya egresadas.", "Protección de Borrado de Ingresos a Produccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
