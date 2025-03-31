using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditStringTouchDlg
{
    public partial class CEditStringTouchDlg : Form
    {
        string m_valueEdit;

        public string VALUE
        {
            get { return m_valueEdit; }
        }

        public CEditStringTouchDlg(string tituloDlg, string tituloEditBoxEdicion, string valueDefault = "", int maxLength = 100, bool typePassword = false)
        {
            InitializeComponent();
            Text = tituloDlg;
            label_textBox.Text = tituloEditBoxEdicion;
            m_valueEdit = valueDefault;
            textBox_ValueEdit.Text = m_valueEdit;
            if (typePassword)
                textBox_ValueEdit.PasswordChar = '*';
            textBox_ValueEdit.MaxLength = maxLength;
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            try
            {
                if (e.KeyboardKeyPressed == "{ENTER}")
                {
                    m_valueEdit = textBox_ValueEdit.Text;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    textBox_ValueEdit.Focus();
                    if (textBox_ValueEdit.MaxLength == textBox_ValueEdit.Text.Length &&
                        (e.KeyboardKeyPressed == "{BACKSPACE}" || e.KeyboardKeyPressed == "{DELETE}" || e.KeyboardKeyPressed == "{LEFT}"))
                    {
                        SendKeys.SendWait(e.KeyboardKeyPressed);
                    }
                    else if (textBox_ValueEdit.MaxLength > textBox_ValueEdit.Text.Length)
                    {
                        if (e.KeyboardKeyPressed == "{^}")
                        {
                            int posCursor = textBox_ValueEdit.SelectionStart;
                            textBox_ValueEdit.Text = textBox_ValueEdit.Text.Insert(posCursor, "^");
                            textBox_ValueEdit.Select(posCursor + 1, 0);
                        }
                        else
                            SendKeys.SendWait(e.KeyboardKeyPressed);
                    }
                }
            }
            catch (ArgumentException aex)
            {
                string str = aex.Message;
            }
        }
    }
}
