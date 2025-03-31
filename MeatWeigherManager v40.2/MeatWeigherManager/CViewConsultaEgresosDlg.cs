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
    public partial class CViewConsultaEgresosDlg : Form
    {

        public CViewConsultaEgresosDlg()
        {
            InitializeComponent();
            //Establece doble buffer para los datagrid
            SetDGVDoubleBuffer(dataGridView_PEDIDOS);
            SetDGVDoubleBuffer(dataGridView_PIEZAS);
        }

        private void LoadDataGrid_Operaciones()
        {
            try
            {
                DataSet ds;
                if (CDb.GetConsultaCompletaEgresos(out ds))
                {
                    if (ds.Tables["PEDIDOS"].Rows.Count > 0)
                    {

                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_PEDIDOS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_PEDIDOS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_PEDIDOS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_PEDIDOS.DataSource = ds;
                        dataGridView_PEDIDOS.DataMember = "PEDIDOS";

                        //IDPEDIDO,EMPRESA,CODCOM,PEDIDO,CLIENTE,FECHA_PEDIDO,ESTADO
                        dataGridView_PEDIDOS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PEDIDOS.Columns["IDPEDIDO"].Visible = false;
                        dataGridView_PEDIDOS.Columns["FECHA_PEDIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PEDIDOS.Columns["FECHA_PEDIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PEDIDOS.Columns["FECHA_PEDIDO"].DefaultCellStyle.Format = "dd-MM-yyyy";

                        dataGridView_PEDIDOS.Columns["PEDIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dataGridView_PEDIDOS.Columns["PEDIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PEDIDOS.Columns["CLIENTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_PEDIDOS.Columns["CLIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_PEDIDOS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        if (ds.Relations.Contains("PEDIDOS_BULTOS"))
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
                            dataGridView_PIEZAS.DataMember = "PEDIDOS.PEDIDOS_BULTOS";

                            //IDPEDIDO,TIPO,NRO,PRODUCTO,LOTE,UNDS,NETO,FECHA_EGRESO,OPERADOR
                            dataGridView_PIEZAS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PIEZAS.Columns["IDPEDIDO"].Visible = false;
                            dataGridView_PIEZAS.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dataGridView_PIEZAS.Columns["TIPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PIEZAS.Columns["NRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PIEZAS.Columns["NRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PIEZAS.Columns["PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PIEZAS.Columns["PRODUCTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PIEZAS.Columns["LOTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PIEZAS.Columns["LOTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PIEZAS.Columns["UNDS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            dataGridView_PIEZAS.Columns["UNDS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PIEZAS.Columns["NETO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PIEZAS.Columns["NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView_PIEZAS.Columns["FECHA_EGRESO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PIEZAS.Columns["FECHA_EGRESO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView_PIEZAS.Columns["OPERADOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dataGridView_PIEZAS.Columns["OPERADOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                            /*
                            HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                            */
                            dataGridView_PIEZAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        }
                        else
                        {
                            dataGridView_PIEZAS.DataSource = null;
                            dataGridView_PIEZAS.DataMember = null;
                        }


                    }
                    else
                    {
                        MessageBox.Show("NO HAY REGISTROS A MOSTRAR", "CONSULTA DE EGRESOS EN PEDIDOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    dataGridView_PEDIDOS.DataSource = null;
                    dataGridView_PEDIDOS.DataMember = null;
                    dataGridView_PIEZAS.DataSource = null;
                    dataGridView_PIEZAS.DataMember = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla de Egresos en Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla de Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CViewConsultaEgresosDlg_Load(object sender, EventArgs e)
        {
            LoadDataGrid_Operaciones();
        }

        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuContextDelete_Click(object sender, EventArgs e)
        {
            BorrarRegistrosSeleccionadosDataGridPiezasEgresadas();
        }
        private bool BorrarRegistrosSeleccionadosDataGridPiezasEgresadas()
        {
            bool borradoOk = false;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_PIEZAS.SelectedRows.Count > 0)
                    {
                        string detailResult;
                        int countSelectRegisters = dataGridView_PIEZAS.SelectedRows.Count;
                        int countDeletedRegisters = 0;
                        string pedido = dataGridView_PEDIDOS.SelectedRows[0].Cells["PEDIDO"].Value.ToString();
                        int idPedido = Convert.ToInt32(dataGridView_PIEZAS.SelectedRows[0].Cells["IDPEDIDO"].Value);
                        bool pedidoAbierto = dataGridView_PEDIDOS.SelectedRows[0].Cells["ESTADO"].Value.ToString().Equals("ABIERTO");
                        if (pedidoAbierto)
                        {
                            string aviso = String.Format("Usted esta a punto de Eliminar {0} registros de piezas Egresadas en el Pedido: {1}  , confirma la eliminación ", countSelectRegisters, pedido);
                            if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int idPieza;
                                string Tipo;

                                foreach (DataGridViewRow dgvr in dataGridView_PIEZAS.SelectedRows)
                                {
                                    idPieza = Convert.ToInt32(dgvr.Cells["NRO"].Value);
                                    Tipo = dgvr.Cells["TIPO"].Value.ToString();

                                    if (Tipo == "PIEZA")
                                    {
                                        if (CDb.BorrarPiezaEgresadaDelPedido(idPieza, idPedido, out detailResult))
                                        {
                                            CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionEgresoPieza, TYPE_CONTEXT_DBLOG.Egresos,
                                                string.Format("Se Eliminó el egreso de la pieza : {0} del Pedido: {1} ", idPieza,idPedido));

                                            countDeletedRegisters++;
                                        }
                                    }
                                    if (Tipo == "CAJA" || Tipo == "COMBO")
                                    {
                                        if (CDb.BorrarContenedorEgresadoDelPedido(idPieza,idPedido,out detailResult))
                                        {
                                            CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EliminacionEgresoContenedor, TYPE_CONTEXT_DBLOG.Egresos,
                                                string.Format("Se Eliminó el egreso del contenedor: {0} del Pedido: {1} ", idPieza, idPedido));
                                            countDeletedRegisters++;
                                        }
                                    }
                                }
                                borradoOk = (countDeletedRegisters > 0);

                                if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters > 0)
                                {
                                    MessageBox.Show("Solo se han podido eliminar " + countDeletedRegisters + " registros de " + countSelectRegisters + " seleccionados .", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (countSelectRegisters != countDeletedRegisters && countDeletedRegisters == 0)
                                {
                                    MessageBox.Show("Ningun registro fue eliminado.", "Protección de Borrado de Pesadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                LoadDataGrid_Operaciones();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Operacion no permitida para un Pedido que se encuentra CERRADO", "Control de Acceso a usuarios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void toolStripMenuItem_Abrir_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip floatMenu = menuItem.Owner as ContextMenuStrip;
                if (floatMenu != null)
                {
                    Control controlSelected = floatMenu.SourceControl;
                    if(controlSelected.Equals(dataGridView_PEDIDOS))
                    {
                        SetStatusSelectPedido(STATUS_PEDIDO.ACTIVO);
                    }
                }
            }
        }

        private void toolStripMenuItem_cerrar_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip floatMenu = menuItem.Owner as ContextMenuStrip;
                if (floatMenu != null)
                {
                    Control controlSelected = floatMenu.SourceControl;
                    if (controlSelected.Equals(dataGridView_PEDIDOS))
                    {
                        SetStatusSelectPedido(STATUS_PEDIDO.CERRADO);
                    }
                }
            }
        }

        private bool SetStatusSelectPedido(STATUS_PEDIDO status)
        {
            bool establecidoOk = false;
            int idPedido;
            try
            {
                if (CDb.m_OperadorActivo.m_tipo == TYPE_OPERATOR.SUPERVISOR)
                {

                    if (dataGridView_PEDIDOS.SelectedRows.Count > 0)
                    {
                        int countSelectRegisters = dataGridView_PIEZAS.SelectedRows.Count;
                        int countDeletedRegisters = 0;
                        string pedido = dataGridView_PEDIDOS.SelectedRows[0].Cells["PEDIDO"].Value.ToString();

                        string aviso = String.Format("Usted esta a punto de Establecer como {0} los pedidos seleccionados, confirma la acción ", STATUS_PEDIDO.ACTIVO.ToString());
                        if (MessageBox.Show(aviso, "CONFIRMACIÓN DE CAMBIO DE ESTADO DEL PEDIDO",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow dgvr in dataGridView_PEDIDOS.SelectedRows)
                            {
                                idPedido = Convert.ToInt32(dgvr.Cells["IDPEDIDO"].Value);
                                if (CDb.SetStatusPedido(idPedido,status))
                                {

                                    CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.ModificacionEstadoPedido, TYPE_CONTEXT_DBLOG.Egresos,
                                        string.Format("Se Modifico el estado del Pedido: {0} quedando el mismo en : {1}", idPedido, status == STATUS_PEDIDO.ACTIVO ?"ABIERTO":"CERRADO"));

                                    countDeletedRegisters++;
                                }
                            }
                            establecidoOk = (countDeletedRegisters > 0);

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
            return establecidoOk;
        }

    }
}
