using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AB_ListItemsDlg
{
    public partial class CAB_ListItemsDlg : Form
    {
        List<string> m_listItems = new List<string>();
        public List<string> ListItems { get => m_listItems; set => m_listItems = value; }
        public TextBox TextBoxItem { get => textBox_Item; set => textBox_Item = value; }

        BindingList<string> m_bindingSources;

        public CAB_ListItemsDlg()
        {
            InitializeComponent();
        }

        public CAB_ListItemsDlg(string titleDgl,string nameItem)
        {
            InitializeComponent();
            label_Item.Text = nameItem;
            Text = titleDgl;
        }

        private void CAB_ListItemsDlg_Load(object sender, EventArgs e)
        {
            m_bindingSources = new BindingList<string>(ListItems);
            listBox_Items.DataSource = m_bindingSources;
        }

        private void button_AgregarItem_Click(object sender, EventArgs e)
        {
            if(textBox_Item.Text != "")
            {
                if(!m_listItems.Exists(x=> x == textBox_Item.Text))
                {
                    m_bindingSources.Add(textBox_Item.Text);
                    textBox_Item.Clear();
                }
            }
        }

        private void button_EliminarItem_Click(object sender, EventArgs e)
        {
            if(listBox_Items.SelectedItem != null)
            {
                m_bindingSources.Remove((string)listBox_Items.SelectedItem);
            }
        }

    }
}
