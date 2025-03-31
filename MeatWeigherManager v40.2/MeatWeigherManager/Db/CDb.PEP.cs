using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Pesajes en Produccion
    /// </summary>
    public static partial class CDb
    {
        #region OPERACIONES DE GESTION DE PESAJES EN PRODUCCION


        #endregion

        #region OPERACIONES DE PESAJES EN PRODUCCION
        /***************************************************************************************
         * Metodo:	    GetAcumuladosLotePorProducto
         *              Obtiene los acumulados de un producto en un Lote (Cantidad de Pesadas ,
         *              Unidades, bruto y neto). 
         * Parametro:   DateTime Lote,int idProducto 
         * Retorna:     (CAcumulado) instancia de la clase acumulado con los valores resultantes.
        *****************************************************************************************/
        public static CAcumulado GetAcumuladosLotePorProducto(DateTime dateLote, int idProducto)
        {
            OleDbDataReader recordSet;
            CAcumulado valuesAcum = new CAcumulado();

            string strCmd = String.Format(" SELECT COUNT(*) as TOT_PESADAS ,SUM(unidades) as TOT_UNIDADES, (SUM(pesoneto)+SUM(pesotara)) as TOT_PESOBRUTO,SUM(pesoneto) as TOT_PESONETO " +
                                          " FROM PESADAS WHERE idoi is null AND CAST(FECHA_HORA as DATE) =  {{d '{0}'}} AND idproducto= {1} AND idEstacion = {2}", dateLote.ToString("yyyy-MM-dd"), idProducto, m_OperadorActivo.m_idEstacion);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    valuesAcum.m_pesadas = GetCampoDbInt(recordSet, "TOT_PESADAS");
                    valuesAcum.m_unidades = GetCampoDbInt(recordSet, "TOT_UNIDADES");
                    valuesAcum.m_bruto = GetCampoDbFloat(recordSet, "TOT_PESOBRUTO");
                    valuesAcum.m_neto = GetCampoDbFloat(recordSet, "TOT_PESONETO");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return valuesAcum;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE ORDENES DE INGRESO

        /***************************************************************************************
        Metodo:		GetDatSet_PesadasLote
                    Crea un dataset con las pesadas existente en un Lote de Produccion. Las columnas son de 
                    minima informacion dado que es solo para visualizar en una grilla.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PesadasLote(DateTime dateLote, out DataSet dsPesadas)
        {
            bool obtenidoSinerrorOk = false;
            dsPesadas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = String.Format(
                    " SELECT pe.id as IDPESADA,pe.fecha_hora as FECHA_HORA,ope.nombre as OPERADOR,sec.nombre as SECTOR,de.nombre as DESTINO,('('+ prd.codigoProductoSAC +') ' + prd.nombre) as PRODUCTO," +
                    " pe.unidades as UNIDADES,pe.pesoNeto as NETO,pe.pesoTARA as TARA " +
                    " FROM pesadas as pe " +
                    " LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id " +
                    " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id " +
                    " LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id " +
                    " LEFT OUTER JOIN Sectores as sec ON pe.idsector = sec.id " +
                    " WHERE pe.idoi is null AND CAST(pe.fecha_hora as DATE) = {{d '{0}'}} order by pe.fecha_hora desc ", dateLote.ToString("yyyy-MM-dd"));

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsPDESFills = oleDbDataAdapter.Fill(dsPesadas, "PESADAS");
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
        Metodo:		GetConsultaPesajesLote
                    Crea un dataset con toda la informacion de las OI creadas con los detalles 
        *           de pesadas vinculados.
                    El data set posee informacion de Pesadas por OI.
        Parametros:	out DataSet (infoOI)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaCompletaLotes(out DataSet dsInfoLote)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoLote = new DataSet();
            string sqlQuery;

            DataColumn[] columnsLoteKeys = null;
            DataColumn[] columnsPesadasKeys = null;
            DataRelation datRelacionLote_P;

            try
            {
                int rowsLoteFills = 0;
                int rowsPesadasFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery =
                    " SELECT DISTINCT CAST(p.fecha_hora AS DATE) as IDLOTE ,CONVERT(varchar(10), CAST(p.fecha_hora as DATE), 103) AS LOTE FROM PESADAS p " +
                    " WHERE p.idoi is null order by IDLOTE desc ";

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsLoteFills = oleDbDataAdapter.Fill(dsInfoLote, "LOTES");

                    if (rowsLoteFills != 0)
                    {
                        sqlQuery =
                            " SELECT * FROM( " +
                            " SELECT TIPO = 'PIEZA', pe.id as NRO, EST='  ',pe.fecha_hora as FECHA_HORA, ope.nombre as OPERADOR," +
                            " de.nombre as DESTINO, prd.codigoProductoSAC as COD_PROD, prd.nombre as PRODUCTO, CAST(pe.fecha_hora AS DATE) as IDLOTE," +
                            " pe.unidades as UNDS, pe.pesoNeto as NETO, pe.pesoTARA as TARA" +
                            " FROM pesadas as pe " +
                            " LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id " +
                            " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id " +
                            " LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id " +
                            " WHERE pe.idoi is null " +
                            " UNION " +
                            " SELECT TIPO = 'CAJA', cn.id as NRO, (case when cn.fecha_desarmado is null then 'ARM' else 'DES' end) as EST, cn.fecha_hora as FECHA_HORA, ope.nombre as OPERADOR, " +
                            " de.nombre as DESTINO, prd.codigoProductoSAC as COD_PROD, prd.nombre as PRODUCTO, CAST(cn.fecha_hora AS DATE) as IDLOTE, " +
                            " cn.unidades as UNDS, cn.pesoNeto as NETO, cn.pesoTARA as TARA " +
                            " FROM Contenedores cn " +
                            " LEFT OUTER JOIN Operadores ope ON ope.id = cn.idoperador " +
                            " LEFT OUTER JOIN Productos as prd ON cn.idproducto = prd.id " +
                            " LEFT OUTER JOIN Destinos as de ON cn.iddestino = de.id " +
                            " WHERE cn.idTipo = 'CAJ' " +
                            " UNION " +
                            " SELECT TIPO = 'COMBO', cn.id as NRO, (case when cn.fecha_desarmado is null then 'ARM' else 'DES' end) as EST,cn.fecha_hora as FECHA_HORA, ope.nombre as OPERADOR, " +
                            " de.nombre as DESTINO, prd.codigoProductoSAC as COD_PROD, prd.nombre as PRODUCTO, CAST(cn.fecha_hora AS DATE) as IDLOTE, " +
                            " cn.unidades as UNDS, cn.pesoNeto as NETO, cn.pesoTARA as TARA " +
                            " FROM Contenedores cn " +
                            " LEFT OUTER JOIN Operadores ope ON ope.id = cn.idoperador " +
                            " LEFT OUTER JOIN Productos as prd ON cn.idproducto = prd.id " +
                            " LEFT OUTER JOIN Destinos as de ON cn.iddestino = de.id " +
                            " WHERE cn.idTipo = 'CMB')T " +
                            "  ORDER BY T.FECHA_HORA desc ";

                        //ejecuto el dataadapter con el nuevo query
                        oleDbDataAdapter.SelectCommand.CommandText = sqlQuery;
                        //cargo el resultado del query en el DataSet como tabla "PESADAS"
                        rowsPesadasFills = oleDbDataAdapter.Fill(dsInfoLote, "PESADAS");

                        columnsLoteKeys = new DataColumn[] { dsInfoLote.Tables["LOTES"].Columns["IDLOTE"] };

                        columnsPesadasKeys = new DataColumn[] { dsInfoLote.Tables["PESADAS"].Columns["IDLOTE"] };
                        //creo el objeto relacion 
                        datRelacionLote_P = new DataRelation("LOTES_PESADA", columnsLoteKeys, columnsPesadasKeys, false);
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

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ProduccionDetalle
         *              Obtiene un DataSet con una consulta de Movimientos de Pesadas de piezas, cajas y combos
         *              producidas en funcion a los filtros por fecha , sector,tipo de producto,producto y tipo
         *              de bulto (pieza,caja,combo).
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ProduccionDetalle(DateTime fechaDesde, DateTime fechaHasta,int idSector,int idTipoProducto,int idProducto,string tipoBulto)
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
                    ParameterName = "@tipo",
                    OleDbType = OleDbType.VarChar,
                    Size=20,
                    Value = tipoBulto=="TODOS"?"":tipoBulto
                });
                //TIPO,NRO,LOTE,COD_PRD,PRODUCTO,TIPO_PRD,CREADA,DESTINO,SECTOR,PUESTO,OPERADOR,UNDS,NETO,TARA
                dt = SelectStoreProcedure("sp_repProduccionDetalladoFull", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_PesajeCajasEnProduccionDetalle
         *              Obtiene un DataSet con una consulta de Movimientos de Pesadas de cajas
         *              en el sector de produccion.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ContenedoresEnProduccionDetalle(DateTime fechaDesde, DateTime fechaHasta,int idTipoProducto,int idProducto)
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
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idProducto
                });

                //TIPO,NRO,CREADO,PRODUCTO,DESTINO,SECTOR,UNDS,BRUTO,TARA,NETO
                dt = SelectStoreProcedure("sp_repContenedoresProduccionDetalle", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_PesajeCajasEnProduccionTotalizadoPorDestino
         *              Obtiene un DataSet con una consulta de Movimientos de Pesadas de cajas
         *              en el sector de produccion.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ContenedoresEnProduccionTotalizadoPorDestino(DateTime fechaDesde, DateTime fechaHasta,int idTipoProducto,int idProducto)
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
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idProducto
                });
                //TIPO,PRODUCTO,DESTINO,BULTOS,UNIDADES,NETO
                dt = SelectStoreProcedure("sp_repContenedoresProduccionTotalizadoPorDestino", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_PiezasProducidasTotalizado
         *              Obtiene un DataSet con una consulta de totalizados de produccion con filtro por
         *              fechas , sector,tipo de producto,producto y tipo de bulto (pieza,caja,combo).
         *              Si tipo de bulto contiene "TODOS" no se filtrara el tipo de bulto.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ProduccionTotalizado(DateTime fechaDesde, DateTime fechaHasta,int idSector,
            int idTipoProducto,int idProducto,string tipoBulto)
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
                    ParameterName = "@tipo",
                    OleDbType = OleDbType.VarChar,
                    Size = 20,
                    Value = tipoBulto == "TODOS"?"":tipoBulto
                });
                //CODIGO,PRODUCTO,TOTAL_NETO
                dt = SelectStoreProcedure("sp_repProduccionTotalizadoFull", "MOV_PESADAS", lparam);
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
