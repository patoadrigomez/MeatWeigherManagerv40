﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models.Entitys
{
    [Table("OI")]
    public partial class OrdenIngreso
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdOperador { get; set; }

        [Required]
        public int IdEstacion { get; set; }

        [Required]
        public DateTime Fecha_Hora { get; set; }

        [Required]
        public string CodigoProveedorSAC { get; set; }

        [Required]
        public string IdCertSanitario { get; set; }

        public bool? Activo { get; set; }
    }
}