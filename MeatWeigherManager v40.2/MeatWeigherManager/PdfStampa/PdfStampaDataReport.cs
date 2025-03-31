using System.Drawing;
using System.Diagnostics;
using PdfStampa;
using System.Collections.Generic;
using Stampa;
using System.Data;

namespace PdfStampa
{
    /// <summary>
    /// Clase que provee funcionalidad para la creacion de reportes PDF que pueden
    /// contener no solo una grilla de datos si no tambien una x cantidad de campos de datos
    /// adicionales pasados como una lista de Nombre de Campo - Valor.
    /// Hereda de CPdfStampa y esta clase base Utiliza la libreria Stampa.
    /// </summary>
    public class CPdfStampaDataReport : CPdfStampa
    {
        public List<CTextField> ListTextFields = new List<CTextField>();
        DataTable m_dataSource;
        public DataTable DataSource { get => m_dataSource; set => m_dataSource = value; }


        public CPdfStampaDataReport() : base()
        {

        }

        public CPdfStampaDataReport(string pathDestinationReport, string nameFileFormatXML, string nameFilePDF, string companyDescription,
            string titleDescription, string reportDescriptionLine1, string reportDescriptionLine2, string fullPathLogoCompany, List<CTextField> listTextFields,DataTable dataSource) : base(pathDestinationReport, nameFileFormatXML, nameFilePDF, companyDescription,
            titleDescription, reportDescriptionLine1, reportDescriptionLine2, fullPathLogoCompany)
        {
            this.ListTextFields = listTextFields;
            this.DataSource = dataSource;
        }


        public override void SetAdditionalData(ReportDocument report)
        {
            foreach (CTextField field in ListTextFields)
            {
                report.AddText(field.Name,field.Value);
            }
            report.AddData(DataSource);
            base.SetAdditionalData(report);
        }
    }

    public class CTextField
    {
        string m_name;
        string m_value;

        public CTextField()
        {

        }

        public CTextField(string _name, string _value)
        {
            m_name = _name;
            m_value = _value;
        }

        public string Name { get => m_name; set => m_name = value; }
        public string Value { get => m_value; set => m_value = value; }
    }
}