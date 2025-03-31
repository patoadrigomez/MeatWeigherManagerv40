using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models
{
    public class ReportTrazabilidadPiezaModel
    {
        public int numPieza { get; set; }

        public DataTable DatTable { get; set; }
    }
}