using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Db;
using EditStringTouchDlg;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CViewOICreadasDlg : Form
    {
        DataSet dsLotes;
        private int m_idEstacion;
        bool m_primerCargaDataGridView = true;
        
        COi m_OISelected;

        public COi OISelected
        {
            get { return m_OISelected; }
            set { m_OISelected=value; }
        }

        public int TrackBarValue
        {
            get { return trackBar_dgvOIs.Maximum - trackBar_dgvOIs.Value + trackBar_dgvOIs.Minimum; }
            set { trackBar_dgvOIs.Value = trackBar_dgvOIs.Maximum - value + trackBar_dgvOIs.Minimum; }
        }

        public enum MODO_VIEW_OI_CREADAS_DLG
        {
            SOLO_REPORTE,
            SELECCION_OI,
        }
        private MODO_VIEW_OI_CREADAS_DLG m_tipoDLG;

        public CViewOICreadasDlg(string titulo = "MeatWeigherManager - (Informacion de Ordenes de Ingresos Creadas)",MODO_VIEW_OI_CREADAS_DLG _tipoDlg  = MODO_VIEW_OI_CREADAS_DLG.SOLO_REPORTE)
        {
            m_idEstacion = CDb.m_OperadorActivo.m_idEstacion;
            m_tipoDLG = _tipoDlg;
            InitializeComponent();
            m_OISelected = new COi();
            Text = titulo;
            if (m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI)
            {
                groupBox_Busqueda.Visible = true;
                button_Seleccionar.Visible = true;
            }
            SetDGVDoubleBuffer(dataGridView_OI);
        }

        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }

        private void CViewLoteCreadosDlg_Load(object sender, EventArgs e)
        {
            CargarDataGrid(m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI);
        }

        private void CargarDataGrid(bool soloOIsEnProceso=false,int idOI=0)
        {
            try
            {
                DataTable dt;
                //OI ,PROVEEDOR,CERT_SANITARIO,FECHA_HORA,ESTACION,IDOPERADOR,OPERADOR,PASW_OPERADOR,TIPO_OPERADOR,CODIGO_PROVEEDOR,ESTADO ,ACTIVO

                if (CDb.GetDatSet_OICreadas(out dt,soloOIsEnProceso,idOI))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_OI.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_OI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_OI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                        dataGridView_OI.DataSource = dt;
                        dataGridView_OI.Columns["IDOPERADOR"].Visible = false;
                        dataGridView_OI.Columns["OPERADOR"].Visible = false;
                        dataGridView_OI.Columns["PASW_OPERADOR"].Visible = false;
                        dataGridView_OI.Columns["TIPO_OPERADOR"].Visible = false;
                        dataGridView_OI.Columns["CODIGO_PROVEEDOR"].Visible = false;
                        dataGridView_OI.Columns["ACTIVO"].Visible = false;
                        dataGridView_OI.Columns["ESTADO"].Visible = m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SOLO_REPORTE;

                        dataGridView_OI.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_OI.Columns["PROVEEDOR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView_OI.Columns["PROVEEDOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_OI.Columns["ESTACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        dataGridView_OI.Columns["ESTACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_OI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        trackBar_dgvOIs.Minimum = 1;
                        trackBar_dgvOIs.Maximum = dataGridView_OI.RowCount;
                        TrackBarValue = 1;
                        /*
                        int dgv_width = dataGridView_OI.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                        */
                    }
                    else
                    {
                        dataGridView_OI.DataSource = null;
                        dataGridView_OI.DataSource = null;
                        if (m_primerCargaDataGridView)
                        {
                            MessageBox.Show("No hay Ordenes de Ingresos que Mostrar", "Cargando Ordes de Ingresos Creadas en la Grilla ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                    }
                }
                else
                {
                    dataGridView_OI.DataSource = null;
                    dataGridView_OI.DataSource = null;
                    MessageBox.Show("Error de Base de Datos: ", "Error Cargando en la Grilla las Ordenes de Ingresos Creadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                m_primerCargaDataGridView = false;
            }
            catch (CDbException dbex)
            {
                dataGridView_OI.DataSource = null;
                dataGridView_OI.DataSource = null;
                MessageBox.Show(dbex.Message, "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error al cargar la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Seleccionar_Click(object sender, EventArgs e)
        {
            if (m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI)
            {
                SelectingRowOI(dataGridView_OI.CurrentRow);
                Close();
            }
        }
        
        private void SelectingRowOI(DataGridViewRow rowSelected)
        {
            int idOISelecting = Convert.ToInt32(rowSelected.Cells["OI"].Value);

            if (rowSelected != null)
            {
                OISelected = new COi()
                {
                    m_id = idOISelecting,
                    m_idEstacion = CDb.GetCellDGVInt(rowSelected,"ESTACION"),
                    m_fechaHoraCreacion = CDb.GetCellDGVDateTime(rowSelected, "FECHA_HORA"),
                    m_idCertSanitario = CDb.GetCellDGVString(rowSelected, "CERT_SANITARIO"),
                    m_activo = CDb.GetCellDGVBool(rowSelected, "ACTIVO"),
                    m_remitos = CDb.GetListRemitosOI(idOISelecting),
                    m_facturas = CDb.GetListFacturasOI(idOISelecting),
                    m_Operador = new COperador()
                    {
                        m_id = CDb.GetCellDGVInt(rowSelected, "IDOPERADOR"),
                        m_nombre = CDb.GetCellDGVString(rowSelected, "OPERADOR"),
                        m_pasw = CDb.GetCellDGVString(rowSelected, "PASW_OPERADOR"),
                        m_tipo = (TYPE_OPERATOR)CDb.GetCellDGVChar(rowSelected, "TIPO_OPERADOR")
                    },
                    m_proveedor = new CProveedorSAC()
                    {
                        Codigo = CDb.GetCellDGVString(rowSelected, "CODIGO_PROVEEDOR"),
                        Nombre = CDb.GetCellDGVString(rowSelected, "PROVEEDOR"),
                    }
                };
            }
        }
        private void dataGridView_OICreadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI)
            {
                if (e.RowIndex != -1)
                {
                    SelectingRowOI(dataGridView_OI.CurrentRow);
                    Close();
                }
            }
        }

        private void dataGridView_OICreadas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI)
            {
                if (e.KeyData == (Keys.Enter))
                {
                    if (dataGridView_OI.CurrentRow.Index != -1)
                    {
                        SelectingRowOI(dataGridView_OI.CurrentRow);
                        Close();
                    }
                }
            }
        }

        private void CargarDataGridView_PorIDOI(int idOI)
        {
            CargarDataGrid(m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI,idOI);
        }

        private void textBox_valorBuscar_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg("EDITAR EL NUMERO DE ORDEN DE INGRESO A BUSCAR", "NUMERO", textBox_valorBuscar.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_valorBuscar.Text = dlg.VALUE;
            }
        }

        private void textBox_valorBuscar_TextChanged(object sender, EventArgs e)
        {
            if (textBox_valorBuscar.Text != "")
            {
                CargarDataGridView_PorIDOI(Convert.ToInt32(textBox_valorBuscar.Text));
            }
            else
            {
                CargarDataGrid(m_tipoDLG == MODO_VIEW_OI_CREADAS_DLG.SELECCION_OI);
            }
        }

        private void textBox_numeric_KeyPress(object sender, KeyPressEventArgs e)
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

        private void trackBar_dgvOIs_Scroll(object sender, EventArgs e)
        {
            if (dataGridView_OI.SelectedRows.Count > 0 && (dataGridView_OI.SelectedRows[0].Index < dataGridView_OI.Rows.Count))
            {
                dataGridView_OI.CurrentCell = dataGridView_OI[0, TrackBarValue - 1];
            }
        }

        private void dataGridView_OI_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_OI.SelectedRows.Count > 0)
                {
                    TrackBarValue = dataGridView_OI.SelectedRows[0].Index + 1;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_OI.SelectedRows.Count > 0 && (dataGridView_OI.SelectedRows[0].Index < dataGridView_OI.Rows.Count - 1))
            {
                dataGridView_OI.CurrentCell = dataGridView_OI[0, dataGridView_OI.SelectedRows[0].Index + 1];
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_OI.SelectedRows.Count > 0 && dataGridView_OI.SelectedRows[0].Index > 0)
            {
                dataGridView_OI.CurrentCell = dataGridView_OI[0, dataGridView_OI.SelectedRows[0].Index - 1];
            }
        }
    }
}
