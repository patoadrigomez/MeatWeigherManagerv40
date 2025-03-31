using ListViewItemExt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Db.Entities
{
    public class IngresoConPesoEditado
    {
        [ListViewColumnAttribute("colProducto")]
        public string Producto { get; set; }

        [ListViewColumnAttribute("colTropa")]
        public int Tropa { get; set; }

        [ListViewColumnAttribute("colTipificacion")]
        public Tipificacion Tipificacion { get; set; }

        [ListViewColumnAttribute("colPesoTara")]
        public float PesoTara { get; set; }

        [ListViewColumnAttribute("colPesoNeto")]
        public float PesoNeto { get; set; }

        [ListViewColumnAttribute("colUnidades")]
        public int Unidades { get; set; }
    }

}
