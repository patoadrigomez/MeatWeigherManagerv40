using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using TypeAlignCell = Microsoft.Office.Interop.Excel.XlHAlign;
using FormatColumn;


/***************************************************************************
 * CExcelLib v2.0 11-5-2020
 * Libreria que permite manejar un archivo Excell utilizando la tecnologia
 * COM Interop usando la libreria Microsoft.Office.Interop.Excel.
 * La Clase CExcell permite Crear un nuevo archivo excell, crear hojas, editar
 * celdas , cambiar los colores de fondo y texto, cambiar tamaño de font etc.
 * 
 * Ejemplo de uso:
 *          cExell = new CExcell("2.xls");
 *          if (cExell.isOpenWorkBook())
 *           {
 *              cExell.SetActiveWorkSheet("REPORTE1");
 *              cExell.SetCellValue(1, 1, "pepenuevoooooooooooooooooooooooooooooo");
 *              cExell.SetActiveWorkSheet("REPORTE2");
 *              cExell.SetCellValue(2, 2, "pepenuevo2");
 *              cExell.SetCellFontColor(1, 1, System.Drawing.Color.Red);
 *              cExell.SetCellBackGroundColor(1, 1, System.Drawing.Color.Green);
 *              cExell.MergeCells(3, 1, 3, 10);
 *              cExell.SetCellValue(3,  1, "MI TEXTO CELDAS AGRUPADAS");
 *              cExell.SetCellFontColor(3, 1, System.Drawing.Color.Red);
 *              cExell.AlignHCells(3, 1, 3, 1, CExcell.CONST_ALIGN_CELL.xlHRight);
 *              cExell.SetFontBoldCells(3, 1, 3, 1);
 *              cExell.SetFontSizeCells(3, 1, 3, 1,25);
 *              cExell.SetActiveWorkSheet("REPORTE1");
 *              cExell.SetCellValue(1, 2, "pepenuevoooooooooooooooooooooooooooooo");
 *              cExell.SetAutoFitCells(1, 1, 1, 2);
 *              cExell.Save();
 *              cExell.Close();
 *          }
 * 
  * ************************************************************************/

namespace ExcelLib
{
    public class CExcell
    {
        private Excel.Application excellApp;
        private Excel.Workbook workbook;
        private Excel.Worksheet worksheetActiva;
        private bool isOpenWorkbook = false;

        public enum CONST_ALIGN_CELL
        {
            xlHCenter = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter,
            xlHLeft = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft,
            xlHRight = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight,
        };

        public string strExceptionExcellClass;

        /**********************************************************************
         * CEscell()
         * Descripcion: Constructor de la clase CEscell. Abre un archivo Excell
         *              si es que existe y si no crea uno nuevo.
         * Parametro:   Nombre de Archivo a Abrir o Crear. 
         * 
        ***********************************************************************/
        public CExcell(string pathFile,string nameExcellFile)
        {
            string pathAndNameFileExcell = pathFile + "\\" + nameExcellFile;

            strExceptionExcellClass = "";
            try
            {
                excellApp = new Excel.Application();

                if (System.IO.File.Exists(pathAndNameFileExcell))
                {
                    workbook = excellApp.Workbooks.Open(pathAndNameFileExcell);
                }
                else
                {
                    workbook = excellApp.Workbooks.Add();
                    workbook.SaveAs(pathAndNameFileExcell);
                }
                isOpenWorkbook = true;

                worksheetActiva = getSheet(1);
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
                isOpenWorkbook = false;
            }
        }

        /**********************************************************************
         * isOpenWorkBook()
         * Descripcion: Retorna con tru si el workbook esta abierto 
         * Retorna:     true WorkBook Abierto
        ***********************************************************************/
        public bool isOpenWorkBook()
        {
            return isOpenWorkbook;
        }

        /**********************************************************************
         * SetActiveWorkSheet(string name)
         * Descripcion: Establece como activa la worksheet que tenga el nombre
         *              indicado. Si no la encuentra la crea y la pone como activa. 
         * Parametro:   String nombre de hoja a poner activa. 
         * Retorna:     
        ***********************************************************************/
        public void SetActiveWorkSheet(string nameSheet)
        {
            worksheetActiva = getSheet(nameSheet);
        }

