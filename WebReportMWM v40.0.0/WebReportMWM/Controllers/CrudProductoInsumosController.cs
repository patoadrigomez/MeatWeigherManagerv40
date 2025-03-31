using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebReportMWM.Models;
using WebReportMWM.Models.Entitys;
using WebReportMWM.services;

namespace WebReportMWM.Controllers
{
    [Authorize(Roles = "S")]
    public class CrudProductoInsumosController : Controller
    {

        public ActionResult CreateAddInsumoPrimario(int idProducto, string NombreProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                ProductoInsumo model = new ProductoInsumo();
                model.IdProducto = idProducto;
                model.NombreProducto = NombreProducto;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CreateAddInsumoPrimario(ProductoInsumo model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                ProductoInsumo newProductoInsumo = new ProductoInsumo();
                newProductoInsumo.IdProducto = model.IdProducto;
                newProductoInsumo.NombreProducto = model.NombreProducto;
                newProductoInsumo.IdInsumoPrimario = model.IdInsumoPrimario;
                newProductoInsumo.IdInsumoSecundario = model.IdInsumoPrimario;
                newProductoInsumo.Unidades = model.Unidades;
                newProductoInsumo.RequiereConfirmacion = model.RequiereConfirmacion;
                ResultValidate resultValidation = DbServices.ValidateCreate_InsumoPrimario(newProductoInsumo);
                if (resultValidation.Validated)
                {
                    context.ProductoInsumos.Add(newProductoInsumo);
                    context.SaveChanges();
                    return RedirectToAction("DetailInsumosPrimarios", new
                    {
                        IdInsumoPrimarioRowSelect = newProductoInsumo.IdInsumoPrimario,
                        IdInsumoSecundarioRowSelect = newProductoInsumo.IdInsumoSecundario,
                        IdProducto = newProductoInsumo.IdProducto,
                        NombreProducto = newProductoInsumo.NombreProducto
                    });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    ViewBag.ModeCreate = true;
                    return View(newProductoInsumo);
                }
            }
        }

        public ActionResult CreateAddInsumoSecundario(int idProducto, string NombreProducto, int idInsumoPrimario, string NombreInsumoPrimario)
        {
            using (var context = new DMMeatWeigherModel())
            {
                ProductoInsumo model = new ProductoInsumo();
                model.IdProducto = idProducto;
                model.IdInsumoPrimario = idInsumoPrimario;
                model.NombreInsumoPrimario = NombreInsumoPrimario;
                model.NombreProducto = NombreProducto;
                ViewBag.ModeCreate = true;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CreateAddInsumoSecundario(ProductoInsumo model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                ProductoInsumo newProductoInsumo = new ProductoInsumo();
                newProductoInsumo.IdProducto = model.IdProducto;
                newProductoInsumo.IdInsumoPrimario = model.IdInsumoPrimario;
                newProductoInsumo.IdInsumoSecundario = model.IdInsumoSecundario;
                newProductoInsumo.Unidades = model.Unidades;
                newProductoInsumo.RequiereConfirmacion = model.RequiereConfirmacion;
                ResultValidate resultValidation = DbServices.ValidateCreate_InsumoSecundario(newProductoInsumo);
                if (resultValidation.Validated)
                {
                    context.ProductoInsumos.Add(newProductoInsumo);
                    context.SaveChanges();
                    return RedirectToAction("DetailInsumosSecundarios", new
                    {
                        IdInsumoPrimarioRowSelect = newProductoInsumo.IdInsumoPrimario,
                        IdInsumoSecundarioRowSelect = newProductoInsumo.IdInsumoSecundario,
                        IdProducto = newProductoInsumo.IdProducto,
                        NombreProducto = newProductoInsumo.NombreProducto,
                        IdInsumoPrimario = newProductoInsumo.IdInsumoPrimario,
                        IdInsumoSecundario = newProductoInsumo.IdInsumoSecundario,
                        InsumoPrimario = newProductoInsumo.NombreInsumoPrimario,
                        InsumoSecundario = newProductoInsumo.NombreInsumoSecundario
                    });
                }
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    return View(newProductoInsumo);
                }
            }
        }

