using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("TiposProducto")]
    public partial class TipoProducto
    {
       
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
    }
}