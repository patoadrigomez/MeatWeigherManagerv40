using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    [Table("Productos")]
    public partial class Producto
    {
        [Required]
        public int Id { get; set; }

        
        [StringLength(12)]
        public string CodigoProductoSac { get; set; }

        [Required(ErrorMessage = "El Nombre del producto no puede quedar vacío.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Tipo de Producto.")]
        [Range(1, int.MaxValue)]
        public int IdTipo { get; set; }
        
        [StringLength(20)]
        public string NumSenasa { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double? PesoNetoPredef { get; set; } = 0.0;

        public int UnidadesPredef { get; set; } = 1;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double? PesoTaraPredef { get; set; }= 0.0;
        
        public int DiasVencimiento { get; set; }
        public bool? EsInsumo { get; set; } = false;
        public bool? EsPesable { get; set; } = true;

        [StringLength(100)]
        public string TextAuxL1 { get; set; }

        [StringLength(100)]
        public string TextAuxL2 { get; set; }
        [StringLength(100)]
        public string NombreL1 { get; set; }

        [StringLength(100)]
        public string NombreL2 { get; set; }

        [StringLength(100)]
        public string NombreL3 { get; set; }

        [StringLength(100)]
        public string NombreL4 { get; set; }

        [StringLength(100)]
        public string NombreL5 { get; set; }

        [StringLength(100)]
        public string NombreL6 { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        [Display(Name = "Rendimiento Estandar (%)")]
        public Double? RendimientoSTD { get; set; } = 0.0;

        public bool? EsCombo { get; set; } = false;
        public bool? EsCaja { get; set; } = false;

        public bool? EsTropa { get; set; } = false;
        public int? IdEtiqueta { get; set; }

        [NotMapped]
        public string Tipo { get; set; }

        [NotMapped]
        public string NombreProductoSac { get; set; } = "";

        [NotMapped]
        [Display(Name = "Nombre Etiqueta")]
        public string NombreEtiqueta { get; set; } = "";

        [NotMapped]
        public string AliasProductoSAC { get; set; } = "";

        [NotMapped]
        public string TipoBulto { get; set; } = "";
    }
}