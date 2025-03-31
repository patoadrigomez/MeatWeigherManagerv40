using System;
using System.IO;
using StatusProgressBar;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using Stampa;

namespace PdfStampa
{
    public class CPdfStampa
    {
        string m_pathDestinationReport;
        string m_nameFileFormatXML;
        string m_nameFilePDF;
        string m_companyDescription;
        string m_titleDescription;
        string m_reportDescriptionLine1;
        string m_reportDescriptionLine2;
        string m_fullPathLogoCompany;

        public string NameFileFormatXML { get => m_nameFileFormatXML; set => m_nameFileFormatXML = value; }
        public string NameFilePDF { get => m_nameFilePDF; set => m_nameFilePDF = value; }
        public string CompanyDescription { get => m_companyDescription; set => m_companyDescription = value; }
        public string TitleDescription { get => m_titleDescription; set => m_titleDescription = value; }
        public string ReportDescriptionLine1 { get => m_reportDescriptionLine1; set => m_reportDescriptionLine1 = value; }
        public string FullPathLogoCompany { get => m_fullPathLogoCompany; set => m_fullPathLogoCompany = value; }
        public string PathDestinationReport { get => m_pathDestinationReport; set => m_pathDestinationReport = value; }
        public string ReportDescriptionLine2 { get => m_reportDescriptionLine2; set => m_reportDescriptionLine2 = value; }

        public CPdfStampa()
        {

        }

        public CPdfStampa(string pathDestinationReport, string nameFileFormatXML, string nameFilePDF, string companyDescription, string titleDescription, string reportDescriptionLine1, string reportDescriptionLine2, string fullPathLogoCompany)
        {
            this.m_pathDestinationReport = pathDestinationReport;
            this.m_nameFileFormatXML = nameFileFormatXML;
            this.m_nameFilePDF = nameFilePDF;
            this.m_companyDescription = companyDescription;
            this.m_titleDescription = titleDescription;
            this.m_reportDescriptionLine1 = reportDescriptionLine1;
            this.m_reportDescriptionLine2 = reportDescriptionLine2;
            this.m_fullPathLogoCompany = fullPathLogoCompany;
        }

        public bool Create()
        {
            bool createOK = false;
            string _pathFolderReport = m_pathDestinationReport;
            string _fullPathPdfFile;
            string _fullPathFileFormatXML;

            Stampa.ReportDocument report = new Stampa.ReportDocument();
            try
            {
                if (!System.IO.Directory.Exists(m_pathDestinationReport))
                {
                    _pathFolderReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    _pathFolderReport = SelectPathDirectory(_pathFolderReport, "Seleccione un Directorio destino para el archivo PDF de reporte");
                }
                _fullPathPdfFile = String.Format("{0}({1}).pdf", _pathFolderReport + "\\" + m_nameFilePDF, DateTime.Now.ToString("ddMMyyyyHHmmss"));

                CStatusProgressBar.ShowStatusProgressBar("Generando el Reporte...");

                _fullPathFileFormatXML = Environment.CurrentDirectory + "\\" + m_nameFileFormatXML + ".xml";
                if (System.IO.File.Exists(_fullPathFileFormatXML))
                {
                    report.setXML(_fullPathFileFormatXML);
                    if (m_fullPathLogoCompany != "")
                    {
                        if (System.IO.File.Exists(m_fullPathLogoCompany))
                        {
                            Bitmap logo = new Bitmap(GetImageFromFile(m_fullPathLogoCompany));

                            if (logo != null)
                            {
                                report.AddPicture("LogoEmpresa", logo, true);
                            }
                        }
                    }
                    report.AddText("CampoTexto_Empresa", m_companyDescription);
                    report.AddText("CampoTexto_Titulo", m_titleDescription);
                    report.AddText("CampoTexto_InfoLine1", m_reportDescriptionLine1);
                    report.AddText("CampoTexto_InfoLine2", m_reportDescriptionLine2);
                    SetAdditionalData(report);
                    report.SerializeToPdfFile(_fullPathPdfFile);

                    Process proc = new Process();
                    proc.StartInfo.FileName = _fullPathPdfFile;
                    proc.StartInfo.Verb = "Open";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    createOK = true;
                    CStatusProgressBar.CloseForm();
                }
                else
                {
                    MessageBox.Show("Error, No se encuentra el archivo XML de Formato de Reporte PDF.", "Error Archivo XML de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                MessageBox.Show(ioEr.Message + "Error, El Archivo temporal PDF esta abierto , Cierre el Archivo.", "Error en Apertura de Archivo PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                CStatusProgressBar.CloseForm();
            }
            return createOK;
        }

        public virtual void SetAdditionalData(ReportDocument report)
        {
        }

        /**********************************************************************************
        * GetImageFromFile
        * Obtiene la Imagen desde un archivo JPG
        * ********************************************************************************/
        private Image GetImageFromFile(string pathAndNameFileImage)
        {
            Image img = null;
            FileStream myStream = null;
            try
            {
                string pathFile = pathAndNameFileImage;
                if (File.Exists(pathFile))
                {
                    myStream = new FileStream(pathFile, FileMode.Open);
                    img = Image.FromStream(myStream);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (myStream != null)
                {
                    myStream.Close();
                    myStream.Dispose();
                }
            }
            return img;
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
