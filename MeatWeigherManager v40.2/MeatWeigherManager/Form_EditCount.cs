using EditValueTouchDlg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeatWeigherManager
{
    public partial class Form_EditCount : Form
    {
        public short Cantidad { get; set; } = 1;

        public Form_EditCount()
        {
            InitializeComponent();
        }

        private void textBox_cantidad_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Cantidad", "Editar Cantidad ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void textBox_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void CForm_EditCount_Load(object sender, EventArgs e)
        {
            textBox_cantidad.Text = Cantidad.ToString();
        }

        private void button_aceptar_Click(object sender, EventArgs e)
        {
            if(textBox_cantidad.Text != "" && Convert.ToInt16(textBox_cantidad.Text) > 0 )
            {
                Cantidad = Convert.ToInt16(textBox_cantidad.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("El valor de cantidad no puede estar vacio o ser cero", "Validación Edición Cantidad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
