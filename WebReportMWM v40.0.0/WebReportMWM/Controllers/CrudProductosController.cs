using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebReportMWM.Models;
using WebReportMWM.Models.Entitys;
using WebReportMWM.services;
using static WebReportMWM.Models.Enum;

namespace WebReportMWM.Controllers
{
    [Authorize(Roles = "S")]
    public class CrudProductosController : Controller
    {
        // GET: CrudProductos
        public ActionResult Create()
        {
            using (var context = new DMMeatWeigherModel())
            {
                Producto model = new Producto();
                ViewBag.ModeCreate = true;
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Producto model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Producto newProducto = new Producto();
                newProducto.CodigoProductoSac = model.CodigoProductoSac;
                newProducto.Nombre = model.Nombre;
                newProducto.IdTipo = model.IdTipo;
                newProducto.NumSenasa = model.NumSenasa;
                newProducto.PesoNetoPredef = model.PesoNetoPredef;
                newProducto.UnidadesPredef = model.UnidadesPredef;
                newProducto.PesoTaraPredef = model.PesoTaraPredef;
                newProducto.DiasVencimiento = model.DiasVencimiento;
                newProducto.EsInsumo = model.EsInsumo;
                newProducto.EsPesable = model.EsPesable;
                newProducto.TextAuxL1 = model.TextAuxL1;
                newProducto.TextAuxL2 = model.TextAuxL2;
                newProducto.NombreL1 = model.NombreL1;
                newProducto.NombreL2 = model.NombreL2;
                newProducto.NombreL3 = model.NombreL3;
                newProducto.NombreL4 = model.NombreL4;
                newProducto.NombreL5 = model.NombreL5;
                newProducto.NombreL6 = model.NombreL6;
                newProducto.RendimientoSTD = model.RendimientoSTD;
                newProducto.EsCombo = model.EsCombo;
                newProducto.EsCaja = model.EsCaja;
                newProducto.EsTropa = model.EsTropa;
                newProducto.IdEtiqueta = model.IdEtiqueta;
                newProducto.TipoBulto = model.IdEtiqueta == null ? "" : context.Etiquetas.FirstOrDefault(x => x.Id == model.IdEtiqueta).IdTipoBulto;

                ResultValidate resultValidation = DbServices.ValidateCreate_Producto(newProducto);
                if (resultValidation.Validated)
                {
                    context.Productos.Add(newProducto);
                    context.SaveChanges();
                    return RedirectToAction("List", new { idRowSelect = newProducto.Id });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View("UpdateCreate", newProducto);
                }
            }
        }

