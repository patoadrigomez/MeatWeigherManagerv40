﻿namespace LedCtrl
{
    partial class LedCtrl
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
            this.components = new System.ComponentModel.Container();
            this.timer_flashed = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer_flashed
            // 
            this.timer_flashed.Interval = 1000;
            this.timer_flashed.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LedCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "LedCtrl";
            this.Size = new System.Drawing.Size(195, 189);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LedCtrl_Paint);
            this.Resize += new System.EventHandler(this.LedCtrl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_flashed;
    }
}
