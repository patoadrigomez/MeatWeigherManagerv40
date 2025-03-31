using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReportMWM.services;
using WebReportMWM.Models.Entitys;


namespace WebReportMWM.Controllers
{
    [Authorize(Roles = "S")]
    public class CrudMovInsumosController : Controller
    {
        // GET: CrudMovInsumos
        [HttpGet]
        public ActionResult List(int? idRowSelect)
        {
            if (idRowSelect == null)
                idRowSelect = 0;

            var stockInsunos = DbServices.GetStockInsumos();
            ViewBag.idRowSelect = idRowSelect;
            return View(stockInsunos);
        }

        [HttpPost]
        public ActionResult Ajustar(StockInsumo model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                MovInsumo movInsumo = new MovInsumo();
                DbLog dbLog = new DbLog();
                if (model.Ajustar != model.Unds)
                {
                    try
                    {
                        movInsumo.IdTipoMov = model.Ajustar > model.Unds ? "ING" : "EGR";
                        movInsumo.IdTipoProc = "AJU";
                        movInsumo.IdProc = context.MovInsumos.Where(x => x.idPrdInsumo == model.Id && x.IdTipoMov == movInsumo.IdTipoMov && x.IdTipoProc == movInsumo.IdTipoProc).Count() + 1;
                        movInsumo.idPrdInsumo = model.Id;
                        movInsumo.Unidades = Math.Abs(model.Ajustar - model.Unds);
                        movInsumo.Fecha_Hora = DateTime.Now;
                        context.MovInsumos.Add(movInsumo);
                        context.SaveChanges();
                    } catch(Exception e)
                    {
                        return new JsonResult()
                        {
                            Data = new
                            {
                                Success = false,
                                Error = e
                            }
                        };
                    }
                   
                    //A continuación se registra el ajuste en la tabla de log de eventos
                    try
                    {
                        int idOperador = ((Operadores)Session["LoggerUser"]).Id;
                        string evento = "SE REALIZÓ AJUSTE MANUAL DE STOCK PARA INSUMO";
                        string contexto = "AJUSTE DE STOCK DE INSUMOS";
                        string detalle = "Se ajustó el stock del insumo: " + model.Insumo;
                        bool logRegistrado = DbServices.Registrar_Log_Evento(idOperador, evento, contexto, detalle);
                        if (!logRegistrado) TempData["MessagesError"] = "No se pudo registrar el evento";
                    }
                    catch (Exception e)
                    {
                        //throw (e);
                        return new JsonResult()
                        {
                            Data = new
                            {
                                Success = false,
                                Error = e
                            }
                        };
                    }
                }
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = ""
                    }
                };
            }
        }

    }
}