        [HttpGet]
        public ActionResult List(int? idRowSelect, string filtroProductoNombre,string filtroProductoTipo)
        {
            if (idRowSelect == null)
                idRowSelect = 0;

            using (var context = new DMMeatWeigherModel())
            {

                var productosSAC = DbServices.GetProductosSacToList();
                var Etiquetas = context.Etiquetas.ToList();

                var data = (from p in context.Productos
                            join tp in context.TiposProducto on p.IdTipo equals tp.Id
                            where (String.IsNullOrEmpty(filtroProductoNombre) || p.Nombre.ToUpper().Contains(filtroProductoNombre.ToUpper())) &&
                                (String.IsNullOrEmpty(filtroProductoTipo) || tp.Nombre.ToUpper().Contains(filtroProductoTipo.ToUpper()))
                            orderby p.Nombre
                            select new
                            {
                                Id = p.Id,
                                CodigoProductoSac = p.CodigoProductoSac,
                                Nombre = p.Nombre,
                                IdTipo = tp.Id,
                                Tipo = tp.Nombre,
                                NumSenasa = p.NumSenasa,
                                PesoNetoPredef = p.PesoNetoPredef,
                                UnidadesPredef = p.UnidadesPredef,
                                PesoTaraPredef = p.PesoTaraPredef,
                                DiasVencimiento = p.DiasVencimiento,
                                EsInsumo = p.EsInsumo,
                                EsPesable = p.EsPesable,
                                TextAuxL1 = p.TextAuxL1,
                                TextAuxL2 = p.TextAuxL2,
                                NombreL1 = p.NombreL1,
                                NombreL2 = p.NombreL2,
                                NombreL3 = p.NombreL3,
                                NombreL4 = p.NombreL4,
                                NombreL5 = p.NombreL5,
                                NombreL6 = p.NombreL6,
                                RendimientoSTD = p.RendimientoSTD,
                                EsCombo = p.EsCombo,
                                EsCaja = p.EsCaja,
                                IdEtiqueta = p.IdEtiqueta,
                                EsTropa = p.EsTropa,
                            }).AsEnumerable().Select(x => new Producto()
                            {
                                Id = x.Id,
                                CodigoProductoSac = x.CodigoProductoSac,
                                NombreProductoSac = productosSAC.Where(y => y.Id.Trim() == (String.IsNullOrEmpty(x.CodigoProductoSac) ? "" : x.CodigoProductoSac.Trim())).FirstOrDefault()?.Nombre ?? "",
                                AliasProductoSAC = productosSAC.Where(y => y.Id.Trim() == (String.IsNullOrEmpty(x.CodigoProductoSac) ? "" : x.CodigoProductoSac.Trim())).FirstOrDefault()?.Alias ?? "",
                                Nombre = x.Nombre,
                                IdTipo = x.IdTipo,
                                Tipo = x.Tipo,
                                NumSenasa = x.NumSenasa,
                                PesoNetoPredef = x.PesoNetoPredef == null ? 0.00 : x.PesoNetoPredef,
                                UnidadesPredef = x.UnidadesPredef,
                                PesoTaraPredef = x.PesoTaraPredef == null ? 0.00 : x.PesoTaraPredef,
                                DiasVencimiento = x.DiasVencimiento,
                                EsInsumo=x.EsInsumo,
                                EsPesable= x.EsPesable,
                                TextAuxL1 = x.TextAuxL1,
                                TextAuxL2 = x.TextAuxL2,
                                NombreL1 = x.NombreL1,
                                NombreL2 = x.NombreL2,
                                NombreL3 = x.NombreL3,
                                NombreL4 = x.NombreL4,
                                NombreL5 = x.NombreL5,
                                NombreL6 = x.NombreL6,
                                RendimientoSTD = x.RendimientoSTD == null ? 0.00 : x.RendimientoSTD,
                                EsCombo = x.EsCombo == null ? false : x.EsCombo,
                                EsCaja= x.EsCaja == null ? false : x.EsCaja,
                                EsTropa = x.EsTropa == null ? false : x.EsTropa,
                                IdEtiqueta = x.IdEtiqueta == null ? 0 : x.IdEtiqueta,
                                NombreEtiqueta = x.IdEtiqueta == null ? "" : Etiquetas.Where(y => y.Id == x.IdEtiqueta).FirstOrDefault()?.Nombre,
                                TipoBulto = x.IdEtiqueta == null ? "" : Etiquetas.Where(y => y.Id == x.IdEtiqueta).FirstOrDefault()?.IdTipoBulto,
                            }).ToList();
                ViewBag.IdRowSelect = idRowSelect;
                ViewBag.FiltroProductoNombre = filtroProductoNombre;
                ViewBag.FiltroProductoTipo = filtroProductoTipo;
                return View(data);
            }
        }
        public ActionResult Update(int idProducto,string filtroProductoNombre,string filtroProductoTipo)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var date = context.Productos.Where(x => x.Id == idProducto).SingleOrDefault();
                Producto model = new Producto()
                {
                    Id = idProducto,
                    CodigoProductoSac = date.CodigoProductoSac,
                    Nombre = date.Nombre,
                    IdTipo = date.IdTipo,
                    NumSenasa = date.NumSenasa,
                    PesoNetoPredef = date.PesoNetoPredef,
                    UnidadesPredef = date.UnidadesPredef,
                    PesoTaraPredef = date.PesoTaraPredef,
                    DiasVencimiento = date.DiasVencimiento,
                    EsInsumo = date.EsInsumo,
                    EsPesable = date.EsPesable,
                    TextAuxL1 = date.TextAuxL1,
                    TextAuxL2 = date.TextAuxL2,
                    NombreL1 = date.NombreL1,
                    NombreL2 = date.NombreL2,
                    NombreL3 = date.NombreL3,
                    NombreL4 = date.NombreL4,
                    NombreL5 = date.NombreL5,
                    NombreL6 = date.NombreL6,
                    RendimientoSTD = date.RendimientoSTD,
                    EsCombo = date.EsCombo == null ? false : date.EsCombo,
                    EsCaja = date.EsCaja == null ? false : date.EsCaja,
                    EsTropa = date.EsTropa == null ? false : date.EsTropa,
                    IdEtiqueta = date.IdEtiqueta,
            };
                ViewBag.ModeCreate = false;
                ViewBag.FiltroProductoNombre = filtroProductoNombre;
                ViewBag.FiltroProductoTipo = filtroProductoTipo; 
                return View("UpdateCreate", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Producto model, string filtroProductoNombre, string filtroProductoTipo)
        {
            ResultValidate resultValidation;
            ViewBag.FiltroProductoNombre = filtroProductoNombre;
            ViewBag.FiltroProductoTipo = filtroProductoTipo;

            if (DbServices.Update_Producto(model, out resultValidation))
            {
                return RedirectToAction("List", new { idRowSelect = model.Id, filtroProductoNombre = filtroProductoNombre, filtroProductoTipo = filtroProductoTipo });
            }
            else
            {
                TempData["MessagesError"] = resultValidation.ErrorMessages;
                ViewBag.ModeCreate = false;
                return View("UpdateCreate", model);
            }
        }


        [HttpPost]
        public ActionResult UpdateFromWebGrid(Producto model)
        {
            ResultValidate resultValidation;
            if (DbServices.Update_Producto(model,out resultValidation))
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true,
                        Error = ""
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
                        Error = resultValidation?.ToString() ?? "ERROR"
                    }
                };
            }
        }

        public ActionResult ConfirmDelete(int idProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Productos.FirstOrDefault(x => x.Id == idProducto);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Productos.FirstOrDefault(x => x.Id == idProducto);
                if (data != null)
                {
                    var resultValidation = DbServices.ValidateDelete_Producto(data.Id);
                    if (resultValidation.Validated)
                    {
                        context.Productos.Remove(data);
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
                    ViewBag.Message = "En base de datos: No se ha podido eliminar el Producto.";
                    return View("Error");
                }
            }
        }


    }
}