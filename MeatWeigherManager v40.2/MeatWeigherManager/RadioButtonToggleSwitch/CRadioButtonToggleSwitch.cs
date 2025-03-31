using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RadioButtonToggleSwitch
{
    public partial class CRadioButtonToggleSwitch: UserControl
    {

        bool m_toggle = false;

        [Description("Establece si la Palanca esta ON o OFF"), Category("Appearance"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(false), Browsable(true)]
        public bool Toggle
        {
            get { return m_toggle; }
            set { m_toggle = value; radioButtonUC.Checked = m_toggle; }
        }

        public delegate void ToggleChange(bool toglleOn);
        [Description("Evento disparado ante un cambio de estado de la Palanca ON o OFF"), Category("Action"), RefreshProperties(RefreshProperties.Repaint),Browsable(true)]
        public event ToggleChange OnToggleChange;

        public CRadioButtonToggleSwitch()
        {
            InitializeComponent();
        }

        private void radioButtonUC_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonUC.Checked)
            {
                radioButtonUC.Image = global::RadioButtonToggleSwitch.Properties.Resources.imageOffToggleSwitch;
            }
            else
            {
                radioButtonUC.Image = global::RadioButtonToggleSwitch.Properties.Resources.imageOnToggleSwitch;
            }

            m_toggle = radioButtonUC.Checked;

            OnToggleChange?.Invoke(radioButtonUC.Checked);
        }

    }
}
