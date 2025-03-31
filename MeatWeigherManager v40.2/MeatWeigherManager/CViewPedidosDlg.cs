using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfigApp;
using Db;
using EditStringTouchDlg;
using EditValueTouchDlg;
using IMCR.WaitCursor;
using StatusProgressBar;

namespace MeatWeigherManager
{
    public partial class CViewPedidosDlg : Form
    {
        int m_idEstacion;
        int m_selectionIdPedido;
        
        CPedido m_PedisoSelected;

        public CPedido PedidoSelected
        {
            get { return m_PedisoSelected; }
            set { m_PedisoSelected=value; }
        }

        public int TrackBarValue
        {
            get { return trackBar_dgvOIs.Maximum - trackBar_dgvOIs.Value + trackBar_dgvOIs.Minimum; }
            set { trackBar_dgvOIs.Value = trackBar_dgvOIs.Maximum - value + trackBar_dgvOIs.Minimum; }
        }

        public int SelectionIdPedido { get => m_selectionIdPedido; set => m_selectionIdPedido = value; }

        public CViewPedidosDlg(int selectionIdPedido=0)
        {
            m_idEstacion = CDb.m_OperadorActivo.m_idEstacion;
            InitializeComponent();
            m_PedisoSelected = new CPedido();
            groupBox_Busqueda.Visible = true;
            button_Seleccionar.Visible = true;
            SelectionIdPedido = selectionIdPedido;
            SetDGVDoubleBuffer(dataGridView_PedidosActivos);
        }
        private void CViewLoteCreadosDlg_Load(object sender, EventArgs e)
        {
            InitDateTimePickerFechaEntrega();
            CargarDataGrid();
            SelectItemDataGrid(SelectionIdPedido);
        }

        private void InitDateTimePickerFechaEntrega()
        {
            dateTimePicker_fechaEntrega.Value = DateTime.Now;
        }


        private void CargarDataGrid()
        {
            CargarDataGrid(checkBox_todosLosPedidos.Checked ? DateTime.MinValue : dateTimePicker_fechaEntrega.Value, textBox_valorBuscar.Text);
        }

