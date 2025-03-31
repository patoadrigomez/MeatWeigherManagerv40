using REPORTEXCEL;
using REPORTPDF;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web;
using WebReportMWM.Models.Entitys;

namespace WebReportMWM.services
{
    public static class ReportServices
    {
        /// <summary>
        /// Genera un reporte PDF con los movimientos de ingreso a planta de productos.
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="idProveedor"></param>
        /// <param name="idProducto"></param>
        /// <param name="idTipoProducto"></param>
        /// <returns></returns>

        public static MemoryStream GenerarPDFReport_IngresoPlantaDetallado(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto, int PesadaManual, string TipoPesada, int NumTropa)
        {
            MemoryStream ms = null;
            //IDPIEZA, IDOI, PROVEEDOR, SANITARIO, CODIGO_PROD, PRODUCTO, TIPO_PRD, PESADA, DESTINO,
            //UNDS, NETO, TARA, REMITIDO, OPERADOR
            DataTable dt = DbServices.GetConsultaReporte_IngresoAPlantaDetalle(DateFrom, DateTo, PesadaManual, idProveedor, idProducto, idTipoProducto, NumTropa);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "IDPIEZA",
                    TextHeader = "PZA",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "IDOI",
                    TextHeader = "OI",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PROVEEDOR",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SANITARIO",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CODIGO_PRD",
                    TextHeader = "CÓDIGO PRODUCTO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER

                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER

                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESADA",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DESTINO",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "REMITIDO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "VENCIMIENTO",
                    TextHeader="VCTO",
                    WidthPercent = 5.0f,
                    Format = "dd/MM/yy",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "MANUAL",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OPERADOR",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte Ingresos de Productos a Planta",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Proveedor: {0} Producto: {1} TipoPrd : {2} Tropa : {3} Pesada : {4}",
                                                Proveedor, Producto, TipoProducto, NumTropa, TipoPesada),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport,
                    HeaderTable = new HeaderTable() { FontSize=8}
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        /// <summary>
        /// Genera un reporte EXCEL con los movimientos de ingreso a planta de productos.
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="idProveedor"></param>
        /// <param name="idProducto"></param>
        /// <param name="idTipoProducto"></param>
        /// <returns></returns>

        public static MemoryStream GenerarExcelReport_IngresoPlantaDetallado(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto, int PesadaManual, string TipoPesada, int NumTropa)
        {
            MemoryStream ms = null;
            //IDPIEZA, IDOI, PROVEEDOR, SANITARIO, CODIGO_PROD, PRODUCTO, TIPO_PRD, PESADA, DESTINO,
            //UNDS, NETO, TARA, REMITIDO, OPERADOR
            DataTable dt = DbServices.GetConsultaReporte_IngresoAPlantaDetalle(DateFrom, DateTo, PesadaManual, idProveedor, idProducto, idTipoProducto, NumTropa);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "IDPIEZA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "IDOI",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PROVEEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SANITARIO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CODIGO_PRD",
                    TextHeader = "CODIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESADA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DESTINO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                    Format = "0.00 \"Kg\""
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "REMITIDO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "VENCIMIENTO",
                    Format = "dd/mm/yyyy",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "MANUAL",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OPERADOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte Ingresos de Productos a  Planta",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Proveedor: {0} Producto: {1} TipoPrd : {2} Tropa : {3} Pesada: {4} ",
                                                Proveedor, Producto, TipoProducto, NumTropa, TipoPesada),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }


