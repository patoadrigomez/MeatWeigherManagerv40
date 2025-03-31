using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigApp;

namespace MeatWeigherManager
{
    public class ScaleForm : ScaleCnfg
    {
        ScaleSerialCtrl.ScaleSerialCtrl m_scaleSerialCtrl;
        ToolStripStatusLabel m_statusScaleToolStrip;
        TabPage m_tabPageContainsScale;

        public ScaleSerialCtrl.ScaleSerialCtrl ScaleSerialCtrl { get => m_scaleSerialCtrl; set => m_scaleSerialCtrl = value; }
        public ToolStripStatusLabel StatusScaleToolStrip { get => m_statusScaleToolStrip; set => m_statusScaleToolStrip = value; }
        public TabPage TabPageContainsScale { get => m_tabPageContainsScale; set => m_tabPageContainsScale = value; }

        public ScaleForm()
        {

        }
        public ScaleForm(ScaleSerialCtrl.ScaleSerialCtrl scaleCtrl, TabPage tabPageContainsScale, ToolStripStatusLabel statusScaleToolStrip, ScaleCnfg scaleCnfg) : base(scaleCnfg)
        {
            ScaleSerialCtrl = scaleCtrl;
            m_statusScaleToolStrip = statusScaleToolStrip;
            m_tabPageContainsScale = tabPageContainsScale;
            m_tabPageContainsScale.Text = Name;
            m_tabPageContainsScale.Enabled = Enable;
        }
    }
}
