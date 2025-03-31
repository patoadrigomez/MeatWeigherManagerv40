using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Ingresos a Produccion
    /// </summary>
    public static partial class CDb
    {

        #region OPERACIONES DE INGRESO A PRODUCCION
        /**************************************************************************************************
         * Metodo:		RegisterReadPieceIP
         * Descripcion: Crea un registro de pieza que ingresa a produccion en la tabla DLP
         * Parametro:	(CPIP) regDatPiece: Todos los datos necesarios de la pieza para la registracion.
         * Parametro:   (int)  idSector: Sector de produccion destino de la pieza.
         * Retorna:     Retorna tru si la registracion fue ok.
        ***************************************************************************************************/
        public static bool RegisterReadPartIP(CPIP regDatPiece,int idSector)
        {
            bool registracionOk = false;

            int regAfectados;

            string strCmd = String.Format(" INSERT INTO DLP(IDOPERADOR,IDESTACION,IDPESAJE,FECHA_HORA,IDOI,LOTEPADRE,IDSECTOR)" +
                                          " VALUES({0},{1},{2},{{ts '{3}'}},{4},{{ts '{5}'}},{6})",
                                          regDatPiece.IdOperadorRegistration,
                                          regDatPiece.IdEstacionRegistration,
                                          regDatPiece.Id,
                                          regDatPiece.FechaIngreso.ToString("yyyy-MM-dd HH:mm:ss"),
                                          regDatPiece.IdOI?.ToString() ??  "null",
                                          regDatPiece.LotePadre.ToString("yyyy-MM-dd HH:mm:ss"),
                                          idSector);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                registracionOk = (regAfectados == 1);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registracionOk;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE INGRESOS A PRODUCCION

        /***************************************************************************************
         * Metodo:	    IsValidPartForProductionInput
         *              Verifica si una pieza es valida para tener un ingreso a produccion
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidPartForProductionInput(int idPieza, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidaPiezaParaIngresoAProduccion", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
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
        Metodo:		GetDatSet_PiezasIngresadasLote
                    Crea un dataset con las piezas ingresadas a produccion para un dado lote o 
                    fecha del dia.
                    Las columnas son de minima informacion dado que es solo para visualizar en una grilla.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasIngresadasLote(DateTime dateLote, out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = String.Format(
                    " SELECT dp.idpesaje as IDPIEZA,dp.fecha_hora as FECHA_INGRESO,ope.nombre as OPERADOR,dp.idoi as OI,"+
                    "(REPLACE(STR(DAY(dp.lotepadre),2,0),' ','0') + REPLACE(STR(MONTH(dp.lotepadre),2,0),' ','0') +REPLACE(STR(YEAR(dp.lotepadre),4,0),' ','0'))as LOTE_PIEZA,"+
                    " s.nombre as SECTOR,('('+ prd.codigoProductoSAC +') ' + prd.nombre) as PRODUCTO,pe.numtropa as TROPA,tip.nombre as TIPIF," +
                    " pe.pesoNeto as NETO " +
                    " FROM DLP as dp " +
                    " LEFT OUTER JOIN operadores ope ON dp.idOperador = ope.id " +
                    " LEFT OUTER JOIN Pesadas as pe ON dp.idpesaje = pe.id " +
                    " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id " +
                    " LEFT OUTER JOIN Sectores as s ON dp.idsector = s.id " +
                    " LEFT OUTER JOIN Tipificaciones as tip ON pe.idtipificacion = tip.id " +
                    " WHERE CAST(dp.fecha_hora as DATE) = {{d '{0}'}} order by dp.fecha_hora desc ", dateLote.ToString("yyyy-MM-dd"));

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsPDESFills = oleDbDataAdapter.Fill(dsPiezas, "PIEZAS");
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
        Metodo:		GetDatSet_TotalesPiezasIngresadasLote
                    Crea un dataset con los totales por producto de piezas que ingresaron
                    a produccion para un dado lote o fecha del dia. 
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_TotalesPiezasCapturadas(DateTime dateLote, out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = String.Format(
                    " SELECT ('('+ prd.codigoProductoSAC +') ' + prd.nombre) as PRODUCTO,s.nombre as SECTOR,COUNT(*) as CANTIDAD, SUM(pe.unidades) as UNIDADES, SUM(pe.pesoNeto) as NETO " +
                    " FROM DLP as dp "+
                    " LEFT OUTER JOIN Pesadas as pe ON dp.idpesaje = pe.id "+
                    " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id "+
                    " LEFT OUTER JOIN Sectores as s ON dp.idsector = s.id " +
                    " WHERE CAST(dp.fecha_hora as DATE) = {{d '{0}'}} group by prd.codigoProductoSAC,prd.nombre,s.nombre ", dateLote.ToString("yyyy-MM-dd"));

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsPDESFills = oleDbDataAdapter.Fill(dsPiezas, "TOTAL_PIEZAS");
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
        Metodo:		GetConsultaCompletaIngresosProduccionLotes
                    Crea un dataset con toda la informacion de las piezas ingresadas a produccion 
                    por cada lote.
        Parametros:	out DataSet (infoLote)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaCompletaIngresosProduccionLotes(out DataSet dsInfoLote)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoLote = new DataSet();
            string sqlQuery;

            DataColumn[] columnsLoteKeys = null;
            DataColumn[] columnsPiezasKeys = null;
            DataRelation datRelacionLote_P;

            try
            {
                int rowsLoteFills = 0;
                int rowsPesadasFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = " SELECT DISTINCT CONVERT(varchar(10), CAST(p.fecha_hora as DATE), 103) AS LOTE, cast(p.fecha_hora as date) as LOTE_PRODUCCION FROM DLP p " +
                               " order by LOTE_PRODUCCION desc ";

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsLoteFills = oleDbDataAdapter.Fill(dsInfoLote, "LOTES");

                    if (rowsLoteFills != 0)
                    {
                        sqlQuery = String.Format(
                        " SELECT dp.idpesaje as IDPIEZA,CAST(dp.fecha_hora AS DATE) AS LOTE_PRODUCCION,ope.nombre as OPERADOR,dp.idoi as OI," +
                        "(REPLACE(STR(DAY(dp.lotepadre),2,0),' ','0') + REPLACE(STR(MONTH(dp.lotepadre),2,0),' ','0') +REPLACE(STR(YEAR(dp.lotepadre),4,0),' ','0'))as LOTE_PIEZA," +
                        " s.nombre as SECTOR , "+
                        " ('('+ prd.codigoProductoSAC +') ' + prd.nombre) as PRODUCTO," +
                        " pe.numTropa as TROPA,"+
                        " tip.nombre as TIPIF,"+
                        " pe.pesoNeto as NETO " +
                        " FROM DLP as dp " +
                        " LEFT OUTER JOIN operadores ope ON dp.idOperador = ope.id " +
                        " LEFT OUTER JOIN Pesadas as pe ON dp.idpesaje = pe.id " +
                        " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id "+
                        " LEFT OUTER JOIN Tipificaciones as tip ON pe.idtipificacion = tip.id " +
                        " LEFT OUTER JOIN Sectores as s ON dp.idsector = s.id ");

                        //ejecuto el dataadapter con el nuevo query
                        oleDbDataAdapter.SelectCommand.CommandText = sqlQuery;
                        //cargo el resultado del query en el DataSet como tabla "PESADAS"
                        rowsPesadasFills = oleDbDataAdapter.Fill(dsInfoLote, "PIEZAS");

                        columnsLoteKeys = new DataColumn[] { dsInfoLote.Tables["LOTES"].Columns["LOTE_PRODUCCION"] };

                        columnsPiezasKeys = new DataColumn[] { dsInfoLote.Tables["PIEZAS"].Columns["LOTE_PRODUCCION"] };
                        //creo el objeto relacion 
                        datRelacionLote_P = new DataRelation("LOTES_PIEZAS", columnsLoteKeys, columnsPiezasKeys, false);
                        //sumo el objeto relacion al dataset    
                        dsInfoLote.Relations.Add(datRelacionLote_P);

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
        /**************************************************************************************************
         * Metodo:		EliminarPiezaIngresadaProduccion
         * Descripcion: Borra el registro de una pieza ingresada a produccion
         * Parametro:	int idPieza.
         * Retorna:     Retorna tru si ok.
        ***************************************************************************************************/
        public static bool EliminarPiezaIngresadaProduccion(int idPieza)
        {
            bool borradoOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE DLP WHERE idpesaje = {0} ", idPieza);
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
         * Metodo:	    GetConsultaReporte_IngresoAProduccionDetalle
         *              Obtiene un DataSet con una lista de piezas ingresadas en producccion en funcion
         *              a un rango de fechas indicado.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_IngresoAProduccionDetalle(DateTime fechaDesde, DateTime fechaHasta, int idSector,int idTipoProducto,int idProducto,int numTropa)
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
                    ParameterName = "@idSector",
                    OleDbType = OleDbType.Integer,
                    Value = idSector
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
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@numTropa",
                    OleDbType = OleDbType.Integer,
                    Value = numTropa
                });
                //IDPIEZA,IDOI,COD_PRD,PRODUCTO,TIPO_PRD,TROPA,TIPIF,INGRESO,SECTOR,UNDS,NETO,TARA,PUESTO,OPERADOR
                dt = SelectStoreProcedure("sp_repIngProduccionDetallePorSector", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_IngresosAProduccionTotalizado
         *              Obtiene un DataSet con los totales de unidades y peso de las piezas que 
         *              ingresaron a produccion en el rango de fechas indicado.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_IngresoAProduccionTotalizado(DateTime fechaDesde, DateTime fechaHasta, int idSector,int idTipoProducto,int idProducto)
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
                    ParameterName = "@idSector",
                    OleDbType = OleDbType.Integer,
                    Value = idSector
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
                //LOTE,COD_PRD,PRODUCTO,TROPA,TIPIF,UNDS,NETO
                dt = SelectStoreProcedure("sp_repIngProduccionTotalizadoPorSector", "MOV_ACUMULADOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_TotalesProductosIP
         *              Obtiene un DataSet con una consulta de Totales de Pesadas,unidades y peso
         *              basado en un numero de OI.
         * Parametro    string lote.
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataSet GetConsultaReporte_TotalesProductosIP(int idOI)
        {
            DataSet datSet = null;

            OleDbDataAdapter oleDbDataAdapter;
            int registrosObtenidos = 0;

            string strCmd = String.Format(
                    " SELECT OI.id as IDOI,prove.Nombre as PROVEEDOR,OI.idCertSanitario as SANITARIO,prd.codigoProductoSAC as COD_PRD,prd.nombre as PRODUCTO,SUM(pe.unidades) as UNDS, SUM(pe.pesoNeto) as NETO " +
                    " FROM pesadas as pe " +
                    " LEFT OUTER JOIN operadores ope ON pe.idOperador=ope.id " +
                    " LEFT OUTER JOIN OI ON pe.idOi=OI.id " +
                    " LEFT OUTER JOIN Productos as prd ON pe.idproducto=prd.id " +
                    " LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo=tp.id " +
                    " LEFT OUTER JOIN proveedores prove ON OI.idProveedor=prove.id " +
                    " WHERE pe.idoi is not null AND OI.id = {0}" +
                    " GROUP BY OI.id,prove.Nombre,OI.idCertSanitario,prd.codigoProductoSAC,prd.nombre ORDER BY OI.ID   ", idOI);
            try
            {
                oleDbDataAdapter = new OleDbDataAdapter(strCmd, m_oleDbConnection);
                datSet = new DataSet();
                registrosObtenidos = oleDbDataAdapter.Fill(datSet, "MOV_ACUMULADOS");
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
         * Metodo:	    GetConsultaReporte_RendimientoPorTipoDeProducto
         *              Obtiene un DataSet con un detalle por producto del rendimiento porcentual 
         *              resultante de una produccion acotada por rango de fechas y tipo de 
         *              articulo. Si el tipo de articulo es 0 se detallan todos los tipos.
         * Parametro    DateTime fechaDesde, DateTime fechaHasta,int idTipoProducto
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_RendimientoPorTipoDeProducto(DateTime fechaDesde, DateTime fechaHasta,int idTipoProducto)
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
                    ParameterName = "@idTipoProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idTipoProducto
                });
                //TIPO,PRODUCTO,TOTAL_TIPO,TOTAL_PRODUCIDO,UNDS_PRODUCIDAS,PESO_UNITARIO,RENDIMIENTO ,STD
                dt = SelectStoreProcedure("sp_repRendimientoPorTipoDeProducto", "MOV_RENDIMIENTO", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_RendimientoPorSector
         *              Obtiene un DataSet con dos tablas:
         *              Una de ellas con el resultado de rendimiento
         *              entre la mercaderia ingresada para producir y la mercaderia producida basado
         *              en rango de fechas y el sector de produccion. 
         *              La otra tabla posee por cada producto producido la incidencia porcentual con 
         *              respecto al total ingresado a produccion para ese mismo sector y fecha.
         * Parametro    DateTime fechaDesde, DateTime fechaHasta,int idSector
         * Retorna:     DataSet con dos tablas
        *****************************************************************************************/
        public static DataSet GetConsultaReporte_RendimientoPorSector(DateTime fechaDesde, DateTime fechaHasta, int idSector)
        {
            DataSet ds = null;
            DataTable dtTotales;
            DataTable dtDetalles;
            try
            {
                List<OleDbParameter> lparamTotales = new List<OleDbParameter>();
                List<OleDbParameter> lparamDetalles = new List<OleDbParameter>();
                lparamTotales.Add(new OleDbParameter()
                {
                    ParameterName = "@desde",
                    OleDbType = OleDbType.Date,
                    Value = fechaDesde
                });
                lparamTotales.Add(new OleDbParameter()
                {
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparamTotales.Add(new OleDbParameter()
                {
                    ParameterName = "@idSector",
                    OleDbType = OleDbType.Integer,
                    Value = idSector
                });

                dtTotales = SelectStoreProcedure("sp_repRendimientoTotalesPorSector", "REND_PRODUCCION_A_PRODUCIR", lparamTotales);
                if(dtTotales != null && dtTotales.Rows.Count > 0)
                {
                    lparamDetalles.Add(new OleDbParameter()
                    {
                        ParameterName = "@desde",
                        OleDbType = OleDbType.Date,
                        Value = fechaDesde
                    });
                    lparamDetalles.Add(new OleDbParameter()
                    {
                        ParameterName = "@hasta",
                        OleDbType = OleDbType.Date,
                        Value = fechaHasta
                    });
                    lparamDetalles.Add(new OleDbParameter()
                    {
                        ParameterName = "@idSector",
                        OleDbType = OleDbType.Integer,
                        Value = idSector
                    });
                    dtDetalles = SelectStoreProcedure("sp_repRendimientoPorProductoPorSector", "REND_PRODUCCION_PRODUCIDO", lparamDetalles);
                    ds = new DataSet();
                    ds.Tables.Add(dtTotales);
                    ds.Tables.Add(dtDetalles);
                }

            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return ds;
        }


        #endregion

    }
}
