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

namespace ABM_DB
{
    public partial class CABM_DB : Form
    {
        DataSet dsTable;
        bool m_inModeNew = false;
        int m_idSelection = 0;
        int m_idSelected = 0;
        OleDbConnection m_oleDbConnection = new OleDbConnection();
        string m_nameTable;
        string m_nameRegister;
        string m_nameFieldID;
        string m_nameFieldFilterDataGrid;
        string m_nameFieldPrincipal;
        string m_sqlQuery_LoadDataGrid;
        string m_subQueryOrderBy_LoadDataGrid ="";
        bool m_selectLastRowAfterNew = true;


        //acceso PUBLICO a propiedades
        public int IdSelected
        {
            get { return m_idSelected;}
            set { m_idSelected = value;}
        }
        public OleDbConnection OleDbConnection { get => m_oleDbConnection; set => m_oleDbConnection = value; }
        public bool InModeNew
        {
            get { return m_inModeNew; }
            set
            {
                m_inModeNew = value;
                label_marcaNuevoRegistro.Visible = m_inModeNew; ;
            }
        }
        /// <summary>
        /// Nombre de Tabla que utiliza la clase base para realizar querys.
        /// </summary>
        public string NameTable { get => m_nameTable; set => m_nameTable = value; }
        /// <summary>
        /// Nombre del Campo ID que posee la tabla.
        /// </summary>
        public string NameFieldID { get => m_nameFieldID; set => m_nameFieldID = value; }
        /// <summary>
        /// Nombre que se le da a un registro por ejemplo en una tabla Productos el nombre
        /// del registro seria PRODUCTO
        /// </summary>
        public string NameRegister { get => m_nameRegister; set => m_nameRegister = value; }
        /// <summary>
        /// Nombre del campo que se utiliza para el filtrado del DataGrid
        /// </summary>
        public string NameFieldFilterDataGrid { get => m_nameFieldFilterDataGrid; set => m_nameFieldFilterDataGrid = value; }
        /// <summary>
        /// Texto para la etiqueta que se coloca por encima del DataGrid que define el contenido del mismo.
        /// </summary>
        public string LabelDataGrid { get => label_nameTableDataGrid.Text; set => label_nameTableDataGrid.Text = value; }
        /// <summary>
        /// Texto para la etiqueta que se coloca a la derecha del textbox de edicion de filtro del DataGrid.
        /// </summary>
        public string LabelTextBoxFilterDataGrid { get => label_textboxFilterDataGrid.Text; set => label_textboxFilterDataGrid.Text = value; }
        /// <summary>
        /// Query SQL que se utilizara para llenar el DataGrid con los registros
        /// </summary>
        public string SqlQuery_LoadDataGrid { get => m_sqlQuery_LoadDataGrid; set => m_sqlQuery_LoadDataGrid = value; }
        /// <summary>
        /// Subquery de tipo Order By que se anexa al final del query de carga del datagrid.
        /// ejemplo : "ORDER BY p.nombre ASC".
        /// </summary>
        public string SubQueryOrderBy_LoadDataGrid { get => m_subQueryOrderBy_LoadDataGrid; set => m_subQueryOrderBy_LoadDataGrid = value; }
        /// <summary>
        /// Indica si selecciona el primer o ultimo registro luego de la insercion de uno nuevo.
        /// </summary>
        public bool SelectLastRowAfterNew { get => m_selectLastRowAfterNew; set => m_selectLastRowAfterNew = value; }

        public delegate void DGVTableSelectionChanged(object sender, EventArgs e);
        //Evento que se dispara luego de una seleccion de un registro del datagrid.
        public event DGVTableSelectionChanged OnDGV_SelectionChanged;

        public delegate void DGVTableCellDoubleClick(object sender, DataGridViewCellEventArgs e);
        //Evento que se dispara luego de un doble clic en una celda del datagrid.
        public event DGVTableCellDoubleClick OnDGV_CellDoubleClick;

        public delegate void DGVTableCellClick(object sender, DataGridViewCellEventArgs e);
        //Evento que se dispara luego de un clic en una celda del datagrid.
        public event DGVTableCellDoubleClick OnDGV_CellClick;

        public TextBox TextBoxEditFilterDGV { get => textBox_filtroDataGrid; }
        //Nombre del campo principal en el Query que carga el datagrid. 
        public string NameFieldPrincipal { get => m_nameFieldPrincipal; set => m_nameFieldPrincipal = value; }

        public CABM_DB()
        {
            InitializeComponent();
            SetDGVDoubleBuffer(dataGridView_Table);
            InModeNew = false;
        }

