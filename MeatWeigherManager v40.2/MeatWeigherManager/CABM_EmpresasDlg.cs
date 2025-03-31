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
    public partial class CABM_ProveedoresDlg : Form
    {
        private OleDbConnection m_oleDbConnection;
        protected OleDbDataAdapter m_oleDbDataAdapter;
        CDb m_dbManagem;
        DataSet dsEmpresas;
        bool m_inModeNew = false;
        int m_idSelected = 0;
        int m_idSelection = 0;
        CEmpresa m_EmpresaSelected;
        BindingSource bsVehiculosVinculados;

        class CItemListBoxVehiculo
        {
            int m_id = 0;
            string m_patente = "";

            public int ID
            {
                get { return m_id; }
                set { m_id = value; }
            }
            public string PATENTE
            {
                get { return m_patente; }
                set { m_patente = value; }
            }

            public CItemListBoxVehiculo(int id, string patente)
            {
                m_id = id;
                m_patente = patente;
            }
            public override string ToString()
            {
                return m_patente;
            }
        }

        class CItemListBoxProducto
        {
            int m_id = 0;
            string m_nombre = "";

            public int ID
            {
                get { return m_id; }
                set { m_id = value; }
            }
            public string NOMBRE
            {
                get { return m_nombre; }
                set { m_nombre = value; }
            }

            public CItemListBoxProducto(int id, string nombre)
            {
                m_id = id;
                m_nombre = nombre;
            }
            public override string ToString()
            {
                return m_nombre;
            }
        }

        public int IdSelected
        {
            get { return m_idSelected;}
            set { m_idSelected = value;}
        }

        public CEmpresa EmpresaSelected
        {
            get { return m_EmpresaSelected; }
            set { m_EmpresaSelected = value; }
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

        public CABM_ProveedoresDlg(CDb dbManager, int idSelection = 0)
        {
            m_dbManagem = dbManager;
            m_oleDbConnection = dbManager.m_oleDbConnection;
            m_idSelection = idSelection;
            m_EmpresaSelected = new CEmpresa();
            InitializeComponent();
            InModeNew = false;
        }

        private void CABM_Empresas_Load(object sender, EventArgs e)
        {
            LoadComboTiposEmpresa();
            LoadListBoxProductos();
            LoadListBoxVehiculos();
            CargarDataGrid();
            SelectItemDataGrid(m_idSelection);
        }

        private void CargarDataGrid(string filter = "")
        {
            string subQueryFilter = "";

            if (filter != "")
            {
                subQueryFilter = String.Format(" WHERE a.nombre like '%{0}%' ", filter);
            }

            //genero el query que obtiene la info de empresass
            string strQuery = String.Format(" SELECT a.id as ID, a.nombre as NOMBRE ,a.cuit as CUIT,t.nombre as TIPO,t.id as ID_TIPO "+
                              " FROM Empresas AS a LEFT OUTER JOIN TIPOSEMPRESA as t ON a.idtipo = t.id {0}",subQueryFilter);
            LoadDataGrid(strQuery);
        }
        
        private void LoadComboTiposEmpresa()
        {
            comboBox_tipoEmpresa.DataSource = Enum.GetValues(typeof(TYPE_EMPRESA));
        }
        
        private void LoadListBoxVehiculos()
        {
            try
            {
                listBox_vehiculos.Items.Clear();

                DataTable dt = m_dbManagem.GetDataTableQuery("SELECT id , patente FROM VEHICULOS");
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                        listBox_vehiculos.Items.Add(new CItemListBoxVehiculo(int.Parse(row["id"].ToString()), row["patente"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Cargando el ListBox de Vehiculos: " + ex.Message,"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadListBoxVehiculosVinculados(int idEmpresa)
        {
            try
            {
                listBox_vehiculosVinculados.Items.Clear();

                string strQuery = String.Format("SELECT v.id as id, v.patente as patente FROM VEHICULOS as v,EMPRESAVEHICULOS as ev WHERE v.id = ev.idvehiculo AND ev.idempresa = {0} ", idEmpresa);
                DataTable dt = m_dbManagem.GetDataTableQuery(strQuery);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                        listBox_vehiculosVinculados.Items.Add(new CItemListBoxVehiculo(int.Parse(row["id"].ToString()), row["patente"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Cargando el ListBox de Vehiculos Vinculados: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListBoxProductos()
        {
            try
            {
                listBox_Productos.Items.Clear();

                DataTable dt = m_dbManagem.GetDataTableQuery("SELECT id , nombre FROM PRODUCTOS");
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                        listBox_Productos.Items.Add(new CItemListBoxProducto(int.Parse(row["id"].ToString()), row["nombre"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Cargando el ListBox de Productos: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListBoxProductosVinculados(int idEmpresa)
        {
            try
            {
                listBox_productosVinculados.Items.Clear();

                string strQuery = String.Format("SELECT p.id as id, p.nombre as nombre FROM PRODUCTOS as p,EMPRESAPRODUCTOS as ep WHERE p.id = ep.idproducto AND ep.idempresa = {0} ", idEmpresa);
                DataTable dt = m_dbManagem.GetDataTableQuery(strQuery);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                        listBox_productosVinculados.Items.Add(new CItemListBoxProducto(int.Parse(row["id"].ToString()), row["nombre"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Cargando el ListBox de Productos Vinculados: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataGrid(string strQuery)
        {
            try
            {
                int rowsChoferesFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //creo el data set que contendra la tabla de cajas y la de pesadas relacionadas
                    dsEmpresas = new DataSet();
                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    m_oleDbDataAdapter = new OleDbDataAdapter(strQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsChoferesFills = m_oleDbDataAdapter.Fill(dsEmpresas, "Empresas");
                    if (rowsChoferesFills != 0)
                    {
                        dataGridView_Empresas.DataSource = dsEmpresas;
                        dataGridView_Empresas.DataMember = "Empresas";
                        dataGridView_Empresas.Columns["ID_TIPO"].Visible = false;
                        int dgv_width = dataGridView_Empresas.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                        if (dgv_width > this.Width) this.Width = dgv_width + 100;
                    }
                    else
                    {
                        dataGridView_Empresas.DataSource = null;
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
                foreach (DataGridViewRow row in dataGridView_Empresas.Rows)
                {
                    if (row.Cells["ID"].Value != null) // Need to check for null if new row is exposed
                    {
                        if (row.Cells["ID"].Value.ToString().Equals(idSelection.ToString()))
                        {
                            rowIndex = row.Index;
                            dataGridView_Empresas.CurrentCell = dataGridView_Empresas.Rows[rowIndex].Cells[0];
                            break;
                        }
                    }
                }
            }
        }
        private bool BorrarEmpresaPermitida(int idEmpresa)
        {
            return !m_dbManagem.ExisteEmpresaEnOperacionesPesaje(idEmpresa);
        }

        private bool BorrarRegistroEmpresa(int idEmpresa)
        {
            bool borrado = false;
            int regAfectados;
 
            string strCmd = String.Format(" DELETE Empresas WHERE id = {0}",idEmpresa);
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
        private bool BorrarRegistrosVehiculosVinculadosEmpresa(int idEmpresa)
        {
            bool borrado = false;
            int regAfectados;

            string strCmd = String.Format(" DELETE EmpresaVehiculos WHERE idempresa = {0}", idEmpresa);
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

        private bool BorrarRegistrosProductosVinculadosEmpresa(int idEmpresa)
        {
            bool borrado = false;
            int regAfectados;

            string strCmd = String.Format(" DELETE EmpresaProductos WHERE idempresa = {0}", idEmpresa);
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
                if (dataGridView_Empresas.SelectedRows.Count > 0)
                {
                    int idxSelectRow = dataGridView_Empresas.SelectedRows[0].Index;
                    int id = Convert.ToInt32(dataGridView_Empresas.Rows[idxSelectRow].Cells["ID"].Value);
                    string nombre = dataGridView_Empresas.Rows[idxSelectRow].Cells["NOMBRE"].Value.ToString();
                    string aviso = String.Format("Usted esta a punto de Eliminar un registro , confirma la eliminacion de un EMPRESA con ID = {0} NOMBRE: {1}?", id, nombre);
                    if (MessageBox.Show(aviso, "CONFIRMACION DE BORRADO DE DATOS",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (BorrarEmpresaPermitida(id))
                        {
                            if (BorrarRegistroEmpresa(id))
                            {
                                BorrarRegistrosVehiculosVinculadosEmpresa(id);
                                BorrarRegistrosProductosVinculadosEmpresa(id);
                                registroBorradoOk = true;
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el registro desde la base de datos", "Error Borrando Registro en base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la Empresa , ya posee operaciones de pesaje realizadas", "Validación de Borrado de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            textBox_cuit.Text = "";
            comboBox_tipoEmpresa.SelectedIndex = -1;
            listBox_vehiculosVinculados.Items.Clear();
            listBox_productosVinculados.Items.Clear();
        }

        private void dataGridView_Empresas_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
            else if (textBox_cuit.Text == "")
            {
                MessageBox.Show("No ha editado el campo Cuit", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBox_tipoEmpresa.SelectedIndex == -1)
            {
                MessageBox.Show("No ha seleccionado el Tipo de Empresa", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (InsertNuevoRegistroEmpresa())
                    {
                        int newIdEmpresa = GetUltimoIdAsignado();
                        InsertRelacionesEmpresaVehiculos(newIdEmpresa);
                        InsertRelacionesEmpresaProductos(newIdEmpresa);
                        CargarDataGrid();
                        SelectDataGridView_UltimoRegistro();
                        dataGridView_Empresas.Focus();
                        InModeNew = false;
                    }
                }
                else
                {
                    if (ActualizarRegistro())
                    {
                        int idxSelectRow = dataGridView_Empresas.SelectedRows[0].Index;
                        InsertRelacionesEmpresaVehiculos(int.Parse(textBox_ID.Text));
                        InsertRelacionesEmpresaProductos(int.Parse(textBox_ID.Text));
                        CargarDataGrid();
                        SelectDataGridView_Registro(idxSelectRow);
                    }
                }
            }
        }

        private void SelectDataGridView_UltimoRegistro()
        {
            if (dataGridView_Empresas.SelectedRows.Count > 0)
            {
                SelectDataGridView_Registro(dataGridView_Empresas.Rows.Count - 1);
            }
        }
        
        private void SelectDataGridView_Registro(int idxRow)
        {
            if (dataGridView_Empresas.SelectedRows.Count > 0)
            {
                dataGridView_Empresas.CurrentCell = dataGridView_Empresas[0, idxRow];
            }
        }

        private bool InsertNuevoRegistroEmpresa()
        {
            bool registracionOk = false;
            int regAfectados;
            try
            {
                string strCmd = String.Format(" INSERT INTO EMPRESAS (NOMBRE,CUIT,IDTIPO) " +
                                              " VALUES('{0}','{1}',{2})", textBox_nombre.Text,textBox_cuit.Text,
                                              comboBox_tipoEmpresa.SelectedIndex + 1);

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

        private bool InsertRelacionesEmpresaVehiculos(int idEmpresa)
        {
            bool registracionOk = false;
            int idvehiculo;
            BorrarRegistrosVehiculosVinculadosEmpresa(idEmpresa);
            if (listBox_vehiculosVinculados.Items.Count > 0)
            {
                foreach (CItemListBoxVehiculo item in listBox_vehiculosVinculados.Items)
                {
                    idvehiculo = item.ID;
                    InsertRelacionEmpresaVehiculo(idEmpresa, idvehiculo);
                }
            }
            return registracionOk;
        }

        private bool InsertRelacionEmpresaVehiculo(int idEmpresa, int idVehiculo)
        {
            bool registracionOk = false;
            int regAfectados;
            try
            {
                string strCmd = String.Format(" INSERT INTO EMPRESAVEHICULOS (IDEMPRESA,IDVEHICULO) " +
                                              " VALUES({0},{1})", idEmpresa,idVehiculo);

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

        private bool InsertRelacionesEmpresaProductos(int idEmpresa)
        {
            bool registracionOk = false;
            int idproducto;
            BorrarRegistrosProductosVinculadosEmpresa(idEmpresa);
            if (listBox_productosVinculados.Items.Count > 0)
            {
                foreach (CItemListBoxProducto item in listBox_productosVinculados.Items)
                {
                    idproducto = item.ID;
                    InsertRelacionEmpresaProducto(idEmpresa, idproducto);
                }
            }
            return registracionOk;
        }
        private bool InsertRelacionEmpresaProducto(int idEmpresa, int idProducto)
        {
            bool registracionOk = false;
            int regAfectados;
            try
            {
                string strCmd = String.Format(" INSERT INTO EMPRESAPRODUCTOS (IDEMPRESA,IDPRODUCTO) " +
                                              " VALUES({0},{1})", idEmpresa, idProducto);

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
            string strCmd = String.Format(" UPDATE EMPRESAS SET nombre = '{1}',cuit = '{2}',idTipo = {3} WHERE ID = {0}",
                                          textBox_ID.Text, textBox_nombre.Text, textBox_cuit.Text,
                                          comboBox_tipoEmpresa.SelectedIndex + 1);
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
            return GetUltimoIdAsignado()+1;
        }

        private int GetUltimoIdAsignado()
        {
            int newId = 0;
            OleDbDataReader recordSet;
            try
            {
                string strCmd = " SELECT MAX(id) AS id FROM Empresas ";

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

        private void dataGridView_Empresas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Empresas.SelectedRows.Count > 0)
                {
                    m_idSelected = Convert.ToInt32(dataGridView_Empresas.SelectedRows[0].Cells["ID"].Value);
                    m_EmpresaSelected.m_id = m_idSelected;
                    m_EmpresaSelected.m_nombre = dataGridView_Empresas.SelectedRows[0].Cells["NOMBRE"].Value.ToString();
                    m_EmpresaSelected.m_cuit = dataGridView_Empresas.SelectedRows[0].Cells["CUIT"].Value.ToString();
                    m_EmpresaSelected.m_Tipo.m_id = Convert.ToInt32(dataGridView_Empresas.SelectedRows[0].Cells["ID_TIPO"].Value);
                    m_EmpresaSelected.m_Tipo.m_nombre = dataGridView_Empresas.SelectedRows[0].Cells["TIPO"].Value.ToString();

                    textBox_ID.Text = m_EmpresaSelected.m_id.ToString();
                    textBox_nombre.Text = m_EmpresaSelected.m_nombre;
                    textBox_cuit.Text = m_EmpresaSelected.m_cuit;
                    comboBox_tipoEmpresa.SelectedIndex = m_EmpresaSelected.m_Tipo.m_id-1;
                    LoadListBoxVehiculosVinculados(m_idSelected);
                    LoadListBoxProductosVinculados(m_idSelected);
                    InModeNew = false;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_Empresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                m_idSelected = Convert.ToInt32(dataGridView_Empresas.Rows[e.RowIndex].Cells["ID"].Value);
                m_EmpresaSelected.m_id = m_idSelected;
                m_EmpresaSelected.m_nombre = dataGridView_Empresas.Rows[e.RowIndex].Cells["NOMBRE"].Value.ToString();
                m_EmpresaSelected.m_cuit = dataGridView_Empresas.Rows[e.RowIndex].Cells["CUIT"].Value.ToString();
                m_EmpresaSelected.m_Tipo.m_id = Convert.ToInt32(dataGridView_Empresas.Rows[e.RowIndex].Cells["ID_TIPO"].Value);
                m_EmpresaSelected.m_Tipo.m_nombre = dataGridView_Empresas.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBox_nombreBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarDataGrid(textBox_nombreBuscar.Text);
        }

        private void textBox_cuit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox_ajusteOculto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b' || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void button_vincularVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_vehiculos.SelectedIndex != -1)
                {
                    if ((listBox_vehiculosVinculados.Items.Count == 0) || !listBox_vehiculosVinculados.Items.Contains(listBox_vehiculos.SelectedItem))
                        listBox_vehiculosVinculados.Items.Add(listBox_vehiculos.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Vinculando Vehiculo", "Validación de Vinculos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button_desvincularVehiculo_Click(object sender, EventArgs e)
        {
            if (listBox_vehiculosVinculados.SelectedIndex != -1)
            {
                listBox_vehiculosVinculados.Items.Remove(listBox_vehiculosVinculados.SelectedItem);
            }
        }

        private void button_vincularProducto_Click(object sender, EventArgs e)
        {
            if (listBox_Productos.SelectedIndex != -1)
            {
                if ((listBox_productosVinculados.Items.Count == 0) || !listBox_productosVinculados.Items.Contains(listBox_Productos.SelectedItem))
                    listBox_productosVinculados.Items.Add(listBox_Productos.SelectedItem);
                else
                    MessageBox.Show("El Producto ya se encuentra vinculado", "Validación de Vinculos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button_desvincularProducto_Click(object sender, EventArgs e)
        {
            if (listBox_productosVinculados.SelectedIndex != -1)
            {
                listBox_productosVinculados.Items.Remove(listBox_productosVinculados.SelectedItem);
            }
        }

        private void contextMenuStrip_listBox_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            if (((ContextMenuStrip)sender).SourceControl == listBox_vehiculos)
            {
                if (e.ClickedItem.Text == "Agregar")
                {
                    CABM_VehiculosDlg abmDlg = new CABM_VehiculosDlg(m_dbManagem);
                    if (abmDlg.ShowDialog() == DialogResult.OK)
                    {
                        LoadListBoxVehiculos();
                    }
                }
            }
            else if (((ContextMenuStrip)sender).SourceControl == listBox_Productos)
            {
                if (e.ClickedItem.Text == "Agregar")
                {
                    CABM_ProductosDlg abmDlg = new CABM_ProductosDlg(m_dbManagem);
                    if (abmDlg.ShowDialog() == DialogResult.OK)
                    {
                        LoadListBoxProductos();
                    }
                }
            }

        }

    }
}
