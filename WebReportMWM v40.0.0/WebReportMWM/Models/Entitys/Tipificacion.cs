using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("Tipificaciones")]
    public partial class Tipificacion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }
    }
}