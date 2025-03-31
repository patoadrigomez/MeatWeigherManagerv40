namespace EditValueTouchDlg
{
    partial class CEditValueTouchDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEditValueTouchDlg));
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.label_value = new System.Windows.Forms.Label();
            this.keyboardcontrol_Num1 = new KeyboardClassLibrarySjf.Keyboardcontrol_Num();
            this.SuspendLayout();
            // 
            // textBox_value
            // 
            this.textBox_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_value.Location = new System.Drawing.Point(12, 40);
            this.textBox_value.MaxLength = 13;
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(334, 44);
            this.textBox_value.TabIndex = 0;
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_value.TextChanged += new System.EventHandler(this.textBox_value_TextChanged);
            this.textBox_value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_value_KeyPress);
            // 
            // label_value
            // 
            this.label_value.AutoSize = true;
            this.label_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_value.Location = new System.Drawing.Point(101, 9);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(0, 25);
            this.label_value.TabIndex = 1;
            // 
            // keyboardcontrol_Num1
            // 
            this.keyboardcontrol_Num1.Location = new System.Drawing.Point(11, 91);
            this.keyboardcontrol_Num1.Name = "keyboardcontrol_Num1";
            this.keyboardcontrol_Num1.Size = new System.Drawing.Size(663, 301);
            this.keyboardcontrol_Num1.TabIndex = 2;
            this.keyboardcontrol_Num1.UserKeyPressed += new KeyboardClassLibrarySjf.KeyboardDelegate(this.keyboardcontrol_UserKeyPressed);
            // 
            // CEditValueTouchDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 409);
            this.Controls.Add(this.keyboardcontrol_Num1);
            this.Controls.Add(this.label_value);
            this.Controls.Add(this.textBox_value);
            this.Icon = ((System.Drawing.Icon)(Properties.Resources.imcr));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CEditValueTouchDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Label label_value;
        private KeyboardClassLibrarySjf.Keyboardcontrol_Num keyboardcontrol_Num1;
    }
}