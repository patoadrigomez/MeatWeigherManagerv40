using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Ordenes de Ingresos
    /// </summary>
    public static partial class CDb
    {
        #region OPERACIONES DE GESTION DE ORDENES DE INGRESOS
        /***************************************************************************************
         * Metodo:	    GetOI
         *              Obtiene una Orden de Ingreso indicando su id
         * Parametro:   int idOi
         * Retorna:     (COi) instancia de la clase que contiene los datos del Operador.
        *****************************************************************************************/
        public static COi GetOI(int idOI)
        {
            OleDbDataReader recordSet;
            COi datOI = null;

            string strCmd = String.Format(
                " declare @tmpprvSAC as table (codigo varchar(20), nombre varchar(50)) " +
                " INSERT INTO @tmpprvSAC Exec sp_getProveedoresSAC " +
                " SELECT oing.id as IDOI ,oing.idOperador as IDOPERADOR, o.nombre as NOMBRE_OPERADOR, o.pasw as PASW_OPERADOR, o.tipo as TIPO_OPERADOR,oing.idEstacion as IDESTACION, oing.fecha_hora as FECHA_HORA," +
                " oing.codigoProveedorSAC as CODIGO_PROVEEDOR,p.Nombre as NOMBRE_PROVEEDOR " +
                " oing.idCertSanitario as CERT_SANITARIO, oing.Activo as ACTIVO " +
                " FROM OI oing " +
                " LEFT OUTER JOIN operadores as o ON oing.idOperador = o.id " +
                " LEFT OUTER JOIN @tmpprvSAC as p ON oing.codigoProveedorSAC = p.codigo " +
                " WHERE oing.id = {0}",idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    datOI = new COi()
                    {
                        m_id = idOI,
                        m_idEstacion = GetCampoDbInt(recordSet, "IDESTACION"),
                        m_fechaHoraCreacion = GetCampoDbDateTime(recordSet, "FECHA_HORA"),
                        m_idCertSanitario = GetCampoDbString(recordSet, "CERT_SANITARIO"),
                        m_activo = GetCampoDbBool(recordSet, "ACTIVO"),
                        m_remitos = GetListRemitosOI(idOI),
                        m_facturas = GetListFacturasOI(idOI),

                        m_Operador = new COperador()
                        {
                            m_id = GetCampoDbInt(recordSet, "IDOPERADOR"),
                            m_nombre = GetCampoDbString(recordSet, "NOMBRE_OPERADOR"),
                            m_pasw = GetCampoDbString(recordSet, "PASW_OPERADOR"),
                            m_tipo = (TYPE_OPERATOR)GetCampoDbString(recordSet, "TIPO_OPERADOR")[0]
                        },
                        m_proveedor = new CProveedorSAC()
                        {
                            Codigo = GetCampoDbString(recordSet, "CODIGO_PROVEEDOR"),
                            Nombre = GetCampoDbString(recordSet, "NOMBRE_PROVEEDOR"),
                        }
                    };
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datOI;
        }

        /**************************************************************************************************
         * Metodo:		RegistrarOI
         * Descripcion: Registra una nueva Orden de Ingreso
         * Parametros:	(COi regOI) que posee toda la informacion de la Cabecera de una Orden de Ingreso
         * Retorna:     tru si se registro.
        ***************************************************************************************************/
        public static bool RegistrarOI(ref COi regOI)
        {
            bool registradoOk = false;
            int regAfectados;

            string strCmd = String.Format(
                " INSERT INTO OI(FECHA_HORA,ACTIVO,IDOPERADOR,IDESTACION,CODIGOPROVEEDORSAC,IDCERTSANITARIO)" +
                " VALUES({{ts '{0}'}},{1},{2},{3},'{4}','{5}')",
                regOI.m_fechaHoraCreacion.ToString("yyyy-MM-dd HH:mm:ss"), regOI.m_activo ? 1 : 0, regOI.m_Operador.m_id, regOI.m_idEstacion, regOI.m_proveedor.Codigo, regOI.m_idCertSanitario);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                if (regAfectados == 1)
                {
                    regOI.m_id = GetIdOI_AfterInsert();
                    registradoOk = (regOI.m_id != 0);
                    if (registradoOk)
                    {
                        RegistrarListaRemitosOI(regOI.m_id, regOI.m_remitos);
                        RegistrarListaFacturasOI(regOI.m_id, regOI.m_facturas);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registradoOk;
        }
        /**************************************************************************************************
         * Metodo:		UpdateOI
         * Descripcion: Actualizar los datos de proveedor, remitos,facturas y numero de certificado sanitario
         *              de una Orden de Ingreso
         * Parametros:	(COi regOI) que posee toda la informacion de la Cabecera de una Orden de Ingreso
         * Retorna:     tru si se registro.
        ***************************************************************************************************/
        public static bool UpdateOI(ref COi regOI)
        {
            bool registradoOk = false;
            int regAfectados;

            string strCmd = String.Format(
                " UPDATE OI SET CODIGOPROVEEDORSAC = '{0}' , IDCERTSANITARIO = '{1}' WHERE id = {2} ",regOI.m_proveedor.Codigo, regOI.m_idCertSanitario,regOI.m_id);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                if (regAfectados == 1)
                {
                    BorrarRemitosOI(regOI.m_id);
                    BorrarFacturasOI(regOI.m_id);
                    RegistrarListaRemitosOI(regOI.m_id, regOI.m_remitos);
                    RegistrarListaFacturasOI(regOI.m_id, regOI.m_facturas);
                    registradoOk = true;
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registradoOk;
        }
        /**************************************************************************************************
         * Metodo:		BorrarOI
         * Descripcion: Elimina una Orden de Ingreso y todos sus registros vinculados a ella.
         *              Antes de eliminar una OI , la funcion verifica que la misma no tenga movimientos en 
         *              produccion. 
         * Parametro:	int idOI.
         * Retorna:     Retorna tru si la borro.
        ***************************************************************************************************/
        public static bool BorrarOI(int idOI)
        {
            bool borradoOk = false;

            int regAfectados;
            if (IsAllInStockOI(idOI))
            {
                string strCmd = String.Format(" DELETE OI WHERE id = {0}", idOI);
                try
                {
                    OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                    regAfectados = dbCommand.ExecuteNonQuery();
                    borradoOk = (regAfectados == 1);
                    if (borradoOk)
                    {
                        BorrarRemitosOI(idOI);
                        BorrarFacturasOI(idOI);
                        BorrarPesadasOI(idOI);
                    }
                }
                catch (OleDbException ex)
                {
                    throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
                }
            }
            return borradoOk;
        }

        /**************************************************************************************************
         * Metodo:		RegistrarRemitoOI
         * Descripcion: Registra una remito con vinculo a una OI
         * Parametros:	(int) idOI,remito
         * Retorna:     tru si se registro.
        ***************************************************************************************************/
        public static bool RegistrarRemitoOI(int idOi, string remito)
        {
            bool registradoOk = false;
            int regAfectados;
            string strCmd = String.Format(
                " INSERT INTO REMITOS(IDOI,REMITO)" +
                " VALUES({0},'{1}')", idOi, remito);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                registradoOk = (regAfectados == 1);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registradoOk;
        }
        /**************************************************************************************************
         * Metodo:		RegistrarFacturaOI
         * Descripcion: Registra una factura con vinculo a una OI
         * Parametros:	(int) idOI,factura
         * Retorna:     tru si se registro.
        ***************************************************************************************************/
        public static bool RegistrarFacturaOI(int idOi, string factura)
        {
            bool registradoOk = false;
            int regAfectados;
            string strCmd = String.Format(
                " INSERT INTO FACTURAS(IDOI,FACTURA)" +
                " VALUES({0},'{1}')", idOi, factura);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                registradoOk = (regAfectados == 1);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registradoOk;
        }

        /**************************************************************************************************
         * Metodo:		RegistrarListaRemitosOI
         * Descripcion: Registra una lista de remitos para una dada OI
         * Parametros:	int idOI , (List<string>) listaRemitos
         * Retorna:     tru si se registraron ok.
        ***************************************************************************************************/
        public static bool RegistrarListaRemitosOI(int idOI, List<string> listaRemitos)
        {
            bool registradoOk = false;
            try
            {
                registradoOk = true;
                foreach (string remito in listaRemitos)
                {
                    registradoOk &= RegistrarRemitoOI(idOI, remito);
                    if (!registradoOk) break;
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registradoOk;
        }
        /**************************************************************************************************
         * Metodo:		RegistrarListaFacturasOI
         * Descripcion: Registra una lista de facturas para una dada OI
         * Parametros:	int idOI , (List<string>) listaFacturas
         * Retorna:     tru si se registraron ok.
        ***************************************************************************************************/
        public static bool RegistrarListaFacturasOI(int idOI, List<string> listaFacturas)
        {
            bool registradoOk = false;
            try
            {
                registradoOk = true;
                foreach (string factura in listaFacturas)
                {
                    registradoOk &= RegistrarFacturaOI(idOI, factura);
                    if (!registradoOk) break;
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registradoOk;
        }

        /***************************************************************************************
         * Metodo:	    GetIdOI_AfterInsert
         *              Obtiene el Id de un OI generado en el ultimo insert en la tabla 
         *              OI. Se requiere del numero de estacion para asegurar que sea el 
         *              insert de esta estacion y no de otra.
         * Retorna:     (int) NuevoidOI. (si = 0 error)
        *****************************************************************************************/
        public static int GetIdOI_AfterInsert()
        {
            OleDbDataReader recordSet;
            int idLote = 0;
            string strCmd = String.Format(" SELECT MAX(id) AS newId FROM OI WHERE idestacion = {0} ", m_OperadorActivo.m_idEstacion);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    idLote = GetCampoDbInt(recordSet, "newId");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return idLote;
        }
        /***************************************************************************************
        Metodo:		GetDatSet_OICreadas
                    Crea un dataset con las ordenes de ingresos (OI) creadas.
                    provee filtro para indicar si solo se deben obtener las que estan en proceso
                    o todas. Tambien filtro para una determinada OI o todas.
        Parametros:	out DataTable OIs, bool soloEnProceso 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_OICreadas(out DataTable dtOIs, bool soloEnproceso=true,int idOI=0)
        {
            bool obtenidoSinerrorOk = false;
            dtOIs = new DataTable();
            //OI ,PROVEEDOR,CERT_SANITARIO,FECHA_HORA,ESTACION,IDOPERADOR,OPERADOR,PASW_OPERADOR,TIPO_OPERADOR,CODIGO_PROVEEDOR,ESTADO ,ACTIVO
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@soloActivas",
                    DbType = DbType.Boolean,
                    Value = soloEnproceso
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idOI",
                    DbType = DbType.Int32,
                    Value = idOI
                });
                dtOIs = SelectStoreProcedure("sp_getOIs", "OIS", lparam);

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
         * Metodo:	    GetListRemitosOI
         *              Obtiene una lista de remitos pertenecen a la OI indicada 
         * Parametro:   (int) idOI
         * Retorna:     List<string> lista de Remitos
        *****************************************************************************************/
        public static List<string> GetListRemitosOI(int idOI)
        {
            List<string> listRemitos = new List<string>();
            OleDbDataReader recordSet;

            string strCmd = String.Format(" SELECT remito as REMITO FROM REMITOS WHERE idoi = {0} ", idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    while (recordSet.Read())
                    {
                        listRemitos.Add(GetCampoDbString(recordSet, "REMITO"));
                    }
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listRemitos;
        }
        /***************************************************************************************
         * Metodo:	    GetListFacturaOI
         *              Obtiene una lista de facturas que pertenecen a la OI indicada 
         * Parametro:   (int) idOI
         * Retorna:     List<string> lista de Facturas
        *****************************************************************************************/
        public static List<string> GetListFacturasOI(int idOI)
        {
            List<string> listFacturas = new List<string>();
            OleDbDataReader recordSet;

            string strCmd = String.Format(" SELECT factura as FACTURA FROM FACTURAS WHERE idoi = {0} ", idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    while (recordSet.Read())
                    {
                        listFacturas.Add(GetCampoDbString(recordSet, "FACTURA"));
                    }
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listFacturas;
        }
        /// <summary>
        /// Verifica que la Orden de Ingreso posea todas sus piezas en stock es decir que no tenga
        /// piezas que pasaron por produccion o fueron egresadas.
        /// </summary>
        /// <param name="idOI"></param>
        /// <returns>
        /// True si todas las piezas estan en stock.
        /// </returns>
        public static bool IsAllInStockOI(int idOI)
        {
            bool allInStock = false;
            string strCmd = String.Format(" select COUNT(*) as registros from Pesadas p where p.idOI = {0} and(p.id in (select idpesaje from DLP) or "+
                                          " p.id in (select idpesaje from Egresos) or p.id in (select idpesaje from ContenedorPiezas)) ", idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                OleDbDataReader recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    allInStock = GetCampoDbInt(recordSet, "registros") == 0;
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return allInStock;
        }

        /**************************************************************************************************
         * Metodo:		CerrarOI
         * Descripcion: Marca a una OI como cerrada colocando el campo "ACTIVO" = 0
         * Parametro:	int idOi.
         * Retorna:     Retorna tru si ok.
        ***************************************************************************************************/
        public static bool CerrarOI(int idOI)
        {
            bool borradoOk = false;
            int regAfectados;
            string strCmd = String.Format(" UPDATE OI SET activo = {1} WHERE ID = {0}", idOI, 0);
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
        /**************************************************************************************************
         * Metodo:		BorrarRemitosOI
         * Descripcion: Elimina todos los remitos vinculados a una OI
         * Parametro:	int idOI.
         * Retorna:     Retorna tru si la borro.
        ***************************************************************************************************/
        public static bool BorrarRemitosOI(int idOI)
        {
            bool borradoOk = false;
            string strCmd = String.Format(" DELETE REMITOS WHERE idoi = {0}", idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                dbCommand.ExecuteNonQuery();
                borradoOk = true;
            }
            catch (OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return borradoOk;
        }
        /**************************************************************************************************
         * Metodo:		BorrarFacturasOI
         * Descripcion: Elimina todas las facturas vinculados a una OI
         * Parametro:	int idOI.
         * Retorna:     Retorna tru si la borro.
        ***************************************************************************************************/
        public static bool BorrarFacturasOI(int idOI)
        {
            bool borradoOk = false;
            string strCmd = String.Format(" DELETE FACTURAS WHERE idoi = {0}", idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                dbCommand.ExecuteNonQuery();
                borradoOk = true;
            }
            catch (OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return borradoOk;
        }
        /***************************************************************************************
         * Metodo:	    ExisteOI
         *              Indica si existe una Orden de Ingreso
         * Parametro:   (int) idOI
         * Retorna:     (bool) true si existe
        *****************************************************************************************/
        public static bool ExisteOI(int idOI)
        {
            OleDbDataReader recordSet;
            bool existe = false;
            string strCmd = String.Format(" SELECT * FROM OI WHERE  id = {0} ", idOI);
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

        #region OPERACIONES DE PESAJE PARA ORDENES DE INGRESOS
        /***************************************************************************************
         * Metodo:	    GetAcumuladosOIPorProducto
         *              Obtiene los acumulados de un producto en una dada OI (Cantidad de Pesadas ,
         *              Unidades, bruto y neto). 
         * Parametro:   int idOi,int idProducto 
         * Retorna:     (CAcumulado) instancia de la clase acumulado con los valores resultantes.
        *****************************************************************************************/
        public static CAcumulado GetAcumuladosOIPorProducto(int idOI, int idProducto)
        {
            OleDbDataReader recordSet;
            CAcumulado valuesAcum = new CAcumulado();

            string strCmd = String.Format(" SELECT COUNT(*) as TOT_PESADAS ,SUM(unidades) as TOT_UNIDADES, (SUM(pesoneto)+SUM(pesotara)) as TOT_PESOBRUTO,SUM(pesoneto) as TOT_PESONETO " +
                                          " FROM PESADAS WHERE idOI = {0} AND idproducto= {1} AND idEstacion = {2}", idOI, idProducto, m_OperadorActivo.m_idEstacion);
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

        /***************************************************************************************
         * Metodo:	    GetNewIdGrupoPesada
         *              Obtiene un nuevo identificador para asignar a un grupo de pesadas por 
         *              cantidad de bultos , para una OI especificada. Es decir el identificador de 
         *              grupo es correlativo a la OI.
         * Retorna:     (int) newId.
        *****************************************************************************************/
        public static int GetNewIdGrupoPesada(int idOI)
        {
            OleDbDataReader recordSet;
            int newId = 0;
            string strCmd = String.Format(" SELECT (max(idgrupo)+1) as ID FROM PESADAS where idoi = {0} ", idOI);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    newId = GetCampoDbInt(recordSet, "ID",1);
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return newId;
        }

        /**************************************************************************************************
         * Metodo:		BorrarPesadasOI
         * Descripcion: Elimina todas las pesadas que posea una orden de Ingreso
         * Parametro:	int idOi.
         * Retorna:     Retorna tru si la borro.
        ***************************************************************************************************/
        public static bool BorrarPesadasOI(int idOI)
        {
            bool borradoOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE PESADAS WHERE idOI = {0}", idOI);
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

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE ORDENES DE INGRESO
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_PesajesOI
         *              Obtiene un DataSet con una consulta de Movimientos de Pesadas de las Ordenes
         *              de Ingresos basado en fechas.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_IngresoAPlantaDetalle(DateTime fechaDesde, DateTime fechaHasta, string idProveedor,int idProducto,int idTipoProducto, int numTropa)
        {
            DataTable dtEgresos = null;
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
                    ParameterName = "@idProveedor",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = idProveedor
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idProducto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idTipoProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idTipoProducto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@numTropa",
                    OleDbType = OleDbType.Integer,
                    Value = numTropa
                });
                //IDPIEZA,IDOI,PROVEEDOR,SANITARIO,CODIGO_PRD,PRODUCTO,TIPO_PRD,TROPA,TIPIF,PESADA,UNDS,NETO,TARA,PUESTO,OPERADOR
                dtEgresos = SelectStoreProcedure("sp_repIngPlantaDetalle", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtEgresos;
        }

        /***************************************************************************************
        Metodo:		GetDatSet_PesadasOI
                    Crea un dataset con las pesadas existente en la OI. Las columnas son de 
                    minima informacion dado que es solo para visualizar en una grilla.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PesadasOI(int idOI, out DataSet dsPesadas)
        {
            bool obtenidoSinerrorOk = false;
            dsPesadas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //IDPESADA,GRUPO,IDOI,FECHA_HORA,OPERADOR,DESTINO,CODIGO,PRODUCTO,TROPA,TIPIF,UNDS,NETO,TARA,REMITIDO,VENCIMIENTO
                    sqlQuery = String.Format(
                    " SELECT pe.id as IDPESADA,pe.idgrupo as GRUPO,pe.idoi as IDOI,pe.fecha_hora as FECHA_HORA,ope.nombre as OPERADOR,de.nombre as DESTINO," +
                    " prd.codigoProductoSAC as CODIGO,prd.nombre as PRODUCTO,pe.numTropa as TROPA,tip.nombre as TIPIF," +
                    " pe.unidades as UNDS,pe.pesoNeto as NETO,pe.pesoTARA as TARA ,pe.pesoRemitido as REMITIDO, pe.FechaVencimiento as VENCIMIENTO" +
                    " FROM pesadas as pe " +
                    " LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id " +
                    " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id " +
                    " LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id " +
                    " LEFT OUTER JOIN Tipificaciones as tip ON pe.idtipificacion = tip.id " +
                    " WHERE pe.idoi = {0} order by pe.fecha_hora desc ", idOI);

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
         * Metodo:	    GetConsultaReporte_IngresoAPlantaTotalizadoXOiProducto
         *              Obtiene un DataSet con los totales acumulados de productos de las pesadas 
         *              que pertenecen a Ordenes de Ingresos para un rango de fechas indicado.
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_IngresoAPlantaTotalizadoXOiProducto(DateTime fechaDesde, DateTime fechaHasta, string idProveedor,int idProducto,int idTipoProducto)
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
                    ParameterName = "@idProveedor",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = idProveedor
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idProducto
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idTipoProducto",
                    OleDbType = OleDbType.Integer,
                    Value = idTipoProducto
                });

                //IDOI,PROVEEDOR,SANITARIO,COD_PRD,PRODUCTO,UNDS,NETO,TARA,REMITIDO
                dt = SelectStoreProcedure("sp_repIngPlantaTotalizado", "MOV_ACUMULADOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_IngresoAPlantaTotalizadoXDiaProveedor
         *              Obtiene un DataSet con los totales acumulados de unidades , kg , tara y remitido
         *              para todos los articulos totalizado por Dia-Proveedor 
         * Parametro    DateTime (fecha desde).
         * Parametro    DateTime (fecha hasta).
         * Parametro    int      idProveedor.
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_IngresoAPlantaTotalizadoXDiaProveedor(DateTime fechaDesde, DateTime fechaHasta, string idProveedor)
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
                    ParameterName = "@idProveedor",
                    OleDbType = OleDbType.VarChar,
                    Size = 12,
                    Value = idProveedor
                });

                //IDOI,PROVEEDOR,SANITARIO,COD_PRD,PRODUCTO,UNDS,NETO,TARA,REMITIDO
                dt = SelectStoreProcedure("sp_repIngPlantaTotalizadoXDiaProveedor", "MOV_ACUMULADOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }

        /***************************************************************************************
        Metodo:		GetConsultaCompletaOI
                    Crea un dataset con toda la informacion de las OI creadas con los detalles 
        *           de pesadas vinculados.
                    El data set posee informacion de Pesadas por OI.
        Parametros:	out DataSet (infoOI)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaCompletaOI(out DataSet dsInfoOI)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoOI = new DataSet();
            string sqlQuery;

            DataColumn[] columnsOIKeys = null;
            DataColumn[] columnsPesadasKeys = null;
            DataRelation datRelacionOI_P;

            try
            {
                int rowsOIFills = 0;
                int rowsPesadasFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = " declare @tmpprvSAC as table (codigo varchar(20), nombre varchar(50)) " +
                               " INSERT INTO @tmpprvSAC Exec sp_getProveedoresSAC " +
                               " SELECT DISTINCT oing.id as IDOI ,oing.fecha_hora as FECHA_HORA,o.nombre as OPERADOR,oing.idEstacion as IDESTACION ,p.Nombre as PROVEEDOR,oing.idCertSanitario as CERT_SANITARIO," +
                               " (CASE WHEN oing.activo = 0 OR oing.activo is null THEN 'CERRADA' ELSE 'ABIERTA' END)as ESTADO " +
                               " FROM OI oing " +
                               " LEFT OUTER JOIN operadores as o ON oing.idOperador = o.id " +
                               " LEFT OUTER JOIN @tmpprvSAC as p ON oing.codigoProveedorSAC = p.codigo " +
                               " order by oing.fecha_hora desc ";

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsOIFills = oleDbDataAdapter.Fill(dsInfoOI, "OI");

                    if (rowsOIFills != 0)
                    {
                        sqlQuery = " SELECT pe.id as IDPESADA,pe.idOI as IDOI, pe.fecha_hora as FECHA_HORA,ope.nombre as OPERADOR,de.nombre as DESTINO," +
                                   " prd.codigoProductoSAC as COD_PROD," +
                                   " prd.nombre as PRODUCTO,pe.numtropa as TROPA,tip.nombre as TIPIF," +
                                   " pe.unidades as UNDS," +
                                   " pe.pesoNeto as PESO_NETO,pe.pesoTARA as PESO_TARA,pe.pesoRemitido as PESO_REMITIDO " +
                                   " FROM pesadas as pe " +
                                   " LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id " +
                                   " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id " +
                                   " LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id " +
                                   " LEFT OUTER JOIN Tipificaciones as tip ON pe.idtipificacion = tip.id " +
                                   " WHERE pe.idoi is not null ";

                        //ejecuto el dataadapter con el nuevo query
                        oleDbDataAdapter.SelectCommand.CommandText = sqlQuery;
                        //cargo el resultado del query en el DataSet como tabla "PESADAS"
                        rowsPesadasFills = oleDbDataAdapter.Fill(dsInfoOI, "PESADAS");

                        columnsOIKeys = new DataColumn[] { dsInfoOI.Tables["OI"].Columns["IDOI"] };

                        columnsPesadasKeys = new DataColumn[] { dsInfoOI.Tables["PESADAS"].Columns["IDOI"] };
                        //creo el objeto relacion 
                        datRelacionOI_P = new DataRelation("OI_PESADA", columnsOIKeys, columnsPesadasKeys, false);
                        //sumo el objeto relacion al dataset    
                        dsInfoOI.Relations.Add(datRelacionOI_P);

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
        Metodo:		GetConsultaOI
                    Crea un dataset con toda la OI creadas que esten activas 
        Parametros:	out DataSet (infoOI)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaOI(out DataSet dsInfoOI)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoOI = new DataSet();
            string sqlQuery;

            try
            {
                int rowsOIFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = " SELECT DISTINCT oing.id as IDOI ,oing.fecha_hora as FECHA_HORA,o.nombre as OPERADOR,oing.idEstacion as IDESTACION ,p.Nombre as PROVEEDOR,oing.idCertSanitario as CERT_SANITARIO," +
                               " (CASE WHEN oing.activo = 0 OR oing.activo is null THEN 'CERRADA' ELSE 'ABIERTA' END)as ESTADO " +
                               " FROM OI oing " +
                               " LEFT OUTER JOIN operadores as o ON oing.idOperador = o.id " +
                               " LEFT OUTER JOIN proveedores as p ON oing.idProveedor = p.id " +
                               " order by oing.fecha_hora desc ";

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsOIFills = oleDbDataAdapter.Fill(dsInfoOI, "OI");
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
        Metodo:		GetConsultaPesadasOI
                    Crea un dataset con toda las pesadas que posee una OI indicada 
        Parametros:	out DataSet (infoPesadas)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaPesadasOI(out DataSet dsInfoPesadas,int OI)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoPesadas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsOIFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = String.Format(" SELECT pe.id as IDPESADA,pe.idOI as IDOI, pe.fecha_hora as FECHA_HORA,ope.nombre as OPERADOR,de.nombre as DESTINO,prd.codigoProductoSAC as COD_PROD," +
                                   " prd.nombre as PRODUCTO,pe.unidades as UNIDADES,pe.pesoNeto as PESO_NETO,pe.pesoTARA as PESO_TARA " +
                                   " FROM pesadas as pe " +
                                   " LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id " +
                                   " LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id " +
                                   " LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id " +
                                   " WHERE pe.idoi = {0}",OI);

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsOIFills = oleDbDataAdapter.Fill(dsInfoPesadas, "OI_PESADAS");
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
        #endregion

    }
}
