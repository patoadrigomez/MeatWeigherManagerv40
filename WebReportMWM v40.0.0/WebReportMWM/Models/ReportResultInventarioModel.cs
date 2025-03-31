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
    public class ReportResultInventarioModel
    {
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

        public DataTable DatTable { get; set; }
    }
}