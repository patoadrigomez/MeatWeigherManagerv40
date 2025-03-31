using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPORTEXCEL
{
    public class ColumnReportEXCEL
    {
        string name;

        public string Name
        {
            get
            { return name; }
            set
            {
                name = value;
                if (TextHeader == "")
                    TextHeader = name;
            }
        }
        public string TextHeader { get; set; } = "";

        public string Format { get; set; } = "";

        public ColumnAlignment Alignment { get; set; } = ColumnAlignment.LEFT;
        /// <summary>
        /// Marca a la columna como parte de un grupo de clave unica. Se utiliza en reportes
        /// que generan la cabecera y detalle en el mismo registro y se requiere destacar 
        /// estas columnas que pertenecen a la cabecera.
        /// </summary>
        public bool IsGrouped { get; set; } = false;
        /// <summary>
        /// Utilizada solamente por el mecanismo de marca de agrupamiento de columnas
        /// de columnas marcadas como IsGrouped=true.
        /// </summary>
        public Object LastWrittenValue { get; set; } = null;
    }
}
