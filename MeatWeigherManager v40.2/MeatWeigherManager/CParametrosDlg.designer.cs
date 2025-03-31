namespace MeatWeigherManager
{
    partial class CParametrosDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CParametrosDlg));
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.groupBox_parametros = new System.Windows.Forms.GroupBox();
            this.textBox_diasProximidadVencimiento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_Actualizar = new System.Windows.Forms.Button();
            this.groupBox_parametros.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(538, 197);
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
            this.button_Cancelar.Location = new System.Drawing.Point(456, 197);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(76, 27);
            this.button_Cancelar.TabIndex = 11;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox_parametros
            // 
            this.groupBox_parametros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_parametros.Controls.Add(this.textBox_diasProximidadVencimiento);
            this.groupBox_parametros.Controls.Add(this.label4);
            this.groupBox_parametros.Controls.Add(this.button_Actualizar);
            this.groupBox_parametros.Location = new System.Drawing.Point(12, 12);
            this.groupBox_parametros.Name = "groupBox_parametros";
            this.groupBox_parametros.Size = new System.Drawing.Size(602, 96);
            this.groupBox_parametros.TabIndex = 21;
            this.groupBox_parametros.TabStop = false;
            this.groupBox_parametros.Text = "Parametros";
            // 
            // textBox_diasProximidadVencimiento
            // 
            this.textBox_diasProximidadVencimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_diasProximidadVencimiento.Location = new System.Drawing.Point(197, 37);
            this.textBox_diasProximidadVencimiento.MaxLength = 3;
            this.textBox_diasProximidadVencimiento.Name = "textBox_diasProximidadVencimiento";
            this.textBox_diasProximidadVencimiento.Size = new System.Drawing.Size(69, 26);
            this.textBox_diasProximidadVencimiento.TabIndex = 6;
            this.textBox_diasProximidadVencimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_diasProximidadVencimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_diasproximidadvencimiento_KeyPress);
            this.textBox_diasProximidadVencimiento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_diasproximidadvencimiento_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Dias de Proximidad de Vencimiento";
            // 
            // button_Actualizar
            // 
            this.button_Actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Actualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Actualizar.ForeColor = System.Drawing.Color.Green;
            this.button_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("button_Actualizar.Image")));
            this.button_Actualizar.Location = new System.Drawing.Point(525, 19);
            this.button_Actualizar.Name = "button_Actualizar";
            this.button_Actualizar.Size = new System.Drawing.Size(71, 62);
            this.button_Actualizar.TabIndex = 11;
            this.button_Actualizar.UseVisualStyleBackColor = true;
            this.button_Actualizar.Click += new System.EventHandler(this.button_Actualizar_Click);
            // 
            // CParametrosDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancelar;
            this.ClientSize = new System.Drawing.Size(619, 236);
            this.Controls.Add(this.groupBox_parametros);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CParametrosDlg";
            this.Text = "MeatWeigherManager    -  Edición de Parametros Generales";
            this.Load += new System.EventHandler(this.CABM_CParametrosDlg_Load);
            this.groupBox_parametros.ResumeLayout(false);
            this.groupBox_parametros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.GroupBox groupBox_parametros;
        private System.Windows.Forms.Button button_Actualizar;
        private System.Windows.Forms.TextBox textBox_diasProximidadVencimiento;
        private System.Windows.Forms.Label label4;
    }
}