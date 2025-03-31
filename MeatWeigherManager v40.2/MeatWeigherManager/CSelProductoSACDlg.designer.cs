namespace MeatWeigherManager
{
    partial class CSelProductoSACDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSelProductoSACDlg));
            this.dataGridView_Productos = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_nombreBuscar = new System.Windows.Forms.TextBox();
            this.button_up = new System.Windows.Forms.Button();
            this.button_down = new System.Windows.Forms.Button();
            this.trackBar_dgvProductos = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Productos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_Productos
            // 
            this.dataGridView_Productos.AllowUserToAddRows = false;
            this.dataGridView_Productos.AllowUserToOrderColumns = true;
            this.dataGridView_Productos.AllowUserToResizeRows = false;
            this.dataGridView_Productos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Productos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Productos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_Productos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Productos.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Productos.Location = new System.Drawing.Point(63, 73);
            this.dataGridView_Productos.MultiSelect = false;
            this.dataGridView_Productos.Name = "dataGridView_Productos";
            this.dataGridView_Productos.ReadOnly = true;
            this.dataGridView_Productos.RowTemplate.Height = 30;
            this.dataGridView_Productos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Productos.Size = new System.Drawing.Size(973, 587);
            this.dataGridView_Productos.StandardTab = true;
            this.dataGridView_Productos.TabIndex = 4;
            this.dataGridView_Productos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Productos_CellDoubleClick);
            this.dataGridView_Productos.SelectionChanged += new System.EventHandler(this.dataGridView_Productos_SelectionChanged);
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(962, 669);
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
            this.button_Cancelar.Location = new System.Drawing.Point(880, 669);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 11;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(902, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Productos SAC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(155, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Buscar Por Nombre";
            // 
            // textBox_nombreBuscar
            // 
            this.textBox_nombreBuscar.BackColor = System.Drawing.Color.Black;
            this.textBox_nombreBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombreBuscar.ForeColor = System.Drawing.Color.White;
            this.textBox_nombreBuscar.Location = new System.Drawing.Point(158, 27);
            this.textBox_nombreBuscar.Name = "textBox_nombreBuscar";
            this.textBox_nombreBuscar.Size = new System.Drawing.Size(262, 30);
            this.textBox_nombreBuscar.TabIndex = 32;
            this.textBox_nombreBuscar.TextChanged += new System.EventHandler(this.textBox_nombreBuscar_TextChanged);
            this.textBox_nombreBuscar.DoubleClick += new System.EventHandler(this.textBox_nombreBuscar_DoubleClick);
            // 
            // button_up
            // 
            this.button_up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_up.BackgroundImage")));
            this.button_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_up.ForeColor = System.Drawing.Color.Transparent;
            this.button_up.Location = new System.Drawing.Point(9, 15);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(51, 42);
            this.button_up.TabIndex = 65;
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_down
            // 
            this.button_down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_down.BackgroundImage")));
            this.button_down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_down.ForeColor = System.Drawing.Color.Transparent;
            this.button_down.Location = new System.Drawing.Point(78, 15);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(51, 42);
            this.button_down.TabIndex = 66;
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // trackBar_dgvProductos
            // 
            this.trackBar_dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar_dgvProductos.Location = new System.Drawing.Point(12, 63);
            this.trackBar_dgvProductos.Name = "trackBar_dgvProductos";
            this.trackBar_dgvProductos.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_dgvProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar_dgvProductos.Size = new System.Drawing.Size(45, 600);
            this.trackBar_dgvProductos.TabIndex = 67;
            this.trackBar_dgvProductos.Scroll += new System.EventHandler(this.trackBar_dgvProductos_Scroll);
            // 
            // CSelProductoSACDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(1044, 703);
            this.Controls.Add(this.trackBar_dgvProductos);
            this.Controls.Add(this.button_down);
            this.Controls.Add(this.button_up);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_nombreBuscar);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_Productos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CSelProductoSACDlg";
            this.Text = "MeatWeigherManager    -  Selección de Producto SAC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CABM_Productos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Productos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Productos;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_nombreBuscar;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.Button button_down;
        private System.Windows.Forms.TrackBar trackBar_dgvProductos;
    }
}