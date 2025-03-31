using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EditStringTouchDlg;

namespace MeatWeigherManager
{
    public partial class CLogDlg : Form
    {
        public string m_nombre;
        public string m_password;

        public CLogDlg()
        {
            InitializeComponent();
            m_nombre = "";
            m_password = "";
        }

        private void button_Ingresar_Click(object sender, EventArgs e)
        {
            m_nombre = textBox_Usuario.Text;
            m_password = textBox_Password.Text;
            Close();
        }

        private void textBox_Usuario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar nombre de Usuario", "Usuario", textBox_Usuario.Text, 30);
            if (dlg.ShowDialog() == DialogResult.OK)
                textBox_Usuario.Text = dlg.VALUE;
        }

        private void textBox_Password_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar password Usuario", "Password", textBox_Password.Text, 10, true);
            if (dlg.ShowDialog() == DialogResult.OK)
                textBox_Password.Text = dlg.VALUE;
        }

    }
}
