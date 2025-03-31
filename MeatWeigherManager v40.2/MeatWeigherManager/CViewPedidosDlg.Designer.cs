namespace MeatWeigherManager
{
    partial class CViewPedidosDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CViewPedidosDlg));
            this.dataGridView_PedidosActivos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Seleccionar = new System.Windows.Forms.Button();
            this.groupBox_Busqueda = new System.Windows.Forms.GroupBox();
            this.textBox_valorBuscar = new System.Windows.Forms.TextBox();
            this.trackBar_dgvOIs = new System.Windows.Forms.TrackBar();
            this.button_up = new System.Windows.Forms.Button();
            this.button_down = new System.Windows.Forms.Button();
            this.dateTimePicker_fechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_todosLosPedidos = new System.Windows.Forms.CheckBox();
            this.button_EditValueFecha = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PedidosActivos)).BeginInit();
            this.groupBox_Busqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_dgvOIs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_PedidosActivos
            // 
            this.dataGridView_PedidosActivos.AllowUserToAddRows = false;
            this.dataGridView_PedidosActivos.AllowUserToDeleteRows = false;
            this.dataGridView_PedidosActivos.AllowUserToOrderColumns = true;
            this.dataGridView_PedidosActivos.AllowUserToResizeRows = false;
            this.dataGridView_PedidosActivos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_PedidosActivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_PedidosActivos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_PedidosActivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_PedidosActivos.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_PedidosActivos.Location = new System.Drawing.Point(67, 82);
            this.dataGridView_PedidosActivos.MultiSelect = false;
            this.dataGridView_PedidosActivos.Name = "dataGridView_PedidosActivos";
            this.dataGridView_PedidosActivos.ReadOnly = true;
            this.dataGridView_PedidosActivos.RowTemplate.Height = 42;
            this.dataGridView_PedidosActivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_PedidosActivos.Size = new System.Drawing.Size(1012, 423);
            this.dataGridView_PedidosActivos.StandardTab = true;
            this.dataGridView_PedidosActivos.TabIndex = 0;
            this.dataGridView_PedidosActivos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_PedidosActivos_CellDoubleClick);
            this.dataGridView_PedidosActivos.SelectionChanged += new System.EventHandler(this.dataGridView_Pedidos_SelectionChanged);
            this.dataGridView_PedidosActivos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView_PedidosActivos_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pedidos Activos";
            // 
            // button_Seleccionar
            // 
            this.button_Seleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Seleccionar.Location = new System.Drawing.Point(69, 18);
            this.button_Seleccionar.Name = "button_Seleccionar";
            this.button_Seleccionar.Size = new System.Drawing.Size(139, 31);
            this.button_Seleccionar.TabIndex = 2;
            this.button_Seleccionar.Text = "&Seleccionar";
            this.button_Seleccionar.UseVisualStyleBackColor = true;
            this.button_Seleccionar.Visible = false;
            this.button_Seleccionar.Click += new System.EventHandler(this.button_Seleccionar_Click);
            // 
            // groupBox_Busqueda
            // 
            this.groupBox_Busqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Busqueda.Controls.Add(this.textBox_valorBuscar);
            this.groupBox_Busqueda.Location = new System.Drawing.Point(921, 9);
            this.groupBox_Busqueda.Name = "groupBox_Busqueda";
            this.groupBox_Busqueda.Size = new System.Drawing.Size(158, 67);
            this.groupBox_Busqueda.TabIndex = 4;
            this.groupBox_Busqueda.TabStop = false;
            this.groupBox_Busqueda.Text = "Busqueda Comprobante";
            // 
            // textBox_valorBuscar
            // 
            this.textBox_valorBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_valorBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_valorBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_valorBuscar.Location = new System.Drawing.Point(6, 35);
            this.textBox_valorBuscar.MaxLength = 13;
            this.textBox_valorBuscar.Name = "textBox_valorBuscar";
            this.textBox_valorBuscar.Size = new System.Drawing.Size(146, 26);
            this.textBox_valorBuscar.TabIndex = 3;
            this.textBox_valorBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_valorBuscar.TextChanged += new System.EventHandler(this.textBox_valorBuscar_TextChanged);
            this.textBox_valorBuscar.DoubleClick += new System.EventHandler(this.textBox_valorBuscar_DoubleClick);
            // 
            // trackBar_dgvOIs
            // 
            this.trackBar_dgvOIs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar_dgvOIs.Location = new System.Drawing.Point(12, 121);
            this.trackBar_dgvOIs.Name = "trackBar_dgvOIs";
            this.trackBar_dgvOIs.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_dgvOIs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar_dgvOIs.Size = new System.Drawing.Size(45, 384);
            this.trackBar_dgvOIs.TabIndex = 68;
            this.trackBar_dgvOIs.Scroll += new System.EventHandler(this.trackBar_dgv_Scroll);
            // 
            // button_up
            // 
            this.button_up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_up.BackgroundImage")));
            this.button_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_up.ForeColor = System.Drawing.Color.Transparent;
            this.button_up.Location = new System.Drawing.Point(6, 12);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(51, 42);
            this.button_up.TabIndex = 69;
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_down
            // 
            this.button_down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_down.BackgroundImage")));
            this.button_down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_down.ForeColor = System.Drawing.Color.Transparent;
            this.button_down.Location = new System.Drawing.Point(6, 68);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(51, 42);
            this.button_down.TabIndex = 70;
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // dateTimePicker_fechaEntrega
            // 
            this.dateTimePicker_fechaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_fechaEntrega.Location = new System.Drawing.Point(6, 38);
            this.dateTimePicker_fechaEntrega.Name = "dateTimePicker_fechaEntrega";
            this.dateTimePicker_fechaEntrega.Size = new System.Drawing.Size(223, 21);
            this.dateTimePicker_fechaEntrega.TabIndex = 71;
            this.dateTimePicker_fechaEntrega.ValueChanged += new System.EventHandler(this.dateTimePicker_fechaEntrega_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_todosLosPedidos);
            this.groupBox1.Controls.Add(this.button_EditValueFecha);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker_fechaEntrega);
            this.groupBox1.Location = new System.Drawing.Point(214, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 67);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // checkBox_todosLosPedidos
            // 
            this.checkBox_todosLosPedidos.AutoSize = true;
            this.checkBox_todosLosPedidos.Location = new System.Drawing.Point(302, 42);
            this.checkBox_todosLosPedidos.Name = "checkBox_todosLosPedidos";
            this.checkBox_todosLosPedidos.Size = new System.Drawing.Size(162, 17);
            this.checkBox_todosLosPedidos.TabIndex = 76;
            this.checkBox_todosLosPedidos.Text = "Mostrar todos los Pendientes";
            this.checkBox_todosLosPedidos.UseVisualStyleBackColor = true;
            this.checkBox_todosLosPedidos.CheckedChanged += new System.EventHandler(this.checkBox_todosLosPedidos_CheckedChanged);
            // 
            // button_EditValueFecha
            // 
            this.button_EditValueFecha.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_EditValueFecha.BackgroundImage")));
            this.button_EditValueFecha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_EditValueFecha.Location = new System.Drawing.Point(235, 19);
            this.button_EditValueFecha.Name = "button_EditValueFecha";
            this.button_EditValueFecha.Size = new System.Drawing.Size(45, 41);
            this.button_EditValueFecha.TabIndex = 75;
            this.button_EditValueFecha.UseVisualStyleBackColor = true;
            this.button_EditValueFecha.Click += new System.EventHandler(this.button_EditValueFecha_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Fecha de Entrega";
            // 
            // CViewPedidosDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 542);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_down);
            this.Controls.Add(this.button_up);
            this.Controls.Add(this.trackBar_dgvOIs);
            this.Controls.Add(this.groupBox_Busqueda);
            this.Controls.Add(this.button_Seleccionar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_PedidosActivos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CViewPedidosDlg";
            this.Text = "MeatWeigherManager    -  (Listado de Pedidos Activos)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CViewLoteCreadosDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PedidosActivos)).EndInit();
            this.groupBox_Busqueda.ResumeLayout(false);
            this.groupBox_Busqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_dgvOIs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_PedidosActivos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Seleccionar;
        private System.Windows.Forms.GroupBox groupBox_Busqueda;
        private System.Windows.Forms.TextBox textBox_valorBuscar;
        private System.Windows.Forms.TrackBar trackBar_dgvOIs;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.Button button_down;
        private System.Windows.Forms.DateTimePicker dateTimePicker_fechaEntrega;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_EditValueFecha;
        private System.Windows.Forms.CheckBox checkBox_todosLosPedidos;
    }
}