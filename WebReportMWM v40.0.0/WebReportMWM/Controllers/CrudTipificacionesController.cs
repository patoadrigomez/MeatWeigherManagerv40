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
    public class CrudTipificacionesController : Controller
    {
        // GET: CrudTipificaciones
        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                Tipificacion model = new Tipificacion();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Tipificacion model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Tipificacion newTipificacion = new Tipificacion();
                newTipificacion.Nombre = model.Nombre;

                ResultValidate resultValidation = DbServices.ValidateUpdateCreate_Tipificacion(newTipificacion);
                if (resultValidation.Validated)
                {
                    context.Tipificaciones.Add(newTipificacion);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newTipificacion.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newTipificacion);
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
                var data = (from t in context.Tipificaciones
                            select new
                            {
                                Id = t.Id,
                                Nombre = t.Nombre,
                            }).AsEnumerable().Select(x => new Tipificacion()
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                            }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                return View(data);
            }
        }

        public ActionResult Update(int idTipificacion)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.Tipificaciones.Where(x => x.Id == idTipificacion).SingleOrDefault();
                Tipificacion model = new Tipificacion()
                {
                    Id = date.Id,
                    Nombre = date.Nombre,
                };
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Tipificacion model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Tipificaciones.FirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.Nombre = model.Nombre;
                }

                ResultValidate resultValidation = DbServices.ValidateUpdateCreate_Tipificacion(model);
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

        public ActionResult ConfirmDelete(int idTipificacion)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Tipificaciones.FirstOrDefault(x => x.Id == idTipificacion);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idTipificacion)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Tipificaciones.FirstOrDefault(x => x.Id == idTipificacion);
                if (data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_Tipificacion(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.Tipificaciones.Remove(data);
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
                    ViewBag.Message = "";
                    return View("Error");
                }
            }
        }
    }
}