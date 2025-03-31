namespace MeatWeigherManager
{
    partial class CAjusteStockInsumosDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CAjusteStockInsumosDlg));
            this.dataGridView_DetalleExistenciaStockInsumos = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.groupBox_registro = new System.Windows.Forms.GroupBox();
            this.textBox_insumo = new System.Windows.Forms.TextBox();
            this.textBox_unidadesAjustar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_unidadesExistencia = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button_Actualizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_filtroDataGrid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetalleExistenciaStockInsumos)).BeginInit();
            this.groupBox_registro.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_DetalleExistenciaStockInsumos
            // 
            this.dataGridView_DetalleExistenciaStockInsumos.AllowUserToAddRows = false;
            this.dataGridView_DetalleExistenciaStockInsumos.AllowUserToOrderColumns = true;
            this.dataGridView_DetalleExistenciaStockInsumos.AllowUserToResizeRows = false;
            this.dataGridView_DetalleExistenciaStockInsumos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_DetalleExistenciaStockInsumos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_DetalleExistenciaStockInsumos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_DetalleExistenciaStockInsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DetalleExistenciaStockInsumos.Location = new System.Drawing.Point(12, 162);
            this.dataGridView_DetalleExistenciaStockInsumos.Name = "dataGridView_DetalleExistenciaStockInsumos";
            this.dataGridView_DetalleExistenciaStockInsumos.ReadOnly = true;
            this.dataGridView_DetalleExistenciaStockInsumos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_DetalleExistenciaStockInsumos.Size = new System.Drawing.Size(693, 309);
            this.dataGridView_DetalleExistenciaStockInsumos.StandardTab = true;
            this.dataGridView_DetalleExistenciaStockInsumos.TabIndex = 14;
            this.dataGridView_DetalleExistenciaStockInsumos.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(629, 477);
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
            this.button_Cancelar.Location = new System.Drawing.Point(547, 477);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 11;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_registro.Controls.Add(this.textBox_insumo);
            this.groupBox_registro.Controls.Add(this.textBox_unidadesAjustar);
            this.groupBox_registro.Controls.Add(this.label4);
            this.groupBox_registro.Controls.Add(this.label3);
            this.groupBox_registro.Controls.Add(this.textBox_unidadesExistencia);
            this.groupBox_registro.Controls.Add(this.label11);
            this.groupBox_registro.Controls.Add(this.button_Actualizar);
            this.groupBox_registro.Location = new System.Drawing.Point(12, 12);
            this.groupBox_registro.Name = "groupBox_registro";
            this.groupBox_registro.Size = new System.Drawing.Size(693, 96);
            this.groupBox_registro.TabIndex = 21;
            this.groupBox_registro.TabStop = false;
            this.groupBox_registro.Text = "Registro";
            // 
            // textBox_insumo
            // 
            this.textBox_insumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_insumo.Location = new System.Drawing.Point(9, 39);
            this.textBox_insumo.MaxLength = 5;
            this.textBox_insumo.Name = "textBox_insumo";
            this.textBox_insumo.ReadOnly = true;
            this.textBox_insumo.Size = new System.Drawing.Size(382, 26);
            this.textBox_insumo.TabIndex = 52;
            // 
            // textBox_unidadesAjustar
            // 
            this.textBox_unidadesAjustar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_unidadesAjustar.Location = new System.Drawing.Point(499, 39);
            this.textBox_unidadesAjustar.MaxLength = 10;
            this.textBox_unidadesAjustar.Name = "textBox_unidadesAjustar";
            this.textBox_unidadesAjustar.Size = new System.Drawing.Size(96, 26);
            this.textBox_unidadesAjustar.TabIndex = 6;
            this.textBox_unidadesAjustar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_unidadesAjustar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_unidadesAjustar_KeyPress);
            this.textBox_unidadesAjustar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_unidadesAjustar_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(501, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Ajustar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Insumo";
            // 
            // textBox_unidadesExistencia
            // 
            this.textBox_unidadesExistencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_unidadesExistencia.Location = new System.Drawing.Point(397, 39);
            this.textBox_unidadesExistencia.MaxLength = 5;
            this.textBox_unidadesExistencia.Name = "textBox_unidadesExistencia";
            this.textBox_unidadesExistencia.ReadOnly = true;
            this.textBox_unidadesExistencia.Size = new System.Drawing.Size(96, 26);
            this.textBox_unidadesExistencia.TabIndex = 4;
            this.textBox_unidadesExistencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(398, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Existencia";
            // 
            // button_Actualizar
            // 
            this.button_Actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Actualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Actualizar.ForeColor = System.Drawing.Color.Green;
            this.button_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("button_Actualizar.Image")));
            this.button_Actualizar.Location = new System.Drawing.Point(615, 19);
            this.button_Actualizar.Name = "button_Actualizar";
            this.button_Actualizar.Size = new System.Drawing.Size(71, 62);
            this.button_Actualizar.TabIndex = 11;
            this.button_Actualizar.UseVisualStyleBackColor = true;
            this.button_Actualizar.Click += new System.EventHandler(this.button_Actualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(592, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Detalle Stock Insumos";
            // 
            // textBox_filtroDataGrid
            // 
            this.textBox_filtroDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_filtroDataGrid.BackColor = System.Drawing.Color.Black;
            this.textBox_filtroDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_filtroDataGrid.ForeColor = System.Drawing.Color.White;
            this.textBox_filtroDataGrid.Location = new System.Drawing.Point(12, 131);
            this.textBox_filtroDataGrid.Name = "textBox_filtroDataGrid";
            this.textBox_filtroDataGrid.Size = new System.Drawing.Size(238, 26);
            this.textBox_filtroDataGrid.TabIndex = 35;
            this.textBox_filtroDataGrid.TextChanged += new System.EventHandler(this.textBox_filtroDataGrid_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Buscar Insumo";
            // 
            // CAjusteStockInsumosDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(710, 516);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_filtroDataGrid);
            this.Controls.Add(this.groupBox_registro);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.dataGridView_DetalleExistenciaStockInsumos);
            this.Controls.Add(this.button_Aceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CAjusteStockInsumosDlg";
            this.Text = "MeatWeigherManager    -  Gestor de Ajustes de Stock para Insumos";
            this.Load += new System.EventHandler(this.CABM_CAjusteStockInsumosDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetalleExistenciaStockInsumos)).EndInit();
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_DetalleExistenciaStockInsumos;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.GroupBox groupBox_registro;
        private System.Windows.Forms.Button button_Actualizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_unidadesExistencia;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_unidadesAjustar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_insumo;
        protected System.Windows.Forms.TextBox textBox_filtroDataGrid;
        private System.Windows.Forms.Label label2;
    }
}