using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    [Table("Combos")]
    public partial class Combo
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public int IdProductoCombo { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int Unidades { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double Peso { get; set; } = 0.0;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        [Display(Name = "Tolerancia Peso")]
        public Double? ToleranciaPeso { get; set; } = 0.0;

        [Display(Name = "Validar Unidades")]
        public bool? ValidarUnds { get; set; } = false;

        [Display(Name = "Validar Peso")]
        public bool? ValidarPeso { get; set; } = false;

        
        [NotMapped]
        [Display(Name = "Nombre Combo")]
        public string NombreCombo { get; set; }

        [NotMapped]
        public List<ProductoEnCombo> ProductosEnCombo { get; set; }
    }
}