using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models.Entitys
{
    public class ProductoSAC
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
    }
}