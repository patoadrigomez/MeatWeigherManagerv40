using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("Inventario")]
    public class Inventario
    {
        [Required]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public DateTime FechaInicioInventario { get; set; }

        [Required]
        public DateTime FechaHoraCaptura { get; set; }

        [Required]
        public int IdDestino { get; set; }

        [Key]
        [Column(Order = 4)]
        [Required]
        [StringLength(30)]
        public string IdPieza { get; set; }

    }
}