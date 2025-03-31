using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.ComponentModel;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Registracion de Inventario por Escanner
    /// </summary>
    public static partial class CDb
    {

        #region OPERACIONES DE REGISTRACION DE PIEZAS PARA INVENTARIO
        /**************************************************************************************************
         * Metodo:		RegisterReadPieceINV
         * Descripcion: Crea un registro de pieza que que colecta para inventario. Se registra en la tabla Inventario
         * Parametro:   (datetime) fechainventario: Es la fecha de inventario en donde se debe agrupar la pieza.
         * Parametro:	(CPIP) regDatPiece: Todos los datos necesarios de la pieza para la registracion.
         * Retorna:     Retorna tru si la registracion fue ok.
        ***************************************************************************************************/
        public static bool RegisterReadPieceINV(DateTime fechaInventario,CPIP regDatPiece)
        {
            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_insertCapturaInv", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = regDatPiece.Destino.Id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.String,
                        Size = 12,
                        Value = regDatPiece.Id.ToString()
                    });

                    dbCommand.ExecuteNonQuery();
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
         * Metodo:		RegisterContainerINV
         * Descripcion: Crea un registro de un contenedor colectado para inventario. Se registra en la tabla Inventario
         * Parametro:   (datetime) fechainventario: Es la fecha de inventario en donde se debe agrupar la pieza.
         * Parametro:	(Contenedor) regDatContainer: Todos los datos necesarios del contenedor para la registracion.
         * Retorna:     Retorna tru si la registracion fue ok.
        ***************************************************************************************************/
        public static bool RegisterContainerINV(DateTime fechaInventario, CContenedor regDatContainer)
        {
            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_insertCapturaInv", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idUbicacion",
                        DbType = DbType.Int32,
                        Value = regDatContainer.Destino.Id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.String,
                        Size = 12,
                        Value = 'A' + regDatContainer.Id.ToString() + 'A'
                    }); ;

                    dbCommand.ExecuteNonQuery();
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
         * Metodo:		RegisterResultInventario
         * Descripcion: Crea un registro con los resultados de un ajuste de inventario en la tabla
         *              resultInventario.
         * Retorna:     Retorna tru si la registracion fue ok.
        ***************************************************************************************************/
        public static bool RegisterResultInventario(DateTime fechaInventario,CResultInventario resultInventario)
        {
            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_insertResulInventario", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPedidoAjuste",
                        DbType = DbType.Int32,
                        Value = resultInventario.IdPedidoAjuste
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalPiezasVerificadasEnStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.StockVerificado.Piezas
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@pzas_sinSTLconSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinSTLconSTF.Piezas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@pzas_conSTLsinSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosConSTLsinSTF.Piezas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustPzas_sinSTLconSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinSTLconSTFAjustados.Piezas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustPzas_conSTLsinSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosConSTLsinSTFAjustados.Piezas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@pzas_fueraContenedor_SinStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.PiezasFueraContenedorSinStock
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@pzas_fueraContenedor_ConStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.PiezasFueraContenedorEnStock
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustPzas_fueraContenedor_SinStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.PiezasFueraContenedorSinStockAjustadas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustPzas_fueraContenedor_ConStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.PiezasFueraContenedorEnStockAjustadas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalCajasVerificadasEnStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.StockVerificado.Cajas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@cajas_sinSTLconSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinSTLconSTF.Cajas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@cajas_conSTLsinSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosConSTLsinSTF.Cajas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustCajas_sinSTLconSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinSTLconSTFAjustados.Cajas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustCajas_conSTLsinSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosConSTLsinSTFAjustados.Cajas
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalCombosVerificadosEnStock",
                        DbType = DbType.Int32,
                        Value = resultInventario.StockVerificado.Combos
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@combos_sinSTLconSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinSTLconSTF.Combos
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@combos_conSTLsinSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosConSTLsinSTF.Combos
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustCombos_sinSTLconSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinSTLconSTFAjustados.Combos
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@ajustCombos_conSTLsinSTF",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosConSTLsinSTFAjustados.Combos
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@cantBultosNoExisten",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosSinRegistracion
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@cantBultosEnPedidosAbiertos",
                        DbType = DbType.Int32,
                        Value = resultInventario.BultosEnPedidosAbiertosNoExisten
                    });
                    dbCommand.ExecuteNonQuery();
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
        * Metodo:		DeleteAllPiecesINV
        * Descripcion: Borra todas las capturas realizadas para un inventario dado
        * Parametro:   (datetime) fechainventario: Es la fecha de inventario en donde se debe agrupar la pieza.
        * Retorna:     Retorna tru si la accion fue satifactoria.
       ***************************************************************************************************/
        public static bool DeleteAllPiecesINV(DateTime fechaInventario)
        {
            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_deleteTodoCapturasInv", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.ExecuteNonQuery();
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
         * Metodo:		DeletePiezaINV
         * Descripcion: Borra una pieza colectada para un dado inventario
         * Parametro:   (datetime) fechainventario: Es la fecha de inventario en donde se debe agrupar la pieza.
         * Parametro:   (int) idpieza.
         * Retorna:     Retorna tru si la accion fue satifactoria.
        ***************************************************************************************************/
        public static bool DeletePiezaINV(DateTime fechaInventario,int idPieza)
        {
            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_deleteCapturaInv", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.String,
                        Size=12,
                        Value = idPieza.ToString()
                    }) ;

                    dbCommand.ExecuteNonQuery();
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
         * Metodo:		DeleteContenedorINV
         * Descripcion: Borra un contenedor colectado para un dado inventario
         * Parametro:   (datetime) fechainventario: Es la fecha de inventario en donde se debe agrupar la pieza.
         * Parametro:   (int) idcontenedor.
         * Retorna:     Retorna tru si la accion fue satifactoria.
        ***************************************************************************************************/
        public static bool DeleteContenedorINV(DateTime fechaInventario, int idcontenedor)
        {
            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_deleteCapturaInv", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idContenedor",
                        DbType = DbType.String,
                        Size = 12,
                        Value = 'A'+idcontenedor.ToString()+'A'
                    });

                    dbCommand.ExecuteNonQuery();
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
         * Metodo:		EsBultoColectadoEnInventario
         * Descripcion: verifica si un bulto (pieza o contenedor) ya posee registro en las colecciones 
         *              de un inventario dado.
         * Retorna:     Retorna tru si la accion fue satifactoria.
        ***************************************************************************************************/
        public static bool EsBultoColectadoEnInventario(DateTime fechaInventario, string idBulto)
        {
            bool esColectado = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esBultoColectadoEnInventario", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        DbType = DbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.String,
                        Size = 12,
                        Value = idBulto
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@esColectada",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    esColectado = Convert.ToBoolean(dbCommand.Parameters["@esColectada"].Value == DBNull.Value ? 0: dbCommand.Parameters["@esColectada"].Value);
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return esColectado;
        }
        /***************************************************************************************
         * Metodo:	    ExisteInventario
         *              Indica si existen registros de inventario para una dada fecha de Invetario
         * Parametro:   (datetime) fechaInventario
         * Retorna:     (bool) true si existe
        *****************************************************************************************/
        public static bool ExisteInventario(DateTime fechaInventario)
        {
            OleDbDataReader recordSet;
            bool existe = false;
            string strCmd = String.Format(" SELECT * FROM INVENTARIO WHERE fechainicioinventario = {{d '{0}'}} ", fechaInventario.ToString("yyyy-MM-dd"));
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    existe = true;
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return existe;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES INVENTARIO

        /***************************************************************************************
        Metodo:		GetDatSet_DetalleColeccionInventario
                    Crea un dataset con las piezas colectadas en un inventario.
        Parametros:	out DataTable 
        Retorna:    != null ok. 
        *****************************************************************************************/
        public static DataTable GetDatSet_DetalleColeccionInventario(DateTime fechaInventario)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@fechaInventario",
                    OleDbType = OleDbType.Date,
                    Value = fechaInventario
                });
                //TIPO,NRO,FECHA,PRODUCTO,UBICACION
                dt = SelectStoreProcedure("sp_getDetalleColeccionINV", "MOV_INVENTARIO", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
        Metodo:		GetDatSet_TotalesPiezasIngresadasLote
                    Crea un dataset con los totales por producto de piezas que ingresaron
                    a produccion para un dado lote o fecha del dia. 
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_TotalesColeccionInventario(DateTime fechaInventario,out DataTable dt)
        {
            bool resOk = false;

            dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@fechaInventario",
                    OleDbType = OleDbType.Date,
                    Value = fechaInventario
                });

                //TIPO',PRODUCTO,UBICACION,BULTOS,PESO

                dt = SelectStoreProcedure("sp_getTotalesColeccionINV", "MOV_INVENTARIO", lparam);
                resOk = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return resOk;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ResultInventario
         *              Obtiene un DataSet con los resultados de ajustes de inventario segun
         *              rango de fecha de inventario.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ResultInventario(DateTime fechaDesde, DateTime fechaHasta)
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

                //FECHA,INVENTARIO,PIEZAS_VERIF_EN_STOCK,PIEZAS_CON_STOCK_NO_EXISTEN,PIEZAS_SIN_STOCK_EXISTEN,PIEZAS_FUERA_CONTENEDOR_ENSTOCK,
                //PIEZAS_FUERA_CONTENEDOR_SINSTOCK,SIN_REGISTRAR,CAJAS_VERIF_EN_STOCK,CAJAS_CON_STOCK_NO_EXISTEN,CAJAS_SIN_STOCK_EXISTEN,
                //COMBOS_VERIF_EN_STOCK,COMBOS_CON_STOCK_NO_EXISTEN,COMBOS_SIN_STOCK_EXISTEN,BULTOS_EN_PEDIDOS_ABIERTOS

                dt = SelectStoreProcedure("sp_repResultInventario", "MOV_INVENTARIO", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporteInventario_PiezasSinRegistro
         *              Obtiene un DataSet con las piezas colectadas en el inventario que no tienen
         *              registros en el sistema 
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporteInventario_BultosSinRegistro(DateTime fechaInventario)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_getInv_sinREG", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;


                    OleDbParameter paramProceso = new OleDbParameter();
                    paramProceso = dbCommand.Parameters.Add("@fechaInventario", OleDbType.Date);
                    paramProceso.Direction = ParameterDirection.Input;
                    paramProceso.Value = fechaInventario;

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    int registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_INVENTARIO");
                    if (registrosObtenidos == 0)
                        datSet = null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return datSet;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporteInventario_PiezasFueraDeContenedorEnStock
         *              Obtiene un DataSet con las piezas que estan fisicamente en el inventario y pertenecen a
         *              un contenedor que esta en stock.
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporteInventario_PiezasFueraDeContenedorEnStock(DateTime fechaInventario)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;
            //PIEZA_NRO,PIEZA_LOTE,PIEZA_PESO,PIEZA_PRODUCTO,CONTENEDOR_TIPO,CONTENEDOR_NRO,CONTENEDOR_LOTE,CONTENEDOR_PRODUCTO
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_getInv_piezasFueraCont_ContEnStock", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramProceso = new OleDbParameter();
                    paramProceso = dbCommand.Parameters.Add("@fechaInventario", OleDbType.Date);
                    paramProceso.Direction = ParameterDirection.Input;
                    paramProceso.Value = fechaInventario;

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    int registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_INVENTARIO");
                    if (registrosObtenidos == 0)
                        datSet = null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return datSet;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporteInventario_PiezasFueraDeContenedorSinStock
         *              Obtiene un DataSet con las piezas existentes en el inventario que pertenecen 
         *              a un contenedor sin stock.
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporteInventario_PiezasFueraDeContenedorSinStock(DateTime fechaInventario)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;
            //PIEZA_NRO,PIEZA_LOTE,PIEZA_PESO,PIEZA_PRODUCTO,CONTENEDOR_TIPO,CONTENEDOR_NRO,CONTENEDOR_LOTE,CONTENEDOR_PRODUCTO
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_getInv_piezasFueraCont_ContSinStock", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramProceso = new OleDbParameter();
                    paramProceso = dbCommand.Parameters.Add("@fechaInventario", OleDbType.Date);
                    paramProceso.Direction = ParameterDirection.Input;
                    paramProceso.Value = fechaInventario;

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    int registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_INVENTARIO");
                    if (registrosObtenidos == 0)
                        datSet = null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return datSet;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporteInventario_PiezasSinStockConExistenciaFisica
         *              Obtiene un DataSet con las piezas que no posee stock por estar egresadas
         *              o ingresadas a producion y existen fisicamente en stock
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporteInventario_BultosSinStockConExistenciaFisica(DateTime fechaInventario)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.SP_GETINV_SINSTL_CONSTF", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramProceso = new OleDbParameter();
                    paramProceso = dbCommand.Parameters.Add("@fechaInventario", OleDbType.Date);
                    paramProceso.Direction = ParameterDirection.Input;
                    paramProceso.Value = fechaInventario;

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    int registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_INVENTARIO");
                    if (registrosObtenidos == 0)
                        datSet = null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return datSet;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporteInventario_PiezasConStockSinExistenciaFisica
         *              Obtiene un DataSet con las piezas que tienen stock pero no se encuentran
         *              fisicamente en stock
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporteInventario_BultosConStockSinExistenciaFisica(DateTime fechaInventario)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.SP_GETINV_CONSTL_SINSTF", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramProceso = new OleDbParameter();
                    paramProceso = dbCommand.Parameters.Add("@fechaInventario", OleDbType.Date);
                    paramProceso.Direction = ParameterDirection.Input;
                    paramProceso.Value = fechaInventario;

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    int registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_INVENTARIO");
                    if (registrosObtenidos == 0)
                        datSet = null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return datSet;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporteInventario_BultosEnPedidosAbiertosSinExistenciaFisica
         *              Obtiene un DataSet con los bultos que se encuentran en pedidos abiertos y que
         *              no tienen existencia fisicamente
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporteInventario_BultosEnPedidosAbiertosSinExistenciaFisica(DateTime fechaInventario)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;
            //TIPO,NRO,PESO_NETO,LOTE,PRODUCTO,PEDIDO,COMPROBANTE

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_getInv_BultosEnPedAbiertos_sinEXT", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramProceso = new OleDbParameter();
                    paramProceso = dbCommand.Parameters.Add("@fechaInventario", OleDbType.Date);
                    paramProceso.Direction = ParameterDirection.Input;
                    paramProceso.Value = fechaInventario;

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    int registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_INVENTARIO");
                    if (registrosObtenidos == 0)
                        datSet = null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return datSet;
        }
        /***************************************************************************************
         * Metodo:	    GetInfoInventario
         * Parametro    DateTime (fecha de inventario).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static bool GetInfoInventario(DateTime fInventario, ref CResultInventario info)
        {
            DataSet ds = null;
            bool getOk = false;
            try
            {
                CDb.GetTotalPiezasEnStockVerificadas(fInventario, ref info);

                ds = CDb.GetConsultaReporteInventario_BultosConStockSinExistenciaFisica(fInventario);
                if(ds != null)
                {
                    info.BultosConSTLsinSTF.Piezas = (from row in ds.Tables[0].AsEnumerable()
                                                      where row.Field<string>("TIPO") == "PIEZA"
                                                      select row).Count();
                    info.BultosConSTLsinSTF.Cajas = (from row in ds.Tables[0].AsEnumerable()
                                                      where row.Field<string>("TIPO") == "CAJA"
                                                      select row).Count();
                    info.BultosConSTLsinSTF.Combos = (from row in ds.Tables[0].AsEnumerable()
                                                      where row.Field<string>("TIPO") == "COMBO"
                                                      select row).Count();
                }


                ds = CDb.GetConsultaReporteInventario_BultosSinStockConExistenciaFisica(fInventario);
                if (ds != null)
                {
                    info.BultosSinSTLconSTF.Piezas = (from row in ds.Tables[0].AsEnumerable()
                                                      where row.Field<string>("TIPO") == "PIEZA"
                                                      select row).Count();
                    info.BultosSinSTLconSTF.Cajas = (from row in ds.Tables[0].AsEnumerable()
                                                     where row.Field<string>("TIPO") == "CAJA"
                                                     select row).Count();
                    info.BultosSinSTLconSTF.Combos = (from row in ds.Tables[0].AsEnumerable()
                                                      where row.Field<string>("TIPO") == "COMBO"
                                                      select row).Count();
                }


                ds = CDb.GetConsultaReporteInventario_BultosSinRegistro(fInventario);
                info.BultosSinRegistracion = ds != null ? ds.Tables[0].Rows.Count : 0;

                ds = CDb.GetConsultaReporteInventario_PiezasFueraDeContenedorEnStock(fInventario);
                info.PiezasFueraContenedorEnStock = ds != null ? ds.Tables[0].Rows.Count : 0;

                ds = CDb.GetConsultaReporteInventario_PiezasFueraDeContenedorSinStock(fInventario);
                info.PiezasFueraContenedorSinStock = ds != null ? ds.Tables[0].Rows.Count : 0;

                ds = CDb.GetConsultaReporteInventario_BultosEnPedidosAbiertosSinExistenciaFisica(fInventario);
                info.BultosEnPedidosAbiertosNoExisten = ds != null ? ds.Tables[0].Rows.Count : 0;

                getOk = true;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return getOk;
        }


        /***************************************************************************************
         * Metodo:	    GetTotalPiezasEnStock
         *              Obtiene el total de bultos que se encuentran en stock incluyendo contenedores.
         * Retorna:     (int) total.
        *****************************************************************************************/
        public static void GetTotalBultosEnStock(ref CResultInventario resultTotal)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;

            OleDbParameter paramTotalPiezas, paramTotalCajas, paramTotalCombos;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_getTotalBultosEnStock", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    paramTotalPiezas=new OleDbParameter()
                    {
                        ParameterName = "@totalPiezas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    };
                    dbCommand.Parameters.Add(paramTotalPiezas);

                    paramTotalCajas = new OleDbParameter()
                    {
                        ParameterName = "@totalCajas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    };
                    dbCommand.Parameters.Add(paramTotalCajas);

                    paramTotalCombos = new OleDbParameter()
                    {
                        ParameterName = "@totalCombos",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    };
                    dbCommand.Parameters.Add(paramTotalCombos);

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    dbCommand.ExecuteNonQuery();
                    resultTotal.StockDespuesAjuste.Piezas = Convert.ToInt32(paramTotalPiezas.Value ?? 0);
                    resultTotal.StockDespuesAjuste.Cajas = Convert.ToInt32(paramTotalCajas.Value ?? 0);
                    resultTotal.StockDespuesAjuste.Combos = Convert.ToInt32(paramTotalCombos.Value ?? 0);
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
        }
        /***************************************************************************************
         * Metodo:	    GetTotalBultosEnStockVerificados
         *              Obtiene los totales de piezas,cajas y combos en stock y los totales
         *              de los mismos pero verificados con el inventario.
         * Parametro:   fecha de inventario.
         * Retorna:     (int) total.
        *****************************************************************************************/
        public static bool GetTotalPiezasEnStockVerificadas(DateTime fechaInventario,ref CResultInventario results)
        {
            DataSet datSet = null;
            OleDbDataAdapter oleDbDataAdapter;
            bool getOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    datSet = new DataSet();

                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_getTotalBultosEnStockVerificados", m_oleDbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        OleDbType = OleDbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalPiezas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalCajas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalCombos",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalPiezasVerificadas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalCajasVerificadas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@totalCombosVerificados",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    oleDbDataAdapter = new OleDbDataAdapter(dbCommand);

                    dbCommand.ExecuteNonQuery();

                    results.Stock.Piezas = Convert.ToInt32(dbCommand.Parameters["@totalPiezas"].Value == DBNull.Value?0: dbCommand.Parameters["@totalPiezas"].Value);
                    results.Stock.Cajas = Convert.ToInt32(dbCommand.Parameters["@totalCajas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@totalCajas"].Value);
                    results.Stock.Combos = Convert.ToInt32(dbCommand.Parameters["@totalCombos"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@totalCombos"].Value);
                    results.StockVerificado.Piezas = Convert.ToInt32(dbCommand.Parameters["@totalPiezasVerificadas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@totalPiezasVerificadas"].Value);
                    results.StockVerificado.Cajas = Convert.ToInt32(dbCommand.Parameters["@totalCajasVerificadas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@totalCajasVerificadas"].Value);
                    results.StockVerificado.Combos = Convert.ToInt32(dbCommand.Parameters["@totalCombosVerificados"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@totalCombosVerificados"].Value);

                    getOk = true;

                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return getOk;
        }

        /// <summary>
        /// Ajuste de stock que detecta piezas que se encuentran en stock pero que no estan fisicamente.
        /// Estas piezas son egresadas a travez de la creacion automatica de un pedido. Este pedido tendra
        /// un numero de comprobante = '000000000000', codigo de cliente SAC='0000',codigo de pedido SAC=0,
        /// idEstacion= 9999.
        /// </summary>
        /// <param name="fechaInventario">fecha del iventario en que se basara el ajuste de stock</param>
        /// <param name="registrosAfectados">cantidad de piezas que se ajustaron</param>
        /// <returns></returns>
        public static bool EjecutarAjusteInventario_BultosConStockSinExistenciaFisica(DateTime fechaInventario, ref CResultInventario infoResults)
        {
            bool callSPok = false;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_ajustInv_conSTL_sinSTF", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        OleDbType = OleDbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Input,
                        Value= CDb.m_OperadorActivo.m_id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Input,
                        Value=9999
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@piezasAjustadas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@cajasAjustadas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@combosAjustados",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@newIdPedido",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });

                    dbCommand.ExecuteNonQuery();

                    infoResults.BultosConSTLsinSTFAjustados.Piezas = Convert.ToInt32(dbCommand.Parameters["@piezasAjustadas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@piezasAjustadas"].Value);
                    infoResults.BultosConSTLsinSTFAjustados.Cajas = Convert.ToInt32(dbCommand.Parameters["@cajasAjustadas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@cajasAjustadas"].Value);
                    infoResults.BultosConSTLsinSTFAjustados.Combos = Convert.ToInt32(dbCommand.Parameters["@combosAjustados"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@combosAjustados"].Value);
                    infoResults.IdPedidoAjuste = Convert.ToInt32(dbCommand.Parameters["@newIdPedido"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@newIdPedido"].Value);
                    callSPok = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }
        /// <summary>
        /// Ajuste de stock que detecta piezas que estan en stock ,existen fisicamente pero se encuentran
        /// fuera de su contenedor.
        /// Estas piezas son eliminadas desde el contenedor que las agrupo y en las propiedades del 
        /// contenedor se ajustan las unidades y peso del contenedor
        /// </summary>
        /// <param name="fechaInventario">fecha del iventario en que se basara el ajuste de stock</param>
        /// <param name="registrosAfectados">cantidad de piezas que se ajustaron</param>
        /// <returns></returns>
        public static bool EjecutarAjusteInventario_PiezasFueraDeContenedorEnStock(DateTime fechaInventario, ref CResultInventario infoResults)
        {
            bool callSPok = false;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_ajustInv_conSTL_conSTF_fueraContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramFechaInventario = new OleDbParameter();
                    paramFechaInventario = dbCommand.Parameters.Add("@fechainventario", OleDbType.Date);
                    paramFechaInventario.Direction = ParameterDirection.Input;
                    paramFechaInventario.Value = fechaInventario;

                    OleDbParameter paramPiezasAjustadas = new OleDbParameter();
                    paramPiezasAjustadas = dbCommand.Parameters.Add("@piezasAjustadas", OleDbType.Integer);
                    paramPiezasAjustadas.Direction = ParameterDirection.Output;

                    dbCommand.ExecuteNonQuery();

                    infoResults.PiezasFueraContenedorEnStockAjustadas = Convert.ToInt32(dbCommand.Parameters["@piezasAjustadas"].Value);
                    callSPok = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }
        /// <summary>
        /// Ajuste de stock que detecta piezas que no estan en stock ,existen fisicamente pero se encuentran
        /// fuera de su contenedor.
        /// Estas piezas son eliminadas desde el contenedor que las agrupo y en las propiedades del 
        /// contenedor se ajustan las unidades y peso del contenedor.
        /// Tambien se eliminan del egreso y del ingreso a produccion para que esten en stock
        /// nuevamente.
        /// </summary>
        /// <param name="fechaInventario">fecha del iventario en que se basara el ajuste de stock</param>
        /// <param name="registrosAfectados">cantidad de piezas que se ajustaron</param>
        /// <returns></returns>
        public static bool EjecutarAjusteInventario_PiezasFueraDeContenedorSinStock(DateTime fechaInventario, ref CResultInventario infoResults)
        {
            bool callSPok = false;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_ajustInv_piezasFueraCont_ContSinStock", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramFechaInventario = new OleDbParameter();
                    paramFechaInventario = dbCommand.Parameters.Add("@fechainventario", OleDbType.Date);
                    paramFechaInventario.Direction = ParameterDirection.Input;
                    paramFechaInventario.Value = fechaInventario;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Input,
                        Value = CDb.m_OperadorActivo.m_id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Input,
                        Value = CDb.m_OperadorActivo.m_idEstacion
                    });


                    OleDbParameter paramPiezasAjustadas = new OleDbParameter();
                    paramPiezasAjustadas = dbCommand.Parameters.Add("@piezasAjustadas", OleDbType.Integer);
                    paramPiezasAjustadas.Direction = ParameterDirection.Output;

                    dbCommand.ExecuteNonQuery();

                    infoResults.PiezasFueraContenedorSinStockAjustadas = Convert.ToInt32(dbCommand.Parameters["@piezasAjustadas"].Value);
                    callSPok = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }
        /// <summary>
        /// Ajuste de stock que detecta piezas que no estan en stock por estar egresadas o ingresadas a produccion
        /// y existen fisicamente.
        /// Estas piezas son eliminadas de los egresos y de los ingresos a produccion para que vueltan
        /// a estar en estock tanto logicamente como fisicamente.
        /// </summary>
        /// <param name="fechaInventario">fecha del iventario en que se basara el ajuste de stock</param>
        /// <param name="registrosAfectados">cantidad de piezas que se ajustaron</param>
        /// <returns></returns>
        public static bool EjecutarAjusteInventario_BultosSinStockConExistenciaFisica(DateTime fechaInventario,ref CResultInventario infoResults)
        {
            bool callSPok = false;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_ajustInv_sinSTL_conSTF", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fechaInventario",
                        OleDbType = OleDbType.Date,
                        Value = fechaInventario
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Input,
                        Value = CDb.m_OperadorActivo.m_id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Input,
                        Value = 9999
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@piezasAjustadas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@cajasAjustadas",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@combosAjustados",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@piezasAjustadasPorIngProduccion",
                        OleDbType = OleDbType.Integer,
                        Direction = ParameterDirection.Output
                    });


                    dbCommand.ExecuteNonQuery();

                    infoResults.BultosSinSTLconSTFAjustados.Piezas = Convert.ToInt32(dbCommand.Parameters["@piezasAjustadas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@piezasAjustadas"].Value);
                    infoResults.BultosSinSTLconSTFAjustados.Cajas = Convert.ToInt32(dbCommand.Parameters["@cajasAjustadas"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@cajasAjustadas"].Value);
                    infoResults.BultosSinSTLconSTFAjustados.Combos = Convert.ToInt32(dbCommand.Parameters["@combosAjustados"].Value == DBNull.Value ? 0 : dbCommand.Parameters["@combosAjustados"].Value);
                    callSPok = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }
        /// <summary>
        /// Ajuste de ubicaciones de piezas que se basa en las piezas colectadas en el inventario.
        /// Asigna la ubicacion real de la pieza .
        /// </summary>
        /// <param name="fechaInventario">fecha del iventario en que se basara el ajuste de stock</param>
        /// <param name="registrosAfectados">cantidad de piezas que se ajustaron</param>
        /// <returns></returns>
        public static bool EjecutarAjusteInventario_ActualizarUbicaciones(DateTime fechaInventario, ref CResultInventario infoResults)
        {
            bool callSPok = false;

            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("dbo.sp_updateDestInv", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 120;

                    OleDbParameter paramFechaInventario = new OleDbParameter();
                    paramFechaInventario = dbCommand.Parameters.Add("@fechainventario", OleDbType.Date);
                    paramFechaInventario.Direction = ParameterDirection.Input;
                    paramFechaInventario.Value = fechaInventario;

                    OleDbParameter paramPiezasAjustadas = new OleDbParameter();
                    paramPiezasAjustadas = dbCommand.Parameters.Add("@bultosAjustados", OleDbType.Integer);
                    paramPiezasAjustadas.Direction = ParameterDirection.Output;

                    dbCommand.ExecuteNonQuery();

                    infoResults.BultosActualizadosEnHubicacion = Convert.ToInt32(dbCommand.Parameters["@bultosAjustados"].Value);
                    callSPok = true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }

        #endregion

    }
}
