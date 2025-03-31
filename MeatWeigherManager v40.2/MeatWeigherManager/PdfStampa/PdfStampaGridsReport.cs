using PdfStampa;
using Stampa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfStampa
{
    /// <summary>
    /// Clase que provee funcionalidad para la creacion de reportes PDF que pueden
    /// contener mas de una grilla. Por ese motivo posee una propiedad de tipo DataSet
    /// que puede contener mas de una tabla (una por grilla).
    /// Hereda de CPdfStampa y esta clase base Utiliza la libreria Stampa.
    /// </summary>
    public class CPdfStampaGridsReport : CPdfStampa
    {
        DataSet m_dataSource;

        public DataSet DataSource { get => m_dataSource; set => m_dataSource = value; }

        public CPdfStampaGridsReport() : base()
        {

        }

        public CPdfStampaGridsReport(string pathDestinationReport, string nameFileFormatXML, string nameFilePDF, string companyDescription,
            string titleDescription, string reportDescriptionLine1, string reportDescriptionLine2, string fullPathLogoCompany, DataSet dataSource) : base(pathDestinationReport, nameFileFormatXML, nameFilePDF, companyDescription,
            titleDescription, reportDescriptionLine1, reportDescriptionLine2, fullPathLogoCompany)
        {
            this.m_dataSource = dataSource;
        }

        public override void SetAdditionalData(ReportDocument report)
        {
            foreach (DataTable dt in m_dataSource.Tables)
            {
                report.AddData(dt);
            }
            base.SetAdditionalData(report);
        }
    }
}
