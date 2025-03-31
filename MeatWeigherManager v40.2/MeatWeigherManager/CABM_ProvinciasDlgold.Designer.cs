namespace MeatWeigherManager
{
    partial class CABM_ProvinciasDlgold
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CABM_ProvinciasDlgold));
            this.dataGridView_Provincias = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.textBox_nombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_paises = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip_comboBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Agregar = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_registro = new System.Windows.Forms.GroupBox();
            this.label_marcaNuevoRegistro = new System.Windows.Forms.Label();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.button_Actualizar = new System.Windows.Forms.Button();
            this.button_Nuevo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Provincias)).BeginInit();
            this.contextMenuStrip_comboBox.SuspendLayout();
            this.groupBox_registro.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_Provincias
            // 
            this.dataGridView_Provincias.AllowUserToAddRows = false;
            this.dataGridView_Provincias.AllowUserToOrderColumns = true;
            this.dataGridView_Provincias.AllowUserToResizeRows = false;
            this.dataGridView_Provincias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Provincias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_Provincias.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_Provincias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Provincias.Location = new System.Drawing.Point(12, 165);
            this.dataGridView_Provincias.MultiSelect = false;
            this.dataGridView_Provincias.Name = "dataGridView_Provincias";
            this.dataGridView_Provincias.ReadOnly = true;
            this.dataGridView_Provincias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Provincias.Size = new System.Drawing.Size(576, 156);
            this.dataGridView_Provincias.StandardTab = true;
            this.dataGridView_Provincias.TabIndex = 0;
            this.dataGridView_Provincias.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Provincias_CellDoubleClick);
            this.dataGridView_Provincias.SelectionChanged += new System.EventHandler(this.dataGridView_Provincias_SelectionChanged);
            this.dataGridView_Provincias.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_Provincias_UserDeletingRow);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(510, 330);
            this.button_Aceptar.Name = "button_Aceptar";
            this.button_Aceptar.Size = new System.Drawing.Size(76, 27);
            this.button_Aceptar.TabIndex = 12;
            this.button_Aceptar.Text = "Aceptar";
            this.button_Aceptar.UseVisualStyleBackColor = true;
            // 
            // button_Cancelar
            // 
            this.button_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancelar.Location = new System.Drawing.Point(428, 330);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 13;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // textBox_nombre
            // 
            this.textBox_nombre.Location = new System.Drawing.Point(102, 62);
            this.textBox_nombre.MaxLength = 30;
            this.textBox_nombre.Name = "textBox_nombre";
            this.textBox_nombre.Size = new System.Drawing.Size(239, 20);
            this.textBox_nombre.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Provincia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pais";
            // 
            // comboBox_paises
            // 
            this.comboBox_paises.ContextMenuStrip = this.contextMenuStrip_comboBox;
            this.comboBox_paises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_paises.FormattingEnabled = true;
            this.comboBox_paises.Location = new System.Drawing.Point(102, 88);
            this.comboBox_paises.Name = "comboBox_paises";
            this.comboBox_paises.Size = new System.Drawing.Size(239, 21);
            this.comboBox_paises.TabIndex = 2;
            // 
            // contextMenuStrip_comboBox
            // 
            this.contextMenuStrip_comboBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Agregar});
            this.contextMenuStrip_comboBox.Name = "contextMenuStrip_comboBox";
            this.contextMenuStrip_comboBox.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStrip_comboBox.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_comboBox_ItemClicked);
            // 
            // toolStripMenuItem_Agregar
            // 
            this.toolStripMenuItem_Agregar.Name = "toolStripMenuItem_Agregar";
            this.toolStripMenuItem_Agregar.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_Agregar.Text = "Agregar";
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Controls.Add(this.label_marcaNuevoRegistro);
            this.groupBox_registro.Controls.Add(this.textBox_ID);
            this.groupBox_registro.Controls.Add(this.label10);
            this.groupBox_registro.Controls.Add(this.button_eliminar);
            this.groupBox_registro.Controls.Add(this.button_Actualizar);
            this.groupBox_registro.Controls.Add(this.button_Nuevo);
            this.groupBox_registro.Controls.Add(this.textBox_nombre);
            this.groupBox_registro.Controls.Add(this.label2);
            this.groupBox_registro.Controls.Add(this.label3);
            this.groupBox_registro.Controls.Add(this.comboBox_paises);
            this.groupBox_registro.Location = new System.Drawing.Point(12, 12);
            this.groupBox_registro.Name = "groupBox_registro";
            this.groupBox_registro.Size = new System.Drawing.Size(576, 132);
            this.groupBox_registro.TabIndex = 21;
            this.groupBox_registro.TabStop = false;
            this.groupBox_registro.Text = "Registro";
            // 
            // label_marcaNuevoRegistro
            // 
            this.label_marcaNuevoRegistro.AutoSize = true;
            this.label_marcaNuevoRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_marcaNuevoRegistro.ForeColor = System.Drawing.Color.Red;
            this.label_marcaNuevoRegistro.Location = new System.Drawing.Point(205, 35);
            this.label_marcaNuevoRegistro.Name = "label_marcaNuevoRegistro";
            this.label_marcaNuevoRegistro.Size = new System.Drawing.Size(18, 24);
            this.label_marcaNuevoRegistro.TabIndex = 30;
            this.label_marcaNuevoRegistro.Text = "*";
            this.label_marcaNuevoRegistro.Visible = false;
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(102, 36);
            this.textBox_ID.MaxLength = 30;
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.ReadOnly = true;
            this.textBox_ID.Size = new System.Drawing.Size(97, 20);
            this.textBox_ID.TabIndex = 0;
            this.textBox_ID.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "ID";
            // 
            // button_eliminar
            // 
            this.button_eliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_eliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_eliminar.ForeColor = System.Drawing.Color.Green;
            this.button_eliminar.Image = ((System.Drawing.Image)(resources.GetObject("button_eliminar.Image")));
            this.button_eliminar.Location = new System.Drawing.Point(498, 21);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(71, 62);
            this.button_eliminar.TabIndex = 10;
            this.button_eliminar.TabStop = false;
            this.button_eliminar.UseVisualStyleBackColor = true;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
            // 
            // button_Actualizar
            // 
            this.button_Actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Actualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Actualizar.ForeColor = System.Drawing.Color.Green;
            this.button_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("button_Actualizar.Image")));
            this.button_Actualizar.Location = new System.Drawing.Point(421, 21);
            this.button_Actualizar.Name = "button_Actualizar";
            this.button_Actualizar.Size = new System.Drawing.Size(71, 62);
            this.button_Actualizar.TabIndex = 9;
            this.button_Actualizar.UseVisualStyleBackColor = true;
            this.button_Actualizar.Click += new System.EventHandler(this.button_Actualizar_Click);
            // 
            // button_Nuevo
            // 
            this.button_Nuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Nuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Nuevo.ForeColor = System.Drawing.Color.Transparent;
            this.button_Nuevo.Image = ((System.Drawing.Image)(resources.GetObject("button_Nuevo.Image")));
            this.button_Nuevo.Location = new System.Drawing.Point(343, 21);
            this.button_Nuevo.Name = "button_Nuevo";
            this.button_Nuevo.Size = new System.Drawing.Size(72, 62);
            this.button_Nuevo.TabIndex = 11;
            this.button_Nuevo.UseVisualStyleBackColor = true;
            this.button_Nuevo.Click += new System.EventHandler(this.button_Nuevo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Provincias";
            // 
            // CABM_ProvinciasDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(592, 366);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_Provincias);
            this.Controls.Add(this.groupBox_registro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CABM_ProvinciasDlg";
            this.Text = "MeatWeigherManager    -  Mantenimiento de Tabla de Provincias";
            this.Load += new System.EventHandler(this.CABM_Provincias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Provincias)).EndInit();
            this.contextMenuStrip_comboBox.ResumeLayout(false);
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Provincias;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.TextBox textBox_nombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_paises;
        private System.Windows.Forms.GroupBox groupBox_registro;
        private System.Windows.Forms.Button button_Nuevo;
        private System.Windows.Forms.Button button_Actualizar;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_comboBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Agregar;
        private System.Windows.Forms.Label label_marcaNuevoRegistro;
    }
}