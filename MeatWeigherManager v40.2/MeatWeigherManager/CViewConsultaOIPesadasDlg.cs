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
    public partial class CViewConsultaOIPesadasDlg : Form
    {

        public CViewConsultaOIPesadasDlg()
        {
            InitializeComponent();
            //Establece doble buffer para los datagrid
            SetDGVDoubleBuffer(dataGridView_OI);
            SetDGVDoubleBuffer(dataGridView_PESADAS);
        }

        private void LoadDataGrid_Operaciones()
        {
            try
            {
                DataSet ds;
                if (CDb.GetConsultaCompletaOI(out ds))
                {
                    if (ds.Tables["OI"].Rows.Count > 0)
                    {

                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_OI.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_OI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_OI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_OI.DataSource = ds;
                        dataGridView_OI.DataMember = "OI";

                        dataGridView_OI.Columns["IDOI"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_OI.Columns["IDOI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["FECHA_HORA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_OI.Columns["FECHA_HORA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["IDESTACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_OI.Columns["IDESTACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["ESTADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_OI.Columns["ESTADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["CERT_SANITARIO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_OI.Columns["CERT_SANITARIO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_OI.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["PROVEEDOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_OI.Columns["PROVEEDOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_OI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        if (ds.Relations.Contains("OI_PESADA"))
                        {
                            /*
                            Antes de realizar el binding con el datagrid es conveniente desabilitar el
                            AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                            para ganar velocidad en el proceso de binding.
                            */

                            dataGridView_PESADAS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                            dataGridView_PESADAS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            dataGridView_PESADAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                            dataGridView_PESADAS.DataSource = ds;
                            dataGridView_PESADAS.DataMember = "OI.OI_PESADA";

                            dataGridView_PESADAS.Columns["IDOI"].Visible = false;
                            dataGridView_PESADAS.Columns["IDPESADA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PESADAS.Columns["IDPESADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PESADAS.Columns["FECHA_HORA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PESADAS.Columns["FECHA_HORA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PESADAS.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PESADAS.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PESADAS.Columns["COD_PROD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PESADAS.Columns["COD_PROD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PESADAS.Columns["UNDS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PESADAS.Columns["UNDS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PESADAS.Columns["PESO_NETO"].HeaderText = "NETO";
                            dataGridView_PESADAS.Columns["PESO_NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PESADAS.Columns["PESO_NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PESADAS.Columns["PESO_TARA"].HeaderText = "TARA";
                            dataGridView_PESADAS.Columns["PESO_TARA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PESADAS.Columns["PESO_TARA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PESADAS.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PESADAS.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PESADAS.Columns["TROPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PESADAS.Columns["TROPA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PESADAS.Columns["TIPIF"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PESADAS.Columns["TIPIF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PESADAS.Columns["PESO_REMITIDO"].HeaderText = "REMITIDO";
                            dataGridView_PESADAS.Columns["PESO_REMITIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PESADAS.Columns["PESO_REMITIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PESADAS.Columns["PESO_REMITIDO"].DefaultCellStyle.Format = "0.##";

                            /*
                            HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                            */
                            dataGridView_PESADAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        }
                        else
                        {
                            dataGridView_PESADAS.DataSource = null;
                            dataGridView_PESADAS.DataMember = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("NO HAY REGISTROS A MOSTRAR", "CONSULTA DE PESADAS EN ORDENES DE INGRESOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    dataGridView_OI.DataSource = null;
                    dataGridView_OI.DataMember = null;
                    dataGridView_PESADAS.DataSource = null;
                    dataGridView_PESADAS.DataMember = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla de Pesadas en Ordenes de Ingresos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }
        private void CViewConsultaOIPesadasDlg_Load(object sender, EventArgs e)
        {
            LoadDataGrid_Operaciones();
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void reimprimirEtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuContextItem = sender as ToolStripMenuItem;
            if (menuContextItem != null)
            {
                List<CPesada> listPesadas = CDb.GetWeightingFromSelectedDGVPesadas(dataGridView_PESADAS,"IDPESADA");

                if (listPesadas != null && listPesadas.Count > 0)
                {
                    foreach (CPesada pesada in listPesadas)
                    {
                        CLabel.PrintProduct(pesada, ConfigApp.CConfigApp.m_ingresoAPlanta_WeightLabelEnable,true, 2);
                    }
                }
            }
        }

        private void toolStripMenuContextDelete_Click(object sender, EventArgs e)
        {
            BorrarRegistrosSeleccionadosDataGridPesadasOI();
        }
        private bool BorrarRegistrosSeleccionadosDataGridPesadasOI()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_PESADAS.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_PESADAS.SelectedRows.Count;
                        int countDeletedRegisters = 0;
                        int idOI = Convert.ToInt32(dataGridView_OI.SelectedRows[0].Cells["IDOI"].Value);

                        string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de pesaje de la Orden de Ingreso: {1}  , confirma la eliminación ", countSelectRegisters, idOI);
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int idPesada;
                            foreach (DataGridViewRow dgvr in dataGridView_PESADAS.SelectedRows)
                            {
                                idPesada = Convert.ToInt32(dgvr.Cells["IDPESADA"].Value);
                                if (CDb.IPartInStock(idPesada))
                                {
                                    if (CDb.BorrarPieza(idPesada))
                                    {
                                        /// por mas que la pesadas sea o no insumo llama al metodo de borrar insumo.
                                        /// si la pesada no es un insumo el metodo no realizara ninguna accion dado que la
                                        /// pesada no existira en la tabla por no pertenecer a un producto de tipo insumo.
                                        CDb.EliminarMovimientoInsumo(TYPE_INSUMO_PROC.IngresoPlanta, idPesada);

                                        CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionPieza, TYPE_CONTEXT_DBLOG.IngresoAPlanta,
                                        string.Format("Se Eliminó la pieza : {0} de la Orden de Ingreso: {1}", idPesada,idOI));

                                        countDeletedRegisters++;
                                    }
                                }
                            }
                            borradoOk = (countDeletedRegisters > 0);

                            if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                            {
                                MessageBox.Show("Solo se han podido eliminar " + countDeletedRegisters + " registros de " + countSelectRegisters + " seleccionados , dado que los registros de pesaje que han sido parte de produccion o fueron egresados no podran ser eliminados.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                            {
                                MessageBox.Show("Ningun registro fue eliminado , los mismos pertenecen a piezas que han sido parte de produccion o ya fueron egresadas.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
