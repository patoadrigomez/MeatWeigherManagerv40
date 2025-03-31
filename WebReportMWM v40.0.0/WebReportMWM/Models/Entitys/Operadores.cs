using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("operadores")]
    public partial class Operadores
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "CONTRASEÑA")]
        public string pasw { get; set; }

        [StringLength(1)]
        public string Tipo { get; set; } = "U";
    }
}