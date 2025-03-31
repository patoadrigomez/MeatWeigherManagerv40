using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    [Table("ProductoInsumos")]
    public partial class ProductoInsumo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int IdProducto { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int IdInsumoPrimario { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int IdInsumoSecundario { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double Unidades { get; set; }

        [Display(Name = "REQUIERE CONFIRMACIÓN")]
        public bool? RequiereConfirmacion { get; set; } = true;

        [NotMapped]
        [Display(Name = "PRODUCTO")]
        public string NombreProducto { get; set; }

        [NotMapped]
        [Display(Name = "INSUMO PRIMARIO")]
        public string NombreInsumoPrimario{ get; set; }

        [NotMapped]
        [Display(Name = "INSUMO SECUNDARIO")]
        public string NombreInsumoSecundario { get; set; }

    }
}