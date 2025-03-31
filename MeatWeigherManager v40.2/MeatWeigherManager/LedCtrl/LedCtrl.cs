using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LedCtrl
{
    public partial class LedCtrl : UserControl
    {

        Color m_colorOn = Color.GreenYellow;
        Color m_colorOff = Color.DarkGreen;
        Color m_alternateColor = new Color();
        protected const int m_iFlashIntervalMid = 500;
        protected const int m_iFlashIntervalFast = 200;
        protected const int m_iFlashIntervalSlow = 1000;
        protected const int m_iFlashIntervalBlipOn = 70;
        protected int iFlashPeriodON;
        protected int iFlashPeriodOFF;
        protected bool m_bIsFlashEnabled = false;
        ENUMFLASHINTERVALSPEED m_intervalSpeed = ENUMFLASHINTERVALSPEED.Mid;
        private bool m_ledStatus = false;
        private int m_edgeWidth = 8;
        private Color m_edgeColor = Color.Red;


        [Description("Select Flasher Interval")]
        public enum ENUMFLASHINTERVALSPEED { Slow = 0, Mid = 1, Fast = 2, BlipSlow = 3, BlipMid = 4, BlipFast = 5 }

        [Description("Color a Establecer para cuando el Led esta encendido"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance"), DefaultValue(typeof(Color), "Green")]
        [Browsable(true)]
        public Color ColorOn
        {
            get { return m_colorOn; }
            set { m_colorOn = value; }
        }

        [Description("Color a Establecer para cuando el Led esta apagado"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance"), DefaultValue(typeof(Color), "Green")]
        [Browsable(true)]
        public Color ColorOff
        {
            get { return m_colorOff; }
            set { m_colorOff = value; }
        }

        public bool FlasherLedStatus { get { return m_bIsFlashEnabled; } }       // True = flashing, false = inactive.


        [Description("Intervalo de Parpadeo a Establecer"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance"), DefaultValue(typeof(ENUMFLASHINTERVALSPEED), "Mid")]
        [Browsable(true)]
        public ENUMFLASHINTERVALSPEED IntervalSpeed
        {
            get { return m_intervalSpeed; }
            set { m_intervalSpeed = value; }
        }


        [Description("Ancho de Trazo del contorno del circulo del led"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance"), DefaultValue(typeof(int), "3")]
        [Browsable(true)]
        public int EdgeWidth
        {
            get { return m_edgeWidth; }
            set { m_edgeWidth = value; this.Invalidate(); }
        }

        [Description("Color del Contorno del circulo del led"), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        [CategoryAttribute("Appearance"), DefaultValue(typeof(Color), "Red")]
        [Browsable(true)]
        public Color EdgeColor
        {
            get { return m_edgeColor; }
            set { m_edgeColor = value; this.Invalidate(); }
        }

        
        public bool LedStatus
        {
            get { return m_ledStatus; }
            set { m_ledStatus = value; SetLedOn(m_ledStatus); }
        }

        public LedCtrl()
        {
            InitializeComponent();
            m_alternateColor = ColorOn;
        }

        private void LedCtrl_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphicsObj;

            graphicsObj = this.CreateGraphics();

            Pen myPen = new Pen(EdgeColor,EdgeWidth);
            SolidBrush myBrush = new SolidBrush(m_alternateColor);

            graphicsObj.DrawEllipse(myPen, new Rectangle(5,5, this.ClientSize.Width-10, this.ClientSize.Height-10));
            graphicsObj.FillEllipse(myBrush, new Rectangle(6,6, this.ClientSize.Width - 12, this.ClientSize.Height - 12));

        }

        private void LedCtrl_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_alternateColor = m_alternateColor == m_colorOn ? m_colorOff : m_colorOn;
            this.Invalidate();
        }

        public void FlasherLedStart(ENUMFLASHINTERVALSPEED SelectFlashMode)
        {
            m_intervalSpeed = SelectFlashMode;
            FlasherLedStart();
        }
        public void FlasherLedStart()
        {
            switch (m_intervalSpeed)
            {
                case ENUMFLASHINTERVALSPEED.Slow:
                    iFlashPeriodON = m_iFlashIntervalSlow / 2;
                    iFlashPeriodOFF = iFlashPeriodON;
                    break;
                case ENUMFLASHINTERVALSPEED.Mid:
                    iFlashPeriodON = m_iFlashIntervalMid / 2;
                    iFlashPeriodOFF = iFlashPeriodON;
                    break;
                case ENUMFLASHINTERVALSPEED.Fast:
                    iFlashPeriodON = m_iFlashIntervalFast / 2;
                    iFlashPeriodOFF = iFlashPeriodON;
                    break;
                case ENUMFLASHINTERVALSPEED.BlipSlow:
                    iFlashPeriodON = m_iFlashIntervalBlipOn;
                    iFlashPeriodOFF = m_iFlashIntervalSlow - m_iFlashIntervalBlipOn;
                    break;
                case ENUMFLASHINTERVALSPEED.BlipMid:
                    iFlashPeriodON = m_iFlashIntervalBlipOn;
                    iFlashPeriodOFF = m_iFlashIntervalMid - m_iFlashIntervalBlipOn;
                    break;
                case ENUMFLASHINTERVALSPEED.BlipFast:
                    iFlashPeriodON = m_iFlashIntervalBlipOn;
                    iFlashPeriodOFF = m_iFlashIntervalFast - m_iFlashIntervalBlipOn;
                    break;
                default:
                    return;     // incorrect entry... ignore command.
            }
            if (m_bIsFlashEnabled == false)
            {
                m_bIsFlashEnabled = true;
                timer_flashed.Interval = iFlashPeriodON;
                m_alternateColor = ColorOn;
                timer_flashed.Start();
            }
        }
        public void FlasherLedStop()
        {
            m_bIsFlashEnabled = false;
            m_alternateColor = ColorOff;
            timer_flashed.Stop();
        }

        private void SetLedOn(bool on)
        {
            if (!FlasherLedStatus)
            {
                m_alternateColor = on ? ColorOn : ColorOff;
                this.Invalidate();
            }
        }
    }
}
