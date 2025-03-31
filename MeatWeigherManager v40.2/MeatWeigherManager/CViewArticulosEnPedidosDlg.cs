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
using System.Windows.Forms;
using ConfigApp;
using Db;
using EditStringTouchDlg;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CViewArticulosEnPedidosDlg : Form
    {
        public int TrackBarValue
        {
            get { return trackBar_dgvOIs.Maximum - trackBar_dgvOIs.Value + trackBar_dgvOIs.Minimum; }
            set { trackBar_dgvOIs.Value = trackBar_dgvOIs.Maximum - value + trackBar_dgvOIs.Minimum; }
        }

        public CViewArticulosEnPedidosDlg()
        {
            InitializeComponent();
            groupBox_Busqueda.Visible = true;
            SetDGVDoubleBuffer(dataGridView_ProductosEnPedidosActivos);
        }
        private void CViewArticulosEnPedidosDlg_Load(object sender, EventArgs e)
        {
            InitDateTimePickerFechaEntrega();
            CargarDataGrid();
        }

        private void InitDateTimePickerFechaEntrega()
        {
            dateTimePicker_fechaEntrega.Value = DateTime.Now;
        }


        private void CargarDataGrid()
        {
            if (checkBox_conDetalleDePedidos.Checked)
                CargarDataGridConDetalleDePedidos(dateTimePicker_fechaEntrega.Value, textBox_valorBuscar.Text);
            else
                CargarDataGridSinDetalleDePedidos(dateTimePicker_fechaEntrega.Value);
        }

        private void CargarDataGridSinDetalleDePedidos(DateTime fechaEntrega)
        {
            try
            {
                DataTable dt;

                if (CDb.GetTotalizadoProductosPedidosActivos(fechaEntrega, out dt))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_ProductosEnPedidosActivos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_ProductosEnPedidosActivos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_ProductosEnPedidosActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        //CodigoSAC,ProductoSAC,Observacion,Unds_PED,Unds_PREP,Unds_REST,Peso_PED,Peso_PREP,Peso_REST
                        dataGridView_ProductosEnPedidosActivos.DataSource = null;
                        dataGridView_ProductosEnPedidosActivos.DataSource = dt;

                        dataGridView_ProductosEnPedidosActivos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["CodigoSAC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["CodigoSAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["ProductoSAC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_ProductosEnPedidosActivos.Columns["ProductoSAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_ProductosEnPedidosActivos.Columns["Observacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Observacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PED"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PED"].DefaultCellStyle.Format = "#";

                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PREP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PREP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PREP"].DefaultCellStyle.Format = "#";

                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_REST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_REST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_REST"].DefaultCellStyle.Format = "#";

                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PED"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PED"].DefaultCellStyle.Format = "#.00";
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PREP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PREP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PREP"].DefaultCellStyle.Format = "#.00";
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_REST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_REST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_REST"].DefaultCellStyle.Format = "#.00";
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_ProductosEnPedidosActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        trackBar_dgvOIs.Minimum = 1;
                        trackBar_dgvOIs.Maximum = dataGridView_ProductosEnPedidosActivos.RowCount;
                        TrackBarValue = 1;
                        /*
                        int dgv_width = dataGridView_OI.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                        */
                    }
                    else
                    {
                        dataGridView_ProductosEnPedidosActivos.DataSource = null;
                        dataGridView_ProductosEnPedidosActivos.DataSource = null;
                    }
                }
                else
                {
                    dataGridView_ProductosEnPedidosActivos.DataSource = null;
                    dataGridView_ProductosEnPedidosActivos.DataSource = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error Cargando en la Grilla los Pediso Activos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                dataGridView_ProductosEnPedidosActivos.DataSource = null;
                dataGridView_ProductosEnPedidosActivos.DataSource = null;
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
        private void CargarDataGridConDetalleDePedidos(DateTime fechaEntrega, string queryFilterNumComprobante="")
        {
            try
            {
                DataTable dt;

                if (CDb.GetDetalleProductosPedidosActivos(fechaEntrega,queryFilterNumComprobante ,out dt))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_ProductosEnPedidosActivos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_ProductosEnPedidosActivos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_ProductosEnPedidosActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        //Comprobante,Cliente,CodigoSAC,ProductoSAC,Observacion,Unds_PED,Unds_PREP,Unds_REST,Peso_PED,Peso_PREP,Peso_REST
                        dataGridView_ProductosEnPedidosActivos.DataSource = null;
                        dataGridView_ProductosEnPedidosActivos.DataSource = dt;

                        dataGridView_ProductosEnPedidosActivos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Comprobante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Comprobante"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["CodigoSAC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["CodigoSAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["CLIENTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_ProductosEnPedidosActivos.Columns["CLIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_ProductosEnPedidosActivos.Columns["ProductoSAC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_ProductosEnPedidosActivos.Columns["ProductoSAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_ProductosEnPedidosActivos.Columns["Observacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Observacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PED"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PREP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_PREP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_REST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Unds_REST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PED"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PED"].DefaultCellStyle.Format= "#.00";
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PREP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PREP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_PREP"].DefaultCellStyle.Format = "#.00";
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_REST"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_REST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_ProductosEnPedidosActivos.Columns["Peso_REST"].DefaultCellStyle.Format = "#.00";
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_ProductosEnPedidosActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        trackBar_dgvOIs.Minimum = 1;
                        trackBar_dgvOIs.Maximum = dataGridView_ProductosEnPedidosActivos.RowCount;
                        TrackBarValue = 1;
                        /*
                        int dgv_width = dataGridView_OI.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                        */
                    }
                    else
                    {
                        dataGridView_ProductosEnPedidosActivos.DataSource = null;
                        dataGridView_ProductosEnPedidosActivos.DataSource = null;
                    }
                }
                else
                {
                    dataGridView_ProductosEnPedidosActivos.DataSource = null;
                    dataGridView_ProductosEnPedidosActivos.DataSource = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error Cargando en la Grilla los Pediso Activos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CDbException dbex)
            {
                dataGridView_ProductosEnPedidosActivos.DataSource = null;
                dataGridView_ProductosEnPedidosActivos.DataSource = null;
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
            if (dataGridView_ProductosEnPedidosActivos.SelectedRows.Count > 0 && (dataGridView_ProductosEnPedidosActivos.SelectedRows[0].Index < dataGridView_ProductosEnPedidosActivos.Rows.Count))
            {
                dataGridView_ProductosEnPedidosActivos.CurrentCell = dataGridView_ProductosEnPedidosActivos[0, TrackBarValue - 1];
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_ProductosEnPedidosActivos.SelectedRows.Count > 0 && (dataGridView_ProductosEnPedidosActivos.SelectedRows[0].Index < dataGridView_ProductosEnPedidosActivos.Rows.Count - 1))
            {
                dataGridView_ProductosEnPedidosActivos.CurrentCell = dataGridView_ProductosEnPedidosActivos[0, dataGridView_ProductosEnPedidosActivos.SelectedRows[0].Index + 1];
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_ProductosEnPedidosActivos.SelectedRows.Count > 0 && dataGridView_ProductosEnPedidosActivos.SelectedRows[0].Index > 0)
            {
                dataGridView_ProductosEnPedidosActivos.CurrentCell = dataGridView_ProductosEnPedidosActivos[0, dataGridView_ProductosEnPedidosActivos.SelectedRows[0].Index - 1];
            }
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

        private void checkBox_conDetalleDePedidos_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Busqueda.Visible = checkBox_conDetalleDePedidos.Checked;
            CargarDataGrid();
        }
    }
}
