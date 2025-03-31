namespace MeatWeigherManager
{
    partial class CViewOICreadasDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CViewOICreadasDlg));
            this.dataGridView_OI = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Seleccionar = new System.Windows.Forms.Button();
            this.groupBox_Busqueda = new System.Windows.Forms.GroupBox();
            this.textBox_valorBuscar = new System.Windows.Forms.TextBox();
            this.trackBar_dgvOIs = new System.Windows.Forms.TrackBar();
            this.button_up = new System.Windows.Forms.Button();
            this.button_down = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_OI)).BeginInit();
            this.groupBox_Busqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_dgvOIs)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_OI
            // 
            this.dataGridView_OI.AllowUserToAddRows = false;
            this.dataGridView_OI.AllowUserToDeleteRows = false;
            this.dataGridView_OI.AllowUserToOrderColumns = true;
            this.dataGridView_OI.AllowUserToResizeRows = false;
            this.dataGridView_OI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_OI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_OI.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_OI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_OI.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_OI.Location = new System.Drawing.Point(67, 68);
            this.dataGridView_OI.MultiSelect = false;
            this.dataGridView_OI.Name = "dataGridView_OI";
            this.dataGridView_OI.ReadOnly = true;
            this.dataGridView_OI.RowTemplate.Height = 42;
            this.dataGridView_OI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_OI.Size = new System.Drawing.Size(1046, 437);
            this.dataGridView_OI.StandardTab = true;
            this.dataGridView_OI.TabIndex = 0;
            this.dataGridView_OI.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_OICreadas_CellDoubleClick);
            this.dataGridView_OI.SelectionChanged += new System.EventHandler(this.dataGridView_OI_SelectionChanged);
            this.dataGridView_OI.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView_OICreadas_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ordenes de Ingresos";
            // 
            // button_Seleccionar
            // 
            this.button_Seleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Seleccionar.Location = new System.Drawing.Point(69, 12);
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
            this.groupBox_Busqueda.Location = new System.Drawing.Point(955, 6);
            this.groupBox_Busqueda.Name = "groupBox_Busqueda";
            this.groupBox_Busqueda.Size = new System.Drawing.Size(158, 56);
            this.groupBox_Busqueda.TabIndex = 4;
            this.groupBox_Busqueda.TabStop = false;
            this.groupBox_Busqueda.Text = "Busqueda por numero";
            // 
            // textBox_valorBuscar
            // 
            this.textBox_valorBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_valorBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_valorBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_valorBuscar.Location = new System.Drawing.Point(6, 19);
            this.textBox_valorBuscar.Name = "textBox_valorBuscar";
            this.textBox_valorBuscar.Size = new System.Drawing.Size(146, 26);
            this.textBox_valorBuscar.TabIndex = 3;
            this.textBox_valorBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_valorBuscar.TextChanged += new System.EventHandler(this.textBox_valorBuscar_TextChanged);
            this.textBox_valorBuscar.DoubleClick += new System.EventHandler(this.textBox_valorBuscar_DoubleClick);
            this.textBox_valorBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numeric_KeyPress);
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
            this.trackBar_dgvOIs.Scroll += new System.EventHandler(this.trackBar_dgvOIs_Scroll);
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
            // CViewOICreadasDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 542);
            this.Controls.Add(this.button_down);
            this.Controls.Add(this.button_up);
            this.Controls.Add(this.trackBar_dgvOIs);
            this.Controls.Add(this.groupBox_Busqueda);
            this.Controls.Add(this.button_Seleccionar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_OI);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CViewOICreadasDlg";
            this.Text = "MeatWeigherManager    -  (Información de Ordenes de Ingresos Creadas )";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CViewLoteCreadosDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_OI)).EndInit();
            this.groupBox_Busqueda.ResumeLayout(false);
            this.groupBox_Busqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_dgvOIs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_OI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Seleccionar;
        private System.Windows.Forms.GroupBox groupBox_Busqueda;
        private System.Windows.Forms.TextBox textBox_valorBuscar;
        private System.Windows.Forms.TrackBar trackBar_dgvOIs;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.Button button_down;
    }
}