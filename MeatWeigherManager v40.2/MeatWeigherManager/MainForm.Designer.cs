namespace MeatWeigherManager
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ingresoAPlantaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingresoAProducciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesajeEnProducciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesajeDeCajasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preparaciónDeCombosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fraccionamientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferenciasDeDepósitoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.egresosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devolucionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colecciónInventarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionModoDeTrabajoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insumosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajusteDeStockDeInsumosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus_LabelOperador = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Operador = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_LabelEstacion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Estacion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_LabelDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Db = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripButton_IngresoAPlanta = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_IngresoAProduccion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_PesajeEnProduccion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Egresos = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_pesajeCajas = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_preparacionCombos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_fraccionamiento = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_transferenciasDeDeposito = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Devoluciones = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_inventario = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_cambioOperador = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.toolsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.ingresoAPlantaToolStripMenuItem,
            this.ingresoAProducciónToolStripMenuItem,
            this.pesajeEnProducciónToolStripMenuItem,
            this.pesajeDeCajasToolStripMenuItem,
            this.preparaciónDeCombosToolStripMenuItem,
            this.fraccionamientosToolStripMenuItem,
            this.transferenciasDeDepósitoToolStripMenuItem,
            this.egresosToolStripMenuItem,
            this.devolucionesToolStripMenuItem,
            this.colecciónInventarioToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(66, 20);
            this.fileMenu.Text = "&Procesos";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(211, 6);
            // 
            // ingresoAPlantaToolStripMenuItem
            // 
            this.ingresoAPlantaToolStripMenuItem.Name = "ingresoAPlantaToolStripMenuItem";
            this.ingresoAPlantaToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.ingresoAPlantaToolStripMenuItem.Text = "&Ingreso a Planta";
            this.ingresoAPlantaToolStripMenuItem.Click += new System.EventHandler(this.IngresoAPlantaToolStripMenuItem_Click);
            // 
            // ingresoAProducciónToolStripMenuItem
            // 
            this.ingresoAProducciónToolStripMenuItem.Name = "ingresoAProducciónToolStripMenuItem";
            this.ingresoAProducciónToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.ingresoAProducciónToolStripMenuItem.Text = "Ingreso a &Producción";
            this.ingresoAProducciónToolStripMenuItem.Click += new System.EventHandler(this.IngresoAProducciónToolStripMenuItem_Click);
            // 
            // pesajeEnProducciónToolStripMenuItem
            // 
            this.pesajeEnProducciónToolStripMenuItem.Name = "pesajeEnProducciónToolStripMenuItem";
            this.pesajeEnProducciónToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.pesajeEnProducciónToolStripMenuItem.Text = "P&esaje en Producción";
            this.pesajeEnProducciónToolStripMenuItem.Click += new System.EventHandler(this.PesajeEnProducciónToolStripMenuItem_Click);
            // 
            // pesajeDeCajasToolStripMenuItem
            // 
            this.pesajeDeCajasToolStripMenuItem.Name = "pesajeDeCajasToolStripMenuItem";
            this.pesajeDeCajasToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.pesajeDeCajasToolStripMenuItem.Text = "Pesaje de Cajas";
            this.pesajeDeCajasToolStripMenuItem.Click += new System.EventHandler(this.pesajeDeCajasToolStripMenuItem_Click);
            // 
            // preparaciónDeCombosToolStripMenuItem
            // 
            this.preparaciónDeCombosToolStripMenuItem.Name = "preparaciónDeCombosToolStripMenuItem";
            this.preparaciónDeCombosToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.preparaciónDeCombosToolStripMenuItem.Text = "Preparación de Combos";
            this.preparaciónDeCombosToolStripMenuItem.Click += new System.EventHandler(this.preparaciónDeCombosToolStripMenuItem_Click);
            // 
            // fraccionamientosToolStripMenuItem
            // 
            this.fraccionamientosToolStripMenuItem.Name = "fraccionamientosToolStripMenuItem";
            this.fraccionamientosToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.fraccionamientosToolStripMenuItem.Text = "Fraccionamientos";
            // 
            // transferenciasDeDepósitoToolStripMenuItem
            // 
            this.transferenciasDeDepósitoToolStripMenuItem.Name = "transferenciasDeDepósitoToolStripMenuItem";
            this.transferenciasDeDepósitoToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.transferenciasDeDepósitoToolStripMenuItem.Text = "Transferencias de Depósito";
            this.transferenciasDeDepósitoToolStripMenuItem.Click += new System.EventHandler(this.transferenciasDeDepósitoToolStripMenuItem_Click);
            // 
            // egresosToolStripMenuItem
            // 
            this.egresosToolStripMenuItem.Name = "egresosToolStripMenuItem";
            this.egresosToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.egresosToolStripMenuItem.Text = "E&gresos";
            this.egresosToolStripMenuItem.Click += new System.EventHandler(this.EgresosToolStripMenuItem_Click);
            // 
            // devolucionesToolStripMenuItem
            // 
            this.devolucionesToolStripMenuItem.Name = "devolucionesToolStripMenuItem";
            this.devolucionesToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.devolucionesToolStripMenuItem.Text = "Devoluciones";
            this.devolucionesToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton_Devoluciones_Click);
            // 
            // colecciónInventarioToolStripMenuItem
            // 
            this.colecciónInventarioToolStripMenuItem.Name = "colecciónInventarioToolStripMenuItem";
            this.colecciónInventarioToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.colecciónInventarioToolStripMenuItem.Text = "Colección Inventario";
            this.colecciónInventarioToolStripMenuItem.Click += new System.EventHandler(this.colecciónInventarioToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.exitToolStripMenuItem.Text = "&Salir";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem,
            this.inventarioToolStripMenuItem,
            this.insumosToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(90, 20);
            this.toolsMenu.Text = "&Herramientas";
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionSistemaToolStripMenuItem,
            this.configuracionModoDeTrabajoToolStripMenuItem,
            this.backupToolStripMenuItem});
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // configuracionSistemaToolStripMenuItem
            // 
            this.configuracionSistemaToolStripMenuItem.Name = "configuracionSistemaToolStripMenuItem";
            this.configuracionSistemaToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.configuracionSistemaToolStripMenuItem.Text = "Sistema";
            this.configuracionSistemaToolStripMenuItem.Click += new System.EventHandler(this.sistemaToolStripMenuItem_Click);
            // 
            // configuracionModoDeTrabajoToolStripMenuItem
            // 
            this.configuracionModoDeTrabajoToolStripMenuItem.Name = "configuracionModoDeTrabajoToolStripMenuItem";
            this.configuracionModoDeTrabajoToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.configuracionModoDeTrabajoToolStripMenuItem.Text = "Modo de Trabajo";
            this.configuracionModoDeTrabajoToolStripMenuItem.Click += new System.EventHandler(this.modoDeTrabajoToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // inventarioToolStripMenuItem
            // 
            this.inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            this.inventarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.inventarioToolStripMenuItem.Text = "Inventario";
            this.inventarioToolStripMenuItem.Click += new System.EventHandler(this.inventarioToolStripMenuItem_Click);
            // 
            // insumosToolStripMenuItem
            // 
            this.insumosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajusteDeStockDeInsumosToolStripMenuItem});
            this.insumosToolStripMenuItem.Name = "insumosToolStripMenuItem";
            this.insumosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.insumosToolStripMenuItem.Text = "Insumos";
            // 
            // ajusteDeStockDeInsumosToolStripMenuItem
            // 
            this.ajusteDeStockDeInsumosToolStripMenuItem.Name = "ajusteDeStockDeInsumosToolStripMenuItem";
            this.ajusteDeStockDeInsumosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ajusteDeStockDeInsumosToolStripMenuItem.Text = "Ajuste de Stock";
            this.ajusteDeStockDeInsumosToolStripMenuItem.Click += new System.EventHandler(this.ajusteDeStockToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(53, 20);
            this.helpMenu.Text = "Ay&uda";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.contentsToolStripMenuItem.Text = "&Contenido";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(173, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.aboutToolStripMenuItem.Text = "&Acerca de... ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus_LabelOperador,
            this.toolStripStatus_Operador,
            this.toolStripStatus_LabelEstacion,
            this.toolStripStatus_Estacion,
            this.toolStripStatus_LabelDb,
            this.toolStripStatus_Db});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatus_LabelOperador
            // 
            this.toolStripStatus_LabelOperador.Name = "toolStripStatus_LabelOperador";
            this.toolStripStatus_LabelOperador.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatus_LabelOperador.Text = "Operador:";
            // 
            // toolStripStatus_Operador
            // 
            this.toolStripStatus_Operador.Name = "toolStripStatus_Operador";
            this.toolStripStatus_Operador.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatus_Operador.Text = "--";
            this.toolStripStatus_Operador.ToolTipText = "Operador Activo";
            // 
            // toolStripStatus_LabelEstacion
            // 
            this.toolStripStatus_LabelEstacion.Name = "toolStripStatus_LabelEstacion";
            this.toolStripStatus_LabelEstacion.Size = new System.Drawing.Size(54, 17);
            this.toolStripStatus_LabelEstacion.Text = "Estación:";
            // 
            // toolStripStatus_Estacion
            // 
            this.toolStripStatus_Estacion.Name = "toolStripStatus_Estacion";
            this.toolStripStatus_Estacion.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatus_Estacion.Text = "--";
            this.toolStripStatus_Estacion.ToolTipText = "Numero de Estación de Pesaje";
            // 
            // toolStripStatus_LabelDb
            // 
            this.toolStripStatus_LabelDb.Image = global::MeatWeigherManager.Properties.Resources.basededatos;
            this.toolStripStatus_LabelDb.Name = "toolStripStatus_LabelDb";
            this.toolStripStatus_LabelDb.Size = new System.Drawing.Size(99, 17);
            this.toolStripStatus_LabelDb.Text = "Base de Datos:";
            // 
            // toolStripStatus_Db
            // 
            this.toolStripStatus_Db.Name = "toolStripStatus_Db";
            this.toolStripStatus_Db.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatus_Db.Text = "--";
            this.toolStripStatus_Db.ToolTipText = "Estado de Conexion con la Base de Datos";
            // 
            // toolStripButton_IngresoAPlanta
            // 
            this.toolStripButton_IngresoAPlanta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_IngresoAPlanta.Image = global::MeatWeigherManager.Properties.Resources.fabrica;
            this.toolStripButton_IngresoAPlanta.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButton_IngresoAPlanta.Name = "toolStripButton_IngresoAPlanta";
            this.toolStripButton_IngresoAPlanta.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_IngresoAPlanta.Text = "Nuevo";
            this.toolStripButton_IngresoAPlanta.ToolTipText = "Ingreso a Planta";
            this.toolStripButton_IngresoAPlanta.Click += new System.EventHandler(this.IngresoAPlantaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_IngresoAProduccion
            // 
            this.toolStripButton_IngresoAProduccion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_IngresoAProduccion.Image = global::MeatWeigherManager.Properties.Resources.EngranajeCodigoBarras;
            this.toolStripButton_IngresoAProduccion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_IngresoAProduccion.Name = "toolStripButton_IngresoAProduccion";
            this.toolStripButton_IngresoAProduccion.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_IngresoAProduccion.ToolTipText = "Ingreso A Produccion";
            this.toolStripButton_IngresoAProduccion.Click += new System.EventHandler(this.IngresoAProducciónToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_PesajeEnProduccion
            // 
            this.toolStripButton_PesajeEnProduccion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_PesajeEnProduccion.Image = global::MeatWeigherManager.Properties.Resources.logo_balanza_3;
            this.toolStripButton_PesajeEnProduccion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_PesajeEnProduccion.Name = "toolStripButton_PesajeEnProduccion";
            this.toolStripButton_PesajeEnProduccion.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_PesajeEnProduccion.ToolTipText = "Pesaje en Producción";
            this.toolStripButton_PesajeEnProduccion.Click += new System.EventHandler(this.PesajeEnProducciónToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_Egresos
            // 
            this.toolStripButton_Egresos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Egresos.Image = global::MeatWeigherManager.Properties.Resources.fabrica_Egresos;
            this.toolStripButton_Egresos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Egresos.Name = "toolStripButton_Egresos";
            this.toolStripButton_Egresos.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_Egresos.ToolTipText = "Egresos";
            this.toolStripButton_Egresos.Click += new System.EventHandler(this.EgresosToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(42, 42);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_IngresoAPlanta,
            this.toolStripSeparator1,
            this.toolStripButton_IngresoAProduccion,
            this.toolStripSeparator3,
            this.toolStripButton_PesajeEnProduccion,
            this.toolStripSeparator7,
            this.toolStripButton_pesajeCajas,
            this.toolStripButton_preparacionCombos,
            this.toolStripSeparator15,
            this.toolStripButton_fraccionamiento,
            this.toolStripSeparator17,
            this.toolStripButton_transferenciasDeDeposito,
            this.toolStripButton_Egresos,
            this.toolStripSeparator9,
            this.toolStripButton_Devoluciones,
            this.toolStripButton_inventario,
            this.toolStripSeparator19,
            this.toolStripButton_cambioOperador,
            this.toolStripSeparator11,
            this.toolStripSeparator12,
            this.toolStripSeparator13,
            this.toolStripSeparator14});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(632, 49);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolStripButton_pesajeCajas
            // 
            this.toolStripButton_pesajeCajas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_pesajeCajas.Image = global::MeatWeigherManager.Properties.Resources.sumar_caja_icono_2;
            this.toolStripButton_pesajeCajas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_pesajeCajas.Name = "toolStripButton_pesajeCajas";
            this.toolStripButton_pesajeCajas.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_pesajeCajas.ToolTipText = "Pesaje de Cajas";
            this.toolStripButton_pesajeCajas.Click += new System.EventHandler(this.pesajeDeCajasToolStripMenuItem_Click);
            // 
            // toolStripButton_preparacionCombos
            // 
            this.toolStripButton_preparacionCombos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_preparacionCombos.Image = global::MeatWeigherManager.Properties.Resources.combo;
            this.toolStripButton_preparacionCombos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_preparacionCombos.Name = "toolStripButton_preparacionCombos";
            this.toolStripButton_preparacionCombos.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_preparacionCombos.ToolTipText = "Preparación de Combos";
            this.toolStripButton_preparacionCombos.Click += new System.EventHandler(this.preparaciónDeCombosToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_fraccionamiento
            // 
            this.toolStripButton_fraccionamiento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_fraccionamiento.Image = global::MeatWeigherManager.Properties.Resources.fraccionar_white;
            this.toolStripButton_fraccionamiento.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_fraccionamiento.Name = "toolStripButton_fraccionamiento";
            this.toolStripButton_fraccionamiento.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_fraccionamiento.Text = "Fraccionamiento";
            this.toolStripButton_fraccionamiento.Click += new System.EventHandler(this.toolStripButton_fraccionamiento_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_transferenciasDeDeposito
            // 
            this.toolStripButton_transferenciasDeDeposito.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_transferenciasDeDeposito.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_transferenciasDeDeposito.Image")));
            this.toolStripButton_transferenciasDeDeposito.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_transferenciasDeDeposito.Name = "toolStripButton_transferenciasDeDeposito";
            this.toolStripButton_transferenciasDeDeposito.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_transferenciasDeDeposito.Text = "Transferencias Depósito";
            this.toolStripButton_transferenciasDeDeposito.Click += new System.EventHandler(this.toolStripButton_transferenciasDeDeposito_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_Devoluciones
            // 
            this.toolStripButton_Devoluciones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Devoluciones.Image = global::MeatWeigherManager.Properties.Resources.fabrica_Devolucion;
            this.toolStripButton_Devoluciones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Devoluciones.Name = "toolStripButton_Devoluciones";
            this.toolStripButton_Devoluciones.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_Devoluciones.Text = "Devoluciones";
            this.toolStripButton_Devoluciones.Click += new System.EventHandler(this.toolStripButton_Devoluciones_Click);
            // 
            // toolStripButton_inventario
            // 
            this.toolStripButton_inventario.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_inventario.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_inventario.Image")));
            this.toolStripButton_inventario.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_inventario.Name = "toolStripButton_inventario";
            this.toolStripButton_inventario.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_inventario.Text = "Inventario";
            this.toolStripButton_inventario.Click += new System.EventHandler(this.colecciónInventarioToolStripMenuItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripButton_cambioOperador
            // 
            this.toolStripButton_cambioOperador.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_cambioOperador.Image = global::MeatWeigherManager.Properties.Resources.Intercambio_usuarios;
            this.toolStripButton_cambioOperador.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cambioOperador.Name = "toolStripButton_cambioOperador";
            this.toolStripButton_cambioOperador.Size = new System.Drawing.Size(46, 46);
            this.toolStripButton_cambioOperador.ToolTipText = "Cambio de Operador";
            this.toolStripButton_cambioOperador.Click += new System.EventHandler(this.toolStripButton_cambioOperador_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 49);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 49);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "MeatWeigherManager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_LabelDb;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ingresoAPlantaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ingresoAProducciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pesajeEnProducciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem egresosToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Db;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_LabelOperador;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Operador;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_LabelEstacion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Estacion;
        private System.Windows.Forms.ToolStripButton toolStripButton_IngresoAPlanta;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_IngresoAProduccion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_PesajeEnProduccion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton_Egresos;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton toolStripButton_cambioOperador;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton toolStripButton_pesajeCajas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem pesajeDeCajasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fraccionamientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_fraccionamiento;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripButton toolStripButton_Devoluciones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem devolucionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionSistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionModoDeTrabajoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preparaciónDeCombosToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_preparacionCombos;
        private System.Windows.Forms.ToolStripMenuItem colecciónInventarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_inventario;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insumosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajusteDeStockDeInsumosToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_transferenciasDeDeposito;
        private System.Windows.Forms.ToolStripMenuItem transferenciasDeDepósitoToolStripMenuItem;
    }
}



