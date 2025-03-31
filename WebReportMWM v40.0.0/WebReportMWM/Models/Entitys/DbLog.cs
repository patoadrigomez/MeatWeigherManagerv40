using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("dbLog")]
    public partial class DbLog
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Fehca_Hora { get; set; }

        [Required]
        public int IdEstacion { get; set; }

        [Required]
        public int IdOperador { get; set; }

        [StringLength(100)]
        [Required]
        public string Evento { get; set; }

        [StringLength(100)]
        [Required]
        public string Contexto { get; set; }

        [StringLength(300)]
        [Required]
        public string Detalle { get; set; }

    }
}