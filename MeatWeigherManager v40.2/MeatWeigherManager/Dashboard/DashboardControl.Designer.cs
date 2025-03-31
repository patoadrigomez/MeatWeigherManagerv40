namespace TW.Dashboard
{
    partial class DashboardControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardControl));
            this.mLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.mUpDownSplitter = new System.Windows.Forms.SplitContainer();
            this.mUpButton = new System.Windows.Forms.Button();
            this.mImageList = new System.Windows.Forms.ImageList(this.components);
            this.mDownButton = new System.Windows.Forms.Button();
            this.mUpDownSplitter.Panel1.SuspendLayout();
            this.mUpDownSplitter.Panel2.SuspendLayout();
            this.mUpDownSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // mLayoutPanel
            // 
            this.mLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mLayoutPanel.BackColor = System.Drawing.Color.Honeydew;
            this.mLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mLayoutPanel.Name = "mLayoutPanel";
            this.mLayoutPanel.Size = new System.Drawing.Size(541, 521);
            this.mLayoutPanel.TabIndex = 0;
            // 
            // mUpDownSplitter
            // 
            this.mUpDownSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mUpDownSplitter.Location = new System.Drawing.Point(542, 0);
            this.mUpDownSplitter.Name = "mUpDownSplitter";
            this.mUpDownSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mUpDownSplitter.Panel1
            // 
            this.mUpDownSplitter.Panel1.Controls.Add(this.mUpButton);
            this.mUpDownSplitter.Panel1MinSize = 0;
            // 
            // mUpDownSplitter.Panel2
            // 
            this.mUpDownSplitter.Panel2.Controls.Add(this.mDownButton);
            this.mUpDownSplitter.Panel2MinSize = 0;
            this.mUpDownSplitter.Size = new System.Drawing.Size(72, 521);
            this.mUpDownSplitter.SplitterDistance = 258;
            this.mUpDownSplitter.SplitterWidth = 1;
            this.mUpDownSplitter.TabIndex = 1;
            // 
            // mUpButton
            // 
            this.mUpButton.BackColor = System.Drawing.Color.Purple;
            this.mUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mUpButton.ImageIndex = 0;
            this.mUpButton.ImageList = this.mImageList;
            this.mUpButton.Location = new System.Drawing.Point(0, 0);
            this.mUpButton.Name = "mUpButton";
            this.mUpButton.Size = new System.Drawing.Size(72, 258);
            this.mUpButton.TabIndex = 0;
            this.mUpButton.UseVisualStyleBackColor = false;
            this.mUpButton.Click += new System.EventHandler(this.OnUpButton_Click);
            // 
            // mImageList
            // 
            this.mImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mImageList.ImageStream")));
            this.mImageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.mImageList.Images.SetKeyName(0, "UpArrow.bmp");
            this.mImageList.Images.SetKeyName(1, "DownArrow.bmp");
            // 
            // mDownButton
            // 
            this.mDownButton.BackColor = System.Drawing.Color.Purple;
            this.mDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mDownButton.ImageIndex = 1;
            this.mDownButton.ImageList = this.mImageList;
            this.mDownButton.Location = new System.Drawing.Point(0, 0);
            this.mDownButton.Name = "mDownButton";
            this.mDownButton.Size = new System.Drawing.Size(72, 262);
            this.mDownButton.TabIndex = 1;
            this.mDownButton.UseVisualStyleBackColor = false;
            this.mDownButton.Click += new System.EventHandler(this.OnDownButton_Click);
            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mUpDownSplitter);
            this.Controls.Add(this.mLayoutPanel);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(614, 521);
            this.Load += new System.EventHandler(this.DashboardControl_Load);
            this.SizeChanged += new System.EventHandler(this.OnDashboard_Resize);
            this.Resize += new System.EventHandler(this.OnDashboardControl_Resize);
            this.mUpDownSplitter.Panel1.ResumeLayout(false);
            this.mUpDownSplitter.Panel2.ResumeLayout(false);
            this.mUpDownSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel mLayoutPanel;
        private System.Windows.Forms.SplitContainer mUpDownSplitter;
        private System.Windows.Forms.Button mUpButton;
        private System.Windows.Forms.Button mDownButton;
        private System.Windows.Forms.ImageList mImageList;
    }
}
