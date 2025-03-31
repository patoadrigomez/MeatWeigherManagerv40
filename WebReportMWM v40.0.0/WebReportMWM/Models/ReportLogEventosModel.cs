using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Glimpse.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebReportMWM.Models.Enum;

namespace WebReportMWM.Models
{
    public class ReportLogEventosModel
    {
        public string detalle { get; set; } = "";
        public string contexto { get; set; } = "";
        public string evento { get; set; } = "";

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

        public List<SelectListItem> ListContexto { get; set; } = null;

        public List<SelectListItem> ListEvento { get;set; } = null;

    }
}
