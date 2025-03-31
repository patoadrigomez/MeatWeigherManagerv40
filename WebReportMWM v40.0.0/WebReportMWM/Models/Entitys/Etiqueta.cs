using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("Etiquetas")]
    public partial class Etiqueta
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre de la etiqueta no puede quedar vacío.")]
        [StringLength(9)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción no puede quedar vacía.")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public string IdTipoBulto { get; set; }

        [NotMapped]
        [Display(Name = "Tipo Bulto")]
        public string TipoBulto { get; set; }
    }
}