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
    public class ReportEgresoSaldosModel
    {
        public string cliente { get; set; } = "";
        public string comprobantePedido { get; set; } = "";

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

        [DisplayName("Lote")]
        [StringLength(8)]
        [DisplayFormat(DataFormatString = "{0:00000000}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(0?[1-9]|[12][0-9]|3[01])(0?[1-9]|1[012])\d{4}$", ErrorMessage = "Valor de lote incorrecto , espera 8 digitos numericos, 2(dia)2(mes)4(año)")]
        public string lote { get; set; } = "";

        /// <summary>
        /// DataTable .
        /// La misma contendra los datos de la grilla a visualizar
        /// </summary>
        public DataTable DatTable { get; set; }

        public List<SelectListItem> ListCliente = null;
    }
}