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
    class CABM_EtiquetasDlg : CABM_DB
    {
        #region Privates VARIABLES AND PROPERTIES 
        private TextBox textBox_nombre;
        private Label label1;
        CEtiqueta m_etiquetaSelected;
        private Label label3;
        private ComboBox comboBox_tipo;
        private TextBox textBox_descripcion;
        private Label label2;
        private BEHAVIOR_MODE m_mode;
        #endregion

        #region Public VARIABLES AND PROPERTIES 
        public CEtiqueta EtiquetaSelected { get => m_etiquetaSelected; set => m_etiquetaSelected = value; }
        public enum BEHAVIOR_MODE
        {
            ABM,
            SELECTION
        }

        #endregion

        #region INICIALIZACION
        public CABM_EtiquetasDlg(OleDbConnection oleDbConnection,BEHAVIOR_MODE mode =BEHAVIOR_MODE.ABM, int idSelection = 0) : base(oleDbConnection, idSelection)
        {
            InitializeComponent();
            m_mode = mode;
            base.Text = "MeatWeigherManager - Mantenimiento de Tabla de Etiquetas";
            NameTable = "Etiquetas";
            NameFieldID = "ID";
            NameFieldPrincipal = "Nombre";
            NameFieldFilterDataGrid = "e.nombre";
            NameRegister = "Etiqueta";
            LabelDataGrid = NameRegister;
            LabelTextBoxFilterDataGrid = "Busqueda por "+ NameFieldFilterDataGrid;
            SqlQuery_LoadDataGrid = "SELECT e.Nombre as NOMBRE,e.id as ID,tb.id as IDTIPO,tb.nombre as TIPO,e.Descripcion as DESCRIPCION FROM Etiquetas e JOIN TiposBulto tb ON e.idTipoBulto=tb.id WHERE 1=1 ";
            OnDGV_CellDoubleClick += CABM_EtiquetasDlg_OnDGV_CellDoubleClick;
            OnDGV_SelectionChanged += CABM_EtiquetasDlg_OnDGV_SelectionChanged;

            EtiquetaSelected = new CEtiqueta();
            if (m_mode == BEHAVIOR_MODE.SELECTION)
            {
                base.Text = "MeatWeigherManager - SELECCIÓN DE ETIQUETA";
                panel_DGV.Dock = DockStyle.Fill;
                dataGridView_Table.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridView_Table.RowTemplate.Height = 35;
            }

            CDb.FillComboBoxIdText(comboBox_tipo, "SELECT id as ID , nombre as DESCRIPCION FROM TIPOSBULTO");
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_nombre = new System.Windows.Forms.TextBox();
            this.textBox_descripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_tipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_registro.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Controls.Add(this.label3);
            this.groupBox_registro.Controls.Add(this.comboBox_tipo);
            this.groupBox_registro.Controls.Add(this.textBox_descripcion);
            this.groupBox_registro.Controls.Add(this.label2);
            this.groupBox_registro.Controls.Add(this.textBox_nombre);
            this.groupBox_registro.Controls.Add(this.label1);
            this.groupBox_registro.Size = new System.Drawing.Size(624, 166);
            this.groupBox_registro.Controls.SetChildIndex(this.button_Nuevo, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.button_Actualizar, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.button_eliminar, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.textBox_ID, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.label1, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.textBox_nombre, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.label2, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.textBox_descripcion, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.comboBox_tipo, 0);
            this.groupBox_registro.Controls.SetChildIndex(this.label3, 0);
            // 
            // textBox_filtroDataGrid
            // 
            this.textBox_filtroDataGrid.Location = new System.Drawing.Point(104, 213);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Location = new System.Drawing.Point(552, 487);
            // 
            // button_Cancelar
            // 
            this.button_Cancelar.Location = new System.Drawing.Point(470, 487);
            // 
            // label_nameTableDataGrid
            // 
            this.label_nameTableDataGrid.Location = new System.Drawing.Point(579, 229);
            // 
            // label_textboxFilterDataGrid
            // 
            this.label_textboxFilterDataGrid.Location = new System.Drawing.Point(348, 220);
            // 
            // button_down
            // 
            this.button_down.Location = new System.Drawing.Point(53, 209);
            // 
            // button_up
            // 
            this.button_up.Location = new System.Drawing.Point(14, 209);
            // 
            // panel_DGV
            // 
            this.panel_DGV.Location = new System.Drawing.Point(12, 248);
            this.panel_DGV.Size = new System.Drawing.Size(622, 233);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Nombre";
            // 
            // textBox_nombre
            // 
            this.textBox_nombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombre.Location = new System.Drawing.Point(102, 63);
            this.textBox_nombre.MaxLength = 8;
            this.textBox_nombre.Name = "textBox_nombre";
            this.textBox_nombre.Size = new System.Drawing.Size(146, 26);
            this.textBox_nombre.TabIndex = 32;

            // 
            // textBox_descripcion
            // 
            this.textBox_descripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_descripcion.Location = new System.Drawing.Point(102, 95);
            this.textBox_descripcion.MaxLength = 100;
            this.textBox_descripcion.Name = "textBox_descripcion";
            this.textBox_descripcion.Size = new System.Drawing.Size(513, 26);
            this.textBox_descripcion.TabIndex = 34;
            this.textBox_descripcion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_descripcion_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Descripción";
            // 
            // comboBox_tipo
            // 
            this.comboBox_tipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_tipo.FormattingEnabled = true;
            this.comboBox_tipo.Location = new System.Drawing.Point(102, 127);
            this.comboBox_tipo.Name = "comboBox_tipo";
            this.comboBox_tipo.Size = new System.Drawing.Size(146, 26);
            this.comboBox_tipo.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Tipo";
            // 
            // CABM_EtiquetasDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 526);
            this.Name = "CABM_EtiquetasDlg";
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region EVENTOS DEL FORM
 
        private void CABM_EtiquetasDlg_OnDGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_Table.SelectedRows.Count > 0)
                {
                    textBox_nombre.Text = dataGridView_Table.SelectedRows[0].Cells["NOMBRE"].Value.ToString();
                    textBox_descripcion.Text = dataGridView_Table.SelectedRows[0].Cells["DESCRIPCION"].Value.ToString();
                    CDb.SelectItemComboDb(comboBox_tipo,dataGridView_Table.SelectedRows[0].Cells["IDTIPO"].Value.ToString());

                    EtiquetaSelected = new CEtiqueta()
                    {
                        Id = IdSelected,
                        Nombre = textBox_nombre.Text,
                        Descripcion=textBox_descripcion.Text,
                        IdTipoBulto= dataGridView_Table.SelectedRows[0].Cells["IDTIPO"].ToString()
                    };
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error Cargando los datos desde la grilla al dialogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_descripcion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Descripción de la Etiqueta", "Descripcion", textBox_descripcion.Text, 100);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_descripcion.Text = dlg.VALUE;
            }
        }

        private void CABM_EtiquetasDlg_OnDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        #endregion

        #region METODOS OVERRIDE E IMPLEMENTACION DE ABSTRAPTOS 
        public override void SetStateVisibleColumnsDGV()
        {
            base.SetStateVisibleColumnsDGV();
            dataGridView_Table.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Table.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); ;
            dataGridView_Table.Columns["IDTIPO"].Visible = false;

            dataGridView_Table.Columns["NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_Table.Columns["NOMBRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_Table.Columns["DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_Table.Columns["DESCRIPCION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_Table.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_Table.Columns["TIPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
        #endregion

    }
}
