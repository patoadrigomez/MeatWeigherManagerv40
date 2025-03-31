using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    public class ProductoGestionInsumos
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = "";
        public List<InsumoPrimario> Insumos { get; set; }
    }
}