        [HttpGet]
        public ActionResult List(int? idRowSelect, string filtroProductoNombre, string filtroProductoTipo)
        {
            if (idRowSelect == null)
                idRowSelect = 0;
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from p in context.Productos
                            join tp in context.TiposProducto on p.IdTipo equals tp.Id
                            where p.EsInsumo == false &&
                                    (String.IsNullOrEmpty(filtroProductoNombre) || p.Nombre.ToUpper().Contains(filtroProductoNombre.ToUpper())) &&
                                    (String.IsNullOrEmpty(filtroProductoTipo) || tp.Nombre.ToUpper().Contains(filtroProductoTipo.ToUpper()))
                            orderby p.Nombre
                            select new ProductoGestionInsumos()
                            {
                                IdProducto = p.Id,
                                NombreProducto = p.Nombre,
                            }).ToList();

                ViewBag.idRowSelect = idRowSelect;
                ViewBag.FiltroProductoNombre = filtroProductoNombre;
                ViewBag.FiltroProductoTipo = filtroProductoTipo;
                return View(data);
            }
           
        }

        [HttpGet]
        public ActionResult DetailInsumosPrimarios(int? idInsumoPrimarioRowSelect, int? idInsumoSecundarioRowSelect, int IdProducto, string NombreProducto, string filtroProductoNombre, string filtroProductoTipo)
        {
            if (idInsumoPrimarioRowSelect == null)
                idInsumoPrimarioRowSelect = 0;
            if (idInsumoSecundarioRowSelect == null)
                idInsumoSecundarioRowSelect = 0;
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id
                            where pi.IdProducto == IdProducto && pi.IdInsumoPrimario== pi.IdInsumoSecundario
                            orderby pi.IdInsumoPrimario
                            select new
                            {
                                IdProducto = pi.IdProducto,
                                IdInsumoPrimario = pi.IdInsumoPrimario,
                                IdInsumoSecundario = 0,
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                NombreProducto = prd.Nombre,
                                NombreInsumoPrimario = prdip.Nombre ?? "",
                                NombreInsumoSecundario = ""
                            }).AsEnumerable().Select(x => new ProductoInsumo()
                            {
                                IdProducto = x.IdProducto,
                                IdInsumoPrimario = x.IdInsumoPrimario,
                                IdInsumoSecundario = x.IdInsumoSecundario,
                                Unidades = x.Unidades,
                                RequiereConfirmacion = x.RequiereConfirmacion,
                                NombreProducto = x.NombreProducto,
                                NombreInsumoPrimario = x.NombreInsumoPrimario,
                                NombreInsumoSecundario = x.NombreInsumoSecundario,
                            }).ToList();

                ViewBag.IdInsumoPrimarioRowSelect = idInsumoPrimarioRowSelect;
                ViewBag.IdInsumoSecundarioRowSelect = idInsumoSecundarioRowSelect;
                ViewBag.NombreProducto = NombreProducto;
                ViewBag.IdProducto = IdProducto;
                ViewBag.FiltroProductoNombre = filtroProductoNombre;
                ViewBag.FiltroProductoTipo = filtroProductoTipo;
                return View(data);
            }
        }

        [HttpGet]
        public ActionResult DetailInsumosSecundarios(int? idInsumoPrimarioRowSelect, int? idInsumoSecundarioRowSelect, int IdProducto, string NombreProducto, 
                                    int idInsumoPrimario, string NombreInsumoPrimario, int? idInsumoSecundario, string NombreInsumoSecundario = "")
        {
            if (idInsumoPrimarioRowSelect == null)
                idInsumoPrimarioRowSelect = 0;
            if (idInsumoSecundarioRowSelect == null)
                idInsumoSecundarioRowSelect = 0;
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id
                            join prdis in context.Productos on pi.IdInsumoSecundario equals prdis.Id
                            where pi.IdProducto == IdProducto && pi.IdInsumoPrimario == idInsumoPrimario && pi.IdInsumoPrimario!= pi.IdInsumoSecundario
                            orderby pi.IdProducto, pi.IdInsumoPrimario
                            select new
                            {
                                IdProducto = pi.IdProducto,
                                IdInsumoPrimario = pi.IdInsumoPrimario,
                                IdInsumoSecundario = pi.IdInsumoSecundario,
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                NombreProducto= prd.Nombre,
                                NombreInsumoPrimario = prdip.Nombre ?? "",
                                NombreInsumoSecundario = prdis.Nombre ?? ""

                            }).AsEnumerable().Select(x => new ProductoInsumo()
                            {
                                IdProducto = x.IdProducto,
                                IdInsumoPrimario = x.IdInsumoPrimario,
                                IdInsumoSecundario = x.IdInsumoSecundario,
                                Unidades = x.Unidades,
                                RequiereConfirmacion = x.RequiereConfirmacion,
                                NombreProducto = x.NombreProducto,
                                NombreInsumoPrimario = x.NombreInsumoPrimario,
                                NombreInsumoSecundario = x.NombreInsumoSecundario
                            }).ToList();

                ViewBag.IdInsumoPrimarioRowSelect = idInsumoPrimarioRowSelect;
                ViewBag.IdInsumoSecundarioRowSelect = idInsumoSecundarioRowSelect;
                ViewBag.NombreProducto = NombreProducto;
                ViewBag.IdProducto = IdProducto;
                ViewBag.IdInsumoPrimario = idInsumoPrimario;
                ViewBag.NombreInsumoPrimario = NombreInsumoPrimario;
                ViewBag.IdInsumoSecundario = idInsumoSecundario;
                ViewBag.NombreInsumoSecundario = NombreInsumoSecundario;
                return View(data);
            }
        }

        public ActionResult ConfirmDelete(int idProducto, string NombreProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id
                            join prdis in context.Productos on pi.IdInsumoSecundario equals prdis.Id into tmpis  //full outer join
                            from prdis in tmpis.DefaultIfEmpty()
                            where pi.IdProducto == idProducto
                            select new
                            {
                                IdProducto = pi.IdProducto,
                                IdInsumoPrimario = pi.IdInsumoPrimario,
                                IdInsumoSecundario = pi.IdInsumoSecundario,
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                NombreProducto= prd.Nombre,
                                NombreInsumoPrimario=prdip.Nombre,
                                NombreInsumoSecundario=prdis.Nombre ?? ""
                            }).AsEnumerable().Select(x => new ProductoInsumo()
                            {
                                IdProducto = x.IdProducto,
                                IdInsumoPrimario = x.IdInsumoPrimario,
                                IdInsumoSecundario = x.IdInsumoSecundario,
                                Unidades = x.Unidades,
                                RequiereConfirmacion = x.RequiereConfirmacion,
                                NombreProducto = x.NombreProducto,
                                NombreInsumoPrimario = x.NombreInsumoPrimario,
                                NombreInsumoSecundario = x.NombreInsumoSecundario
                            }).ToList();
                ViewBag.IdProducto = idProducto;
                ViewBag.NombreProducto = NombreProducto;
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Delete(int idProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                while (context.ProductoInsumos.Any(x => x.IdProducto == idProducto))
                {
                    var data = context.ProductoInsumos.FirstOrDefault(x => x.IdProducto == idProducto);
                    context.ProductoInsumos.Remove(data);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        public ActionResult ConfirmDeleteInsumoPrimario(int IdProducto, int IdInsumoPrimario, string NombreProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id into tmpis
                            from prdip in tmpis.DefaultIfEmpty()
                            where pi.IdProducto == IdProducto && pi.IdInsumoPrimario == IdInsumoPrimario
                            select new InsumoPrimario()
                            {
                                IdProducto = pi.IdProducto,
                                Id = pi.IdInsumoPrimario,
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                NombreProducto = prd.Nombre,
                                Nombre = prdip.Nombre
                            }).FirstOrDefault();
                ViewBag.IdProducto = IdProducto;
                ViewBag.NombreProducto = NombreProducto;
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult ConfirmDeleteInsumoPrimario(InsumoPrimario model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                while(context.ProductoInsumos.Any(x => x.IdProducto == model.IdProducto && x.IdInsumoPrimario == model.Id))
                {
                    var data = context.ProductoInsumos.FirstOrDefault(x => x.IdProducto == model.IdProducto &&
                                     x.IdInsumoPrimario == model.Id);
                    context.ProductoInsumos.Remove(data);
                    context.SaveChanges();
                }
                if (context.ProductoInsumos.Any(x => x.IdProducto == model.IdProducto))
                {
                    ViewBag.NombreProducto = model.NombreProducto;
                    return RedirectToAction("DetailInsumosPrimarios", new
                    {
                        IdProducto = model.IdProducto,
                        NombreProducto = model.NombreProducto
                    });
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
        }


        public ActionResult ConfirmDeleteInsumoSecundario(int IdProducto, int IdInsumoPrimario, int IdInsumoSecundario, string NombreProducto,
            string NombreInsumoPrimario, string NombreInsumoSecundario)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id
                            join prdis in context.Productos on pi.IdInsumoSecundario equals prdis.Id into tmpis
                            from prdis in tmpis.DefaultIfEmpty()
                            where pi.IdProducto == IdProducto && pi.IdInsumoPrimario == IdInsumoPrimario && pi.IdInsumoSecundario == IdInsumoSecundario
                            && pi.IdInsumoPrimario != pi.IdInsumoSecundario
                            select new InsumoSecundario()
                            {
                                IdProducto = pi.IdProducto,
                                IdInsumoPrimario = pi.IdInsumoPrimario,
                                Id = pi.IdInsumoSecundario,               
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                NombreProducto = prd.Nombre,
                                NombreInsumoPrimario = prdip.Nombre ?? "",
                                Nombre = prdis.Nombre
                            }).FirstOrDefault();
                ViewBag.IdProducto = IdProducto;
                ViewBag.NombreProducto = NombreProducto;
                ViewBag.IdInsumoPrimario = IdInsumoPrimario;
                ViewBag.NombreInsumoPrimario = NombreInsumoPrimario;
                ViewBag.IdInsumoSecundario = IdInsumoSecundario;
                ViewBag.NombreInsumoSecundario = NombreInsumoSecundario;
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult ConfirmDeleteInsumoSecundario(InsumoSecundario model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.ProductoInsumos.FirstOrDefault(x => x.IdProducto == model.IdProducto &&
                 x.IdInsumoPrimario == model.IdInsumoPrimario && x.IdInsumoSecundario == model.Id);
                if (data != null)
                {
                    context.ProductoInsumos.Remove(data);
                    context.SaveChanges();
                    if(context.ProductoInsumos.Any(x => x.IdProducto == model.IdProducto && x.IdInsumoPrimario == model.IdInsumoPrimario
                    && x.IdInsumoPrimario != x.IdInsumoSecundario))
                    {
                        return RedirectToAction("DetailInsumosSecundarios", new
                        {
                            IdInsumoPrimarioRowSelect = model.IdInsumoPrimario,
                            IdProducto = model.IdProducto,
                            NombreProducto = model.NombreProducto,
                            IdInsumoPrimario = model.IdInsumoPrimario,
                            InsumoPrimario = model.NombreInsumoPrimario
                        });
                    } else
                    {
                        return RedirectToAction("DetailInsumosPrimarios", new
                        {
                            IdInsumoPrimarioRowSelect = model.IdInsumoPrimario,
                            IdProducto = model.IdProducto,
                            NombreProducto = model.NombreProducto
                        });
                    }
                }
                else
                {
                    ViewBag.Message = "En base de datos: No se ha podido eliminar el Producto.";
                    return View("Error");
                }

            }
        }

        public ActionResult UpdateInsumoPrimario(int IdProducto, int IdInsumoPrimario)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id
                            where pi.IdProducto == IdProducto && pi.IdInsumoPrimario == IdInsumoPrimario
                            && pi.IdInsumoPrimario == pi.IdInsumoSecundario
                            orderby pi.IdInsumoPrimario
                            select new InsumoPrimario()
                            {
                                IdProducto = pi.IdProducto,
                                Id = pi.IdInsumoPrimario,
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                Nombre = prdip.Nombre ?? "",
                                NombreProducto = prd.Nombre
                            }).FirstOrDefault();
                return View(data);
            };
        }
       

        [HttpPost]
        public ActionResult UpdateInsumoPrimario(InsumoPrimario model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.ProductoInsumos.FirstOrDefault(x => x.IdProducto == model.IdProducto
                && x.IdInsumoPrimario == model.Id && x.IdInsumoPrimario == x.IdInsumoSecundario);
                data.Unidades = model.Unidades;
                data.RequiereConfirmacion = model.RequiereConfirmacion;
                context.SaveChanges();
                ViewBag.NombreProducto = model.NombreProducto;
                return RedirectToAction("DetailInsumosPrimarios", new
                {
                    IdInsumoPrimarioRowSelect = data.IdInsumoPrimario,
                    IdInsumoSecundarioRowSelect = data.IdInsumoSecundario,
                    IdProducto = data.IdProducto,
                    NombreProducto = model.NombreProducto
                }); 
            }
        }

        public ActionResult UpdateInsumoSecundario(int IdProducto, int IdInsumoPrimario, int IdInsumoSecundario)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from pi in context.ProductoInsumos
                            join prd in context.Productos on pi.IdProducto equals prd.Id
                            join prdip in context.Productos on pi.IdInsumoPrimario equals prdip.Id
                            join prdips in context.Productos on pi.IdInsumoSecundario equals prdips.Id
                            where pi.IdProducto == IdProducto && pi.IdInsumoPrimario == IdInsumoPrimario
                            && pi.IdInsumoSecundario == IdInsumoSecundario && pi.IdInsumoPrimario != pi.IdInsumoSecundario
                            orderby pi.IdInsumoPrimario
                            select new InsumoSecundario()
                            {
                                IdProducto = pi.IdProducto,
                                IdInsumoPrimario = pi.IdInsumoPrimario,
                                Id = pi.IdInsumoSecundario,
                                Unidades = pi.Unidades,
                                RequiereConfirmacion = pi.RequiereConfirmacion,
                                Nombre = prdips.Nombre ?? "",
                                NombreProducto = prd.Nombre ?? "",
                                NombreInsumoPrimario = prdip.Nombre ?? ""
                            }).FirstOrDefault();
                return View(data);
            };
        }

        [HttpPost]
        public ActionResult UpdateInsumoSecundario(InsumoSecundario model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.ProductoInsumos.FirstOrDefault(x => x.IdProducto == model.IdProducto
                && x.IdInsumoPrimario == model.IdInsumoPrimario && x.IdInsumoSecundario == model.Id
                && x.IdInsumoPrimario != x.IdInsumoSecundario);
                if (data != null)
                {
                    data.IdInsumoSecundario = model.Id;
                    data.Unidades = model.Unidades;
                    data.RequiereConfirmacion = model.RequiereConfirmacion;
                    data.NombreProducto = model.NombreProducto;
                    data.NombreInsumoPrimario = model.NombreInsumoPrimario;
                    data.NombreInsumoSecundario = model.Nombre;
                }
                context.SaveChanges();
                ViewBag.NombreProducto = model.NombreProducto;
                ViewBag.InsumoPrimario = model.NombreInsumoPrimario;
                return RedirectToAction("DetailInsumosSecundarios", new
                {
                    IdInsumoPrimarioRowSelect = data.IdInsumoPrimario,
                    IdInsumoSecundarioRowSelect = data.IdInsumoSecundario,
                    IdProducto = data.IdProducto,
                    NombreProducto = model.NombreProducto,
                    IdInsumoPrimario = data.IdInsumoPrimario,
                    InsumoPrimario = model.NombreInsumoPrimario,
                    IdInsumoSecundario = data.IdInsumoSecundario,
                    InsumoSecundario = data.NombreInsumoSecundario
                });
            }
        }
    }
}