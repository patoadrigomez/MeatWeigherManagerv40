namespace KeyboardClassLibrarySjf
{
    partial class Keyboardcontrol_Num
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxKeyboard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKeyboard)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxKeyboard
            // 
            this.pictureBoxKeyboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxKeyboard.Image = global::KeyboardClassLibrarySjf.Properties.Resources.keyboard_numeric_grd;
            this.pictureBoxKeyboard.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxKeyboard.Name = "pictureBoxKeyboard";
            this.pictureBoxKeyboard.Size = new System.Drawing.Size(663, 282);
            this.pictureBoxKeyboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxKeyboard.TabIndex = 0;
            this.pictureBoxKeyboard.TabStop = false;
            this.pictureBoxKeyboard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxKeyboard_MouseClick);
            // 
            // Keyboardcontrol_Num
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxKeyboard);
            this.Name = "Keyboardcontrol_Num";
            this.Size = new System.Drawing.Size(663, 282);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKeyboard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxKeyboard;
    }
}
