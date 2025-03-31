using Newtonsoft.Json.Linq;
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
    public class CrudOperadoresController : Controller
    {
        // GET: CrudOperadores
        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                Operadores model = new Operadores();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Operadores model)
        {
            using(var context = new DMMeatWeigherModel())
            {
                Operadores newOperador = new Operadores();
                newOperador.Nombre = model.Nombre;
                newOperador.pasw = model.pasw;
                newOperador.Tipo = model.Tipo;

                ResultValidate resultValidation = DbServices.ValidateCreate_Operador(newOperador);
                if(resultValidation.Validated)
                {
                    context.operadores.Add(newOperador);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newOperador.Id });
                } else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newOperador);
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
                var data = (from o in context.operadores select new
                {
                        Id = o.Id,
                        Nombre = o.Nombre,
                        Password = o.pasw,
                        Tipo = o.Tipo == "S" ? "SUPERVISOR" : "USUARIO"
                    }).AsEnumerable().Select(x => new Operadores()
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        pasw = x.Password,
                        Tipo = x.Tipo
                    }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                return View(data);
            }
        }

        public ActionResult Update(int idOperador)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.operadores.Where(x => x.Id == idOperador).SingleOrDefault();
                Operadores model = new Operadores()
                {
                    Id = date.Id,
                    Nombre = date.Nombre,
                    pasw = date.pasw,
                    Tipo = date.Tipo,
                };
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Operadores model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.operadores.FirstOrDefault(x => x.Id == model.Id);
                if(data != null)
                {
                    data.Nombre = model.Nombre;
                    data.pasw = model.pasw;
                    data.Tipo = model.Tipo;
                }

                ResultValidate resultValidation = DbServices.ValidateUpdate_Operador(data.Id, data.Nombre);
                if (resultValidation.Validated)
                {
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = data.Id });
                } else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = false;
                    return View("UpdateCreate", data);
                }
            }
        }

        public ActionResult ConfirmDelete(int idOperador)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.operadores.FirstOrDefault(x => x.Id == idOperador);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idOperador)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.operadores.FirstOrDefault(x => x.Id == idOperador);
                if(data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_Operador(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.operadores.Remove(data);
                        context.SaveChanges();
                        return RedirectToAction("List");
                    } else
                    {
                        TempData["MessagesError"] = resultValidation.ErrorMessages;
                        return View("ConfirmDelete", data);
                    }
                } else
                {
                    ViewBag.Message = "En base de datos: No se ha podido eliminar el Operador.";
                    return View("Error");
                }
            }
        }
    }
}