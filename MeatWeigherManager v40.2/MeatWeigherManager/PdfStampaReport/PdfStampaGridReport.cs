using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stampa;
using System.IO;
using StatusProgressBar;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using PdfStampa;

namespace PdfStampaGridReport
{
    /// <summary>
    /// Clase que provee funcionalidad para la creacion de reportes PDF que pueden
    /// contener una grilla. Por ese motivo posee una propiedad de tipo DataTable
    /// que puede contener una tabla para la grilla.
    /// Hereda de CPdfStampa y esta clase base Utiliza la libreria Stampa.
    /// </summary>
    public class CPdfStampaGridReport:CPdfStampa
    {
        DataTable m_dataSource;

        public DataTable DataSource { get => m_dataSource; set => m_dataSource = value; }

        public CPdfStampaGridReport():base()
        {

        }

        public CPdfStampaGridReport(string pathDestinationReport,string nameFileFormatXML, string nameFilePDF, string companyDescription,
            string titleDescription, string reportDescriptionLine1, string reportDescriptionLine2, string fullPathLogoCompany, DataTable dataSource):base(pathDestinationReport,nameFileFormatXML,nameFilePDF,companyDescription,
            titleDescription,reportDescriptionLine1,reportDescriptionLine2,fullPathLogoCompany)
        {
            this.m_dataSource = dataSource;
        }

        public override void SetAdditionalData(ReportDocument report)
        {
            report.AddData(DataSource);
            base.SetAdditionalData(report);
        }
    }
}
