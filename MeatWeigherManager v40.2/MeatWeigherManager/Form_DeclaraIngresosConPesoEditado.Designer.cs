namespace ListViewExtendedItem
{
    partial class Form_DeclaraIngresosConPesoEditado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DeclaraIngresosConPesoEditado));
            this.ListView_Ingresos = new System.Windows.Forms.ListView();
            this.colProductoNombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnidades = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPesoTara = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPesoNeto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTropa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipificacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Eliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel_ingreso = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_nombreProducto = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_unidadesContenidas = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_pesoNeto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_pesoTara = new System.Windows.Forms.TextBox();
            this.label_tropa = new System.Windows.Forms.Label();
            this.textBox_tropa = new System.Windows.Forms.TextBox();
            this.label_Tipificacion = new System.Windows.Forms.Label();
            this.comboBox_tipificaciones = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_bultos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_totalUnidadesIngresos = new System.Windows.Forms.Label();
            this.label_pesoTotalNeto = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_pesoTotalBruto = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.contextMenuListView.SuspendLayout();
            this.tableLayoutPanel_ingreso.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView_Ingresos
            // 
            this.ListView_Ingresos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Ingresos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProductoNombre,
            this.colUnidades,
            this.colPesoNeto,
            this.colPesoTara,
            this.colTropa,
            this.colTipificacion});
            this.ListView_Ingresos.ContextMenuStrip = this.contextMenuListView;
            this.ListView_Ingresos.FullRowSelect = true;
            this.ListView_Ingresos.HideSelection = false;
            this.ListView_Ingresos.Location = new System.Drawing.Point(12, 12);
            this.ListView_Ingresos.Name = "ListView_Ingresos";
            this.ListView_Ingresos.Size = new System.Drawing.Size(636, 421);
            this.ListView_Ingresos.TabIndex = 0;
            this.ListView_Ingresos.UseCompatibleStateImageBehavior = false;
            this.ListView_Ingresos.View = System.Windows.Forms.View.Details;
            this.ListView_Ingresos.SelectedIndexChanged += new System.EventHandler(this.ListViewIngresos_SelectedIndexChanged);
            this.ListView_Ingresos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListViewIngresos_MouseUp);
            // 
            // colProductoNombre
            // 
            this.colProductoNombre.Text = "PRODUCTO";
            this.colProductoNombre.Width = 157;
            // 
            // colUnidades
            // 
            this.colUnidades.DisplayIndex = 4;
            this.colUnidades.Text = "Unidades";
            this.colUnidades.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colPesoTara
            // 
            this.colPesoTara.DisplayIndex = 5;
            this.colPesoTara.Text = "Peso Tara";
            this.colPesoTara.Width = 80;
            // 
            // colPesoNeto
            // 
            this.colPesoNeto.DisplayIndex = 3;
            this.colPesoNeto.Text = "Peso Neto";
            this.colPesoNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPesoNeto.Width = 80;
            // 
            // colTropa
            // 
            this.colTropa.DisplayIndex = 1;
            this.colTropa.Text = "Tropa";
            this.colTropa.Width = 80;
            // 
            // colTipificacion
            // 
            this.colTipificacion.DisplayIndex = 2;
            this.colTipificacion.Text = "Tipificación";
            this.colTipificacion.Width = 82;
            // 
            // contextMenuListView
            // 
            this.contextMenuListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Eliminar});
            this.contextMenuListView.Name = "contextMenuDataGrid";
            this.contextMenuListView.Size = new System.Drawing.Size(135, 28);
            // 
            // toolStripMenuItem_Eliminar
            // 
            this.toolStripMenuItem_Eliminar.Font = new System.Drawing.Font("Tahoma", 12F);
            this.toolStripMenuItem_Eliminar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_Eliminar.Image")));
            this.toolStripMenuItem_Eliminar.Name = "toolStripMenuItem_Eliminar";
            this.toolStripMenuItem_Eliminar.Size = new System.Drawing.Size(134, 24);
            this.toolStripMenuItem_Eliminar.Text = "Eliminar";
            this.toolStripMenuItem_Eliminar.ToolTipText = "Borrar";
            this.toolStripMenuItem_Eliminar.Click += new System.EventHandler(this.toolStripMenuItem_Eliminar_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(654, 305);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "Nuevo";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(738, 305);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(819, 305);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.Location = new System.Drawing.Point(819, 410);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(738, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_ingreso
            // 
            this.tableLayoutPanel_ingreso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_ingreso.ColumnCount = 2;
            this.tableLayoutPanel_ingreso.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.25F));
            this.tableLayoutPanel_ingreso.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.75F));
            this.tableLayoutPanel_ingreso.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label_nombreProducto, 1, 0);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel_ingreso.Controls.Add(this.textBox_unidadesContenidas, 1, 1);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel_ingreso.Controls.Add(this.textBox_pesoNeto, 1, 2);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel_ingreso.Controls.Add(this.textBox_pesoTara, 1, 3);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label_tropa, 0, 4);
            this.tableLayoutPanel_ingreso.Controls.Add(this.textBox_tropa, 1, 4);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label_Tipificacion, 0, 5);
            this.tableLayoutPanel_ingreso.Controls.Add(this.comboBox_tipificaciones, 1, 5);
            this.tableLayoutPanel_ingreso.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanel_ingreso.Controls.Add(this.textBox_bultos, 1, 6);
            this.tableLayoutPanel_ingreso.Location = new System.Drawing.Point(654, 61);
            this.tableLayoutPanel_ingreso.Name = "tableLayoutPanel_ingreso";
            this.tableLayoutPanel_ingreso.RowCount = 7;
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.53572F));
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.67857F));
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.21428F));
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.56168F));
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel_ingreso.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel_ingreso.Size = new System.Drawing.Size(240, 236);
            this.tableLayoutPanel_ingreso.TabIndex = 7;
            this.tableLayoutPanel_ingreso.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_nombreProducto
            // 
            this.label_nombreProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_nombreProducto.AutoSize = true;
            this.label_nombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_nombreProducto.Location = new System.Drawing.Point(78, 0);
            this.label_nombreProducto.Name = "label_nombreProducto";
            this.label_nombreProducto.Size = new System.Drawing.Size(159, 29);
            this.label_nombreProducto.TabIndex = 4;
            this.label_nombreProducto.Text = "-";
            this.label_nombreProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 40);
            this.label5.TabIndex = 13;
            this.label5.Text = "Unidades Contenidas";
            // 
            // textBox_unidadesContenidas
            // 
            this.textBox_unidadesContenidas.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox_unidadesContenidas.Location = new System.Drawing.Point(78, 32);
            this.textBox_unidadesContenidas.Name = "textBox_unidadesContenidas";
            this.textBox_unidadesContenidas.Size = new System.Drawing.Size(96, 20);
            this.textBox_unidadesContenidas.TabIndex = 9;
            this.textBox_unidadesContenidas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_unidadesContenidas.DoubleClick += new System.EventHandler(this.textBox_unidadesContenidas_DoubleClick);
            this.textBox_unidadesContenidas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericInteger_KeyPress);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 33);
            this.label4.TabIndex = 3;
            this.label4.Text = "Peso Neto";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_pesoNeto
            // 
            this.textBox_pesoNeto.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox_pesoNeto.Location = new System.Drawing.Point(78, 72);
            this.textBox_pesoNeto.Name = "textBox_pesoNeto";
            this.textBox_pesoNeto.Size = new System.Drawing.Size(96, 20);
            this.textBox_pesoNeto.TabIndex = 7;
            this.textBox_pesoNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_pesoNeto.DoubleClick += new System.EventHandler(this.textBox_pesoNeto_DoubleClick);
            this.textBox_pesoNeto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericFloat_KeyPress);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 28);
            this.label9.TabIndex = 23;
            this.label9.Text = "Peso Tara";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_pesoTara
            // 
            this.textBox_pesoTara.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox_pesoTara.Location = new System.Drawing.Point(78, 105);
            this.textBox_pesoTara.Name = "textBox_pesoTara";
            this.textBox_pesoTara.Size = new System.Drawing.Size(96, 20);
            this.textBox_pesoTara.TabIndex = 22;
            this.textBox_pesoTara.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_pesoTara.DoubleClick += new System.EventHandler(this.textBox_pesoTara_DoubleClick);
            this.textBox_pesoTara.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericFloat_KeyPress);
            // 
            // label_tropa
            // 
            this.label_tropa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_tropa.AutoSize = true;
            this.label_tropa.Location = new System.Drawing.Point(3, 130);
            this.label_tropa.Name = "label_tropa";
            this.label_tropa.Size = new System.Drawing.Size(69, 41);
            this.label_tropa.TabIndex = 10;
            this.label_tropa.Text = "Tropa";
            // 
            // textBox_tropa
            // 
            this.textBox_tropa.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox_tropa.Location = new System.Drawing.Point(78, 133);
            this.textBox_tropa.Name = "textBox_tropa";
            this.textBox_tropa.Size = new System.Drawing.Size(96, 20);
            this.textBox_tropa.TabIndex = 11;
            this.textBox_tropa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_tropa.DoubleClick += new System.EventHandler(this.textBox_tropa_DoubleClick);
            this.textBox_tropa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericInteger_KeyPress);
            // 
            // label_Tipificacion
            // 
            this.label_Tipificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Tipificacion.AutoSize = true;
            this.label_Tipificacion.Location = new System.Drawing.Point(3, 171);
            this.label_Tipificacion.Name = "label_Tipificacion";
            this.label_Tipificacion.Size = new System.Drawing.Size(69, 34);
            this.label_Tipificacion.TabIndex = 14;
            this.label_Tipificacion.Text = "Tipificacion";
            // 
            // comboBox_tipificaciones
            // 
            this.comboBox_tipificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_tipificaciones.FormattingEnabled = true;
            this.comboBox_tipificaciones.Location = new System.Drawing.Point(78, 174);
            this.comboBox_tipificaciones.Name = "comboBox_tipificaciones";
            this.comboBox_tipificaciones.Size = new System.Drawing.Size(159, 21);
            this.comboBox_tipificaciones.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Bultos";
            // 
            // textBox_bultos
            // 
            this.textBox_bultos.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox_bultos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_bultos.ForeColor = System.Drawing.Color.Red;
            this.textBox_bultos.Location = new System.Drawing.Point(78, 208);
            this.textBox_bultos.Name = "textBox_bultos";
            this.textBox_bultos.Size = new System.Drawing.Size(96, 21);
            this.textBox_bultos.TabIndex = 16;
            this.textBox_bultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_bultos.DoubleClick += new System.EventHandler(this.textBox_bultos_DoubleClick);
            this.textBox_bultos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericInteger_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(654, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ingresos Total";
            // 
            // label_totalUnidadesIngresos
            // 
            this.label_totalUnidadesIngresos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_totalUnidadesIngresos.AutoSize = true;
            this.label_totalUnidadesIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_totalUnidadesIngresos.Location = new System.Drawing.Point(681, 32);
            this.label_totalUnidadesIngresos.Name = "label_totalUnidadesIngresos";
            this.label_totalUnidadesIngresos.Size = new System.Drawing.Size(15, 16);
            this.label_totalUnidadesIngresos.TabIndex = 9;
            this.label_totalUnidadesIngresos.Text = "0";
            // 
            // label_pesoTotalNeto
            // 
            this.label_pesoTotalNeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_pesoTotalNeto.AutoSize = true;
            this.label_pesoTotalNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pesoTotalNeto.Location = new System.Drawing.Point(766, 32);
            this.label_pesoTotalNeto.Name = "label_pesoTotalNeto";
            this.label_pesoTotalNeto.Size = new System.Drawing.Size(15, 16);
            this.label_pesoTotalNeto.TabIndex = 11;
            this.label_pesoTotalNeto.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(732, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Peso Total Neto";
            // 
            // label_pesoTotalBruto
            // 
            this.label_pesoTotalBruto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_pesoTotalBruto.AutoSize = true;
            this.label_pesoTotalBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pesoTotalBruto.Location = new System.Drawing.Point(851, 32);
            this.label_pesoTotalBruto.Name = "label_pesoTotalBruto";
            this.label_pesoTotalBruto.Size = new System.Drawing.Size(15, 16);
            this.label_pesoTotalBruto.TabIndex = 13;
            this.label_pesoTotalBruto.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(819, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Peso Total Bruto";
            // 
            // Form_DeclaraIngresosConPesoEditado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 445);
            this.Controls.Add(this.label_pesoTotalBruto);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label_pesoTotalNeto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_totalUnidadesIngresos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableLayoutPanel_ingreso);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.ListView_Ingresos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_DeclaraIngresosConPesoEditado";
            this.Text = "Declaración de Ingresos con Peso Editado";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_DeclaraIngresosConPesoEditado_FormClosing);
            this.contextMenuListView.ResumeLayout(false);
            this.tableLayoutPanel_ingreso.ResumeLayout(false);
            this.tableLayoutPanel_ingreso.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListView_Ingresos;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ColumnHeader colProductoNombre;
        private System.Windows.Forms.ColumnHeader colPesoNeto;
        private System.Windows.Forms.ColumnHeader colTropa;
        private System.Windows.Forms.ColumnHeader colTipificacion;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_ingreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_nombreProducto;
        private System.Windows.Forms.ComboBox comboBox_tipificaciones;
        private System.Windows.Forms.TextBox textBox_pesoNeto;
        private System.Windows.Forms.ContextMenuStrip contextMenuListView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Eliminar;
        private System.Windows.Forms.Label label_tropa;
        private System.Windows.Forms.Label label_Tipificacion;
        private System.Windows.Forms.ColumnHeader colUnidades;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_unidadesContenidas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_bultos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_totalUnidadesIngresos;
        private System.Windows.Forms.Label label_pesoTotalNeto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_tropa;
        private System.Windows.Forms.TextBox textBox_pesoTara;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader colPesoTara;
        private System.Windows.Forms.Label label_pesoTotalBruto;
        private System.Windows.Forms.Label label10;
    }
}

