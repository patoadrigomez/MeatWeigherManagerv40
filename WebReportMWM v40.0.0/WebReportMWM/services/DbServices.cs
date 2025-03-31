using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;
using System.Globalization;
using WebReportMWM.Models.Entitys;
using WebReportMWM.Models;
using DocumentFormat.OpenXml.EMMA;
using static WebReportMWM.Models.Enum;

namespace WebReportMWM.services
{
    public class DbServices
    {
        public static Operadores LoggedUser = null;
        #region VALIDATIONS OPERADOR
        public static ResultValidate ValidateDelete_Operador(int idOperador)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (context.dbLog.Any(x => x.IdOperador == idOperador))
                {
                    result.ErrorMessages.Add("No se puede eliminar el operador porque posee movimientos");
                }
            }
            return result;
        }
        public static ResultValidate ValidateUpdate_Operador(int id, string nombre)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.operadores.Any(x => (x.Nombre.TrimEnd().Equals(nombre.TrimEnd())) && x.Id != id))
                {
                    result.ErrorMessages.Add("El Nombre del Operador ya existe.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateCreate_Operador(Operadores operadores)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.operadores.Any(x => (x.Nombre.TrimEnd().Equals(operadores.Nombre.TrimEnd())) && x.Id != operadores.Id))
                {
                    result.ErrorMessages.Add("El Nombre del Operador ya existe.");
                }
            }
            return result;
        }
        #endregion

        #region VALIDATION TIPOS PRODUCTOS
        public static ResultValidate ValidateDelete_TipoProducto(int idTipoProducto)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (context.Productos.Any(x => x.IdTipo == idTipoProducto))
                {
                    result.ErrorMessages.Add("No se puede eliminar el tipo de producto porque está asociado a un producto");
                }
            }
            return result;
        }
        public static ResultValidate ValidateUpdate_TipoProducto(TipoProducto tipoProducto)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.TiposProducto.Any(x => (x.Nombre.TrimEnd().Equals(tipoProducto.Nombre.TrimEnd())) && x.Id != tipoProducto.Id))
                {
                    result.ErrorMessages.Add("Ya existe un Tipo de Producto con ese nombre.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateCreate_TipoProducto(TipoProducto tipoProducto)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.TiposProducto.Any(x => (x.Nombre.TrimEnd().Equals(tipoProducto.Nombre.TrimEnd())) && x.Id != tipoProducto.Id))
                {
                    result.ErrorMessages.Add("Ya existe un Tipo de Producto con ese nombre.");
                }
            }
            return result;
        }
        #endregion

        #region VALIDATION PRODUCTOS

        public static ResultValidate ValidateDelete_Producto(int idProducto)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (context.MovInsumos.Any(x => x.idPrdInsumo == idProducto))
                {
                    result.ErrorMessages.Add("No se puede eliminar el Producto porque es un Insumo que tiene movimientos");
                }
                else if (context.Pesadas.Any(x => x.IdProducto == idProducto))
                {
                    result.ErrorMessages.Add("No se puede eliminar el Producto porque tiene movimientos");
                }
                else if (context.Cajas.Any(x => x.IdProducto == idProducto) || context.Combos.Any(x => x.IdProducto == idProducto))
                {
                    result.ErrorMessages.Add("No se puede eliminar el Producto porque está asignado a una Caja o Combo");
                }
            }
            return result;
        }

        public static ResultValidate ValidateCreate_Producto(Producto producto)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Productos.Any(x => (x.Nombre.TrimEnd().Equals(producto.Nombre.TrimEnd())) && x.Id != producto.Id))
                {
                    result.ErrorMessages.Add("El Producto ya existe.");
                } else if (producto.EsCombo == true && producto.EsCaja == true)
                {
                    result.ErrorMessages.Add("Un producto no puede ser caja y combo a la vez.");
                } else if ((producto.EsCaja == true || producto.EsCombo == true) && producto.EsInsumo == true)
                {
                    result.ErrorMessages.Add("Un producto no puede ser insumo y caja o combo a la vez.");
                }
                if (producto.EsInsumo == false && String.IsNullOrEmpty(producto.NombreL2)) //Esta validación es porque la lína NombreL2 es requerida en las etiquetas de productos. 
                {
                    result.ErrorMessages.Add("La línea NombreL2 es obligatoria (nombre primario del producto en la etiqueta).");
                } 
                if(producto.TipoBulto != "" && (producto.EsCaja == true || producto.EsCombo == true) && context.TiposBulto.Any(x => x.Id == producto.TipoBulto && x.Id != "CNT"))
                {
                    result.ErrorMessages.Add("No se puede asignar una etiqueta de Pieza a un producto Contenedor.");
                }
                if (producto.TipoBulto != "" && (producto.EsCaja == false && producto.EsCombo == false) && context.TiposBulto.Any(x => x.Id == producto.TipoBulto && x.Id == "CNT"))
                {
                    result.ErrorMessages.Add("No se puede asignar una etiqueta de Contenedor a un producto tipo pieza.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateUpdate_Producto(Producto producto)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (producto == null)
                {
                    result.ErrorMessages.Add("La Entidad Producto no tiene datos !!!.");
                    return result;
                }
                else if (context.Productos.Any(x => (x.Nombre.TrimEnd().Equals(producto.Nombre.TrimEnd())) && x.Id != producto.Id))
                {
                    result.ErrorMessages.Add("El Producto ya existe.");
                }
                else if (producto.EsCombo == true && producto.EsCaja == true)
                {
                    result.ErrorMessages.Add("Un producto no puede ser caja y combo a la vez.");
                }
                else if ((producto.EsCaja == true || producto.EsCombo == true) && producto.EsInsumo == true)
                {
                    result.ErrorMessages.Add("Un producto no puede ser insumo y caja o combo a la vez.");
                }

                if (producto.EsInsumo == false && String.IsNullOrEmpty(producto.NombreL2)) //Esta validación es porque la lína NombreL2 es requerida en las etiquetas de productos. 
                {
                    result.ErrorMessages.Add("La línea NombreL2 es obligatoria (nombre primario del producto en la etiqueta).");
                }
                if (producto.TipoBulto != "" && (producto.EsCaja == true || producto.EsCombo == true) && context.TiposBulto.Any(x => x.Id == producto.TipoBulto && x.Id != "CNT"))
                {
                    result.ErrorMessages.Add("No se puede asignar una etiqueta de Pieza a un producto Contenedor.");
                }
                if (producto.TipoBulto != "" && (producto.EsCaja == false && producto.EsCombo == false) && context.TiposBulto.Any(x => x.Id == producto.TipoBulto && x.Id == "CNT"))
                {
                    result.ErrorMessages.Add("No se puede asignar una etiqueta de Contenedor a un producto tipo pieza.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateUpdateFromWebGrid_Producto(int id, string propertyname, string value)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (propertyname == "Nombre")
                {
                    if (context.Productos.Any(x => (x.Nombre.TrimEnd().Equals(value.TrimEnd())) && x.Id != id))
                    {
                        result.ErrorMessages.Add("El Producto ya existe.");
                    }
                    if (String.IsNullOrEmpty(value))
                    {
                        result.ErrorMessages.Add("El Nombre del producto no puede quedar vacío.");
                    }
                } else if (propertyname == "EsCombo")
                {
                    if (context.Productos.Any(x => (x.EsCaja == true && x.Id == id)) && value == "true")
                    {
                        result.ErrorMessages.Add("Un producto no puede ser caja y combo a la vez.");
                    }
                } else if (propertyname == "EsCaja")
                {
                    if (context.Productos.Any(x => (x.EsCombo == true && x.Id == id)) && value == "true")
                    {
                        result.ErrorMessages.Add("Un producto no puede ser caja y combo a la vez.");
                    }
                } else if (propertyname == "EsInsumo")
                {
                    if (context.Productos.Any(x => ((x.EsCaja == true || x.EsCombo == true) && x.Id == id)) && value == "true")
                    {
                        result.ErrorMessages.Add("Un producto no puede ser insumo y caja o combo a la vez.");
                    }
                    if (value == "false" && context.Productos.Any(x => x.Id == id && String.IsNullOrEmpty(x.NombreL2)))
                    {
                        result.ErrorMessages.Add("Primero debe ingresar un valor para Nomble L2.");
                    }
                } else if (propertyname == "NombreL2")
                {
                    if (context.Productos.Any(x => (x.EsInsumo == false && x.Id == id) && String.IsNullOrEmpty(value)))
                    {
                        result.ErrorMessages.Add("La línea Nombre L2 es obligatoria (nombre primario del producto en la etiqueta).");
                    }
                }
            }
            return result;
        }

        #endregion

        #region VALIDATION ASIGNACIÓN DE INSUMOS A PRODCUTOS(CRUD PRODUCTOINSUMOS)

        public static ResultValidate ValidateCreate_ProductoInsumos(ProductoInsumo productoInsumo)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (productoInsumo.IdInsumoPrimario == 0)
                {
                    result.ErrorMessages.Add("Debe seleccionar un producto y un insumo primario.");
                }
                else if (context.ProductoInsumos.Any(x => x.IdProducto == productoInsumo.IdProducto))
                {
                    result.ErrorMessages.Add("El Producto ya tiene algún insumo asignado.");
                } else if (productoInsumo.Unidades == 0.0)
                {
                    result.ErrorMessages.Add("Debe indicar las unidades.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateCreate_InsumoPrimario(ProductoInsumo productoInsumo)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (productoInsumo.IdInsumoPrimario == 0)
                {
                    result.ErrorMessages.Add("Debe seleccionar un insumo primario.");
                    return result;
                }

                if (context.ProductoInsumos.Any(x => x.IdProducto == productoInsumo.IdProducto && x.IdInsumoPrimario == productoInsumo.IdInsumoPrimario && x.IdInsumoPrimario != productoInsumo.IdInsumoPrimario))
                {
                    result.ErrorMessages.Add("Este insumo ya está asignado como primario en este producto.");
                }

                if (context.ProductoInsumos.Any(x => x.IdProducto == productoInsumo.IdProducto && x.IdInsumoSecundario == productoInsumo.IdInsumoPrimario /*&& x.IdInsumoPrimario != x.IdInsumoSecundario*/))
                {
                    result.ErrorMessages.Add("Este insumo ya está asignado como secundario en este producto.");

                }
                if (productoInsumo.Unidades == 0.0)
                {
                    result.ErrorMessages.Add("Debe indicar las unidades.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateCreate_InsumoSecundario(ProductoInsumo productoInsumo)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (productoInsumo.IdInsumoSecundario == 0)
                {
                    result.ErrorMessages.Add("Debe seleccionar un insumo secundario.");
                    return result;
                }
                if (context.ProductoInsumos.Any(x => x.IdProducto == productoInsumo.IdProducto && x.IdInsumoPrimario == productoInsumo.IdInsumoPrimario
                && x.IdInsumoSecundario == productoInsumo.IdInsumoSecundario))
                {
                    result.ErrorMessages.Add("Este insumo ya está asignado como secundario de este insumo primario .");
                }
                if (context.ProductoInsumos.Any(x => x.IdProducto == productoInsumo.IdProducto && x.IdInsumoPrimario == productoInsumo.IdInsumoSecundario))
                {
                    result.ErrorMessages.Add("Este insumo está asignado como primario en este producto .");
                }
                if (productoInsumo.Unidades == 0.0)
                {
                    result.ErrorMessages.Add("Debe indicar las unidades.");
                }
            }
            return result;
        }

        #endregion

        #region VALIDATION DESTINOS 

        public static ResultValidate ValidateUpdate_Destino(Destino destino)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Destinos.Any(x => (x.Nombre.TrimEnd().Equals(destino.Nombre.TrimEnd())) && x.Id != destino.Id))
                {
                    result.ErrorMessages.Add("Ya existe un destino con ese nombre.");
                }
            }
            return result;
        }
        public static ResultValidate ValidateCreate_Destino(Destino destino)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Destinos.Any(x => (x.Nombre.TrimEnd().Equals(destino.Nombre.TrimEnd()))))
                {
                    result.ErrorMessages.Add("Ya existe un destino con ese nombre.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateDelete_Destino(int idDestino)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Inventario.Any(x => x.IdDestino == idDestino) || context.Pesadas.Any(x => x.IdDestino == idDestino)
                || context.Contenedores.Any(x => x.IdDestino == idDestino))
                {
                    result.ErrorMessages.Add("No se puede eliminar este Destino porque posee movimientos.");
                }
            }
            return result;
        }
        #endregion

        #region VALIDATION SECTORES
        public static ResultValidate ValidateCreate_Sector(Sector sector)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Sectores.Any(x => (x.Nombre.TrimEnd().Equals(sector.Nombre.TrimEnd()))))
                {
                    result.ErrorMessages.Add("Ya existe un sector con ese nombre.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateUpdate_Sector(Sector sector)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Sectores.Any(x => (x.Nombre.TrimEnd().Equals(sector.Nombre.TrimEnd())) && x.Id != sector.Id))
                {
                    result.ErrorMessages.Add("Ya existe un sector con ese nombre.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateDelete_Sector(int idSector)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Pesadas.Any(x => x.IdSector == idSector) || context.DLP.Any(x => x.IdSector == idSector))
                {
                    result.ErrorMessages.Add("No se puede eliminar este Sector porque posee movimientos.");
                }
            }
            return result;
        }

        #endregion

        #region VALIDATION CAJAS  

        public static ResultValidate ValidateCreate_Cajas(Caja caja)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (caja.IdProductoCaja == 0 || caja.IdProducto == 0)
                {
                    result.ErrorMessages.Add("Debe seleccionar una caja y un producto para asignar.");
                } else if (context.Cajas.Any(x => x.IdProductoCaja == caja.IdProductoCaja))
                {
                    result.ErrorMessages.Add("Esta caja ya tiene un producto asignado.");
                }
            }
            return result;
        }
        #endregion

        #region VALIDATION COMBOS

        public static ResultValidate ValidateAdd_ProductoCombo(Combo combo)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (combo.IdProducto == 0) result.ErrorMessages.Add("Debe seleccionar un producto para asignar .");

                else if (context.Combos.Any(x => x.IdProductoCombo == combo.IdProductoCombo && x.IdProducto == combo.IdProducto)) result.ErrorMessages.Add("Este combo ya tiene este producto asignado .");

                else if (combo.Peso == 0) result.ErrorMessages.Add("Debe indicar un peso");

                else if (combo.ValidarPeso == true && combo.ToleranciaPeso == 0) result.ErrorMessages.Add("Debe indicar un valor para el peso o de tolerancia para el peso");

                else if (combo.ValidarUnds == true && combo.Unidades == 0) result.ErrorMessages.Add("Debe indicar las unidades");
            }
            return result;
        }

        public static ResultValidate ValidateUpdate_ProductoCombo(Combo combo)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (combo.Peso == 0) result.ErrorMessages.Add("El peso debe ser mayor a 0(cero)");

                else if (combo.ValidarPeso == true && combo.ToleranciaPeso == 0) result.ErrorMessages.Add("Debe indicar un valor de tolerancia para el peso");

                else if (combo.ValidarUnds == true && combo.Unidades == 0) result.ErrorMessages.Add("Debe indicar las unidades");
            }
            return result;
        }


        #endregion

        #region VALIDATIONS ETIQUETAS
        public static ResultValidate ValidateUpdateCreate_Etiqueta(Etiqueta etiquetas)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Etiquetas.Any(x => (x.Nombre.TrimEnd().Equals(etiquetas.Nombre.TrimEnd())) && x.Id != etiquetas.Id))
                {
                    result.ErrorMessages.Add("Ya existe una etiqueta con este nombre.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateDelete_Etiqueta(int idEtiqueta)
        {
            ResultValidate result = new ResultValidate();
            using (var context = new DMMeatWeigherModel())
            {
                if (context.Productos.Any(x => x.IdEtiqueta == idEtiqueta))
                {
                    result.ErrorMessages.Add("No se puede eliminar la etiqueta porque está asignada a un producto");
                }
            }
            return result;
        }
        #endregion

        #region VALIDATIONS TIPIFICACIONES
        public static ResultValidate ValidateUpdateCreate_Tipificacion(Tipificacion tipificacion)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Tipificaciones.Any(x => (x.Nombre.TrimEnd().Equals(tipificacion.Nombre.TrimEnd()))))
                {
                    result.ErrorMessages.Add("Ya existe una tipificación con ese nombre.");
                }
            }
            return result;
        }

        public static ResultValidate ValidateDelete_Tipificacion(int idTipificacion)
        {
            ResultValidate result = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                if (context.Pesadas.Any(x => (x.IdTipificacion == idTipificacion)))
                {
                    result.ErrorMessages.Add("No se puede eliminar la tipificación porque posee un movimiento.");
                }
            }
            return result;
        }
        #endregion

        #region TOOLS
        /// <summary>
        /// Ejecuta un StoreProcedure 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecStoreProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters = null)
        {
            using (DMMeatWeigherModel context = new DMMeatWeigherModel())
            {
                var conn = context.Database.Connection;
                var initialState = conn.State;
                var dt = new DataTable();

                try
                {
                    if (initialState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = storedProcedureName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 480;  //6'

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                cmd.Parameters.Add(parameter);
                            }
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (initialState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public static DataTable ExecQuery(string query)
        {
            using (DMMeatWeigherModel context = new DMMeatWeigherModel())
            {
                var conn = context.Database.Connection;
                var initialState = conn.State;
                var dt = new DataTable();

                try
                {
                    if (initialState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandTimeout = 480;  //6'

                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (initialState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        #endregion

        #region REPORTES    
        //REPORTE INGRESO A PLANTA DETALLADO
        public static DataTable GetConsultaReporte_IngresoAPlantaDetalle(DateTime fechaDesde, DateTime fechaHasta, int pesadaManual, string idProveedor = "", int idProducto = 0, int idTipoProducto = 0, int NumTropa = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repIngPlantaDetalle", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProveedor",
                        DbType = DbType.String,
                        Value = idProveedor
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@numTropa",
                        DbType = DbType.Int32,
                        Value = NumTropa
                    },
                       new SqlParameter()
                    {
                        ParameterName = "@manual",
                        DbType = DbType.Int32,
                        Value = pesadaManual
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //REPORTE INGRESO A PLANTA TOTALIZADO X OI PRODUCTO
        public static DataTable GetConsultaReporte_IngresoAPlantaTotalizadoxOIProducto(DateTime fechaDesde, DateTime fechaHasta, string idProveedor = "", int idProducto = 0, int idTipoProducto = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repIngPlantaTotalizado", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProveedor",
                        DbType = DbType.String,
                        Value = idProveedor
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //INGRESO A PLANTA TOTALIZADO X DIA-PROVEEDOR
        public static DataTable GetConsultaReporte_IngresoAPlantaTotalizadoxDiaProveedor(DateTime fechaDesde, DateTime fechaHasta, string idProveedor = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repIngPlantaTotalizadoXDiaProveedor", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProveedor",
                        DbType = DbType.String,
                        Value = idProveedor
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //REPORTE INGRESO A PRODUCCION DETALLADO
        public static DataTable GetConsultaReporte_IngresoAProduccionDetalle(DateTime fechaDesde, DateTime fechaHasta, int idSector = 0, int idTipoProducto = 0, int idProducto = 0, int NumTropa = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repIngProduccionDetallePorSector", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idSector",
                        DbType = DbType.Int32,
                        Value = idSector
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@numTropa",
                        DbType = DbType.Int32,
                        Value = NumTropa
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //REPORTE INGRESO A PRODUCCION TOTALIZADO
        public static DataTable GetConsultaReporte_IngresoAProduccionTotalizado(DateTime fechaDesde, DateTime fechaHasta, int idSector = 0, int idTipoProducto = 0, int idProducto = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repIngProduccionTotalizadoPorSector", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idSector",
                        DbType = DbType.Int32,
                        Value = idSector
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //PRODUCCIÓN DETALLADO

        public static DataTable GetConsultaReporte_ProduccionDetalle(DateTime fechaDesde, DateTime fechaHasta, int idSector = 0, int idTipoProducto = 0, int idProducto = 0, string tipoBulto = "")
        {
            DataTable dt = null;
            try
            {
                //TIPO,NRO,LOTE,COD_PRD,PRODUCTO,TIPO_PRD,CREADA,OPERADOR,DESTINO,SECTOR,PUESTO,UNDS,NETO,TARA   
                dt = ExecStoreProcedure("sp_repProduccionDetalladoFull", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idSector",
                        DbType = DbType.Int32,
                        Value = idSector
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@tipo",
                        Size=20,
                        Value = tipoBulto=="TODOS"?"":tipoBulto
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //PRODUCCIÓN TOTALIZADO

        public static DataTable GetConsultaReporte_ProduccionTotalizado(DateTime fechaDesde, DateTime fechaHasta, int idSector = 0, int idTipoProducto = 0, int idProducto = 0, string tipoBulto = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repProduccionTotalizadoFull", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idSector",
                        DbType = DbType.Int32,
                        Value = idSector
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@tipo",
                        Size=20,
                        Value = tipoBulto=="TODOS"?"":tipoBulto
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_InsumosProduccionDetallado(DateTime fechaDesde, DateTime fechaHasta, int idPrdInsumo = 0, string tipoBulto = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repInsumosEnProduccionDetallado", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idPrdInsumo
                    },
                     new SqlParameter()
                     {
                         ParameterName = "@tipoBulto",
                         DbType = DbType.String,
                         Value = tipoBulto
                     }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_InsumosProduccionTotalizado(DateTime fechaDesde, DateTime fechaHasta, int idPrdInsumo = 0, string tipoBulto = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repInsumosEnProduccionTotalizado", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idPrdInsumo
                    },
                     new SqlParameter()
                     {
                         ParameterName = "@tipoBulto",
                         DbType = DbType.String,
                         Value = tipoBulto
                     }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_LogEventos(DateTime fechaDesde, DateTime fechaHasta, string contexto = "", string evento = "", string detalle = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repEventosLog", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@contexto",
                        DbType = DbType.String,
                        Value = contexto
                    },
                          new SqlParameter()
                    {
                        ParameterName= "@evento",
                        DbType = DbType.String,
                        Value = evento
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@detalle",
                        DbType = DbType.String,
                        Value = detalle
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        //Inserta una registro en la tabla lod de eventos cuando se realiza un ajuste manual de inventario
        public static bool Registrar_Log_Evento(int idOperador, string evento, string contexto, string detalle)
        {
            bool insert = false;
            try
            {
                ExecStoreProcedure("sp_insertDbLog", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@idOperador",
                        DbType= DbType.Int32,
                        Value= idOperador
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@idEstacion",
                        DbType =  DbType.Int32,
                        Value = 1
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@evento",
                        DbType = DbType.String,
                        Value = evento
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@contexto",
                        DbType = DbType.String,
                        Value = contexto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@detalle",
                        DbType = DbType.String,
                        Value = detalle
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    },
                });
                insert = true;
            }
            catch (Exception e)
            {
                throw (e);
            }
            return insert;
        }

        public static DataTable GetConsultaRepote_RendimientoProduccionTP(DateTime fechaDesde, DateTime fechaHasta, int idTipoProducto = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repRendimientoPorTipoDeProducto", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.String,
                        Value = idTipoProducto
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_RendimientoProduccionSector(DateTime fechaDesde, DateTime fechaHasta, int idSector)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repRendimientoPorProductoPorSector", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@idsector",
                        DbType = DbType.String,
                        Value = idSector
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_EgresosDetallado(DateTime fechaDesde, DateTime fechaHasta, string lote, string cliente,
        string tipoBulto, int idTipoProducto, int idProducto, string comprobantePedido)
        {

            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repDetalleEgresosFull", new List<SqlParameter>()
                {
                     new SqlParameter()
                    {
                        ParameterName="@cliente",
                        DbType= DbType.String,
                        Value= cliente
                    },
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.String,
                        Value= !String.IsNullOrEmpty(comprobantePedido) || !String.IsNullOrEmpty(lote) ? "" : fechaDesde.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType= DbType.String,
                        Value = !String.IsNullOrEmpty(comprobantePedido) || !String.IsNullOrEmpty(lote)? "" : fechaHasta.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@comprobantePedido",
                        DbType = DbType.String,
                        Value = comprobantePedido ?? ""
                    },
                     new SqlParameter()
                     {
                        ParameterName= "@lote",
                        DbType= DbType.String,
                        Value = ConverterEditedLoteToString(lote)
                     },
                      new SqlParameter()
                      {
                        ParameterName= "@tipoBulto",
                        DbType = DbType.String,
                        Value = tipoBulto == "Todos" ? "" : tipoBulto
                      },
                       new SqlParameter()
                       {
                         ParameterName= "@idTipoProducto",
                         DbType = DbType.Int32,
                         Value = idTipoProducto
                       },
                         new SqlParameter()
                       {
                         ParameterName= "@idProducto",
                         DbType = DbType.Int32,
                         Value = idProducto
                       },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }


        /// <summary>
        /// Convierte un lote en formato string ddMMyyyy en un string
        /// con formato yyyyMMdd
        /// Si su valor de fecha ddMMyyyy no es valido , retorna el valor
        /// minimo de una fecha "00010101"
        /// Si su valor es null retorna una cadena vacia "".
        /// </summary>
        /// <param name="editedLote"></param>
        /// <returns></returns>
        public static string ConverterEditedLoteToString(string editedLote,string defaultValue="")
        {
            string loteConverted = defaultValue;
            if (!String.IsNullOrEmpty(editedLote))
            {
                DateTime dtLote;
                
                if(!DateTime.TryParseExact(editedLote, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtLote))
                    dtLote = new DateTime(1900, 01, 01, 00, 00, 00); //minima fecha para sql server
                loteConverted = dtLote.ToString("yyyyMMdd");
            }
            return loteConverted;
        }

        /// <summary>
        /// Convierte un lote en formato string ddMMyyyy en un datetime
        /// Si su valor de fecha ddMMyyyy no es valido , retorna el valor
        /// minimo de una fecha "0001-01-01"
        /// </summary>
        /// <param name="editedLote"></param>
        /// <returns></returns>
        public static DateTime ConverterEditedLoteToDateTime(string editedLote)
        {
            DateTime dtLote = new DateTime(1900, 01, 01, 00, 00, 00); // Esto es porque para SQL la minima fecha posible es 1/1/1753 12:00:00 y no se puede usar DateTime.MinValue
            if (!String.IsNullOrEmpty(editedLote))
            {
                if(!DateTime.TryParseExact(editedLote, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtLote))
                    dtLote = new DateTime(1900, 01, 01, 00, 00, 00);
            }
            return dtLote;
        }

        public static DataTable GetConsultaRepote_EgresosTotalizadoxProdFecha(DateTime fechaDesde, DateTime fechaHasta, string cliente = "", string comprobantePedido = "",
        string tipoBulto = "", int idTipoProducto = 0, int idProducto = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repEgresosTotalizadosFullPorProductoPorFecha", new List<SqlParameter>()
                {
                     new SqlParameter()
                    {
                        ParameterName="@cliente",
                        DbType= DbType.String,
                        Value= cliente
                    },
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.String,
                        Value= /*fechaDesde == DateTime.Today || comprobantePedido != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")  */
                        comprobantePedido != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.String,
                        Value = comprobantePedido != "" ? "" : fechaHasta.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@comprobantePedido",
                        DbType = DbType.String,
                        Value = comprobantePedido
                    },
                      new SqlParameter()
                      {
                        ParameterName= "@tipoBulto",
                        DbType = DbType.String,
                        Value = tipoBulto
                      },
                       new SqlParameter()
                       {
                         ParameterName= "@idTipoProducto",
                         DbType = DbType.Int32,
                         Value = idTipoProducto
                       },
                         new SqlParameter()
                       {
                         ParameterName= "@idProducto",
                         DbType = DbType.Int32,
                         Value = idProducto
                       },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_EgresosTotalizadoxDiaCliente(DateTime fechaDesde, DateTime fechaHasta, string idCliente)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repEgresosTotalizadoXDiaCliente", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.String,
                        Value= fechaDesde.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.String,
                        Value = fechaHasta.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@idCliente",
                        DbType = DbType.String,
                        Value = idCliente
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_EgresosConSaldos(DateTime fechaDesde, DateTime fechaHasta, string lote, string cliente, string comprobantePedido = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_getSaldosEgresosPorFechas", new List<SqlParameter>()
                {
                     new SqlParameter()
                    {
                        ParameterName="@cliente",
                        DbType= DbType.String,
                        Value= cliente ?? ""
                    },
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.String,
                        Value=  !String.IsNullOrEmpty(comprobantePedido) || !String.IsNullOrEmpty(lote) ? "" : fechaDesde.ToString("yyyy-MM-dd")   
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.String,
                        Value = !String.IsNullOrEmpty(comprobantePedido) || !String.IsNullOrEmpty(lote) ? "" : fechaHasta.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@comprobantePedido",
                        DbType = DbType.String,
                        Value = comprobantePedido
                    },
                     new SqlParameter()
                     {
                        ParameterName= "@lote",
                        DbType = DbType.String,
                        Value = ConverterEditedLoteToString(lote)
                     },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }
        public static DataTable GetConsultaRepote_InsumosEgresosDetallado(DateTime fechaDesde, DateTime fechaHasta, string comprobantePedido = "", string cliente = "", int idPrdInsumo = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repInsumosEnEgresosDetallado", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@comprobantePedido",
                        DbType = DbType.String,
                        Value = comprobantePedido
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@cliente",
                        DbType = DbType.String,
                        Value = cliente
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idPrdInsumo
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_InsumosEgresosTotalizado(DateTime fechaDesde, DateTime fechaHasta, string cliente = "", int idPrdInsumo = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repInsumosEnEgresosTotalizado", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@cliente",
                        DbType = DbType.String,
                        Value = cliente
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idPrdInsumo
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }

            return dt;
        }

        public static DataTable GetConsultaRepote_Devoluciones(DateTime fechaDesde, DateTime fechaHasta, string cliente = "", string comprobantePedido = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repDevoluciones", new List<SqlParameter>()
                {
                     new SqlParameter()
                    {
                        ParameterName="@cliente",
                        DbType= DbType.String,
                        Value= cliente
                    },
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.String,
                        Value= fechaDesde == DateTime.Today || comprobantePedido != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")    
                        //comprobantePedido != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.String,
                        Value = comprobantePedido != "" ? "" : fechaHasta.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@comprobantePedido",
                        DbType = DbType.String,
                        Value = comprobantePedido
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_PedidosReqPreparaciionPedidos(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repTotalizadoProductosPedidoPendienteVentaPorPreparacion", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.String,
                        Value= fechaDesde.ToString("yyyy-MM-dd")
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.String,
                        Value = fechaHasta.ToString("yyyy-MM-dd")
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockDetalle(DateTime fechaHasta, int idTipoProducto = 0, int idProducto = 0, int idUbicacion = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockDetalle", new List<SqlParameter>()
                {
                      new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = idUbicacion
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockTotalizado(DateTime fechaHasta, int idTipoProducto = 0, int idProducto = 0, int idUbicacion = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockTotalizado", new List<SqlParameter>()
                {
                      new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = idUbicacion
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockTotalizadoDestino(DateTime fechaHasta, int idTipoProducto = 0, int idProducto = 0, int idUbicacion = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockFullTotalizadoPorDestino", new List<SqlParameter>()
                {
                      new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = idUbicacion
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockDetalleVencimiento(DateTime fechaHasta, int idTipoProducto = 0, int idProducto = 0, int idUbicacion = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento", new List<SqlParameter>()
                {
                      new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = idUbicacion
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockDetalleProxVencimiento(DateTime fechaHasta, int idTipoProducto = 0, int idProducto = 0, int idUbicacion = 0, int diasVencimiento = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockDetalleProximidadVencimiento", new List<SqlParameter>()
                {
                      new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                       new SqlParameter()
                    {
                        ParameterName= "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = idUbicacion
                    },
                         new SqlParameter()
                    {
                        ParameterName= "@diasProximidadVencimiento",
                        DbType = DbType.Int32,
                        Value = diasVencimiento
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockContenedoresTotalDestino(DateTime fechaHasta, int idTipoProducto = 0, int idProducto = 0, int idUbicacion = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockContenedoresTotalizadoPorDestino", new List<SqlParameter>()
                {
                      new SqlParameter()
                    {
                        ParameterName= "@idTipoProducto",
                        DbType = DbType.Int32,
                        Value = idTipoProducto
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                      new SqlParameter()
                    {
                        ParameterName= "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = idUbicacion
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_ExistenciaStockInsumos(DateTime fechaHasta, int idInsumo = 0)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repExistenciaEnStockInsumos", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                     new SqlParameter()
                    {
                        ParameterName= "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idInsumo
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_TrazabilidadPieza(int numPieza)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_GetOIsPieza", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName= "@numPieza",
                        DbType = DbType.Int32,
                        Value = numPieza
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaRepote_TrazabilidadLote(string lote)
        {
            DataTable dt = null;
            DateTime dtLote = DateTime.MinValue;
            dtLote = ConverterEditedLoteToDateTime(lote);
            
            try
            {
                dt = ExecStoreProcedure("sp_GetOIsLote", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName= "@lote",
                        DbType = DbType.Date,
                        Value = dtLote
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_HistoricoPiezaContenedor(int idPieza,int esContenedor)
        {
            DataTable dt = null;

            try
            {
                dt = ExecStoreProcedure("sp_getMovimientos", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName= "@id",
                        DbType = DbType.Int32,
                        Value = idPieza
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@esContenedor",
                        DbType = DbType.Boolean,
                        Value = esContenedor
                    }
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        public static DataTable GetConsultaReporte_ResultInventario(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_repResultInventario", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@desde",
                        DbType= DbType.Date,
                        Value= fechaDesde
                    },
                    new SqlParameter()
                    {
                        ParameterName= "@hasta",
                        DbType = DbType.Date,
                        Value = fechaHasta
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }
        #endregion

        #region GET SELECT ITEMS
        //Obtiene lista de Tipos de Productos
        public static List<SelectListItem> GetTipoProductosToListSelectListItem(string tipoProducto = "")
        {
            List<SelectListItem> listTipoProductos = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listTipoProductos = (from t in context.TiposProducto
                                         where t.Nombre == tipoProducto || tipoProducto == ""
                                         orderby t.Nombre
                                         select new SelectListItem()
                                         {
                                             Value = t.Id.ToString(),
                                             Text = t.Nombre
                                         }).ToList();

                }
                if (listTipoProductos == null)
                {
                    listTipoProductos = new List<SelectListItem>();
                }
                listTipoProductos.Insert(0, new SelectListItem() { Text = "Todos", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listTipoProductos;
        }

        //Obtiene lista de Destinos
        public static List<SelectListItem> GetDestinosToListSelectListItem()
        {
            List<SelectListItem> listDestinos = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listDestinos = (from d in context.Destinos
                                         orderby d.Nombre
                                         select new SelectListItem()
                                         {
                                             Value = d.Id.ToString(),
                                             Text = d.Nombre
                                         }).ToList();

                }
                if (listDestinos == null)
                {
                    listDestinos = new List<SelectListItem>();
                }
                listDestinos.Insert(0, new SelectListItem() { Text = "Todos", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listDestinos;
        }
        //Obtiene lista de Tipos de Productos
        public static List<SelectListItem> GetTipoProductosToListSelectListItem(SelectListItem firstItemToInsert)
        {
            List<SelectListItem> listTipoProductos = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listTipoProductos = (from t in context.TiposProducto
                                         orderby t.Nombre
                                         select new SelectListItem()
                                         {
                                             Value = t.Id.ToString(),
                                             Text = t.Nombre
                                         }).ToList();

                }
                if (listTipoProductos == null)
                {
                    listTipoProductos = new List<SelectListItem>();
                }
                if (firstItemToInsert != null)
                    listTipoProductos.Insert(0, firstItemToInsert);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listTipoProductos;
        }

        //Obtiene lista de Etiquetas
        public static List<SelectListItem> GetEtiquetasToListSelectListItem(SelectListItem firstItemToInsert)
        {
            List<SelectListItem> listEtiquetas = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listEtiquetas = (from e in context.Etiquetas
                                         orderby e.Nombre
                                         select new SelectListItem()
                                         {
                                             Value = e.Id.ToString(),
                                             Text = e.Nombre
                                         }).ToList();

                }
                if (listEtiquetas == null)
                {
                    listEtiquetas = new List<SelectListItem>();
                }
                if (firstItemToInsert != null)
                    listEtiquetas.Insert(0, firstItemToInsert);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listEtiquetas;
        }

        //Devuelve lista de tipo de contenedores
        public static List<SelectListItem> GetTipoContenedoresToListSelectListItem()
        {
            List<SelectListItem> listTipoContenedores = null;

            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listTipoContenedores = (from tc in context.TiposContenedor
                                            orderby tc.Descripcion
                                            select new SelectListItem()
                                            {
                                                Value = tc.Descripcion,
                                                Text = tc.Descripcion
                                            }).ToList();

                }
                if (listTipoContenedores == null)
                {
                    listTipoContenedores = new List<SelectListItem>();
                }
                listTipoContenedores.Insert(0, new SelectListItem() { Text = "Todos", Value = "" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listTipoContenedores;
        }

        //Obtiene lista de Proveedores desde PHY
        public static List<SelectListItem> GetProveedoresSACToListSelectListItem(string nombreProv = "")
        {
            DataTable dt = null;
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Insert(0, new SelectListItem() { Text = "Todos", Value = "" });
            try
            {
                dt = ExecStoreProcedure("sp_getProveedoresSAC", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nameFilter",
                        DbType= DbType.String,
                        Value= nombreProv
                    },
                });
                var selectList = new SelectList(dt.AsDataView(), "CODIGO", "NOMBRE");
                if (selectList != null)
                    lst.AddRange(selectList);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return lst;
        }
        //Obtiene lista de Proveedores desde PHY
        public static List<ProveedorSAC> GetProveedoresSACToList()
        {
            DataTable dt = null;
            List<ProveedorSAC> lst = new List<ProveedorSAC>();
            try
            {   //CODIGO,NOMBRE
                dt = ExecStoreProcedure("sp_getProveedoresSAC", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nameFilter",
                        DbType= DbType.String,
                        Value= ""
                    },
                });
                if (dt != null)
                {
                    lst = (from psac in dt.AsEnumerable()
                           select new ProveedorSAC
                           {
                               Id = TOOLS.Tools.GetValueColumn<string>(psac, "CODIGO"),
                               Nombre = TOOLS.Tools.GetValueColumn<string>(psac, "NOMBRE"),
                           }).ToList();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return lst;
        }

        //Obtiene lista de Productos
        public static List<SelectListItem> GetProductosToListSelectListItem(int tipoProducto = 0)
        {
            List<SelectListItem> listProductos = null;

            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listProductos = (from p in context.Productos
                                     where p.IdTipo == tipoProducto || tipoProducto == 0
                                     orderby p.Nombre
                                     select new SelectListItem()
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Nombre
                                     }).ToList();

                }
                if (listProductos == null)
                {
                    listProductos = new List<SelectListItem>();
                }
                listProductos.Insert(0, new SelectListItem() { Text = "Todos", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listProductos;
        }

        //Obtiene lista de productos sacando los productos que son insumos
        public static List<SelectListItem> GetProductosNoInsumosToListSelectListItem()
        {
            List<SelectListItem> listProductos = null;

            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listProductos = (from p in context.Productos
                                     where p.EsInsumo == false
                                     orderby p.Nombre
                                     select new SelectListItem()
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Nombre
                                     }).ToList();

                }
                if (listProductos == null)
                {
                    listProductos = new List<SelectListItem>();
                }
                listProductos.Insert(0, new SelectListItem() { Text = "Todos", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listProductos;
        }

        //Obtiene los sectores
        public static List<SelectListItem> GetSectoresToListSelectListItem()
        {
            List<SelectListItem> listSectores = null;

            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listSectores = (from s in context.Sectores
                                    orderby s.Nombre
                                    select new SelectListItem()
                                    {
                                        Value = s.Id.ToString(),
                                        Text = s.Nombre
                                    }).ToList();

                }
                if (listSectores == null)
                {
                    listSectores = new List<SelectListItem>();
                }
                listSectores.Insert(0, new SelectListItem() { Text = "Todos", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listSectores;
        }

        //Obtiene los tipos de bultos. Contenedores abierto por caja y combo
        public static List<SelectListItem> GetTiposBultoToListSelectListItem()
        {
            List<SelectListItem> listTiposBulto = null;
            List<SelectListItem> listPiezas = null;
            List<SelectListItem> listContenedores = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listPiezas = (from tp in context.TiposBulto
                                  where tp.Nombre == "PIEZA"
                                  orderby tp.Nombre
                                  select new SelectListItem()
                                  {
                                      Value = tp.Nombre.ToString(),
                                      Text = tp.Nombre.ToString()
                                  }).ToList();
                    listContenedores = (from tc in context.TiposContenedor
                                        orderby tc.Descripcion
                                        select new SelectListItem()
                                        {
                                            Value = tc.Descripcion.ToString(),
                                            Text = tc.Descripcion.ToString()
                                        }).ToList();
                }
                listTiposBulto = listPiezas.Concat(listContenedores).ToList();
                if (listTiposBulto == null)
                {
                    listTiposBulto = new List<SelectListItem>();
                }
                listTiposBulto.Insert(0, new SelectListItem() { Text = "Todos", Value = "" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listTiposBulto;
        }

        //Obtiene los tipos de Bulto (Pieza o Contenedor)

        public static List<SelectListItem> GetBultosToListSelectListItem()
        {
            List<SelectListItem> listBultos = null;

            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listBultos = (from b in context.TiposBulto
                                  orderby b.Nombre
                                  select new SelectListItem()
                                  {
                                      Value = b.Id.ToString(),
                                      Text = b.Nombre.ToString()
                                  }).ToList();

                }
                if (listBultos == null)
                {
                    listBultos = new List<SelectListItem>();
                }
                listBultos.Insert(0, new SelectListItem() { Text = "Todos", Value = "" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listBultos;
        }



        //Obtiene los Insumos de la tabla Insumos
        public static List<SelectListItem> GetInsumosToListSelectListItem()
        {
            List<SelectListItem> listInsumos = null;

            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listInsumos = (from i in context.Productos
                                   where i.EsInsumo == true
                                   orderby i.Nombre
                                   select new SelectListItem()
                                   {
                                       Value = i.Id.ToString(),
                                       Text = i.Nombre
                                   }).ToList();

                }
                if (listInsumos == null)
                {
                    listInsumos = new List<SelectListItem>();
                }
                listInsumos.Insert(0, new SelectListItem() { Text = "Todos", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listInsumos;
        }

        //Obtiene lista de etiquetas

        public static List<SelectListItem> GetEtiquetasToListSelectListItem(string etiqueta = "", SelectListItem firstItemToInsert = null)
        {
            List<SelectListItem> listEtiquetas = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listEtiquetas = (from e in context.Etiquetas
                                     select new SelectListItem()
                                     {
                                         Value = e.Id.ToString(),
                                         Text = e.Nombre
                                     }).ToList();
                }
                if (listEtiquetas == null)
                {
                    listEtiquetas = new List<SelectListItem>();
                }
                if (firstItemToInsert != null && etiqueta == "")
                {
                    listEtiquetas.Insert(0, firstItemToInsert);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listEtiquetas;
        }

        //Obtiene lista de Clientes desde PHY
        public static List<SelectListItem> GetClientesToListSelectListItem(string cliente = "", SelectListItem firstItemToInsert = null)
        {
            DataTable dt = null;
            List<SelectListItem> listClientes = new List<SelectListItem>();
            try
            {
                dt = GetClientesSac(cliente);
                var selectList = new SelectList(dt.AsDataView(), "NOMBRE", "NOMBRE");
                if (selectList != null)
                {
                    listClientes.AddRange(selectList);
                    if (firstItemToInsert != null && cliente == "")
                    {
                        listClientes.Insert(0, firstItemToInsert);
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listClientes;
        }

        //Obtiene un datatable de Clientes SAC
        public static DataTable GetClientesSac(string cliente = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_getClientesSAC", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nameFilter",
                        DbType= DbType.String,
                        Value= cliente
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene una lista de tipo List<SelectListItem> con los
        /// Depositos SAC
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetDepositoSacToListSelectListItem(string deposito = "", SelectListItem firstItemToInsert = null)
        {
            DataTable dt = null;
            List<SelectListItem> listDepositoSac = new List<SelectListItem>();
            try
            {
                dt = GetDepositosSac(deposito);
                var selectList = new SelectList(dt.AsDataView(), "CODIGO", "NOMBRE");
                if (selectList != null)
                {
                    listDepositoSac.AddRange(selectList);
                    if (firstItemToInsert != null && deposito == "")
                    {
                        listDepositoSac.Insert(0, firstItemToInsert);
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listDepositoSac;
        }

        /// <summary>
        /// Obtiene una lista de tipo List<DepositoSAC> con los
        /// depósitos SAC
        /// </summary>
        /// <returns></returns>
        public static List<DepositoSAC> GetDepositosSacToList()
        {
            DataTable dt = null;
            List<DepositoSAC> listDepositoSac = new List<DepositoSAC>();
            try
            {
                dt = GetDepositosSac();
                listDepositoSac = dt.AsEnumerable().Select(i => new DepositoSAC()
                {
                    Id = i.Field<string>("CODIGO"),
                    Nombre = i.Field<string>("NOMBRE")
                }).ToList();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listDepositoSac;
        }

        public static string NombreDeposito(string deposito)
        {
            DataTable dt = null;
           try
            {
                dt = GetDepositosSac();
                foreach (DataRow fila in dt.Rows)
                {
                    string codigo = fila["CODIGO"].ToString();
                    string nombreDeposito = fila["NOMBRE"].ToString();
                    if (codigo == deposito) return nombreDeposito;
                }
            } catch(Exception e)
            {
                throw (e);
            }
            return "";
        }

        //Obtiene un datatable de Depósitos SAC
        public static DataTable GetDepositosSac(string deposito = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_getDepositosSAC", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nombreFiltro",
                        DbType= DbType.String,
                        Value= deposito
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene una lista de tipo List<SelectListItem> con los
        /// productos SAC
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetProductosSacToListSelectListItem(string producto = "", SelectListItem firstItemToInsert = null)
        {
            DataTable dt = null;
            List<SelectListItem> listProductoSac = new List<SelectListItem>();
            try
            {
                dt = GetProductosSac(producto);
                var selectList = new SelectList(dt.AsDataView(), "CODIGO", "NOMBRE");
                if (selectList != null)
                {
                    listProductoSac.AddRange(selectList);
                    if (firstItemToInsert != null && producto == "")
                    {
                        listProductoSac.Insert(0, firstItemToInsert);
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listProductoSac;
        }

        /// <summary>
        /// Obtiene una lista de tipo List<ProductoSAC> con los
        /// productos SAC
        /// </summary>
        /// <returns></returns>
        public static List<ProductoSAC> GetProductosSacToList()
        {
            DataTable dt = null;
            List<ProductoSAC> listProductoSac = new List<ProductoSAC>();
            try
            {
                dt = GetProductosSac();
                listProductoSac = dt.AsEnumerable().Select(i => new ProductoSAC()
                {
                    Id = i.Field<string>("CODIGO"),
                    Nombre = i.Field<string>("NOMBRE"),
                    Alias = i.Field<string>("ALIAS")
                }).ToList();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listProductoSac;
        }

        //Obtiene un datatable de Productos SAC
        public static DataTable GetProductosSac(string producto = "")
        {
            DataTable dt = null;
            try
            {
                dt = ExecStoreProcedure("sp_getProductosSAC", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nombreFiltro",
                        DbType= DbType.String,
                        Value= producto
                    },
                });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene una lista de las órdenes de ingreso y el proveedor que le corresponde
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetOIProveedor()
        {
            List<SelectListItem> listOIProveedor = new List<SelectListItem>();
            try
            {
                var proveedoresSAC = GetProveedoresSACToList();
                using (var context = new DMMeatWeigherModel())
                {
                    listOIProveedor = (
                                from oi in context.OrdenIngreso.AsEnumerable()
                                join prv in proveedoresSAC on oi.CodigoProveedorSAC.Trim() equals prv.Id.Trim()
                                select new SelectListItem
                                {
                                    Text = oi.Id.ToString() + " - " + prv.Nombre,
                                    Value = oi.Id.ToString()
                                }).ToList();

                    if (listOIProveedor == null)
                        listOIProveedor = new List<SelectListItem>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listOIProveedor;
        }

        /// <summary>
        /// Retorna lista de OI con nombres de proveedor que le corresponde.
        /// La misma es preparada para listas de seleccion
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetOIProveedorToListSelectListItem()
        {
            var oiPrvs = GetOIProveedor().OrderByDescending(x => Convert.ToInt32(x.Value)).ToList();
            oiPrvs.Insert(0, new SelectListItem() { Value = "0", Text = "Todos" });
            return oiPrvs;
        }

        //Obtiene lista de Clientes desde PHY x Código
        public static List<SelectListItem> GetClientesxCodigoToListSelectListItem(string cliente = "")
        {

            DataTable dt = null;
            List<SelectListItem> listClientes = new List<SelectListItem>();
            listClientes.Insert(0, new SelectListItem() { Text = "Todos", Value = "" });
            try
            {
                dt = ExecStoreProcedure("sp_getClientesSAC", new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nameFilter",
                        DbType= DbType.String,
                        Value= cliente
                    },
                });
                foreach (DataRow fila in dt.Rows)
                {
                    string codigo = fila["CODIGO"].ToString();
                    string nombreCliente = fila["NOMBRE"].ToString();
                    listClientes.Add(new SelectListItem
                    {
                        Text = nombreCliente,
                        Value = codigo
                    });
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listClientes;
        }

        //Devuelve una lista de Producto con informacion de su contenido de insumos primarios y secundarios
        public static List<InsumosEnProducto> GetInsumosEnProductos()
        {
            List<InsumosEnProducto> listInsumosEnProductos = new List<InsumosEnProducto>();
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listInsumosEnProductos = (
                                from p in context.Productos
                                join pi in context.ProductoInsumos on p.Id equals pi.IdProducto
                                select new
                                {
                                    IdProducto = p.Id,
                                    NombreProducto = p.Nombre,
                                    InsumosPrimarios = GetInsumosEnProducto(p.Id)
                                }).AsEnumerable().Select(x => new InsumosEnProducto()
                                {
                                    IdProducto = x.IdProducto,
                                    NombreProducto = x.NombreProducto,
                                    InsumosPrimarios = x.InsumosPrimarios
                                }).ToList();

                    if (listInsumosEnProductos == null)
                        listInsumosEnProductos = new List<InsumosEnProducto>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listInsumosEnProductos;
        }
        //Devuelve una lista de Insumos Primarios que posee un Producto.
        //Cada insumo primario tambien poseera una lista de secundarios si es que los tiene
        // producto-->  Insumo 1    insumo secundario 1
        //                          insumo secundario 2
        //              Insumo 2
        //
        //              Insumo 3    insumo secundario 1
        //                          insumo secundario 2
        public static List<InsumoPrimario> GetInsumosEnProducto(int idProducto)
        {
            List<InsumoPrimario> listInsumosPrimarios = new List<InsumoPrimario>();
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listInsumosPrimarios = (
                                from p in context.Productos
                                join pi in context.ProductoInsumos on p.Id equals pi.IdInsumoPrimario
                                where pi.IdProducto == idProducto && pi.IdInsumoPrimario == pi.IdInsumoSecundario
                                select new 
                                {
                                    Id = pi.IdInsumoPrimario,
                                    Nombre = p.Nombre,
                                    Unidades = pi.Unidades,
                                    RequiereConfirmacion = pi.RequiereConfirmacion,
                                }).AsEnumerable().Select(x => new InsumoPrimario()
                                { 
                                    Id=x.Id,
                                    Nombre = x.Nombre,
                                    Unidades = x.Unidades,  
                                    RequiereConfirmacion=x.RequiereConfirmacion,
                                    InsumosSecundarios= GetInsumosSecundariosEnInsumoPrimario(idProducto, x.Id)
                                }).ToList();

                    if (listInsumosPrimarios == null)
                        listInsumosPrimarios = new List<InsumoPrimario>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listInsumosPrimarios;
        }

        //Devuelve una lista de Insumos Secundarios que posee un insumo primario de un producto
        public static List<InsumoSecundario> GetInsumosSecundariosEnInsumoPrimario(int idProducto, int idInsumoPrimario)
        {
            List<InsumoSecundario> listInsumosSecundarios = new List<InsumoSecundario>();
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    listInsumosSecundarios = (
                                from p in context.Productos
                                join pi in context.ProductoInsumos on p.Id equals pi.IdInsumoSecundario
                                where pi.IdProducto == idProducto && pi.IdInsumoPrimario == idInsumoPrimario && pi.IdInsumoPrimario != pi.IdInsumoSecundario
                                select new InsumoSecundario()
                                {
                                    Id = pi.IdInsumoPrimario,
                                    Nombre = p.Nombre,
                                    Unidades = pi.Unidades,
                                    RequiereConfirmacion = pi.RequiereConfirmacion,
                                }).ToList();

                    if (listInsumosSecundarios == null)
                        listInsumosSecundarios = new List<InsumoSecundario>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listInsumosSecundarios;
        }

        //Devuelve una lista con los productos que no son insumos, ni combos ni cajas
        public static List<SelectListItem> GetProductosNoCajasCombosInsumos()
        {
            List<SelectListItem> listProductos = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {

                    listProductos = (from p in context.Productos
                                     where (p.EsCaja == false && p.EsCombo == false
                                     && p.EsInsumo == false) || (p.EsCaja == null && p.EsCombo == false
                                     && p.EsInsumo == false) || (p.EsCaja == false && p.EsCombo == null
                                     && p.EsInsumo == false) || (p.EsCaja == false && p.EsCombo == false
                                     && p.EsInsumo == null)
                                     select new SelectListItem()
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Nombre.ToString()
                                     }).ToList();
                }
                listProductos.Insert(0, new SelectListItem() { Text = "Seleccionar", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return listProductos;
        }

        //Devuelve una lista con los contextos de un evento Log
        public static List<SelectListItem>GetContextosDbLog()
        {
            List<SelectListItem> listContextos = null;

            try
            {
                using(var context = new DMMeatWeigherModel())
                {
                    listContextos = (from dblog in context.dbLog
                                     select new  SelectListItem()
                                     {
                                         Value = dblog.Contexto.ToString(),
                                         Text = dblog.Contexto.ToString()
                                     }).Distinct().ToList();
                }
                listContextos.Insert(0, new SelectListItem() { Text = "TODOS", Value = "" });
            }
            catch (Exception e) 
            {
                throw (e);
            }
            return listContextos;
        }

        //Devuelve una lista con los eventos de DbLog que corresponden al contexto recibido como parámetro
        public static List<SelectListItem> GetEventosDbLog(string contexto)
        {
            List<SelectListItem> listEventos = null;

            try
            {
                using(var context = new DMMeatWeigherModel())
                {
                    listEventos = (from dblog in context.dbLog
                                   where dblog.Contexto == contexto
                                   select new SelectListItem()
                                   {
                                       Text = dblog.Evento.ToString(),
                                       Value = dblog.Evento.ToString()
                                   }).Distinct().ToList();
                }
            } catch (Exception e)
            {
                throw (e);
            }
            return listEventos;
        }

        //Devuelve una lista con todos los productos del tipo CAJA
        public static List<SelectListItem> GetCajas()
        {
            List<SelectListItem> cajas = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {

                    cajas = (from p in context.Productos
                                     where p.EsCaja == true
                                     select new SelectListItem()
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Nombre.ToString()
                                     }).ToList();
                }
                cajas.Insert(0, new SelectListItem() { Text = "--Seleccionar Caja", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return cajas;
        }

        //Devuelve una lista con todos los productos del tipo CAJA sin producto asingado
        public static List<SelectListItem> GetCajasSinProducto()
        {
            List<SelectListItem> cajas = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {
                    cajas = (from p in context.Productos
                             join c in context.Cajas on p.Id equals c.IdProductoCaja
                             where p.EsCaja == true
                             select new SelectListItem()
                             {
                                 Value = p.Id.ToString(),
                                 Text = p.Nombre.ToString()
                             }).ToList();
                }
                cajas.Insert(0, new SelectListItem() { Text = "--Seleccionar Caja", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return cajas;
        }

        //Devuelve una lista con todos los combos
        public static List<SelectListItem> GetCombos()
        {
            List<SelectListItem> combos = null;
            try
            {
                using (var context = new DMMeatWeigherModel())
                {

                    combos = (from p in context.Productos
                             where p.EsCombo == true
                             select new SelectListItem()
                             {
                                 Value = p.Id.ToString(),
                                 Text = p.Nombre.ToString()
                             }).ToList();
                }
                combos.Insert(0, new SelectListItem() { Text = "--Seleccionar Combo", Value = "0" });
            }
            catch (Exception e)
            {
                throw (e);
            }
            return combos;
        }

        //Devuelve una lista con los insumos y sus unidades en stock
        public static List<StockInsumo> GetStockInsumos()
        {
            DataTable dt = null;
            List<StockInsumo> listStockInsumos = new List<StockInsumo>();
            try
            {
                //ID,INSUMO,UNDS
                dt = ExecStoreProcedure("sp_getUnidadesStockInsumos", null);
                listStockInsumos = (from si in dt.AsEnumerable()
                               select new StockInsumo
                               {
                                   Id = TOOLS.Tools.GetValueColumn<int>(si, "ID"),
                                   Insumo = TOOLS.Tools.GetValueColumn<string>(si, "INSUMO"),
                                   Unds = TOOLS.Tools.GetValueColumn<float>(si, "UNDS"),
                               }).OrderBy(x => x.Insumo).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return listStockInsumos;
        }
        //Devuelve el stock de un insumo
        public static StockInsumo GetStockInsumo(int idInsumoBuscado)
        {
            DataTable dt = null;
            StockInsumo stockInsumo = new StockInsumo();
            try
            {
                //ID,INSUMO,UNDS
                dt = ExecStoreProcedure("sp_getUnidadesStockInsumos", null);
                stockInsumo = (from si in dt.AsEnumerable()
                               where TOOLS.Tools.GetValueColumn<int>(si, "ID") == idInsumoBuscado
                               select new  StockInsumo
                               {
                                   Id = TOOLS.Tools.GetValueColumn<int>(si, "ID"),
                                   Insumo= TOOLS.Tools.GetValueColumn<string>(si, "INSUMO"),
                                   Unds = TOOLS.Tools.GetValueColumn<float>(si, "UNDS"),
                               }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return stockInsumo;
        }

        #endregion

        #region UPDATES ENTITYS
        public static bool Update_Producto(Producto model, out ResultValidate resultValidation)
        {
            bool updated = false;
            resultValidation = new ResultValidate();

            using (var context = new DMMeatWeigherModel())
            {
                var data = context.Productos.FirstOrDefault(x => x.Id == model.Id);
                if (data != null)
                {
                    data.CodigoProductoSac = model.CodigoProductoSac;
                    data.Nombre = model.Nombre;
                    data.IdTipo = model.IdTipo;
                    data.NumSenasa = model.NumSenasa;
                    data.PesoNetoPredef = model.PesoNetoPredef;
                    data.UnidadesPredef = model.UnidadesPredef;
                    data.PesoTaraPredef = model.PesoTaraPredef;
                    data.DiasVencimiento = model.DiasVencimiento;
                    data.EsInsumo = model.EsInsumo;
                    data.EsPesable = model.EsPesable;
                    data.TextAuxL1 = model.TextAuxL1;
                    data.TextAuxL2 = model.TextAuxL2;
                    data.NombreL1 = model.NombreL1;
                    data.NombreL2 = model.NombreL2;
                    data.NombreL3 = model.NombreL3;
                    data.NombreL4 = model.NombreL4;
                    data.NombreL5 = model.NombreL5;
                    data.NombreL6 = model.NombreL6;
                    data.RendimientoSTD = model.RendimientoSTD;
                    data.EsCombo = model.EsCombo;
                    data.EsCaja = model.EsCaja;
                    data.EsTropa = model.EsTropa;
                    data.IdEtiqueta = model.IdEtiqueta;
                    data.TipoBulto = model.IdEtiqueta == null ? "" : context.Etiquetas.FirstOrDefault(x => x.Id == model.IdEtiqueta).IdTipoBulto;
                }
                resultValidation = DbServices.ValidateUpdate_Producto(data);
                if (resultValidation.Validated)
                {
                    context.SaveChanges();
                    updated = true;
                }
            }
            return updated;
        }
        #endregion
    }
}
