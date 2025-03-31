using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReportMWM.Models
{
    public class ReportHistoricoPiezaContenedorModel
    {
        public string numPieza { get; set; } = "0";

        public string esContenedor { get; set; } = "0";

        public DataTable DatTable { get; set; }

    }
}