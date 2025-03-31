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
    public class CrudDestinosController : Controller
    {
        // GET: CrudDestinos
        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                Destino model = new Destino();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Destino model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Destino newDestino = new Destino();
                newDestino.Nombre = model.Nombre;

                ResultValidate resultValidation = DbServices.ValidateCreate_Destino(newDestino);
                if (resultValidation.Validated)
                {
                    context.Destinos.Add(newDestino);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newDestino.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newDestino);
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
                var data = (from d in context.Destinos
                            select new
                            {
                                Id = d.Id,
                                Nombre = d.Nombre,
                            }).AsEnumerable().Select(x => new Destino()
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                            }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                return View(data);
            }
        }

        public ActionResult Update(int idDestino)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.Destinos.Where(x => x.Id == idDestino).SingleOrDefault();
                Destino model = new Destino()
                {
                    Id = date.Id,
                    Nombre = date.Nombre,
                };
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Destino model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Destinos.FirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.Nombre = model.Nombre;
                }

                ResultValidate resultValidation = DbServices.ValidateUpdate_Destino(data);
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

        public ActionResult ConfirmDelete(int idDestino)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Destinos.FirstOrDefault(x => x.Id == idDestino);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idDestino)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Destinos.FirstOrDefault(x => x.Id == idDestino);
                if (data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_Destino(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.Destinos.Remove(data);
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