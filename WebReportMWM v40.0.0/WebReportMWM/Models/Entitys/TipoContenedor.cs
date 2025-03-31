using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("TiposContenedor")]
    public partial class TipoContenedor
    {
        [Required]
        [StringLength(3)]
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Descripcion { get; set; }
    }
}