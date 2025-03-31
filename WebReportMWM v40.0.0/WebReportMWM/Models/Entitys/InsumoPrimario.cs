using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    public class InsumoPrimario
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Id { get; set; }

        [Display(Name = "INSUMO PRIMARIO")]
        public string Nombre { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.000}")]
        public Double Unidades { get; set; }

        [Display(Name = "REQUIERE CONFIRMACIÓN")]
        public bool? RequiereConfirmacion { get; set; } = true;

        public List<InsumoSecundario> InsumosSecundarios { get; set; }=new List<InsumoSecundario>();
    }
}