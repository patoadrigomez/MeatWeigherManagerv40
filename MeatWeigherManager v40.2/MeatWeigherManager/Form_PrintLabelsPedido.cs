using Db;
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
    public partial class Form_PrintLabelsPedido : Form
    {
        public CPEgreso DatPedido { get; set; }
        private int TotalBultos { get; set; } = 0;

        public Form_PrintLabelsPedido(CPEgreso _datPedido,int totalBultos=0)
        {
            InitializeComponent();
            DatPedido = _datPedido;
            TotalBultos = totalBultos;
        }

        private void textBox_nroBulto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Form_PrintLabelsPedido_Load(object sender, EventArgs e)
        {
            textBox_totalBultos.Text = TotalBultos.ToString();
            textBox_cantDuplicados.Text = "0";
            bigCheckBox_numerarBultos.Checked = false;
        }

        private void textBox_totalBultos_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Total Bultos:", "Editar Total de Bultos ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }
        private void textBox_cantDuplicados_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Duplicados:", "Editar Cantidad de Duplicados", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void button_printLabes_Click(object sender, EventArgs e)
        {
            if(esValidoNumeracionBultos())
            {
                CLabel.PrintPedido(DatPedido, Convert.ToInt32(textBox_totalBultos.Text),bigCheckBox_numerarBultos.Checked,Convert.ToInt16(textBox_cantDuplicados.Text ));
            }
        }
        
        private bool esValidoNumeracionBultos()
        {
            bool esValido = false;
            int totalBultos;
            int.TryParse(textBox_totalBultos.Text, out totalBultos);
            if (totalBultos>0 )
                esValido = true;
            else
                MessageBox.Show("La cantidad total de bultos no puede ser cero", "Validación de numeración de Bultos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return esValido;
        }

    }
}
