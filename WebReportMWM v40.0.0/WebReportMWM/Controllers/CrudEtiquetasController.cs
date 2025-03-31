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
    public class CrudEtiquetasController : Controller
    {
        // GET: CrudEtiquetas

        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                Etiqueta model = new Etiqueta();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Etiqueta model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Etiqueta newEtiqueta = new Etiqueta();
                newEtiqueta.Nombre = model.Nombre;
                newEtiqueta.Descripcion = model.Descripcion;
                newEtiqueta.IdTipoBulto = model.IdTipoBulto;

                ResultValidate resultValidation = DbServices.ValidateUpdateCreate_Etiqueta(newEtiqueta);
                if (resultValidation.Validated)
                {
                    context.Etiquetas.Add(newEtiqueta);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newEtiqueta.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newEtiqueta);
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
                var data = (from e in context.Etiquetas
                            join tb in context.TiposBulto
                            on e.IdTipoBulto equals tb.Id
                            select new
                            {
                                Id = e.Id,
                                Nombre = e.Nombre,
                                Descripcion = e.Descripcion,
                                IdTipoBulto = e.IdTipoBulto,
                                TipoBulto = tb.Nombre
                            }).AsEnumerable().Select(x => new Etiqueta()
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                                Descripcion = x.Descripcion,
                                IdTipoBulto = x.IdTipoBulto,
                                TipoBulto    = x.TipoBulto
                            }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                return View(data);
            }
        }

        public ActionResult Update(int idEtiqueta)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.Etiquetas.Where(x => x.Id == idEtiqueta).SingleOrDefault();
                Etiqueta model = new Etiqueta()
                {
                    Id = date.Id,
                    Nombre = date.Nombre,
                    Descripcion = date.Descripcion,
                    IdTipoBulto = date.IdTipoBulto
                };
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Etiqueta model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Etiquetas.FirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.Nombre = model.Nombre;
                    data.Descripcion = model.Descripcion;
                    data.IdTipoBulto = model.IdTipoBulto;
                }

                ResultValidate resultValidation = DbServices.ValidateUpdateCreate_Etiqueta(model);
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

        public ActionResult ConfirmDelete(int idEtiqueta)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Etiquetas.FirstOrDefault(x => x.Id == idEtiqueta);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idEtiqueta)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Etiquetas.FirstOrDefault(x => x.Id == idEtiqueta);
                if (data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_Etiqueta(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.Etiquetas.Remove(data);
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
                    ViewBag.Message = "En base de datos: No se ha podido eliminar el Operador.";
                    return View("Error");
                }
            }
        }
    }
}