        private void CargarDataGrid(DateTime fechaEntrega, string queryFilterNumComprobante="")
        {
            try
            {
                DataTable dt;
                
                
                if (CDb.GetPedidosActivos(fechaEntrega,queryFilterNumComprobante ,out dt))
                {//CodigoClienteSAC,CLIENTE,CodigoPedidoSAC,TipoPedidoSAC,COMPROBANTE,FECHA,ID,ACTIVO,IDOPERADOR,OPERADOR,PASW_OPERADOR,TIPO_OPERADOR
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_PedidosActivos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_PedidosActivos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_PedidosActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        //CodigoClienteSAC,CLIENTE,CodigoPedidoSAC,COMPROBANTE,FECHA,ID,ACTIVO,IDOPERADOR,OPERADOR,PASW_OPERADOR,TIPO_OPERADOR
                        dataGridView_PedidosActivos.DataSource = dt;
                        dataGridView_PedidosActivos.Columns["CodigoClienteSAC"].Visible = false;
                        dataGridView_PedidosActivos.Columns["CodigoPedidoSAC"].Visible = false;
                        dataGridView_PedidosActivos.Columns["IDOPERADOR"].Visible = false;
                        dataGridView_PedidosActivos.Columns["OPERADOR"].Visible = false;
                        dataGridView_PedidosActivos.Columns["PASW_OPERADOR"].Visible = false;
                        dataGridView_PedidosActivos.Columns["TIPO_OPERADOR"].Visible = false;
                        dataGridView_PedidosActivos.Columns["ACTIVO"].Visible = false;
                        dataGridView_PedidosActivos.Columns["ID"].Visible = false;
                        dataGridView_PedidosActivos.Columns["FECHA_CREACION"].Visible = false;

                        dataGridView_PedidosActivos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_PedidosActivos.Columns["CLIENTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_PedidosActivos.Columns["CLIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PedidosActivos.Columns["CLIENTE"].DisplayIndex=0;

                        dataGridView_PedidosActivos.Columns["COMPROBANTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_PedidosActivos.Columns["COMPROBANTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PedidosActivos.Columns["COMPROBANTE"].DisplayIndex=1;

                        dataGridView_PedidosActivos.Columns["TipoPedidoSAC"].HeaderText = "TIPO";
                        dataGridView_PedidosActivos.Columns["TipoPedidoSAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        dataGridView_PedidosActivos.Columns["FECHA_PREPARACION"].HeaderText = "PREPARACIÓN";
                        dataGridView_PedidosActivos.Columns["FECHA_PREPARACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_PedidosActivos.Columns["FECHA_PREPARACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_PedidosActivos.Columns["FECHA_PREPARACION"].DisplayIndex = 3;
                        dataGridView_PedidosActivos.Columns["FECHA_PREPARACION"].DefaultCellStyle.Format = "dd-MM-yyyy";

                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_PedidosActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        trackBar_dgvOIs.Minimum = 1;
                        trackBar_dgvOIs.Maximum = dataGridView_PedidosActivos.RowCount;
                        TrackBarValue = 1;
                        /*
                        int dgv_width = dataGridView_OI.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                        */
                    }
                    else
                    {
                        dataGridView_PedidosActivos.DataSource = null;
                        dataGridView_PedidosActivos.DataSource = null;
                    }
                }
                else
                {
                    dataGridView_PedidosActivos.DataSource = null;
                    dataGridView_PedidosActivos.DataSource = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error Cargando en la Grilla los Pediso Activos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                dataGridView_PedidosActivos.DataSource = null;
                dataGridView_PedidosActivos.DataSource = null;
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }

        private void button_Seleccionar_Click(object sender, EventArgs e)
        {
            SelectingRow(dataGridView_PedidosActivos.CurrentRow);
            DialogResult = DialogResult.OK;
            Close();
        }
        public void SelectItemDataGrid(int idSelection)
        {
            if (idSelection != 0)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView_PedidosActivos.Rows)
                {
                    if (row.Cells["ID"].Value != null) // Need to check for null if new row is exposed
                    {
                        if (row.Cells["ID"].Value.ToString().Equals(idSelection.ToString()))
                        {
                            rowIndex = row.Index;
                            dataGridView_PedidosActivos.CurrentCell= dataGridView_PedidosActivos.Rows[rowIndex].Cells[1];//usar el indice de una celda que este visible si no exeption!!
                            break;
                        }
                    }
                }
            }
        }
        //CodigoClienteSAC,CLIENTE,CodigoPedidoSAC,COMPROBANTE,FECHA,ID,ACTIVO,IDOPERADOR,OPERADOR,PASW_OPERADOR,TIPO_OPERADOR
        private void SelectingRow(DataGridViewRow rowSelected)
        {
            if (rowSelected != null)
            {
                PedidoSelected = new CPedido();
                PedidoSelected.CodigoPedidoSAC = CDb.GetCellDGVInt(rowSelected, "CodigoPedidoSAC");
                PedidoSelected.ComprobantePedidoSAC = CDb.GetCellDGVString(rowSelected, "COMPROBANTE");

                PedidoSelected.FechaHoraCreacion = CDb.GetCellDGVDateTime(rowSelected, "FECHA_CREACION");
                PedidoSelected.FechaHoraPreparacion = CDb.GetCellDGVDateTime(rowSelected, "FECHA_PREPARACION");

                PedidoSelected.Id = CDb.GetCellDGVInt(rowSelected, "ID");
                PedidoSelected.Activo = CDb.GetCellDGVBool(rowSelected, "ACTIVO");

                PedidoSelected.TipoPedidoSAC = CDb.GetCellDGVString(rowSelected, "TipoPedidoSAC");

                PedidoSelected.Operador = new COperador();
                PedidoSelected.Operador.m_id = CDb.GetCellDGVInt(rowSelected, "IDOPERADOR");
                PedidoSelected.Operador.m_nombre = CDb.GetCellDGVString(rowSelected, "OPERADOR");
                PedidoSelected.Operador.m_pasw = CDb.GetCellDGVString(rowSelected, "PASW_OPERADOR");
                PedidoSelected.Operador.m_tipo = CDb.GetCellDGVString(rowSelected, "TIPO_OPERADOR") != "" ? (TYPE_OPERATOR)CDb.GetCellDGVString(rowSelected, "TIPO_OPERADOR")[0] : TYPE_OPERATOR.USUARIO ;

                PedidoSelected.Cliente = new CClienteSAC();
                PedidoSelected.Cliente.Codigo = CDb.GetCellDGVString(rowSelected, "CodigoClienteSAC");
                PedidoSelected.Cliente.Nombre = CDb.GetCellDGVString(rowSelected, "CLIENTE");
            }
        }
        private void dataGridView_PedidosActivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex != -1)
                {
                    SelectingRow(dataGridView_PedidosActivos.CurrentRow);
                    DialogResult = DialogResult.OK;
                    Close();
                }
        }

        private void dataGridView_PedidosActivos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                if (dataGridView_PedidosActivos.CurrentRow.Index != -1)
                {
                    SelectingRow(dataGridView_PedidosActivos.CurrentRow);
                    Close();
                }
            }
        }

