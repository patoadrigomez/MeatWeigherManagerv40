using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models
{
    public class ReportEgresosTotalizadosxProdFechaModel
    {
        public string cliente { get; set; } = "";
        public string comprobantePedido { get; set; } = "";

        public string tipoBulto { get; set; } = "";

        public string idTipoProducto { get; set; } = "0";
        public string idProducto { get; set; } = "0";



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

        public List<SelectListItem> ListCliente = null;
        public List<SelectListItem> ListTiposBulto = null;
        public List<SelectListItem> ListTiposProducto = null;
        public List<SelectListItem> ListProductos = null;
    }
}