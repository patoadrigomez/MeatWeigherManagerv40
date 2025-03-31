using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ABMDLG
{
    public partial class CABM : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;
        DataSet dsOperadores;
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

        public CABM()
        {
            InitializeComponent();
            InModeNew = false;
        }

        private void CABM_Load(object sender, EventArgs e)
        {
            CargarDataGrid();
        }

        private void CargarDataGrid()
        {
            //genero el query que obtiene la info de pedidos
            string strQuery = " SELECT id AS ID, nombre AS NOMBRE,tipo as TIPO,pasw AS PASSWORD  FROM Operadores "; 
            CargarDataGrid(strQuery);
        }        

        private void CargarDataGrid(string strQuery)
        {
            try
            {
                int rowsChoferesFills = 0;

                if (CDb.m_oleDbConnection.State == ConnectionState.Open)
                {
                    //creo el data set que contendra la tabla de cajas y la de pesadas relacionadas
                    dsOperadores = new DataSet();
                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    m_oleDbDataAdapter = new OleDbDataAdapter(strQuery, CDb.m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsChoferesFills = m_oleDbDataAdapter.Fill(dsOperadores, "Operadores");
                    if (rowsChoferesFills != 0)
                    {
                        dataGridView_Registers.DataSource = dsOperadores;
                        dataGridView_Registers.DataMember = "Operadores";
                        dataGridView_Registers.Columns["PASSWORD"].Visible = false;

                        int dgv_width = dataGridView_Registers.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                    }
                    else
                    {
                        MessageBox.Show("No hay datos para esta consulta", "Cargando datos en la grilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView_Registers.DataSource = null;
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

        private bool BorrarRegistro(int idOperador)
        {
            bool borrado = false;
            int regAfectados;
 
            string strCmd = String.Format(" DELETE Operadores WHERE id = {0}",idOperador);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, CDb.m_oleDbConnection);
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
                if (dataGridView_Registers.SelectedRows.Count > 0)
                {
                    int idxSelectRow = dataGridView_Registers.SelectedRows[0].Index;
                    int id = Convert.ToInt32(dataGridView_Registers.Rows[idxSelectRow].Cells["ID"].Value);
                    string nombre = dataGridView_Registers.Rows[idxSelectRow].Cells["NOMBRE"].Value.ToString();
                    string aviso = String.Format("Usted esta a punto de Eliminar un registro , confirma la eliminacion del Operador ID = {0} NOMBRE: {1}?", id, nombre);
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
            textBox_password.Text = "";
            textBox_repitePassword.Text = "";
            comboBox_TiposOperadores.SelectedIndex = -1;
        }

        private void dataGridView_Operadores_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
            else if (textBox_password.Text == "")
            {
                MessageBox.Show("No ha editado el Password", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox_repitePassword.Text == "" || (textBox_password.Text != textBox_repitePassword.Text))
            {
                MessageBox.Show("No ha editado la repeticion del Password o el valor no es el mismo", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox_TiposOperadores.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado el Tipo de Operador", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        dataGridView_Registers.Focus();
                        InModeNew = false;
                    }
                }
                else
                {
                    if (ActualizarRegistro())
                    {
                        int idxSelectRow = dataGridView_Registers.SelectedRows[0].Index;
                        CargarDataGrid();
                        SelectDataGridView_Registro(idxSelectRow);
                    }
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_Registers.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_Registers.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Registers.SelectedRows.Count > 0)
            {
                dataGridView_Registers.CurrentCell = dataGridView_Registers[0, idxRow];
            }
        }

        private bool InsertNuevoRegistro()
        {
            bool registracionOk = false;
            int regAfectados;
            try
            {
                string strCmd = String.Format(" INSERT INTO OPERADORES (nombre,tipo,pasw) " +
                                              " VALUES('{0}','{1}','{2}')",
                                              textBox_nombre.Text,
                                              comboBox_TiposOperadores.Items[comboBox_TiposOperadores.SelectedIndex].ToString(),
                                              textBox_password.Text);

                OleDbCommand dbCommand = new OleDbCommand(strCmd, CDb.m_oleDbConnection);
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
            string strCmd = String.Format(" UPDATE OPERADORES SET nombre = '{1}', tipo = '{2}',pasw = '{3}' WHERE ID = {0}",
                                            textBox_ID.Text,
                                            textBox_nombre.Text,
                                            comboBox_TiposOperadores.Items[comboBox_TiposOperadores.SelectedIndex].ToString(),
                                            textBox_password.Text);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, CDb.m_oleDbConnection);
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
                string strCmd = " SELECT MAX(id) AS id FROM operadores ";

                OleDbCommand dbCommand = new OleDbCommand(strCmd, CDb.m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    if (recordSet.Read())
                    {
                        newId = CDb.GetCampoDbInt(recordSet, "id", 0)+1;
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

        private void dataGridView_Operadores_SelectionChanged(object sender, EventArgs e)
        {
            int idComboBox;
            try
            {
                if (dataGridView_Registers.SelectedRows.Count > 0)
                {
                    m_idSelected = Convert.ToInt32(dataGridView_Registers.SelectedRows[0].Cells["ID"].Value);
                    textBox_ID.Text = dataGridView_Registers.SelectedRows[0].Cells["ID"].Value.ToString();
                    textBox_nombre.Text = dataGridView_Registers.SelectedRows[0].Cells["NOMBRE"].Value.ToString();
                    textBox_password.Text = dataGridView_Registers.SelectedRows[0].Cells["PASSWORD"].Value.ToString();
                    textBox_repitePassword.Text = textBox_password.Text; 

                    idComboBox = -1;
                    if (dataGridView_Registers.SelectedRows[0].Cells["TIPO"].Value != DBNull.Value)
                    {
                        TYPE_OPERATOR tipo = (TYPE_OPERATOR) Convert.ToChar(dataGridView_Registers.SelectedRows[0].Cells["TIPO"].Value);
                        if (tipo == TYPE_OPERATOR.SUPERVISOR)
                            idComboBox = 0;
                        else
                            idComboBox = 1;
                    }
                    comboBox_TiposOperadores.SelectedIndex = idComboBox;
                    InModeNew = false;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_Operadores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                m_idSelected = Convert.ToInt32(dataGridView_Registers.Rows[e.RowIndex].Cells["ID"].Value);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            textBox_repitePassword.Text = "";
        }

        private void textBox_nombre_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("EDITAR NOMBRE", "Nombre", textBox_nombre.Text,30);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_nombre.Text = dlg.VALUE;
            }
        }

        private void textBox_password_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("EDITAR PASSWORD", "Password",((TextBox)sender).Text,10,true);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = dlg.VALUE;
            }
        }
    }
}
