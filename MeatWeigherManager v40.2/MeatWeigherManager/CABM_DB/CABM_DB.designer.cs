namespace ABM_DB
{
    partial class CABM_DB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CABM_DB));
            this.dataGridView_Table = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.groupBox_registro = new System.Windows.Forms.GroupBox();
            this.label_marcaNuevoRegistro = new System.Windows.Forms.Label();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.button_Actualizar = new System.Windows.Forms.Button();
            this.button_Nuevo = new System.Windows.Forms.Button();
            this.label_nameTableDataGrid = new System.Windows.Forms.Label();
            this.label_textboxFilterDataGrid = new System.Windows.Forms.Label();
            this.textBox_filtroDataGrid = new System.Windows.Forms.TextBox();
            this.button_down = new System.Windows.Forms.Button();
            this.button_up = new System.Windows.Forms.Button();
            this.panel_DGV = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Table)).BeginInit();
            this.groupBox_registro.SuspendLayout();
            this.panel_DGV.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_Table
            // 
            this.dataGridView_Table.AllowUserToAddRows = false;
            this.dataGridView_Table.AllowUserToOrderColumns = true;
            this.dataGridView_Table.AllowUserToResizeRows = false;
            this.dataGridView_Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_Table.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Table.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Table.EnableHeadersVisualStyles = false;
            this.dataGridView_Table.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGridView_Table.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Table.MultiSelect = false;
            this.dataGridView_Table.Name = "dataGridView_Table";
            this.dataGridView_Table.ReadOnly = true;
            this.dataGridView_Table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Table.Size = new System.Drawing.Size(613, 251);
            this.dataGridView_Table.StandardTab = true;
            this.dataGridView_Table.TabIndex = 0;
            this.dataGridView_Table.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Table_CellClick);
            this.dataGridView_Table.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Table_CellDoubleClick);
            this.dataGridView_Table.SelectionChanged += new System.EventHandler(this.dataGridView_Table_SelectionChanged);
            this.dataGridView_Table.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_Table_UserDeletingRow);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(558, 430);
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
            this.button_Cancelar.Location = new System.Drawing.Point(476, 430);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 13;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Controls.Add(this.label_marcaNuevoRegistro);
            this.groupBox_registro.Controls.Add(this.textBox_ID);
            this.groupBox_registro.Controls.Add(this.label10);
            this.groupBox_registro.Controls.Add(this.button_eliminar);
            this.groupBox_registro.Controls.Add(this.button_Actualizar);
            this.groupBox_registro.Controls.Add(this.button_Nuevo);
            this.groupBox_registro.Location = new System.Drawing.Point(12, 12);
            this.groupBox_registro.Name = "groupBox_registro";
            this.groupBox_registro.Size = new System.Drawing.Size(622, 112);
            this.groupBox_registro.TabIndex = 21;
            this.groupBox_registro.TabStop = false;
            this.groupBox_registro.Text = "Registro";
            // 
            // label_marcaNuevoRegistro
            // 
            this.label_marcaNuevoRegistro.AutoSize = true;
            this.label_marcaNuevoRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_marcaNuevoRegistro.ForeColor = System.Drawing.Color.Red;
            this.label_marcaNuevoRegistro.Location = new System.Drawing.Point(205, 36);
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
            this.button_eliminar.Location = new System.Drawing.Point(544, 21);
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
            this.button_Actualizar.Location = new System.Drawing.Point(467, 21);
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
            this.button_Nuevo.Location = new System.Drawing.Point(389, 21);
            this.button_Nuevo.Name = "button_Nuevo";
            this.button_Nuevo.Size = new System.Drawing.Size(72, 62);
            this.button_Nuevo.TabIndex = 11;
            this.button_Nuevo.UseVisualStyleBackColor = true;
            this.button_Nuevo.Click += new System.EventHandler(this.button_New_Click);
            // 
            // label_nameTableDataGrid
            // 
            this.label_nameTableDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_nameTableDataGrid.AutoSize = true;
            this.label_nameTableDataGrid.Location = new System.Drawing.Point(598, 146);
            this.label_nameTableDataGrid.Name = "label_nameTableDataGrid";
            this.label_nameTableDataGrid.Size = new System.Drawing.Size(19, 13);
            this.label_nameTableDataGrid.TabIndex = 2;
            this.label_nameTableDataGrid.Text = "----";
            // 
            // label_textboxFilterDataGrid
            // 
            this.label_textboxFilterDataGrid.AutoSize = true;
            this.label_textboxFilterDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_textboxFilterDataGrid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_textboxFilterDataGrid.Location = new System.Drawing.Point(346, 137);
            this.label_textboxFilterDataGrid.Name = "label_textboxFilterDataGrid";
            this.label_textboxFilterDataGrid.Size = new System.Drawing.Size(17, 15);
            this.label_textboxFilterDataGrid.TabIndex = 35;
            this.label_textboxFilterDataGrid.Text = "--";
            // 
            // textBox_filtroDataGrid
            // 
            this.textBox_filtroDataGrid.BackColor = System.Drawing.Color.Black;
            this.textBox_filtroDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_filtroDataGrid.ForeColor = System.Drawing.Color.White;
            this.textBox_filtroDataGrid.Location = new System.Drawing.Point(102, 130);
            this.textBox_filtroDataGrid.Name = "textBox_filtroDataGrid";
            this.textBox_filtroDataGrid.Size = new System.Drawing.Size(238, 29);
            this.textBox_filtroDataGrid.TabIndex = 34;
            this.textBox_filtroDataGrid.TextChanged += new System.EventHandler(this.textBox_filtroDataGrid_TextChanged);
            // 
            // button_down
            // 
            this.button_down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_down.BackgroundImage")));
            this.button_down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_down.ForeColor = System.Drawing.Color.Transparent;
            this.button_down.Location = new System.Drawing.Point(51, 126);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(33, 36);
            this.button_down.TabIndex = 68;
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // button_up
            // 
            this.button_up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_up.BackgroundImage")));
            this.button_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_up.ForeColor = System.Drawing.Color.Transparent;
            this.button_up.Location = new System.Drawing.Point(12, 126);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(33, 36);
            this.button_up.TabIndex = 67;
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // panel_DGV
            // 
            this.panel_DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_DGV.Controls.Add(this.dataGridView_Table);
            this.panel_DGV.Location = new System.Drawing.Point(12, 168);
            this.panel_DGV.Name = "panel_DGV";
            this.panel_DGV.Size = new System.Drawing.Size(622, 257);
            this.panel_DGV.TabIndex = 69;
            // 
            // CABM_DB
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(640, 465);
            this.Controls.Add(this.panel_DGV);
            this.Controls.Add(this.button_down);
            this.Controls.Add(this.button_up);
            this.Controls.Add(this.label_textboxFilterDataGrid);
            this.Controls.Add(this.textBox_filtroDataGrid);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.label_nameTableDataGrid);
            this.Controls.Add(this.groupBox_registro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CABM_DB";
            this.Load += new System.EventHandler(this.CABM_Paises_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Table)).EndInit();
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.panel_DGV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_marcaNuevoRegistro;
        protected System.Windows.Forms.GroupBox groupBox_registro;
        protected System.Windows.Forms.DataGridView dataGridView_Table;
        protected System.Windows.Forms.TextBox textBox_ID;
        protected System.Windows.Forms.TextBox textBox_filtroDataGrid;
        protected System.Windows.Forms.Button button_Aceptar;
        protected System.Windows.Forms.Button button_Cancelar;
        protected System.Windows.Forms.Label label_nameTableDataGrid;
        protected System.Windows.Forms.Label label_textboxFilterDataGrid;
        protected System.Windows.Forms.Button button_down;
        protected System.Windows.Forms.Button button_up;
        protected System.Windows.Forms.Panel panel_DGV;
        protected System.Windows.Forms.Button button_Nuevo;
        protected System.Windows.Forms.Button button_Actualizar;
        protected System.Windows.Forms.Button button_eliminar;
    }
}