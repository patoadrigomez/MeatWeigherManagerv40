namespace MeatWeigherManager
{
    partial class CViewHistoryPartDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CViewHistoryPartDlg));
            this.dataGridView_Historico = new System.Windows.Forms.DataGridView();
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.checkBox_esContenedor = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Historico)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_Historico
            // 
            this.dataGridView_Historico.AllowUserToAddRows = false;
            this.dataGridView_Historico.AllowUserToOrderColumns = true;
            this.dataGridView_Historico.AllowUserToResizeRows = false;
            this.dataGridView_Historico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Historico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Historico.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_Historico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Historico.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Historico.Location = new System.Drawing.Point(12, 59);
            this.dataGridView_Historico.MultiSelect = false;
            this.dataGridView_Historico.Name = "dataGridView_Historico";
            this.dataGridView_Historico.ReadOnly = true;
            this.dataGridView_Historico.RowTemplate.Height = 30;
            this.dataGridView_Historico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Historico.Size = new System.Drawing.Size(1049, 413);
            this.dataGridView_Historico.StandardTab = true;
            this.dataGridView_Historico.TabIndex = 4;
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(987, 481);
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
            this.button_Cancelar.Location = new System.Drawing.Point(905, 481);
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
            this.label1.Location = new System.Drawing.Point(872, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Historico de Movimientos";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(9, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Identificador";
            // 
            // textBox_id
            // 
            this.textBox_id.BackColor = System.Drawing.Color.Black;
            this.textBox_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_id.ForeColor = System.Drawing.Color.White;
            this.textBox_id.Location = new System.Drawing.Point(12, 25);
            this.textBox_id.MaxLength = 7;
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(86, 30);
            this.textBox_id.TabIndex = 32;
            this.textBox_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_id.TextChanged += new System.EventHandler(this.textBox_id_TextChanged);
            this.textBox_id.DoubleClick += new System.EventHandler(this.textBox_idBuscar_DoubleClick);
            this.textBox_id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_id_KeyPress);
            // 
            // checkBox_esContenedor
            // 
            this.checkBox_esContenedor.AutoSize = true;
            this.checkBox_esContenedor.Location = new System.Drawing.Point(104, 32);
            this.checkBox_esContenedor.Name = "checkBox_esContenedor";
            this.checkBox_esContenedor.Size = new System.Drawing.Size(110, 17);
            this.checkBox_esContenedor.TabIndex = 68;
            this.checkBox_esContenedor.Text = "Es un contenedor";
            this.checkBox_esContenedor.UseVisualStyleBackColor = true;
            this.checkBox_esContenedor.CheckedChanged += new System.EventHandler(this.checkBox_esContenedor_CheckedChanged);
            // 
            // CViewHistoryPartDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(1069, 515);
            this.Controls.Add(this.checkBox_esContenedor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_Historico);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CViewHistoryPartDlg";
            this.Text = "MeatWeigherManager    -  Vista de Movimientos de una Pieza o Contenedor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CViewHistoryPartDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Historico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Historico;
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.CheckBox checkBox_esContenedor;
    }
}