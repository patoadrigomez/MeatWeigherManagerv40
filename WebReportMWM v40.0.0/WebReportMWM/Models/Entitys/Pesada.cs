using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("Pesadas")]
    public partial class Pesada
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int IOperador { get; set; }

        [Required]
        public int IEstacion { get; set; }
        public int IdOi { get; set; }
        public int IdDestino { get; set; }
        public int IdSector { get; set; }

        [Required]
        public DateTime Fecha_Hora { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int Unidades { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double PesoNeto { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double PesoTara { get; set; }

        public int IdGrupo { get; set; }
        public int IdPieza { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double? PesoRemitido { get; set; }

        public int NumTropa { get; set; }
        public int IdTipificacion { get; set; }
    }
}