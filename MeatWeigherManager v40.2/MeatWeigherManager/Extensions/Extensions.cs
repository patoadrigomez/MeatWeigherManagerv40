using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Obtiene el string del atributo Description de un Enum
        /// El enum debe estar definido de esta forma:
        ///
        ///   [Description("INGRESO")]
        ///   Ingreso,
        /// 
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string ToStringValue(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var stringValue = ((DescriptionAttribute)attributes[0]).Description;
            return stringValue;
        }

        public static T Swap<T>(this T x, ref T y)
        {
            T t = y;
            y = x;
            return t;
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        ///     Example Use:
        ///     
        ///     public enum Estados
        ///     {
        ///          [Display(Name = "En Reposo")]
        ///          Reposs,
        ///     }
        ///     .....
        ///     public Estados Estado = Estados.Reposs;
        ///     string name = Estado.GetAttribute<DisplayAttribute>().Name;
        ///     
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }


        /// <summary>
        /// Obtiene un miembro de un Enum a partir de su nombre.
        /// El string de nombre es IgnoreCase.
        /// Ejemplo de uso:
        /// string umName = "KG"
        /// UNIDAD_MEDIDA undm = umName.GetEnumValue<UNIDAD_MEDIDA>();
        ///
        /// UNIDAD_MEDIDA undm = "UND".GetEnumValue<UNIDAD_MEDIDA>();
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetEnumValue<T>(this string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            return Enum.TryParse(str, true, out T val) ? val : default;
        }

        /// <summary>
        /// Obtiene un miembro de un Enum a partir de su valor entero.
        ///    int x = 0;
        ///    UNIDAD_MEDIDA undm = x.GetEnumValue<UNIDAD_MEDIDA>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetEnumValue<T>(this int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T)Enum.ToObject(enumType, intValue);
        }

        /// <summary>
        /// Convierte a Objeto de una clase desde un DataRow
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T ToObject<T>(this DataRow dataRow) where T : new()
        {
            T item = new T();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                if (dataRow[column] != DBNull.Value)
                {
                    PropertyInfo prop = item.GetType().GetProperty(column.ColumnName);
                    if (prop != null)
                    {
                        object result = Convert.ChangeType(dataRow[column], prop.PropertyType);
                        prop.SetValue(item, result, null);
                        continue;
                    }
                    else
                    {
                        FieldInfo fld = item.GetType().GetField(column.ColumnName);
                        if (fld != null)
                        {
                            object result = Convert.ChangeType(dataRow[column], fld.FieldType);
                            fld.SetValue(item, result);
                        }
                    }
                }
            }
            return item;
        }


        /// <summary>
        /// DataReader Map to List 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T> DataReaderMapToList<T>(this IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);

            var columnsNames = Enumerable.Range(0, dr.FieldCount).Select(dr.GetName).ToList();
            columnsNames = columnsNames.ConvertAll(c => c.ToUpper());

            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (columnsNames.Contains(prop.Name.ToUpper()))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            var newvalue = Convert.ChangeType(dr[prop.Name], prop.PropertyType);
                            prop.SetValue(obj, newvalue, null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// Genera un DataTable desde una lista de tipo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Linqlist"></param>
        /// <returns></returns>
        public static DataTable GetDataTable_FromList<T>(this IEnumerable<T> Linqlist, string tableName = "")
        {
            System.Data.DataTable dt = new System.Data.DataTable(tableName);

            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((System.Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        System.Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }


        /**************************************************************************************************
        Metodo:		<T> GetValueColumn<T>
           Obtener el valor de una columna de un DataRow considerando que puede ser null
        Parametros:	(DataRow) dataRow:
               DataRow desde desde donde se leera el valor 
           (string) nombreCampo:
               Nombre del campo a leer
           (T) valorDefault:
               Valor a tomar si es que el campo es null.
        Retorna:    Retorna el valor T obtenido. 
        ***************************************************************************************************/
        public static T GetValueColumn<T>(this DataRow dr, string nombreCampo, T valDefault = default(T))
        {
            T valor = valDefault;
            try
            {
                if (!dr.IsNull(nombreCampo))
                {
                    valor = (T)Convert.ChangeType(dr[nombreCampo], typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return valor;
        }


        /**************************************************************************************************
        Metodo:		<T> GetValueColumn<T>
            Obtener el valor de una celda desde una coleccion DataGridViewCellCollection considerando que puede ser null
        Parametros:	(DataRow) DataGridViewCellCollection:
                DataRow desde desde donde se leera el valor 
            (string) nombreCampo:
                Nombre del campo a leer
            (T) valorDefault:
                Valor a tomar si es que el campo es null.
        Retorna:    Retorna el valor T obtenido. 
        ***************************************************************************************************/

        public static T GetValueColumn<T>(this DataGridViewCellCollection dgvcc, string nombreCampo, T valDefault = default(T))
        {
            T valor = valDefault;
            try
            {
                if (!DBNull.Value.Equals(dgvcc[nombreCampo]) && !DBNull.Value.Equals(dgvcc[nombreCampo].Value) && dgvcc[nombreCampo].Value != null)
                {
                    valor = (T)Convert.ChangeType(dgvcc[nombreCampo].Value, typeof(T));
                }
            }
            catch (InvalidCastException ice)
            {
                throw ice;
            }
            catch (OleDbException e)
            {
                throw e;
            }
            return valor;
        }
        /**************************************************************************************************
        Metodo:		<T> GetValueColumn<T>
           Obtener el valor de una columna de un DataReader considerando que puede ser null
        Parametros:	(OleDbDataReader) dr:
               DataReader desde desde donde se leera el valor 
           (string) nombreCampo:
               Nombre del campo a leer
           (T) valorDefault:
               Valor a tomar si es que el campo es null.
        Retorna:    Retorna el valor T obtenido. 
        ***************************************************************************************************/
        public static T GetValueColumn<T>(this OleDbDataReader dr, string nombreCampo, T valDefault = default(T))
        {
            T valor = valDefault;
            try
            {

                int idxColumna = dr.GetOrdinal(nombreCampo);
                if (!dr.IsDBNull(idxColumna))
                {
                    valor = (T)Convert.ChangeType(dr[nombreCampo], typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return valor;
        }

        /// <summary>
        /// Obtener el valor numerico asignado a un control en su propiedad TEXT
        /// Parametros:	(Control) ctrl:
        ///            Control desde donde se convierte el valor de Text a Int,Uint,Float etc
        ///            (T) valorDefault:
        ///            Valor a tomar si es que el campo es null.
        /// Retorna:    Retorna el valor T obtenido.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctrl"></param>
        /// <param name="valDefault"></param>
        /// <returns></returns>
        public static T GetValue<T>(this Control ctrl, T valDefault = default(T))
        {
            T valor = valDefault;
            try
            {
                if (ctrl != null && !String.IsNullOrEmpty(ctrl.Text))
                {
                    var valtext = ctrl.Text.Replace(",", ".");

                    valor = (T)Convert.ChangeType(valtext, typeof(T), CultureInfo.GetCultureInfo("en-US"));
                }
            }
            catch (Exception ex)
            {
            }
            return valor;
        }

        /// <summary>
        /// Asigna un valor numerico a la propiedad TEXT de un Control
        /// Parametros:	(Control) ctrl:
        ///            Control al que se asigna el valor numerico en su propiedad Text
        /// </summary>
        public static void SetValue<T>(this Control ctrl, T value, string format = "")
        {
            try
            {
                if (ctrl != null)
                {
                    ctrl.Text = String.Format("{0:" + format + "}", value);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Obtiene de un control la instancia del objeto asignado a la propiedad
        /// Tag.
        /// 
        /// USO:
        /// var tipificacion = textBox_tipificacion.CastTag<Tipificacion>();
        /// 
        /// Si la propiedad tag es null retorna una nueva instancia del Typo a Castear.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static T CastTag<T>(this Control ctrl) where T : class, new()
        {
            T valDefault= new T();
            T valor = valDefault;
            try
            {
                if (ctrl != null && ctrl.Tag != null && ctrl.Tag.GetType()==typeof(T))
                {
                    valor = (T)ctrl.Tag;
                }
            }
            catch (Exception ex)
            {
            }
            return valor;
        }


    }
}
