using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    [Table("Cajas")]
    public partial class Caja
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProductoCaja { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Producto a Contener !!")] 
        public int IdProducto { get; set; }

        [NotMapped]
        public string NombreCaja { get; set; }

        [NotMapped]
        public string NombreProducto { get; set; }

    }
}