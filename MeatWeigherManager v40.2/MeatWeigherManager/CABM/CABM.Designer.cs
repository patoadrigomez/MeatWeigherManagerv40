namespace ABMDLG
{
    partial class CABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CABM));
            this.dataGridView_Registers = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.groupBox_registro = new System.Windows.Forms.GroupBox();
            this.label_marcaNuevoRegistro = new System.Windows.Forms.Label();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.button_Nuevo = new System.Windows.Forms.Button();
            this.button_Actualizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Registers)).BeginInit();
            this.groupBox_registro.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_Registers
            // 
            this.dataGridView_Registers.AllowUserToAddRows = false;
            this.dataGridView_Registers.AllowUserToOrderColumns = true;
            this.dataGridView_Registers.AllowUserToResizeRows = false;
            this.dataGridView_Registers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Registers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_Registers.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_Registers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Registers.Location = new System.Drawing.Point(12, 168);
            this.dataGridView_Registers.MultiSelect = false;
            this.dataGridView_Registers.Name = "dataGridView_Registers";
            this.dataGridView_Registers.ReadOnly = true;
            this.dataGridView_Registers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Registers.Size = new System.Drawing.Size(740, 237);
            this.dataGridView_Registers.StandardTab = true;
            this.dataGridView_Registers.TabIndex = 14;
            this.dataGridView_Registers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Operadores_CellDoubleClick);
            this.dataGridView_Registers.SelectionChanged += new System.EventHandler(this.dataGridView_Operadores_SelectionChanged);
            this.dataGridView_Registers.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_Operadores_UserDeletingRow);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(676, 411);
            this.button_Aceptar.Name = "button_Aceptar";
            this.button_Aceptar.Size = new System.Drawing.Size(76, 27);
            this.button_Aceptar.TabIndex = 10;
            this.button_Aceptar.Text = "Aceptar";
            this.button_Aceptar.UseVisualStyleBackColor = true;
            // 
            // button_Cancelar
            // 
            this.button_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancelar.Location = new System.Drawing.Point(594, 411);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 11;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Controls.Add(this.button_Actualizar);
            this.groupBox_registro.Controls.Add(this.label_marcaNuevoRegistro);
            this.groupBox_registro.Controls.Add(this.textBox_ID);
            this.groupBox_registro.Controls.Add(this.label10);
            this.groupBox_registro.Controls.Add(this.button_eliminar);
            this.groupBox_registro.Controls.Add(this.button_Nuevo);
            this.groupBox_registro.Location = new System.Drawing.Point(12, 12);
            this.groupBox_registro.Name = "groupBox_registro";
            this.groupBox_registro.Size = new System.Drawing.Size(740, 121);
            this.groupBox_registro.TabIndex = 21;
            this.groupBox_registro.TabStop = false;
            this.groupBox_registro.Text = "Registro";
            // 
            // label_marcaNuevoRegistro
            // 
            this.label_marcaNuevoRegistro.AutoSize = true;
            this.label_marcaNuevoRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_marcaNuevoRegistro.ForeColor = System.Drawing.Color.Red;
            this.label_marcaNuevoRegistro.Location = new System.Drawing.Point(181, 17);
            this.label_marcaNuevoRegistro.Name = "label_marcaNuevoRegistro";
            this.label_marcaNuevoRegistro.Size = new System.Drawing.Size(18, 24);
            this.label_marcaNuevoRegistro.TabIndex = 47;
            this.label_marcaNuevoRegistro.Text = "*";
            this.label_marcaNuevoRegistro.Visible = false;
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(79, 19);
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
            this.label10.Location = new System.Drawing.Point(11, 21);
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
            this.button_eliminar.Location = new System.Drawing.Point(662, 21);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(71, 62);
            this.button_eliminar.TabIndex = 12;
            this.button_eliminar.TabStop = false;
            this.button_eliminar.UseVisualStyleBackColor = true;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
            // 
            // button_Nuevo
            // 
            this.button_Nuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Nuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Nuevo.ForeColor = System.Drawing.Color.Transparent;
            this.button_Nuevo.Image = ((System.Drawing.Image)(resources.GetObject("button_Nuevo.Image")));
            this.button_Nuevo.Location = new System.Drawing.Point(507, 21);
            this.button_Nuevo.Name = "button_Nuevo";
            this.button_Nuevo.Size = new System.Drawing.Size(72, 62);
            this.button_Nuevo.TabIndex = 13;
            this.button_Nuevo.UseVisualStyleBackColor = true;
            this.button_Nuevo.Click += new System.EventHandler(this.button_Nuevo_Click);
            // 
            // button_Actualizar
            // 
            this.button_Actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Actualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Actualizar.ForeColor = System.Drawing.Color.Green;
            this.button_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("button_Actualizar.Image")));
            this.button_Actualizar.Location = new System.Drawing.Point(587, 21);
            this.button_Actualizar.Name = "button_Actualizar";
            this.button_Actualizar.Size = new System.Drawing.Size(71, 62);
            this.button_Actualizar.TabIndex = 48;
            this.button_Actualizar.UseVisualStyleBackColor = true;
            // 
            // CABM
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(757, 450);
            this.Controls.Add(this.groupBox_registro);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.dataGridView_Registers);
            this.Controls.Add(this.button_Aceptar);
            this.Name = "CABM";
            this.Text = "ABM";
            this.Load += new System.EventHandler(this.CABM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Registers)).EndInit();
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Registers;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.GroupBox groupBox_registro;
        private System.Windows.Forms.Button button_Nuevo;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_marcaNuevoRegistro;
        private System.Windows.Forms.Button button_Actualizar;
    }
}