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
    public class CrudSectoresController : Controller
    {
        // GET: CrudSectores

        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                Sector model = new Sector();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Sector model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Sector newSector = new Sector();
                newSector.Nombre = model.Nombre;

                ResultValidate resultValidation = DbServices.ValidateCreate_Sector(newSector);
                if (resultValidation.Validated)
                {
                    context.Sectores.Add(newSector);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newSector.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newSector);
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
                var data = (from d in context.Sectores
                            select new
                            {
                                Id = d.Id,
                                Nombre = d.Nombre,
                            }).AsEnumerable().Select(x => new Sector()
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                            }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                return View(data);
            }
        }

        public ActionResult Update(int idSector)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.Sectores.Where(x => x.Id == idSector).SingleOrDefault();
                Sector model = new Sector()
                {
                    Id = date.Id,
                    Nombre = date.Nombre,
                };
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Sector model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Sectores.FirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.Nombre = model.Nombre;
                }

                ResultValidate resultValidation = DbServices.ValidateUpdate_Sector(data);
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

        public ActionResult ConfirmDelete(int idSector)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Sectores.FirstOrDefault(x => x.Id == idSector);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idSector)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Sectores.FirstOrDefault(x => x.Id == idSector);
                if (data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_Sector(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.Sectores.Remove(data);
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