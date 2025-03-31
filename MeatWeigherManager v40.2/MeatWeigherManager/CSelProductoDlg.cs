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
using ConfigApp;
using Db;
using EditStringTouchDlg;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CSelProductoDlg : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;
        DataSet dsProductos;
        int m_idSelected = 0;
        
        int m_idProductSelection = 0;
        int m_idTipoProductSelection = 0;


        CProducto m_ProductoSeleted;
        bool m_filtrarSoloProductosCombo;
        bool m_filtrarSoloProductosCaja;


        public int IdSelected
        {
            get { return m_idSelected;}
            set { m_idSelected = value;}
        }

        public CProducto ProductoSelected
        {
            get { return m_ProductoSeleted; }
            set { m_ProductoSeleted = value; }
        }

        public int TrackBarValue
        {
            get { return trackBar_dgvProductos.Maximum - trackBar_dgvProductos.Value + trackBar_dgvProductos.Minimum; }
            set { trackBar_dgvProductos.Value =  trackBar_dgvProductos.Maximum - value + trackBar_dgvProductos.Minimum; }
        }

        public bool FiltrarSoloProductosCombo { get => m_filtrarSoloProductosCombo; set => m_filtrarSoloProductosCombo = value; }
        public bool FiltrarSoloProductosCaja { get => m_filtrarSoloProductosCaja; set => m_filtrarSoloProductosCaja = value; }

        /// <summary>
        /// Clase de Visualizacion y seleccion de un articulo.
        /// </summary>
        /// <param name="idProductSelection"> id del articulo a seleccionar una vez visualizada la grilla.</param>
        /// <param name="conditionFilterProducts">condicion del query sql que filtra registros ej: "AND pesable == 0 "</param>
        public CSelProductoDlg(int idProductSelection = 0, int idTipoProductSelection = 0, bool filtrarSoloProductosCombo=false,bool filtrarSoloProductosCaja = false)
        {
            FiltrarSoloProductosCombo = filtrarSoloProductosCombo;
            FiltrarSoloProductosCaja = filtrarSoloProductosCaja;
            m_idProductSelection = idProductSelection;
            m_idTipoProductSelection = idTipoProductSelection;
            m_ProductoSeleted = new CProducto();
            InitializeComponent();
        }

        private void CABM_Productos_Load(object sender, EventArgs e)
        {
            CargarDataGrid();
            LoadComboBoxTipoProducto(m_idTipoProductSelection);
            SelectItemDataGrid(m_idProductSelection);
        }

        private void LoadComboBoxTipoProducto(int idSelection = 0)
        {
            CDb.FillComboBox(comboBox_filtroPorTipo, "SELECT id,nombre AS DESCRIPCION FROM TiposProducto");
            comboBox_filtroPorTipo.Items.Add(new CItemCBoxTable(0, "TODOS"));
            CDb.SelectItemComboDb(comboBox_filtroPorTipo, idSelection);
        }

        private void CargarDataGrid(string filterForName = "",int filterForIdTypePrd=0 )
        {
            string subQueryFilter = "";

            if (filterForName != "")
            {
                subQueryFilter = String.Format(" AND p.nombre like '%{0}%' ", filterForName);
            }
            if(filterForIdTypePrd != 0)
                subQueryFilter += String.Format(" AND p.idtipo = {0} ",filterForIdTypePrd);

            //genero el query que obtiene la info de productos
            string strQuery = String.Format(
                " declare @tmpprdSAC as table (codigo varchar(20), nombre varchar(50),alias varchar(20) ) " +
                " INSERT INTO @tmpprdSAC Exec sp_getProductosSAC " +
                " SELECT " +
                " p.codigoProductoSAC as COD_SAC," +
                " pk.alias as ALIAS_SAC," +
                " pk.nombre as DENOMI_SAC, " +
                " p.id as ID,p.nombre as NOMBRE, " +
                " tp.id as IDTIPO," +
                " tp.nombre as TIPO," +
                " p.numSenasa as NUMSENASA," +
                " p.pesonetopredef as NETO_PRE," +
                " p.pesotarapredef as TARA_PRE," +
                " p.unidadesPredef as UNDS_PRE," +
                " p.rendimientoSTD as REND," +
                " p.diasvencimiento as VENC," +
                " (CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS, " +
                " (CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES, "+
                " (CASE WHEN p.estropa = 0 OR p.estropa is null THEN 'NO' ELSE 'SI' END) as TROPA, " +
                " p.esinsumo as ESINSUMO," +
                " p.espesable as ESPESABLE," +
                " p.esTropa as ESTROPA," +
                " p.nombreL1 as NOMBRE_L1," +
                " p.nombreL2 as NOMBRE_L2," +
                " p.nombreL3 as NOMBRE_L3," +
                " p.nombreL4 as NOMBRE_L4," +
                " p.nombreL5 as NOMBRE_L5," +
                " p.nombreL6 as NOMBRE_L6," +
                " p.textauxl1 as TEXTAUXL1," +
                " p.textauxl2 as TEXTAUXL2, " +
                " e.id as IDETIQUETA," +
                " e.nombre as NOMBRE_ETIQUETA," +
                " e.idtipobulto as IDTIPOBULTO_ETIQUETA," +
                " e.descripcion as DESCRIPCION_ETIQUETA " +
                " FROM Productos p " +
                " LEFT OUTER JOIN @tmpprdSAC as pk ON pk.codigo = p.codigoProductoSAC " +
                " LEFT OUTER JOIN Etiquetas as e ON p.idEtiqueta=e.id " +
                " LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id WHERE 1=1 {0} {1}",
                  FiltrarSoloProductosCombo?" AND p.escombo=1 " : FiltrarSoloProductosCaja ? " AND p.escaja=1 " : " AND (p.escombo is null or p.escombo=0) AND (p.escaja is null or p.escaja=0)",
                  subQueryFilter);
            LoadDataGrid(strQuery);
        }        

        private void LoadDataGrid(string strQuery)
        {
            try
            {
                int rowsChoferesFills = 0;

                if (CDb.m_oleDbConnection.State == ConnectionState.Open)
                {
                    //creo el data set que contendra la tabla de cajas y la de pesadas relacionadas
                    dsProductos = new DataSet();
                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    m_oleDbDataAdapter = new OleDbDataAdapter(strQuery, CDb.m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsChoferesFills = m_oleDbDataAdapter.Fill(dsProductos, "Productos");
                    if (rowsChoferesFills != 0)
                    {

                        dataGridView_Productos.DataSource = dsProductos;
                        dataGridView_Productos.DataMember = "Productos";
                        dataGridView_Productos.Columns["ID"].Visible = false;
                        dataGridView_Productos.Columns["IDTIPO"].Visible = false;
                        dataGridView_Productos.Columns["ESPESABLE"].Visible = false;
                        dataGridView_Productos.Columns["ESINSUMO"].Visible = false;
                        dataGridView_Productos.Columns["ESTROPA"].Visible = false;
                        dataGridView_Productos.Columns["NUMSENASA"].Visible = false;
                        dataGridView_Productos.Columns["NOMBRE_L1"].Visible = false;
                        dataGridView_Productos.Columns["NOMBRE_L2"].Visible = false;
                        dataGridView_Productos.Columns["NOMBRE_L3"].Visible = false;
                        dataGridView_Productos.Columns["NOMBRE_L4"].Visible = false;
                        dataGridView_Productos.Columns["NOMBRE_L5"].Visible = false;
                        dataGridView_Productos.Columns["NOMBRE_L6"].Visible = false;
                        dataGridView_Productos.Columns["TEXTAUXL1"].Visible = false;
                        dataGridView_Productos.Columns["TEXTAUXL2"].Visible = false;
                        dataGridView_Productos.Columns["DENOMI_SAC"].Visible = false;
                        dataGridView_Productos.Columns["PES"].Visible = false;
                        dataGridView_Productos.Columns["INS"].Visible = false;
                        dataGridView_Productos.Columns["NETO_PRE"].Visible = false;
                        dataGridView_Productos.Columns["TARA_PRE"].Visible = false;
                        dataGridView_Productos.Columns["UNDS_PRE"].Visible = false;
                        dataGridView_Productos.Columns["REND"].Visible = false;
                        dataGridView_Productos.Columns["ALIAS_SAC"].Visible = false;
                        dataGridView_Productos.Columns["IDETIQUETA"].Visible = false;
                        dataGridView_Productos.Columns["DESCRIPCION_ETIQUETA"].Visible = false;
                        dataGridView_Productos.Columns["IDTIPOBULTO_ETIQUETA"].Visible = false;


                        dataGridView_Productos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dataGridView_Productos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                        dataGridView_Productos.Columns["COD_SAC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["COD_SAC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Productos.Columns["NOMBRE"].MinimumWidth = 200;
                        dataGridView_Productos.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        //dataGridView_Productos.Columns["NOMBRE"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView_Productos.Columns["NOMBRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_Productos.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["TIPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dataGridView_Productos.Columns["TROPA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["TROPA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Productos.Columns["VENC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["VENC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Productos.Columns["NOMBRE_ETIQUETA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView_Productos.Columns["NOMBRE_ETIQUETA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView_Productos.Columns["NOMBRE_ETIQUETA"].HeaderText = "ETI";


                        trackBar_dgvProductos.Minimum = 1;
                        trackBar_dgvProductos.Maximum = dataGridView_Productos.RowCount;
                        TrackBarValue = 1;
                        /*
                        int dgv_width = dataGridView_Productos.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                        */
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para esta consulta", "Cargando datos en la grilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView_Productos.DataSource = null;
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

        private void SelectItemDataGrid(int idSelection)
        {
            if (idSelection != 0)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView_Productos.Rows)
                {
                    if (row.Cells["ID"].Value != null) // Need to check for null if new row is exposed
                    {
                        if (row.Cells["ID"].Value.ToString().Equals(idSelection.ToString()))
                        {
                            rowIndex = row.Index;
                            dataGridView_Productos.CurrentCell = dataGridView_Productos.Rows[rowIndex].Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_Productos.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_Productos.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0)
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, idxRow];
            }
        }

        private void dataGridView_Productos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Productos.SelectedRows.Count > 0)
                {
                    SelectingRowProducto(dataGridView_Productos.SelectedRows[0]);
                    TrackBarValue = dataGridView_Productos.SelectedRows[0].Index + 1;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectingRowProducto(DataGridViewRow rowSelected)
        {
            if (rowSelected != null)
            {
                IdSelected = Convert.ToInt32(rowSelected.Cells["ID"].Value);

                ProductoSelected = new CProducto()
                {
                    Id = CDb.GetCellDGVInt(rowSelected,"ID"),
                    Nombre = CDb.GetCellDGVString(rowSelected, "NOMBRE"),
                    m_tipo = new CTipoProducto()
                    {
                        Id = CDb.GetCellDGVInt(rowSelected, "IDTIPO"),
                        Nombre = CDb.GetCellDGVString(rowSelected, "TIPO"),
                    },
                    ProductoSAC = new CProductoSAC()
                    {
                        Codigo = CDb.GetCellDGVString(rowSelected, "COD_SAC"),
                        Nombre = CDb.GetCellDGVString(rowSelected, "DENOMI_SAC"),
                        Alias = CDb.GetCellDGVString(rowSelected, "ALIAS_SAC"),
                    },
                    CodSenasa = CDb.GetCellDGVString(rowSelected, "NUMSENASA"),
                    PesoNetoPredefinido = CDb.GetCellDGVFloat(rowSelected, "NETO_PRE"),
                    PesoTaraPredefinida = CDb.GetCellDGVFloat(rowSelected, "TARA_PRE"),
                    RendimientoSTD = CDb.GetCellDGVFloat(rowSelected, "REND"),
                    UnidadesPredefinidas = CDb.GetCellDGVInt(rowSelected, "UNDS_PRE"),
                    DiasVencimientoPredefinido = CDb.GetCellDGVInt(rowSelected, "VENC"),
                    EsInsumo = CDb.GetCellDGVBool(rowSelected, "ESINSUMO"),
                    EsPesable = CDb.GetCellDGVBool(rowSelected, "ESPESABLE"),
                    EsTropa = CDb.GetCellDGVBool(rowSelected, "ESTROPA"),
                    NombreEtiL1 = CDb.GetCellDGVString(rowSelected, "NOMBRE_L1"),
                    NombreEtiL2 = CDb.GetCellDGVString(rowSelected, "NOMBRE_L2"),
                    NombreEtiL3 = CDb.GetCellDGVString(rowSelected, "NOMBRE_L3"),
                    NombreEtiL4 = CDb.GetCellDGVString(rowSelected, "NOMBRE_L4"),
                    NombreEtiL5 = CDb.GetCellDGVString(rowSelected, "NOMBRE_L5"),
                    NombreEtiL6 = CDb.GetCellDGVString(rowSelected, "NOMBRE_L6"),
                    TextAuxEtiL1 = CDb.GetCellDGVString(rowSelected, "TEXTAUXL1"),
                    TextAuxEtiL2 = CDb.GetCellDGVString(rowSelected, "TEXTAUXL2"),
                    Etiqueta = new CEtiqueta()
                    {
                        Id = CDb.GetCellDGVInt(rowSelected, "IDETIQUETA"),
                        Nombre = CDb.GetCellDGVString(rowSelected, "NOMBRE_ETIQUETA"),
                        Descripcion = CDb.GetCellDGVString(rowSelected, "DESCRIPCION_ETIQUETA"),
                        IdTipoBulto = CDb.GetCellDGVString(rowSelected, "IDTIPOBULTO_ETIQUETA"),
                    }
                };
            }
        }

        private void dataGridView_Productos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SelectingRowProducto(dataGridView_Productos.CurrentRow);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBox_nombreBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarDataGrid(textBox_nombreBuscar.Text,GetIdTipoPrdSelectComboBox());
        }

        private void textBox_nombreBuscar_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("EDITAR PRODUCTO A BUSCAR", "Nombre o Aproximación", textBox_nombreBuscar.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_nombreBuscar.Text = dlg.VALUE;
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0 && dataGridView_Productos.SelectedRows[0].Index > 0)
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, dataGridView_Productos.SelectedRows[0].Index-1];
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0 && (dataGridView_Productos.SelectedRows[0].Index < dataGridView_Productos.Rows.Count-1))
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, dataGridView_Productos.SelectedRows[0].Index + 1];
            }
        }

        private void trackBar_dgvProductos_Scroll(object sender, EventArgs e)
        {
            if (dataGridView_Productos.SelectedRows.Count > 0 && (dataGridView_Productos.SelectedRows[0].Index < dataGridView_Productos.Rows.Count))
            {
                dataGridView_Productos.CurrentCell = dataGridView_Productos[0, TrackBarValue - 1];
            }
        }

        private void comboBox_filtroPoTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDataGrid(textBox_nombreBuscar.Text, GetIdTipoPrdSelectComboBox());
        }
        private int GetIdTipoPrdSelectComboBox()
        {
            int idSelect = 0;
            idSelect= CDb.GetSelectItemIdComboDb(comboBox_filtroPorTipo);
            return idSelect;
        }
    }
}
