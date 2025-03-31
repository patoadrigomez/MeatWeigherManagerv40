using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WebReportMeatWeigher.Models
{
    public class ReportIngresoProduccionDetallado
    {
        /// <summary>
        ///Filtro de consulta para el id del Proveedor
        /// </summary>
        public int idSector { get; set; } = 0;

        ///<summary>
        ///Filtro de consulta para el id del producto
        /// </summary>
        public int idProducto { get; set; } = 0;

        ///<summary>
        ///Filtro de consulta para el tipo de producto
        /// </summary>
        public int TipoProducto { get; set; } = 0;

        [DisplayName("Fecha Desde")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //este formato es requerido para que la vista funcione bien con los controles tipo "date"
        public DateTime selectDateFrom { get; set; } = DateTime.Now;

        /// <summary>
        /// Fecha hasta donde se realiza la consulta
        /// </summary>
        [DisplayName("Fecha Hasta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //este formato es requerido para que la vista funcione bien con los controles tipo "date"
        public DateTime selectDateTo { get; set; } = DateTime.Now;

        /// <summary>
        /// DataTable .
        /// La misma contendra los datos de la grilla a visualizar
        /// </summary>
        public DataTable DatTable { get; set; }
    }
}