using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Extensions;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Movimientos de Insumos
    /// </summary>
    public static partial class CDb
    {
        #region OPERACIONES DE REGISTRACION DE INSUMOS


        /***************************************************************************************
         * Metodo:	    GetInsumosPedido
         *              Obtiene una lista de Insumos que se registraron para un pedido
         * Parametro:   
         * Retorna:     List<CItemInsumoPedido> lista de insumos.
        *****************************************************************************************/
        public static List<CItemInsumoPedido> GetInsumosPedido(int idPedido)
        {
            CItemInsumoPedido datInsumo;


            List<CItemInsumoPedido> listInsumos = new List<CItemInsumoPedido>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPedido",
                    DbType = DbType.Int32,
                    Value = idPedido
                });
                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PRE,TARA_PRE,UNDS_PRE,REND,VENC,INS,PES,ESINSUMO,ESPESABLE,NOMBRE_L1,
                //NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUXL1,TEXTAUXL2,UNIDADES_INSUMO,IDPEDIDO

                DataTable dt = SelectStoreProcedure("sp_getDetalleInsumosPedido", "INSUMOSPEDIDO", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datInsumo = new CItemInsumoPedido()
                        {
                            Id = GetValueColumn<int>(dr, "ID"),
                            Nombre = GetValueColumn<string>(dr, "NOMBRE"),
                            m_tipo = new CTipoProducto()
                            {
                                Id = GetValueColumn<int>(dr, "IDTIPO"),
                                Nombre = GetValueColumn<string>(dr, "TIPO")
                            },
                            ProductoSAC = new CProductoSAC()
                            {
                                Codigo = GetValueColumn<string>(dr, "CODIGO_SAC"),
                                Nombre = GetValueColumn<string>(dr, "NOMBRE_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Unidades = GetValueColumn<float>(dr, "UNIDADES_INSUMO"),
                            IdPedido = GetValueColumn<int>(dr, "IDPEDIDO")

                        };
                        listInsumos.Add(datInsumo);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listInsumos;
        }
        /***************************************************************************************
         * Metodo:	    GetComposicionInsumoProducto
         *              Obtiene una lista de Insumos que integran un producto de tipo pieza-caja-combo
         * Parametro:   
         * Retorna:     List<CItemProductoInsumo> lista de insumos.
        *****************************************************************************************/
        public static List<CItemInsumoProducto> GetComposicionInsumoProducto(int idProducto)
        {
            CItemInsumoProducto datProducto;
            
            
            List<CItemInsumoProducto> listInsumos = new List<CItemInsumoProducto>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    DbType = DbType.Int32,
                    Value = idProducto
                });
                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PRE,TARA_PRE,UNDS_PRE,REND,VENC,INS,PES,ESINSUMO,ESPESABLE,NOMBRE_L1,
                //NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUXL1,TEXTAUXL2,UNIDADES_INSUMO,IDINSUMO_PRIMARIO

                DataTable dt = SelectStoreProcedure("sp_getDetalleInsumosProducto", "INSUMOSPRODUCTO", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datProducto = new CItemInsumoProducto()
                        {
                            Id = GetValueColumn<int>(dr, "ID"),
                            Nombre = GetValueColumn<string>(dr, "NOMBRE"),
                            m_tipo = new CTipoProducto()
                            {
                                Id = GetValueColumn<int>(dr, "IDTIPO"),
                                Nombre = GetValueColumn<string>(dr, "TIPO")
                            },
                            ProductoSAC = new CProductoSAC()
                            {
                                Codigo = GetValueColumn<string>(dr, "CODIGO_SAC"),
                                Nombre = GetValueColumn<string>(dr, "NOMBRE_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Unidades = GetValueColumn<float>(dr, "UNIDADES_INSUMO"),
                            IdInsumoPrimario = GetValueColumn<int>(dr, "IDINSUMO_PRIMARIO"),
                            RequiereConfirmacion = GetValueColumn<bool>(dr, "REQUIERE_CONFIRMACION")

                        };
                        listInsumos.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listInsumos;
        }

        /***************************************************************************************
         * Metodo:	    RegistrarNuevoInsumoProducto
         *              Realiza la registracion de un nuevo insumo para un producto en la tabla ProductoInsumos
         *              
         * Parametro:   int idProducto,int idProductoInsumoPrimario,int idProductoInsumoSecundario,float unidades
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool RegistrarNuevoInsumoProducto(int idProducto, int idInsumoPrimario, int idInsumoSecundario, float unidades, bool requiereConfirmacion)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_registrarInsumoProducto", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idInsumoPrimario",
                        DbType = DbType.Int32,
                        Value = idInsumoPrimario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idInsumoSecundario",
                        DbType = DbType.Int32,
                        Value = idInsumoSecundario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@unidades",
                        DbType = DbType.Single,
                        Value = unidades
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@requiereConfirmacion",
                        DbType = DbType.Boolean,
                        Value = requiereConfirmacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    registerOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;

        }
        /***************************************************************************************
         * Metodo:	    ActualizarInsumoProducto
         *              Realiza la actualizacion de unidades de un insumo producto en la tabla ProductoInsumos
         *              
         * Parametro:   int idProducto,int idProductoInsumoPrimario,int idProductoInsumoSecundario,float unidades
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool ActualizarInsumoProducto(int idProducto, int idInsumoPrimario, int idInsumoSecundario, float unidades,bool requiereConfirmacion)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_actualizarInsumoProducto", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = idProducto
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idInsumoPrimario",
                        DbType = DbType.Int32,
                        Value = idInsumoPrimario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idInsumoSecundario",
                        DbType = DbType.Int32,
                        Value = idInsumoSecundario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@unidades",
                        DbType = DbType.Single,
                        Value = unidades
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@requiereConfirmacion",
                        DbType = DbType.Boolean,
                        Value = requiereConfirmacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    registerOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;

        }
        /***************************************************************************************
         * Metodo:	    RegistrarMovimientoInsumo
         *              Realiza la registracion de un movimiento de un insumo
         *              
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool RegistrarMovimientoInsumo(TYPE_INSUMO_MOV tipoMov, TYPE_INSUMO_PROC tipoProc ,int idProc, int idPrdInsumo,float unidades)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_registrarMovInsumo", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipoMov",
                        DbType = DbType.String,
                        Size=3,
                        Value = Extensions.Extensions.ToStringValue(tipoMov)
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipoProc",
                        DbType = DbType.String,
                        Size = 3,
                        Value = Extensions.Extensions.ToStringValue(tipoProc)
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProc",
                        DbType = DbType.Int32,
                        Value = idProc
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idPrdInsumo
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@unidades",
                        DbType = DbType.Single,
                        Value = unidades
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    registerOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;

        }
        /***************************************************************************************
         * Metodo:	    ActualizarUnidadesMovimientoInsumo
         *              Realiza la actualizacion de unidades de un movimiento de un insumo
         *              
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool ActualizarUnidadesMovimientoInsumo(TYPE_INSUMO_MOV tipoMov, TYPE_INSUMO_PROC tipoProc, int idProc, int idPrdInsumo, float unidades)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_actualizarUnidadesMovInsumo", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipoMov",
                        DbType = DbType.String,
                        Size = 3,
                        Value = Extensions.Extensions.ToStringValue(tipoMov)
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipoProc",
                        DbType = DbType.String,
                        Size = 3,
                        Value = Extensions.Extensions.ToStringValue(tipoProc)
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProc",
                        DbType = DbType.Int32,
                        Value = idProc
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idPrdInsumo
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@unidades",
                        DbType = DbType.Single,
                        Value = unidades
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    registerOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;

        }
        /***************************************************************************************
         * Metodo:	    RegistrarMovimientoInsumos
         *              Realiza la registracion de un movimiento de una lista de insumos
         *              
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool RegistrarMovimientoInsumos(TYPE_INSUMO_MOV tipoMov, TYPE_INSUMO_PROC tipoProc,int idProc,List<CItemInsumoProductoEnProceso> listInsumos)
        {
            bool registerOk = false;
            try
            {
                foreach (CItemInsumoProductoEnProceso item in listInsumos)
                {
                    registerOk = CDb.RegistrarMovimientoInsumo(tipoMov, tipoProc, idProc, item.IdInsumoSelected, item.Unidades);
                    if (!registerOk) break;
                }

            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;

        }

        /***************************************************************************************
         * Metodo:	    EliminarMovimientoInsumo
         *              Realiza la eliminacion de los moviminetos de insumos registrados bajo la clave
         *              Tipo de Proceso de Insumo y Id del proceso.
         *              Por ejemplo para eliminar la registracion de un movimiento de insumo en ingreo 
         *              a planta la clave sera :
         *              "IPL" ,ID = id del registro de pesaje.
         *              Para el caso de la eliminacion de registraciones de insumos de un contenedor
         *              sera:
         *              "CNT" , ID= id del contenedor.
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarMovimientoInsumo(TYPE_INSUMO_PROC tipoProc, int idProc)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_borrasMovInsumo", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipoProc",
                        DbType = DbType.String,
                        Size = 3,
                        Value = Extensions.Extensions.ToStringValue(tipoProc)
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProc",
                        DbType = DbType.Int32,
                        Value = idProc
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    registerOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;

        }

        /***************************************************************************************
         * Metodo:	    AjustarStockInsumo
         *              Realiza la actualizacion de unidades en stock de un insumo.
         *              Se especifica el codigode articulo del insumo y las unidades de stock a establecer.
         * Parametro:   int idProductoInsumo,float UnidadesStock
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool AjustarStockInsumo(int idProductoInsumo,float UnidadesStock)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_ajustarStockInsumo", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPrdInsumo",
                        DbType = DbType.Int32,
                        Value = idProductoInsumo
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@unidadesEstablecer",
                        DbType = DbType.Single,
                        Value = UnidadesStock
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    registerOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registerOk;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE REGISTRACIONES DE INSUMOS
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_InsumosDetalleProduccion
         *              Obtiene un DataSet con una consulta de detalles de Movimientos de insumos en produccion
         *              basado en fechas.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_InsumosEnProduccionDetallado(DateTime fechaDesde, DateTime fechaHasta, int idProductoInsumo = 0, string tipoBulto = "")
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Value = fechaDesde
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPrdInsumo",
                    OleDbType = OleDbType.Integer,
                    Value = idProductoInsumo
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@tipoBulto",
                    OleDbType = OleDbType.VarChar,
                    Size = 20,
                    Value = tipoBulto == "TODOS" ? "" : tipoBulto
                });
                //TIPO,NRO,LOTE,PRODUCTO,NETO,INSUMO,UNDS,FECHA
                dt = SelectStoreProcedure("sp_repInsumosEnProduccionDetallado", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_InsumosEnProduccionTotalizado
         *              Obtiene un DataSet con una consulta de Movimientos de Totales de pesajes
         *              de insumos en produccion con filtro por fecha.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_InsumosEnProduccionTotalizado(DateTime fechaDesde, DateTime fechaHasta, int idProductoInsumo = 0, string tipoBulto = "")
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Value = fechaDesde
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPrdInsumo",
                    OleDbType = OleDbType.Integer,
                    Value = idProductoInsumo
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@tipoBulto",
                    OleDbType = OleDbType.VarChar,
                    Size = 20,
                    Value = tipoBulto == "TODOS" ? "" : tipoBulto
                });
                //TIPO,INSUMO,UNDS
                dt = SelectStoreProcedure("sp_repInsumosEnProduccionTotalizado", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_InsumosEnEgresosDetallado
         *              Obtiene un DataSet con una consulta de detalles de Movimientos de insumos en egresos
         *              basado en fechas.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_InsumosEnEgresosDetallado(DateTime fechaDesde, DateTime fechaHasta,string comprobante="",string cliente="", int idProductoInsumo = 0)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Value = fechaDesde
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    OleDbType = OleDbType.VarChar,
                    Size = 20,
                    Value = comprobante
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@cliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 50,
                    Value = cliente
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPrdInsumo",
                    OleDbType = OleDbType.Integer,
                    Value = idProductoInsumo
                });
                //PEDIDO,COMPROBANTE,CLIENTE,INSUMO,UNDS,FECHA
                dt = SelectStoreProcedure("sp_repInsumosEnEgresosDetallado", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_InsumosEnEgresosTotalizado
         *              Obtiene un DataSet con una consulta de totalizados de Movimientos de insumos en egresos
         *              basado en fechas.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_InsumosEnEgresosTotalizado(DateTime fechaDesde, DateTime fechaHasta, string cliente = "", int idProductoInsumo = 0)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Value = fechaDesde
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@cliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 50,
                    Value = cliente
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPrdInsumo",
                    OleDbType = OleDbType.Integer,
                    Value = idProductoInsumo
                });
                //PEDIDO,COMPROBANTE,CLIENTE,INSUMO,UNDS,FECHA
                dt = SelectStoreProcedure("sp_repInsumosEnEgresosTotalizado", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetUnidadesEnStockInsumos
         *              Obtiene un DataSet con una consulta de Existencia en stock de Insumos.
         * Parametro    
         * Parametro    
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetUnidadesEnStockInsumos()
        {
            DataTable dt = null;
            try
            {
                //ID,INSUMO,UNDS
                dt = SelectStoreProcedure("sp_getUnidadesStockInsumos", "EXISTENCIA_STOCK");
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockInsumos
         *              Obtiene un DataSet con una consulta de Existencia en stock de Insumos.
         * Parametro    
         * Parametro    
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockInsumos(DateTime fechaHasta, int idPrdInsumo=0)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPrdInsumo",
                    OleDbType = OleDbType.Integer,
                    Value = idPrdInsumo
                });
                //INSUMO,UNDS
                dt = SelectStoreProcedure("sp_repExistenciaEnStockInsumos", "EXISTENCIA_STOCK", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        #endregion

    }
}
