using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    public class ProductoEnCombo
    {
        public int IdProductoCombo { get; set; }
        public int IdProducto { get; set; }

        [Required]
        public int Unidades { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double Peso { get; set; } = 0.0;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        [Display(Name = "Tolerancia Peso")]
        public Double? ToleranciaPeso { get; set; } = 0.0;

        [Display(Name = "Validar Unidades")]
        public bool? ValidarUnds { get; set; } = false;

        [Display(Name = "Validar Peso")]
        public bool? ValidarPeso { get; set; } = false;

        [Display(Name = "Nombre Combo")]
        public string NombreCombo { get; set; }

        [Display(Name = "Nombre Producto")]
        public string NombreProducto { get; set; }
    }
}