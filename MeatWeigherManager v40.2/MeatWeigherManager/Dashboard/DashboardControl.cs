using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TW.Dashboard
{
    #region Delegates
    public delegate void ButtonPressedEventHandler(object sender, ButtonPressedEventArgs e);
    #endregion Delegates

    public partial class DashboardControl : UserControl
    {
        #region Private properties
        private float mFontSize = 8.5f;
        #endregion Private properties

        #region Constructor
        public DashboardControl()
        {
            InitializeComponent();
            InitalizeDefaults();
        }
        #endregion Constructor

        #region Public Methods
        #region Projectors
        public int ButtonRows { get; set; }
        public int ButtonColumns { get; set; }
        public Size MinButtonSize { get; set; }
        public bool AutoSizeButtons { get; set; }
        public bool SuspendDashboardRefresh { get; set; } 
        public bool PreserveImageAspectRatio { get; set; }
        public bool ResizeImage { get; set; }
        #endregion Projectors

        public Color UpButtonColor
        {
            get { return mUpButton.BackColor; }
            set { mUpButton.BackColor = value; }
        }

        public Color DownButtonColor
        {
            get { return mDownButton.BackColor; }
            set { mDownButton.BackColor = value; }
        }

        public Color UpDownButtonColor
        {
            set 
            {
                mUpButton.BackColor = value;
                mDownButton.BackColor = value; 
            }
        }

        public Color DashboardColor 
        {
            get { return mLayoutPanel.BackColor; }
            set { mLayoutPanel.BackColor = value; }
        }

        public float ButtonsFontSize 
        {
            get
            {
                return mFontSize;
            }
            set
            {
                mFontSize = value;
                SuspendLayout();
                foreach (Control ctrl in mLayoutPanel.Controls)
                    ctrl.Font = new Font(FontFamily.GenericSansSerif, value);

                this.Invalidate(true);
                ResumeLayout(true); 
            }
        }
        
        public Button AddButton(int id_, string text_, string image_)
        {

            ButtonData btnData = null;

            if (File.Exists(image_))
                btnData = new ButtonData(id_, text_, image_);
            else
                btnData = new ButtonData(id_, text_, Resource.NotFound);
   
            SuspendLayout();

            Button btn = new Button()
            {
                Tag = btnData,
                Text = btnData.Text,
                TextAlign = ContentAlignment.BottomCenter,
                Size = MinButtonSize,
                Image = ResizeBitmap(btnData.Bitmap, MinButtonSize.Width - 10, MinButtonSize.Height - 10),
                BackColor = Color.Orange,
                Font = new Font(FontFamily.GenericSansSerif, mFontSize,FontStyle.Bold),
            };

            mLayoutPanel.Controls.Add(btn);
            btn.Click += new EventHandler(OnButton_Click);

            RefreshLayout();

            ResumeLayout(!SuspendDashboardRefresh);

            UpdateVerticalScrollMax();

            return btn;
        }

        public void RemoveButton(Control button_)
        {
            SuspendLayout();
            if (mLayoutPanel.Controls.Contains(button_))
            {
                button_.Click -= OnButton_Click;
                mLayoutPanel.Controls.Remove(button_);
            }
            
            RefreshLayout();
            ResumeLayout(true);

            UpdateVerticalScrollMax();
        }

        public void RemoveButton(int buttonId_)
        {
            Button btn = null;

            if ((btn = GetButton(buttonId_)) != null)
                RemoveButton(btn);
        }

        public void RemoveAllButtons()
        {
            SuspendLayout();
            mLayoutPanel.Controls.Clear();
            ResumeLayout(true);

            RefreshLayout();
            mLayoutPanel.VerticalScroll.Minimum = mLayoutPanel.VerticalScroll.Maximum = 1;
            mLayoutPanel.VerticalScroll.Value = 1;
            mLayoutPanel.PerformLayout();

            UpdateVerticalScrollMax();
        }

        public void SetButtonBackColor(int buttonId_, Color color_)
        { 
            Button btn = null;

            if ((btn = GetButton(buttonId_)) != null)
                btn.BackColor = color_;
        }

        public void SetButtonForeColor(int buttonId_, Color color_)
        {
            Button btn = null;

            if ((btn = GetButton(buttonId_)) != null)
                btn.ForeColor = color_;
        }

        public void SetButtonsForeColor(Color color_)
        {
            foreach (Control ctrl in mLayoutPanel.Controls)
            {
                if (ctrl is Button)
                {
                    Button btn = (Button)ctrl;
                    btn.ForeColor = color_;
                }
            }
        }

        public void RefreshLayout()
        {
            if (!SuspendDashboardRefresh && AutoSizeButtons)
            {
                SuspendLayout();
                //get size of dashboard
                Size panelSize = mLayoutPanel.Size;

                Size buttonSize = new Size(panelSize.Width / ButtonColumns - 6, panelSize.Height / ButtonRows - 6);

                foreach (Control ctrl in mLayoutPanel.Controls)
                {
                    if (ctrl is Button)
                    {
                        Button btn = (Button)ctrl;
                        ButtonData btnData = (ButtonData)btn.Tag;
                        btn.Size = buttonSize;

                        if (ResizeImage)
                        {
                            if (PreserveImageAspectRatio)
                            {
                                decimal imgRatio = (decimal)btn.Image.Size.Height / (decimal)btn.Image.Size.Width;
                                decimal dstRatio = (decimal)buttonSize.Height / (decimal)buttonSize.Width;

                                if (imgRatio > dstRatio)
                                    btn.Image = ResizeBitmap(btnData.Bitmap, (int)((buttonSize.Height - 10) / imgRatio), buttonSize.Height - 10);
                                else
                                    btn.Image = ResizeBitmap(btnData.Bitmap, buttonSize.Width - 10, (int)((buttonSize.Width - 10) * imgRatio));
                            }
                            else
                                btn.Image = ResizeBitmap(btnData.Bitmap, buttonSize.Width - 10, buttonSize.Height - 10);
                        }
                    }
                }

                ResumeLayout(true);
            }
        }

        public void HideButton(int buttonId_)
        {
            Button btn = null;

            if((btn = GetButton(buttonId_)) != null)
                btn.Visible = false;

            UpdateVerticalScrollMax();
        }

        public void ShowButton(int buttonId_)
        {
            Button btn = null;

            if ((btn = GetButton(buttonId_)) != null)
                btn.Visible = true;

            UpdateVerticalScrollMax();
        }

        public void SetFocusButton(int buttonId_)
        {
            Button btn = null;
            try
            {
                if ((btn = GetButton(buttonId_)) != null)
                {
                    int newPosLayoutPanel = btn.Location.Y ;

                    if (newPosLayoutPanel > mLayoutPanel.VerticalScroll.Maximum)
                        newPosLayoutPanel = mLayoutPanel.VerticalScroll.Maximum;

                    mLayoutPanel.VerticalScroll.Value = newPosLayoutPanel;
                    mLayoutPanel.PerformLayout();
                    btn.Focus();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void HideAllButtons()
        {
            foreach (Control ctrl in mLayoutPanel.Controls)
                ctrl.Visible = false;

            UpdateVerticalScrollMax();
        }

        public void ShowAllButtons()
        {
            foreach (Control ctrl in mLayoutPanel.Controls)
                ctrl.Visible = true;

            UpdateVerticalScrollMax();
        }
        #endregion Public Methods

        //To avoid scrollbar flickering when modifying scroll position and scroll bars are hiden
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #region Private methods
        private void InitalizeDefaults()
        {
            ButtonRows = 3;
            ButtonColumns = 3;
            AutoSizeButtons = true;
            SuspendDashboardRefresh = false;
            MinButtonSize = new Size(64, 64);
            ResizeImage = true;
            PreserveImageAspectRatio = true;
        }

        private void OnDashboard_Resize(object sender, EventArgs e)
        {
            RefreshLayout();
        }

        private void DashboardControl_Load(object sender, EventArgs e)
        {
        }
        
        private void OnDownButton_Click(object sender, EventArgs e)
        {
            DownVerticalScroll();
        }

        private void OnUpButton_Click(object sender, EventArgs e)
        {
            UpVerticalScroll();
        }

        private void DownVerticalScroll()
        {
            try
            {
                int newPos = Math.Abs(mLayoutPanel.AutoScrollPosition.Y) + mLayoutPanel.Size.Height;

                if (newPos > mLayoutPanel.VerticalScroll.Maximum)
                    newPos = mLayoutPanel.VerticalScroll.Maximum;

                mLayoutPanel.VerticalScroll.Value = newPos;

                mLayoutPanel.PerformLayout();
            }
            catch (Exception ex)
            {
            }
        }

        private void UpVerticalScroll()
        {
            try
            {
                int newPos = Math.Abs(mLayoutPanel.AutoScrollPosition.Y) - mLayoutPanel.Size.Height;

                if (newPos < mLayoutPanel.VerticalScroll.Minimum)
                    newPos = mLayoutPanel.VerticalScroll.Minimum;

                //workaround because assingning zero to VerticalScroll.Value 
                //has no effects
                if (newPos == mLayoutPanel.VerticalScroll.Minimum)
                    mLayoutPanel.AutoScrollPosition = new Point(0, newPos);
                else
                    mLayoutPanel.VerticalScroll.Value = newPos;

                mLayoutPanel.PerformLayout();
            }
            catch (Exception ex)
            {
            }
        }

        private void ScrollUp(ScrollableControl control, bool isLargeChange)
        {
            if (control == null)
            {
                return;
            }

            control.VerticalScroll.Enabled = true;
            int changeAmount;

            if (isLargeChange == false)
            {
                changeAmount = control.VerticalScroll.SmallChange * 3; 
            }
            else
            {
                changeAmount = control.VerticalScroll.LargeChange;
            }
 
            int currentPosition = control.VerticalScroll.Value;
            
            if ((currentPosition - changeAmount) > control.VerticalScroll.Minimum)
            {
                control.VerticalScroll.Value -= changeAmount;
            }
            else
            {
                control.VerticalScroll.Value = control.VerticalScroll.Minimum;
            }

            control.PerformLayout();
        }

        private void ScrollDown(ScrollableControl control, bool isLargeChange)
        {
            if (control == null)
            {
                return;
            }

            int changeAmount;

            if (isLargeChange == false)
            {
                changeAmount = control.VerticalScroll.SmallChange * 3;
            }
            else
            {
                changeAmount = control.VerticalScroll.LargeChange;
            }

            int currentPosition = control.VerticalScroll.Value;

            if ((currentPosition + changeAmount) < control.VerticalScroll.Maximum)
            {
                control.VerticalScroll.Value += changeAmount;
            }
            else
            {
                control.VerticalScroll.Value = control.VerticalScroll.Maximum;
            }

            control.PerformLayout();
        }

        private void UpdateVerticalScrollMax()
        {
            int surface = ButtonRows * ButtonColumns;
            mLayoutPanel.VerticalScroll.Maximum = mLayoutPanel.Height * (mLayoutPanel.Controls.Count / surface) + ((mLayoutPanel.Controls.Count % surface) == 0? 0 : 1); 
        }

        private void OnDashboardControl_Resize(object sender, EventArgs e)
        {
            UpdateVerticalScrollMax();
        }

        private Button GetButton(int buttonId_)
        {
            Button retVal = null;
                    
            foreach (Control ctrl in mLayoutPanel.Controls)
                if (ctrl is Button && ((ButtonData)ctrl.Tag).Id == buttonId_)
                {
                    retVal = (Button)ctrl;
                    break;  
                }

            return retVal; 
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        void OnButton_Click(object sender, EventArgs e)
        {
            RaiseButtonPressedEvent((ButtonData)((Button)sender).Tag);
        }
        #endregion Private methods

        #region Events
        public event ButtonPressedEventHandler ButtonPressedEvent;
        #endregion

        #region Event Raisers
        private void RaiseButtonPressedEvent(ButtonData buttonData_)
        {
            ButtonPressedEventHandler tempHandler = ButtonPressedEvent;
            
            if (tempHandler != null)
                tempHandler(this, new ButtonPressedEventArgs(buttonData_));
        }
        #endregion
    }

    #region Helper Classes
    public class ButtonData
    {
        public ButtonData(int id_, string text_, string imageName_)
        {
            Id = id_;
            Text = text_;
            Bitmap = new Bitmap(imageName_);
            BitmapName = imageName_;
        }

        public ButtonData(int id_, string text_, Bitmap imageBMP_)
        {
            Id = id_;
            Text = text_;
            Bitmap = imageBMP_;
            BitmapName = "Not found";
        }

        public ButtonData(ButtonData btn_)
        {
            Id = btn_.Id;
            Text = btn_.Text;
            Bitmap = btn_.Bitmap;
            BitmapName = btn_.BitmapName;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string BitmapName { get; set; }
        public Bitmap Bitmap { get; set; }
    }
   
    public class ButtonPressedEventArgs : EventArgs
    {
        public ButtonData ButtonData { get; set; }

        public ButtonPressedEventArgs(ButtonData buttonData_)
        {
            ButtonData = new ButtonData(buttonData_);
        }
    }
    #endregion Helper Classes
}