        private void textBox_valorBuscar_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_valorBuscar.Text,"NUMERO", "EDITAR EL NUMERO DE COMPROBANTE DEL PEDIDO A BUSCAR",CEditValueTouchDlg.TYPE_VALUE.STRING,textBox_valorBuscar.MaxLength);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_valorBuscar.Text = dlg.VALUE;
            }
        }

        private void textBox_valorBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarDataGrid();
        }

        private void textBox_numeric_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void trackBar_dgv_Scroll(object sender, EventArgs e)
        {
            if (dataGridView_PedidosActivos.SelectedRows.Count > 0 && (dataGridView_PedidosActivos.SelectedRows[0].Index < dataGridView_PedidosActivos.Rows.Count))
            {
                dataGridView_PedidosActivos.CurrentCell = dataGridView_PedidosActivos[1, TrackBarValue - 1];
            }
        }

        private void dataGridView_Pedidos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_PedidosActivos.SelectedRows.Count > 0)
                {
                    TrackBarValue = dataGridView_PedidosActivos.SelectedRows[0].Index + 1;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_PedidosActivos.SelectedRows.Count > 0 && (dataGridView_PedidosActivos.SelectedRows[0].Index < dataGridView_PedidosActivos.Rows.Count - 1))
            {
                dataGridView_PedidosActivos.CurrentCell = dataGridView_PedidosActivos[1, dataGridView_PedidosActivos.SelectedRows[0].Index + 1];
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_PedidosActivos.SelectedRows.Count > 0 && dataGridView_PedidosActivos.SelectedRows[0].Index > 0)
            {
                dataGridView_PedidosActivos.CurrentCell = dataGridView_PedidosActivos[1, dataGridView_PedidosActivos.SelectedRows[0].Index - 1];
            }
        }

        private void comboBox_transportistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDataGrid();
        }

        private void dateTimePicker_fechaEntrega_ValueChanged(object sender, EventArgs e)
        {
            CargarDataGrid();
        }

        public bool IsValidDateTime(string dateTime)
        {
            string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy" };
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("es-ES"),
                                           DateTimeStyles.None, out parsedDateTime);
        }
        public DateTime GetDateTimeFromString(string strDateTime)
        {
            return DateTime.Parse(strDateTime, new CultureInfo("es-ES", true));
        }

        private void button_EditValueFecha_Click(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Fecha de Entrega", "Fecha (dd-MM-yyyy) o (dd/MM/yyyy)", dateTimePicker_fechaEntrega.Value.ToString("dd-MM-yyyy"), 15);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (IsValidDateTime(dlg.VALUE))
                {
                    dateTimePicker_fechaEntrega.Value = GetDateTimeFromString(dlg.VALUE);
                }
                else
                {
                    MessageBox.Show("Error , edición de fecha con formato valido . El formato valido puede ser dd-mm-yyyy o dd/mm/yyyy .", "VALIDACIÓN DE EDICIÓN DE FECHA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox_todosLosPedidos_CheckedChanged(object sender, EventArgs e)
        {
            CargarDataGrid();
        }
    }
}
