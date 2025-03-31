using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListViewItemExt
{
    /// <summary>
    /// ListViewItemExt es una personalizacion de la clase ListViewItem
    /// que permite mapear un objeto en un item del listview.
    /// 
    /// Se debe crear las columnas manualmente en el control listview.
    /// El nombre de la columna sera asignado en en la inicializacion del dialogo:
    /// 
    ///   public mainForm()
    ///    {
    ///        InitializeComponent();
    ///        colID.Name = "colID";
    ///        colName.Name = "colName";
    ///        colDate.Name = "colDate";
    ///    }
    /// 
    /// En el modelo de la clase que se realizara el Mapping se debe decorar cada propiedad con un atributo 
    /// personalizado que manifiesta el nombre de la columna a la que pertenece el atributo:
    ///     [Category("Data")]
    ///     [ListViewColumn("colID")]
    ///     public int ID
    ///     {
    ///         get { return _id; }
    ///         set { _id = value; }
    ///     }
    ///     El Atributo Categoria es para asignar un nombre de categoria a cada propiedad por
    ///     si esta es mostrada en un control del tipo PropertyGrid
    /// </summary>
    /// 
    public class ListViewItemExt : ListViewItem
    {
        private object _data;

        public object Data
        {
            get { return _data; }
        }

        public ListViewItemExt(ListView parentListView, object data)
        {
            Update(data, parentListView);
        }

        public void Update(object data)
        {
            if (this.ListView != null)
            {
                Update(data, this.ListView);
            }
            else
            {
                throw new Exception("The item does not contain a refference of a ListViewControl, please use the provided overload of this method and specify a ListView control");
            }
        }

        public void Update(object data, ListView listView)
        {
            this.SubItems.Clear();
            Type typeOfData = data.GetType();
            bool completed_column = false;
            foreach (ColumnHeader column in listView.Columns)
            {
                completed_column = false;
                foreach (PropertyInfo pInfo in typeOfData.GetProperties())
                {
                    foreach (object pAttrib in pInfo.GetCustomAttributes(true))
                    {
                        if (pAttrib.GetType() == typeof(ListViewColumnAttribute))
                        {
                            if (pAttrib.ToString() == column.Name)
                            {
                                if (column.DisplayIndex == 0)
                                {
                                    this.Text = pInfo.GetValue(data, null).ToString();
                                    completed_column = true;
                                    break;
                                }
                                else
                                {
                                    this.SubItems.Add(pInfo.GetValue(data, null).ToString());
                                    completed_column = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (completed_column)
                    {
                        break;
                    }
                }
            }
            _data = data;
        }
    }
}
