using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    public class StockInsumo
    {
        public int Id { get; set; }
        public string Insumo { get; set; }
        public float Unds { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public float Ajustar { get; set; }
    }
}