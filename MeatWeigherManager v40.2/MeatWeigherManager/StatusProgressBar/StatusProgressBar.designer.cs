using System.Windows.Forms;

namespace StatusProgressBar
{
	partial class CStatusProgressBar : Form
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
            this.lblStatusResult = new System.Windows.Forms.Label();
            this.loadingCircle1 = new IMCR.LoadCircleCtrl.LoadingCircle();
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
            this.lblStatusAction.DoubleClick += new System.EventHandler(this.StatusProgressBar_DoubleClick);
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.ForeColor = System.Drawing.Color.Black;
            this.lblTimeRemaining.Location = new System.Drawing.Point(6, 85);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(417, 18);
            this.lblTimeRemaining.TabIndex = 2;
            this.lblTimeRemaining.Text = "Time remaining";
            this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.StatusProgressBar_DoubleClick);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // progressBar_Splash
            // 
            this.progressBar_Splash.BackColor = System.Drawing.Color.Black;
            this.progressBar_Splash.ForeColor = System.Drawing.Color.Lime;
            this.progressBar_Splash.Location = new System.Drawing.Point(9, 60);
            this.progressBar_Splash.Name = "progressBar_Splash";
            this.progressBar_Splash.Size = new System.Drawing.Size(420, 22);
            this.progressBar_Splash.TabIndex = 3;
            // 
            // lblStatusResult
            // 
            this.lblStatusResult.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusResult.ForeColor = System.Drawing.Color.Red;
            this.lblStatusResult.Location = new System.Drawing.Point(9, 40);
            this.lblStatusResult.Name = "lblStatusResult";
            this.lblStatusResult.Size = new System.Drawing.Size(420, 17);
            this.lblStatusResult.TabIndex = 4;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = true;
            this.loadingCircle1.Color = System.Drawing.Color.OliveDrab;
            this.loadingCircle1.InnerCircleRadius = 10;
            this.loadingCircle1.Location = new System.Drawing.Point(373, 106);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 10;
            this.loadingCircle1.OuterCircleRadius = 13;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(56, 29);
            this.loadingCircle1.SpokeThickness = 4;
            this.loadingCircle1.StylePreset = IMCR.LoadCircleCtrl.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 5;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // CStatusProgressBar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(444, 147);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.lblStatusResult);
            this.Controls.Add(this.progressBar_Splash);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.lblStatusAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CStatusProgressBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.DoubleClick += new System.EventHandler(this.StatusProgressBar_DoubleClick);
            this.ResumeLayout(false);

		}
		#endregion


		private System.Windows.Forms.Label lblStatusAction;
		private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Timer UpdateTimer;
        private ProgressBar progressBar_Splash;
        private Label lblStatusResult;
        private IMCR.LoadCircleCtrl.LoadingCircle loadingCircle1;
    }
}