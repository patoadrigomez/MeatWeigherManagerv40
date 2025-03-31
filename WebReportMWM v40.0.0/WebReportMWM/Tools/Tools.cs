using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOOLS
{
    public static class Tools
    {
        /// <summary>
        /// Convierte un datatable en una lista dinamica.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<dynamic> dataTableToDynamicList(DataTable dt)
        {
            if(dt!=null)
            {
                var list = new List<dynamic>(dt.Rows.Count);

                foreach (DataRow row in dt.Rows)
                {
                    var obj = (IDictionary<string, object>)new ExpandoObject();

                    foreach (DataColumn col in dt.Columns)
                    {
                        obj.Add(col.ColumnName, row[col]);
                    }

                    list.Add(obj);
                }
                return list;
            }
            return new List<dynamic>(0); 
        }

        /**************************************************************************************************
        Metodo:		GetValueColumn
                    Obtener el valor de una columna de tipo T de un DataRow.
        Parametros:	(DataRow) record:
                    (string) nombreCampo:
                    (T) valorDefault:
                    Valor a tomar si es que el campo es null.
        Retorna:    Retorna el valor de tipo T obtenido. 
        ***************************************************************************************************/
        public static T GetValueColumn<T>(DataRow dataRow, string nombreCampo, T valDefault = default(T))
        {
            T valor = valDefault;
            try
            {
                if (dataRow[nombreCampo] != DBNull.Value)
                {
                    if (typeof(T) == typeof(bool))
                    {
                        valor = (T)Convert.ChangeType(dataRow[nombreCampo], typeof(bool));
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        valor = (T)Convert.ChangeType(dataRow[nombreCampo], typeof(float));
                    }
                    else
                    {
                        valor = dataRow.Field<T>(nombreCampo);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return valor;
        }


        /* public static List<dynamic> dataSetToDynamicList(DataSet dt)
         {
             if (dt != null)
             {
                 var list = new List<dynamic>(dt.Reset.Rows.Count);
                 []
                 foreach (DataRow row in dt.Rows)
                 {
                     var obj = (IDictionary<string, object>)new ExpandoObject();

                     foreach (DataColumn col in dt.Columns)
                     {
                         obj.Add(col.ColumnName, row[col]);
                     }

                     list.Add(obj);
                 }
                 return list;
             }
             return new List<dynamic>(0);
         }*/
    }
}
