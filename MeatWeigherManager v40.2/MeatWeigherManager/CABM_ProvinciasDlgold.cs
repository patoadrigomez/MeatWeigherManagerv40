using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Db;

namespace MeatWeigherManager
{
    public partial class CABM_ProvinciasDlgold : Form
    {
        private OleDbConnection m_oleDbConnection;
        protected OleDbDataAdapter m_oleDbDataAdapter;
        CDb m_dbManagem;
        DataSet dsProvincias;
        bool m_inModeNew = false;
        int m_idSelected = 0;

        public int IdSelected
        {
            get { return m_idSelected;}
            set { m_idSelected = value;}
        }

        public bool InModeNew
        {
            get { return m_inModeNew; }
            set
            {
                m_inModeNew = value;
                label_marcaNuevoRegistro.Visible = m_inModeNew; ;
            }
        }

        public CABM_ProvinciasDlgold(CDb dbManager)
        {
            m_dbManagem = dbManager;
            m_oleDbConnection = dbManager.m_oleDbConnection;
            InitializeComponent();
            InModeNew = false;
        }

        private void CABM_Provincias_Load(object sender, EventArgs e)
        {
            LoadComboBoxs();
            CargarDataGrid();
        }

        private void LoadComboBoxs()
        {
            m_dbManagem.FillComboBox(comboBox_paises, "SELECT id , nombre AS DESCRIPCION FROM Paises");
        }
        
        private void CargarDataGrid()
        {
            //genero el query que obtiene la info de pedidos
            string strQuery = " SELECT pr.id as ID, pr.nombre as NOMBRE ,p.nombre as PAIS,pr.idpais as IDPAIS "+
                              " FROM Provincias AS pr LEFT OUTER JOIN Paises AS p ON pr.idpais = p.id ";
            CargarDataGrid(strQuery);
        }        