        public CABM_DB(OleDbConnection oleDbConnection, int idSelection = 0)
        {
            InitializeComponent();
            InModeNew = false;
            m_oleDbConnection = oleDbConnection;
            m_idSelection = idSelection;
        }

        private void CABM_Paises_Load(object sender, EventArgs e)
        {
            CargarDataGrid();
            SelectItemDataGrid(m_idSelection);
        }

        private void SetDGVDoubleBuffer(DataGridView dgv)
        {
            //Set Double buffering on the Grid using reflection and the bindingflags enum.
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            dgv, new object[] { true });
        }

        public void CargarDataGrid(string filterBusqueda = "")
        {
            string strQuery = "";
            string subQueryFilterBusqueda = "";

            if (filterBusqueda != "")
            {
                subQueryFilterBusqueda = String.Format(" AND {0} like '%{1}%' ",NameFieldFilterDataGrid,filterBusqueda);
            }
            strQuery = String.Format(" {0} {1} {2}", SqlQuery_LoadDataGrid,subQueryFilterBusqueda,m_subQueryOrderBy_LoadDataGrid);
            LoadDataGrid(strQuery);
        }

        private void LoadDataGrid(string strQuery)
        {
            try
            {
                int rowsFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //creo el data set que contendra la tabla de cajas y la de pesadas relacionadas
                    dsTable = new DataSet();
                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(strQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsFills = oleDbDataAdapter.Fill(dsTable, NameTable);
                    if (rowsFills != 0)
                    {
                        /*
                        Antes de realizar el binding con el datagrid es conveniente desabilitar el
                        AutoSizeRowsMode,AutoSizeColumnsMode y ColumnHeadersHeightSizeMode
                        para ganar velocidad en el proceso de binding.
                        */

                        dataGridView_Table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        dataGridView_Table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dataGridView_Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


                        dataGridView_Table.DataSource = dsTable;
                        dataGridView_Table.DataMember = NameTable;
                        SetStateVisibleColumnsDGV();
                        /*
                        HAbilito el ColumnHeadersHeightSizeMode dado que estar realizado el binding
                        */
                        dataGridView_Table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                        PostLoadDataGrid();
                    }
                    else
                    {
                        dataGridView_Table.Columns.Clear();
                        dataGridView_Table.DataSource = null;
                        ClrControls();
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

        public virtual void SetStateVisibleColumnsDGV()
        {
            dataGridView_Table.Columns[NameFieldID].Visible = false;
        }

        private bool BorrarRegistro(int id)
        {
            bool borrado = false;
            int regAfectados;
 
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(GetQueryDeleteRegister(), m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                borrado = (regAfectados == 1);
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return borrado;
        }

        private bool BorrarRegistroSeleccionado()
        {
            bool registroBorradoOk = false;
            try
            {
                if (dataGridView_Table.SelectedRows.Count > 0)
                {
                    if (ValidateDelete())
                    {
                        int idxSelectRow = dataGridView_Table.SelectedRows[0].Index;
                        int id = Convert.ToInt32(dataGridView_Table.Rows[idxSelectRow].Cells[m_nameFieldID].Value);
                        string nombreCampo = dataGridView_Table.Rows[idxSelectRow].Cells[m_nameFieldPrincipal].Value.ToString();
                        string aviso = String.Format("Usted esta a punto de Eliminar un registro , confirma la eliminacion de un {0} {1} = {2} NOMBRE: {3}?", m_nameRegister, m_nameFieldID, id, nombreCampo);
                        if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (BorrarRegistro(id))
                            {
                                registroBorradoOk = true;
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el registro desde la base de datos", "Error Borrando Registro en base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando un Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registroBorradoOk;
        }

        public virtual void ClrControls()
        {
            textBox_ID.Text = "";
        }

        private void dataGridView_Table_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = !BorrarRegistroSeleccionado();
        }


        private void button_New_Click(object sender, EventArgs e)
        {
            if (ValidateNew())
            {
                ClrControls();
                textBox_ID.Text = GetNuevoIdAsignado().ToString();
                InModeNew = true;
            }
        }

        private void button_Actualizar_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate())
            {
                if (ValidateControls())
                {
                    if (InModeNew)
                    {
                        if (InsertNewRegister())
                        {
                            CargarDataGrid();
                            SelectRowDataGridViewAfterNew();
                            dataGridView_Table.Focus();
                            InModeNew = false;
                        }
                    }
                    else
                    {
                        if (dataGridView_Table.Rows.Count > 0)
                        {
                            if (UpdateRegister())
                            {
                                int idxSelectRow = dataGridView_Table.SelectedRows[0].Index;
                                CargarDataGrid(textBox_filtroDataGrid.Text);
                                SelectDataGridView_Registro(idxSelectRow);
                            }
                        }
                    }
                }
            }
        }

        private void SelectRowDataGridViewAfterNew()
        {
            if (dataGridView_Table.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(m_selectLastRowAfterNew? (dataGridView_Table.Rows.Count - 1) : 0);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Table.SelectedRows.Count > 0)
            {
                dataGridView_Table.CurrentCell = dataGridView_Table[0, idxRow];
            }
        }
        public void SelectItemDataGrid(int idSelection)
        {
            if (idSelection != 0)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView_Table.Rows)
                {
                    if (row.Cells[m_nameFieldID].Value != null) // Need to check for null if new row is exposed
                    {
                        if (row.Cells[m_nameFieldID].Value.ToString().Equals(idSelection.ToString()))
                        {
                            rowIndex = row.Index;
                            dataGridView_Table.CurrentCell = dataGridView_Table.Rows[rowIndex].Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        //Metodos Virtuales , la clase padre debe implementarlos 
        public virtual void PostLoadDataGrid()
        { }
        public virtual bool ValidateUpdate()
        { return true; }
        public virtual bool ValidateNew()
        { return true; }
        public virtual bool ValidateDelete()
        { return true; }
        public virtual bool ValidateControls()
        { return false; }
        public virtual string GetQueryInsertRegister()
        { return ""; }
        public virtual string GetQueryUpdateRegister()
        { return ""; }
        public virtual string GetQueryDeleteRegister()
        { return ""; }

        public bool InsertNewRegister()
        {
            bool registracionOk = false;
            int regAfectados;
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(GetQueryInsertRegister(), m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                if (regAfectados == 1)
                {
                    registracionOk = true;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Insertando nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registracionOk;
        }

        private bool UpdateRegister()
        {
            bool actualizadoOk = false;
            int regAfectados;
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(GetQueryUpdateRegister(),m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                if (regAfectados == 1)
                {
                    actualizadoOk = true;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Actualizando nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return actualizadoOk;
        }


        private int GetNuevoIdAsignado()
        {
            int newId = 0;
            OleDbDataReader recordSet;
            try
            {
                string strCmd = String.Format(" SELECT MAX({0}) AS id FROM {1}",m_nameFieldID,m_nameTable);

                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    if (recordSet.Read())
                    {
                        newId = GetCampoDbInt(recordSet, "id", 0)+1;
                    }
                }
                recordSet.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Obteniendo el Id del nuevo Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return newId;
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            if (!InModeNew)
            {
                if(BorrarRegistroSeleccionado())
                    CargarDataGrid();
            }
        }

        private void dataGridView_Table_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Table.SelectedRows.Count > 0)
                {
                    m_idSelected = Convert.ToInt32(dataGridView_Table.SelectedRows[0].Cells[m_nameFieldID].Value);
                    textBox_ID.Text = dataGridView_Table.SelectedRows[0].Cells[m_nameFieldID].Value.ToString();

                    OnDGV_SelectionChanged?.Invoke(sender, e);

                    InModeNew = false;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_Table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                m_idSelected = Convert.ToInt32(dataGridView_Table.Rows[e.RowIndex].Cells[m_nameFieldID].Value);
                OnDGV_CellDoubleClick?.Invoke(sender, e);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBox_filtroDataGrid_TextChanged(object sender, EventArgs e)
        {
            CargarDataGrid(textBox_filtroDataGrid.Text);
        }

        public int GetCampoDbInt(OleDbDataReader recordset, string nombreCampo, int valDefault = 0)
        {
            int valor = valDefault;
            try
            {
                int idxColumna = recordset.GetOrdinal(nombreCampo);
                if (recordset[idxColumna] != DBNull.Value)
                    valor = recordset.GetInt32(idxColumna);
            }
            catch (OleDbException e)
            {
            }
            return valor;
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_Table.SelectedRows.Count > 0 && dataGridView_Table.SelectedRows[0].Index > 0)
            {
                dataGridView_Table.CurrentCell = dataGridView_Table[0, dataGridView_Table.SelectedRows[0].Index - 1];
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {

            if (dataGridView_Table.SelectedRows.Count > 0 && (dataGridView_Table.SelectedRows[0].Index < dataGridView_Table.Rows.Count - 1))
            {
                dataGridView_Table.CurrentCell = dataGridView_Table[0, dataGridView_Table.SelectedRows[0].Index + 1];
            }
        }

        private void dataGridView_Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                OnDGV_CellClick?.Invoke(sender, e);
            }
        }
    }
}
