namespace MeatWeigherManager
{
    partial class Form_EgresoDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_EgresoDlg));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_Consultas = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_consultaLote_Destino_pesaje = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatusScanner = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusScannerValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusProcessValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_buttons = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Iniciar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Parar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_ListarDetalleProductosEnPedidos = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_AbrirPedido = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CerrarPedido = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_ModoEliminarPiezas = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_TituloProceso = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_ModoEliminacion = new System.Windows.Forms.ToolStripLabel();
            this.label_comprobantePedido = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.borrarToolStripMenuItem_Eliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_datCaptura = new System.Windows.Forms.GroupBox();
            this.button_generarEtiquetasPedido = new System.Windows.Forms.Button();
            this.button_declaracionInsumos = new System.Windows.Forms.Button();
            this.button_print = new System.Windows.Forms.Button();
            this.groupBox_pedido = new System.Windows.Forms.GroupBox();
            this.label_modoPedido = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_cliente = new System.Windows.Forms.Label();
            this.label_fechaEntregaPedido = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ledCtrl_readyError = new LedCtrl.LedCtrl();
            this.textBox_valueReadCodBar = new System.Windows.Forms.TextBox();
            this.label_detalleLectura = new System.Windows.Forms.Label();
            this.ledCtrl_readyOK = new LedCtrl.LedCtrl();
            this.dataGridView_totalesPiezasEgresadasPedido = new System.Windows.Forms.DataGridView();
            this.dataGridView_detallePiezasEgresadasPedido = new System.Windows.Forms.DataGridView();
            this.groupBox_detallesPiezasEgresadas = new System.Windows.Forms.GroupBox();
            this.tabControl_ProcesoEscaneo = new System.Windows.Forms.TabControl();
            this.tabPage_ProcesoEscaneo = new System.Windows.Forms.TabPage();
            this.tabPage_listaEscaneo = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip_buttons.SuspendLayout();
            this.contextMenuDataGrid.SuspendLayout();
            this.groupBox_datCaptura.SuspendLayout();
            this.groupBox_pedido.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_totalesPiezasEgresadasPedido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_detallePiezasEgresadasPedido)).BeginInit();
            this.groupBox_detallesPiezasEgresadas.SuspendLayout();
            this.tabControl_ProcesoEscaneo.SuspendLayout();
            this.tabPage_ProcesoEscaneo.SuspendLayout();
            this.tabPage_listaEscaneo.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Consultas,
            this.toolStripMenuItemVersion});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1026, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // ToolStripMenuItem_Consultas
            // 
            this.ToolStripMenuItem_Consultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_consultaLote_Destino_pesaje});
            this.ToolStripMenuItem_Consultas.Name = "ToolStripMenuItem_Consultas";
            this.ToolStripMenuItem_Consultas.Size = new System.Drawing.Size(71, 20);
            this.ToolStripMenuItem_Consultas.Text = "C&onsultas";
            // 
            // ToolStripMenuItem_consultaLote_Destino_pesaje
            // 
            this.ToolStripMenuItem_consultaLote_Destino_pesaje.Name = "ToolStripMenuItem_consultaLote_Destino_pesaje";
            this.ToolStripMenuItem_consultaLote_Destino_pesaje.Size = new System.Drawing.Size(313, 22);
            this.ToolStripMenuItem_consultaLote_Destino_pesaje.Text = "&Operaciones de Piezas Colectadas por Pedido";
            this.ToolStripMenuItem_consultaLote_Destino_pesaje.Click += new System.EventHandler(this.consultaEgresosPedidos_ToolStripMenuItem_Click);
            // 
            // toolStripMenuItemVersion
            // 
            this.toolStripMenuItemVersion.Name = "toolStripMenuItemVersion";
            this.toolStripMenuItemVersion.Size = new System.Drawing.Size(12, 20);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatusScanner,
            this.toolStripStatusScannerValue,
            this.toolStripStatusLabelStatusProcess,
            this.toolStripStatusProcessValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 671);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1029, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatusScanner
            // 
            this.toolStripStatusLabelStatusScanner.Name = "toolStripStatusLabelStatusScanner";
            this.toolStripStatusLabelStatusScanner.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabelStatusScanner.Text = "Escaner:";
            // 
            // toolStripStatusScannerValue
            // 
            this.toolStripStatusScannerValue.Name = "toolStripStatusScannerValue";
            this.toolStripStatusScannerValue.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusScannerValue.Text = "--";
            // 
            // toolStripStatusLabelStatusProcess
            // 
            this.toolStripStatusLabelStatusProcess.Name = "toolStripStatusLabelStatusProcess";
            this.toolStripStatusLabelStatusProcess.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabelStatusProcess.Text = "Estado del Proceso:";
            // 
            // toolStripStatusProcessValue
            // 
            this.toolStripStatusProcessValue.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusProcessValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripStatusProcessValue.Name = "toolStripStatusProcessValue";
            this.toolStripStatusProcessValue.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusProcessValue.Text = "--";
            // 
            // toolStrip_buttons
            // 
            this.toolStrip_buttons.ImageScalingSize = new System.Drawing.Size(42, 42);
            this.toolStrip_buttons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Iniciar,
            this.toolStripButton_Parar,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.toolStripButton_ListarDetalleProductosEnPedidos,
            this.toolStripButton_AbrirPedido,
            this.toolStripButton_CerrarPedido,
            this.toolStripSeparator1,
            this.toolStripButton_ModoEliminarPiezas,
            this.toolStripSeparator2,
            this.toolStripSeparator5,
            this.toolStripSeparator6,
            this.toolStripLabel_TituloProceso,
            this.toolStripLabel_ModoEliminacion});
            this.toolStrip_buttons.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_buttons.Name = "toolStrip_buttons";
            this.toolStrip_buttons.Size = new System.Drawing.Size(1026, 49);
            this.toolStrip_buttons.TabIndex = 2;
            this.toolStrip_buttons.Text = "toolStrip1";
            this.toolStrip_buttons.Visible = false;
            // 
            // toolStripButton_Iniciar
            // 
            this.toolStripButton_Iniciar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Iniciar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Iniciar.Image")));
            this.toolStripButton_Iniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Iniciar.Name = "toolStripButton_Iniciar";
            this.toolStripButton_Iniciar.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_Iniciar.Text = "Iniciar";
            this.toolStripButton_Iniciar.Click += new System.EventHandler(this.toolStripButton_Iniciar_Click);
            // 
            // toolStripButton_Parar
            // 
            this.toolStripButton_Parar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Parar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Parar.Image")));
            this.toolStripButton_Parar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Parar.Name = "toolStripButton_Parar";
            this.toolStripButton_Parar.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_Parar.Text = "Parar";
            this.toolStripButton_Parar.Click += new System.EventHandler(this.toolStripButton_Parar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_ListarDetalleProductosEnPedidos
            // 
            this.toolStripButton_ListarDetalleProductosEnPedidos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ListarDetalleProductosEnPedidos.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_ListarDetalleProductosEnPedidos.Image")));
            this.toolStripButton_ListarDetalleProductosEnPedidos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ListarDetalleProductosEnPedidos.Name = "toolStripButton_ListarDetalleProductosEnPedidos";
            this.toolStripButton_ListarDetalleProductosEnPedidos.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_ListarDetalleProductosEnPedidos.Text = "Abrir Pedido";
            this.toolStripButton_ListarDetalleProductosEnPedidos.ToolTipText = "Listar Detalle de Productos en Pedidos";
            this.toolStripButton_ListarDetalleProductosEnPedidos.Click += new System.EventHandler(this.toolStripButton_ListarDetalleProductosEnPedidos_Click);
            // 
            // toolStripButton_AbrirPedido
            // 
            this.toolStripButton_AbrirPedido.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_AbrirPedido.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_AbrirPedido.Image")));
            this.toolStripButton_AbrirPedido.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AbrirPedido.Name = "toolStripButton_AbrirPedido";
            this.toolStripButton_AbrirPedido.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_AbrirPedido.Text = "Abrir Pedido";
            this.toolStripButton_AbrirPedido.ToolTipText = "Mostrar Pedidos a Procesar";
            this.toolStripButton_AbrirPedido.Click += new System.EventHandler(this.toolStripButton_AbrirPedido_Click);
            // 
            // toolStripButton_CerrarPedido
            // 
            this.toolStripButton_CerrarPedido.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_CerrarPedido.Image = global::MeatWeigherManager.Properties.Resources.candado2;
            this.toolStripButton_CerrarPedido.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CerrarPedido.Name = "toolStripButton_CerrarPedido";
            this.toolStripButton_CerrarPedido.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_CerrarPedido.ToolTipText = "Cerrar Pedido";
            this.toolStripButton_CerrarPedido.Click += new System.EventHandler(this.toolStripButton_CerrarPedido_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_ModoEliminarPiezas
            // 
            this.toolStripButton_ModoEliminarPiezas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ModoEliminarPiezas.Image = global::MeatWeigherManager.Properties.Resources.EliminarPieza;
            this.toolStripButton_ModoEliminarPiezas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ModoEliminarPiezas.Name = "toolStripButton_ModoEliminarPiezas";
            this.toolStripButton_ModoEliminarPiezas.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_ModoEliminarPiezas.ToolTipText = "Alterna entre Modo de Captura Normal y Modo de Eliminación de Piezas";
            this.toolStripButton_ModoEliminarPiezas.Click += new System.EventHandler(this.toolStripButton_ModoEliminarPiezas_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripLabel_TituloProceso
            // 
            this.toolStripLabel_TituloProceso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel_TituloProceso.Name = "toolStripLabel_TituloProceso";
            this.toolStripLabel_TituloProceso.Size = new System.Drawing.Size(147, 46);
            this.toolStripLabel_TituloProceso.Text = "Egresos de Piezas ";
            // 
            // toolStripLabel_ModoEliminacion
            // 
            this.toolStripLabel_ModoEliminacion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel_ModoEliminacion.ForeColor = System.Drawing.Color.DarkRed;
            this.toolStripLabel_ModoEliminacion.Name = "toolStripLabel_ModoEliminacion";
            this.toolStripLabel_ModoEliminacion.Size = new System.Drawing.Size(209, 46);
            this.toolStripLabel_ModoEliminacion.Text = "(EN MODO ELIMINACIÓN)";
            this.toolStripLabel_ModoEliminacion.Visible = false;
            // 
            // label_comprobantePedido
            // 
            this.label_comprobantePedido.AutoSize = true;
            this.label_comprobantePedido.BackColor = System.Drawing.Color.Black;
            this.label_comprobantePedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_comprobantePedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_comprobantePedido.ForeColor = System.Drawing.Color.White;
            this.label_comprobantePedido.Location = new System.Drawing.Point(64, 20);
            this.label_comprobantePedido.Name = "label_comprobantePedido";
            this.label_comprobantePedido.Size = new System.Drawing.Size(176, 26);
            this.label_comprobantePedido.TabIndex = 34;
            this.label_comprobantePedido.Text = "0000-00000000";
            this.label_comprobantePedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "PEDIDO:";
            // 
            // contextMenuDataGrid
            // 
            this.contextMenuDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borrarToolStripMenuItem_Eliminar});
            this.contextMenuDataGrid.Name = "contextMenuDataGrid";
            this.contextMenuDataGrid.Size = new System.Drawing.Size(118, 26);
            // 
            // borrarToolStripMenuItem_Eliminar
            // 
            this.borrarToolStripMenuItem_Eliminar.Image = ((System.Drawing.Image)(resources.GetObject("borrarToolStripMenuItem_Eliminar.Image")));
            this.borrarToolStripMenuItem_Eliminar.Name = "borrarToolStripMenuItem_Eliminar";
            this.borrarToolStripMenuItem_Eliminar.Size = new System.Drawing.Size(117, 22);
            this.borrarToolStripMenuItem_Eliminar.Text = "Eliminar";
            this.borrarToolStripMenuItem_Eliminar.Click += new System.EventHandler(this.borrarToolStripMenuItem_Eliminar_Click);
            // 
            // groupBox_datCaptura
            // 
            this.groupBox_datCaptura.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_datCaptura.Controls.Add(this.button_generarEtiquetasPedido);
            this.groupBox_datCaptura.Controls.Add(this.button_declaracionInsumos);
            this.groupBox_datCaptura.Controls.Add(this.button_print);
            this.groupBox_datCaptura.Controls.Add(this.groupBox_pedido);
            this.groupBox_datCaptura.Controls.Add(this.label3);
            this.groupBox_datCaptura.Controls.Add(this.tableLayoutPanel1);
            this.groupBox_datCaptura.Controls.Add(this.dataGridView_totalesPiezasEgresadasPedido);
            this.groupBox_datCaptura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_datCaptura.Location = new System.Drawing.Point(6, 6);
            this.groupBox_datCaptura.Name = "groupBox_datCaptura";
            this.groupBox_datCaptura.Size = new System.Drawing.Size(1003, 599);
            this.groupBox_datCaptura.TabIndex = 28;
            this.groupBox_datCaptura.TabStop = false;
            this.groupBox_datCaptura.Text = "Captura de Egresos";
            // 
            // button_generarEtiquetasPedido
            // 
            this.button_generarEtiquetasPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generarEtiquetasPedido.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_generarEtiquetasPedido.BackgroundImage")));
            this.button_generarEtiquetasPedido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_generarEtiquetasPedido.Location = new System.Drawing.Point(926, 158);
            this.button_generarEtiquetasPedido.Name = "button_generarEtiquetasPedido";
            this.button_generarEtiquetasPedido.Size = new System.Drawing.Size(71, 40);
            this.button_generarEtiquetasPedido.TabIndex = 74;
            this.toolTip1.SetToolTip(this.button_generarEtiquetasPedido, "Generador de Etiquetas para Bultos");
            this.button_generarEtiquetasPedido.UseVisualStyleBackColor = true;
            this.button_generarEtiquetasPedido.Click += new System.EventHandler(this.button_generarEtiquetasPedido_Click);
            // 
            // button_declaracionInsumos
            // 
            this.button_declaracionInsumos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_declaracionInsumos.BackgroundImage = global::MeatWeigherManager.Properties.Resources.insumos;
            this.button_declaracionInsumos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_declaracionInsumos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_declaracionInsumos.Location = new System.Drawing.Point(926, 26);
            this.button_declaracionInsumos.Name = "button_declaracionInsumos";
            this.button_declaracionInsumos.Size = new System.Drawing.Size(71, 49);
            this.button_declaracionInsumos.TabIndex = 73;
            this.button_declaracionInsumos.Text = "INSUMOS";
            this.toolTip1.SetToolTip(this.button_declaracionInsumos, "Declaracion de Insumos");
            this.button_declaracionInsumos.UseVisualStyleBackColor = true;
            this.button_declaracionInsumos.Click += new System.EventHandler(this.button_declaracionInsumos_Click);
            // 
            // button_print
            // 
            this.button_print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_print.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_print.BackgroundImage")));
            this.button_print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_print.Location = new System.Drawing.Point(926, 100);
            this.button_print.Name = "button_print";
            this.button_print.Size = new System.Drawing.Size(71, 40);
            this.button_print.TabIndex = 72;
            this.button_print.UseVisualStyleBackColor = true;
            this.button_print.Click += new System.EventHandler(this.button_print_Click);
            // 
            // groupBox_pedido
            // 
            this.groupBox_pedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_pedido.Controls.Add(this.label_modoPedido);
            this.groupBox_pedido.Controls.Add(this.label9);
            this.groupBox_pedido.Controls.Add(this.label1);
            this.groupBox_pedido.Controls.Add(this.label_cliente);
            this.groupBox_pedido.Controls.Add(this.label_fechaEntregaPedido);
            this.groupBox_pedido.Controls.Add(this.label6);
            this.groupBox_pedido.Controls.Add(this.label_comprobantePedido);
            this.groupBox_pedido.Controls.Add(this.label8);
            this.groupBox_pedido.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox_pedido.Location = new System.Drawing.Point(6, 20);
            this.groupBox_pedido.Name = "groupBox_pedido";
            this.groupBox_pedido.Size = new System.Drawing.Size(914, 97);
            this.groupBox_pedido.TabIndex = 70;
            this.groupBox_pedido.TabStop = false;
            this.groupBox_pedido.Text = "Pedido";
            // 
            // label_modoPedido
            // 
            this.label_modoPedido.AutoSize = true;
            this.label_modoPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_modoPedido.Location = new System.Drawing.Point(307, 20);
            this.label_modoPedido.Name = "label_modoPedido";
            this.label_modoPedido.Size = new System.Drawing.Size(17, 24);
            this.label_modoPedido.TabIndex = 71;
            this.label_modoPedido.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(257, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 70;
            this.label9.Text = "MODO:";
            // 
            // label_cliente
            // 
            this.label_cliente.AutoSize = true;
            this.label_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cliente.Location = new System.Drawing.Point(95, 60);
            this.label_cliente.Name = "label_cliente";
            this.label_cliente.Size = new System.Drawing.Size(15, 20);
            this.label_cliente.TabIndex = 67;
            this.label_cliente.Text = "-";
            // 
            // label_fechaEntregaPedido
            // 
            this.label_fechaEntregaPedido.AutoSize = true;
            this.label_fechaEntregaPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_fechaEntregaPedido.Location = new System.Drawing.Point(435, 17);
            this.label_fechaEntregaPedido.Name = "label_fechaEntregaPedido";
            this.label_fechaEntregaPedido.Size = new System.Drawing.Size(17, 24);
            this.label_fechaEntregaPedido.TabIndex = 69;
            this.label_fechaEntregaPedido.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "CLIENTE:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(384, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 68;
            this.label8.Text = "FECHA :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(293, 13);
            this.label3.TabIndex = 65;
            this.label3.Text = "TOTALIZADO POR PRODUCTO DE PIEZAS EGRESADAS";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.87344F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.07492F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.83714F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ledCtrl_readyError, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_valueReadCodBar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_detalleLectura, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ledCtrl_readyOK, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 125);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.70588F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.29412F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 72);
            this.tableLayoutPanel1.TabIndex = 64;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(656, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 67;
            this.label7.Text = "DETALLE ";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(384, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "ERROR";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(297, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "OK";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "LECTURA";
            // 
            // ledCtrl_readyError
            // 
            this.ledCtrl_readyError.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ledCtrl_readyError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledCtrl_readyError.ColorOff = System.Drawing.Color.Transparent;
            this.ledCtrl_readyError.ColorOn = System.Drawing.Color.Red;
            this.ledCtrl_readyError.EdgeColor = System.Drawing.Color.Transparent;
            this.ledCtrl_readyError.EdgeWidth = 4;
            this.ledCtrl_readyError.LedStatus = false;
            this.ledCtrl_readyError.Location = new System.Drawing.Point(389, 29);
            this.ledCtrl_readyError.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.ledCtrl_readyError.Name = "ledCtrl_readyError";
            this.ledCtrl_readyError.Size = new System.Drawing.Size(35, 30);
            this.ledCtrl_readyError.TabIndex = 64;
            // 
            // textBox_valueReadCodBar
            // 
            this.textBox_valueReadCodBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_valueReadCodBar.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_valueReadCodBar.Font = new System.Drawing.Font("Digital SF", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_valueReadCodBar.ForeColor = System.Drawing.Color.Lime;
            this.textBox_valueReadCodBar.Location = new System.Drawing.Point(4, 27);
            this.textBox_valueReadCodBar.Name = "textBox_valueReadCodBar";
            this.textBox_valueReadCodBar.ReadOnly = true;
            this.textBox_valueReadCodBar.Size = new System.Drawing.Size(251, 34);
            this.textBox_valueReadCodBar.TabIndex = 36;
            this.textBox_valueReadCodBar.Text = "------";
            this.textBox_valueReadCodBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_valueReadCodBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_valueReadCodBar_MouseDoubleClick);
            // 
            // label_detalleLectura
            // 
            this.label_detalleLectura.AutoSize = true;
            this.label_detalleLectura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detalleLectura.Location = new System.Drawing.Point(460, 17);
            this.label_detalleLectura.Name = "label_detalleLectura";
            this.label_detalleLectura.Size = new System.Drawing.Size(19, 13);
            this.label_detalleLectura.TabIndex = 66;
            this.label_detalleLectura.Text = "----";
            // 
            // ledCtrl_readyOK
            // 
            this.ledCtrl_readyOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ledCtrl_readyOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledCtrl_readyOK.ColorOff = System.Drawing.Color.Transparent;
            this.ledCtrl_readyOK.ColorOn = System.Drawing.Color.ForestGreen;
            this.ledCtrl_readyOK.EdgeColor = System.Drawing.Color.Transparent;
            this.ledCtrl_readyOK.EdgeWidth = 4;
            this.ledCtrl_readyOK.LedStatus = false;
            this.ledCtrl_readyOK.Location = new System.Drawing.Point(291, 29);
            this.ledCtrl_readyOK.Margin = new System.Windows.Forms.Padding(5);
            this.ledCtrl_readyOK.Name = "ledCtrl_readyOK";
            this.ledCtrl_readyOK.Size = new System.Drawing.Size(33, 30);
            this.ledCtrl_readyOK.TabIndex = 62;
            // 
            // dataGridView_totalesPiezasEgresadasPedido
            // 
            this.dataGridView_totalesPiezasEgresadasPedido.AllowUserToAddRows = false;
            this.dataGridView_totalesPiezasEgresadasPedido.AllowUserToDeleteRows = false;
            this.dataGridView_totalesPiezasEgresadasPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_totalesPiezasEgresadasPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_totalesPiezasEgresadasPedido.Location = new System.Drawing.Point(3, 225);
            this.dataGridView_totalesPiezasEgresadasPedido.Name = "dataGridView_totalesPiezasEgresadasPedido";
            this.dataGridView_totalesPiezasEgresadasPedido.ReadOnly = true;
            this.dataGridView_totalesPiezasEgresadasPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_totalesPiezasEgresadasPedido.Size = new System.Drawing.Size(994, 368);
            this.dataGridView_totalesPiezasEgresadasPedido.TabIndex = 61;
            // 
            // dataGridView_detallePiezasEgresadasPedido
            // 
            this.dataGridView_detallePiezasEgresadasPedido.AllowUserToAddRows = false;
            this.dataGridView_detallePiezasEgresadasPedido.AllowUserToDeleteRows = false;
            this.dataGridView_detallePiezasEgresadasPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_detallePiezasEgresadasPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_detallePiezasEgresadasPedido.ContextMenuStrip = this.contextMenuDataGrid;
            this.dataGridView_detallePiezasEgresadasPedido.Location = new System.Drawing.Point(6, 19);
            this.dataGridView_detallePiezasEgresadasPedido.Name = "dataGridView_detallePiezasEgresadasPedido";
            this.dataGridView_detallePiezasEgresadasPedido.ReadOnly = true;
            this.dataGridView_detallePiezasEgresadasPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_detallePiezasEgresadasPedido.Size = new System.Drawing.Size(997, 581);
            this.dataGridView_detallePiezasEgresadasPedido.TabIndex = 60;
            this.dataGridView_detallePiezasEgresadasPedido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_detallePiezasEgresadas_KeyDown);
            // 
            // groupBox_detallesPiezasEgresadas
            // 
            this.groupBox_detallesPiezasEgresadas.Controls.Add(this.dataGridView_detallePiezasEgresadasPedido);
            this.groupBox_detallesPiezasEgresadas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_detallesPiezasEgresadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_detallesPiezasEgresadas.Location = new System.Drawing.Point(3, 3);
            this.groupBox_detallesPiezasEgresadas.Name = "groupBox_detallesPiezasEgresadas";
            this.groupBox_detallesPiezasEgresadas.Size = new System.Drawing.Size(1009, 606);
            this.groupBox_detallesPiezasEgresadas.TabIndex = 62;
            this.groupBox_detallesPiezasEgresadas.TabStop = false;
            this.groupBox_detallesPiezasEgresadas.Text = "Detalle de Piezas Egresadas para el Pedido";
            // 
            // tabControl_ProcesoEscaneo
            // 
            this.tabControl_ProcesoEscaneo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_ProcesoEscaneo.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl_ProcesoEscaneo.Controls.Add(this.tabPage_ProcesoEscaneo);
            this.tabControl_ProcesoEscaneo.Controls.Add(this.tabPage_listaEscaneo);
            this.tabControl_ProcesoEscaneo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_ProcesoEscaneo.Location = new System.Drawing.Point(0, 25);
            this.tabControl_ProcesoEscaneo.Name = "tabControl_ProcesoEscaneo";
            this.tabControl_ProcesoEscaneo.SelectedIndex = 0;
            this.tabControl_ProcesoEscaneo.Size = new System.Drawing.Size(1023, 643);
            this.tabControl_ProcesoEscaneo.TabIndex = 63;
            // 
            // tabPage_ProcesoEscaneo
            // 
            this.tabPage_ProcesoEscaneo.AutoScroll = true;
            this.tabPage_ProcesoEscaneo.Controls.Add(this.groupBox_datCaptura);
            this.tabPage_ProcesoEscaneo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage_ProcesoEscaneo.Location = new System.Drawing.Point(4, 27);
            this.tabPage_ProcesoEscaneo.Name = "tabPage_ProcesoEscaneo";
            this.tabPage_ProcesoEscaneo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ProcesoEscaneo.Size = new System.Drawing.Size(1015, 612);
            this.tabPage_ProcesoEscaneo.TabIndex = 0;
            this.tabPage_ProcesoEscaneo.Text = "Proceso de Escaneo";
            this.tabPage_ProcesoEscaneo.UseVisualStyleBackColor = true;
            // 
            // tabPage_listaEscaneo
            // 
            this.tabPage_listaEscaneo.Controls.Add(this.groupBox_detallesPiezasEgresadas);
            this.tabPage_listaEscaneo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage_listaEscaneo.Location = new System.Drawing.Point(4, 27);
            this.tabPage_listaEscaneo.Name = "tabPage_listaEscaneo";
            this.tabPage_listaEscaneo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_listaEscaneo.Size = new System.Drawing.Size(1015, 612);
            this.tabPage_listaEscaneo.TabIndex = 1;
            this.tabPage_listaEscaneo.Text = "Grilla de Piezas Colectadas";
            this.tabPage_listaEscaneo.UseVisualStyleBackColor = true;
            // 
            // Form_EgresoDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 693);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip_buttons);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl_ProcesoEscaneo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form_EgresoDlg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_EgresoDlg_FormClosing);
            this.Load += new System.EventHandler(this.Form_EgresoDlg_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip_buttons.ResumeLayout(false);
            this.toolStrip_buttons.PerformLayout();
            this.contextMenuDataGrid.ResumeLayout(false);
            this.groupBox_datCaptura.ResumeLayout(false);
            this.groupBox_datCaptura.PerformLayout();
            this.groupBox_pedido.ResumeLayout(false);
            this.groupBox_pedido.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_totalesPiezasEgresadasPedido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_detallePiezasEgresadasPedido)).EndInit();
            this.groupBox_detallesPiezasEgresadas.ResumeLayout(false);
            this.tabControl_ProcesoEscaneo.ResumeLayout(false);
            this.tabPage_ProcesoEscaneo.ResumeLayout(false);
            this.tabPage_listaEscaneo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip_buttons;
        private System.Windows.Forms.ToolStripButton toolStripButton_Iniciar;
        private System.Windows.Forms.ToolStripButton toolStripButton_Parar;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Consultas;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_consultaLote_Destino_pesaje;
        private System.Windows.Forms.GroupBox groupBox_datCaptura;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersion;
        private System.Windows.Forms.DataGridView dataGridView_detallePiezasEgresadasPedido;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataGrid;
        private System.Windows.Forms.GroupBox groupBox_detallesPiezasEgresadas;
        private System.Windows.Forms.TabControl tabControl_ProcesoEscaneo;
        private System.Windows.Forms.TabPage tabPage_ProcesoEscaneo;
        private System.Windows.Forms.TabPage tabPage_listaEscaneo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatusProcess;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProcessValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_comprobantePedido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_TituloProceso;
        private System.Windows.Forms.DataGridView dataGridView_totalesPiezasEgresadasPedido;
        private System.Windows.Forms.TextBox textBox_valueReadCodBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem borrarToolStripMenuItem_Eliminar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private LedCtrl.LedCtrl ledCtrl_readyOK;
        private System.Windows.Forms.Label label5;
        private LedCtrl.LedCtrl ledCtrl_readyError;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_detalleLectura;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatusScanner;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusScannerValue;
        private System.Windows.Forms.GroupBox groupBox_pedido;
        private System.Windows.Forms.Label label_cliente;
        private System.Windows.Forms.Label label_fechaEntregaPedido;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton toolStripButton_AbrirPedido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_CerrarPedido;
        private System.Windows.Forms.ToolStripButton toolStripButton_ModoEliminarPiezas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_ModoEliminacion;
        private System.Windows.Forms.Button button_print;
        private System.Windows.Forms.ToolStripButton toolStripButton_ListarDetalleProductosEnPedidos;
        private System.Windows.Forms.Button button_declaracionInsumos;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_generarEtiquetasPedido;
        private System.Windows.Forms.Label label_modoPedido;
        private System.Windows.Forms.Label label9;
    }
}

