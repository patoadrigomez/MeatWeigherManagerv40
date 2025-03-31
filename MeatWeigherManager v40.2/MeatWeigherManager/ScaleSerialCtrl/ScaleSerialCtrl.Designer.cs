namespace ScaleSerialCtrl
{
    partial class ScaleSerialCtrl
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
            this.groupBox_Balanza = new System.Windows.Forms.GroupBox();
            this.ledCtrl_pesajeAutomatico = new LedCtrl.LedCtrl();
            this.button_Pesar = new System.Windows.Forms.Button();
            this.checkBox_modoPesajeAutomatico = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button_ledConexionBalanza = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button_ledTara = new System.Windows.Forms.Button();
            this.button_ledEstable = new System.Windows.Forms.Button();
            this.button_ledZero = new System.Windows.Forms.Button();
            this.button_LedPesar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_displayBalanza = new System.Windows.Forms.TextBox();
            this.groupBox_Balanza.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Balanza
            // 
            this.groupBox_Balanza.Controls.Add(this.ledCtrl_pesajeAutomatico);
            this.groupBox_Balanza.Controls.Add(this.button_Pesar);
            this.groupBox_Balanza.Controls.Add(this.checkBox_modoPesajeAutomatico);
            this.groupBox_Balanza.Controls.Add(this.label15);
            this.groupBox_Balanza.Controls.Add(this.label13);
            this.groupBox_Balanza.Controls.Add(this.button_ledConexionBalanza);
            this.groupBox_Balanza.Controls.Add(this.label12);
            this.groupBox_Balanza.Controls.Add(this.label11);
            this.groupBox_Balanza.Controls.Add(this.label10);
            this.groupBox_Balanza.Controls.Add(this.button_ledTara);
            this.groupBox_Balanza.Controls.Add(this.button_ledEstable);
            this.groupBox_Balanza.Controls.Add(this.button_ledZero);
            this.groupBox_Balanza.Controls.Add(this.button_LedPesar);
            this.groupBox_Balanza.Controls.Add(this.label9);
            this.groupBox_Balanza.Controls.Add(this.textBox_displayBalanza);
            this.groupBox_Balanza.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Balanza.Location = new System.Drawing.Point(7, 5);
            this.groupBox_Balanza.Name = "groupBox_Balanza";
            this.groupBox_Balanza.Size = new System.Drawing.Size(257, 187);
            this.groupBox_Balanza.TabIndex = 8;
            this.groupBox_Balanza.TabStop = false;
            // 
            // ledCtrl_pesajeAutomatico
            // 
            this.ledCtrl_pesajeAutomatico.ColorOff = System.Drawing.Color.Transparent;
            this.ledCtrl_pesajeAutomatico.ColorOn = System.Drawing.Color.Red;
            this.ledCtrl_pesajeAutomatico.EdgeColor = System.Drawing.Color.Transparent;
            this.ledCtrl_pesajeAutomatico.EdgeWidth = 4;
            this.ledCtrl_pesajeAutomatico.LedStatus = false;
            this.ledCtrl_pesajeAutomatico.Location = new System.Drawing.Point(207, 132);
            this.ledCtrl_pesajeAutomatico.Margin = new System.Windows.Forms.Padding(4);
            this.ledCtrl_pesajeAutomatico.Name = "ledCtrl_pesajeAutomatico";
            this.ledCtrl_pesajeAutomatico.Size = new System.Drawing.Size(29, 35);
            this.ledCtrl_pesajeAutomatico.TabIndex = 38;
            // 
            // button_Pesar
            // 
            this.button_Pesar.Enabled = false;
            this.button_Pesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Pesar.Location = new System.Drawing.Point(49, 146);
            this.button_Pesar.Name = "button_Pesar";
            this.button_Pesar.Size = new System.Drawing.Size(137, 35);
            this.button_Pesar.TabIndex = 37;
            this.button_Pesar.Text = "PESAR";
            this.button_Pesar.UseVisualStyleBackColor = true;
            this.button_Pesar.Click += new System.EventHandler(this.button_Pesar_Click);
            // 
            // checkBox_modoPesajeAutomatico
            // 
            this.checkBox_modoPesajeAutomatico.AutoSize = true;
            this.checkBox_modoPesajeAutomatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_modoPesajeAutomatico.Location = new System.Drawing.Point(14, 121);
            this.checkBox_modoPesajeAutomatico.Name = "checkBox_modoPesajeAutomatico";
            this.checkBox_modoPesajeAutomatico.Size = new System.Drawing.Size(145, 19);
            this.checkBox_modoPesajeAutomatico.TabIndex = 36;
            this.checkBox_modoPesajeAutomatico.Text = "Pesaje Automatico";
            this.checkBox_modoPesajeAutomatico.UseVisualStyleBackColor = true;
            this.checkBox_modoPesajeAutomatico.CheckedChanged += new System.EventHandler(this.checkBox_modoPesajeAutomatico_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(183, 102);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Peso Ok";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(11, 102);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Conexión";
            // 
            // button_ledConexionBalanza
            // 
            this.button_ledConexionBalanza.BackColor = System.Drawing.Color.Red;
            this.button_ledConexionBalanza.Enabled = false;
            this.button_ledConexionBalanza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ledConexionBalanza.Location = new System.Drawing.Point(20, 89);
            this.button_ledConexionBalanza.Name = "button_ledConexionBalanza";
            this.button_ledConexionBalanza.Size = new System.Drawing.Size(13, 10);
            this.button_ledConexionBalanza.TabIndex = 29;
            this.button_ledConexionBalanza.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(151, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Tara";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(104, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Estable";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(68, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Zero";
            // 
            // button_ledTara
            // 
            this.button_ledTara.Enabled = false;
            this.button_ledTara.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ledTara.Location = new System.Drawing.Point(158, 89);
            this.button_ledTara.Name = "button_ledTara";
            this.button_ledTara.Size = new System.Drawing.Size(13, 10);
            this.button_ledTara.TabIndex = 25;
            this.button_ledTara.UseVisualStyleBackColor = true;
            // 
            // button_ledEstable
            // 
            this.button_ledEstable.Enabled = false;
            this.button_ledEstable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ledEstable.Location = new System.Drawing.Point(114, 89);
            this.button_ledEstable.Name = "button_ledEstable";
            this.button_ledEstable.Size = new System.Drawing.Size(13, 10);
            this.button_ledEstable.TabIndex = 24;
            this.button_ledEstable.UseVisualStyleBackColor = true;
            // 
            // button_ledZero
            // 
            this.button_ledZero.Enabled = false;
            this.button_ledZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ledZero.Location = new System.Drawing.Point(69, 89);
            this.button_ledZero.Name = "button_ledZero";
            this.button_ledZero.Size = new System.Drawing.Size(13, 10);
            this.button_ledZero.TabIndex = 23;
            this.button_ledZero.UseVisualStyleBackColor = true;
            // 
            // button_LedPesar
            // 
            this.button_LedPesar.Enabled = false;
            this.button_LedPesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_LedPesar.Location = new System.Drawing.Point(205, 89);
            this.button_LedPesar.Name = "button_LedPesar";
            this.button_LedPesar.Size = new System.Drawing.Size(13, 10);
            this.button_LedPesar.TabIndex = 22;
            this.button_LedPesar.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(224, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "kg";
            // 
            // textBox_displayBalanza
            // 
            this.textBox_displayBalanza.BackColor = System.Drawing.SystemColors.ControlText;
            this.textBox_displayBalanza.Font = new System.Drawing.Font("Digital SF", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_displayBalanza.ForeColor = System.Drawing.Color.Lime;
            this.textBox_displayBalanza.Location = new System.Drawing.Point(20, 19);
            this.textBox_displayBalanza.Name = "textBox_displayBalanza";
            this.textBox_displayBalanza.ReadOnly = true;
            this.textBox_displayBalanza.Size = new System.Drawing.Size(198, 60);
            this.textBox_displayBalanza.TabIndex = 19;
            this.textBox_displayBalanza.TabStop = false;
            this.textBox_displayBalanza.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ScaleSerialCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_Balanza);
            this.Name = "ScaleSerialCtrl";
            this.Size = new System.Drawing.Size(272, 200);
            this.groupBox_Balanza.ResumeLayout(false);
            this.groupBox_Balanza.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Balanza;
        private System.Windows.Forms.CheckBox checkBox_modoPesajeAutomatico;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button_ledConexionBalanza;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_ledTara;
        private System.Windows.Forms.Button button_ledEstable;
        private System.Windows.Forms.Button button_ledZero;
        private System.Windows.Forms.Button button_LedPesar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_displayBalanza;
        private System.Windows.Forms.Button button_Pesar;
        private LedCtrl.LedCtrl ledCtrl_pesajeAutomatico;
    }
}
