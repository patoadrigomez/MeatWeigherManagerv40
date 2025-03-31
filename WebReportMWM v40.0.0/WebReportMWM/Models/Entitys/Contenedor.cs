using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("Contenedores")]
    public partial class Contenedor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(3)]
        public string IdTipo { get; set; }
        public int IdProducto { get; set; }

        [Required]
        public int IdEstacion { get; set; }
        public int IdOperador { get; set; }
        public int IdDestino { get; set; }

        [Required]
        public DateTime Fecha_Hora { get; set; }
        public Double? PesoTara { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double? PesoNeto { get; set; }
        public int Unidades { get; set; }
        public DateTime Fecha_Desarmado { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
    }
}