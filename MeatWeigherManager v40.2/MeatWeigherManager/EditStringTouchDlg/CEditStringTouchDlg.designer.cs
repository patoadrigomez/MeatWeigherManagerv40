namespace EditStringTouchDlg
{
    partial class CEditStringTouchDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEditStringTouchDlg));
            this.textBox_ValueEdit = new System.Windows.Forms.TextBox();
            this.label_textBox = new System.Windows.Forms.Label();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.SuspendLayout();
            // 
            // textBox_ValueEdit
            // 
            this.textBox_ValueEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ValueEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ValueEdit.Location = new System.Drawing.Point(12, 43);
            this.textBox_ValueEdit.MaxLength = 100;
            this.textBox_ValueEdit.Name = "textBox_ValueEdit";
            this.textBox_ValueEdit.Size = new System.Drawing.Size(888, 29);
            this.textBox_ValueEdit.TabIndex = 0;
            // 
            // label_textBox
            // 
            this.label_textBox.AutoSize = true;
            this.label_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_textBox.Location = new System.Drawing.Point(12, 9);
            this.label_textBox.Name = "label_textBox";
            this.label_textBox.Size = new System.Drawing.Size(14, 17);
            this.label_textBox.TabIndex = 2;
            this.label_textBox.Text = "-";
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardcontrol1.Location = new System.Drawing.Point(12, 79);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(888, 337);
            this.keyboardcontrol1.TabIndex = 5;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // CEditStringTouchDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 424);
            this.Controls.Add(this.keyboardcontrol1);
            this.Controls.Add(this.label_textBox);
            this.Controls.Add(this.textBox_ValueEdit);
            this.Icon = ((System.Drawing.Icon)(Properties.Resources.imcr));
            this.Name = "CEditStringTouchDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ValueEdit;
        private System.Windows.Forms.Label label_textBox;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
    }
}