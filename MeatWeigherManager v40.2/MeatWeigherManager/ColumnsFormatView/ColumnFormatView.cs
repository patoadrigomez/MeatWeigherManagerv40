using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatColumn
{
    /// <summary>
    /// Tipos de formatos de Visualizacion que pueden asignarce a una columna
    /// </summary>
    public enum FormatViewType
    {
        NONE,
        FLOAT,
        FLOAT_ROUND2,
        DATETIME_ONLYDATE,
        TIMESPAN_WHITOUT_SECONDS,
        INTEGER
    }
    /// <summary>
    /// Indica de que manera debe visualizarce una columna 
    /// </summary>
    public class ColumnFormatView
    {
        public string Name { get; set; }
        public FormatViewType FormatView { get; set; }
    }
}
