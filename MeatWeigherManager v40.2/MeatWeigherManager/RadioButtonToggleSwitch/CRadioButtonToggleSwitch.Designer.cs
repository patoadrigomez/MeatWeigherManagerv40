namespace RadioButtonToggleSwitch
{
    partial class CRadioButtonToggleSwitch
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

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonUC = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonUC
            // 
            this.radioButtonUC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonUC.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonUC.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonUC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radioButtonUC.Image = global::RadioButtonToggleSwitch.Properties.Resources.imageOnToggleSwitch;
            this.radioButtonUC.Location = new System.Drawing.Point(3, 3);
            this.radioButtonUC.Name = "radioButtonUC";
            this.radioButtonUC.Size = new System.Drawing.Size(127, 61);
            this.radioButtonUC.TabIndex = 0;
            this.radioButtonUC.TabStop = true;
            this.radioButtonUC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonUC.UseVisualStyleBackColor = false;
            this.radioButtonUC.CheckedChanged += new System.EventHandler(this.radioButtonUC_CheckedChanged);
            // 
            // CRadioButtonToggleSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.radioButtonUC);
            this.Name = "CRadioButtonToggleSwitch";
            this.Size = new System.Drawing.Size(132, 66);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonUC;
    }
}
