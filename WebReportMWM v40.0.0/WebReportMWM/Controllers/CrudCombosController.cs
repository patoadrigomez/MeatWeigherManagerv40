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
    public class CrudCombosController : Controller
    {
        // GET: CrudCombos

        public ActionResult CreateAddProducto(int IdProductoCombo, string NombreCombo)
        {
            ProductoEnCombo producto = new ProductoEnCombo();
            producto.IdProductoCombo = IdProductoCombo;
            producto.NombreCombo = NombreCombo;
            producto.ToleranciaPeso = 0;
            return View(producto);
        }

        [HttpPost]
        public ActionResult CreateAddProducto(ProductoEnCombo producto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                Combo newComboProducto = new Combo();
                newComboProducto.IdProductoCombo = producto.IdProductoCombo;
                newComboProducto.IdProducto = producto.IdProducto;
                newComboProducto.Unidades = producto.Unidades;
                newComboProducto.Peso = producto.Peso;
                newComboProducto.ToleranciaPeso = producto.ToleranciaPeso;
                newComboProducto.ValidarUnds = producto.ValidarUnds;
                newComboProducto.ValidarPeso = producto.ValidarPeso;

                ResultValidate resultValidation = DbServices.ValidateAdd_ProductoCombo(newComboProducto);
                if (resultValidation.Validated)
                {
                    context.Combos.Add(newComboProducto);
                    context.SaveChanges();
                    return RedirectToAction("DetailProductos", new
                    {
                        IdComboRowSelect = producto.IdProductoCombo,
                        IdProductoRowSelect = producto.IdProducto,
                        IdProductoCombo = producto.IdProductoCombo,
                        NombreCombo = producto.NombreCombo,
                    });
                } 
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    return View(producto);
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
                var data = (from pc in context.Productos
                            where pc.EsCombo == true
                            select new
                            {
                                IdProductoCombo = pc.Id,
                                NombreCombo = pc.Nombre,
                                ProductosEnCombo = (from cb in context.Combos
                                                    join pec in context.Productos on cb.IdProducto equals pec.Id
                                                    join pcb in context.Productos on cb.IdProductoCombo equals pcb.Id
                                                    where cb.IdProductoCombo == pc.Id
                                                    orderby pec.Nombre
                                                    select new ProductoEnCombo()
                                                    {
                                                        IdProductoCombo = cb.IdProductoCombo,
                                                        IdProducto=cb.IdProducto,
                                                        NombreCombo= pcb.Nombre,
                                                        NombreProducto=pec.Nombre,
                                                        Peso=cb.Peso,
                                                        ToleranciaPeso=cb.ToleranciaPeso,
                                                        Unidades=cb.Unidades,
                                                        ValidarPeso=cb.ValidarPeso,
                                                        ValidarUnds = cb.ValidarUnds    
                                                    }).ToList()
                            }).AsEnumerable().Select(x => new Combo()
                            {
                                IdProductoCombo = x.IdProductoCombo,
                                NombreCombo = x.NombreCombo,
                                ProductosEnCombo= x.ProductosEnCombo
                            }).OrderBy(x => x.NombreCombo).ToList();
                ViewBag.idRowSelect = idRowSelect;
                return View(data);
            }
        }
        [HttpGet]

        public ActionResult DetailProductos(int? idComboRowSelect, int? idProductoRowSelect, int idProductoCombo, string NombreCombo)
        {
            if (idComboRowSelect == null)
                idComboRowSelect = 0;
            if (idProductoRowSelect == null)
                idProductoRowSelect = 0;
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from c in context.Combos
                            join pc in context.Productos on c.IdProductoCombo equals pc.Id
                            join pcc in context.Productos on c.IdProducto equals pcc.Id
                            where c.IdProductoCombo == idProductoCombo
                            orderby pcc.Nombre
                            select new ProductoEnCombo()
                            {
                                IdProductoCombo = c.IdProductoCombo,
                                IdProducto = c.IdProducto,
                                Unidades = c.Unidades,
                                Peso = c.Peso,
                                ToleranciaPeso = c.ToleranciaPeso ?? 0,
                                ValidarUnds = c.ValidarUnds,
                                ValidarPeso = c.ValidarPeso,
                                NombreCombo= pc.Nombre,
                                NombreProducto=pcc.Nombre
                            }).ToList();

                ViewBag.IdComboRowSelect = idComboRowSelect;
                ViewBag.IdProductoRowSelect = idProductoRowSelect;
                ViewBag.NombreCombo = NombreCombo;
                ViewBag.IdProductoCombo = idProductoCombo;
                return View(data);
            }
        }

        public ActionResult UpdateProductoEnCombo(int IdProductoCombo, int IdProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from c in context.Combos
                            join pc in context.Productos on c.IdProductoCombo equals pc.Id
                            join pcc in context.Productos on c.IdProducto equals pcc.Id
                            where c.IdProductoCombo == IdProductoCombo && c.IdProducto == IdProducto
                            select new ProductoEnCombo()
                            {
                                IdProductoCombo = c.IdProductoCombo,
                                IdProducto = c.IdProducto,
                                Unidades = c.Unidades,
                                Peso = c.Peso,
                                ToleranciaPeso = c.ToleranciaPeso ?? 0,
                                ValidarUnds = c.ValidarUnds,
                                ValidarPeso = c.ValidarPeso,
                                NombreCombo = pc.Nombre,
                                NombreProducto = pcc.Nombre
                            }).FirstOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult UpdateProductoEnCombo(ProductoEnCombo producto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Combos.FirstOrDefault(x => x.IdProductoCombo == producto.IdProductoCombo && x.IdProducto == producto.IdProducto);
                data.Unidades = producto.Unidades;
                data.Peso = producto.Peso;
                data.ToleranciaPeso = producto.ToleranciaPeso;
                data.ValidarUnds = producto.ValidarUnds;
                data.ValidarPeso = producto.ValidarPeso;

                ResultValidate resultValidation = DbServices.ValidateUpdate_ProductoCombo(data);
                if(resultValidation.Validated)
                {
                    context.SaveChanges();
                    ViewBag.NombreCombo = producto.NombreCombo;
                    return RedirectToAction("DetailProductos", new
                    {
                        IdComboRowSelect = data.IdProductoCombo,
                        IdProductoRowSelect = data.IdProducto,
                        IdProductoCombo = data.IdProductoCombo,
                        NombreCombo = producto.NombreCombo
                    });
                } 
                else
                {
                    TempData["MessagesError"] = resultValidation.ErrorMessages;
                    return View(producto);
                }
            }
        }

        public ActionResult ConfirmDelete(int IdProductoCombo, string NombreCombo)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from c in context.Combos
                            join pc in context.Productos on c.IdProductoCombo equals pc.Id
                            join pcc in context.Productos on c.IdProducto equals pcc.Id
                            where c.IdProductoCombo == IdProductoCombo
                            orderby pcc.Nombre
                            select new ProductoEnCombo()
                            {
                                IdProductoCombo = c.IdProductoCombo,
                                IdProducto = c.IdProducto,
                                Unidades = c.Unidades,
                                Peso = c.Peso,
                                ToleranciaPeso = c.ToleranciaPeso ?? 0,
                                ValidarUnds = c.ValidarUnds,
                                ValidarPeso = c.ValidarPeso,
                                NombreCombo = pc.Nombre,
                                NombreProducto = pcc.Nombre
                            }).ToList();
                
                ViewBag.IdProductoCombo = IdProductoCombo;
                ViewBag.NombreCombo = NombreCombo;
                if(data != null && data.Count> 0)
                {
                    return View(data);
                }
                else
                {
                    ViewBag.Message = " Este Combo no posee definido productos contenidos para poder eliminar.";
                    return View("Error");
                }
            }
        }

        //Procedimiento que Elimina todos los productos asignados al combo
        [HttpPost]
        public ActionResult Delete(int IdProductoCombo)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Combos.Where(x => x.IdProductoCombo == IdProductoCombo);
                context.Combos.RemoveRange(data);
                context.SaveChanges();
                return RedirectToAction("List", new
                {
                    idRowSelect = IdProductoCombo
                });
            }
        }

        
        public ActionResult ConfirmDeleteProductoCombo (int IdProductoCombo, int IdProducto)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = (from c in context.Combos
                             join pc in context.Productos on c.IdProductoCombo equals pc.Id
                             join pcc in context.Productos on c.IdProducto equals pcc.Id
                             where c.IdProductoCombo == IdProductoCombo && c.IdProducto== IdProducto
                             select new ProductoEnCombo()
                             {
                                 IdProductoCombo = c.IdProductoCombo,
                                 IdProducto = c.IdProducto,
                                 Unidades = c.Unidades,
                                 Peso = c.Peso,
                                 ToleranciaPeso = c.ToleranciaPeso ?? 0,
                                 ValidarUnds = c.ValidarUnds,
                                 ValidarPeso = c.ValidarPeso,
                                 NombreCombo = pc.Nombre,
                                 NombreProducto = pcc.Nombre
                             }).FirstOrDefault();
                return View(data);
            }
        }

        //Procedimiento que elimina un producto específico de un combo

        [HttpPost]

        public ActionResult DeleteProductoCombo(ProductoEnCombo model)
        {
            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Combos.FirstOrDefault(x => x.IdProductoCombo == model.IdProductoCombo && x.IdProducto == model.IdProducto);
                if (data != null)
                {
                    context.Combos.Remove(data);
                    context.SaveChanges();
                    if (context.Combos.Any(x => x.IdProductoCombo == model.IdProductoCombo))
                    {
                        return RedirectToAction("DetailProductos", new
                        {
                            idProductoCombo = model.IdProductoCombo,
                            NombreCombo = model.NombreCombo
                        });
                    }
                    else
                    {
                        return RedirectToAction("List", new
                        {
                            idRowSelect = model.IdProductoCombo
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
    }
}