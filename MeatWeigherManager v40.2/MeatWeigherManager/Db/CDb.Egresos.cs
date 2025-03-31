using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Pesajes en Produccion
    /// </summary>
    public static partial class CDb
    {

        #region OPERACIONES DE EGRESOS DE PEDIDO

        /***************************************************************************************
         * Metodo:	    RegisterEgressPart
         *              Registra un egreso para una Pieza
         * Parametro:   int idPieza,int idPedido
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool RegisterEgressPart(CPEgreso regDatPart)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_registrarEgresoPieza", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = regDatPart.Pieza.Id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = regDatPart.IdPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        DbType = DbType.Int32,
                        Value = m_OperadorActivo.m_idEstacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        DbType = DbType.Int32,
                        Value = m_OperadorActivo.m_id
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
         * Metodo:	    RegisterEgressContainer
         *              Registra un egreso para un contenedor
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool RegisterEgressContainer(CContenedor regDatContainer, int idPedido)
        {
            bool registerOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_registrarEgresoContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idContenedor",
                        DbType = DbType.Int32,
                        Value = regDatContainer.Id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        DbType = DbType.Int32,
                        Value = m_OperadorActivo.m_idEstacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        DbType = DbType.Int32,
                        Value = m_OperadorActivo.m_id
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

        /**************************************************************************************************
         * Metodo:		GenerarPedido
         * Descripcion: Crea un nuevo registro de pedido a partir un los datos pasados en la clase CPEDIDO
         * Parametro:	(ref CPedido) regPedido: Todos los datos para insertar el registro
         * Retorna:     Retorna tru si la registracion fue ok.
        ***************************************************************************************************/
        public static bool GenerarPedido(ref CPedido regPedido)
        {
            bool registracionOk = false;

            int regAfectados;

            regPedido.Operador.m_id = m_OperadorActivo.m_id;
            regPedido.Activo = true;

            string strCmd = String.Format(" INSERT INTO Pedidos(FECHA_HORA,IDOPERADOR,ACTIVO,CODIGOCLIENTESAC,CODIGOPEDIDOSAC,COMPROBANTEPEDIDOSAC,TIPOPEDIDOSAC)" +
                                          " VALUES({{ts '{0}'}},{1},{2},'{3}',{4},'{5}','{6}')",
                                          regPedido.FechaHoraPreparacion.ToString("yyyy-MM-dd HH:mm:ss"),
                                          regPedido.Operador.m_id,
                                          1,
                                          regPedido.Cliente.Codigo,
                                          regPedido.CodigoPedidoSAC,
                                          regPedido.ComprobantePedidoSAC,
                                          regPedido.TipoPedidoSAC);

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                registracionOk = (regAfectados == 1);
                if (registracionOk)
                    regPedido.Id = GetLastIdPedido();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registracionOk;
        }

        /***************************************************************************************
         * Metodo:	    GetLastIdPedido
         *              Obtiene el ultimo numero de pedido generado por esta estacion .
         * Retorna:     (int) idpedido.
        *****************************************************************************************/
        public static int GetLastIdPedido()
        {
            OleDbDataReader recordSet;
            int maxId = 0;
            string strCmd = " SELECT max(id) as ID FROM PEDIDOS ";
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    maxId = GetCampoDbInt(recordSet, "ID");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return maxId;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE EGRESOS

        /***************************************************************************************
        Metodo:		GetDatSet_BultosEgresadosPorPedido
                    Crea un dataset con los bultos egresados para un dado pedido.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_BultosEgresadosPorPedido(int idPedido, out DataTable dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataTable();
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    List<OleDbParameter> lparam = new List<OleDbParameter>();
                    lparam.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    //TIPO,NRO,PRODUCTO,LOTE,UNDS,NETO,FECHA_EGRESO,OPERADOR
                    dsPiezas = SelectStoreProcedure("sp_getDetalleBultosEgresadosPorPedido", "BULTOS_REG_PEDIDO", lparam);
                    obtenidoSinerrorOk = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }
        /***************************************************************************************
        Metodo:		GetDatSet_PiezasEgresadasPedidos
                    Crea un dataset con las piezas egresadas para todos los pedidos.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_BultosEgresadasPedidos(out DataTable dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataTable();
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //IDPEDIDO,TIPO,NRO,PRODUCTO,LOTE,UNDS,NETO,FECHA_EGRESO,OPERADOR
                    dsPiezas = SelectStoreProcedure("sp_getDetalleBultosEgresadosPorPedidos", "BULTOS");
                    obtenidoSinerrorOk = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }

        /***************************************************************************************
        Metodo:		GetDatSet_TotalesPorProductoDePiezasEgresadasPedido
                    Crea un dataset con los totales por producto de piezas egresadas
                    para un dado pedido.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDataTable_TotalesPorProductoDePiezasEgresadasPedido(int idPedido,out DataTable dtTotales)
        {
            bool obtenidoSinerrorOk = false;
            dtTotales = new DataTable();
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    List<OleDbParameter> lparam = new List<OleDbParameter>();
                    lparam.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });
                    //ITEM_PRD_SAC,IDPRODUCTO,CODIGO_SAC,PRODUCTO_SAC,OBSERVACION,UNDS_PED,PESO_PED,UNDS_COL,PESO_COL
                    dtTotales = SelectStoreProcedure("sp_getPiezasEgresadasPorPedido", "TOTALES_PEDIDO", lparam);
                    obtenidoSinerrorOk = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }

        /***************************************************************************************
        Metodo:		GetPedidosActivos
                    Crea un dataset con los pedidos que se encuentran activos (no cerrados).
                    para una fecha de entrega especificada y un numero de comprobante especificado.
                    El numero de comprobante puede buscarce exacto o por aproximacion segun parametro.
        Parametros:	out DataSet Pedidos 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetPedidosActivos(DateTime fechaEntrega,string queryFilterNumComprobante, out DataTable dtPedidos,bool serchComprobanteExact = false)
        {
            bool obtenidoSinerrorOk = false;
            dtPedidos = new DataTable();

            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@fechaPreparacion",
                    DbType = DbType.String,
                    Size = 12,
                    Value = fechaEntrega == DateTime.MinValue ? "" : fechaEntrega.ToString("yyyy-MM-dd")
                }) ;
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    DbType = DbType.String,
                    Value = queryFilterNumComprobante
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@exactComprobante",
                    DbType = DbType.Boolean,
                    Value = serchComprobanteExact
                });
                //CodigoClienteSAC,CLIENTE,CodigoPedidoSAC,TipoPedidoSAC,COMPROBANTE,FECHA,ID,ACTIVO,IDOPERADOR,OPERADOR,PASW_OPERADOR,TIPO_OPERADOR

                dtPedidos = SelectStoreProcedure("sp_getPedidosPendientesVenta", "PEDIDOS", lparam);
                obtenidoSinerrorOk = true;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }
        /***************************************************************************************
        Metodo:		GetDetalleProductosPedidosActivos
                    Crea un datatable con todos los productos de los pedidos activos que tengan
                    la fecha de entrega especificada.
                    Se informa por cada producto los datos del pedido.
        Parametros:	out DataTable Productos 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDetalleProductosPedidosActivos(DateTime fechaEntrega, string queryFilterNumComprobante, out DataTable dtPedidos, bool serchComprobanteExact = false)
        {
            bool obtenidoSinerrorOk = false;
            dtPedidos = new DataTable();

            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@fechaPreparacion",
                    DbType = DbType.Date,
                    Value = fechaEntrega
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    DbType = DbType.String,
                    Value = queryFilterNumComprobante
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@exactComprobante",
                    DbType = DbType.Boolean,
                    Value = serchComprobanteExact
                });

                //Comprobante,Cliente,CodigoSAC,ProductoSAC,Observacion,Unds_PED,Unds_PREP,Unds_REST,Peso_PED,Peso_PREP,Peso_REST

                dtPedidos = SelectStoreProcedure("sp_getDetalleProductosPedidoPendienteVentaPorPreparacion", "PEDIDOS", lparam);
                obtenidoSinerrorOk = true;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }

        /***************************************************************************************
        Metodo:		GetTotalizadoProductosPedidosActivos
                    Crea un datatable con todos los productos de los pedidos activos que tengan
                    la fecha de entrega especificada.
                    Solo se informa los totales de cantidades por cada producto.
        Parametros:	out DataTable Productos 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetTotalizadoProductosPedidosActivos(DateTime fechaEntrega, out DataTable dtPedidos)
        {
            bool obtenidoSinerrorOk = false;
            dtPedidos = new DataTable();

            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@fechaPreparacion",
                    DbType = DbType.Date,
                    Value = fechaEntrega
                });

                //CodigoSAC,ProductoSAC,Observacion,Unds_PED,Unds_PREP,Unds_REST,Peso_PED,Peso_PREP,Peso_REST

                dtPedidos = SelectStoreProcedure("sp_getTotalizadoProductosPedidoPendienteVentaPorPreparacion", "PEDIDOS", lparam);
                obtenidoSinerrorOk = true;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }

        /**************************************************************************************************
         * Metodo:		CerrarPedido
         * Descripcion: Marca a un Pedido como cerrado colocando el campo "ACTIVO" = 0
         * Parametro:	int idPedido.
         * Retorna:     Retorna tru si ok.
        ***************************************************************************************************/
        public static bool CerrarPedido(int idPedido)
        {
            bool borradoOk = false;
            int regAfectados;
            string strCmd = String.Format(" UPDATE PEDIDOS SET activo = {1} WHERE ID = {0}", idPedido, 0);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                borradoOk = (regAfectados == 1);
            }
            catch (OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return borradoOk;
        }

        /***************************************************************************************
         * Metodo:	    BorrarPiezaEgresadaDelPedido
         *              Elimina una pieza de un pedido especifico de la tabla egresos
         * Parametro:   int idPieza,int idPedido
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool BorrarPiezaEgresadaDelPedido(int idPieza, int idPedido, out string detailResult)
        {
            detailResult = "";
            bool borradoOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_borrarPiezaEgresada", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@error",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    borradoOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "" : dbCommand.Parameters["@error"].Value.ToString());

                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return borradoOk;
        }



        /***************************************************************************************
         * Metodo:	    IsValidPartForEgress
         *              Verifica si una pieza para un dado pedido es valida para poder realizar
         *              un egreso.
         * Parametro:   int idPieza,int idPedido
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidPartForEgress(int idPieza, int idPedido, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidaPiezaParaEgreso", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@error",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    validOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "" : dbCommand.Parameters["@error"].Value.ToString());

                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return validOk;
        }
        /***************************************************************************************
         * Metodo:	    IsValidContainerForEgress
         *              Verifica si un contenedor para un dado pedido es valido para poder realizar
         *              un egreso.
         * Parametro:   int idContenedor,int idPedido
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidContainerForEgress(int idContenedor, int idPedido, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoContenedorParaEgreso", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idContenedor
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@error",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    validOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "" : dbCommand.Parameters["@error"].Value.ToString());

                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return validOk;
        }

        /***************************************************************************************
         * Metodo:	    IsValidDeleteEgressPart
         *              Verifica si es posible eliminar un egreso de una pieza para un pedido dado
         * Parametro:   int idPieza,int idPedido
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidDeleteEgressPart(int idPieza, int idPedido, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoBorrarEgresoDePieza", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@error",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    validOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "" : dbCommand.Parameters["@error"].Value.ToString());

                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return validOk;
        }
        /***************************************************************************************
         * Metodo:	    IsValidDeleteEgressContainer
         *              Verifica si es posible eliminar un egreso de un contenedor para un pedido dado
         * Parametro:   int idContenedor,int idPedido
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidDeleteEgressContainer(int idContenedor, int idPedido, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoBorrarEgresoDeContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idContenedor",
                        DbType = DbType.Int32,
                        Value = idContenedor
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@error",
                        DbType = DbType.String,
                        Size = 100,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    validOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "" : dbCommand.Parameters["@error"].Value.ToString());

                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return validOk;
        }
        /**************************************************************************************************
         * Metodo:		CrearRemitoDespachoSAC
         * Descripcion: Llama a un SP que realiza la creacion de un remito de despacho en el sistema SAC . Incluyendo
         *              en el los items de articulos colectados en el pedido.
         *              Al SP se le pasa la clave de pedido del SAC , el pedido de produccion y el numero de estacion.
         *              
         * Parametro:	int CodigoPedidoSAC : codigo del pedido SAC.
         *              string CodigoClienteSAC : codigo del cliente SAC vinculado al pedido como parte de la clave.
         *              int IdPedidoC : id pedido del sistema de produccion desde donde se tomaran los items a 
         *              registrar en el pedido.

         *              out string resultSP (retorna un string con el resultado de la accion del SP , puede ser:
         *              "OK:XXXXXXXXXXXX","ERROR:XXXXXXXX...."
         * Retorna:     Retorna tru si la llamada al SP se pudo realizar sin errores .
        ***************************************************************************************************/
        public static bool CrearRemitoDespachoSAC(int codigoPedidoSAC,string codigoClienteSAC,int idPedido,int idEstacion,out string resultSP)
        {

            bool callSPok = false;
            resultSP = "Error no identificado";

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_crearRemitoDespachoSAC", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName= "@CodigoPedidoSAC",
                        DbType = DbType.Int32,
                        Value=codigoPedidoSAC
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@CodigoClienteSAC",
                        DbType = DbType.String,
                        Size=12,
                        Value = codigoClienteSAC
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedido",
                        DbType = DbType.Int32,
                        Value = idPedido
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        DbType = DbType.Int32,
                        Value = idEstacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.String,
                        Size=100,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.ExecuteNonQuery();

                    resultSP = dbCommand.Parameters["@result"].Value.ToString();
                    callSPok = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }
        /**************************************************************************************************
         * Metodo:		SetStatusPedido
         * Descripcion: Marca a un Pedido como cerrado o activo
         * Parametro:	int idPedido,bool status.
         * Retorna:     Retorna tru si ok.
        ***************************************************************************************************/
        public static bool SetStatusPedido(int idPedido,STATUS_PEDIDO status)
        {
            bool actualizadoOk = false;
            int regAfectados;
            string strCmd = String.Format(" UPDATE PEDIDOS SET activo = {1} WHERE ID = {0}", idPedido, (int)status);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                actualizadoOk = (regAfectados == 1);
            }
            catch (OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return actualizadoOk;
        }

        /***************************************************************************************
        Metodo:		GetConsultaCompletaEgresos
                    Crea un dataset con toda la informacion de las piezas ingresadas a produccion 
                    por cada lote.
        Parametros:	out DataSet (infoLote)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaCompletaEgresos(out DataSet dsInfoPedidosEgresados)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoPedidosEgresados = new DataSet();
            DataTable dtPiezasEgresadas;

            string sqlQuery;

            DataColumn[] columnsPedidoKeys = null;
            DataColumn[] columnsEgresosKeys = null;
            DataRelation datRelacionPedido_Egreso;

            try
            {
                int rowsPedidoFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //IDPEDIDO,EMPRESA,CODCOM,PEDIDO,CLIENTE,FECHA_PEDIDO,ESTADO
                    sqlQuery =
                               " DECLARE @cliSAC TABLE( CODIGO char(12), NOMBRE char(40)) INSERT INTO @cliSAC EXEC sp_getClientesSAC " +
                               " SELECT DISTINCT e.idpedido as IDPEDIDO,p.ComprobantePedidoSAC as PEDIDO, cli.nombre as CLIENTE,p.fecha_hora as FECHA_PEDIDO," +
                               " (CASE WHEN p.activo = 0 OR p.activo is null THEN 'CERRADO' ELSE 'ABIERTO' END) as ESTADO FROM EGRESOS e " +
                               " LEFT OUTER JOIN Pedidos as p ON e.idpedido = p.id " +
                               " LEFT OUTER JOIN @cliSAC as cli ON cli.codigo = p.codigoClienteSAC " +
                               " order by FECHA_PEDIDO desc ";


                    

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsPedidoFills = oleDbDataAdapter.Fill(dsInfoPedidosEgresados, "PEDIDOS");

                    if (rowsPedidoFills != 0)
                    {
                        //IDPEDIDO,TIPO,NRO,PRODUCTO,LOTE,UNDS,NETO,FECHA_EGRESO,OPERADOR
                        GetDatSet_BultosEgresadasPedidos(out dtPiezasEgresadas);

                        //cargo el resultado del query en el DataSet como tabla "PESADAS"
                        dsInfoPedidosEgresados.Tables.Add(dtPiezasEgresadas);

                        columnsPedidoKeys = new DataColumn[] { dsInfoPedidosEgresados.Tables["PEDIDOS"].Columns["IDPEDIDO"] };

                        columnsEgresosKeys = new DataColumn[] { dsInfoPedidosEgresados.Tables["BULTOS"].Columns["IDPEDIDO"] };
                        //creo el objeto relacion 
                        datRelacionPedido_Egreso = new DataRelation("PEDIDOS_BULTOS", columnsPedidoKeys, columnsEgresosKeys, false);
                        //sumo el objeto relacion al dataset    
                        dsInfoPedidosEgresados.Relations.Add(datRelacionPedido_Egreso);

                    }
                    obtenidoSinerrorOk = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new CDbException("Error en Base de Datos: " + aex.Source + "--" + aex.Message);
            }
            return obtenidoSinerrorOk;
        }

        /// <summary>
        /// Obtiene la fecha y hora de la primer pieza egresada para el pedido indicado.
        /// Si no hay piezas para el pedido retorna la fecha y hora actual.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        public static DateTime GetFechaHoraPrimerPiezaEgresada(int idPedido)
        {
            DateTime dateFirstPart = DateTime.Now;
            OleDbDataReader recordSet;
            string sqlQuery;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = String.Format(" SELECT fecha_hora FECHA_HORA FROM EGRESOS WHERE idpedido = {0} order by fecha_hora desc ", idPedido);
                    OleDbCommand dbCommand = new OleDbCommand(sqlQuery, m_oleDbConnection);
                    recordSet = dbCommand.ExecuteReader();
                    if (recordSet.HasRows)
                    {
                        recordSet.Read();
                        dateFirstPart = GetCampoDbDateTime(recordSet, "FECHA_HORA");
                    }
                    recordSet.Close();
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return dateFirstPart;
        }
        
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_EgresosDetalleFull
         *              Obtiene un DataSet con una lista de piezas,cajas,combos egresados por rango de fechas
         *              y otros filtros como ser numero de comprobante, lote de pieza, nombre de
         *              cliente , nombre de transportista.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataTable 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_EgresosDetalleFull(DateTime fechaDesde, DateTime fechaHasta, string nombreCliente, string comprobantePedidoSAC, DateTime lote, string tipoBulto, int idTipoProducto,int idProducto)
        {
            //TIPO,NRO,LOTE,PEDIDO,ESTADO,CLIENTE,COD_PRD,TIPO_PRD,PRODUCTO,TROPA,TIPIF,ORIGEN,EGRESO,UNDS,NETO,OPERADOR
            DataTable dtResult = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@cliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 40,
                    Value = nombreCliente
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaHasta.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = comprobantePedidoSAC
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@lote",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = lote == DateTime.MinValue ? "" : lote.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@tipoBulto",
                    OleDbType = OleDbType.VarChar,
                    Size = 20,
                    Value = tipoBulto == "TODOS" ? "" : tipoBulto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idTipoProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idTipoProducto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idProducto
                });

                dtResult = SelectStoreProcedure("sp_repDetalleEgresosFull", "DETEGRESOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtResult;
        }


        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_EventosLog
         *              Obtiene un reporte de los eventos de log registrados para un rango dado de fechas
         *              y para un texto de busqueda de aproximacion para la columna detalle.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataTable 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_EventosLog(DateTime fechaDesde, DateTime fechaHasta, string detalleAprox ="")
        {
            //TIPO,NRO,LOTE,PEDIDO,CLIENTE,COD_PRD,TIPO_PRD,PRODUCTO,ORIGEN,EGRESO,UNDS,NETO,OPERADOR
            DataTable dtResult = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Size = 10,
                    Value = fechaDesde
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Size = 10,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@detalle",
                    OleDbType = OleDbType.VarChar,
                    Size = 100,
                    Value = detalleAprox
                });

                dtResult = SelectStoreProcedure("sp_repEventosLog", "EVENTOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtResult;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ArticulosParaPreparacionPedidosPorFecha
         *              Obtiene un DataSet con una lista de articulos que se incluyen en pedidos
         *              activos para un rango de fecha de cumplimiento 
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataTable 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ArticulosParaPreparacionPedidosPorFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            //CodigoSAC,ProductoSAC,Observacion,Unds_PED,Peso_PED,Unds_PREP,Peso_PREP,Unds_REST,Peso_REST

            DataTable dtResult = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Size = 10,
                    Value = fechaDesde
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Size = 10,
                    Value = fechaHasta
                });

                dtResult = SelectStoreProcedure("sp_repTotalizadoProductosPedidoPendienteVentaPorPreparacion", "PRODUCTOSPEDIDOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtResult;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_EgresosSaldos
         *              Obtiene un DataSet con una lista de piezas egresadas agrupadas por pedido
         *              indicando el total de unidades y peso pedido,el total de unidades y peso egresado y
         *              el saldo de unidades y peso entre lo pedido y lo egresado.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_EgresosSaldos(DateTime fechaDesde, DateTime fechaHasta, string nombreCliente, string comprobantePedidoSAC, DateTime lote)
        {
            DataTable dtEgresos = null;

            //TIPO,COMPROBANTE,CLIENTE,COD_PRD,PRODUCTO,UNDS_PED,PESO_PED,UNDS_EGR,PESO_EGR,SALDO_UNDS,SALDO_PESO

            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@cliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 40,
                    Value = nombreCliente
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaHasta.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = comprobantePedidoSAC
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@lote",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = lote == DateTime.MinValue ? "" : lote.ToString("yyyy-MM-dd")
                });

                dtEgresos = SelectStoreProcedure("sp_getSaldosEgresosPorFechas", "EGRESOSSALDOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtEgresos;
        }
        
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_Devoluciones
         *              Obtiene un DataSet con todas las devoluciones coincidentes con los 
         *              filtros por fecha , cliente y comprobante
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Parametro    string (nombreCliente)
         * Parametro    string (comprobantePedidoSac)
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_Devoluciones(DateTime fechaDesde, DateTime fechaHasta, string nombreCliente, string comprobantePedidoSAC)
        {
            DataTable dtEgresos = null;
            try
            {
                /* TIPO,NRO,FECHA,PRODUCTO,PESO_NETO,COMPROBANTE,CLIENTE */
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@cliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 40,
                    Value = nombreCliente
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaHasta.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = comprobantePedidoSAC
                });
                dtEgresos = SelectStoreProcedure("sp_repDevoluciones", "DEVOLUCIONES", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtEgresos;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_EgresosTotalizadosFull
         *              Obtiene un DataSet con los totales de unidades y peso de cada producto egresado del
         *              tipo Pieza,caja o combo en un rango de fechas especificado.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_EgresosTotalizadosFull(DateTime fechaDesde, DateTime fechaHasta, string nombreCliente, string comprobantePedidoSAC, string tipoBulto,int idTipoProducto,int idProducto )
        {
            DataTable dtEgresos = null;
            try
            {
                /* PEDIDO,CLIENTE,TIPO,PRODUCTO,CODIGO,BULTOS,UNIDADES,PESO_NETO*/
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@cliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 40,
                    Value = nombreCliente
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaDesde.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.VarChar,
                    Size = 10,
                    Value = comprobantePedidoSAC != "" ? "" : fechaHasta.ToString("yyyy-MM-dd")
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@comprobantePedido",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = comprobantePedidoSAC
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@tipoBulto",
                    OleDbType = OleDbType.VarChar,
                    Size = 20,
                    Value = tipoBulto == "TODOS" ? "" : tipoBulto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idTipoProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idTipoProducto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idProducto
                });

                dtEgresos = SelectStoreProcedure("sp_repEgresosTotalizadosFullPorProductoPorFecha", "TOTAL_EGRESOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtEgresos;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_TrazabilidadPorLote
         *              Obtiene un DataSet con informacion de las ordenes de ingresos que se vincularon 
         *              en produccion con el lote indicado.
         * Parametro    DateTime Lote.
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporte_TrazabilidadPorLote(DateTime lote)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;
            int registrosObtenidos = 0;

            //OI,INGRESO,PROVEEDOR,SANITARIO,PRODUCTO,TROPA
            try
            {
                datSet = new DataSet();
                OleDbCommand command = new OleDbCommand();
                command.Connection = CDb.m_oleDbConnection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetOIsLote";
                command.Parameters.AddWithValue("@lote", SqlDbType.Date).Value=lote;
                oleDbDataAdapter = new OleDbDataAdapter(command);
                registrosObtenidos = oleDbDataAdapter.Fill(datSet, "TRAZABILIDAD_LOTE");
                if (registrosObtenidos == 0)
                    datSet = null;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datSet;
        }



        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_EgresosTrazabilidadPorPieza
         *              Obtiene un DataSet con informacion de las ordenes de ingresos que se vincularon 
         *              en produccion con la pieza indicada.
         * Parametro    DateTime Lote.
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporte_EgresosTrazabilidadPorPieza(int numPieza)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;
            int registrosObtenidos = 0;

            //OI,INGRESO,PROVEEDOR,SANITARIO,PRODUCTO,TROPA
            try
            {
                datSet = new DataSet();
                OleDbCommand command = new OleDbCommand();
                command.Connection = CDb.m_oleDbConnection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetOIsPieza";
                command.Parameters.AddWithValue("@numPieza", SqlDbType.Int).Value = numPieza;
                oleDbDataAdapter = new OleDbDataAdapter(command);
                registrosObtenidos = oleDbDataAdapter.Fill(datSet, "TRAZABILIDAD_LOTE");
                if (registrosObtenidos == 0)
                    datSet = null;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datSet;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_EgresosTotalizadoXDiaCliente
         *              Obtiene un DataSet con los totales acumulados de unidades , kg 
         *              para todos los articulos totalizado por Dia-Cliente 
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Parametro    int      idCliente.
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_EgresosTotalizadoXDiaCliente(DateTime fechaDesde, DateTime fechaHasta, string idCliente)
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
                    ParameterName = "@idCliente",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = idCliente
                });

                //IDOI,PROVEEDOR,SANITARIO,COD_PRD,PRODUCTO,UNDS,NETO,TARA,REMITIDO
                dt = SelectStoreProcedure("sp_repEgresosTotalizadoXDiaCliente", "MOV_ACUMULADOS", lparam);
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
