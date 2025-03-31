using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    public class InsumosEnProducto
    {
        public int IdProducto { get; set; }

        [Display(Name = "PRODUCTO")]
        public string NombreProducto { get; set; }

        public List<InsumoPrimario> InsumosPrimarios { get; set; }
    }
}