        /**********************************************************************
         * SetActiveWorkSheet(int idxSheet)
         * Descripcion: Establece como activa la worksheet que tenga el indice
         *              indicado (base 1). Si no la encuentra la crea y la pone
         *              como activa. 
         * Parametro:   int valor del indice a la hoja. 
         * Retorna:     
        ***********************************************************************/
        public void SetActiveWorkSheet(int idxsheet)
        {
            worksheetActiva = getSheet(idxsheet);
        }

        /**********************************************************************
         * getSheet(int idxSheet)
         * Descripcion: Obtiene una referencia a un objeto Worksheet a partir 
         *              de indicar su numero de indice en el libro. Si no existe 
         *              hoja con ese indice se crea una nueva con nombre Hoja+(indice).
         *              Los indices de hojas van desde 1.
         * Parametro:   Numero de indice de hoja en el libro actual. 
         * Retorna:     Objeto Worksheet (null si ubo excepcion)
        ***********************************************************************/
        public Excel.Worksheet getSheet(int idxSheet)
        {
            Excel.Worksheet ws = null;
            strExceptionExcellClass = "";
            try
            {
                if (isOpenWorkbook)
                {
                    if (workbook.Worksheets.Count >= (idxSheet))
                    {
                        ws = (Excel.Worksheet)workbook.Worksheets.Item[idxSheet];
                    }
                    else
                    {
                        string nameSheet = "Hoja" + idxSheet;
                        ws = createSheet(nameSheet);
                    }
                }
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return ws;
        }

        /**********************************************************************
         * getSheet(string nameSheet)
         * Descripcion: Obtiene una referencia a un objeto Worksheet en el libro
         *              actual a partir de indicar su nombre. Si no existe una
         *              hoja con ese nombre crea una nueva.
         * Parametro:   Nombre de la Hoja a Buscar o Crear. 
         * Retorna:     Objeto Worksheet (null si ubo excepcion)
        ***********************************************************************/
        public Excel.Worksheet getSheet(string nameSheet)
        {
            Excel.Worksheet ws = null;
            int countSheets;
            strExceptionExcellClass = "";
            try
            {
                if (isOpenWorkbook)
                {
                    countSheets = workbook.Worksheets.Count;
                    for (int i = 1; i < countSheets; i++)
                    {
                        if (((Excel.Worksheet)workbook.Worksheets.get_Item(i)).Name == nameSheet)
                        {
                            ws = (Excel.Worksheet)workbook.Worksheets.get_Item(i);
                        }
                    }
                    if (ws == null)
                    {
                        ws = createSheet(nameSheet);
                    }
                }
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return ws;
        }

        /**********************************************************************
         * createSheet(string nameSheet)
         * Descripcion: Crea una nueva hoja en el WorkBook actual
         * Parametro:   Nombre de la Hoja a Crear. 
         * Retorna:     Objeto Worksheet (null si ubo excepcion)
        ***********************************************************************/
        public Excel.Worksheet createSheet(string nameSheet)
        {
            Excel.Worksheet ws = null;
            strExceptionExcellClass = "";
            try
            {
                ws = (Excel.Worksheet)workbook.Worksheets.Add();
                ws.Name = nameSheet;
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return ws;
        }

        /**********************************************************************
         * SetCellFontColor
         * Descripcion: Establece el color del font de una celda
        ***********************************************************************/
        public void SetCellFontColor(int idxRow, int idxCol, System.Drawing.Color color)
        {
            Excel.Range rng = (Excel.Range)worksheetActiva.Cells[idxRow, idxCol];
            rng.Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        /**********************************************************************
         * SetCellFontColor
         * Descripcion: Establece el color del font de un rango de celdas
        ***********************************************************************/
        public void SetCellFontColor(int idxRow1, int idxCol1, int idxRow2, int idxCol2, System.Drawing.Color color)
        {
            worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1],
                                  worksheetActiva.Cells[idxRow2, idxCol2]].Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        /**********************************************************************
         * SetCellBackGroundColor
         * Descripcion: Establece el color de fondo de una celdas
        ***********************************************************************/
        public void SetCellBackGroundColor(int idxRow, int idxCol, System.Drawing.Color color)
        {
            Excel.Range rng = (Excel.Range)worksheetActiva.Cells[idxRow, idxCol];
            rng.Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        /**********************************************************************
         * SetCellBackGroundColor
         * Descripcion: Establece el color de fondo de un rango de celdas
        ***********************************************************************/
        public void SetCellBackGroundColor(int idxRow1, int idxCol1, int idxRow2, int idxCol2, System.Drawing.Color color)
        {
            worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1],
                                  worksheetActiva.Cells[idxRow2, idxCol2]].Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        /**********************************************************************
         * Save()
         * Descripcion: Salva el WorkBook con todas sus hojas vinculadas.
         * Parametro:   
         * Retorna:     true (si la accion fue realizada)
        ***********************************************************************/
        public bool Save()
        {
            bool saveOk = false;
            strExceptionExcellClass = "";
            try
            {
                workbook.Save();
                saveOk = true;
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return saveOk;
        }

        /**********************************************************************
         * MostrarWorkBook()
         * Descripcion: Hace Visible al usuario el archivo excell y le da el control
        ***********************************************************************/
        public void MostrarWorkBook()
        {
            strExceptionExcellClass = "";
            try
            {
                excellApp.Visible = true;
                excellApp.UserControl = true;
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
        }
        /**********************************************************************
         * Close()
         * Descripcion: Cierra el WorkBook Activo.
         * Parametro:   
         * Retorna:     
        ***********************************************************************/
        public void Close()
        {
            workbook.Close();
            excellApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excellApp);
            GC.Collect();
        }

        /**********************************************************************
         * SetCellValue()
         * Descripcion: Coloca un valor string una celda especificada por fila columna.
         * Parametro:   int idxRow (numero de fila base 1)
         * Parametro:   int idxCol (numero de Columna base 1)
         * Parametro:   string strValue (valor string a escribir)
         * Retorna:     
        ***********************************************************************/
        public void SetCellValue(int idxRow, int idxCol, string strValue)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Cells[idxRow, idxCol] = strValue;
            }
        }

        /**********************************************************************
         * SetCellValue()
         * Descripcion: Coloca un valor entero en una celda especificada por fila columna.
         * Parametro:   int idxRow (numero de fila base 1)
         * Parametro:   int idxCol (numero de Columna base 1)
         * Parametro:   uint (valor a escribir)
         * Retorna:     
        ***********************************************************************/
        public void SetCellValue(int idxRow, int idxCol, uint valor)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Cells[idxRow, idxCol] = (object)valor;
            }
        }