        private void CargarDataGrid(string strQuery)
        {
            try
            {
                int rowsChoferesFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //creo el data set que contendra la tabla de cajas y la de pesadas relacionadas
                    dsProvincias = new DataSet();
                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    m_oleDbDataAdapter = new OleDbDataAdapter(strQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsChoferesFills = m_oleDbDataAdapter.Fill(dsProvincias, "Provincias");
                    if (rowsChoferesFills != 0)
                    {
                        dataGridView_Provincias.DataSource = dsProvincias;
                        dataGridView_Provincias.DataMember = "Provincias";
                        dataGridView_Provincias.Columns["IDPAIS"].Visible = false;
                    }
                    else
                    {
                        dataGridView_Provincias.DataSource = null;
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

        private bool BorrarRegistro(int idLocalidad)
        {
            bool borrado = false;
            int regAfectados;
 
            string strCmd = String.Format(" DELETE Provincias WHERE id = {0}",idLocalidad);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
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
                if (dataGridView_Provincias.SelectedRows.Count > 0)
                {
                    int idxSelectRow = dataGridView_Provincias.SelectedRows[0].Index;
                    int id = Convert.ToInt32(dataGridView_Provincias.Rows[idxSelectRow].Cells["ID"].Value);
                    string nombre = dataGridView_Provincias.Rows[idxSelectRow].Cells["NOMBRE"].Value.ToString();
                    string aviso = String.Format("Usted esta a punto de Eliminar un registro , confirma la eliminacion de una provincia ID = {0} NOMBRE: {1}?", id, nombre);
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
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Borrando un Registro de la Grilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return registroBorradoOk;
        }

        private void ClrControles()
        {
            textBox_ID.Text = "";
            textBox_nombre.Text = "";
            comboBox_paises.SelectedIndex = -1;
        }

        private void dataGridView_Provincias_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = !BorrarRegistroSeleccionado();
        }

        private bool ValidarControles()
        {
            bool validadoOk = false;
            if (textBox_nombre.Text == "")
            {
                MessageBox.Show("No ha editado el campo Nombre", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox_paises.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado una Provincia", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                validadoOk = true;
            return validadoOk;
        }

        private void button_Nuevo_Click(object sender, EventArgs e)
        {
            ClrControles();
            textBox_ID.Text = GetNuevoIdAsignado().ToString();
            InModeNew = true;
        }

        private void button_Actualizar_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                if (InModeNew)
                {
                    if (InsertNuevoRegistro())
                    {
                        CargarDataGrid();
                        SelectDataGridView_UltimoRegistro();
                        dataGridView_Provincias.Focus();
                        InModeNew = false;
                    }
                }
                else
                {
                    if (ActualizarRegistro())
                    {
                        int idxSelectRow = dataGridView_Provincias.SelectedRows[0].Index;
                        CargarDataGrid();
                        SelectDataGridView_Registro(idxSelectRow);
                    }
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_Provincias.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_Provincias.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Provincias.SelectedRows.Count > 0)
            {
                dataGridView_Provincias.CurrentCell = dataGridView_Provincias[0, idxRow];
            }
        }

        private bool InsertNuevoRegistro()
        {
            bool registracionOk = false;
            int regAfectados;
            try
            {
                string strCmd = String.Format(" INSERT INTO PROVINCIAS(NOMBRE,IDPAIS) " +
                                              " VALUES('{0}',{1})",textBox_nombre.Text,
                                          ((CItemCBoxTable)comboBox_paises.Items[comboBox_paises.SelectedIndex]).Id);

                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
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

        private bool ActualizarRegistro()
        {
            bool actualizadoOk = false;
            int regAfectados;
            string strCmd = String.Format(" UPDATE PROVINCIAS SET nombre = '{1}',idPais = {2} WHERE ID = {0}",
                                          textBox_ID.Text,
                                          textBox_nombre.Text,
                                          ((CItemCBoxTable)comboBox_paises.Items[comboBox_paises.SelectedIndex]).Id);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
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
                string strCmd = " SELECT MAX(id) AS id FROM provincias ";

                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    if (recordSet.Read())
                    {
                        newId = m_dbManagem.GetCampoDbInt(recordSet, "id", 0);
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
                BorrarRegistroSeleccionado();
                CargarDataGrid();
            }
        }

        private void dataGridView_Provincias_SelectionChanged(object sender, EventArgs e)
        {
            int idComboBox;
            try
            {
                if (dataGridView_Provincias.SelectedRows.Count > 0)
                {
                    m_idSelected = Convert.ToInt32(dataGridView_Provincias.SelectedRows[0].Cells["ID"].Value);
                    textBox_ID.Text = dataGridView_Provincias.SelectedRows[0].Cells["ID"].Value.ToString();
                    textBox_nombre.Text = dataGridView_Provincias.SelectedRows[0].Cells["NOMBRE"].Value.ToString();
                    idComboBox = -1;
                    if (dataGridView_Provincias.SelectedRows[0].Cells["IDPAIS"].Value != DBNull.Value)
                    {
                        idComboBox = Convert.ToInt32(dataGridView_Provincias.SelectedRows[0].Cells["IDPAIS"].Value);
                    }
                    m_dbManagem.SelectItemComboDb(comboBox_paises, idComboBox);
                    InModeNew = false;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_Provincias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                m_idSelected = Convert.ToInt32(dataGridView_Provincias.Rows[e.RowIndex].Cells["ID"].Value);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void contextMenuStrip_comboBox_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (((ContextMenuStrip)sender).SourceControl == comboBox_paises)
            {
                if (e.ClickedItem.Text == "Agregar")
                {
                    CABM_PaisesDlg abmDlg = new CABM_PaisesDlg(m_dbManagem);
                    if (abmDlg.ShowDialog() == DialogResult.OK)
                    {
                        m_dbManagem.FillComboBox(comboBox_paises, "SELECT id as ID, nombre AS DESCRIPCION FROM Paises");
                        m_dbManagem.SelectItemComboDb(comboBox_paises, abmDlg.IdSelected);
                    }
                }
            }
        }
    }
}
