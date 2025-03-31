using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ExcelLib;
using StatusProgressBar;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using FormatColumn;

namespace ExcelReport
{
    /// <summary>
    /// Clase que provee funcionalidad para la creacion de reportes con Excel. 
    /// Utiliza la libreria CExcell que exporta dataset a excell.
    /// </summary>
    public class CExcelReport
    {
        string m_pathDestinationReport;
        string m_nameFileExcell;
        string m_companyDescription;
        string m_titleDescription;
        string m_reportDescription;
        DataTable m_dataSource;
        int    m_startRowTable=3;

        public string NameFileExcell { get => m_nameFileExcell; set => m_nameFileExcell = value; }
        public string CompanyDescription { get => m_companyDescription; set => m_companyDescription = value; }
        public string TitleDescription { get => m_titleDescription; set => m_titleDescription = value; }
        public string ReportDescription { get => m_reportDescription; set => m_reportDescription = value; }
        public DataTable DataSource { get => m_dataSource; set => m_dataSource = value; }
        public string PathDestinationReport { get => m_pathDestinationReport; set => m_pathDestinationReport = value; }
        public TablesColumnsFormatView TablesColumnsFormatView { get; set; }
        public int StartRowTable { get => m_startRowTable; set => m_startRowTable = value; }

        public CExcelReport()
        {
        }

        public CExcelReport(string pathDestinationReport, string nameFileExcell, string companyDescription, string titleDescription, string reportDescription, DataTable dataSource)
        {
            this.m_pathDestinationReport = pathDestinationReport;
            this.m_nameFileExcell = nameFileExcell;
            this.m_companyDescription = companyDescription;
            this.m_titleDescription = titleDescription;
            this.m_reportDescription = reportDescription;
            this.m_dataSource = dataSource;
        }

        public bool Create()
        {
            bool createOK = false;
            string _pathFolderReport = m_pathDestinationReport;
            string _fullPathExcellFile;
            string _nameFileExcell;

            try
            {
                if (!System.IO.Directory.Exists(m_pathDestinationReport))
                {
                    _pathFolderReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    _pathFolderReport = SelectPathDirectory(_pathFolderReport, "Seleccione un Directorio destino para el archivo Excell de reporte");
                }
                _nameFileExcell = String.Format("{0}({1}).xls",m_nameFileExcell, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                _fullPathExcellFile = _pathFolderReport + "\\" + _nameFileExcell;

                CStatusProgressBar.ShowStatusProgressBar("Generando el Reporte...");

                if (System.IO.File.Exists(_fullPathExcellFile))
                {
                    System.IO.File.Delete(_fullPathExcellFile);
                }
                CExcell m_cExell = new CExcell(m_pathDestinationReport, _nameFileExcell);

                if (m_cExell.isOpenWorkBook())
                {
                    m_cExell.MergeCells(1, 1, 1, 16);
                    m_cExell.SetCellValue(1, 1, TitleDescription);
                    m_cExell.SetCellFontColor(1, 1, System.Drawing.Color.White);
                    m_cExell.SetCellBackGroundColor(1, 1, System.Drawing.Color.Black);
                    m_cExell.AlignHCells(1, 1, 1, 1, CExcell.CONST_ALIGN_CELL.xlHCenter);

                    m_cExell.MergeCells(2, 1, 2, 16);
                    m_cExell.SetCellValue(2, 1, ReportDescription);
                    m_cExell.SetCellFontColor(2, 1, System.Drawing.Color.White);
                    m_cExell.SetCellBackGroundColor(2, 1, System.Drawing.Color.Black);
                    m_cExell.AlignHCells(2, 1, 2, 1, CExcell.CONST_ALIGN_CELL.xlHCenter);

                    if(m_dataSource.DataSet != null)
                        m_cExell.ExportDataSetTOExcell(m_dataSource.DataSet, StartRowTable, TablesColumnsFormatView);
                    else
                        m_cExell.ExportDataSetTOExcell(m_dataSource, StartRowTable, TablesColumnsFormatView);


                    m_cExell.Save();
                    m_cExell.MostrarWorkBook();
                }
                createOK = true;
                CStatusProgressBar.CloseForm();
            }
            catch (Win32Exception er)
            {
                if (er.NativeErrorCode == (int)WIN32EXCEPTION.ERROR_FILE_NOT_FOUND)
                {
                    MessageBox.Show("Archivo no Encontrado ." + er.Message, "Excepcion Acceso Archivos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (er.NativeErrorCode == (int)WIN32EXCEPTION.ERROR_ACCESS_DENIED)
                {
                    MessageBox.Show("El Archivo que intenta abrir esta en uso." + er.Message, "Excepcion Acceso Archivos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(er.Message, "Excepcion Acceso Archivos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException ioEr)
            {
                MessageBox.Show(ioEr.Message + "Error, El Archivo temporal Excell esta abierto , Cierre el Archivo.", "Error en Apertura de Archivo Excell", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                CStatusProgressBar.CloseForm();
            }
            return createOK;
        }

        private string SelectPathDirectory(string initialDirectory, string tituloDetalle)
        {
            string selectPath = initialDirectory;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = selectPath;
            fbd.Description = tituloDetalle;
            if (fbd.ShowDialog() == DialogResult.OK)
                selectPath = fbd.SelectedPath;
            return selectPath;
        }

    }
    public enum WIN32EXCEPTION
    {
        ERROR_SUCCESS = 0,
        ERROR_INVALID_FUNCTION,
        ERROR_FILE_NOT_FOUND,
        ERROR_PATH_NOT_FOUND,
        ERROR_ACCESS_DENIED = 5,
    }

}
