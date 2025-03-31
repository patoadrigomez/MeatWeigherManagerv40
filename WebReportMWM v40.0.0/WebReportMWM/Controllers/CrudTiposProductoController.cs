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
    public class CrudTiposProductoController : Controller
    {
        // GET: CrudTiposProducto

        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                TipoProducto model = new TipoProducto();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(TipoProducto model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                TipoProducto newTipoProducto = new TipoProducto();
                newTipoProducto.Nombre = model.Nombre;

                ResultValidate resultValidation = DbServices.ValidateCreate_TipoProducto(newTipoProducto);
                if (resultValidation.Validated)
                {
                    context.TiposProducto.Add(newTipoProducto);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newTipoProducto.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newTipoProducto);
                }
            }
        }


        [HttpGet]
        public ActionResult List(int? idRowSelect)
        {
            if (idRowSelect == null)
                idRowSelect = 0;
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from tp in context.TiposProducto
                            select new
                            {
                                Id = tp.Id,
                                Nombre = tp.Nombre,
                            }).AsEnumerable().Select(x => new TipoProducto()
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                            }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                return View(data);
            }
        }

        public ActionResult Update(int idTipoProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.TiposProducto.Where(x => x.Id == idTipoProducto).SingleOrDefault();
                TipoProducto model = new TipoProducto()
                {
                    Id = date.Id,
                    Nombre = date.Nombre,
                };
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(TipoProducto model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.TiposProducto.FirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.Nombre = model.Nombre;
                }

                ResultValidate resultValidation = DbServices.ValidateUpdate_TipoProducto(data);
                if (resultValidation.Validated)
                {
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = data.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = false;
                    return View("UpdateCreate", data);
                }
            }
        }

        public ActionResult ConfirmDelete(int idTipoProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.TiposProducto.FirstOrDefault(x => x.Id == idTipoProducto);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idTipoProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.TiposProducto.FirstOrDefault(x => x.Id == idTipoProducto);
                if (data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_TipoProducto(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.TiposProducto.Remove(data);
                        context.SaveChanges();
                        return RedirectToAction("List");
                    }
                    else
                    {
                        TempData["MessagesError"] = resultValidation.ErrorMessages;
                        return View("ConfirmDelete", data);
                    }
                }
                else
                {
                    ViewBag.Message = "En base de datos: No se ha podido eliminar el Tipo de Producto.";
                    return View("Error");
                }
            }
        }
    }
}