namespace MeatWeigherManager
{
    partial class Form_TransferenciaDeposito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TransferenciaDeposito));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_Consultas = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_consulta_TransferenciasRealizadas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatusScanner = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusScannerValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusProcessValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_buttons = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Iniciar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Parar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_TituloProceso = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox_datCaptura = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_selectDestino = new System.Windows.Forms.Button();
            this.textBox_destinos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ledCtrl_readyError = new LedCtrl.LedCtrl();
            this.textBox_valueReadCodBar = new System.Windows.Forms.TextBox();
            this.ledCtrl_readyOK = new LedCtrl.LedCtrl();
            this.label_detalleLectura = new System.Windows.Forms.Label();
            this.dataGridView_bultosTransferidos = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip_buttons.SuspendLayout();
            this.groupBox_datCaptura.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_bultosTransferidos)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Consultas,
            this.toolStripMenuItemVersion});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1005, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // ToolStripMenuItem_Consultas
            // 
            this.ToolStripMenuItem_Consultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_consulta_TransferenciasRealizadas});
            this.ToolStripMenuItem_Consultas.Name = "ToolStripMenuItem_Consultas";
            this.ToolStripMenuItem_Consultas.Size = new System.Drawing.Size(71, 20);
            this.ToolStripMenuItem_Consultas.Text = "C&onsultas";
            // 
            // ToolStripMenuItem_consulta_TransferenciasRealizadas
            // 
            this.ToolStripMenuItem_consulta_TransferenciasRealizadas.Name = "ToolStripMenuItem_consulta_TransferenciasRealizadas";
            this.ToolStripMenuItem_consulta_TransferenciasRealizadas.Size = new System.Drawing.Size(205, 22);
            this.ToolStripMenuItem_consulta_TransferenciasRealizadas.Text = "&Transferencias Realizadas";
            this.ToolStripMenuItem_consulta_TransferenciasRealizadas.Click += new System.EventHandler(this.consultaTransferenciasRealizadas_ToolStripMenuItem_Click);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1005, 22);
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
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.toolStripSeparator5,
            this.toolStripLabel_TituloProceso,
            this.toolStripSeparator3,
            this.toolStripSeparator4});
            this.toolStrip_buttons.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_buttons.Name = "toolStrip_buttons";
            this.toolStrip_buttons.Size = new System.Drawing.Size(1005, 49);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 49);
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
            // toolStripLabel_TituloProceso
            // 
            this.toolStripLabel_TituloProceso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel_TituloProceso.Name = "toolStripLabel_TituloProceso";
            this.toolStripLabel_TituloProceso.Size = new System.Drawing.Size(215, 46);
            this.toolStripLabel_TituloProceso.Text = "Transferencias de Depósito";
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
            // groupBox_datCaptura
            // 
            this.groupBox_datCaptura.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_datCaptura.Controls.Add(this.label6);
            this.groupBox_datCaptura.Controls.Add(this.button_selectDestino);
            this.groupBox_datCaptura.Controls.Add(this.textBox_destinos);
            this.groupBox_datCaptura.Controls.Add(this.label3);
            this.groupBox_datCaptura.Controls.Add(this.tableLayoutPanel1);
            this.groupBox_datCaptura.Controls.Add(this.dataGridView_bultosTransferidos);
            this.groupBox_datCaptura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_datCaptura.Location = new System.Drawing.Point(13, 47);
            this.groupBox_datCaptura.Name = "groupBox_datCaptura";
            this.groupBox_datCaptura.Size = new System.Drawing.Size(980, 511);
            this.groupBox_datCaptura.TabIndex = 29;
            this.groupBox_datCaptura.TabStop = false;
            this.groupBox_datCaptura.Text = "Captura";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 16);
            this.label6.TabIndex = 76;
            this.label6.Text = "DEPÓSITO DESTINO";
            // 
            // button_selectDestino
            // 
            this.button_selectDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_selectDestino.Location = new System.Drawing.Point(319, 50);
            this.button_selectDestino.Name = "button_selectDestino";
            this.button_selectDestino.Size = new System.Drawing.Size(32, 29);
            this.button_selectDestino.TabIndex = 75;
            this.button_selectDestino.Text = "..";
            this.button_selectDestino.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_selectDestino.UseVisualStyleBackColor = true;
            this.button_selectDestino.Click += new System.EventHandler(this.button_selectDestino_Click);
            // 
            // textBox_destinos
            // 
            this.textBox_destinos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_destinos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_destinos.Location = new System.Drawing.Point(9, 52);
            this.textBox_destinos.MaxLength = 15;
            this.textBox_destinos.Name = "textBox_destinos";
            this.textBox_destinos.ReadOnly = true;
            this.textBox_destinos.Size = new System.Drawing.Size(304, 26);
            this.textBox_destinos.TabIndex = 74;
            this.textBox_destinos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 65;
            this.label3.Text = "BULTOS TRANSFERIDOS";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tableLayoutPanel1.Controls.Add(this.ledCtrl_readyOK, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_detalleLectura, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(360, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.70588F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.29412F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(614, 85);
            this.tableLayoutPanel1.TabIndex = 64;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(431, 3);
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
            this.label5.Location = new System.Drawing.Point(250, 3);
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
            this.label4.Location = new System.Drawing.Point(196, 3);
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
            this.label2.Location = new System.Drawing.Point(58, 3);
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
            this.ledCtrl_readyError.Location = new System.Drawing.Point(249, 29);
            this.ledCtrl_readyError.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.ledCtrl_readyError.Name = "ledCtrl_readyError";
            this.ledCtrl_readyError.Size = new System.Drawing.Size(49, 46);
            this.ledCtrl_readyError.TabIndex = 64;
            // 
            // textBox_valueReadCodBar
            // 
            this.textBox_valueReadCodBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_valueReadCodBar.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_valueReadCodBar.Font = new System.Drawing.Font("Digital SF", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_valueReadCodBar.ForeColor = System.Drawing.Color.Lime;
            this.textBox_valueReadCodBar.Location = new System.Drawing.Point(19, 32);
            this.textBox_valueReadCodBar.Name = "textBox_valueReadCodBar";
            this.textBox_valueReadCodBar.ReadOnly = true;
            this.textBox_valueReadCodBar.Size = new System.Drawing.Size(136, 40);
            this.textBox_valueReadCodBar.TabIndex = 36;
            this.textBox_valueReadCodBar.Text = "------";
            this.textBox_valueReadCodBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_valueReadCodBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_valueReadCodBar_MouseDoubleClick);
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
            this.ledCtrl_readyOK.Location = new System.Drawing.Point(181, 29);
            this.ledCtrl_readyOK.Margin = new System.Windows.Forms.Padding(5);
            this.ledCtrl_readyOK.Name = "ledCtrl_readyOK";
            this.ledCtrl_readyOK.Size = new System.Drawing.Size(51, 46);
            this.ledCtrl_readyOK.TabIndex = 62;
            // 
            // label_detalleLectura
            // 
            this.label_detalleLectura.AutoSize = true;
            this.label_detalleLectura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detalleLectura.Location = new System.Drawing.Point(310, 20);
            this.label_detalleLectura.Name = "label_detalleLectura";
            this.label_detalleLectura.Size = new System.Drawing.Size(19, 13);
            this.label_detalleLectura.TabIndex = 66;
            this.label_detalleLectura.Text = "----";
            // 
            // dataGridView_bultosTransferidos
            // 
            this.dataGridView_bultosTransferidos.AllowUserToAddRows = false;
            this.dataGridView_bultosTransferidos.AllowUserToDeleteRows = false;
            this.dataGridView_bultosTransferidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_bultosTransferidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_bultosTransferidos.Location = new System.Drawing.Point(3, 138);
            this.dataGridView_bultosTransferidos.Name = "dataGridView_bultosTransferidos";
            this.dataGridView_bultosTransferidos.ReadOnly = true;
            this.dataGridView_bultosTransferidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_bultosTransferidos.Size = new System.Drawing.Size(971, 367);
            this.dataGridView_bultosTransferidos.TabIndex = 61;
            // 
            // Form_TransferenciaDeposito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 583);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox_datCaptura);
            this.Controls.Add(this.toolStrip_buttons);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1021, 599);
            this.Name = "Form_TransferenciaDeposito";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_TransferenciaDeposito_FormClosing);
            this.Load += new System.EventHandler(this.Form_TransferenciaDeposito_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip_buttons.ResumeLayout(false);
            this.toolStrip_buttons.PerformLayout();
            this.groupBox_datCaptura.ResumeLayout(false);
            this.groupBox_datCaptura.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_bultosTransferidos)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_consulta_TransferenciasRealizadas;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatusProcess;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProcessValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_TituloProceso;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatusScanner;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusScannerValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox_datCaptura;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_selectDestino;
        private System.Windows.Forms.TextBox textBox_destinos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private LedCtrl.LedCtrl ledCtrl_readyError;
        private System.Windows.Forms.TextBox textBox_valueReadCodBar;
        private LedCtrl.LedCtrl ledCtrl_readyOK;
        private System.Windows.Forms.Label label_detalleLectura;
        private System.Windows.Forms.DataGridView dataGridView_bultosTransferidos;
    }
}

