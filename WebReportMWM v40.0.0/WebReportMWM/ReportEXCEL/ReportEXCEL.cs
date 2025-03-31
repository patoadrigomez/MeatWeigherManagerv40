using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ReportEXCEL es una libreria que genera reportes en excel con las siguientes caracteristicas:
/// - Utiliza la libreria EPPlus  (Nuget) en su vercion libre 4.5.3.3. La misma permite la gestion de archivos excel.
/// - Cabecera del reporte con tres lineas de texto.
/// - Recive un DataTable como flujo de datos para la tabla del reporte.
/// - Permite declarar las columnas que se visualizaran como tambien el nombre que se le asignara , el formato y alineacion horizontal.
///   Un ejemplo de asignacion de Formato a una columna podria ser este para el caso de una columna DateTime:
///   "dd-MM-yyyy HH:mm";
///   Si no se declara ninguna columna la libreria generara todas las columnas existentes en el DataTable.
/// - El Metodo Make genera el reporte Excel retornando un MemoryStream.
/// - Si se an definido columnas el orden de las mismas sera el de la coleccion y si no sera el orden que posee el DataTable.
/// </summary>
namespace REPORTEXCEL
{
 
    public class ReportEXCEL
    {
        public DataTable DatTable { get; set; }
        public HeaderReportData HeaderData { get; set; }
        public ColumnReportEXCELCollection ColumnsReport { get; set; }

        const int ROW_INIT_HEAD_REPORT = 1;
        const int COL_INIT_HEAD_REPORT = 1;

        const int ROW_INIT_TABLE_REPORT = 6;
        const int COL_INIT_TABLE_REPORT = 1;

        public ReportEXCEL()
        {
            HeaderData = new HeaderReportData();
            ColumnsReport = new ColumnReportEXCELCollection();
        }

        public ReportEXCEL(DataTable datTable, HeaderReportData headerData, ColumnReportEXCELCollection columnsReport = null)
        {
            DatTable = datTable;
            HeaderData = headerData;
            ColumnsReport = columnsReport;
        }

        /// <summary>
        /// Genera un reporte EXCEL en formato MemoryStream 
        /// </summary>
        /// <returns></returns>
        public MemoryStream Make()
        {
            int rowIdx;
            int colIdx;
            bool makeMarkRowGroupe;

            MemoryStream ms = new MemoryStream();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Hoja1");

            //Titulo del reporte.

            rowIdx = ROW_INIT_HEAD_REPORT;
            colIdx = COL_INIT_TABLE_REPORT;

            using (ExcelRange dateReport = ws.Cells[rowIdx, COL_INIT_HEAD_REPORT, rowIdx, COL_INIT_HEAD_REPORT + 6])
            {
                dateReport.Merge = true;
                dateReport.Value = DateTime.Now.ToLongDateString();
                dateReport.Style.Font.Color.SetColor(Color.Black);
                dateReport.Style.Font.Bold = true;
                dateReport.Style.Font.Size = 9;
                dateReport.Style.Fill.PatternType = ExcelFillStyle.Solid;
                dateReport.Style.Fill.BackgroundColor.SetColor(Color.WhiteSmoke);
                dateReport.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            rowIdx++;

            using (ExcelRange textTitle = ws.Cells[rowIdx, COL_INIT_HEAD_REPORT, rowIdx, COL_INIT_HEAD_REPORT + 6])
            {
                textTitle.Merge = true;
                textTitle.Value = HeaderData.Title;
                textTitle.Style.Font.Color.SetColor(Color.White);
                textTitle.Style.Font.Bold = true;
                textTitle.Style.Font.Size = 16;
                textTitle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                textTitle.Style.Fill.BackgroundColor.SetColor(Color.Black);
                textTitle.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            rowIdx++;

            using (ExcelRange textSubTitle1 = ws.Cells[rowIdx, COL_INIT_HEAD_REPORT, rowIdx, COL_INIT_HEAD_REPORT + 6])
            {
                textSubTitle1.Merge = true;
                textSubTitle1.Value = HeaderData.SubTitleLine1;
                textSubTitle1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            rowIdx++;

            using (ExcelRange textSubTitle2 = ws.Cells[rowIdx, COL_INIT_HEAD_REPORT, rowIdx, COL_INIT_HEAD_REPORT + 6])
            {
                textSubTitle2.Merge = true;
                textSubTitle2.Value = HeaderData.SubTitleLine2;
                textSubTitle2.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            //verifico si se han definido columnas para el reporte y si no se ha hecho se asignan
            //todas las columnas que pose el DataTable.
            if (ColumnsReport == null || ColumnsReport.Count == 0)
            {
                foreach (DataColumn dc in DatTable.Columns)
                {
                    ColumnsReport.Add(new ColumnReportEXCEL()
                    {
                        Name = dc.ColumnName
                    });
                }
            }
            //generar encabezado de la tabla
            rowIdx = ROW_INIT_TABLE_REPORT;
            colIdx = COL_INIT_TABLE_REPORT;
            
            foreach (ColumnReportEXCEL col in ColumnsReport)
            {
                if (DatTable.Columns.Contains(col.Name))
                {
                    ws.Cells[rowIdx, colIdx].Value = col.TextHeader;
                    ws.Cells[rowIdx, colIdx].Style.Font.Color.SetColor(Color.White);
                    ws.Cells[rowIdx, colIdx].Style.Font.Bold = true;
                    ws.Cells[rowIdx, colIdx].Style.Font.Size = 13;
                    ws.Cells[rowIdx, colIdx].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[rowIdx, colIdx].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[rowIdx, colIdx].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    colIdx++;
                }
            }
            rowIdx++;
            colIdx = COL_INIT_HEAD_REPORT;
            // generar los registros de la tabla;
            foreach (DataRow row in DatTable.Rows)
            {
                makeMarkRowGroupe = HaveToMarkGrouping(row);

                foreach (ColumnReportEXCEL col in ColumnsReport)
                {
                    if (DatTable.Columns.Contains(col.Name))
                    {
                        ws.Cells[rowIdx, colIdx].Value = col.IsGrouped && makeMarkRowGroupe ? "--" : row[col.Name];
                        //si el tipo es DateTime y no se indico formato se coloca uno por defecto.
                        ws.Cells[rowIdx, colIdx].Style.Numberformat.Format = 
                            DatTable.Columns[col.Name].DataType == typeof(DateTime) && col.Format == ""? "dd/MM/yyyy hh:mm:ss":col.Format;

                        ws.Cells[rowIdx, colIdx].Style.HorizontalAlignment = (ExcelHorizontalAlignment)col.Alignment;
                        colIdx++;
                    }
                }
                rowIdx++;
                colIdx = COL_INIT_HEAD_REPORT;
            }
            ws.Cells.AutoFitColumns();


            pck.SaveAs(ms);
            return ms;
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
            foreach (ColumnReportEXCEL cr in ColumnsReport.Where(x => x.IsGrouped == true).Select(x => x))
            {
                if (!(compareGroupEqual &= (dr[cr.Name].Equals(cr.LastWrittenValue))))
                    cr.LastWrittenValue = dr[cr.Name];

                makeMark = compareGroupEqual;
            }
            return makeMark;
        }

    }
}