        public static MemoryStream GenerarPDFReport_IngresoPlantaTotalizadoxOIProducto(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto)
        {
            MemoryStream ms = null;
            //IDOI, PROVEEDOR, SANITARIO, CODIGO_PROD, PRODUCTO,UNDS, NETO, TARA, REMITIDO
            DataTable dt = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxOIProducto(DateFrom, DateTo, idProveedor, idProducto, idTipoProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "IDOI",
                    TextHeader = "OI",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PROVEEDOR",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SANITARIO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "COD. PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    WidthPercent = 3.0f,
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "REMITIDO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Ingresos Totalizados a Planta ordenado por OI y Producto",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Proveedor: {0} Producto: {1} TipoProducto: {2}",
                                                Proveedor, Producto, TipoProducto),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_IngresoPlantaTotalizadoxOIProducto(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto)
        {
            MemoryStream ms = null;
            //IDOI, PROVEEDOR, SANITARIO, CODIGO_PROD, PRODUCTO,UNDS, NETO, TARA, REMITIDO
            DataTable dt = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxOIProducto(DateFrom, DateTo, idProveedor, idProducto, idTipoProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "IDOI",
                    TextHeader = "OI",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PROVEEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SANITARIO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CODIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "REMITIDO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte Ingresos de Totalizados a  Planta ordenado por OI y Producto",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Proveedor: {0} Producto: {1} TipoProducto: {2}",
                                                Proveedor, Producto, TipoProducto),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }


        public static MemoryStream GenerarPDFReport_IngresoPlantaTotalizadoxDiaProveedor(DateTime DateFrom, DateTime DateTo, string idProveedor, string Proveedor)
        {
            MemoryStream ms = null;
            //DIA, UNDS, NETO, TARA, REMITIDO
            DataTable dt = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxDiaProveedor(DateFrom, DateTo, idProveedor);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DIA",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PROVEEDOR",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "REMITIDO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte Ingresos de Totalizados a Planta ordenado por Día y Proveedor",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Proveedor: {0}",
                                                Proveedor),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcel_IngresoPlantaTotalizadoxDiaProveedor(DateTime DateFrom, DateTime DateTo, string idProveedor, string Proveedor)
        {
            MemoryStream ms = null;
            //DIA, UNDS, NETO, TARA, REMITIDO
            DataTable dt = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxDiaProveedor(DateFrom, DateTo, idProveedor);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DIA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PROVEEDOR",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "REMITIDO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte Ingresos de Totalizados a  Planta ordenado por Día y Proveedor",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Proveedor{0}", Proveedor)

                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_IngresoProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto, int NumTropa)
        {
            MemoryStream ms = null;
            //IDPIEZA, IDOI, COD_PRD, PRODUCTO, TIPO_PRD, INGRESO, SECTOR, UNDS, NETO, TARA, PUESTO, OPERADOR
            DataTable dt = DbServices.GetConsultaReporte_IngresoAProduccionDetalle(DateFrom, DateTo, idSector, idTipoProducto, idProducto, NumTropa);


            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "IDPIEZA",
                    TextHeader = "PIEZA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "IDOI",
                    TextHeader = "OI",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "COD PRD",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INGRESO",
                    WidthPercent = 8.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SECTOR",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PUESTO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OPERADOR",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte Ingreso a Producción Detallado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} Sector: {1} TipoProducto: {2} Tropa: {3}",
                                                Producto, Sector, TipoProducto, NumTropa),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_IngresoProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto, int NumTropa)
        {
            MemoryStream ms = null;
            //IDPIEZA, IDOI, COD_PRD, PRODUCTO, TIPO_PRD, INGRESO, SECTOR, UNDS, NETO, TARA, PUESTO, OPERADOR
            DataTable dt = DbServices.GetConsultaReporte_IngresoAProduccionDetalle(DateFrom, DateTo, idSector, idTipoProducto, idProducto, NumTropa);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "IDPIEZA",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "IDOI",
                    TextHeader = "OI"
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CODIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INGRESO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SECTOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PUESTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OPERADOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte Ingresos de Productos a Producción",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} Sector: {1} TipoProducto: {2} Tropa: {3}",
                                                Producto, Sector, TipoProducto, NumTropa),

                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }


        public static MemoryStream GenerarPDFReport_IngresoProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto)
        {
            MemoryStream ms = null;
            //LOTE, SECTOR, COD_PRD, PRODUCTO, UNDS, NETO
            DataTable dt = DbServices.GetConsultaReporte_IngresoAProduccionTotalizado(DateFrom, DateTo, idSector, idTipoProducto, idProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SECTOR",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER

                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "COD PRD",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte Ingreso a Producción Totalizado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Sector: {0} Producto: {1} TipoProducto: {2}",
                                                Sector, Producto, TipoProducto),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_IngresoProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto)
        {
            MemoryStream ms = null;
            //LOTE, SECTOR, COD_PRD, PRODUCTO, UNDS, NETO
            DataTable dt = DbServices.GetConsultaReporte_IngresoAProduccionTotalizado(DateFrom, DateTo, idSector, idTipoProducto, idProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SECTOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CODIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte Ingresos de Productos a Producción Totalizado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Sector: {0} Producto: {1} TipoProducto: {2}",
                                                Sector, Producto, TipoProducto),

                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TipoBulto)
        {
            MemoryStream ms = null;
            //TIPO,NRO,LOTE,COD_PRD,PRODUCTO,TIPO_PRD,CREADA,OPERADOR,DESTINO,SECTOR,PUESTO,UNDS,NETO,TARA
            DataTable dt = DbServices.GetConsultaReporte_ProduccionDetalle(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo);

            string strBultosTotales = dt.Rows.Count.ToString();
            string strUnidadesTotales = dt.Compute("Sum(UNDS)", string.Empty).ToString();
            var strPesoTotal = dt.Compute("Sum(NETO)", string.Empty);

            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    TextHeader = "BULTO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    TextHeader = "LOTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "CÓD PRD",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CREADA",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DESTINO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SECTOR",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PUESTO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OPERADOR",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    WidthPercent = 4.0f,
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Piezas Pesadas en Producción Detallado",
                        SubTitleLine1 = String.Format("Reporte desde el {0} al {1} Sector: {2} Tipo: {3} Prd: {4} Bulto: {5}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy"), Sector, TipoProducto, Producto, TipoBulto),
                        SubTitleLine2 = String.Format("TOALES-BULTOS: {0} UNIDAES: {1} PESO TOTAL: {2:#0} kg",
                                                strBultosTotales, strUnidadesTotales, strPesoTotal),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TipoBulto)
        {
            MemoryStream ms = null;
            //TIPO,NRO,LOTE,COD_PRD,PRODUCTO,TIPO_PRD,CREADA,OPERADOR,DESTINO,SECTOR,PUESTO,UNDS,NETO,TARA
            DataTable dt = DbServices.GetConsultaReporte_ProduccionDetalle(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo);

            string strBultosTotales = dt.Rows.Count.ToString();
            string strUnidadesTotales = dt.Compute("Sum(UNDS)", string.Empty).ToString();
            var strPesoTotal = dt.Compute("Sum(NETO)", string.Empty);

            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO BULTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    TextHeader = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CÓDIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CREADA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DESTINO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SECTOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PUESTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OPERADOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TARA",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Piezas Pesadas en Producción Detallado",
                    SubTitleLine1 = String.Format("Reporte desde el {0} al {1} Sector: {2} Tipo: {3} Prd: {4} Bulto: {5}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy"), Sector, TipoProducto, Producto, TipoBulto),
                    SubTitleLine2 = String.Format("TOALES-BULTOS: {0} UNIDAES: {1} PESO TOTAL: {2:#0} kg",
                                                strBultosTotales, strUnidadesTotales, strPesoTotal),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TipoBulto)
        {
            MemoryStream ms = null;
            
            DataTable dt = DbServices.GetConsultaReporte_ProduccionTotalizado(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo);

            string strBultosTotales = dt.Rows.Count.ToString();
            string strUnidadesTotales = dt.Compute("Sum(UNDS)", string.Empty).ToString();
            var strPesoTotal = dt.Compute("Sum(NETO)", string.Empty);

            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO BULTO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "COD. PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "BULTOS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Piezas Pesadas en Producción Totalizado",
                        SubTitleLine1 = String.Format("Reporte desde el {0} al {1} Sector: {2} Tipo: {3} Prd: {4} Bulto: {5}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy"), Sector, TipoProducto, Producto, tipo),
                        SubTitleLine2 = String.Format("TOALES-BULTOS: {0} UNIDAES: {1} PESO TOTAL: {2:#0} kg",
                                                strBultosTotales, strUnidadesTotales, strPesoTotal),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }


        public static MemoryStream GenerarExcelReport_ProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TpoBulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_ProduccionTotalizado(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo);

            string strBultosTotales = dt.Rows.Count.ToString();
            string strUnidadesTotales = dt.Compute("Sum(UNDS)", string.Empty).ToString();
            var strPesoTotal = dt.Compute("Sum(NETO)", string.Empty);

            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO BULTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CÓDIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "BULTOS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Piezas Pesadas en Producción Totalizado",
                    SubTitleLine1 = String.Format("Reporte desde el {0} al {1} Sector: {2} Tipo: {3} Prd: {4} Bulto: {5}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy"), Sector, TipoProducto, Producto, tipo),
                    SubTitleLine2 = String.Format("TOALES-BULTOS: {0} UNIDAES: {1} PESO TOTAL: {2:#0} kg",
                                                strBultosTotales, strUnidadesTotales, strPesoTotal),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_InsumosProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipoBulto, string Insumo, string Bulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_InsumosProduccionDetallado(DateFrom, DateTo, idPrdInsumo, tipoBulto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO PIEZA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INSUMO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "FECHA",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });

                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Insumos en Producción Detallado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Insumo: {0} tipoBulto: {1}",
                                                Insumo, Bulto),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_InsumosProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipoBulto, string Insumo, string Bulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_InsumosProduccionDetallado(DateFrom, DateTo, idPrdInsumo, tipoBulto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO PIEZA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INSUMO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "FECHA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Insumos en Producción Detallado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Insumo: {0} tipoBulto: {1}",
                                                Insumo, Bulto),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_InsumosProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipo, string Insumo, string TipoBulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_InsumosProduccionTotalizado(DateFrom, DateTo, idPrdInsumo, tipo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO PIEZA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INSUMO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Insumos en Producción Totalizado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Insumo: {0} tipoBulto: {1}",
                                                Insumo, TipoBulto),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_InsumosProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipo, string Insumo, string TipoBulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_InsumosProduccionTotalizado(DateFrom, DateTo, idPrdInsumo, tipo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    TextHeader = "TIPO PIEZA",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INSUMO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
              
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Insumos en Producción Totalizado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Insumo: {0} tipoBulto: {1}",
                                                Insumo, TipoBulto),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_LogEventos(DateTime DateFrom, DateTime DateTo, string contexto, string evento, string detalle)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_LogEventos(DateFrom, DateTo, contexto, evento, detalle);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "FECHA",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                    WidthPercent = 7.0f
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OPERADOR",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "ESTACION",
                    TextHeader = "ESTACIÓN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "EVENTO",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CONTEXTO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DETALLE",
                    WidthPercent = 10.0f,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Log de Eventos",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> contexto: {0} evento: {1} detalle: {2}", contexto, evento,
                                                detalle),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_LogEventos(DateTime DateFrom, DateTime DateTo, string contexto, string evento, string detalle)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_LogEventos(DateFrom, DateTo, contexto, evento, detalle);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "FECHA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OPERADOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "ESTACION",
                    TextHeader = "ESTACIÓN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "EVENTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CONTEXTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DETALLE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Log de Eventos",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> contexto: {0} evento: {1} detalle: {2}", contexto, evento,
                                                detalle),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_RendimientoProduccionxTP(DateTime DateFrom, DateTime DateTo, int idTipoProducto, string TipoProducto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_RendimientoProduccionTP(DateFrom, DateTo, idTipoProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TOTAL_TIPO",
                    TextHeader = "TOTAL TIPO",
                    WidthPercent = 3.0f,
                    Format = "0.0",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TOTAL_PRODUCIDO",
                    TextHeader = "TOTAL PRODUCIDO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS_PRODUCIDAS",
                    TextHeader= "UNDS PRODUCIDAS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_UNITARIO",
                    Format = "0.00 \"Kg\"",
                    TextHeader = "PESO UNITARIO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "RENDIMIENTO",
                    Format = "0.0 \\%",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "STD",
                    Format = "0.0 \\%",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Rendimiento en Producción por Tipo de Producto",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> TipoProducto: {0}",
                                                TipoProducto),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_RendimientoProduccionxTP(DateTime DateFrom, DateTime DateTo, int idTipoProducto, string TipoProducto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_RendimientoProduccionTP(DateFrom, DateTo, idTipoProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TOTAL_TIPO",
                    TextHeader = "TOTAL TIPO",
                    Format = "0.0",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TOTAL_PRODUCIDO",
                    TextHeader = "TOTAL PRODUCIDO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS_PRODUCIDAS",
                    TextHeader = "UNDS PRODUCIDAS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_UNITARIO",
                    TextHeader = "PESO UNITARIO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "RENDIMIENTO",
                    Format = "0.0 \\%",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "STD",
                    Format = "0.0 \\%",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Rendimiento en Producción por Tipo de Producto",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> TipoProducto: {0}",
                                                TipoProducto),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_RendimientoProduccionxSector(DateTime DateFrom, DateTime DateTo, int idSector, string Sector)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_RendimientoProduccionSector(DateFrom, DateTo, idSector);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRODUCTO",
                    TextHeader = "CÓDIGO PRD",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CANTIDAD",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_TOTAL",
                    TextHeader = "PESO TOTAL",
                    WidthPercent = 3.0f,
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_PROM",
                    TextHeader = "PESO PROMEDIO",
                    WidthPercent = 3.0f,
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INCIDENTE",
                    Format = "0.0 \\%",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "RENDIMIENTO_STD",
                    TextHeader = "RENDIMIENTO STD",
                    WidthPercent = 3.0f,
                    Format = "0.0 \\%",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DESVIO",
                    Format = "0.0 \\%",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Rendimiento en Producción por Sector",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Sector: {0}",
                                                Sector),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_RendimientoProduccionxSector(DateTime DateFrom, DateTime DateTo, int idSector, string Sector)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_RendimientoProduccionSector(DateFrom, DateTo, idSector);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRODUCTO",
                    TextHeader = "CÓDIGO PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CANTIDAD",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_TOTAL",
                    TextHeader = "PESO TOTAL",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_PROM",
                    TextHeader = "PESO PROMEDIO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INCIDENTE",
                    Format = "0.0 \\%",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "RENDIMIENTO_STD",
                    TextHeader = "RENDIMIENTO STD",
                    Format = "0.0 \\%",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DESVIO",
                    Format = "0.0 \\%",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Rendimiento en Producción por Sector",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Sector: {0}",
                                                Sector),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_EgresosDetallado(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote,
           string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_EgresosDetallado(DateFrom, DateTo, lote, cliente, tipoBulto, idTipoProducto, idProducto, comprobantePedido);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PEDIDO",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "ESTADO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "COD PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRD",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 12.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "ORIGEN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "EGRESO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OPERADOR",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });

                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Egresos Detallado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                        SubTitleLine2 = String.Format("Filtros: Cliente: {0} Comprob: {1} Tipo Bulto: {2} Tipo Prod: {3} Prod: {4} Lote: {5}",
                                                nombreCliente, comprobantePedido, Bulto, TipoProducto, Producto,lote),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_EgresosDetallado(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote,
           string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_EgresosDetallado(DateFrom, DateTo, lote, cliente, tipoBulto, idTipoProducto, idProducto, comprobantePedido);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PEDIDO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "ESTADO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CÓDIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÒN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "ORIGEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "EGRESO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OPERADOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Egresos Detallado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                    SubTitleLine2 = String.Format("Filtros: Cliente: {0} Comprob: {1} Tipo Bulto: {2} Tipo Prod: {3} Prod: {4} Lote: {5}",
                                                nombreCliente, comprobantePedido, Bulto, TipoProducto, Producto, lote),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_EgresosTotalizadoxProdFecha(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido,
           string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_EgresosTotalizadoxProdFecha(DateFrom, DateTo, cliente, comprobantePedido,
                 tipoBulto, idTipoProducto, idProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PEDIDO",
                    WidthPercent = 5.0f,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CODIGO",
                    TextHeader = "COD PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "BULTOS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
              
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Egresos Totalizado x Producto/Fecha",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                                               
                        SubTitleLine2 = String.Format("Filtros aplicados -> cliente: {0} comprobante: {1} Bulto: {2} TipoPrd: {3} Prd: {4}",
                                                nombreCliente, comprobantePedido, Bulto, TipoProducto, Producto),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_EgresosTotalizadoxProdFecha(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido,
           string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_EgresosTotalizadoxProdFecha(DateFrom, DateTo, cliente, comprobantePedido
                , tipoBulto, idTipoProducto, idProducto);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PEDIDO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CODIGO",
                    TextHeader = "CÓDIGO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "BULTOS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
              
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Egresos Totalizado x Producto/Fecha",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                    SubTitleLine2 = String.Format("Filtros aplicados -> cliente: {0} comprobante: {1} Bulto: {2} TipoPrd: {3} Prd: {4}",
                                                nombreCliente, comprobantePedido, Bulto, TipoProducto, Producto),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_EgresosTotalizadoxDiaCliente(DateTime DateFrom, DateTime DateTo, string idCliente, string cliente)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_EgresosTotalizadoxDiaCliente(DateFrom, DateTo, idCliente);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DIA",
                    TextHeader = "DÍA",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Egresos Totalizado x Día/Cliente",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                               DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Cliente: {0}", cliente),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_EgresosTotalizadoxDiaCliente(DateTime DateFrom, DateTime DateTo, string idCliente, string cliente)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_EgresosTotalizadoxDiaCliente(DateFrom, DateTo, idCliente);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DIA",
                    TextHeader = "DÍA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Egresos Totalizado x Día/Cliente",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                               DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Cliente: {0}", cliente),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_EgresosSaldo(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote, string nombreCliente)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_EgresosConSaldos(DateFrom, DateTo, lote, cliente, comprobantePedido);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMPROBANTE",
                    WidthPercent = 8.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COD_PRD",
                    TextHeader = "CÓDIGO PRD",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS_PED",
                    TextHeader = "UNDS PEDIDAS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_PED",
                    TextHeader = "PESO PEDIDO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS_EGR",
                    TextHeader = "UNDS EGRESADAS",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_EGR",
                    TextHeader = "PESO EGRESADO",
                    WidthPercent = 4.0f,
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SALDO_UNDS",
                    TextHeader = "SALDO UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SALDO_PESO",
                    TextHeader = "SALDO PESO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Egresos con Saldo",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros: Cliente: {0} ComprobantePedido: {1} Lote: {2}",
                                                nombreCliente, comprobantePedido, lote),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_EgresosSaldos(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote, string nombreCliente)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_EgresosConSaldos(DateFrom, DateTo, lote, cliente, comprobantePedido);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMPROBANTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COD_PRD",
                    TextHeader = "CÓDIGO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS_PED",
                    TextHeader = "UNIDADES PEDIDAS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_PED",
                    TextHeader = "PESO PEDIDO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS_EGR",
                    TextHeader = "UNIDADES EGRESADAS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_EGR",
                    TextHeader = "PESO EGRESADO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SALDO_UNDS",
                    TextHeader = "SALDO UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SALDO_PESO",
                    TextHeader = "SALDO PESO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
               
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Egresos con Saldo",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}", DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> cliente: {0} comprobantePedido: {1} lote: {2}", nombreCliente, comprobantePedido, lote),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_EgresosInsumosDetallado(DateTime DateFrom, DateTime DateTo, string comprobantePedido, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_InsumosEgresosDetallado(DateFrom, DateTo, comprobantePedido, cliente, idPrdInsumo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PEDIDO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMPROBANTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INSUMO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Format = "0",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "FECHA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Egresos de Insumos Detallado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros: Comprobante :{0} Cliente :{1} Insumo :{2}",
                                                comprobantePedido, nombreCliente, Insumo),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_EgresosInsumosDetallado(DateTime DateFrom, DateTime DateTo, string comprobantePedido, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_InsumosEgresosDetallado(DateFrom, DateTo, comprobantePedido, cliente, idPrdInsumo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PEDIDO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMPROBANTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INSUMO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Format = "0",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "FECHA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
               
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Egresos de Insumos Detallado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros: Comprobante :{0} Cliente :{1} Insumo :{2}",
                                                comprobantePedido, nombreCliente, Insumo),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_EgresosInsumosTotalizado(DateTime DateFrom, DateTime DateTo, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_InsumosEgresosTotalizado(DateFrom, DateTo, cliente, idPrdInsumo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INSUMO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Format = "0.0",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Egresos de Insumos Totalizado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                        SubTitleLine2 = String.Format("Filtros aplicados -> Cliente: {0} Insumo: {1}",
                                                 nombreCliente, Insumo),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_EgresosInsumosTotalizado(DateTime DateFrom, DateTime DateTo, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_InsumosEgresosTotalizado(DateFrom, DateTo, cliente, idPrdInsumo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INSUMO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Format = "0",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
               
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Egresos de Insumos Totalizado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    SubTitleLine2 = String.Format("Filtros aplicados -> Cliente: {0} Insumo: {1}",
                                                 nombreCliente, Insumo),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_Devoluciones(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string nombreCliente)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_Devoluciones(DateFrom, DateTo, cliente, comprobantePedido);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                    WidthPercent = 3.0f,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "FECHA",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMPROBANTE",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Devoluciones",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                                               
                        SubTitleLine2 = String.Format("Filtros aplicados -> Cliente: {0} ComprobantePedido: {1}",
                                                nombreCliente, comprobantePedido),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_Devoluciones(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string nombreCliente)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_Devoluciones(DateFrom, DateTo, cliente, comprobantePedido);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "FECHA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMPROBANTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
               

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Devoluciones",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Cliente: {0} ComprobantePedido: {1}",
                                                nombreCliente, comprobantePedido),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }
        public static MemoryStream GenerarPDFReport_ProdReqPrepPedidos(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_PedidosReqPreparaciionPedidos(DateFrom, DateTo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CodigoSAC",
                    TextHeader = "CÓDIGO SAC",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "ProductoSAC",
                    TextHeader = "PRODUCTO SAC",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "Observacion",
                    TextHeader = "OBSERVACIÓN",
                    WidthPercent = 15.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "Unds_PED",
                    TextHeader = "UNIDADES PEDIDAS",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "Peso_PED",
                    TextHeader = "PESO PEDIDO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Productos Requeridos para Preparación de Pedidos",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ProdReqPrepPedidos(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_PedidosReqPreparaciionPedidos(DateFrom, DateTo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CodigoSAC",
                    TextHeader = "CÓDIGO SAC",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "ProductoSAC",
                    TextHeader = "PRODUCTO SAC",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "Observacion",
                    TextHeader = "OBSERVACIÓN",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "Unds_PED",
                    TextHeader = "UNIDADES PEDIDAS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "Peso_PED",
                    TextHeader = "PESO PEDIDO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Productos Requeridos para Preparación de Pedidos",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}",
                                                DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockDetalle(DateTime DateTo,int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockDetalle(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPIF",
                    TextHeader = "TIPIFICACIÓN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock Detallado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockDetalle(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockDetalle(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPIF",
                    TextHeader="TIPIFICACIÓN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });


                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock Detallado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockTotalizado(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockTotalizado(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CODIGO",
                    TextHeader = "CÓDIGO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "BULTOS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock Totalizado",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockTotalizado(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockTotalizado(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO_PRD",
                    TextHeader = "TIPO PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CODIGO",
                    TextHeader = "CÓDIGO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "BULTOS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });


                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock Totalizado",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockTotalizadoDestino(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockTotalizadoDestino(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CODIGO",
                    TextHeader = "CÓDIGO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "BULTOS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESONETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock Totalizado por Destino",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockTotalizadoDestino(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockTotalizadoDestino(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CODIGO",
                    TextHeader = "CÓDIGO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "BULTOS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESONETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });


                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock Totalizado por Destino",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockDetalleVencimiento(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockDetalleVencimiento(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "VENCIMIENTO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock Detallado por Vencimiento",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockDetalleVencimiento(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockDetalleVencimiento(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "VENCIMIENTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock Detalle por Vencimiento",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockDetalleProxVencimiento(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion, int diasProximmidadVencimiento)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockDetalleProxVencimiento(DateTo, idTipoProducto, idProducto, idUbicacion, diasProximmidadVencimiento);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TIPO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "VENCIMIENTO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock Detallado por Proximidad al Vencimiento",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2} Días Vencimiento: {3}",
                                                Producto, TipoProducto, Ubicacion, diasProximmidadVencimiento),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockDetalleProxVencimiento(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion, int diasProximidadVencimiento)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockDetalleProxVencimiento(DateTo, idTipoProducto, idProducto, idUbicacion, diasProximidadVencimiento);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TIPO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NRO",
                    TextHeader = "NÚMERO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UBICACION",
                    TextHeader = "UBICACIÓN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "VENCIMIENTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock Detalle por Proximidad al Vencimiento",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2} Días Vencimiento: {3}",
                                                Producto, TipoProducto, Ubicacion, diasProximidadVencimiento),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockContenedoresTotalDestino(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockContenedoresTotalDestino(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CONTENEDOR",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "DESTINO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "BULTOS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESONETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
            
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock de Contenedores Totalizado por Destino",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1} Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockContenedoresTotalDestino(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockContenedoresTotalDestino(DateTo, idTipoProducto, idProducto, idUbicacion);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CONTENEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DESTINO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "BULTOS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESONETO",
                    TextHeader = "PESO NETO",
                    Format = "0.00 \"Kg\"",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock de Contenedores Totalizado por Destino",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> Producto: {0} TipoProducto: {1}, Ubicación: {2}",
                                                Producto, TipoProducto, Ubicacion),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ExistenciaStockInsumos(DateTime DateTo, int idPrdInsumo, string Insumo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockInsumos(DateTo, idPrdInsumo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INSUMO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Format = "0",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
               
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Existencia en Stock de Insumos",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                        SubTitleLine2 = String.Format("Filtros aplicados -> IdPrdInsumo: {0} Insumo: {1}",
                                               idPrdInsumo, Insumo),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ExistenciaStockInsumos(DateTime DateTo, int idPrdInsumo, string Insumo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_ExistenciaStockInsumos(DateTo, idPrdInsumo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INSUMO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNDS",
                    TextHeader = "UNIDADES",
                    Format = "0",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
               
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Existencia en Stock de Insumos",
                    SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0}",
                                                DateTo),

                    SubTitleLine2 = String.Format("Filtros aplicados -> IdPrdInsumo: {0} Insumo: {1}",
                                               idPrdInsumo, Insumo),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_TrazabilidadPieza(int numPieza)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_TrazabilidadPieza(numPieza);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OI",
                    WidthPercent = 3.0f,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INGRESO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PROVEEDOR",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SANITARIO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });

                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Trazabilidad por Pieza",
                        
                        SubTitleLine2 = String.Format("Filtros aplicados -> númeroPieza: {0}",
                                                numPieza),
                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_TrazabiliadPieza(int numPieza)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_TrazabilidadPieza(numPieza);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OI",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INGRESO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PROVEEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SANITARIO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Trazabilidad por Pieza",

                    SubTitleLine2 = String.Format("Filtros aplicados -> númeroPieza: {0}",
                                                numPieza),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_TrazabilidadLote(string lote)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_TrazabilidadLote(lote);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OI",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INGRESO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PROVEEDOR",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SANITARIO",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TROPA",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });

                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Trazabilidad por Lote",
                        SubTitleLine1 = String.Format("Reporte basado en Lote: {0}", lote),

                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_TrazabiliadLote(string lote)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaRepote_TrazabilidadLote(lote);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OI",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INGRESO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PROVEEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SANITARIO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TROPA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte de Trazabilidad por Lote",
                    SubTitleLine1 = String.Format("Reporte basado en Lote: {0}",lote),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_HistoricoPiezaContenedor(int numPieza, int esContenedor, string TipoBulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_HistoricoPiezaContenedor(numPieza, esContenedor);
            string Bulto = TipoBulto == "NO" ? "PIEZA": "CONTENEDOR";
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "MOV",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "FECHA",
                    WidthPercent = 5.0f,
                    Format = "dd/mm/yyyy",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PRODUCTO",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "LOTE",
                    WidthPercent = 5.0f,
                    Format = "ddmmyyyy",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OI",
                    WidthPercent = 2.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PROVEEDOR",
                    WidthPercent = 10.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SANITARIO",
                    WidthPercent = 7.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CONTENEDOR",
                    TextHeader = "CONT",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CLIENTE",
                    WidthPercent = 8.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMPROBANTE",
                    TextHeader = "COMPROB",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SECTOR",
                    WidthPercent = 4.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "OPERADOR",
                    WidthPercent = 5.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "ESTACION",
                    TextHeader= "EST",
                    WidthPercent = 2.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "UNIDADES",
                    TextHeader = "UNDS",
                    WidthPercent = 3.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "NETO",
                    WidthPercent = 3.0f,
                    Format = "0.00",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "TARA",
                    WidthPercent = 3.0f,
                    Format = "0.00",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PESO_REMITIDO",
                    TextHeader = "REM",
                    WidthPercent = 3.0f,
                    Format = "0.00",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte Histórico de Pieza y Contenedor",
                        SubTitleLine1 = String.Format("Reporte basado en Pieza Num: {0} Tipo Bulto: {1}", numPieza, Bulto),

                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_HistoricoPiezaContenedor(int numPieza, int esContenedor, string TipoBulto)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_HistoricoPiezaContenedor(numPieza, esContenedor);
            string Bulto = TipoBulto == "NO" ? "PIEZA" : "CONTENEDOR";

            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "MOV",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "FECHA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PRODUCTO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "LOTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OI",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PROVEEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SANITARIO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CONTENEDOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "DESTINO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CLIENTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMPROBANTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SECTOR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "OPERADPR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "ESTACION",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "UNIDADES",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "NETO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "TARA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PESO_REMITIDO",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                ReportEXCEL report = new ReportEXCEL(dt, new REPORTEXCEL.HeaderReportData()
                {
                    Title = "Reporte Histórico de Pieza y Contenedor",
                    SubTitleLine1 = String.Format("Reporte basado en NUm Pieza: {0} Tipo Bulto: {1}", numPieza, Bulto),
                },
                columnsReport);
                ms = report.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarPDFReport_ResultadoInventario(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_ResultInventario(DateFrom, DateTo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportPDFCollection columnsReport = new ColumnReportPDFCollection();
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "FECHA",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "INVENTARIO",
                    Format ="dd-MM-yyyy",
                    WidthPercent = 6.0f,
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_VERIF_EN_STOCK",
                    TextHeader = "PIEZAS VERIF EN STOCK",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_SIN_STOCK_EXISTEN",
                    TextHeader = "PIEZAS SIN STOCK EXISTEN",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_SIN_STOCK_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_CON_STOCK_NO_EXISTEN",
                    TextHeader = "PIEZAS CON STOCK NO EXISTEN",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_CON_STOCK_NO_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_ENSTOCK",
                    TextHeader = "PIEZAS FUERA CONTENEDOR EN STOCK",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_ENSTOCK_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_SINSTOCK",
                    TextHeader = "PIEZAS FUERA CONTENEDOR SIN STOCK",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });

                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_SINSTOCK_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CAJAS_VERIF_EN_STOCK",
                    TextHeader = "CAJAS VERIF EN STOCK",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CAJAS_CON_STOCK_NO_EXISTEN",
                    TextHeader = "CAJAS CON STOCK NO EXISTEN",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CAJAS_CON_STOCK_NO_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CAJAS_SIN_STOCK_EXISTEN",
                    TextHeader = "CAJAS SIN STOCK EXISTEN",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "CAJAS_SIN_STOCK_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMBOS_VERIF_EN_STOCK",
                    TextHeader = "COMBOS VERIF EN STOCK",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMBOS_CON_STOCK_NO_EXISTEN",
                    TextHeader = "COMBOS CON STOCK NO EXISTEN",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMBOS_CON_STOCK_NO_EXISTEN_AJUSTADOS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMBOS_SIN_STOCK_EXISTEN",
                    TextHeader = "COMBOS SIN STOCK EXISTEN",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "COMBOS_SIN_STOCK_EXISTEN_AJUSTADOS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "BULTOS_EN_PEDIDOS_ABIERTOS",
                    TextHeader = "BULTOS EN PEDIDOS ABIERTOS",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportPDF()
                {
                    Name = "SIN_REGISTRAR",
                    TextHeader = "SIN REGISTRAR",
                    Alignment = REPORTPDF.ColumnAlignment.CENTER,
                });

                ReportPDF rpdf = new ReportPDF()
                {
                    DatTable = dt,
                    HeaderData = new REPORTPDF.HeaderReportData()
                    {
                        Title = "Reporte de Resultado de Inventario",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}", DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),

                        BusinessLogoPath = HttpContext.Current.Request.MapPath("~/images/LogoBusiness.png")
                    },
                    FooterData = new FooterReportData()
                    {
                        TextLine = "Software desarrollado por Ingeniería MCR "
                    },
                    ColumnsReport = columnsReport,
                    
                    HeaderTable = new HeaderTable()
                    {
                        FontSize=8.0f
                    }
                   
                };
                ms = rpdf.Make();
            }
            return ms;
        }

        public static MemoryStream GenerarExcelReport_ResultadoInventario(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms = null;
            DataTable dt = DbServices.GetConsultaReporte_ResultInventario(DateFrom, DateTo);
            if (dt != null && dt.Rows.Count > 0)
            {
                ColumnReportEXCELCollection columnsReport = new ColumnReportEXCELCollection();
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "FECHA",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "INVENTARIO",
                    Format = "dd-MM-yyyy",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_VERIF_EN_STOCK",
                    TextHeader = "PIEZAS VERIF EN STOCK",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_SIN_STOCK_EXISTEN",
                    TextHeader = "PIEZAS SIN STOCK EXISTEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_SIN_STOCK_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_CON_STOCK_NO_EXISTEN",
                    TextHeader = "PIEZAS CON STOCK NO EXISTEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_CON_STOCK_NO_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_ENSTOCK",
                    TextHeader = "PIEZAS FUERA CONTENEDOR EN STOCK",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_ENSTOCK_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_SINSTOCK",
                    TextHeader = "PIEZAS FUERA CONTENEDOR SIN STOCK",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "PIEZAS_FUERA_CONTENEDOR_SINSTOCK_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CAJAS_VERIF_EN_STOCK",
                    TextHeader = "CAJAS VERIF EN STOCK",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CAJAS_CON_STOCK_NO_EXISTEN",
                    TextHeader = "CAJAS CON STOCK NO EXISTEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CAJAS_CON_STOCK_NO_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CAJAS_SIN_STOCK_EXISTEN",
                    TextHeader = "CAJAS SIN STOCK EXISTEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "CAJAS_SIN_STOCK_EXISTEN_AJUSTADAS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMBOS_VERIF_EN_STOCK",
                    TextHeader = "COMBOS VERIF EN STOCK",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMBOS_CON_STOCK_NO_EXISTEN",
                    TextHeader = "COMBOS CON STOCK NO EXISTEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMBOS_CON_STOCK_NO_EXISTEN_AJUSTADOS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMBOS_SIN_STOCK_EXISTEN",
                    TextHeader = "COMBOS SIN STOCK EXISTEN",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "COMBOS_SIN_STOCK_EXISTEN_AJUSTADOS",
                    TextHeader = "AJUSTE",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "BULTOS_EN_PEDIDOS_ABIERTOS",
                    TextHeader = "BULTOS EN PEDIDOS ABIERTOS",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });
                columnsReport.Add(new ColumnReportEXCEL()
                {
                    Name = "SIN_REGISTRAR",
                    TextHeader = "SIN REGISTRAR",
                    Alignment = REPORTEXCEL.ColumnAlignment.CENTER,
                });

                ReportEXCEL report = new ReportEXCEL()
                {
                    DatTable = dt,
                    HeaderData = new REPORTEXCEL.HeaderReportData()
                    {
                        Title = "Reporte de Resultado de Inventario",
                        SubTitleLine1 = String.Format("Reporte basado en rango de fechas del {0} al {1}", DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy")),
                    },
                    ColumnsReport = columnsReport
                };
                ms = report.Make();
            }
            return ms;
        }
    }
}