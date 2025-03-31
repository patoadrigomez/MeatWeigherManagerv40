using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("MovInsumos")]
    public partial class MovInsumo
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [StringLength(3)]
        public string IdTipoMov { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        [Required]
        public string IdTipoProc { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int IdProc { get; set; }

        [Key]
        [Column(Order = 3)]
        [Required]
        public int idPrdInsumo { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double Unidades { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Fecha_Hora { get; set; }
    }
}