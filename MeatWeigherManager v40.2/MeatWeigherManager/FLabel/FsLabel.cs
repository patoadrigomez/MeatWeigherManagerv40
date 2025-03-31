using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FLabel
{
    [Description("Select Flasher Interval")]
    public enum FlashIntervalSpeed { Slow = 0, Mid = 1, Fast = 2, BlipSlow = 3, BlipMid = 4, BlipFast = 5 }

    [Description("Flasher Label Control")]
    public partial class FsLabel : System.Windows.Forms.Label
    {
        protected const int m_iFlashIntervalMid = 500;
        protected const int m_iFlashIntervalFast = 200;
        protected const int m_iFlashIntervalSlow = 1000;
        protected const int m_iFlashIntervalBlipOn = 70;
        protected Color colorOff = SystemColors.Control;
        protected Color colorOn = Color.LightGreen;

        protected bool m_bIsFlashEnabled = false;
        protected int iFlashPeriodON;
        protected int iFlashPeriodOFF;
        protected Timer timer;

        [Browsable(true), CategoryAttribute("Appearance"), 
        Description("Get/Set Label color while 'OFF' flash period or disabled"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]

        public Color FlasherLabelColorOff { get { return colorOff; } set { colorOff = value; } }

        [Browsable(true),CategoryAttribute("Appearance"),
        Description("Get/Set Label color while 'ON' flash period"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]

        public Color FlasherLabelColorOn { get { return colorOn; } set { colorOn = value; } }

        [Browsable(true),CategoryAttribute("Appearance"),
        Description("Get flasher status, True=flashing, False=disabled"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]

        public bool FlasherLabelStatus { get { return m_bIsFlashEnabled; } }       // True = flashing, false = inactive.

        public FsLabel()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        [Browsable(true), CategoryAttribute("Appearance"),
        Description("Enable Label flashing, select interval with standard / blip mode"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]

        public void FlasherLabelStart(FlashIntervalSpeed SelectFlashMode = FlashIntervalSpeed.Mid)
        {
            switch (SelectFlashMode)
            {
                case FlashIntervalSpeed.Slow:
                    iFlashPeriodON = m_iFlashIntervalSlow / 2;
                    iFlashPeriodOFF = iFlashPeriodON;
                    break;
                case FlashIntervalSpeed.Mid:
                    iFlashPeriodON = m_iFlashIntervalMid / 2;
                    iFlashPeriodOFF = iFlashPeriodON;
                    break;
                case FlashIntervalSpeed.Fast:
                    iFlashPeriodON = m_iFlashIntervalFast / 2;
                    iFlashPeriodOFF = iFlashPeriodON;
                    break;
                case FlashIntervalSpeed.BlipSlow:
                    iFlashPeriodON = m_iFlashIntervalBlipOn;
                    iFlashPeriodOFF = m_iFlashIntervalSlow - m_iFlashIntervalBlipOn;
                    break;
                case FlashIntervalSpeed.BlipMid:
                    iFlashPeriodON = m_iFlashIntervalBlipOn;
                    iFlashPeriodOFF = m_iFlashIntervalMid - m_iFlashIntervalBlipOn;
                    break;
                case FlashIntervalSpeed.BlipFast:
                    iFlashPeriodON = m_iFlashIntervalBlipOn;
                    iFlashPeriodOFF = m_iFlashIntervalFast - m_iFlashIntervalBlipOn;
                    break;
                default:
                    return;     // incorrect entry... ignore command.
            }
            if (m_bIsFlashEnabled == false)
            {
                m_bIsFlashEnabled = true;
                timer = new Timer();
                timer.Interval = iFlashPeriodON;
                base.BackColor = colorOn;
                timer.Tick += new EventHandler(TimerOnTick);
                timer.Start();
            }
        }
        [Description("Disable Label flashing")]
        [Category("Layout")]
        [Browsable(true)]
        public void FlasherLabelStop()
        {
            if (timer != null)
            {
                base.BackColor = colorOff;
                timer.Stop();
                timer.Dispose();
            }
            m_bIsFlashEnabled = false;
        }

        protected void TimerOnTick(object obj, EventArgs e)
        {
            if (base.BackColor == colorOff)
            {
                base.BackColor = colorOn;
                timer.Interval = iFlashPeriodON;
            }
            else
            {
                base.BackColor = colorOff;
                timer.Interval = iFlashPeriodOFF;
            }
            this.Invalidate();
        }
        [Browsable(true), CategoryAttribute("Appearance"),
        Description("Set Flasher Color, ON and OFF"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]

        public void FlasherLabelColor(Color colorOn, Color colorOff)
        {
            this.colorOn = colorOn;
            this.colorOff = colorOff;
        }
    }
}
