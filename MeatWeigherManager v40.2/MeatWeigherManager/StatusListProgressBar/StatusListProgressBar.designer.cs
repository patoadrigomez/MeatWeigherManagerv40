using System.Windows.Forms;

namespace StatusListProgressBar
{
	partial class CStatusListProgressBar : Form
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
            this.lblStatusAction = new System.Windows.Forms.Label();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar_Splash = new System.Windows.Forms.ProgressBar();
            this.listBox_statusResult = new System.Windows.Forms.ListBox();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStatusAction
            // 
            this.lblStatusAction.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusAction.ForeColor = System.Drawing.Color.Black;
            this.lblStatusAction.Location = new System.Drawing.Point(9, 9);
            this.lblStatusAction.Name = "lblStatusAction";
            this.lblStatusAction.Size = new System.Drawing.Size(420, 31);
            this.lblStatusAction.TabIndex = 0;
            this.lblStatusAction.DoubleClick += new System.EventHandler(this.StatusListProgressBar_DoubleClick);
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.ForeColor = System.Drawing.Color.Black;
            this.lblTimeRemaining.Location = new System.Drawing.Point(11, 193);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(290, 18);
            this.lblTimeRemaining.TabIndex = 2;
            this.lblTimeRemaining.Text = "Time remaining";
            this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.StatusListProgressBar_DoubleClick);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // progressBar_Splash
            // 
            this.progressBar_Splash.BackColor = System.Drawing.Color.Black;
            this.progressBar_Splash.ForeColor = System.Drawing.Color.Lime;
            this.progressBar_Splash.Location = new System.Drawing.Point(9, 164);
            this.progressBar_Splash.Name = "progressBar_Splash";
            this.progressBar_Splash.Size = new System.Drawing.Size(420, 22);
            this.progressBar_Splash.TabIndex = 3;
            // 
            // listBox_statusResult
            // 
            this.listBox_statusResult.BackColor = System.Drawing.Color.Black;
            this.listBox_statusResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox_statusResult.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_statusResult.ForeColor = System.Drawing.Color.White;
            this.listBox_statusResult.FormattingEnabled = true;
            this.listBox_statusResult.Location = new System.Drawing.Point(9, 47);
            this.listBox_statusResult.Name = "listBox_statusResult";
            this.listBox_statusResult.Size = new System.Drawing.Size(420, 108);
            this.listBox_statusResult.TabIndex = 4;
            this.listBox_statusResult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_statusResult_DrawItem);
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(353, 192);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 24);
            this.button_close.TabIndex = 5;
            this.button_close.Text = "Cerrar";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // CStatusListProgressBar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(444, 221);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.listBox_statusResult);
            this.Controls.Add(this.progressBar_Splash);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.lblStatusAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CStatusListProgressBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.DoubleClick += new System.EventHandler(this.StatusListProgressBar_DoubleClick);
            this.ResumeLayout(false);

		}
		#endregion


		private System.Windows.Forms.Label lblStatusAction;
		private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Timer UpdateTimer;
        private ProgressBar progressBar_Splash;
        private ListBox listBox_statusResult;
        private Button button_close;
    }
}