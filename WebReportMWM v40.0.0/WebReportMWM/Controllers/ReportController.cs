using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReportMWM.Models;
using WebReportMWM.services;

namespace WebReportMWM.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        #region ReportIngresoPlantaDetallado

        // GET: Report
        [HttpGet]
        public ActionResult ReportIngresoPlantaDetallado()
        {
            ReportIngresoPlantaDetalladoModel model;
            model = TempData["ReportIngresoPlantaDetalladoModel"] == null ? new ReportIngresoPlantaDetalladoModel() : TempData["ReportIngresoPlantaDetalladoModel"] as ReportIngresoPlantaDetalladoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProveedores = DbServices.GetProveedoresSACToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            //IDPIEZA, IDOI, PROVEEDOR, SANITARIO, CODIGO_PROD, PRODUCTO, TIPO_PRD, PESADA, DESTINO,
            //UNDS, NETO, TARA, REMITIDO, OPERADOR
            model.DatTable = DbServices.GetConsultaReporte_IngresoAPlantaDetalle(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.pesadaManual), model.idProveedor, Convert.ToInt32(model.idProducto), Convert.ToInt32(model.SelectIdTipoProducto), Convert.ToInt32(model.numTropa));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportIngresoPlantaDetallado(ReportIngresoPlantaDetalladoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProveedores = DbServices.GetProveedoresSACToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.SelectIdTipoProducto));
            model.DatTable = DbServices.GetConsultaReporte_IngresoAPlantaDetalle(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.pesadaManual), model.idProveedor, Convert.ToInt32(model.idProducto), Convert.ToInt32(model.SelectIdTipoProducto), Convert.ToInt32(model.numTropa));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_IngresoPlantaDetallado(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto, int PesadaManual, string TipoPesada, int NumTropa)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_IngresoPlantaDetallado(DateFrom, DateTo, idProveedor, idProducto, idTipoProducto, Proveedor, Producto, TipoProducto, PesadaManual, TipoPesada,NumTropa);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteIngresosAPlanta.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_IngresoPlantaDetallado(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto, int PesadaManual, string TipoPesada, int NumTropa)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_IngresoPlantaDetallado(DateFrom, DateTo, idProveedor, idProducto, idTipoProducto, Proveedor, Producto, TipoProducto, PesadaManual, TipoPesada, NumTropa);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteIngresosAPlanta.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportIngresoPlantaTotalizadoxOIProducto

        [HttpGet]
        public ActionResult ReportIngresoPlantaTotalizadoxOIProducto()
        {
            ReportIngPlantaTotalizadoxOIProductoModel model;
            model = TempData["ReportIngPlantaTotalizadoxOIProductoModel"] == null ? new ReportIngPlantaTotalizadoxOIProductoModel() : TempData["ReportIngPlantaTotalizadoxOIProductoModel"] as ReportIngPlantaTotalizadoxOIProductoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProveedores = DbServices.GetProveedoresSACToListSelectListItem();
            model.ListProductos = DbServices.GetProductosNoInsumosToListSelectListItem();

            //IDOI, PROVEEDOR, SANITARIO, CODIGO_PROD, PRODUCTO,UNDS, NETO, TARA, REMITIDO
            model.DatTable = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxOIProducto(model.selectDateFrom, model.selectDateTo, model.idProveedor, Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idTipoProducto));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportIngresoPlantaTotalizadoxOIProducto(ReportIngPlantaTotalizadoxOIProductoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProveedores = DbServices.GetProveedoresSACToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.DatTable = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxOIProducto(model.selectDateFrom, model.selectDateTo, model.idProveedor, Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idTipoProducto));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_IngresoPlantaTotalizadoxOIProducto(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_IngresoPlantaTotalizadoxOIProducto(DateFrom, DateTo, idProveedor, idProducto, idTipoProducto, Proveedor, Producto, TipoProducto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteIngresosAPlantaxOIProducto.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_IngresoPlantaTotalizadoxOIProducto(DateTime DateFrom, DateTime DateTo, string idProveedor, int idProducto, int idTipoProducto, string Proveedor, string Producto, string TipoProducto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_IngresoPlantaTotalizadoxOIProducto(DateFrom, DateTo, idProveedor, idProducto, idTipoProducto, Proveedor, Producto, TipoProducto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteIngresosAPlantaxOIProducto.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportIngresoPlantaTotalizadoxDiaProveedor
        [HttpGet]
        public ActionResult ReportIngresoPlantaTotalizadoxDiaProveedor()
        {
            ReportIngPlantaTotalizadoxDiaProveedorModel model;
            model = TempData["ReportIngPlantaTotalizadoxDiaProveedorModel"] == null ? new ReportIngPlantaTotalizadoxDiaProveedorModel() : TempData["ReportIngPlantaTotalizadoxDiaProveedorModel"] as ReportIngPlantaTotalizadoxDiaProveedorModel;
            model.ListProveedores = DbServices.GetProveedoresSACToListSelectListItem();
            //DIA, PROVEEDOR, UNDS, NETO, TARA, REMITIDO
            model.DatTable = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxDiaProveedor(model.selectDateFrom, model.selectDateTo, model.idProveedor);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportIngresoPlantaTotalizadoxDiaProveedor(ReportIngPlantaTotalizadoxDiaProveedorModel model)
        {
            model.ListProveedores = DbServices.GetProveedoresSACToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_IngresoAPlantaTotalizadoxDiaProveedor(model.selectDateFrom, model.selectDateTo, model.idProveedor);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_IngresoPlantaTotalizadoxDiaProveedor(DateTime DateFrom, DateTime DateTo, string idProveedor, string Proveedor)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_IngresoPlantaTotalizadoxDiaProveedor(DateFrom, DateTo, idProveedor, Proveedor);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteIngresosAPlantaxDiaProveedor.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcel_IngresoPlantaTotalizadoxDiaProveedor(DateTime DateFrom, DateTime DateTo, string idProveedor, string Proveedor)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcel_IngresoPlantaTotalizadoxDiaProveedor(DateFrom, DateTo, idProveedor, Proveedor);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteIngresosAPlantaxDiaProveedor.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportIngresoProduccionDetallado
        [HttpGet]
        public ActionResult ReportIngresoProduccionDetallado()
        {
            ReportIngresoProduccionDetalladoModel model;
            model = TempData["ReportIngresoProduccionDetalladoModel"] == null ? new ReportIngresoProduccionDetalladoModel() : TempData["ReportIngresoProduccionDetalladoModel"] as ReportIngresoProduccionDetalladoModel;
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            //IDPIEZA, IDOI, COD_PRD, PRODUCTO, TIPO_PRD, INGRESO, SECTOR, UNDS, NETO, TARA, PUESTO, OPERADOR
            model.DatTable = DbServices.GetConsultaReporte_IngresoAProduccionDetalle(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.numTropa));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportIngresoProduccionDetallado(ReportIngresoProduccionDetalladoModel model)
        {
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_IngresoAProduccionDetalle(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.numTropa));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerarPDFReport_IngresoProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto, int NumTropa)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_IngresoProduccionDetallado(DateFrom, DateTo, idSector, idProducto, idTipoProducto, Sector, Producto, TipoProducto, NumTropa);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteIngresosAProduccion.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_IngresoProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto, int NumTropa)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_IngresoProduccionDetallado(DateFrom, DateTo, idSector, idProducto, idTipoProducto, Sector, Producto, TipoProducto, NumTropa);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteIngresosAProduccion.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportIngresoProduccionTotalizado
        [HttpGet]
        public ActionResult ReportIngresoProduccionTotalizado()
        {
            ReportIngresoProduccionTotalizadoModel model;
            model = TempData["ReportIngresoProduccionTotalizadoModel"] == null ? new ReportIngresoProduccionTotalizadoModel() : TempData["ReportIngresoProduccionTotalizadoModel"] as ReportIngresoProduccionTotalizadoModel;
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            //LOTE, SECTOR, COD_PRD, PRODUCTO, UNDS, NETO
            model.DatTable = DbServices.GetConsultaReporte_IngresoAProduccionTotalizado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportIngresoProduccionTotalizado(ReportIngresoProduccionTotalizadoModel model)
        {
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_IngresoAProduccionTotalizado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_IngresoProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_IngresoProduccionTotalizado(DateFrom, DateTo, idSector, idProducto, idTipoProducto, Sector, Producto, TipoProducto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteIngresosAProduccionTotalizado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_IngresoProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idProducto, int idTipoProducto, string Sector, string Producto, string TipoProducto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_IngresoProduccionTotalizado(DateFrom, DateTo, idSector, idProducto, idTipoProducto, Sector, Producto, TipoProducto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteIngresosAProduccionTotalizado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReporProducciónDetallado
        [HttpGet]
        public ActionResult ReportProduccionDetallado()
        {
            ReportProduccionDetalladoModel model;
            model = TempData["ReportProduccionDetalladoModel"] == null ? new ReportProduccionDetalladoModel() : TempData["ReportProduccionDetalladoModel"] as ReportProduccionDetalladoModel;
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_ProduccionDetalle(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), model.tipo);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportProduccionDetallado(ReportProduccionDetalladoModel model)
        {
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_ProduccionDetalle(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), model.tipo);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TipoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ProduccionDetallado(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo, Sector, TipoProducto, Producto, TipoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteProduccionDetalldo.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TipoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ProduccionDetallado(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo, Sector, TipoProducto, Producto, TipoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteProduccionDetallado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportProduccionTotalizado

        [HttpGet]

        public ActionResult ReportProduccionTotalizado()
        {
            ReportProduccionTotalizadoModel model;
            model = TempData["ReportProduccionTotalizadoModel"] == null ? new ReportProduccionTotalizadoModel() : TempData["ReportProduccionTotalizadoModel"] as ReportProduccionTotalizadoModel;
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_ProduccionTotalizado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), model.tipo);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportProduccionTotalizado(ReportProduccionTotalizadoModel model)
        {
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_ProduccionTotalizado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector), Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), model.tipo);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TpoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ProduccionTotalizado(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo, Sector, TipoProducto, Producto, TpoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteProduccionTotlizado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idSector, int idTipoProducto, int idProducto, string tipo, string Sector, string TipoProducto, string Producto, string TpoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ProduccionTotalizado(DateFrom, DateTo, idSector, idTipoProducto, idProducto, tipo, Sector, TipoProducto, Producto, TpoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteProduccionTotalizado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportInsumosProduccionDetallado

        [HttpGet]
        public ActionResult ReportInsumosProduccionDetallado()
        {
            ReportInsumosProduccionDetalladoModel model;
            model = TempData["ReportInsumosProduccionDetalladoModel"] == null ? new ReportInsumosProduccionDetalladoModel() : TempData["ReportInsumosProduccionDetalladoModel"] as ReportInsumosProduccionDetalladoModel;
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_InsumosProduccionDetallado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idPrdInsumo), model.tipoBulto);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportInsumosProduccionDetallado(ReportInsumosProduccionDetalladoModel model)
        {
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_InsumosProduccionDetallado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idPrdInsumo), model.tipoBulto);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_InsumosProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipoBulto, string Insumo, string Bulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_InsumosProduccionDetallado(DateFrom, DateTo, idPrdInsumo, tipoBulto, Insumo, Bulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteInsumosProduccionDetallado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_InsumosProduccionDetallado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipoBulto, string Insumo, string Bulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_InsumosProduccionDetallado(DateFrom, DateTo, idPrdInsumo, tipoBulto, Insumo, Bulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteInsumosProduccionDetallado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region ReportInsumosProduccionTotalizado
        [HttpGet]
        public ActionResult ReportInsumosProduccionTotalizado()
        {
            ReportInsumosProduccionTotalizadoModel model;
            model = TempData["ReportInsumosProduccionTotalizadoModel"] == null ? new ReportInsumosProduccionTotalizadoModel() : TempData["ReportInsumosProduccionTotalizadoModel"] as ReportInsumosProduccionTotalizadoModel;
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_InsumosProduccionTotalizado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idPrdInsumo), model.tipoBulto);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportInsumosProduccionTotalizado(ReportInsumosProduccionTotalizadoModel model)
        {
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_InsumosProduccionTotalizado(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idPrdInsumo), model.tipoBulto);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_InsumosProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipo, string Insumo, string TipoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_InsumosProduccionTotalizado(DateFrom, DateTo, idPrdInsumo, tipo, Insumo, TipoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteInsumosProduccionTotalizado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_InsumosProduccionTotalizado(DateTime DateFrom, DateTime DateTo, int idPrdInsumo, string tipo, string Insumo, string TipoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_InsumosProduccionTotalizado(DateFrom, DateTo, idPrdInsumo, tipo, Insumo, TipoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteInsumosProduccionTotalizado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Log de Eventos

        [HttpGet]
        public JsonResult CargarListEventos(string contexto)
        {
            var eventos = DbServices.GetEventosDbLog(contexto);
            return Json(eventos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReportLogEventos()
        {
            ReportLogEventosModel model = new ReportLogEventosModel();
            model = TempData["ReportLogEventosModel"] == null ? new ReportLogEventosModel() : TempData["ReportLogEventosModel"] as ReportLogEventosModel;
            model.ListContexto = DbServices.GetContextosDbLog();
            model.ListEvento = new List<SelectListItem>();
            model.ListEvento.Insert(0, new SelectListItem { Text = "TODOS", Value = "" });
            model.DatTable = DbServices.GetConsultaReporte_LogEventos(model.selectDateFrom, model.selectDateTo, model.contexto, model.evento, model.detalle);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportLogEventos(ReportLogEventosModel model)
        {
            model.ListContexto = DbServices.GetContextosDbLog();
            model.ListEvento = new List<SelectListItem>();
            model.ListEvento = DbServices.GetEventosDbLog(model.contexto);
            model.ListEvento.Insert(0, new SelectListItem { Text = "TODOS", Value = "" });
            model.DatTable = DbServices.GetConsultaReporte_LogEventos(model.selectDateFrom, model.selectDateTo, model.contexto, model.evento, model.detalle);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }
        public ActionResult GenerarPDFReport_LogEventos(DateTime DateFrom, DateTime DateTo, string contexto, string evento, string detalle)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_LogEventos(DateFrom, DateTo, contexto, evento, detalle);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteLogEventos.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_LogEventos(DateTime DateFrom, DateTime DateTo, string contexto, string evento, string detalle)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_LogEventos(DateFrom, DateTo, contexto, evento, detalle);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteLogEventos.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        #endregion

        #region Reporte Rendimiento en Produccion x TP
        [HttpGet]
        public ActionResult ReportRendimientoProduccionTP()
        {
            ReportRendimientoProduccionTPModel model;
            model = TempData["ReportRendimientoProduccionTPModel"] == null ? new ReportRendimientoProduccionTPModel() : TempData["ReportRendimientoProduccionTPModel"] as ReportRendimientoProduccionTPModel;
            model.ListTiposProducto = DbServices.GetTipoProductosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_RendimientoProduccionTP(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idTipoProducto));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportRendimientoProduccionTP(ReportRendimientoProduccionTPModel model)
        {
            model.ListTiposProducto = DbServices.GetTipoProductosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_RendimientoProduccionTP(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idTipoProducto));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_RendimientoProduccionxTP(DateTime DateFrom, DateTime DateTo, int idTipoProducto, string TipoProducto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_RendimientoProduccionxTP(DateFrom, DateTo, idTipoProducto, TipoProducto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteRendimientoProduccionTP.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_RendimientoProduccionxTP(DateTime DateFrom, DateTime DateTo, int idTipoProducto, string TipoProducto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_RendimientoProduccionxTP(DateFrom, DateTo, idTipoProducto, TipoProducto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteRendimientoProduccionTP.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Rendimiento Produccion por Sector
        [HttpGet]
        public ActionResult ReportRendimientoProduccionSector()
        {
            ReportRendiminentoProduccionXSectorModel model;
            model = TempData["ReportRendiminentoProduccionXSectorModel"] == null ? new ReportRendiminentoProduccionXSectorModel() : TempData["ReportRendiminentoProduccionXSectorModel"] as ReportRendiminentoProduccionXSectorModel;
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            if(model.idSector != "0")
            {
                model.DatTable = DbServices.GetConsultaRepote_RendimientoProduccionSector(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector));
            } 
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportRendimientoProduccionSector(ReportRendiminentoProduccionXSectorModel model)
        {
            model.ListSectores = DbServices.GetSectoresToListSelectListItem();
            if (model.idSector != "0")
            {
                model.DatTable = DbServices.GetConsultaRepote_RendimientoProduccionSector(model.selectDateFrom, model.selectDateTo, Convert.ToInt32(model.idSector));
                TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            } else
            {
                TempData["MessageStatus"] = "Debe seleccionar un sector !!.";
            }
          
            return View(model);
        }

        public ActionResult GenerarPDFReport_RendimientoProduccionxSector(DateTime DateFrom, DateTime DateTo, int idSector, string Sector)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_RendimientoProduccionxSector(DateFrom, DateTo, idSector,Sector);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteRendimientoProduccionSector.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_RendimientoProduccionxSector(DateTime DateFrom, DateTime DateTo, int idSector, string Sector)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_RendimientoProduccionxSector(DateFrom, DateTo, idSector, Sector);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteRendimientoProduccionSector.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Egresos Detallado
        [HttpGet]
        public ActionResult ReportEgresosDetallado()
        {
            ReportEgresosDetalladoModel model;
            model = TempData["ReportEgresosDetalladoModel"] == null ? new ReportEgresosDetalladoModel() : TempData["ReportEgresosDetalladoModel"] as ReportEgresosDetalladoModel;
            model.ListCliente = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.ListTiposProducto = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaReporte_EgresosDetallado(model.selectDateFrom, model.selectDateTo, model.lote, model.cliente, model.tipoBulto, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), model.comprobantePedido);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportEgresosDetallado(ReportEgresosDetalladoModel model)
        {
            model.ListCliente = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.ListTiposProducto = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.DatTable = DbServices.GetConsultaReporte_EgresosDetallado(model.selectDateFrom, model.selectDateTo, model.lote, model.cliente,  model.tipoBulto, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto),model.comprobantePedido);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_EgresosDetallados(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote,
            string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_EgresosDetallado(cliente, DateFrom, DateTo, comprobantePedido, lote
                , tipoBulto, idTipoProducto, idProducto, nombreCliente, Bulto, TipoProducto, Producto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteEgresosDetallados.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_EgresosDetallados(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote,
            string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_EgresosDetallado(cliente, DateFrom, DateTo, comprobantePedido, lote
                , tipoBulto, idTipoProducto, idProducto, nombreCliente, Bulto, TipoProducto, Producto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteEgresosDetallados.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Egresos Totalizados x Producto Fecha
        [HttpGet]
        public ActionResult ReportEgresosTotalizadoxProdFecha()
        {
            ReportEgresosTotalizadosxProdFechaModel model;
            model = TempData["ReportEgresosTotalizadosxProdFechaModel"] == null ? new ReportEgresosTotalizadosxProdFechaModel() : TempData["ReportEgresosTotalizadosxProdFechaModel"] as ReportEgresosTotalizadosxProdFechaModel;
            model.ListCliente = DbServices.GetClientesToListSelectListItem("",new SelectListItem() { Value="",Text="Todos"});
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.ListTiposProducto = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_EgresosTotalizadoxProdFecha(model.selectDateFrom, model.selectDateFrom, model.cliente, model.comprobantePedido,
                 model.tipoBulto, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportEgresosTotalizadoxProdFecha(ReportEgresosTotalizadosxProdFechaModel model)
        {
            model.ListCliente = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" }); 
            model.ListTiposBulto = DbServices.GetTiposBultoToListSelectListItem();
            model.ListTiposProducto = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.DatTable = DbServices.GetConsultaRepote_EgresosTotalizadoxProdFecha(model.selectDateFrom, model.selectDateFrom, model.cliente, model.comprobantePedido,
                 model.tipoBulto, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerarPDFReport_EgresosTotalizadoxProdFecha(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido,
            string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_EgresosTotalizadoxProdFecha(cliente, DateFrom, DateTo, comprobantePedido, tipoBulto, idTipoProducto, idProducto, nombreCliente,
                Bulto, TipoProducto, Producto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteEgresosTotalizadoxProdFecha.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_EgresosTotalizadoxProdFecha(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido,
           string tipoBulto, int idTipoProducto, int idProducto, string nombreCliente, string Bulto, string TipoProducto, string Producto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_EgresosTotalizadoxProdFecha(cliente, DateFrom, DateTo, comprobantePedido, tipoBulto, idTipoProducto, idProducto,
                nombreCliente, Bulto, TipoProducto, Producto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteEgresosTotalizadoxProdFecha.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion
        #region Reporte Egresos Totalizados por Día Cliente
        [HttpGet]
        public ActionResult ReportEgresosTotalizadoxDiaCliente()
        {
            ReportEgresosTotalizadoXDiaClienteModel model;
            model = TempData["ReportEgresosTotalizadoXDiaClienteModel"] == null ? new ReportEgresosTotalizadoXDiaClienteModel() : TempData["ReportEgresosTotalizadoXDiaClienteModel"] as ReportEgresosTotalizadoXDiaClienteModel;
            model.ListCliente = DbServices.GetClientesxCodigoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_EgresosTotalizadoxDiaCliente(model.selectDateFrom, model.selectDateTo, model.idCliente);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportEgresosTotalizadoxDiaCliente(ReportEgresosTotalizadoXDiaClienteModel model)
        {
            model.ListCliente = DbServices.GetClientesxCodigoToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_EgresosTotalizadoxDiaCliente(model.selectDateFrom, model.selectDateTo, model.idCliente);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerarPDFReport_EgresosTotalizadoxDiaCliente(DateTime DateFrom, DateTime DateTo, string idCliente, string cliente)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_EgresosTotalizadoxDiaCliente(DateFrom, DateTo, idCliente, cliente);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteEgresosTotalizaddoxDiaCliente.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_EgresosTotalizadoxDiaCliente(DateTime DateFrom, DateTime DateTo, string idCliente, string cliente)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_EgresosTotalizadoxDiaCliente(DateFrom, DateTo, idCliente, cliente);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteEgresosTotalizadoxDiaCliente.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Egresos con Saldo
        [HttpGet]
        public ActionResult ReportEgresosSaldo()
        {
            ReportEgresoSaldosModel model;
            model = TempData["ReportEgresoSaldosModel"] == null ? new ReportEgresoSaldosModel() : TempData["ReportEgresoSaldosModel"] as ReportEgresoSaldosModel;
            model.ListCliente = DbServices.GetClientesToListSelectListItem("",new SelectListItem() { Value="",Text="Todos"});
            model.DatTable = DbServices.GetConsultaRepote_EgresosConSaldos(model.selectDateFrom, model.selectDateTo, model.lote, model.cliente, model.comprobantePedido);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportEgresosSaldo(ReportEgresoSaldosModel model)
        {
            model.ListCliente = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.DatTable = DbServices.GetConsultaRepote_EgresosConSaldos(model.selectDateFrom, model.selectDateTo, model.lote, model.cliente, model.comprobantePedido);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerarPDFReport_EgresosSaldos(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote, string nombreCliente)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_EgresosSaldo(cliente, DateFrom, DateTo, comprobantePedido, lote, nombreCliente);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteEgresosConSaldo.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_EgresosSaldos(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string lote, string nombreCliente)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_EgresosSaldos(cliente, DateFrom, DateTo, comprobantePedido, lote, nombreCliente);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteEgresosConSaldo.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion
        #region Reporte Insumos Egresos Detallado
        [HttpGet]
        public ActionResult ReportInsumosEgresosDetallado()
        {
            ReportInsumosEgresosDetalladoModel model;
            model = TempData["ReportInsumosEgresosDetalladoModel"] == null ? new ReportInsumosEgresosDetalladoModel() : TempData["ReportInsumosEgresosDetalladoModel"] as ReportInsumosEgresosDetalladoModel;
            model.ListClientes = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_InsumosEgresosDetallado(model.selectDateFrom, model.selectDateTo, model.comprobantePedido, model.cliente, Convert.ToInt32(model.idPrdInsumo));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportInsumosEgresosDetallado(ReportInsumosEgresosDetalladoModel model)
        {
            model.ListClientes = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_InsumosEgresosDetallado(model.selectDateFrom, model.selectDateTo, model.comprobantePedido, model.cliente, Convert.ToInt32(model.idPrdInsumo));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerarPDFReport_EgresosInsumosDetallado(DateTime DateFrom, DateTime DateTo, string comprobantePedido, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_EgresosInsumosDetallado(DateFrom, DateTo, comprobantePedido, cliente, idPrdInsumo, nombreCliente, Insumo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteEgresosInsumosDetallado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_EgresosInsumosDetallado(DateTime DateFrom, DateTime DateTo, string comprobantePedido, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_EgresosInsumosDetallado(DateFrom, DateTo, comprobantePedido, cliente, idPrdInsumo, nombreCliente, Insumo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteEgresosInsumosDetallado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Egresos Insumo Totalizado
        [HttpGet]
        public ActionResult ReportInsumosEgresosTotalizado()
        {
            ReportInsumosEgresosTotalizadosModel model;
            model = TempData["ReportInsumosEgresosTotalizadosModel"] == null ? new ReportInsumosEgresosTotalizadosModel() : TempData["ReportInsumosEgresosTotalizadosModel"] as ReportInsumosEgresosTotalizadosModel;
            model.ListClientes = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_InsumosEgresosTotalizado(model.selectDateFrom, model.selectDateTo, model.cliente, Convert.ToInt32(model.idPrdInsumo));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportInsumosEgresosTotalizado(ReportInsumosEgresosTotalizadosModel model)
        {
            model.ListClientes = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_InsumosEgresosTotalizado(model.selectDateFrom, model.selectDateTo, model.cliente, Convert.ToInt32(model.idPrdInsumo));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_EgresosInsumosTotlizado(DateTime DateFrom, DateTime DateTo, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_EgresosInsumosTotalizado(DateFrom, DateTo, cliente, idPrdInsumo, nombreCliente, Insumo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteEgresosInsumosTotalizado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_EgresosInsumosTotalizado(DateTime DateFrom, DateTime DateTo, string cliente, int idPrdInsumo, string nombreCliente, string Insumo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_EgresosInsumosTotalizado(DateFrom, DateTo, cliente, idPrdInsumo, nombreCliente, Insumo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteEgresosInsumosTotalizado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Repote Devoluciones
        [HttpGet]
        public ActionResult ReportDevoluciones()
        {
            ReportDevolucionesModel model;
            model = TempData["ReportDevolucionesModel"] == null ? new ReportDevolucionesModel() : TempData["ReportDevolucionesModel"] as ReportDevolucionesModel;
            model.ListClientes = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.DatTable = DbServices.GetConsultaRepote_Devoluciones(model.selectDateFrom, model.selectDateFrom, model.cliente, model.comprobantePedido);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportDevoluciones(ReportDevolucionesModel model)
        {
            model.ListClientes = DbServices.GetClientesToListSelectListItem("", new SelectListItem() { Value = "", Text = "Todos" });
            model.DatTable = DbServices.GetConsultaRepote_Devoluciones(model.selectDateFrom, model.selectDateFrom, model.cliente, model.comprobantePedido);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_Devoluciones(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string nombreCliente)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_Devoluciones(cliente, DateFrom, DateTo, comprobantePedido, nombreCliente);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteDevoluciones.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_Devoluciones(string cliente, DateTime DateFrom, DateTime DateTo, string comprobantePedido, string nombreCliente)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_Devoluciones(cliente, DateFrom, DateTo, comprobantePedido, nombreCliente);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteDevoluciones.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Producto Requeridos para Preparacion Pedidos
        [HttpGet]
        public ActionResult ReportProdReqPrepPedidos()
        {
            ReportProdReqPrepPedidosModel model;
            model = TempData["ReportProdReqPrepPedidosModel"] == null ? new ReportProdReqPrepPedidosModel() : TempData["ReportProdReqPrepPedidosModel"] as ReportProdReqPrepPedidosModel;
            model.DatTable = DbServices.GetConsultaReporte_PedidosReqPreparaciionPedidos(model.selectDateFrom, model.selectDateTo);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportProdReqPrepPedidos(ReportProdReqPrepPedidosModel model)
        {
            model.DatTable = DbServices.GetConsultaReporte_PedidosReqPreparaciionPedidos(model.selectDateFrom, model.selectDateTo);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ProdReqPrepPedidos(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ProdReqPrepPedidos(DateFrom, DateTo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reportProdReqArmadoPedidos.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerareXCELReport_ProdReqPrepPedidos(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ProdReqPrepPedidos(DateFrom, DateTo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reportProdReqArmadoPedidos.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Existencia Stock Detallado
        [HttpGet]
        public ActionResult ReportExsistenciaStockDetalle()
        {
            ReportExistenciaStockDetalleModel model;
            model = TempData["ReportExistenciaStockDetalleModel"] == null ? new ReportExistenciaStockDetalleModel() : TempData["ReportExistenciaStockDetalleModel"] as ReportExistenciaStockDetalleModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockDetalle(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockDetalle(ReportExistenciaStockDetalleModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockDetalle(model.selectDateTo,Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockDetalle(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockDetalle(DateTo ,idTipoProducto, idProducto, idUbicacion, TipoProducto, Producto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockDetallado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockDetalle(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockDetalle(DateTo, idProducto, idTipoProducto, idUbicacion, Producto, TipoProducto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStockDetallado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Existencia Stock Totalizado
        [HttpGet]
        public ActionResult ReportExsistenciaStockTotalizado()
        {
            ReportExistenciaStockTotalizadoModel model;
            model = TempData["ReportExistenciaStockTotalizadoModel"] == null ? new ReportExistenciaStockTotalizadoModel() : TempData["ReportExistenciaStockTotalizadoModel"] as ReportExistenciaStockTotalizadoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockTotalizado(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockTotalizado(ReportExistenciaStockTotalizadoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockTotalizado(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockTotalizado(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockTotalizado(DateTo, idTipoProducto, idProducto, idUbicacion, TipoProducto, Producto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockTotalizado.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockTotalizado(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockTotalizado(DateTo, idProducto, idTipoProducto, idUbicacion, Producto, TipoProducto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStocktotalizado.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Existencia Stock Totalizado Ubicación

        [HttpGet]
        public ActionResult ReportExsistenciaStockTotalizadoDestino()
        {
            ReportExistenciaStockTotalizadoDestinoModel model;
            model = TempData["ReportExistenciaStockTotalizadoDestino"] == null ? new ReportExistenciaStockTotalizadoDestinoModel() : TempData["ReportExistenciaStockTotalizadoDestino"] as ReportExistenciaStockTotalizadoDestinoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockTotalizadoDestino(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockTotalizadoDestino(ReportExistenciaStockTotalizadoDestinoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockTotalizadoDestino(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockTotalizadoDestino(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockTotalizadoDestino(DateTo, idTipoProducto, idProducto, idUbicacion, TipoProducto, Producto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockTotalizadoDestino.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockTotalizadoDestino(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockTotalizadoDestino(DateTo, idProducto, idTipoProducto, idUbicacion, Producto, TipoProducto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStocktotalizadoDestino.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Existencia en Stock Detallado por Vencimiento
        [HttpGet]
        public ActionResult ReportExsistenciaStockDetalleVencimiento()
        {
            ReportExistenciaStockDetalleVencimientoModel model;
            model = TempData["ReportExistenciaStockDetalleVencimientoModel"] == null ? new ReportExistenciaStockDetalleVencimientoModel() : TempData["ReportExistenciaStockDetalleVencimientoModel"] as ReportExistenciaStockDetalleVencimientoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockDetalleVencimiento(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockDetalleVencimiento(ReportExistenciaStockDetalleVencimientoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockDetalleVencimiento(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockDetalleVencimiento(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockDetalleVencimiento(DateTo, idTipoProducto, idProducto, idUbicacion, TipoProducto, Producto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockDetalleVcto.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockDetalleVencimiento(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockDetalleVencimiento(DateTo, idProducto, idTipoProducto, idUbicacion, Producto, TipoProducto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStockDetalleVcto.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Existencia en Stock Detalle con Proximidad de Vencimiento
        [HttpGet]
        public ActionResult ReportExsistenciaStockDetalleProxVencimiento()
        {
            ReportExistenciaStockProxVencimientoModel model;
            model = TempData["ReportExistenciaStockProxVencimientoModel"] == null ? new ReportExistenciaStockProxVencimientoModel() : TempData["ReportExistenciaStockProxVencimientoModel"] as ReportExistenciaStockProxVencimientoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockDetalleProxVencimiento(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion), Convert.ToInt32(model.diasProximidadVencimiento));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockDetalleProxVencimiento(ReportExistenciaStockProxVencimientoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockDetalleProxVencimiento(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion), Convert.ToInt32(model.diasProximidadVencimiento));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockDetalleProxVencimiento(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion, int diasProximidadVencimiento)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockDetalleProxVencimiento(DateTo, idTipoProducto, idProducto, idUbicacion, TipoProducto, Producto, Ubicacion, diasProximidadVencimiento);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockDetalleProximidadVcto.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockDetalleProxVencimiento(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion, int diasProximidadVencimiento)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockDetalleProxVencimiento(DateTo, idProducto, idTipoProducto, idUbicacion, Producto, TipoProducto, Ubicacion, diasProximidadVencimiento);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStockDetalleProximidadVcto.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Existencia en Stock Contenedores Total por Ubicación
        [HttpGet]
        public ActionResult ReportExsistenciaStockContenedoresTotalDestino()
        {
            ReportExistenciaStockContenedoresTotalPorDestinoModel model;
            model = TempData["ReportExistenciaStockContenedoresTotalPorDestinoModel"] == null ? new ReportExistenciaStockContenedoresTotalPorDestinoModel() : TempData["ReportExistenciaStockContenedoresTotalPorDestinoModel"] as ReportExistenciaStockContenedoresTotalPorDestinoModel;
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem();
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockContenedoresTotalDestino(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockContenedoresTotalDestino(ReportExistenciaStockContenedoresTotalPorDestinoModel model)
        {
            model.ListTipoProductos = DbServices.GetTipoProductosToListSelectListItem();
            model.ListProductos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(model.idTipoProducto));
            model.ListUbicacion = DbServices.GetDestinosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockContenedoresTotalDestino(model.selectDateTo, Convert.ToInt32(model.idTipoProducto), Convert.ToInt32(model.idProducto), Convert.ToInt32(model.idUbicacion));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockContenedoresTotalDestino(DateTime DateTo, int idProducto, int idTipoProducto, int idUbicacion, string Producto, string TipoProducto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockContenedoresTotalDestino(DateTo, idTipoProducto, idProducto, idUbicacion, TipoProducto, Producto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockContenedoresTotalDestino.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockContenedoresTotalDestino(DateTime DateTo, int idTipoProducto, int idProducto, int idUbicacion, string TipoProducto, string Producto, string Ubicacion)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockContenedoresTotalDestino(DateTo, idProducto, idTipoProducto, idUbicacion, Producto, TipoProducto, Ubicacion);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStockContenedoresTotalDestino.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Existencia Stpck Insumos
        [HttpGet]
        public ActionResult ReportExsistenciaStockInsumos()
        {
            ReportExistenciaStockInsumosModel model;
            model = TempData["ReportExistenciaStockInsumosModel"] == null ? new ReportExistenciaStockInsumosModel() : TempData["ReportExistenciaStockInsumosModel"] as ReportExistenciaStockInsumosModel;
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockInsumos(model.selectDateTo, Convert.ToInt32(model.idPrdInsumo));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportExsistenciaStockInsumos(ReportExistenciaStockInsumosModel model)
        {
            model.ListInsumos = DbServices.GetInsumosToListSelectListItem();
            model.DatTable = DbServices.GetConsultaRepote_ExistenciaStockInsumos(model.selectDateTo, Convert.ToInt32(model.idPrdInsumo));
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_ExistenciaStockInsumos(DateTime DateTo, int idPrdInsumo, string Insumo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ExistenciaStockInsumos(DateTo, idPrdInsumo, Insumo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteExistenciaStockInsumos.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_ExistenciaStockInsumos(DateTime DateTo, int idPrdInsumo, string Insumo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ExistenciaStockInsumos(DateTo, idPrdInsumo, Insumo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteExistenciaStockInsumos.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        #endregion

        #region Trazabilidad por Pieza
        [HttpGet]
        public ActionResult ReportTrazabilidadPieza()
        {
            ReportTrazabilidadPiezaModel model;
            model = TempData["ReportTrazabilidadPiezaModel"] == null ? new ReportTrazabilidadPiezaModel() : TempData["ReportTrazabilidadPiezaModel"] as ReportTrazabilidadPiezaModel;
            model.DatTable = DbServices.GetConsultaRepote_TrazabilidadPieza(model.numPieza);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportTrazabilidadPieza(ReportTrazabilidadPiezaModel model)
        {
            model.DatTable = DbServices.GetConsultaRepote_TrazabilidadPieza(model.numPieza);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_TrazabilidadPieza(int numPieza)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_TrazabilidadPieza(numPieza);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteTrazabilidadPieza.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_TrazabilidadPieza(int numPieza)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_TrazabiliadPieza(numPieza);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteTrazabilidadPieza.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Trazabilidad por Lote
        [HttpGet]
        public ActionResult ReportTrazabilidadLote()
        {
            ReportTrazabilidadLoteModel model;
            model = TempData["ReportTrazabilidadLoteModel"] == null ? new ReportTrazabilidadLoteModel() : TempData["ReportTrazabilidadLoteModel"] as ReportTrazabilidadLoteModel;
            model.DatTable = DbServices.GetConsultaRepote_TrazabilidadLote(model.lote);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportTrazabilidadLote(ReportTrazabilidadLoteModel model)
        {
            try
            {
                model.DatTable = DbServices.GetConsultaRepote_TrazabilidadLote(model.lote);
                TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
                return View(model);
            }
            catch (FormatException)
            {
                TempData["MessageStatus"] = "Incorrecto valor del numero de Lote !!!";
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_TrazabilidadLote(string lote)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            try
            {
                ms = ReportServices.GenerarPDFReport_TrazabilidadLote(lote);
                if (ms != null)
                {
                    string handle = Guid.NewGuid().ToString();
                    Session[handle] = ms.ToArray();
                    return new JsonResult()
                    {
                        Data = new
                        {
                            Success = true,
                            Error = "",
                            FileGuid = handle,
                            MimeType = "application/pdf",
                            FileName = "reporteTrazabilidadLote.pdf"
                        }
                    };
                }
                else
                {
                    return new JsonResult()
                    {
                        Data = new
                        {
                            Success = false,
                            Error = "No se han obtenido datos"
                        }
                    };
                }
            }
            catch (FormatException)
            {
                return new JsonResult()
                {
                    Data = new { Success = false, Error = "Incorrecto valor del numero de Lote !!!" }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_TrazabilidadLote(string lote)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            try
            {
                ms = ReportServices.GenerarExcelReport_TrazabiliadLote(lote);
                if (ms != null)
                {
                    string handle = Guid.NewGuid().ToString();
                    Session[handle] = ms.ToArray();
                    return new JsonResult()
                    {
                        Data = new
                        {
                            Success = true,
                            Error = "",
                            FileGuid = handle,
                            MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            FileName = "reporteTrazabilidadLote.xlsx"
                        }
                    };
                }
                else
                {
                    return new JsonResult()
                    {
                        Data = new
                        {
                            Success = false,
                            Error = "No se han obtenido datos"
                        }
                    };
                }
            }
            catch (FormatException)
            {
                return new JsonResult()
                {
                    Data = new { Success = false, Error = "Incorrecto valor del numero de Lote !!!" }
                };
            }
        }
        #endregion

        #region Histórico Piezas Contenedor

        [HttpGet]
        public ActionResult ReportHistoricoPiezaContenedor()
        {
            ReportHistoricoPiezaContenedorModel model;
            model = TempData["ReportHistoricoPiezaContenedorModel"] == null ? new ReportHistoricoPiezaContenedorModel() : TempData["ReportHistoricoPiezaContenedorModel"] as ReportHistoricoPiezaContenedorModel;
            model.DatTable = DbServices.GetConsultaReporte_HistoricoPiezaContenedor(Convert.ToInt32(model.numPieza), Convert.ToInt32(model.esContenedor));
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportHistoricoPiezaContenedor(ReportHistoricoPiezaContenedorModel model)
        {
            try
            {
                model.DatTable = DbServices.GetConsultaReporte_HistoricoPiezaContenedor(Convert.ToInt32(model.numPieza), Convert.ToInt32(model.esContenedor));
                TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
                return View(model);
            }
            catch (FormatException)
            {
                TempData["MessageStatus"] = "Incorrecto valor del numero de Lote !!!";
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult GenerarPDFReport_HistoricoPiezaContenedor(int numPieza, int esContenedor, string TipoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_HistoricoPiezaContenedor(numPieza, esContenedor, TipoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteHistoricoPiezaContenedor.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult GenerarExcelReport_HistoricoPiezaContenedor(int numPieza, int esContenedor, string TipoBulto)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_HistoricoPiezaContenedor(numPieza, esContenedor, TipoBulto);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteHistoricoPiezaContenedor.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }
        #endregion

        #region Reporte Resultado Inventario

        [HttpGet]
        public ActionResult ReportResultInventario()
        {
            ReportResultInventarioModel model;
            model = TempData["ReportResultInventarioModel"] == null ? new ReportResultInventarioModel() : TempData["ReportResultInventarioModel"] as ReportResultInventarioModel;
            model.DatTable = DbServices.GetConsultaReporte_ResultInventario(model.selectDateFrom, model.selectDateTo);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportResultInventario(ReportResultInventarioModel model)
        {
            model.DatTable = DbServices.GetConsultaReporte_ResultInventario(model.selectDateFrom, model.selectDateTo);
            TempData["MessageStatus"] = model.DatTable == null || model.DatTable.Rows.Count == 0 ? "La consulta no generó resultados !!." : "";
            return View(model);
        }

        public ActionResult GenerarPDFReport_ResultadoInventario(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarPDFReport_ResultadoInventario(DateFrom, DateTo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/pdf",
                        FileName = "reporteResultInventario.pdf"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        public ActionResult GenerarExcelReport_ResultadoInventario(DateTime DateFrom, DateTime DateTo)
        {
            MemoryStream ms;
            TempData["MessageStatus"] = "";
            ms = ReportServices.GenerarExcelReport_ResultadoInventario(DateFrom, DateTo);
            if (ms != null)
            {
                string handle = Guid.NewGuid().ToString();
                Session[handle] = ms.ToArray();
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = "",
                        FileGuid = handle,
                        MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        FileName = "reporteResultadoInventario.xlsx"
                    }
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = false,
                        Error = "No se han obtenido datos"
                    }
                };
            }
        }

        #endregion


        [HttpGet]
        public JsonResult CargarListProducto(string tipoProducto)
        {
            var productos = DbServices.GetProductosToListSelectListItem(Convert.ToInt32(tipoProducto));
            return Json(productos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download(string fileGuid, string mimeType, string filename)
        {
            if (Session[fileGuid] != null)
            {
                byte[] data = Session[fileGuid] as byte[];
                Session.Remove(fileGuid);  // Cleanup session data
                return File(data, mimeType, filename);
            }
            else
            {
                // Log the error if you want
                return new EmptyResult();
            }
        }
    }
}