        /**********************************************************************
         * SetCellValue()
         * Descripcion: Coloca un float en una celda especificada por fila columna.
         * Parametro:   int idxRow (numero de fila base 1)
         * Parametro:   int idxCol (numero de Columna base 1)
         * Parametro:   float (valor a escribir)
         * Retorna:     
        ***********************************************************************/
        public void SetCellValue(int idxRow, int idxCol, float valor)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Cells[idxRow, idxCol] = (object)valor;
            }
        }
        /**********************************************************************
         * MergeCells()
         * Descripcion: Agrupar Celdas.
         * Parametro:   int idxRow1 , int idxCol1 (numero de fila Columna inicial)
         * Parametro:   int idxRow2 , int idxCol2 (numero de fila Columna final)
         * Retorna:     
        ***********************************************************************/
        public void MergeCells(int idxRow1, int idxCol1, int idxRow2, int idxCol2)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1], worksheetActiva.Cells[idxRow2, idxCol2]].Merge();
            }
        }

        /**********************************************************************
         * AlignHCells()
         * Descripcion: Alineacion horizontal de Celda o Celdas.
         * Parametro:   int idxRow1 , int idxCol1 (numero de fila Columna inicial)
         * Parametro:   int idxRow2 , int idxCol2 (numero de fila Columna final)
         * Parametro:   TypeAlignCell , tipo de alineacion a realizar 
         * Retorna:     
        ***********************************************************************/
        public void AlignHCells(int idxRow1, int idxCol1, int idxRow2, int idxCol2, CONST_ALIGN_CELL typeAlign)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1], worksheetActiva.Cells[idxRow2, idxCol2]].Cells.HorizontalAlignment = typeAlign;
            }
        }
        /**********************************************************************
         * AlignHCol()
         * Descripcion: Alineacion horizontal Columna.
         * Parametro:   int idxRow1 , int idxCol1 (numero de fila Columna inicial)
         * Parametro:   int idxRow2 , int idxCol2 (numero de fila Columna final)
         * Parametro:   TypeAlignCell , tipo de alineacion a realizar 
         * Retorna:     
        ***********************************************************************/
        public void AlignHCol(int idxRow, int idxCol, CONST_ALIGN_CELL typeAlign)
        {
            if (worksheetActiva != null)
            {
                ((Excel.Range)worksheetActiva.Cells[idxRow, idxCol]).EntireColumn.HorizontalAlignment = typeAlign;
            }
        }

        /**********************************************************************
         * SetFontBoldCells()
         * Descripcion: Coloca en fuente de una celda o un rango de celdas en negrita.
         * Parametro:   int idxRow1 , int idxCol1 (numero de fila Columna inicial)
         * Parametro:   int idxRow2 , int idxCol2 (numero de fila Columna final)
         * Parametro:   Bool si es igual a true coloca en negrita y si es false
         *              coloca el font en normal 
         * Retorna:     
        ***********************************************************************/
        public void SetFontBoldCells(int idxRow1, int idxCol1, int idxRow2, int idxCol2, bool setBold = true)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1], worksheetActiva.Cells[idxRow2, idxCol2]].Cells.Font.Bold = setBold;
            }
        }
        /**********************************************************************
         * SetFontSizeCells()
         * Descripcion: Coloca en tamaño de fuente a la fuente actual para una
         *              celda o un rango de celdas en negrita.
         * Parametro:   int idxRow1 , int idxCol1 (numero de fila Columna inicial)
         * Parametro:   int idxRow2 , int idxCol2 (numero de fila Columna final)
         * Parametro:   int tamaño de font de la celda 
         * Retorna:     
        ***********************************************************************/
        public void SetFontSizeCells(int idxRow1, int idxCol1, int idxRow2, int idxCol2, int sizeFont)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1], worksheetActiva.Cells[idxRow2, idxCol2]].Cells.Font.Size = sizeFont;
            }
        }

        public void SetNumFormatEntireColum(int idxRow, int idxCol, string numFormat)
        {
            if (worksheetActiva != null)
            {
                ((Excel.Range)worksheetActiva.Cells[idxRow, idxCol]).EntireColumn.NumberFormat = numFormat;
            }
        }

        /**********************************************************************
         * SetAutoFitCells()
         * Descripcion: Ajusta el ancho de columna automaticamente en funcion de
         *              su contenido para una celda o un rango de celdas.
         * Parametro:   int idxRow1 , int idxCol1 (numero de fila Columna inicial)
         * Parametro:   int idxRow2 , int idxCol2 (numero de fila Columna final)
         * Retorna:     
        ***********************************************************************/
        public void SetAutoFitCells(int idxRow1, int idxCol1, int idxRow2, int idxCol2)
        {
            if (worksheetActiva != null)
            {
                worksheetActiva.Range[worksheetActiva.Cells[idxRow1, idxCol1], worksheetActiva.Cells[idxRow2, idxCol2]].Cells.EntireColumn.AutoFit();
            }
        }
        /**********************************************************************
         * GetCellValue()
         * Descripcion: Obtiene el valor de una celda especificada por fila columna.
         * Parametro:   int idxRow (numero de fila base 1)
         * Parametro:   int idxCol (numero de Columna base 1)
         * Retorna:     string (valor string de la celda)
        ***********************************************************************/
        public string GetCellValue(int idxRow, int idxCol)
        {
            string strVal = null;
            if (worksheetActiva != null)
            {
                strVal = ((Excel.Range)worksheetActiva.Cells[idxRow, idxCol]).Value2.ToString();
            }
            return strVal;
        }

        /**********************************************************************
         * ExportDataSetTOExcell()
         * Descripcion: Exporta un DataSet a un archivo excell. Por cada tabla en
         *              el dataset se imprime en excel una debajo de la otra.
         * Parametro:   DataSet ds
         * Parametro:   int startRowTabla (indica desde que fila en la hoja excel se
         *              comenzara a imprimir las tablas).
         * Parametro:   TablesColumnsFormatView cfv 
         *              Es un array de definiciones de como se debe visualizar ciertas
         *              columnas en cada tabla. Las tablas en el dataset se relacionan 
         *              con este array con el indice .
         *              
         * Retorna:     string (valor string de la celda)
        ***********************************************************************/
        public bool ExportDataSetTOExcell(DataSet ds,int startRowTable=3, TablesColumnsFormatView cfv = null)
        {
            bool exportedOk = false;
            bool primerTabla = true;
            FormatViewType formatView;
            int idxTable = 0;
            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    //Si hay mas de una tabla se van colocando una de bajo de otra

                    if (!primerTabla)
                    {
                        startRowTable += 2; //dejo espacio de dos filas entre tablas.
                        idxTable++;
                    }
                    primerTabla = false;

                    //establece color y tamañio de fuente para el encabezado del reporte
                    SetCellFontColor(startRowTable, 1, startRowTable, table.Columns.Count, System.Drawing.Color.Red);
                    SetFontBoldCells(startRowTable, 1, startRowTable, table.Columns.Count);
                    SetFontSizeCells(startRowTable, 1, startRowTable, table.Columns.Count, 10);

                    for (int i = 1; i < table.Columns.Count + 1; i++)
                    {
                        worksheetActiva.Cells[startRowTable, i] = table.Columns[i - 1].ColumnName;
                    }
                    startRowTable++;

                    //establece color y tamañio de fuente para los registros del reporte
                    SetCellFontColor(startRowTable, 1, table.Rows.Count + startRowTable - 1, table.Columns.Count, System.Drawing.Color.Black);
                    SetFontSizeCells(startRowTable, 1, table.Rows.Count + startRowTable - 1, table.Columns.Count, 8);


                    for (int j = 0; j < table.Rows.Count; j++, startRowTable++)
                    {
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            if (table.Rows[j].ItemArray[k] != DBNull.Value)
                            {
                                if (cfv != null && cfv.Count >= idxTable + 1)
                                {
                                    formatView = cfv[idxTable].Where(x => x.Name.ToUpper() == table.Columns[k].ColumnName.ToUpper()).Select(x => x.FormatView).FirstOrDefault();
                                    if (formatView == FormatViewType.DATETIME_ONLYDATE)
                                    {
                                        /* Para asignar a una celda un valor de fecha y que sea bien interpretado por excel debemos
                                         configurar la celda con el correcto formato de fecha y el valor a asignar a esta debe ser un valor flotante 
                                         representativo de cantidad de dias y su fraccion las horas*/
                                        worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "dd-mm-yyyy";
                                        worksheetActiva.Cells[startRowTable, k + 1] = ((DateTime)table.Rows[j].ItemArray[k]).ToOADate();
                                    }
                                    else if (formatView == FormatViewType.TIMESPAN_WHITOUT_SECONDS)
                                    {
                                        worksheetActiva.Cells[startRowTable, k + 1] = ((TimeSpan)table.Rows[j].ItemArray[k]).ToString(@"hh\:mm");
                                    }
                                    else if (formatView == FormatViewType.FLOAT)
                                    {
                                        worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "0,#";
                                        worksheetActiva.Cells[startRowTable, k + 1] = table.Rows[j].ItemArray[k];
                                    }
                                    else if (formatView == FormatViewType.FLOAT_ROUND2)
                                    {
                                        worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "0,00";
                                        worksheetActiva.Cells[startRowTable, k + 1] = table.Rows[j].ItemArray[k];
                                    }
                                    else if (formatView == FormatViewType.INTEGER)
                                    {
                                        worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "0";
                                        worksheetActiva.Cells[startRowTable, k + 1] = table.Rows[j].ItemArray[k];
                                    }
                                    else
                                        worksheetActiva.Cells[startRowTable, k + 1] = table.Rows[j].ItemArray[k].ToString();
                                }
                                else
                                    worksheetActiva.Cells[startRowTable, k + 1] = table.Rows[j].ItemArray[k].ToString();
                            }
                        }
                    }
                    SetAutoFitCells(startRowTable, 1, startRowTable, table.Columns.Count);
                }
                exportedOk = true;
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return exportedOk;
        }

        /**********************************************************************
         * ExportDataSetTOExcell()
         * Descripcion: Exporta un DataSet a un archivo excell. Por cada tabla en
         *              el dataset se imprime en excel una debajo de la otra.
         * Parametro:   DataSet ds
         * Parametro:   int startRowTabla (indica desde que fila en la hoja excel se
         *              comenzara a imprimir las tablas).
         * Parametro:   TablesColumnsFormatView cfv 
         *              Es un array de definiciones de como se debe visualizar ciertas
         *              columnas en cada tabla. Las tablas en el dataset se relacionan 
         *              con este array con el indice .
         *              
         * Retorna:     string (valor string de la celda)
        ***********************************************************************/
        public bool ExportDataSetTOExcell(DataTable dt, int startRowTable = 3, TablesColumnsFormatView cfv = null)
        {
            bool exportedOk = false;
            FormatViewType formatView;
            int idxTable = 0;
            try
            {

                //establece color y tamañio de fuente para el encabezado del reporte
                SetCellFontColor(startRowTable, 1, startRowTable, dt.Columns.Count, System.Drawing.Color.Red);
                SetFontBoldCells(startRowTable, 1, startRowTable, dt.Columns.Count);
                SetFontSizeCells(startRowTable, 1, startRowTable, dt.Columns.Count, 10);

                for (int i = 1; i < dt.Columns.Count + 1; i++)
                {
                    worksheetActiva.Cells[startRowTable, i] = dt.Columns[i - 1].ColumnName;
                }
                startRowTable++;

                //establece color y tamañio de fuente para los registros del reporte
                SetCellFontColor(startRowTable, 1, dt.Rows.Count + startRowTable - 1, dt.Columns.Count, System.Drawing.Color.Black);
                SetFontSizeCells(startRowTable, 1, dt.Rows.Count + startRowTable - 1, dt.Columns.Count, 8);


                for (int j = 0; j < dt.Rows.Count; j++, startRowTable++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (dt.Rows[j].ItemArray[k] != DBNull.Value)
                        {
                            if (cfv != null && cfv.Count >= idxTable + 1)
                            {
                                formatView = cfv[idxTable].Where(x => x.Name.ToUpper() == dt.Columns[k].ColumnName.ToUpper()).Select(x => x.FormatView).FirstOrDefault();
                                if (formatView == FormatViewType.DATETIME_ONLYDATE)
                                {
                                    /* Para asignar a una celda un valor de fecha y que sea bien interpretado por excel debemos
                                        configurar la celda con el correcto formato de fecha y el valor a asignar a esta debe ser un valor flotante 
                                        representativo de cantidad de dias y su fraccion las horas*/
                                    worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "dd-mm-yyyy";
                                    worksheetActiva.Cells[startRowTable, k + 1] = ((DateTime)dt.Rows[j].ItemArray[k]).ToOADate();
                                }
                                else if (formatView == FormatViewType.TIMESPAN_WHITOUT_SECONDS)
                                {
                                    worksheetActiva.Cells[startRowTable, k + 1] = ((TimeSpan)dt.Rows[j].ItemArray[k]).ToString(@"hh\:mm");
                                }
                                else if (formatView == FormatViewType.FLOAT)
                                {
                                    worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "0,#";
                                    worksheetActiva.Cells[startRowTable, k + 1] = dt.Rows[j].ItemArray[k];
                                }
                                else if (formatView == FormatViewType.FLOAT_ROUND2)
                                {
                                    worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "0,00";
                                    worksheetActiva.Cells[startRowTable, k + 1] = dt.Rows[j].ItemArray[k];
                                }
                                else if (formatView == FormatViewType.INTEGER)
                                {
                                    worksheetActiva.Cells[startRowTable, k + 1].NumberFormat = "0";
                                    worksheetActiva.Cells[startRowTable, k + 1] = dt.Rows[j].ItemArray[k];
                                }
                                else
                                    worksheetActiva.Cells[startRowTable, k + 1] = dt.Rows[j].ItemArray[k].ToString();
                            }
                            else
                                worksheetActiva.Cells[startRowTable, k + 1] = dt.Rows[j].ItemArray[k].ToString();
                        }
                    }
                }
                SetAutoFitCells(startRowTable, 1, startRowTable, dt.Columns.Count);
                exportedOk = true;
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return exportedOk;
        }

        public float GetCellFLOAT(int idxRow, int idxCol)
        {
            float value = 0.0f;
            object obj;
            string str;
            try
            {
                obj = worksheetActiva.Cells[idxRow, idxCol];
                if (isNumeric(obj))
                {
                    str = obj.ToString();
                    value = Convert.ToSingle(str);
                }
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return value;
        }
        // Lee un estring y separa cada caracter del string en un byte en un array
        // de bytes
        public byte[] GetCellStringToBytes(int idxRow, int idxCol)
        {
            byte[] bts = null;
            object obj;
            string str;
            try
            {
                obj = worksheetActiva.Cells[idxRow, idxCol];
                if (obj != null)
                {
                    str = obj.ToString();
                    bts = GetBytes(str);
                }
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return bts;
        }

        public string getException()
        {
            return strExceptionExcellClass;
        }

        public bool isNumeric(Object Expression)
        {
            strExceptionExcellClass = "";

            if (Expression == null || Expression is DateTime)
                return false;
            if (Expression is Int16 || Expression is Int32 || Expression is Int64 ||
                Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;
            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch (Exception ex)
            {
                strExceptionExcellClass = ex.Message;
            }
            return false;
        }

        public byte[] GetBytes(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(str);
        }
    }
}
