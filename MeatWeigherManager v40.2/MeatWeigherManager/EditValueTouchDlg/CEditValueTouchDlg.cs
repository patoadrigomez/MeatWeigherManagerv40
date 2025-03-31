using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditValueTouchDlg
{
    public partial class CEditValueTouchDlg:Form
    {
        public enum TYPE_VALUE
        {
            NUMERIC,
            FLOAT,
            STRING
        }

        string m_value;

        TYPE_VALUE m_tipeValue = TYPE_VALUE.STRING;

        public string VALUE
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public CEditValueTouchDlg()
        {
            InitializeComponent();
        }

        public CEditValueTouchDlg(string value, string nameValue, string titleDlg, TYPE_VALUE tipeValue = TYPE_VALUE.STRING, int maxLength = 100, bool typePassword = false)
        {
            InitializeComponent();
            m_value = value;
            textBox_value.Text = value;
            label_value.Text = nameValue;
            Text = titleDlg;
            m_tipeValue = tipeValue;
            if (typePassword)
                textBox_value.PasswordChar = '*';
            textBox_value.MaxLength = maxLength;
        }

        private void textBox_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b' || (m_tipeValue == TYPE_VALUE.FLOAT && e.KeyChar == ','))
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void keyboardcontrol_UserKeyPressed(object sender, KeyboardClassLibrarySjf.KeyboardEventArgs e)
        {
            if (e.KeyboardKeyPressed != "{ENTER}")
            {
                textBox_value.Focus();
                SendKeys.Send(e.KeyboardKeyPressed);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBox_value_TextChanged(object sender, EventArgs e)
        {
            m_value = textBox_value.Text;
        }
    }
}
