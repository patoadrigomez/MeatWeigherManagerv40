using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("TiposBulto")]
    public partial class TipoBulto
    {
        [Required]
        [StringLength(3)]
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }
    }
}