using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("DLP")]
    public class DLP
    {
        [Required]
        public int IdOperador { get; set; }

        [Required]
        public int IdEstacion { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int IdPesaje { get; set; }

        [Required]
        public DateTime Fecha_Hora { get; set; }

        public int IdOi { get; set; }

        [Required]
        public DateTime LotePadre { get; set; }

        public int IdSector { get; set; }

    }
}