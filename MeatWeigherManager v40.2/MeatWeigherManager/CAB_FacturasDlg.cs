using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AB_ListItemsDlg;
using EditStringTouchDlg;
using System.Windows.Forms;


namespace MeatWeigherManager
{
    class CAB_FacturasDlg : AB_ListItemsDlg.CAB_ListItemsDlg
    {
        public CAB_FacturasDlg(ref List<string> listRemitos):base("Altas y Bajas de Facturas","Factura")
        {
            ListItems = listRemitos;
            TextBoxItem.MouseDoubleClick += CAB_FacturasDlg_MouseDoubleClick;
            TextBoxItem.KeyPress += TextBoxItem_KeyPress;
            TextBoxItem.MaxLength = 20;
        }

        private void TextBoxItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b' || e.KeyChar == '-')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void CAB_FacturasDlg_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Numero de Factura", "Factura", base.TextBoxItem.Text, 15);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                base.TextBoxItem.Text = dlg.VALUE;
            }
        }
    }
}
