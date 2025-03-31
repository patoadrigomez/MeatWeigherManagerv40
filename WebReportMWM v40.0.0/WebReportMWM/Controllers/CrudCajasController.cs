using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReportMWM.Models;
using WebReportMWM.Models.Entitys;
using WebReportMWM.services;

namespace WebReportMWM.Controllers
{
    [Authorize(Roles = "S")]
    public class CrudCajasController : Controller
    {
        [HttpGet]
        public ActionResult List(int? idRowSelect)
        {
            if (idRowSelect == null)
                idRowSelect = 0;
            ViewBag.IdRowSelect = idRowSelect;
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pc in context.Productos
                            join c in context.Cajas on pc.Id equals c.IdProductoCaja into tmpc
                            from fullc in tmpc.DefaultIfEmpty()
                            join p in context.Productos on fullc.IdProducto equals p.Id into tmpp
                            from fullp in tmpp.DefaultIfEmpty()
                            where pc.EsCaja == true
                            select new
                            {
                                IdProductoCaja = pc.Id,
                                NombreCaja = pc.Nombre,
                                IdProducto = (int?)fullp.Id ?? 0,
                                NombreProducto = fullp.Nombre ?? ""
                            }).AsEnumerable().Select(x => new Caja()
                            {
                                IdProductoCaja = x.IdProductoCaja,
                                NombreCaja = x.NombreCaja,
                                IdProducto = x.IdProducto,
                                NombreProducto = x.NombreProducto
                            }).ToList();
                return View(data);
            }
        }

        public ActionResult AddChangeProducto(int IdProductoCaja, int IdProducto, string NombreCaja, string NombreProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Caja caja = new Caja()
                {
                    IdProductoCaja = IdProductoCaja,
                    IdProducto = IdProducto,
                    NombreCaja = NombreCaja,
                    NombreProducto = NombreProducto,
                };
                return View(caja);
            }
        }

        [HttpPost]
        public ActionResult AddChangeProducto(Caja caja)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.Cajas.FirstOrDefault(x => x.IdProductoCaja == caja.IdProductoCaja);
                if (date != null)
                {
                    date.IdProducto = caja.IdProducto;
                }
                else
                {
                    Caja newCaja = new Caja()
                    {
                        IdProductoCaja = caja.IdProductoCaja,
                        IdProducto = caja.IdProducto,
                    };
                    context.Cajas.Add(newCaja);
                }
                context.SaveChanges();
                return RedirectToAction("List", new  { IdRowSelect = caja.IdProductoCaja });
            }
        }

        public ActionResult ConfirmDelete(int IdProductoCaja, int IdProducto, string NombreCaja)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var productoEnCaja = context.Productos.FirstOrDefault(x => x.Id == IdProducto);
                var data = new Caja();
                data.IdProductoCaja = IdProductoCaja;
                data.IdProducto = IdProducto;
                data.NombreCaja = NombreCaja;
                data.NombreProducto = productoEnCaja.Nombre;
                ViewBag.IdProductoCaja = IdProductoCaja;
                ViewBag.NombreCaja = NombreCaja;
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult ConfirmDelete(Caja model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Cajas.FirstOrDefault(x => x.IdProductoCaja == model.IdProductoCaja);
                if(data != null)
                {
                    context.Cajas.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("List", new { IdRowSelect = data.IdProductoCaja });
                } else
                {
                    ViewBag.Message = "En base de datos: No se ha podido eliminar el Producto contenido.";
                    return View("Error");
                }
            }
        }
    }
}