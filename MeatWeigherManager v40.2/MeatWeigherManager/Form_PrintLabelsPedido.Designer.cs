namespace MeatWeigherManager
{
    partial class Form_PrintLabelsPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PrintLabelsPedido));
            this.button_cancelar = new System.Windows.Forms.Button();
            this.textBox_totalBultos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_printLabes = new System.Windows.Forms.Button();
            this.bigCheckBox_numerarBultos = new BigCheckBox.BigCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_cantDuplicados = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_cancelar
            // 
            this.button_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancelar.Location = new System.Drawing.Point(218, 126);
            this.button_cancelar.Name = "button_cancelar";
            this.button_cancelar.Size = new System.Drawing.Size(75, 23);
            this.button_cancelar.TabIndex = 3;
            this.button_cancelar.Text = "Cancelar";
            this.button_cancelar.UseVisualStyleBackColor = true;
            // 
            // textBox_totalBultos
            // 
            this.textBox_totalBultos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_totalBultos.Location = new System.Drawing.Point(149, 12);
            this.textBox_totalBultos.MaxLength = 3;
            this.textBox_totalBultos.Name = "textBox_totalBultos";
            this.textBox_totalBultos.Size = new System.Drawing.Size(47, 23);
            this.textBox_totalBultos.TabIndex = 2;
            this.textBox_totalBultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_totalBultos.DoubleClick += new System.EventHandler(this.textBox_totalBultos_DoubleClick);
            this.textBox_totalBultos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_nroBulto_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total Bultos";
            // 
            // button_printLabes
            // 
            this.button_printLabes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_printLabes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_printLabes.BackgroundImage")));
            this.button_printLabes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_printLabes.Location = new System.Drawing.Point(299, 85);
            this.button_printLabes.Name = "button_printLabes";
            this.button_printLabes.Size = new System.Drawing.Size(69, 69);
            this.button_printLabes.TabIndex = 74;
            this.button_printLabes.UseVisualStyleBackColor = true;
            this.button_printLabes.Click += new System.EventHandler(this.button_printLabes_Click);
            // 
            // bigCheckBox_numerarBultos
            // 
            this.bigCheckBox_numerarBultos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bigCheckBox_numerarBultos.Location = new System.Drawing.Point(149, 85);
            this.bigCheckBox_numerarBultos.Name = "bigCheckBox_numerarBultos";
            this.bigCheckBox_numerarBultos.Size = new System.Drawing.Size(28, 31);
            this.bigCheckBox_numerarBultos.TabIndex = 5;
            this.bigCheckBox_numerarBultos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bigCheckBox_numerarBultos.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Numerar Bultos";
            // 
            // textBox_cantDuplicados
            // 
            this.textBox_cantDuplicados.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_cantDuplicados.Location = new System.Drawing.Point(149, 49);
            this.textBox_cantDuplicados.MaxLength = 3;
            this.textBox_cantDuplicados.Name = "textBox_cantDuplicados";
            this.textBox_cantDuplicados.Size = new System.Drawing.Size(32, 23);
            this.textBox_cantDuplicados.TabIndex = 7;
            this.textBox_cantDuplicados.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_cantDuplicados.DoubleClick += new System.EventHandler(this.textBox_cantDuplicados_DoubleClick);
            this.textBox_cantDuplicados.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_nroBulto_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cantidad Duplicados";
            // 
            // Form_PrintLabelsPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancelar;
            this.ClientSize = new System.Drawing.Size(380, 161);
            this.Controls.Add(this.textBox_cantDuplicados);
            this.Controls.Add(this.button_printLabes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_totalBultos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_cancelar);
            this.Controls.Add(this.bigCheckBox_numerarBultos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_PrintLabelsPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MeatWeigherManager    - Imprimir Etiquetas de un Pedido";
            this.Load += new System.EventHandler(this.Form_PrintLabelsPedido_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_cancelar;
        private System.Windows.Forms.TextBox textBox_totalBultos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_printLabes;
        private BigCheckBox.BigCheckBox bigCheckBox_numerarBultos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_cantDuplicados;
        private System.Windows.Forms.Label label3;
    }
}