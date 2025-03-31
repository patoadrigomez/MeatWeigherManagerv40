namespace MeatWeigherManager
{
    partial class CABM_DeclaracionInsumosPedidoDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CABM_DeclaracionInsumosPedidoDlg));
            this.dataGridView_DetalleInsumos = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.groupBox_registro = new System.Windows.Forms.GroupBox();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.textBox_searchInsumo = new System.Windows.Forms.TextBox();
            this.textBox_unidades = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Insumos = new System.Windows.Forms.ComboBox();
            this.button_Actualizar = new System.Windows.Forms.Button();
            this.button_Agregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox_pedido = new System.Windows.Forms.GroupBox();
            this.label_cliente = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_comprobante = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_nroPedido = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetalleInsumos)).BeginInit();
            this.groupBox_registro.SuspendLayout();
            this.groupBox_pedido.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_DetalleInsumos
            // 
            this.dataGridView_DetalleInsumos.AllowUserToAddRows = false;
            this.dataGridView_DetalleInsumos.AllowUserToOrderColumns = true;
            this.dataGridView_DetalleInsumos.AllowUserToResizeRows = false;
            this.dataGridView_DetalleInsumos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_DetalleInsumos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_DetalleInsumos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_DetalleInsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DetalleInsumos.Location = new System.Drawing.Point(12, 240);
            this.dataGridView_DetalleInsumos.Name = "dataGridView_DetalleInsumos";
            this.dataGridView_DetalleInsumos.ReadOnly = true;
            this.dataGridView_DetalleInsumos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_DetalleInsumos.Size = new System.Drawing.Size(752, 207);
            this.dataGridView_DetalleInsumos.StandardTab = true;
            this.dataGridView_DetalleInsumos.TabIndex = 14;
            this.dataGridView_DetalleInsumos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_DetalleInsumosPrimarios_CellClick);
            this.dataGridView_DetalleInsumos.SelectionChanged += new System.EventHandler(this.dataGridView_DetalleInsumosPrimariosManager_SelectionChanged);
            this.dataGridView_DetalleInsumos.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_DetalleInsumos_UserDeletingRow);
            this.dataGridView_DetalleInsumos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_DetalleInsumos_KeyDown);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(688, 453);
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
            this.button_Cancelar.Location = new System.Drawing.Point(606, 453);
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
            this.groupBox_registro.Controls.Add(this.button_eliminar);
            this.groupBox_registro.Controls.Add(this.textBox_searchInsumo);
            this.groupBox_registro.Controls.Add(this.textBox_unidades);
            this.groupBox_registro.Controls.Add(this.label4);
            this.groupBox_registro.Controls.Add(this.label2);
            this.groupBox_registro.Controls.Add(this.comboBox_Insumos);
            this.groupBox_registro.Controls.Add(this.button_Actualizar);
            this.groupBox_registro.Controls.Add(this.button_Agregar);
            this.groupBox_registro.Location = new System.Drawing.Point(12, 115);
            this.groupBox_registro.Name = "groupBox_registro";
            this.groupBox_registro.Size = new System.Drawing.Size(752, 97);
            this.groupBox_registro.TabIndex = 21;
            this.groupBox_registro.TabStop = false;
            this.groupBox_registro.Text = "Registro";
            // 
            // button_eliminar
            // 
            this.button_eliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_eliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_eliminar.ForeColor = System.Drawing.Color.Green;
            this.button_eliminar.Image = ((System.Drawing.Image)(resources.GetObject("button_eliminar.Image")));
            this.button_eliminar.Location = new System.Drawing.Point(674, 18);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(71, 62);
            this.button_eliminar.TabIndex = 55;
            this.button_eliminar.TabStop = false;
            this.button_eliminar.UseVisualStyleBackColor = true;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
            // 
            // textBox_searchInsumo
            // 
            this.textBox_searchInsumo.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_searchInsumo.Location = new System.Drawing.Point(208, 28);
            this.textBox_searchInsumo.MaxLength = 50;
            this.textBox_searchInsumo.Name = "textBox_searchInsumo";
            this.textBox_searchInsumo.Size = new System.Drawing.Size(229, 20);
            this.textBox_searchInsumo.TabIndex = 48;
            this.toolTip1.SetToolTip(this.textBox_searchInsumo, "Busqueda por Aproximación");
            this.textBox_searchInsumo.TextChanged += new System.EventHandler(this.textBox_searchInsumo_TextChanged);
            this.textBox_searchInsumo.DoubleClick += new System.EventHandler(this.textBox_searchNombreAprox_DoubleClick);
            // 
            // textBox_unidades
            // 
            this.textBox_unidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_unidades.Location = new System.Drawing.Point(451, 53);
            this.textBox_unidades.MaxLength = 6;
            this.textBox_unidades.Name = "textBox_unidades";
            this.textBox_unidades.Size = new System.Drawing.Size(49, 26);
            this.textBox_unidades.TabIndex = 6;
            this.textBox_unidades.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_unidades.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_unidades_KeyPress);
            this.textBox_unidades.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_unidades_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(448, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Unidades";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "INSUMO";
            // 
            // comboBox_Insumos
            // 
            this.comboBox_Insumos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Insumos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Insumos.FormattingEnabled = true;
            this.comboBox_Insumos.Location = new System.Drawing.Point(6, 53);
            this.comboBox_Insumos.Name = "comboBox_Insumos";
            this.comboBox_Insumos.Size = new System.Drawing.Size(431, 28);
            this.comboBox_Insumos.TabIndex = 1;
            this.comboBox_Insumos.SelectedIndexChanged += new System.EventHandler(this.comboBox_Insumos_SelectedIndexChanged);
            // 
            // button_Actualizar
            // 
            this.button_Actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Actualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Actualizar.ForeColor = System.Drawing.Color.Green;
            this.button_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("button_Actualizar.Image")));
            this.button_Actualizar.Location = new System.Drawing.Point(597, 17);
            this.button_Actualizar.Name = "button_Actualizar";
            this.button_Actualizar.Size = new System.Drawing.Size(71, 62);
            this.button_Actualizar.TabIndex = 11;
            this.button_Actualizar.UseVisualStyleBackColor = true;
            this.button_Actualizar.Click += new System.EventHandler(this.button_Actualizar_Click);
            // 
            // button_Agregar
            // 
            this.button_Agregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Agregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Agregar.ForeColor = System.Drawing.Color.Transparent;
            this.button_Agregar.Image = ((System.Drawing.Image)(resources.GetObject("button_Agregar.Image")));
            this.button_Agregar.Location = new System.Drawing.Point(519, 18);
            this.button_Agregar.Name = "button_Agregar";
            this.button_Agregar.Size = new System.Drawing.Size(72, 62);
            this.button_Agregar.TabIndex = 9;
            this.button_Agregar.UseVisualStyleBackColor = true;
            this.button_Agregar.Click += new System.EventHandler(this.Button_Agregar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Insumos";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // groupBox_pedido
            // 
            this.groupBox_pedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_pedido.Controls.Add(this.label_cliente);
            this.groupBox_pedido.Controls.Add(this.label8);
            this.groupBox_pedido.Controls.Add(this.label_comprobante);
            this.groupBox_pedido.Controls.Add(this.label7);
            this.groupBox_pedido.Controls.Add(this.label_nroPedido);
            this.groupBox_pedido.Controls.Add(this.label3);
            this.groupBox_pedido.Location = new System.Drawing.Point(12, 12);
            this.groupBox_pedido.Name = "groupBox_pedido";
            this.groupBox_pedido.Size = new System.Drawing.Size(752, 97);
            this.groupBox_pedido.TabIndex = 22;
            this.groupBox_pedido.TabStop = false;
            this.groupBox_pedido.Text = "Pedido";
            // 
            // label_cliente
            // 
            this.label_cliente.AutoSize = true;
            this.label_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cliente.Location = new System.Drawing.Point(59, 56);
            this.label_cliente.Name = "label_cliente";
            this.label_cliente.Size = new System.Drawing.Size(17, 22);
            this.label_cliente.TabIndex = 52;
            this.label_cliente.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Cliente:";
            // 
            // label_comprobante
            // 
            this.label_comprobante.AutoSize = true;
            this.label_comprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_comprobante.Location = new System.Drawing.Point(278, 20);
            this.label_comprobante.Name = "label_comprobante";
            this.label_comprobante.Size = new System.Drawing.Size(17, 22);
            this.label_comprobante.TabIndex = 50;
            this.label_comprobante.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(199, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Comprobante:";
            // 
            // label_nroPedido
            // 
            this.label_nroPedido.AutoSize = true;
            this.label_nroPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_nroPedido.Location = new System.Drawing.Point(59, 20);
            this.label_nroPedido.Name = "label_nroPedido";
            this.label_nroPedido.Size = new System.Drawing.Size(17, 22);
            this.label_nroPedido.TabIndex = 48;
            this.label_nroPedido.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Nº:";
            // 
            // CABM_DeclaracionInsumosPedidoDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(769, 492);
            this.Controls.Add(this.groupBox_pedido);
            this.Controls.Add(this.groupBox_registro);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.dataGridView_DetalleInsumos);
            this.Controls.Add(this.button_Aceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CABM_DeclaracionInsumosPedidoDlg";
            this.Text = "MeatWeigherManager    -  Declaración de Insumos en Despacho";
            this.Load += new System.EventHandler(this.CABM_ProductoInsumosManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetalleInsumos)).EndInit();
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.groupBox_pedido.ResumeLayout(false);
            this.groupBox_pedido.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_DetalleInsumos;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.GroupBox groupBox_registro;
        private System.Windows.Forms.Button button_Agregar;
        private System.Windows.Forms.Button button_Actualizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Insumos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_unidades;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_searchInsumo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.GroupBox groupBox_pedido;
        private System.Windows.Forms.Label label_cliente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_comprobante;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_nroPedido;
        private System.Windows.Forms.Label label3;
    }
}