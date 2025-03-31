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
    public class ReportExistenciaStockProxVencimientoModel
    {
        public string idTipoProducto { get; set; } = "0";
        public string idProducto { get; set; } = "0";

        public string idUbicacion { get; set; } = "0";

        public string diasProximidadVencimiento { get; set; } = "0";

        [DisplayName("Fecha Hasta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //este formato es requerido para que la vista funcione bien con los controles tipo "date"
        public DateTime selectDateTo { get; set; } = DateTime.Now;

        public DataTable DatTable { get; set; }

        public List<SelectListItem> ListTipoProductos = null;

        public List<SelectListItem> ListProductos = null;

        public List<SelectListItem> ListUbicacion = null;
    }
}