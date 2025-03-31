namespace MeatWeigherManager
{
    partial class CViewConsultaEgresosDlg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CViewConsultaEgresosDlg));
            this.dataGridView_PEDIDOS = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip_datagridPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Abrir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_cerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.label16 = new System.Windows.Forms.Label();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.dataGridView_PIEZAS = new System.Windows.Forms.DataGridView();
            this.contextMenuDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PEDIDOS)).BeginInit();
            this.contextMenuStrip_datagridPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PIEZAS)).BeginInit();
            this.contextMenuDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_PEDIDOS
            // 
            this.dataGridView_PEDIDOS.AllowUserToAddRows = false;
            this.dataGridView_PEDIDOS.AllowUserToDeleteRows = false;
            this.dataGridView_PEDIDOS.AllowUserToOrderColumns = true;
            this.dataGridView_PEDIDOS.AllowUserToResizeRows = false;
            this.dataGridView_PEDIDOS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_PEDIDOS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_PEDIDOS.BackgroundColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_PEDIDOS.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_PEDIDOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_PEDIDOS.ContextMenuStrip = this.contextMenuStrip_datagridPedidos;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_PEDIDOS.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_PEDIDOS.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView_PEDIDOS.Location = new System.Drawing.Point(9, 28);
            this.dataGridView_PEDIDOS.Name = "dataGridView_PEDIDOS";
            this.dataGridView_PEDIDOS.ReadOnly = true;
            this.dataGridView_PEDIDOS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_PEDIDOS.Size = new System.Drawing.Size(1007, 244);
            this.dataGridView_PEDIDOS.TabIndex = 16;
            // 
            // contextMenuStrip_datagridPedidos
            // 
            this.contextMenuStrip_datagridPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Abrir,
            this.toolStripMenuItem_cerrar});
            this.contextMenuStrip_datagridPedidos.Name = "contextMenuDataGrid";
            this.contextMenuStrip_datagridPedidos.Size = new System.Drawing.Size(247, 52);
            // 
            // toolStripMenuItem_Abrir
            // 
            this.toolStripMenuItem_Abrir.Font = new System.Drawing.Font("Tahoma", 12F);
            this.toolStripMenuItem_Abrir.Image = global::MeatWeigherManager.Properties.Resources.carpeta_abierta;
            this.toolStripMenuItem_Abrir.Name = "toolStripMenuItem_Abrir";
            this.toolStripMenuItem_Abrir.Size = new System.Drawing.Size(246, 24);
            this.toolStripMenuItem_Abrir.Text = "Establece como Abierto";
            this.toolStripMenuItem_Abrir.ToolTipText = "Establece Estado a Abierto";
            this.toolStripMenuItem_Abrir.Click += new System.EventHandler(this.toolStripMenuItem_Abrir_Click);
            // 
            // toolStripMenuItem_cerrar
            // 
            this.toolStripMenuItem_cerrar.Font = new System.Drawing.Font("Tahoma", 12F);
            this.toolStripMenuItem_cerrar.Image = global::MeatWeigherManager.Properties.Resources.candado2;
            this.toolStripMenuItem_cerrar.Name = "toolStripMenuItem_cerrar";
            this.toolStripMenuItem_cerrar.Size = new System.Drawing.Size(246, 24);
            this.toolStripMenuItem_cerrar.Text = "Establece como Cerrado";
            this.toolStripMenuItem_cerrar.Click += new System.EventHandler(this.toolStripMenuItem_cerrar_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Pedidos";
            // 
            // button_Cancelar
            // 
            this.button_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancelar.Location = new System.Drawing.Point(942, 591);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(68, 28);
            this.button_Cancelar.TabIndex = 20;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            this.button_Cancelar.Click += new System.EventHandler(this.button_Cancelar_Click);
            // 
            // dataGridView_PIEZAS
            // 
            this.dataGridView_PIEZAS.AllowUserToAddRows = false;
            this.dataGridView_PIEZAS.AllowUserToDeleteRows = false;
            this.dataGridView_PIEZAS.AllowUserToOrderColumns = true;
            this.dataGridView_PIEZAS.AllowUserToResizeRows = false;
            this.dataGridView_PIEZAS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_PIEZAS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_PIEZAS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView_PIEZAS.BackgroundColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_PIEZAS.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_PIEZAS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_PIEZAS.ContextMenuStrip = this.contextMenuDataGrid;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_PIEZAS.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_PIEZAS.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView_PIEZAS.Location = new System.Drawing.Point(9, 300);
            this.dataGridView_PIEZAS.Name = "dataGridView_PIEZAS";
            this.dataGridView_PIEZAS.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_PIEZAS.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_PIEZAS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_PIEZAS.Size = new System.Drawing.Size(1007, 285);
            this.dataGridView_PIEZAS.TabIndex = 23;
            // 
            // contextMenuDataGrid
            // 
            this.contextMenuDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuDataGrid.Name = "contextMenuDataGrid";
            this.contextMenuDataGrid.Size = new System.Drawing.Size(135, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 24);
            this.toolStripMenuItem1.Text = "Eliminar";
            this.toolStripMenuItem1.ToolTipText = "Borrar";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuContextDelete_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "PIEZAS EGRESADAS";
            // 
            // CViewConsultaEgresosDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(1022, 630);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView_PIEZAS);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.dataGridView_PEDIDOS);
            this.Controls.Add(this.label16);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CViewConsultaEgresosDlg";
            this.Text = "MeatWeigherManager    -  Operaciones de Egresos Por Pedidos";
            this.Load += new System.EventHandler(this.CViewConsultaEgresosDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PEDIDOS)).EndInit();
            this.contextMenuStrip_datagridPedidos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PIEZAS)).EndInit();
            this.contextMenuDataGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_PEDIDOS;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.DataGridView dataGridView_PIEZAS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_datagridPedidos;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Abrir;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_cerrar;
    }
}