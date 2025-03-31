using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models
{
    public class ReportTrazabilidadLoteModel
    {
        
        [DisplayName("Lote")]
        [StringLength(8)]
        [DisplayFormat(DataFormatString = "{0:00000000}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(0?[1-9]|[12][0-9]|3[01])(0?[1-9]|1[012])\d{4}$", ErrorMessage = "Valor de lote incorrecto , espera 8 digitos numericos, 2(dia)2(mes)4(año)")]
        public string lote { get; set; } = "";

        public DataTable DatTable { get; set; }
    }
}