using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAB_ListItemsDlg;
using EditStringTouchDlg;
using System.Windows.Forms;
using ABM_DB;
using System.Data.OleDb;
using Db;

namespace MeatWeigherManager
{
    class CABM_TiposProductosDlg : CABM_DB
    {
        #region Privates VARIABLES AND PROPERTIES 
        private TextBox textBox_nombre;
        private Label label1;
        CTipoProducto m_tipoProductoSelected;
        #endregion

        #region Public VARIABLES AND PROPERTIES 
        public CTipoProducto TipoProductoSelected { get => m_tipoProductoSelected; set => m_tipoProductoSelected = value; }
        #endregion

        #region INICIALIZACION
        public CABM_TiposProductosDlg(OleDbConnection oleDbConnection, int idSelection = 0) : base(oleDbConnection, idSelection)
        {
            InitializeComponent();
            base.Text = "MeatWeigherManager - Mantenimiento de Tabla de Tipos de Productos";
            NameTable = "TiposProducto";
            NameFieldID = "ID";
            NameFieldPrincipal = "Nombre";
            NameFieldFilterDataGrid = "Nombre";
            NameRegister = "Tipo de Producto";
            LabelDataGrid = NameRegister;
            LabelTextBoxFilterDataGrid = "Busqueda por " + NameFieldFilterDataGrid;
            SqlQuery_LoadDataGrid = "SELECT NOMBRE,ID FROM TiposProducto WHERE 1=1 ";
            OnDGV_CellDoubleClick += CABM_TiposProductosDlg_OnDGV_CellDoubleClick;
            OnDGV_SelectionChanged += CABM_TiposProductosDlg_OnDGV_SelectionChanged;

            TipoProductoSelected = new CTipoProducto();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_nombre = new System.Windows.Forms.TextBox();
            this.groupBox_registro.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Controls.Add(this.textBox_nombre);
            this.groupBox_registro.Controls.Add(this.label1);
            this.groupBox_registro.Size = new System.Drawing.Size(624, 100);
            this.groupBox_registro.Controls.SetChildIndex(this.button_Nuevo, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.button_Actualizar, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.button_eliminar, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.textBox_ID, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.label1, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.textBox_nombre, 0);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Location = new System.Drawing.Point(552, 420);
            // 
            // button_Cancelar
            // 
            this.button_Cancelar.Location = new System.Drawing.Point(470, 420);
            // 
            // label_nameTableDataGrid
            // 
            this.label_nameTableDataGrid.Location = new System.Drawing.Point(543, 146);
            // 
            // panel_DGV
            // 
            this.panel_DGV.Size = new System.Drawing.Size(622, 246);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Nombre";
            // 
            // textBox_nombre
            // 
            this.textBox_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombre.Location = new System.Drawing.Point(102, 63);
            this.textBox_nombre.MaxLength = 30;
            this.textBox_nombre.Name = "textBox_nombre";
            this.textBox_nombre.Size = new System.Drawing.Size(239, 26);
            this.textBox_nombre.TabIndex = 32;
            // 
            // CABM_TiposProductosDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 459);
            this.Name = "CABM_TiposProductosDlg";
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region EVENTOS DEL FORM
        private void CABM_TiposProductosDlg_OnDGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Table.SelectedRows.Count > 0)
                {
                    textBox_nombre.Text = dataGridView_Table.SelectedRows[0].Cells["NOMBRE"].Value.ToString();
                    TipoProductoSelected = new CTipoProducto()
                    {
                        Id = IdSelected,
                        Nombre = textBox_nombre.Text
                    };
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CABM_TiposProductosDlg_OnDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        #endregion

        #region METODOS OVERRIDE E IMPLEMENTACION DE ABSTRAPTOS 
        public override void SetStateVisibleColumnsDGV()
        {
            base.SetStateVisibleColumnsDGV();
            dataGridView_Table.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        #endregion

    }
}
