using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// ReportPDF es una clase que permite la generacion de reportes en formato PDF.
/// La estructura del reporte se compone:
/// -Logo Empresa.
/// -Titulo.
/// -Subtitulo Linea1.
/// -Subtitulo Linea2.
/// -Tabla con la grilla de datos.
/// -Pie de Pagina con una linea de texto y numerador de hoja.
/// 
/// Propiedades:
///  DataTable      : DataTable que contiene toda la informacion de la grilla de datos.
///  HeaderData     : Contiene los valores que corresponden al encabezado del reporte.
///  FooterData     : Contiene los valores que corresponden al Pie de Pagina del reporte.
///
///  ColumnsReport  : Declara cada una de las columnas que deben ser visualizadas en el reporte.
///                   Cada columna en la coleccion debe existir en el DataTable. El nombre de la
///                   columna debe ser exactamente igual que el que posee la columna del datatable.
///                   Se podra asignar las siguientes a cada columna:
///                   
///                   Nombre:       Nombre de columna (debe coincidir con la columna del DataTable).
///                   TextHeader:   Texto que debe aparecer en el header de la columna.
///                   WidthPercent: Ancho que tendra la columna en porcentaje . Si se coloca 0.0 se asignara
///                                 en forma automatica un ancho porcentual por distrubucion.
///                   Alignment:    Alineacion que debe tener la columna.
///                     
///                   *Si no se declara ninguna columna en esta propiedad se utilizan todas las columnas 
///                   que estan en el DataTable , los anchos de columnas se distribuyen de forma equitativa.
///                   
/// Esta clase utiliza la libreria Itext7 para generar los reportes PDF.
/// 
/// </summary>
namespace REPORTPDF
{
    public class ReportPDF
    {

        public DataTable DatTable { get; set; }
        public HeaderReportData HeaderData { get; set; }
        public FooterReportData FooterData { get; set; }
        public ColumnReportPDFCollection ColumnsReport { get; set; }
        public HeaderTable HeaderTable { get; set; }

        public ReportPDF()
        {
            HeaderData = new HeaderReportData();
            FooterData = new FooterReportData();
            ColumnsReport = new ColumnReportPDFCollection();
            HeaderTable = new HeaderTable();
        }

        public ReportPDF(DataTable datTable, HeaderReportData headerData,FooterReportData footerData, ColumnReportPDFCollection columnsReports=null)
        {
            DatTable = datTable;
            FooterData = footerData;
            HeaderData = headerData;
            ColumnsReport = columnsReports;
        }


        /// <summary>
        /// Genera un reporte PDF en formato MemoryStream 
        /// </summary>
        /// <returns></returns>
        public MemoryStream Make()
        {
            bool makeMarkRowGroupe = false;

            MemoryStream ms = new MemoryStream();
            PdfWriter pdfWriter = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document doc = new Document(pdfDocument, PageSize.LEGAL.Rotate());
            doc.SetMargins(130.0f, 20.0f, 50.0f, 20.0f);
     

            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(HeaderData));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler(FooterData));

            //verifico si se han definido columnas para el reporte y si no se ha hecho se asignan
            //todas las columnas que pose el DataTable.
            if(ColumnsReport == null || ColumnsReport.Count == 0)
            {
                foreach (DataColumn dc in DatTable.Columns)
                {
                    ColumnsReport.Add(new ColumnReportPDF()
                    {
                        Name=dc.ColumnName
                    });
                }
            }

            //Create the table
            Table table = new Table(UnitValue.CreatePercentArray(getWidthsColumns())).UseAllAvailableWidth();

            //Table header
            foreach (ColumnReportPDF col in ColumnsReport)
            {
                if (DatTable.Columns.Contains(col.Name))
                {
                    Cell cell = new Cell();
                    cell.Add(new Paragraph(col.TextHeader))
                        .SetFontSize(HeaderTable.FontSize)
                        .SetFontColor(ColorConstants.BLUE)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetTextAlignment(TextAlignment.CENTER);
                    table.AddHeaderCell(cell);
                }
            }
           
            //table cells
            foreach (DataRow dr in DatTable.Rows)
            {
                makeMarkRowGroupe = HaveToMarkGrouping(dr);
                foreach (ColumnReportPDF cr in ColumnsReport)
                {
                    if (DatTable.Columns.Contains(cr.Name))
                    {
                        Paragraph paragraph = new Paragraph(String.Format("{0:" + cr.Format + "}", 
                            cr.IsGrouped && makeMarkRowGroupe? "--":dr[cr.Name]));

                        Cell cell = new Cell();
                        cell.Add(paragraph)
                            .SetFontSize(9)
                            .SetTextAlignment((TextAlignment)cr.Alignment);
                        table.AddCell(cell);
                    }
                }
            }

            //adaptar los anchos de columnas al ancho del documento.
            table.SetFixedLayout();
            table.SetKeepTogether(true);

            doc.Add(table);
            doc.Close();

            byte[] bytesStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Obtiene un array de los anchos de columnas en valor porcentual.
        /// </summary>
        /// <returns></returns>
        private float[] getWidthsColumns()
        {
            //Obtiene un ancho a asignar a las columnas que no se le asigno ancho (=0.0f).
            float anchoColumnaAsignar = 0.0f;
            int totalColumnasSinAnchoAsignado = ColumnsReport.Where(x => x.WidthPercent == 0.0f).Count();
            float totalAnchosAsignadosColumnas = ColumnsReport.Select(x => x.WidthPercent).Sum();

            if (totalColumnasSinAnchoAsignado >0 && totalAnchosAsignadosColumnas < 100.0f)
            {
                anchoColumnaAsignar = (100.0f - totalAnchosAsignadosColumnas) / totalColumnasSinAnchoAsignado;
            }

            float[] widthsColumns = new float[ColumnsReport.Count];
            for(int i=0;i< ColumnsReport.Count;i++)
            {
                widthsColumns[i] = ColumnsReport[i].WidthPercent == 0.0f ? anchoColumnaAsignar: ColumnsReport[i].WidthPercent;
            }
            return widthsColumns;
        }

        /// <summary>
        /// Verifica si se debe realizar una marca de grupo en el registro a imprimir.
        /// Compara el ultimo valor escrito en cada columna que forma parte del grupo con los nuevos valores a escribir 
        /// de cada una de esas columnas. Si la comparacion resulta igual se retorna true.
        /// Si la comparacion es false se actuliza en cada columna el valor de la ultima escritura.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnsReport"></param>
        /// <returns></returns>
        private bool HaveToMarkGrouping(DataRow dr)
        {
            bool makeMark = false;
            bool compareGroupEqual = true;
            foreach(ColumnReportPDF cr in ColumnsReport.Where(x=>x.IsGrouped==true).Select(x=>x))
            {
                if (!(compareGroupEqual &= (dr[cr.Name].Equals(cr.LastWrittenValue))))
                    cr.LastWrittenValue = dr[cr.Name];
                
                makeMark = compareGroupEqual;
            }
            return makeMark;
        }
        
    }
}
