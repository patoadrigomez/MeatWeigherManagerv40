using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    public class InsumoSecundario
    {
        public int IdProducto { get; set; }

        public int IdInsumoPrimario { get; set; }
        public int Id { get; set; }

        [Display(Name = "INSUMO SECUNDARIO")]

        public string NombreProducto { get; set; }
        public string NombreInsumoPrimario { get; set; }
        public string Nombre { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double Unidades { get; set; }

        [Display(Name = "REQUIERE CONFIRMACIÓN")]
        public bool? RequiereConfirmacion { get; set; } = true;
    }
}