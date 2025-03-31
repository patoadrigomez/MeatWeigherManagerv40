namespace MeatWeigherManager
{
    partial class CABM_ProveedoresDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CABM_ProveedoresDlg));
            this.dataGridView_Empresas = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.textBox_nombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_registro = new System.Windows.Forms.GroupBox();
            this.groupBox_ProductosVinculados = new System.Windows.Forms.GroupBox();
            this.listBox_productosVinculados = new System.Windows.Forms.ListBox();
            this.button_desvincularProducto = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button_vincularProducto = new System.Windows.Forms.Button();
            this.listBox_Productos = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_listBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Agregar = new System.Windows.Forms.ToolStripMenuItem();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox_vehiculosVinculados = new System.Windows.Forms.GroupBox();
            this.listBox_vehiculosVinculados = new System.Windows.Forms.ListBox();
            this.button_desvincularVehiculo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button_vincularVehiculo = new System.Windows.Forms.Button();
            this.listBox_vehiculos = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_tipoEmpresa = new System.Windows.Forms.ComboBox();
            this.label_marcaNuevoRegistro = new System.Windows.Forms.Label();
            this.textBox_cuit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.button_Actualizar = new System.Windows.Forms.Button();
            this.button_Nuevo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_nombreBuscar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Empresas)).BeginInit();
            this.groupBox_registro.SuspendLayout();
            this.groupBox_ProductosVinculados.SuspendLayout();
            this.contextMenuStrip_listBox.SuspendLayout();
            this.groupBox_vehiculosVinculados.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_Empresas
            // 
            this.dataGridView_Empresas.AllowUserToAddRows = false;
            this.dataGridView_Empresas.AllowUserToOrderColumns = true;
            this.dataGridView_Empresas.AllowUserToResizeRows = false;
            this.dataGridView_Empresas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Empresas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_Empresas.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_Empresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Empresas.Location = new System.Drawing.Point(12, 418);
            this.dataGridView_Empresas.MultiSelect = false;
            this.dataGridView_Empresas.Name = "dataGridView_Empresas";
            this.dataGridView_Empresas.ReadOnly = true;
            this.dataGridView_Empresas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Empresas.Size = new System.Drawing.Size(700, 226);
            this.dataGridView_Empresas.StandardTab = true;
            this.dataGridView_Empresas.TabIndex = 9;
            this.dataGridView_Empresas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Empresas_CellDoubleClick);
            this.dataGridView_Empresas.SelectionChanged += new System.EventHandler(this.dataGridView_Empresas_SelectionChanged);
            this.dataGridView_Empresas.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_Empresas_UserDeletingRow);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(642, 653);
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
            this.button_Cancelar.Location = new System.Drawing.Point(560, 653);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 11;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // textBox_nombre
            // 
            this.textBox_nombre.Location = new System.Drawing.Point(67, 45);
            this.textBox_nombre.MaxLength = 50;
            this.textBox_nombre.Name = "textBox_nombre";
            this.textBox_nombre.Size = new System.Drawing.Size(349, 20);
            this.textBox_nombre.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nombre";
            // 
            // groupBox_registro
            // 
            this.groupBox_registro.Controls.Add(this.groupBox_ProductosVinculados);
            this.groupBox_registro.Controls.Add(this.groupBox_vehiculosVinculados);
            this.groupBox_registro.Controls.Add(this.label3);
            this.groupBox_registro.Controls.Add(this.comboBox_tipoEmpresa);
            this.groupBox_registro.Controls.Add(this.label_marcaNuevoRegistro);
            this.groupBox_registro.Controls.Add(this.textBox_cuit);
            this.groupBox_registro.Controls.Add(this.label4);
            this.groupBox_registro.Controls.Add(this.textBox_ID);
            this.groupBox_registro.Controls.Add(this.label10);
            this.groupBox_registro.Controls.Add(this.button_eliminar);
            this.groupBox_registro.Controls.Add(this.button_Actualizar);
            this.groupBox_registro.Controls.Add(this.button_Nuevo);
            this.groupBox_registro.Controls.Add(this.textBox_nombre);
            this.groupBox_registro.Controls.Add(this.label2);
            this.groupBox_registro.Location = new System.Drawing.Point(12, 12);
            this.groupBox_registro.Name = "groupBox_registro";
            this.groupBox_registro.Size = new System.Drawing.Size(702, 363);
            this.groupBox_registro.TabIndex = 21;
            this.groupBox_registro.TabStop = false;
            this.groupBox_registro.Text = "Registro";
            // 
            // groupBox_ProductosVinculados
            // 
            this.groupBox_ProductosVinculados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_ProductosVinculados.Controls.Add(this.listBox_productosVinculados);
            this.groupBox_ProductosVinculados.Controls.Add(this.button_desvincularProducto);
            this.groupBox_ProductosVinculados.Controls.Add(this.label9);
            this.groupBox_ProductosVinculados.Controls.Add(this.button_vincularProducto);
            this.groupBox_ProductosVinculados.Controls.Add(this.listBox_Productos);
            this.groupBox_ProductosVinculados.Controls.Add(this.label11);
            this.groupBox_ProductosVinculados.Location = new System.Drawing.Point(261, 140);
            this.groupBox_ProductosVinculados.Name = "groupBox_ProductosVinculados";
            this.groupBox_ProductosVinculados.Size = new System.Drawing.Size(435, 217);
            this.groupBox_ProductosVinculados.TabIndex = 43;
            this.groupBox_ProductosVinculados.TabStop = false;
            // 
            // listBox_productosVinculados
            // 
            this.listBox_productosVinculados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_productosVinculados.FormattingEnabled = true;
            this.listBox_productosVinculados.HorizontalScrollbar = true;
            this.listBox_productosVinculados.Location = new System.Drawing.Point(239, 33);
            this.listBox_productosVinculados.Name = "listBox_productosVinculados";
            this.listBox_productosVinculados.Size = new System.Drawing.Size(187, 173);
            this.listBox_productosVinculados.TabIndex = 36;
            // 
            // button_desvincularProducto
            // 
            this.button_desvincularProducto.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_desvincularProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_desvincularProducto.BackgroundImage")));
            this.button_desvincularProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_desvincularProducto.Location = new System.Drawing.Point(198, 81);
            this.button_desvincularProducto.Name = "button_desvincularProducto";
            this.button_desvincularProducto.Size = new System.Drawing.Size(36, 27);
            this.button_desvincularProducto.TabIndex = 41;
            this.button_desvincularProducto.UseVisualStyleBackColor = false;
            this.button_desvincularProducto.Click += new System.EventHandler(this.button_desvincularProducto_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(236, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Vinculados";
            // 
            // button_vincularProducto
            // 
            this.button_vincularProducto.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_vincularProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_vincularProducto.BackgroundImage")));
            this.button_vincularProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_vincularProducto.Location = new System.Drawing.Point(198, 48);
            this.button_vincularProducto.Name = "button_vincularProducto";
            this.button_vincularProducto.Size = new System.Drawing.Size(36, 27);
            this.button_vincularProducto.TabIndex = 40;
            this.button_vincularProducto.UseVisualStyleBackColor = false;
            this.button_vincularProducto.Click += new System.EventHandler(this.button_vincularProducto_Click);
            // 
            // listBox_Productos
            // 
            this.listBox_Productos.ContextMenuStrip = this.contextMenuStrip_listBox;
            this.listBox_Productos.FormattingEnabled = true;
            this.listBox_Productos.HorizontalScrollbar = true;
            this.listBox_Productos.Location = new System.Drawing.Point(6, 33);
            this.listBox_Productos.Name = "listBox_Productos";
            this.listBox_Productos.Size = new System.Drawing.Size(187, 173);
            this.listBox_Productos.TabIndex = 38;
            // 
            // contextMenuStrip_listBox
            // 
            this.contextMenuStrip_listBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Agregar});
            this.contextMenuStrip_listBox.Name = "contextMenuStrip_comboBox";
            this.contextMenuStrip_listBox.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStrip_listBox.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_listBox_ItemClicked);
            // 
            // toolStripMenuItem_Agregar
            // 
            this.toolStripMenuItem_Agregar.Name = "toolStripMenuItem_Agregar";
            this.toolStripMenuItem_Agregar.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_Agregar.Text = "Agregar";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Productos";
            // 
            // groupBox_vehiculosVinculados
            // 
            this.groupBox_vehiculosVinculados.Controls.Add(this.listBox_vehiculosVinculados);
            this.groupBox_vehiculosVinculados.Controls.Add(this.button_desvincularVehiculo);
            this.groupBox_vehiculosVinculados.Controls.Add(this.label5);
            this.groupBox_vehiculosVinculados.Controls.Add(this.button_vincularVehiculo);
            this.groupBox_vehiculosVinculados.Controls.Add(this.listBox_vehiculos);
            this.groupBox_vehiculosVinculados.Controls.Add(this.label8);
            this.groupBox_vehiculosVinculados.Location = new System.Drawing.Point(14, 140);
            this.groupBox_vehiculosVinculados.Name = "groupBox_vehiculosVinculados";
            this.groupBox_vehiculosVinculados.Size = new System.Drawing.Size(241, 217);
            this.groupBox_vehiculosVinculados.TabIndex = 42;
            this.groupBox_vehiculosVinculados.TabStop = false;
            // 
            // listBox_vehiculosVinculados
            // 
            this.listBox_vehiculosVinculados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_vehiculosVinculados.FormattingEnabled = true;
            this.listBox_vehiculosVinculados.Location = new System.Drawing.Point(145, 33);
            this.listBox_vehiculosVinculados.Name = "listBox_vehiculosVinculados";
            this.listBox_vehiculosVinculados.Size = new System.Drawing.Size(91, 173);
            this.listBox_vehiculosVinculados.TabIndex = 36;
            // 
            // button_desvincularVehiculo
            // 
            this.button_desvincularVehiculo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_desvincularVehiculo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_desvincularVehiculo.BackgroundImage")));
            this.button_desvincularVehiculo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_desvincularVehiculo.Location = new System.Drawing.Point(103, 81);
            this.button_desvincularVehiculo.Name = "button_desvincularVehiculo";
            this.button_desvincularVehiculo.Size = new System.Drawing.Size(36, 27);
            this.button_desvincularVehiculo.TabIndex = 41;
            this.button_desvincularVehiculo.UseVisualStyleBackColor = false;
            this.button_desvincularVehiculo.Click += new System.EventHandler(this.button_desvincularVehiculo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(145, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Vinculados";
            // 
            // button_vincularVehiculo
            // 
            this.button_vincularVehiculo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_vincularVehiculo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_vincularVehiculo.BackgroundImage")));
            this.button_vincularVehiculo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_vincularVehiculo.Location = new System.Drawing.Point(103, 48);
            this.button_vincularVehiculo.Name = "button_vincularVehiculo";
            this.button_vincularVehiculo.Size = new System.Drawing.Size(36, 27);
            this.button_vincularVehiculo.TabIndex = 40;
            this.button_vincularVehiculo.UseVisualStyleBackColor = false;
            this.button_vincularVehiculo.Click += new System.EventHandler(this.button_vincularVehiculo_Click);
            // 
            // listBox_vehiculos
            // 
            this.listBox_vehiculos.ContextMenuStrip = this.contextMenuStrip_listBox;
            this.listBox_vehiculos.FormattingEnabled = true;
            this.listBox_vehiculos.Location = new System.Drawing.Point(6, 33);
            this.listBox_vehiculos.Name = "listBox_vehiculos";
            this.listBox_vehiculos.Size = new System.Drawing.Size(91, 173);
            this.listBox_vehiculos.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Vehiculos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Tipo";
            // 
            // comboBox_tipoEmpresa
            // 
            this.comboBox_tipoEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tipoEmpresa.FormattingEnabled = true;
            this.comboBox_tipoEmpresa.Items.AddRange(new object[] {
            "CLIENTE",
            "PROVEEDOR"});
            this.comboBox_tipoEmpresa.Location = new System.Drawing.Point(67, 97);
            this.comboBox_tipoEmpresa.Name = "comboBox_tipoEmpresa";
            this.comboBox_tipoEmpresa.Size = new System.Drawing.Size(145, 21);
            this.comboBox_tipoEmpresa.TabIndex = 3;
            // 
            // label_marcaNuevoRegistro
            // 
            this.label_marcaNuevoRegistro.AutoSize = true;
            this.label_marcaNuevoRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_marcaNuevoRegistro.ForeColor = System.Drawing.Color.Red;
            this.label_marcaNuevoRegistro.Location = new System.Drawing.Point(170, 19);
            this.label_marcaNuevoRegistro.Name = "label_marcaNuevoRegistro";
            this.label_marcaNuevoRegistro.Size = new System.Drawing.Size(18, 24);
            this.label_marcaNuevoRegistro.TabIndex = 29;
            this.label_marcaNuevoRegistro.Text = "*";
            this.label_marcaNuevoRegistro.Visible = false;
            // 
            // textBox_cuit
            // 
            this.textBox_cuit.Location = new System.Drawing.Point(67, 71);
            this.textBox_cuit.MaxLength = 15;
            this.textBox_cuit.Name = "textBox_cuit";
            this.textBox_cuit.Size = new System.Drawing.Size(129, 20);
            this.textBox_cuit.TabIndex = 2;
            this.textBox_cuit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_cuit_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Cuit";
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(67, 19);
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
            this.button_eliminar.Location = new System.Drawing.Point(625, 19);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(71, 62);
            this.button_eliminar.TabIndex = 7;
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
            this.button_Actualizar.Location = new System.Drawing.Point(548, 19);
            this.button_Actualizar.Name = "button_Actualizar";
            this.button_Actualizar.Size = new System.Drawing.Size(71, 62);
            this.button_Actualizar.TabIndex = 5;
            this.button_Actualizar.UseVisualStyleBackColor = true;
            this.button_Actualizar.Click += new System.EventHandler(this.button_Actualizar_Click);
            // 
            // button_Nuevo
            // 
            this.button_Nuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Nuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Nuevo.ForeColor = System.Drawing.Color.Transparent;
            this.button_Nuevo.Image = ((System.Drawing.Image)(resources.GetObject("button_Nuevo.Image")));
            this.button_Nuevo.Location = new System.Drawing.Point(470, 19);
            this.button_Nuevo.Name = "button_Nuevo";
            this.button_Nuevo.Size = new System.Drawing.Size(72, 62);
            this.button_Nuevo.TabIndex = 8;
            this.button_Nuevo.UseVisualStyleBackColor = true;
            this.button_Nuevo.Click += new System.EventHandler(this.button_Nuevo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(659, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Empresas";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(11, 395);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Buscar Por Nombre";
            // 
            // textBox_nombreBuscar
            // 
            this.textBox_nombreBuscar.BackColor = System.Drawing.Color.Black;
            this.textBox_nombreBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombreBuscar.ForeColor = System.Drawing.Color.White;
            this.textBox_nombreBuscar.Location = new System.Drawing.Point(152, 393);
            this.textBox_nombreBuscar.Name = "textBox_nombreBuscar";
            this.textBox_nombreBuscar.Size = new System.Drawing.Size(234, 21);
            this.textBox_nombreBuscar.TabIndex = 32;
            this.textBox_nombreBuscar.TextChanged += new System.EventHandler(this.textBox_nombreBuscar_TextChanged);
            // 
            // CABM_ProveedoresDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(724, 687);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_nombreBuscar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.dataGridView_Empresas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox_registro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CABM_ProveedoresDlg";
            this.Text = "MeatWeigherManager    -  Mantenimiento de Tabla de Empresas";
            this.Load += new System.EventHandler(this.CABM_Empresas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Empresas)).EndInit();
            this.groupBox_registro.ResumeLayout(false);
            this.groupBox_registro.PerformLayout();
            this.groupBox_ProductosVinculados.ResumeLayout(false);
            this.groupBox_ProductosVinculados.PerformLayout();
            this.contextMenuStrip_listBox.ResumeLayout(false);
            this.groupBox_vehiculosVinculados.ResumeLayout(false);
            this.groupBox_vehiculosVinculados.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Empresas;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.TextBox textBox_nombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_registro;
        private System.Windows.Forms.Button button_Nuevo;
        private System.Windows.Forms.Button button_Actualizar;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_cuit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_nombreBuscar;
        private System.Windows.Forms.Label label_marcaNuevoRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_tipoEmpresa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox_vehiculos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox_vehiculosVinculados;
        private System.Windows.Forms.Button button_vincularVehiculo;
        private System.Windows.Forms.Button button_desvincularVehiculo;
        private System.Windows.Forms.GroupBox groupBox_ProductosVinculados;
        private System.Windows.Forms.ListBox listBox_productosVinculados;
        private System.Windows.Forms.Button button_desvincularProducto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_vincularProducto;
        private System.Windows.Forms.ListBox listBox_Productos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox_vehiculosVinculados;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_listBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Agregar;
    }
}