using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AB_ListItemsDlg;
using EditStringTouchDlg;
using System.Windows.Forms;
using ABM_DB;
using System.Data.OleDb;
using Db;

namespace MeatWeigherManager
{
    class CABM_TipificacionesDlg : CABM_DB
    {
        #region Privates VARIABLES AND PROPERTIES 
        private TextBox textBox_nombre;
        private Label label1;
        Tipificacion m_tipificacionSelected;
        private BEHAVIOR_MODE m_mode;
        #endregion

        #region Public VARIABLES AND PROPERTIES 
        public Tipificacion TipificacionSelected { get => m_tipificacionSelected; set => m_tipificacionSelected = value; }
        public enum BEHAVIOR_MODE
        {
            ABM,
            SELECTION
        }

        #endregion

        #region INICIALIZACION
        public CABM_TipificacionesDlg(OleDbConnection oleDbConnection, BEHAVIOR_MODE mode = BEHAVIOR_MODE.ABM, int idSelection = 0) : base(oleDbConnection, idSelection)
        {
            InitializeComponent();
            m_mode = mode;
            base.Text = "MeatWeigherManager - Mantenimiento de Tabla de Tipificaciones";
            NameTable = "Tipificaciones";
            NameFieldID = "ID";
            NameFieldPrincipal = "Nombre";
            NameFieldFilterDataGrid = "Nombre";
            NameRegister = "Tipificación";
            LabelDataGrid = NameRegister;
            LabelTextBoxFilterDataGrid = "Busqueda por " + NameFieldFilterDataGrid;
            SqlQuery_LoadDataGrid = "SELECT NOMBRE,ID FROM Tipificaciones WHERE 1=1 ";
            OnDGV_CellDoubleClick += CABM_TipificacionesDlg_OnDGV_CellDoubleClick;
            OnDGV_SelectionChanged += CABM_TipificacionesDlg_OnDGV_SelectionChanged;


            TipificacionSelected = new Tipificacion();
            if (m_mode == BEHAVIOR_MODE.SELECTION)
            {
                base.Text = "MeatWeigherManager - SELECCIÓN DE TIPIFICACIÓN";
                panel_DGV.Dock = DockStyle.Fill;
                dataGridView_Table.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridView_Table.RowTemplate.Height = 35;
            }
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
            this.label_nameTableDataGrid.Location = new System.Drawing.Point(577, 146);
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
            // CABM_TipificacionesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 459);
            this.Name = "CABM_TipificacionesDlg";
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region EVENTOS DEL FORM

        private void CABM_TipificacionesDlg_OnDGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Table.SelectedRows.Count > 0)
                {
                    textBox_nombre.Text = dataGridView_Table.SelectedRows[0].Cells["NOMBRE"].Value.ToString();
                    TipificacionSelected = new Tipificacion()
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

        private void CABM_TipificacionesDlg_OnDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        #endregion

        #region METODOS OVERRIDE E IMPLEMENTACION DE ABSTRAPTOS 
       
        public override void SetStateVisibleColumnsDGV()
        {
            base.SetStateVisibleColumnsDGV();
            dataGridView_Table.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Table.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); ;

            dataGridView_Table.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_Table.Columns["NOMBRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
        #endregion

    }
}
