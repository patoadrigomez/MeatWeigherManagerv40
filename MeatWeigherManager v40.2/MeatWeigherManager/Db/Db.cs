using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Db
{

    /// <summary>
    /// Clase para Acceso a la base de datos
    /// </summary>
    public static partial class CDb
    {
        public static OleDbConnection m_oleDbConnection;
        static OleDbDataAdapter m_oleDbDataAdapter;
        static string m_connectionString;
        public static DataSet m_dataSetResultConsultas;
        public static OleDbDataAdapter m_dataAdapter;
        public static bool isOpen = false;
        public static COperador m_OperadorActivo;
        public enum TypeSecurity
        {
            SQL,
            SSPI
        }
        #region METDOS GLOBALES DE ACCESO A LA BASE DE DATOS
        // Cadena de conexion por Seguridad SQL
        static CDb()
        {
            m_OperadorActivo = new COperador();
            m_oleDbConnection = new OleDbConnection();
            m_oleDbDataAdapter = new OleDbDataAdapter();
        }

        /********************************************************************
            Open Data Base
        **********************************************************************/
        public static bool Open(string nombreServidorSQL, string nombreBaseDeDatos, string nombreUsuario, string passwordUsuario, TypeSecurity tSecurity = TypeSecurity.SQL)
        {
            try
            {
                if (isOpen)
                {
                    m_oleDbConnection.Close();
                    isOpen = false;
                }
                if (tSecurity == TypeSecurity.SQL)
                {

                    m_connectionString = "User ID=" + nombreUsuario +
                                                ";Data Source=" + nombreServidorSQL +
                                                ";Initial Catalog=" + nombreBaseDeDatos +
                                                ";password=" + passwordUsuario +
                                                ";provider=SQLOLEDB";
                }
                else
                {
                    m_connectionString = "Integrated Security=SSPI" +
                                                ";Data Source=" + nombreServidorSQL +
                                                ";Initial Catalog=" + nombreBaseDeDatos +
                                                ";provider=SQLOLEDB";
                }
                m_oleDbConnection.ConnectionString = m_connectionString;
                m_oleDbConnection.Open();
                isOpen = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return isOpen;
        }

        /// <summary>
        /// Crear una conexion con el servidor de base de datos SQL SERVER - No requiere el nombre de la base de datos.
        /// </summary>
        /// <param name="nombreServidorSQL"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="passwordUsuario"></param>
        /// <param name="tSecurity"></param>
        /// <returns></returns>
        public static OleDbConnection OpenConnectionDB(string nombreServidorSQL, string nombreUsuario, string passwordUsuario, TypeSecurity tSecurity = TypeSecurity.SQL)
        {
            OleDbConnection oledbConn = new OleDbConnection();
            string connectionString = "";
            try
            {
                if (tSecurity == TypeSecurity.SQL)
                {

                    connectionString = "User ID=" + nombreUsuario +
                                                ";Data Source=" + nombreServidorSQL +
                                                ";password=" + passwordUsuario +
                                                ";provider=SQLOLEDB";
                }
                else
                {
                    m_connectionString = "Integrated Security=SSPI" +
                                                ";Data Source=" + nombreServidorSQL +
                                                 ";provider=SQLOLEDB";
                }
                oledbConn.ConnectionString = connectionString;
                oledbConn.Open();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return oledbConn;
        }

        /// <summary>
        /// Realiza un backup de una base de datos.
        /// </summary>
        /// <param name="nombreServidorSQL"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="passwordUsuario"></param>
        /// <param name="nameDb"></param>
        /// <param name="pathFile"></param>
        /// <param name="tSecurity"></param>
        /// <returns></returns>
        public static bool MakeBackup(string nombreServidorSQL, string nombreUsuario, string passwordUsuario, string nameDb,
            string pathFile, TypeSecurity tSecurity = TypeSecurity.SQL)
        {
            bool makeOk = false;
            string pathnamefileBackup = pathFile + "\\" + nameDb + DateTime.Now.ToString("dd-MM-yyyy-hh.mm.ss.bak");

            try
            {
                OleDbDataReader recordSet;
                OleDbConnection oledbcon = OpenConnectionDB(nombreServidorSQL, nombreUsuario, passwordUsuario, tSecurity);
                if (oledbcon.State == ConnectionState.Open)
                {
                    /*  Crea el backup en el servidor   */
                    string strCmd = String.Format("BACKUP DATABASE {0} TO  DISK = '{1}'", nameDb, pathnamefileBackup);
                    OleDbCommand dbCommand = new OleDbCommand(strCmd, oledbcon);
                    dbCommand.ExecuteNonQuery();

                    /*  Verifica que el archivo de backup haya sido creado en el servidor   */
                    strCmd = String.Format("DECLARE @result INT declare @path varchar(512) = '{0}' EXEC master.dbo.xp_fileexist @path, @result OUTPUT select cast(@result as bit) as result ", pathnamefileBackup);
                    dbCommand = new OleDbCommand(strCmd, oledbcon);
                    recordSet = dbCommand.ExecuteReader();
                    recordSet.Read();
                    if (recordSet.HasRows)
                    {
                        makeOk = GetCampoDbBool(recordSet, "result");
                    }
                    recordSet.Close();
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return makeOk;
        }


        /// <summary>
        /// Restaura una base de datos a partir de un archivo de copia de seguridad. 
        /// </summary>
        /// <param name="nombreServidorSQL"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="passwordUsuario"></param>
        /// <param name="nameDb"></param>
        /// <param name="fullpathFile"></param>
        /// <param name="tSecurity"></param>
        /// <returns></returns>
        public static bool RestoreBackupDB(string nombreServidorSQL, string nombreUsuario, string passwordUsuario, string nameDb,
            string fullpathFile, TypeSecurity tSecurity = TypeSecurity.SQL)
        {
            bool makeOk = false;
            int test = 0;
            try
            {
                OleDbConnection oledbcon = OpenConnectionDB(nombreServidorSQL, nombreUsuario, passwordUsuario, tSecurity);
                if (oledbcon.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand(String.Format("ALTER DATABASE[{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ", nameDb), oledbcon);
                    test = dbCommand.ExecuteNonQuery();
                    dbCommand = new OleDbCommand(String.Format("USE MASTER RESTORE DATABASE [{0}] FROM DISK='{1}' WITH REPLACE", nameDb, fullpathFile), oledbcon);
                    test = dbCommand.ExecuteNonQuery();
                    dbCommand = new OleDbCommand(String.Format("ALTER DATABASE[{0}] SET MULTI_USER", nameDb), oledbcon);
                    test = dbCommand.ExecuteNonQuery();
                    makeOk = true;
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return makeOk;
        }

        /// <summary>
        /// Obtiene el directorio de backup por defecto del sql server
        /// </summary>
        /// <returns></returns>
        public static string GetPathDirectoryDefaulBackupSqlServer()
        {
            string pathDirectory = "";
            OleDbDataReader recordSet;
            string strCmd = @"DECLARE @path NVARCHAR(4000) EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer',N'BackupDirectory',@path OUTPUT,'no_output' select @path as Directory";
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                recordSet.Read();
                if (recordSet.HasRows)
                {
                    pathDirectory = GetCampoDbString(recordSet, "Directory");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return pathDirectory;
        }

        /***************************************************************************************
        Metodo:		ValidarOperador
                    Verifica si existe el Usuario y contrasela indicado
                    Si existe carga las propiedades de este en la clase m_operadorActivo.
        Parametros:	string Nombre
        Parametros:	string Password.
        Retorna:    True o False. 
        *****************************************************************************************/
        public static bool validarOperador(string nombreOperador, string password, int idEstacionActiva)
        {
            bool existe = false;
            if (nombreOperador != "" && password != "")
            {
                OleDbDataReader recordSet;
                string strCmd = String.Format("SELECT id,tipo FROM Operadores WHERE Nombre = '{0}' AND Pasw = '{1}'", nombreOperador, password);
                try
                {
                    OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                    recordSet = dbCommand.ExecuteReader();
                    recordSet.Read();
                    if (recordSet.HasRows)
                    {
                        m_OperadorActivo.m_id = Convert.ToInt32(recordSet.GetValue(0));
                        m_OperadorActivo.m_tipo = (TYPE_OPERATOR)Convert.ToChar(recordSet.GetValue(1));
                        m_OperadorActivo.m_nombre = nombreOperador;
                        m_OperadorActivo.m_idEstacion = idEstacionActiva;
                        existe = true;
                    }
                    recordSet.Close();
                }
                catch (OleDbException e)
                {
                    throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
                }
            }
            return existe;
        }

        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockDetalle
         *              Obtiene un DataSet con todas las piezas que se encuentran en stock hasta fecha. 
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockDetalleFull(int idTipoProducto, DateTime fechaHasta, int idProducto, int idUbicacion)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idtipoProducto",
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
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idUbicacion",
                    OleDbType = OleDbType.Integer,
                    Value = idUbicacion
                });
                //TIPO,NRO,LOTE,UBICACION,PRODUCTO,TROPA,TIPIF,UNIDADES,NETO
                dt = SelectStoreProcedure("sp_repExistenciaEnStockDetalle", "EXISTENCIA_STOCK", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockDetalleFullPorVencimineto
         *              Obtiene un DataSet con todas las piezas que se encuentran en stock hasta 
         *              fecha ordenadas por su fecha de vencimiento. 
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockDetalleFullPorVencimineto(int idTipoProducto, DateTime fechaHasta, int idProducto, int idUbicacion)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idtipoProducto",
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
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idUbicacion",
                    OleDbType = OleDbType.Integer,
                    Value = idUbicacion
                });

                //TIPO,NRO,LOTE,UBICACION,PRODUCTO,UNIDADES,NETO,VENCIMIENTO
                dt = SelectStoreProcedure("sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento", "EXISTENCIA_STOCK", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockDetalleFullEnProximidadVencimineto
         *              Obtiene un DataSet con todas las piezas que se encuentran en stock hasta 
         *              fecha y que se encuentren en proximidad de vencimiento segun parametro de 
         *              Dias de Proximidad de Vencimiento definido en la tabla Parametros de la
         *              base de datos. 
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockDetalleFullEnProximidadVencimineto(int idTipoProducto, DateTime fechaHasta, int idProducto, int idUbicacion)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idtipoProducto",
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
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idUbicacion",
                    OleDbType = OleDbType.Integer,
                    Value = idUbicacion
                });
                //TIPO,NRO,LOTE,UBICACION,PRODUCTO,UNIDADES,NETO,VENCIMIENTO
                dt = SelectStoreProcedure("sp_repExistenciaEnStockDetalleProximidadVencimiento", "EXISTENCIA_STOCK", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockTotalizado
         *              Obtiene un DataSet con todas las piezas que se encuentran en stock. 
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockTotalizado(int idTipoProducto, DateTime fechaHasta, int idProducto, int idUbicacion)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
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
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idUbicacion",
                    OleDbType = OleDbType.Integer,
                    Value = idUbicacion
                });
                //TIPO,PRODUCTO,CODIGO,BULTOS,UNIDADES,NETO
                dt = SelectStoreProcedure("sp_repExistenciaEnStockTotalizado", "EXISTENCIA_STOCK", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockFullTotalizadoPorDestino
         *              Obtiene un DataSet con todas las piezas y contenedores que se encuentran en stock agrupadas por destino. 
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockFullTotalizadoPorDestino(int idTipoProducto, DateTime fechaHasta, int idProducto, int idUbicacion)
        {
            DataTable dt = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
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
                    ParameterName = "@hasta",
                    OleDbType = OleDbType.Date,
                    Value = fechaHasta
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idUbicacion",
                    OleDbType = OleDbType.Integer,
                    Value = idUbicacion
                });
                //TIPO,PRODUCTO,CODIGO,UBICACION,BULTOS,UNIDADES,PESONETO
                dt = SelectStoreProcedure("sp_repExistenciaEnStockFullTotalizadoPorDestino", "EXISTENCIA_STOCK", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    GetMovimientosPieza
         *              Obtiene un historico de movimientos de una pieza o contenedor en todos
         *              los procesos del sistema : Ingreso a Planta, Ingreso a Produccion , Pesaje
         *              en produccion, Confeccion de Cajas , Confeccion de combos y Egreso.
         * Parametro    int id (identificador de pieza o contenedor).
         * Parametro    bool esContenedor (true si el id es de un contenedor.
         * Retorna:     DataTable 
        *****************************************************************************************/
        public static DataTable GetMovimientosPieza(int id, bool esContenedor = false)
        {
            //MOV,FECHA,PRODUCTO,LOTE,OI,PROVEEDOR,SANITARIO,CONTENEDOR,DESTINO,CLIENTE,COMPROBANTE,SECTOR,OPERADOR,ESTACION,UNIDADES,NETO,TARA,PESO_REMITIDO
            DataTable dtResult = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@id",
                    OleDbType = OleDbType.Integer,
                    Value = id
                });
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@esContenedor",
                    DbType = DbType.Boolean,
                    Value = esContenedor
                }); ;

                dtResult = SelectStoreProcedure("sp_getMovimientos", "MOVIMIENTOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtResult;
        }

        #endregion

        #region METODOS DE USO GENERAL
        /**************************************************************************************************
         * Metodo:		RegistrarPesada
         * Descripcion: Crea un registro de pesada en la tabla PESADAS
         * Parametro:	(ref CRegPesada) regPesada: Todos los datos para insertar el registro
         * Parametro:	(bool assingNowDate) true si al registrar la pesada se asigna la fecha del dia. False
         *              si se utiliza la fecha que trae los datos de la estructura regPesada.
         * Retorna:     Retorna tru si la registracion fue ok.
        ***************************************************************************************************/
        public static bool RegistrarPesada(ref CPesada regPesada, bool assignNowDate = true)
        {
            bool registracionOk = false;

            int regAfectados;

            string strCmd = String.Format(" INSERT INTO Pesadas(IDOPERADOR,IDESTACION,IDOI,FECHA_HORA,IDPRODUCTO,UNIDADES,PESONETO,PESOTARA,IDGRUPO,IDDESTINO,IDSECTOR,IDPIEZAPADRE,PESOREMITIDO,NUMTROPA,IDTIPIFICACION,FECHAVENCIMIENTO, MANUAL)" +
                                          " VALUES({0},{1},{2},{{ts '{3}'}},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}, {{ts '{15}'}}, {16})",
                                          regPesada.Operador.m_id,
                                          regPesada.IdEstacion,
                                          regPesada.Oi == null || regPesada.Oi.m_id == 0 ? "null" : regPesada.Oi.m_id.ToString(),
                                          regPesada.FechaHora.ToString("yyyy-MM-dd HH:mm:ss"),
                                          regPesada.Producto.Id,
                                          regPesada.Unidades,
                                          regPesada.PesoNeto.ToString().Replace(',', '.'),
                                          regPesada.PesoTara.ToString().Replace(',', '.'),
                                          regPesada.IdGrupo != 0 ? regPesada.IdGrupo.ToString() : "null",
                                          regPesada.Destino.Id,
                                          regPesada.Sector.Id,
                                          regPesada.IdPiezaPadre != 0 ? regPesada.IdPiezaPadre.ToString() : "null",
                                          regPesada.PesoRemitido.ToString().Replace(',', '.'),
                                          regPesada.Tropa.Numero !=0 ? regPesada.Tropa.Numero.ToString() : "null",
                                          regPesada.Tropa.Tipificacion.Id != 0 ? regPesada.Tropa.Tipificacion.Id.ToString() : "null",
                                          regPesada.FechaVencimiento.ToString("yyyy-MM-dd 00:00:00"),
                                          regPesada.Manual ? 1 : 0); 
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                regAfectados = dbCommand.ExecuteNonQuery();
                registracionOk = (regAfectados == 1);
                if (registracionOk)
                    regPesada.Id = GetLastIdPesada();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registracionOk;
        }

        /***************************************************************************************
         * Metodo:	    GetPesada
         *              Obtiene todos los datos de una Pesada indicando su ID
         * Parametro:   int idPesada
         * Retorna:     (CPesada) instancia de la clase que contiene los datos de la Pesada.
        *****************************************************************************************/
        public static CPesada GetPesada(int idPesada)
        {
            CPesada datPesada = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idPesada",
                    DbType = DbType.Int32,
                    Value = idPesada
                });
                DataTable dt = SelectStoreProcedure("sp_getPesada", "PESADA", lparam);

                if (dt != null && dt.Rows.Count > 0)
                {
                    datPesada = new CPesada();
                    datPesada.Id = GetValueColumn<int>(dt.Rows[0], "ID");
                    datPesada.IdPiezaPadre = GetValueColumn<int>(dt.Rows[0], "IDPIEZAPADRE");
                    datPesada.FechaHora = GetValueColumn<DateTime>(dt.Rows[0], "FECHA_HORA_PESADA");
                    datPesada.IdEstacion = GetValueColumn<int>(dt.Rows[0], "IDESTACION_PESADA");
                    datPesada.Unidades = GetValueColumn<int>(dt.Rows[0], "UNIDADES_PESADA");
                    datPesada.PesoNeto = GetValueColumn<float>(dt.Rows[0], "PESO_NETO");
                    datPesada.PesoRemitido = GetValueColumn<float>(dt.Rows[0], "PESO_REMITIDO");
                    datPesada.PesoTara = GetValueColumn<float>(dt.Rows[0], "PESO_TARA");
                    datPesada.FechaVencimiento = GetValueColumn<DateTime>(dt.Rows[0], "FECHA_VENCIMIENTO");

                    datPesada.Operador = new COperador();
                    datPesada.Operador.m_id = GetValueColumn<int>(dt.Rows[0], "IDOPERADOR_PESADA");
                    datPesada.Operador.m_nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_OPERADOR_PESADA");
                    datPesada.Operador.m_pasw = GetValueColumn<string>(dt.Rows[0], "PASW_OPERADOR_PESADA");
                    datPesada.Operador.m_tipo = (TYPE_OPERATOR)GetValueColumn<string>(dt.Rows[0], "TIPO_OPERADOR_PESADA")[0];

                    datPesada.Oi = new COi();
                    datPesada.Oi.m_id = GetValueColumn<int>(dt.Rows[0], "IDOI");
                    datPesada.Oi.m_idEstacion = GetValueColumn<int>(dt.Rows[0], "IDESTACION_OI");
                    datPesada.Oi.m_fechaHoraCreacion = GetValueColumn<DateTime>(dt.Rows[0], "FECHA_HORA_OI");
                    datPesada.Oi.m_idCertSanitario = GetValueColumn<string>(dt.Rows[0], "CERT_SANITARIO");
                    datPesada.Oi.m_activo = GetValueColumn<bool>(dt.Rows[0], "ACTIVO");
                    datPesada.Oi.m_Operador = new COperador();
                    datPesada.Oi.m_Operador.m_id = GetValueColumn<int>(dt.Rows[0], "IDOPERADOR_OI");
                    datPesada.Oi.m_Operador.m_nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_OPERADOR_OI");
                    datPesada.Oi.m_Operador.m_pasw = GetValueColumn<string>(dt.Rows[0], "PASW_OPERADOR_OI");
                    datPesada.Oi.m_Operador.m_tipo = (TYPE_OPERATOR)GetValueColumn<string>(dt.Rows[0], "TIPO_OPERADOR_OI", TYPE_OPERATOR.USUARIO.ToString().Substring(0, 1))[0];

                    datPesada.Oi.m_proveedor = new CProveedorSAC();
                    datPesada.Oi.m_proveedor.Codigo = GetValueColumn<string>(dt.Rows[0], "CODIGO_PROVEEDOR");
                    datPesada.Oi.m_proveedor.Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_PROVEEDOR");

                    datPesada.Producto = new CProducto();
                    datPesada.Producto.Id = GetValueColumn<int>(dt.Rows[0], "IDPRODUCTO");
                    datPesada.Producto.Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_PRODUCTO");
                    datPesada.Producto.ProductoSAC = new CProductoSAC();
                    datPesada.Producto.ProductoSAC.Codigo = GetValueColumn<string>(dt.Rows[0], "CODIGO_PRODUCTO_SAC");
                    datPesada.Producto.ProductoSAC.Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_PRODUCTO_SAC");
                    datPesada.Producto.ProductoSAC.Alias = GetValueColumn<string>(dt.Rows[0], "ALIAS_PRODUCTO_SAC");


                    datPesada.Producto.m_tipo = new CTipoProducto();
                    datPesada.Producto.m_tipo.Id = GetValueColumn<int>(dt.Rows[0], "IDTIPO_PRODUCTO");
                    datPesada.Producto.m_tipo.Nombre = GetValueColumn<string>(dt.Rows[0], "TIPO_PRODUCTO");

                    datPesada.Producto.CodSenasa = GetValueColumn<string>(dt.Rows[0], "NUMSENASA_PRODUCTO");
                    datPesada.Producto.PesoNetoPredefinido = GetValueColumn<float>(dt.Rows[0], "PESONETOPREDEF_PRODUCTO");
                    datPesada.Producto.PesoTaraPredefinida = GetValueColumn<float>(dt.Rows[0], "PESOTARAPREDEF_PRODUCTO");
                    datPesada.Producto.RendimientoSTD = GetValueColumn<float>(dt.Rows[0], "REND_PRODUCTO");
                    datPesada.Producto.UnidadesPredefinidas = GetValueColumn<int>(dt.Rows[0], "UNIDADESPREDEF_PRODUCTO");
                    //datPesada.Producto.DiasVencimientoPredefinido = GetValueColumn<int>(dt.Rows[0], "DIASVENCIMIENTO_PRODUCTO");
                    
                    datPesada.Producto.EsInsumo = GetValueColumn<bool>(dt.Rows[0], "ESINSUMO_PRODUCTO");
                    datPesada.Producto.EsPesable = GetValueColumn<bool>(dt.Rows[0], "ESPESABLE_PRODUCTO");
                    datPesada.Producto.EsTropa = GetValueColumn<bool>(dt.Rows[0], "ESTROPA_PRODUCTO");
                    datPesada.Producto.EsCombo = GetValueColumn<bool>(dt.Rows[0], "ESCOMBO_PRODUCTO");
                    datPesada.Producto.EsCaja = GetValueColumn<bool>(dt.Rows[0], "ESCAJA_PRODUCTO");
                    datPesada.Producto.NombreEtiL1 = GetValueColumn<string>(dt.Rows[0], "NOMBREL1_PRODUCTO");
                    datPesada.Producto.NombreEtiL2 = GetValueColumn<string>(dt.Rows[0], "NOMBREL2_PRODUCTO");
                    datPesada.Producto.NombreEtiL3 = GetValueColumn<string>(dt.Rows[0], "NOMBREL3_PRODUCTO");
                    datPesada.Producto.NombreEtiL4 = GetValueColumn<string>(dt.Rows[0], "NOMBREL4_PRODUCTO");
                    datPesada.Producto.NombreEtiL5 = GetValueColumn<string>(dt.Rows[0], "NOMBREL5_PRODUCTO");
                    datPesada.Producto.NombreEtiL6 = GetValueColumn<string>(dt.Rows[0], "NOMBREL6_PRODUCTO");
                    datPesada.Producto.TextAuxEtiL1 = GetValueColumn<string>(dt.Rows[0], "TEXTAUXL1_PRODUCTO");
                    datPesada.Producto.TextAuxEtiL2 = GetValueColumn<string>(dt.Rows[0], "TEXTAUXL2_PRODUCTO");
                    datPesada.Producto.Etiqueta = new CEtiqueta()
                    {
                        Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                        Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                        Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                        IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                    };

                    datPesada.Destino = new CDestino();
                    datPesada.Destino.Id = GetValueColumn<int>(dt.Rows[0], "IDDESTINO");
                    datPesada.Destino.Nombre = GetValueColumn<string>(dt.Rows[0], "DESTINO");

                    datPesada.Tropa = new CTropa();
                    datPesada.Tropa.Numero = GetValueColumn<int>(dt.Rows[0], "NUMTROPA");
                    datPesada.Tropa.Tipificacion = new Tipificacion()
                    {
                        Id=  GetValueColumn<int>(dt.Rows[0], "IDTIPIFICACION"),
                        Nombre= GetValueColumn<string>(dt.Rows[0], "NOMBRE_TIPIFICACION")
                    };

                    datPesada.Sector = new CSector();
                    datPesada.Sector.Id = GetValueColumn<int>(dt.Rows[0], "IDSECTOR");
                    datPesada.Sector.Nombre = GetValueColumn<string>(dt.Rows[0], "SECTOR");

                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datPesada;
        }
        /***************************************************************************************
         * Metodo:	    ActualizarPesoNetoUnidadesPesada
         *              Actualiza los campos pesoneto y unidades de un registro de pesada indicando 
         *              su id de pieza.
         *              
         * Parametro:   int idpieza.
         * 
         * Retorna:     true if ok.
        *****************************************************************************************/
        public static bool ActualizarPesoNetoUnidadesPesada(int idPieza, float newPesoNeto, int newUnidades, float pesoRemitido)
        {
            bool registracionOk = false;

            int regAfectados;

            string strCmd = String.Format(" UPDATE PESADAS set PESONETO = {0} , UNIDADES = {1} ,PESOREMITIDO = {2} WHERE id = {3}", newPesoNeto.ToString().Replace(',', '.'), newUnidades, pesoRemitido.ToString().Replace(',', '.'), idPieza);
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
        /***************************************************************************************
         * Metodo:	    GetProducto
         *              Obtiene el registro de datos de un producto indicando su id
         * Parametro:   int idProducto
         * Retorna:     (CProducto) instancia de la clase que contiene los datos del Producto.
        *****************************************************************************************/
        public static CProducto GetProducto(int idProducto)
        {
            CProducto datProducto = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProducto",
                    DbType = DbType.Int32,
                    Value = idProducto
                });
                DataTable dt = SelectStoreProcedure("sp_getProducto", "PRODUCTO", lparam);

                if (dt != null && dt.Rows.Count > 0)
                {
                    datProducto = new CProducto()
                    {
                        Id = idProducto,
                        Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE"),
                        m_tipo = new CTipoProducto()
                        {
                            Id = GetValueColumn<int>(dt.Rows[0], "IDTIPO"),
                            Nombre = GetValueColumn<string>(dt.Rows[0], "TIPO")
                        },
                        ProductoSAC = new CProductoSAC()
                        {
                            Codigo = GetValueColumn<string>(dt.Rows[0], "CODIGO_SAC"),
                            Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_SAC"),
                            Alias = GetValueColumn<string>(dt.Rows[0], "ALIAS_SAC"),
                        },
                        CodSenasa = GetValueColumn<string>(dt.Rows[0], "NUMSENASA"),
                        PesoNetoPredefinido = GetValueColumn<float>(dt.Rows[0], "NETO_PREDEF"),
                        PesoTaraPredefinida = GetValueColumn<float>(dt.Rows[0], "TARA_PREDEF"),
                        UnidadesPredefinidas = GetValueColumn<int>(dt.Rows[0], "UNIDADES_PREDEF"),
                        RendimientoSTD = GetValueColumn<float>(dt.Rows[0], "REND"),
                        DiasVencimientoPredefinido = GetValueColumn<int>(dt.Rows[0], "DIAS_VENCIMIENTO"),
                        EsInsumo = GetValueColumn<bool>(dt.Rows[0], "ESINSUMO"),
                        EsPesable = GetValueColumn<bool>(dt.Rows[0], "ESPESABLE"),
                        EsCombo = GetValueColumn<bool>(dt.Rows[0], "ESCOMBO"),
                        EsCaja = GetValueColumn<bool>(dt.Rows[0], "ESCAJA"),
                        EsTropa = GetValueColumn<bool>(dt.Rows[0], "ESTROPA"),
                        NombreEtiL1 = GetValueColumn<string>(dt.Rows[0], "NOMBRE_L1"),
                        NombreEtiL2 = GetValueColumn<string>(dt.Rows[0], "NOMBRE_L2"),
                        NombreEtiL3 = GetValueColumn<string>(dt.Rows[0], "NOMBRE_L3"),
                        NombreEtiL4 = GetValueColumn<string>(dt.Rows[0], "NOMBRE_L4"),
                        NombreEtiL5 = GetValueColumn<string>(dt.Rows[0], "NOMBRE_L5"),
                        NombreEtiL6 = GetValueColumn<string>(dt.Rows[0], "NOMBRE_L6"),
                        TextAuxEtiL1 = GetValueColumn<string>(dt.Rows[0], "TEXTAUX_L1"),
                        TextAuxEtiL2 = GetValueColumn<string>(dt.Rows[0], "TEXTAUX_L2"),
                        Etiqueta = new CEtiqueta()
                        {
                            Id= GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                            Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                            Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                            IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                        }
                    };
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datProducto;
        }
        /***************************************************************************************
         * Metodo:	    GetProductos
         *              Obtiene una lista de todos los productos en la tabla Productos
         * Parametro:   string filtroAproxNombre 
         * Retorna:     (CProducto) instancia de la clase que contiene los datos del Producto.
        *****************************************************************************************/
        public static List<CProducto> GetProductos(string filtroAproxNombre = "")
        {
            List<CProducto> listProductos = new List<CProducto>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@prodFilter",
                    DbType = DbType.String,
                    Size = 100,
                    Value = filtroAproxNombre
                });
                DataTable dt = SelectStoreProcedure("sp_getProductos", "PRODUCTOS", lparam);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CProducto datProducto = new CProducto()
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
                                Alias = GetValueColumn<string>(dr, "ALIAS_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsTropa = GetValueColumn<bool>(dr, "ESTROPA"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            EsCaja = GetValueColumn<bool>(dr, "ESCAJA"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Etiqueta = new CEtiqueta()
                            {
                                Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                                Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                                Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                                IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                            }
                        };
                        listProductos.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listProductos;
        }
        /***************************************************************************************
         * Metodo:	    GetComposicionProductoCombo
         *              Obtiene una lista de productos que integra un producto combo
         * Parametro:   
         * Retorna:     List<CProducto> lista de productos.
        *****************************************************************************************/
        public static List<CItemProductoCombo> GetComposicionProductoCombo(int idProductoCombo)
        {
            CItemProductoCombo datProducto;
            List<CItemProductoCombo> listCombos = new List<CItemProductoCombo>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProductoCombo",
                    DbType = DbType.Int32,
                    Value = idProductoCombo
                });
                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PRE,TARA_PRE,UNDS_PRE,REND,VENC,INS,PES,ESINSUMO,ESPESABLE,NOMBRE_L1,
                //NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUXL1,TEXTAUXL2,UNIDADES_COMBO,PESO_COMBO,VALIDAR_UNDS,VALIDAR_PESO,TOLERANCIA_PESO

                DataTable dt = SelectStoreProcedure("sp_getDetalleProductosCombo", "PRODUCTOSCOMBO", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datProducto = new CItemProductoCombo()
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
                                Alias = GetValueColumn<string>(dr, "ALIAS_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsTropa = GetValueColumn<bool>(dr, "ESTROPA"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            ValidarUnidades = GetValueColumn<bool>(dr, "VALIDAR_UNDS"),
                            ToleranciaPeso = GetValueColumn<float>(dr, "TOLERANCIA_PESO"),
                            ValidarPeso = GetValueColumn<bool>(dr, "VALIDAR_PESO"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Etiqueta = new CEtiqueta()
                            {
                                Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                                Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                                Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                                IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                            },
                            UnidadesCombo = GetValueColumn<int>(dr, "UNIDADES_COMBO"),
                            PesoCombo = GetValueColumn<float>(dr, "PESO_COMBO")
                        };
                        listCombos.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listCombos;
        }

        /***************************************************************************************
         * Metodo:	    GetComposicionProductoCaja
         *              Obtiene el producto que integra un producto caja
         * Parametro:   
         * Retorna:     List<CProducto> lista de productos.
        *****************************************************************************************/
        public static List<CProducto> GetComposicionProductoCaja(int idProductoCaja)
        {
            CProducto datProducto;
            List<CProducto> listPrds = new List<CProducto>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@idProductoCaja",
                    DbType = DbType.Int32,
                    Value = idProductoCaja
                });
                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PRE,TARA_PRE,UNDS_PRE,REND,VENC,INS,PES,ESINSUMO,ESPESABLE,NOMBRE_L1,
                //NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUXL1,TEXTAUXL2

                DataTable dt = SelectStoreProcedure("sp_getDetalleProductosCaja", "PRODUCTOSCAJA", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datProducto = new CItemProductoCombo()
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
                                Alias = GetValueColumn<string>(dr, "ALIAS_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsTropa = GetValueColumn<bool>(dr, "ESTROPA"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Etiqueta = new CEtiqueta()
                            {
                                Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                                Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                                Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                                IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                            }
                        };
                        listPrds.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listPrds;
        }
        /***************************************************************************************
         * Metodo:	    GetProductosParaContenedores
         *              Obtiene una lista de todos los productos que pueden ser utilizados para 
         *              la creacion de un combo o caja
         * Parametro:   
         * Retorna:     List<CProducto> lista de productos.
        *****************************************************************************************/
        public static List<CProducto> GetProductosParaContenedores(string nombreFiltro = "")
        {
            CProducto datProducto;
            List<CProducto> listCombos = new List<CProducto>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@prodFilter",
                    DbType = DbType.String,
                    Value = nombreFiltro
                });

                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PREDEF,TARA_PREDEF,UNIDADES_PREDEF,REND,DIAS_VENCIMIENTO,INS,PES,ESINSUMO,ESPESABLE,ESCOMBO,
                //NOMBRE_L1,NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUX_L1,TEXTAUX_L2

                DataTable dt = SelectStoreProcedure("sp_getProductosParaContenedores", "PRODUCTOSCONTENEDORES", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datProducto = new CProducto()
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
                                Alias = GetValueColumn<string>(dr, "ALIAS_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsTropa = GetValueColumn<bool>(dr, "ESTROPA"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            EsCaja = GetValueColumn<bool>(dr, "ESCAJA"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Etiqueta = new CEtiqueta()
                            {
                                Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                                Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                                Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                                IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                            }
                        };
                        listCombos.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listCombos;
        }
        /***************************************************************************************
         * Metodo:	    GetProductosCombo
         *              Obtiene una lista de todos los productos de tipo combo 
         * Parametro:   
         * Retorna:     List<CProducto> lista de productos.
        *****************************************************************************************/
        public static List<CProducto> GetProductosCombo(string nombreFiltro = "")
        {
            CProducto datProducto;
            List<CProducto> listCombos = new List<CProducto>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@prodFilter",
                    DbType = DbType.String,
                    Value = nombreFiltro
                });

                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PREDEF,TARA_PREDEF,UNIDADES_PREDEF,REND,DIAS_VENCIMIENTO,INS,PES,
                //ESINSUMO,ESPESABLE,ESCOMBO,ESCAJA,NOMBRE_L1,
                //NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUX_L1,TEXTAUX_L2

                DataTable dt = SelectStoreProcedure("sp_getProductosCombo", "PRODUCTOSCOMBO", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datProducto = new CProducto()
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
                                Alias = GetValueColumn<string>(dr, "ALIAS_SAC"),
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsTropa = GetValueColumn<bool>(dr, "ESTROPA"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            EsCaja = GetValueColumn<bool>(dr, "ESCAJA"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Etiqueta = new CEtiqueta()
                            {
                                Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                                Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                                Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                                IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                            }
                        };
                        listCombos.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listCombos;
        }
        /***************************************************************************************
         * Metodo:	    GetProductosCaja
         *              Obtiene una lista de todos los productos de tipo combo 
         * Parametro:   
         * Retorna:     List<CProducto> lista de productos.
        *****************************************************************************************/
        public static List<CProducto> GetProductosCaja(string nombreFiltro = "")
        {
            CProducto datProducto;
            List<CProducto> listCajas = new List<CProducto>();
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@prodFilter",
                    DbType = DbType.String,
                    Value = nombreFiltro
                });

                //CODIGO_SAC,NOMBRE_SAC,ID,NOMBRE,IDTIPO,TIPO,NUMSENASA,NETO_PREDEF,TARA_PREDEF,UNIDADES_PREDEF,REND,DIAS_VENCIMIENTO,INS,PES,ESINSUMO,
                //ESPESABLE,ESCOMBO,ESCAJA,NOMBRE_L1,
                //NOMBRE_L2,NOMBRE_L3,NOMBRE_L4,NOMBRE_L5,NOMBRE_L6,TEXTAUX_L1,TEXTAUX_L2

                DataTable dt = SelectStoreProcedure("sp_getProductosCaja", "PRODUCTOSCAJA", lparam);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        datProducto = new CProducto()
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
                                Alias = GetValueColumn<string>(dr, "ALIAS_SAC")
                            },
                            CodSenasa = GetValueColumn<string>(dr, "NUMSENASA"),
                            PesoNetoPredefinido = GetValueColumn<float>(dr, "NETO_PREDEF"),
                            PesoTaraPredefinida = GetValueColumn<float>(dr, "TARA_PREDEF"),
                            UnidadesPredefinidas = GetValueColumn<int>(dr, "UNIDADES_PREDEF"),
                            RendimientoSTD = GetValueColumn<float>(dr, "REND"),
                            DiasVencimientoPredefinido = GetValueColumn<int>(dr, "DIAS_VENCIMIENTO"),
                            EsInsumo = GetValueColumn<bool>(dr, "ESINSUMO"),
                            EsPesable = GetValueColumn<bool>(dr, "ESPESABLE"),
                            EsTropa = GetValueColumn<bool>(dr, "ESTROPA"),
                            EsCombo = GetValueColumn<bool>(dr, "ESCOMBO"),
                            EsCaja = GetValueColumn<bool>(dr, "ESCAJA"),
                            NombreEtiL1 = GetValueColumn<string>(dr, "NOMBRE_L1"),
                            NombreEtiL2 = GetValueColumn<string>(dr, "NOMBRE_L2"),
                            NombreEtiL3 = GetValueColumn<string>(dr, "NOMBRE_L3"),
                            NombreEtiL4 = GetValueColumn<string>(dr, "NOMBRE_L4"),
                            NombreEtiL5 = GetValueColumn<string>(dr, "NOMBRE_L5"),
                            NombreEtiL6 = GetValueColumn<string>(dr, "NOMBRE_L6"),
                            TextAuxEtiL1 = GetValueColumn<string>(dr, "TEXTAUX_L1"),
                            TextAuxEtiL2 = GetValueColumn<string>(dr, "TEXTAUX_L2"),
                            Etiqueta = new CEtiqueta()
                            {
                                Id = GetValueColumn<int>(dt.Rows[0], "IDETIQUETA"),
                                Nombre = GetValueColumn<string>(dt.Rows[0], "NOMBRE_ETIQUETA"),
                                Descripcion = GetValueColumn<string>(dt.Rows[0], "DESCRIPCION_ETIQUETA"),
                                IdTipoBulto = GetValueColumn<string>(dt.Rows[0], "IDTIPOBULTO_ETIQUETA"),
                            }
                        };
                        listCajas.Add(datProducto);
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return listCajas;
        }

        /***************************************************************************************
         * Metodo:	    GetProductosSAC
         *              Obtiene los productos del sistema administrativo contable SAC con 
         *              filtro por nombre por aproximacion.
         * Parametro:   string filtroNombre
         * Retorna:     (DataTable) tabla con los productos.
        *****************************************************************************************/
        public static DataTable GetProductosSAC(string filtroNombre)
        {
            DataTable dtProductos = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@nombreFiltro",
                    OleDbType = OleDbType.VarChar,
                    Size = 100,
                    Value = filtroNombre
                });
                dtProductos = SelectStoreProcedure("sp_getProductosSAC", "PRODUCTOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtProductos;
        }
        /***************************************************************************************
         * Metodo:	    GetProveedoresSAC
         *              Obtiene los proveedores del sistema administrativo contable SAC con 
         *              filtro por nombre por aproximacion.
         * Parametro:   string filtroNombre
         * Retorna:     (DataTable) tabla con los proveedores.
        *****************************************************************************************/
        public static DataTable GetProveedoresSAC(string filtroNombre)
        {
            DataTable dtProductos = null;
            try
            {
                List<OleDbParameter> lparam = new List<OleDbParameter>();
                lparam.Add(new OleDbParameter()
                {
                    ParameterName = "@nombreFiltro",
                    OleDbType = OleDbType.VarChar,
                    Size = 100,
                    Value = filtroNombre
                });
                dtProductos = SelectStoreProcedure("sp_getProveedoresSAC", "PRODUCTOS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dtProductos;
        }

        /***************************************************************************************
         * Metodo:	    GetIdTipoContenedor
         *              Obtiene el tipo de contenedor desde su id
         *              si el id no corresponde a un contenedor retorna ""
         * Parametro:   int idContenedor
         * Retorna:     string idTipoContenedor.
        *****************************************************************************************/
        public static string GetIdTipoContenedor(int idContenedor)
        {
            OleDbDataReader recordSet;
            string idTIPO = "";

            string strCmd = String.Format(
                " SELECT idTipo as IDTIPO FROM CONTENEDORES WHERE id = {0} ", idContenedor);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();

                    idTIPO = GetCampoDbString(recordSet, "IDTIPO");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return idTIPO;
        }
        /***************************************************************************************
         * Metodo:	    GetEtiquetas
         *              Obtiene la lista de etiquetas desde la base de datos
         * Retorna:     List<CEtiqueta>.
        *****************************************************************************************/
        public static List<CEtiqueta> GetEtiquetas()
        {
            List<CEtiqueta> list = new List<CEtiqueta>();

            string strCmd = " SELECT id as ID,nombre as NOMBRE ,descripcion as DESCRIPCION,idtipobulto as IDTIPOBULTO FROM ETIQUETAS ORDER BY nombre";
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                using (var recordSet = dbCommand.ExecuteReader())
                {
                    if (recordSet.HasRows)
                    {
                        while (recordSet.Read())
                        {
                            list.Add(new CEtiqueta()
                            {
                                Id = GetCampoDbInt(recordSet, "ID"),
                                Nombre = GetCampoDbString(recordSet, "NOMBRE"),
                                Descripcion = GetCampoDbString(recordSet, "DESCRIPCION"),
                                IdTipoBulto = GetCampoDbString(recordSet, "IDTIPOBULTO")
                            });
                        }
                    }
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return list;
        }
        /***************************************************************************************
         * Metodo:	    GetContenedor
         *              Obtiene un contenedor segun su id. Puede ser tipo Caja o Combo.
         * Parametro:   int idContenedor
         * Retorna:     (CContenedor) instancia de la clase que contiene los datos del Contenedor.
        *****************************************************************************************/
        public static CContenedor GetContenedor(int idContenedor)
        {
            CContenedor datContenedor = null;
            string tipo = "";
            tipo = GetIdTipoContenedor(idContenedor);
            if (tipo == "CAJ")
                datContenedor = GetCaja(idContenedor);
            else if (tipo == "CMB")
                datContenedor = GetCombo(idContenedor);
            return datContenedor;
        }

        /***************************************************************************************
         * Metodo:	    GetAllTextLinesProductos
         *              Obtiene una lista de todos los textos de las 5 lineas que definen a los
         *              productos y sus dos lineas auxiliares.
         *              Filtra valores repetidos, null y vacios.
         * Parametro:   
         * Retorna:     List<string> lista de valores string obtenidos
        *****************************************************************************************/
        public static List<string> GetAllTextLinesProductos()
        {
            List<string> liststr = new List<string>();
            OleDbDataReader recordSet;

            string strCmd =
                " select textline from ( " +
                " select distinct nombrel1 as textline from Productos where nombrel1 is not null and nombreL1 <> '' " +
                " union " +
                " select distinct nombrel2 as textline from Productos where nombrel2 is not null and nombreL2 <> '' " +
                " union " +
                " select distinct nombrel3 as textline from Productos where nombrel3 is not null and nombreL3 <> '' " +
                " union " +
                " select distinct nombrel4 as textline from Productos where nombrel4 is not null and nombreL4 <> '' " +
                " union " +
                " select distinct nombrel5 as textline from Productos where nombrel5 is not null and nombreL5 <> '' " +
                " union " +
                " select distinct nombrel6 as textline from Productos where nombrel6 is not null and nombreL6 <> '' " +
                " union " +
                " select distinct textAuxl1 as textline from Productos where textAuxl1 is not null and textAuxL1 <> '' " +
                " union " +
                " select distinct textAuxl2 as textline from Productos where textAuxl2 is not null and textAuxL2 <> '') as lines ";
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    while (recordSet.Read())
                    {
                        liststr.Add(GetCampoDbString(recordSet, "textline"));
                    }
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return liststr;
        }

        /***************************************************************************************
         * Metodo:	    GetListStringColumn
         *              Obtiene una lista de strings que se encuentran en una columna de una tabla.
         *              Filtra valores repetidos, null y vacios.
         * Parametro:   (string) nombreTabla (string) nombreColumna
         * Retorna:     List<string> lista de valores string obtenidos
        *****************************************************************************************/
        public static List<string> GetListStringColumn(string nombreTabla, string nombreColumna)
        {
            List<string> liststr = new List<string>();
            OleDbDataReader recordSet;

            string strCmd = String.Format(" SELECT distinct {0} FROM {1} WHERE {0} is not null and {0} <> '' ", nombreColumna, nombreTabla);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    while (recordSet.Read())
                    {
                        liststr.Add(GetCampoDbString(recordSet, nombreColumna));
                    }
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return liststr;
        }

        /***************************************************************************************
        Metodo:		GetDatSet_PiezasHijas
                    Crea un dataset con las piezas hijas de una pieza padre.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasHijas(int idPiezaPadre, out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //PIEZA,NETO,UNDS
                    sqlQuery = String.Format(
                    " SELECT id as PIEZA,PesoNeto as NETO,unidades as UNDS,PesoRemitido as PESO_REMITIDO FROM pesadas WHERE idPiezaPadre={0} ORDER BY id desc  ", idPiezaPadre);
                    System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(sqlQuery, m_oleDbConnection);
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
         * Metodo:	    ExisteCodigoProductoSAC
         *              Indica si un codigo de producto del SAC ya existe en la tabla productos
         * Parametro:   (string) Codigo
         * Retorna:     (bool) true si existe
        *****************************************************************************************/
        public static bool ExisteCodigoProductoSAC(string codigo)
        {
            OleDbDataReader recordSet;
            bool existe = false;
            string strCmd = String.Format(" SELECT * FROM PRODUCTOS WHERE  UPPER(CodigoProductoSAC) = {0} ", codigo.ToUpper());
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

        /***************************************************************************************
         * Metodo:	    GetLastIdPesada
         *              Obtiene el ultimo numero de pesada generado por esta estacion .
         * Retorna:     (int) idpesada.
        *****************************************************************************************/
        public static int GetLastIdPesada()
        {
            OleDbDataReader recordSet;
            int maxId = 0;
            string strCmd = String.Format(" SELECT max(id) as ID FROM PESADAS WHERE idestacion = {0} ", m_OperadorActivo.m_idEstacion);
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
        /***************************************************************************************
         * Metodo:	    GetCodigoPedido
         *              Obtiene el codigo de pedido a partir de su id.
         * Retorna:     (int) codigo.
        *****************************************************************************************/
        public static int GetCodigoPedido(int idpedido)
        {
            OleDbDataReader recordSet;
            int codigo = 0;
            string strCmd = String.Format(" SELECT codigo FROM Pedidos WHERE id = {0} ", idpedido);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    codigo = GetCampoDbInt(recordSet, "codigo");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return codigo;
        }

        /**************************************************************************************************
         * Metodo:		BorrarPieza
         * Descripcion: Elimina una pesada indicando su ID
         * Parametro:	int id.
         * Retorna:     Retorna tru si la borro.
        ***************************************************************************************************/
        public static bool BorrarPieza(int id)
        {
            bool borradoOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE PESADAS WHERE id = {0}", id);
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
         * Metodo:	    IsValidPartDelete
         *              Verifica si una pieza es valida para ser eliminada de su registracion en Pesadas
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidPartDelete(int idPieza, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoBorrarPieza", m_oleDbConnection);

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


        /**************************************************************************************************
         * Metodo:		RegistrarDevolucionPieza
         * Descripcion: Llama a un SP que realiza la tarea de registrar una pieza como devolucion.
         *              
         * Parametro:	int idPieza.
         * Retorna:     Retorna tru si la llamada al SP se pudo realizar sin errores .
        ***************************************************************************************************/
        public static bool RegistrarDevolucionPieza(int idPieza)
        {

            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_registrarDevolucionPieza", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
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
                    callSPok = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);

                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }

        /**************************************************************************************************
         * Metodo:		RegistrarEventoLog
         * Descripcion: Llama a un SP que realiza la registracion de un evento en la tabla de dbLog de 
         *              la base de datos.
         *              
         * Retorna:     Retorna tru si la llamada al SP se pudo realizar sin errores .
        ***************************************************************************************************/
        public static bool RegistrarEventoLog(TYPE_EVENT_DBLOG tipoEvento, TYPE_CONTEXT_DBLOG tipoContexto, string detalle)
        {

            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_insertDbLog", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        DbType = DbType.Int32,
                        Value = m_OperadorActivo.m_id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        DbType = DbType.Int32,
                        Value = m_OperadorActivo.m_idEstacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@evento",
                        DbType = DbType.String,
                        Size = 100,
                        Value = Extensions.Extensions.ToStringValue(tipoEvento)
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@contexto",
                        DbType = DbType.String,
                        Size = 100,
                        Value = Extensions.Extensions.ToStringValue(tipoContexto)
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@detalle",
                        DbType = DbType.String,
                        Size = 100,
                        Value = detalle
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    callSPok = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);

                }
            }
            catch (System.Data.OleDb.OleDbException)
            {
            }
            return callSPok;
        }
        /**************************************************************************************************
         * Metodo:		RegistrarDevolucionContenedor
         * Descripcion: Llama a un SP que realiza la tarea de registrar un contenedor como devolucion.
         *              
         * Parametro:	int idContenedor.
         * Retorna:     Retorna tru si la llamada al SP se pudo realizar sin errores .
        ***************************************************************************************************/
        public static bool RegistrarDevolucionContenedor(int idContenedor)
        {

            bool callSPok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_registrarDevolucionContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idContenedor",
                        DbType = DbType.Int32,
                        Value = idContenedor
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
                    callSPok = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);

                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new CDbException("Error en Base de Datos: " + ex.Source + "--" + ex.Message);
            }
            return callSPok;
        }

        /***************************************************************************************
         * Metodo:	    ExisteProductoEnPesadas
         *              Indica si el producto indicado ya fue utilizado en alguna pesada
         * Parametro:   (int) idProducto
         * Retorna:     (bool) true si existe
        *****************************************************************************************/
        public static bool ExisteProductoEnPesadas(int idProducto)
        {
            OleDbDataReader recordSet;
            bool existe = false;
            string strCmd = String.Format(" SELECT idproducto FROM PESADAS WHERE idproducto = {0} ", idProducto);
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

        /***************************************************************************************
         * Metodo:	    EsInsumoVinculadoAAlgunProducto
         *              Indica si un producto tipo insumo se encuentra vinculado a algun producto
         * Parametro:   (int) idProductoInsumo
         * Retorna:     (bool) true si existe como vinculado
        *****************************************************************************************/
        public static bool EsInsumoVinculadoAAlgunProducto(int idProductoInsumo)
        {
            OleDbDataReader recordSet;
            bool existe = false;
            string strCmd = String.Format(" SELECT idproducto FROM productoinsumos WHERE idInsumoPrimario = {0} or idInsumoSecundario={0}  ", idProductoInsumo);
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


        /***************************************************************************************
         * Metodo:	    GetParametroDiasProximidadVencimiento
         *              Obtener el valor del parametro Dias de Proximidad de Vencimiento
         * Parametro:   
         * Retorna:     int con el valor de dias
        *****************************************************************************************/
        public static int GetParametroDiasProximidadVencimiento()
        {
            int dias = 0;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_getDiasProximidadVencimiento", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@dias",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.ReturnValue
                    });
                    dbCommand.ExecuteNonQuery();
                    dias = Convert.ToInt32(dbCommand.Parameters["@dias"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dias;
        }
        /***************************************************************************************
         * Metodo:	    SetParametroDiasProximidadVencimiento
         *              Establece el valor del parametro Dias de Proximidad de Vencimiento
         * Parametro:   int dias
         * Retorna:     bool true si ok
        *****************************************************************************************/
        public static bool SetParametroDiasProximidadVencimiento(int dias)
        {
            bool isok = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_setDiasProximidadVencimiento", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@dias",
                        DbType = DbType.Int32,
                        Value = dias
                    });
                    isok = dbCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return isok;
        }
        /***************************************************************************************
        Metodo:		GetConsultaMovimientosTransladosEntreDepositos
                    Crea un dataset con toda la informacion de los movimientos LOGS de translados de stock 
        Parametros:	out DataSet (infoMovs)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaMovimientosTransladosEntreDepositos(out DataSet dsInfoMovs)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoMovs = new DataSet();
            string sqlQuery;

            DataColumn[] columnsFechaKeys = null;
            DataColumn[] columnsMovimientosKeys = null;
            DataRelation datRelacionMovimientos_Fecha;

            try
            {
                int rowsFechasFills = 0;
                int rowsMovimientosFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    sqlQuery = String.Format(" SELECT DISTINCT cast(cast(fecha_hora as date) as datetime) AS FECHA" +
                               " FROM DBLOG " +
                               " WHERE contexto = '{0}'" +
                               " ORDER BY FECHA desc", Extensions.Extensions.ToStringValue(TYPE_CONTEXT_DBLOG.TransferenciasDeposito));

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsFechasFills = oleDbDataAdapter.Fill(dsInfoMovs, "FECHAS");

                    if (rowsFechasFills != 0)
                    {
                        sqlQuery = String.Format(" SELECT cast(cast(lg.fecha_hora as date) as datetime) AS FECHA," +
                                    " lg.fecha_hora as FECHA_HORA," +
                                    " o.nombre as OPERADOR," +
                                    " lg.idEstacion as ESTACION," +
                                    " lg.detalle as DETALLE " +
                                    " FROM dbLog lg " +
                                    " JOIN operadores o on o.id = lg.idoperador" +
                                    " WHERE lg.contexto = '{0}'" +
                                    " ORDER BY lg.fecha_hora desc", Extensions.Extensions.ToStringValue(TYPE_CONTEXT_DBLOG.TransferenciasDeposito));

                        //ejecuto el dataadapter con el nuevo query
                        oleDbDataAdapter.SelectCommand.CommandText = sqlQuery;
                        //cargo el resultado del query en el DataSet como tabla "MOVIMIENTOS"
                        rowsMovimientosFills = oleDbDataAdapter.Fill(dsInfoMovs, "MOVIMIENTOS");

                        columnsFechaKeys = new DataColumn[] { dsInfoMovs.Tables["FECHAS"].Columns["FECHA"] };

                        columnsMovimientosKeys = new DataColumn[] { dsInfoMovs.Tables["MOVIMIENTOS"].Columns["FECHA"] };
                        //creo el objeto relacion 
                        datRelacionMovimientos_Fecha = new DataRelation("FECHAS_MOVIMIENTOS", columnsFechaKeys, columnsMovimientosKeys, false);
                        //sumo el objeto relacion al dataset    
                        dsInfoMovs.Relations.Add(datRelacionMovimientos_Fecha);

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
         * Metodo:	    ActualizarDepositoDestinoDePieza
         *              Actualiza la hubicacion de una pieza 
         * Parametro:   int idpieza.
         *              int newIdDestino.
         * 
         * Retorna:     true if ok.
        *****************************************************************************************/
        public static bool ActualizarDepositoDestinoDePieza(int idPieza, int newIdDestino)
        {
            bool registracionOk = false;

            int regAfectados;

            string strCmd = String.Format(" UPDATE PESADAS set IDDESTINO = {1} WHERE id = {0}", idPieza, newIdDestino);
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
        /***************************************************************************************
         * Metodo:	    ActualizarDepositoDestinoDeContenedor
         *              Actualiza la hubicacion de un contenedor 
         * Parametro:   int idContenedor.
         *              int newIdDestino.
         * 
         * Retorna:     true if ok.
        *****************************************************************************************/
        public static bool ActualizarDepositoDestinoDeContenedor(int idContenedor, int newIdDestino)
        {
            bool registracionOk = false;

            int regAfectados;

            string strCmd = String.Format(" UPDATE CONTENEDORES set IDDESTINO = {1} WHERE id = {0}", idContenedor, newIdDestino);
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

        /***************************************************************************************
         * Metodo:	    FindFieldTable
         *              Busca un valor en una columna de una tabla , si escuentra coincidencia
         *              retorna true.
         * Parametro:   (string) nombreTabla, (string) nombreCampo , (string) valor
         * Retorna:     (bool) true si existe
        *****************************************************************************************/
        public static bool FindFieldTable(string nombreTabla, string nombreCampo, string valor)
        {
            OleDbDataReader recordSet;
            bool existe = false;
            string strCmd = String.Format(" SELECT {0} FROM {1} WHERE {0} = {2} ", nombreCampo, nombreTabla, valor);
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
        /***************************************************************************************
         * Metodo:	    GetMaxValueFieldTable
         *              Busca en una columna de tipo int de una tabla el maximo valor que posee.
         * Parametro:   (string) nombreTabla, (string) nombreCampo
         * Retorna:     (int) maximo valor obtenido.
        *****************************************************************************************/
        public static int GetMaxValueFieldTable(string nombreTabla, string nombreCampo)
        {
            OleDbDataReader recordSet;
            int maxValue = 0;
            string strCmd = String.Format(" SELECT MAX({0})AS MAXVALUE FROM {1} ", nombreCampo, nombreTabla);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    maxValue = GetCampoDbInt(recordSet, "MAXVALUE");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return maxValue;
        }

        /// <summary>
        /// Verifica si una pieza con su numero de pesada se encuntra en stock , es decir no fue procesada o egresada.
        /// </summary>
        /// <param name="idPesada"></param>
        /// <returns>
        /// True si la pieza se encuentra en stock.
        /// </returns>
        public static bool IPartInStock(int idPesada)
        {
            bool allInStock = false;
            string strCmd = String.Format(" " +
                "select count(*) as registros from pesadas where pesadas.id = {0} and (pesadas.id in (select idpesaje from dlp) or pesadas.id in (select idpesaje from egresos where idtipobulto='PZA'))", idPesada);
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

        /// <summary>
        /// Verifica si un pieza ya fue egresada buscandola en la tabla egresos
        /// </summary>
        /// <param name="idPesada"></param>
        /// <returns></returns>
        public static bool IsPartInEgresos(int idPesada)
        {
            bool partOut = false;
            string strCmd = String.Format(" " +
                "select idpesaje from egresos where idpesaje = {0} and idtipobulto='PZA'", idPesada);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                OleDbDataReader recordSet = dbCommand.ExecuteReader();
                partOut = (recordSet.HasRows);
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return partOut;
        }
        /// <summary>
        /// Verifica si un pieza es valida para realizar una devolucion , es decir que ya este egresada tanto individualmente 
        /// como parte de un contenedor y que el pedido del egreso se encuentre cerrado.
        /// </summary>
        /// <param name="idPesada"></param>
        /// <returns></returns>
        public static bool IsValidPartForReturn(int idPesada)
        {
            bool esValida = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidaPiezaParaDevolucion", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPesada
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    esValida = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return esValida;
        }
        /// <summary>
        /// Verifica si un pieza es Padre , es decir que no fue creada por un fraccionamiento
        /// </summary>
        /// <param name="idPieza"></param>
        /// <returns></returns>
        public static bool IsPartFather(int idPieza)
        {
            bool partFather = false;
            string strCmd = String.Format(" " +
                "select id from pesadas where id = {0} and (idpiezapadre is null or idpiezapadre = 0) ", idPieza);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                OleDbDataReader recordSet = dbCommand.ExecuteReader();
                partFather = (recordSet.HasRows);
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return partFather;
        }
        /***************************************************************************************
         * GetWeightingFromSelectedDGVPesadas
         * Obtiene una lista de clases CPesada construidas desde una seleccion de pesada en una
         * grilla de tipo DataGridView.
         * retorna (List<CPesada>)
        ***************************************************************************************/
        public static List<CPesada> GetWeightingFromSelectedDGVPesadas(DataGridView dgv, string nameColummIdPesada)
        {
            List<CPesada> listPesadas = new List<CPesada>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idPesada;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        idPesada = Convert.ToInt32(dgvr.Cells[nameColummIdPesada].Value);
                        listPesadas.Add(CDb.GetPesada(idPesada));
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listPesadas;
        }
        /***************************************************************************************
         * GetWeightingFromSelectedDGVPesadasTipoPiezas
         * Obtiene una lista de clases CPesada construidas desde una seleccion de pesada en una
         * grilla de tipo DataGridView.
         * retorna (List<CPesada>)
        ***************************************************************************************/
        public static List<CPesada> GetWeightingFromSelectedDGVPesadasTipoPiezas(DataGridView dgv, string nameColummIdPesada, string nameColumnType = "TIPO", string valueColumnType = "PIEZA")
        {
            List<CPesada> listPesadas = new List<CPesada>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idPesada;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        if (dgvr.Cells[nameColumnType].Value.ToString() == valueColumnType)
                        {
                            idPesada = Convert.ToInt32(dgvr.Cells[nameColummIdPesada].Value);
                            listPesadas.Add(CDb.GetPesada(idPesada));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listPesadas;
        }
        /***************************************************************************************
         * GetWeightingFromSelectedDGVPesadasTipoContenedores
         * Obtiene una lista de clases CContenedor construidas desde una seleccion de pesada en una
         * grilla de tipo DataGridView.
         * retorna (List<CContenedor>)
        ***************************************************************************************/
        public static List<CContenedor> GetWeightingFromSelectedDGVPesadasTipoContenedores(DataGridView dgv, string nameColummIdPesada, string nameColumnType = "TIPO", string valueColumnType = "CAJA")
        {
            List<CContenedor> listContenedor = new List<CContenedor>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idPesada;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        if (dgvr.Cells[nameColumnType].Value.ToString() == valueColumnType)
                        {
                            idPesada = Convert.ToInt32(dgvr.Cells[nameColummIdPesada].Value);
                            listContenedor.Add(CDb.GetContenedor(idPesada));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listContenedor;
        }
        /**************************************************************************************************
         * Metodo:		BorrarRegistroTabla
         * Descripcion: Elimina un registro en una tabla de base de datos indicando el nombre de la
         *              tabla, el nombre de la columna y la clave de coincidencia
         * Parametro:	string id.
         * Retorna:     Retorna tru si la borro.
        ***************************************************************************************************/
        public static bool BorrarRegistroTabla(string nombreTabla, string nombreColumna, string id)
        {
            bool borradoOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE {0} WHERE {1} = {2}", nombreTabla, nombreColumna, id);
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

        #region METODOS DE USO DE CONTROLES CON BASE DE DATOS 
        /***************************************************************************************
        Metodo:		FillComboBox
                    Carga un ComboBox con el resultado del query de base de datos ingresado
                    como parametro, el query debe retornar un campo con nombre 'id' y otro 'descripcion'.
                    En el ComboBox cada item guardado es una instancia de clase CItemCBoxTable.
        Parametros:	(ComboBox) componente combobox en donde se cargara el resultado del query
        Parametros:	(string)   query de Base de Datos
        Retorna:    True si cargado ok. 
        *****************************************************************************************/
        public static bool FillComboBox(ComboBox comboBox, string strQuery)
        {
            bool cargaOk = false;

            comboBox.Items.Clear();
            OleDbDataReader recordSet;

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strQuery, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                while (recordSet.Read())
                {
                    comboBox.Items.Add(new CItemCBoxTable(GetCampoDbInt(recordSet, "id"),
                                            GetCampoDbString(recordSet, "descripcion", " ")));
                }
                comboBox.SelectedIndex = -1;
                recordSet.Close();
                cargaOk = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return cargaOk;
        }
        /***************************************************************************************
        Metodo:		FillComboBoxIdText
                    Carga un ComboBox con el resultado del query de base de datos ingresado
                    como parametro, el query debe retornar un campo con nombre 'id' tipo texto 
                    y otro 'descripcion'.
                    En el ComboBox cada item guardado es una instancia de clase CItemIdTextCBoxTable.
        Parametros:	(ComboBox) componente combobox en donde se cargara el resultado del query
        Parametros:	(string)   query de Base de Datos
        Retorna:    True si cargado ok. 
        *****************************************************************************************/
        public static bool FillComboBoxIdText(ComboBox comboBox, string strQuery)
        {
            bool cargaOk = false;

            comboBox.Items.Clear();
            OleDbDataReader recordSet;

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strQuery, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                while (recordSet.Read())
                {
                    comboBox.Items.Add(new CItemIdTextCBoxTable(GetCampoDbString(recordSet, "id"),
                                            GetCampoDbString(recordSet, "descripcion", " ")));
                }
                comboBox.SelectedIndex = -1;
                recordSet.Close();
                cargaOk = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return cargaOk;
        }

        /***************************************************************************************
        Metodo:		FillComboBoxProductos
                    Carga un ComboBox con la tabla PRODUCTOS con estructura los datos :
                    (id,nombreL1,NombreL2,NombreL3,NombreL4) .
                    En el ComboBox cada item guardado es una instancia de clase CItemCBoxProducto.
        Parametros:	(ComboBox) componente combobox en donde se cargara el resultado del query
        Retorna:    True si cargado ok. 
        *****************************************************************************************/
        public static bool FillComboBoxProductos(ComboBox comboBox)
        {
            bool cargaOk = false;

            comboBox.Items.Clear();
            OleDbDataReader recordSet;

            try
            {
                string strCmd = " SELECT id,nombreL1,nombreL21,nombreL3,nombreL4 FROM PRODUCTOS order by nombreL1 ";
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                while (recordSet.Read())
                {
                    comboBox.Items.Add(new CItemCBoxProducto(GetCampoDbInt(recordSet, "id"),
                        GetCampoDbString(recordSet, "nombreL1", " "), GetCampoDbString(recordSet, "nombreL2", " "),
                        GetCampoDbString(recordSet, "nombreL3", " "), GetCampoDbString(recordSet, "nombreL4", " ")));
                }
                comboBox.SelectedIndex = -1;
                recordSet.Close();
                cargaOk = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return cargaOk;
        }

        /***************************************************************************************
        Metodo:		FillComboBox
                    Carga un ComboBox con el resultado del query de una tabla de base de datos
                    indicando el nombre de la tabla, el nombre del campo id y el nombre del
                    campo descripcion y el campo orden del query (ej: "nombre ASC").
                    En el ComboBox cada item guardado es una instancia de clase CItemCBoxTable.
        Parametros:	(ComboBox) componente combobox en donde se cargara el resultado del query
        Parametros:	(string)   nombre de tabla
        Parametros:	(string)   nombre de campo id de la tabla
        Parametros:	(string)   nombre de compo que contiene la descripcion
        Parametros:	(string)   nombre de compo orden del query (debe contener el nombre de la 
                               columna de ordenamiento y si es ascendente o descendente "nombre ASC" 
                               o "nombre DESC".
        Retorna:    True si cargado ok. 
        *****************************************************************************************/
        public static bool FillComboBox(ComboBox comboBox, string tabla, string campoid, string campoDescripcion, string campoOrden)
        {
            bool cargaOk = false;

            comboBox.Items.Clear();
            OleDbDataReader recordSet;

            try
            {
                string strCmd = String.Format(" SELECT {0} as id,{1} as descripcion FROM {2} order by {3}", campoid, campoDescripcion, tabla, campoOrden);
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                while (recordSet.Read())
                {
                    comboBox.Items.Add(new CItemCBoxTable(GetCampoDbInt(recordSet, "id"),
                                            GetCampoDbString(recordSet, "descripcion", " ")));
                }
                comboBox.SelectedIndex = -1;
                recordSet.Close();
                cargaOk = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return cargaOk;
        }


        /***************************************************************************************
        Metodo:		FillListBox
                    Carga un ListBox con el resultado del query de base de datos ingresado
                    como parametro, el query debe retornar un campo con nombre 'id' y otro 'descripcion'.
                    En el ComboBox cada item guardado es una instancia de clase CItemCBoxTable.
        Parametros:	(ListBox) componente combobox en donde se cargara el resultado del query
        Parametros:	(string)   query de Base de Datos
        Retorna:    True si cargado ok. 
        *****************************************************************************************/
        public static bool FillListBox(ListBox listBox, string strQuery)
        {
            bool cargaOk = false;

            listBox.Items.Clear();
            OleDbDataReader recordSet;

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strQuery, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                while (recordSet.Read())
                {
                    listBox.Items.Add(new CItemCBoxTable(GetCampoDbInt(recordSet, "id"),
                                            GetCampoDbString(recordSet, "descripcion", " ")));
                }
                listBox.SelectedIndex = -1;
                recordSet.Close();
                cargaOk = true;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return cargaOk;
        }


        /**************************************************************************
         * SelectItemComboDb
         * Selecciona un item de un combo box indicando su id (clave valor) 		 
         ***************************************************************************/
        public static bool SelectItemComboDb(ComboBox comboBox, int id)
        {
            bool seleccionado = false;
            comboBox.SelectedIndex = -1;
            try
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (((CItemCBoxTable)comboBox.Items[i]).Id == id)
                    {
                        comboBox.SelectedIndex = i;
                        seleccionado = true;
                        break;
                    }
                }
                if (seleccionado == false) comboBox.SelectedIndex = -1;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return seleccionado;
        }
        /**************************************************************************
         * SelectItemComboDb
         * Selecciona un item de un combo box indicando su id (clave valor) 		 
         ***************************************************************************/
        public static bool SelectItemComboDb(ComboBox comboBox, string id)
        {
            bool seleccionado = false;
            comboBox.SelectedIndex = -1;
            try
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (((CItemIdTextCBoxTable)comboBox.Items[i]).Id == id)
                    {
                        comboBox.SelectedIndex = i;
                        seleccionado = true;
                        break;
                    }
                }
                if (seleccionado == false) comboBox.SelectedIndex = -1;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return seleccionado;
        }

        /**************************************************************************
         * GetSelectItemIdComboDb
         * Obtiene el id (clave valor) de un Item Seleccionado en un ComboBox 		 
         * si el valor retornado es 0 , el combobox no tenia seleccion o esta vacio
         ***************************************************************************/
        public static int GetSelectItemIdComboDb(ComboBox comboBox)
        {
            int id = 0;
            if (comboBox.SelectedIndex != -1)
            {
                try
                {
                    id = ((CItemCBoxTable)comboBox.SelectedItem).Id;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
                }
            }
            return id;
        }
        /**************************************************************************
         * GetSelectItemIdTextComboDb
         * Obtiene el id (clave valor texto) de un Item Seleccionado en un ComboBox 		 
         * si el valor retornado es "" , el combobox no tenia seleccion o esta vacio
         ***************************************************************************/
        public static string GetSelectItemIdTextComboDb(ComboBox comboBox)
        {
            string id = "";
            if (comboBox.SelectedIndex != -1)
            {
                try
                {
                    id = ((CItemIdTextCBoxTable)comboBox.SelectedItem).Id;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
                }
            }
            return id;
        }
        #endregion

        #region METODOS SEGUROS DE LECTURA DE CAMPOS EN TABLAS DE BASE DE DATOS 
        /**************************************************************************************************
		Metodo:		GetCampoDbInt
					Obtener el valor integer de una columna de un recordser considerando que puede ser null
		Parametros:	(OleDbDataReader) recordset:
						Recordser desde donde se leera el valor 
					(string) nombreCampo:
						Nombre del campo a leer
					(int) valorDefault:
						Valor a tomar si es que el campo es null.
		Retorna:    Retorna el valor obtenido. 
		***************************************************************************************************/
        public static int GetCampoDbInt(OleDbDataReader recordset, string nombreCampo, int valDefault = 0)
        {
            int valor = valDefault;
            try
            {
                int idxColumna = recordset.GetOrdinal(nombreCampo);
                if (recordset[idxColumna] != DBNull.Value)
                    valor = recordset.GetInt32(idxColumna);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return valor;
        }
        /**************************************************************************************************
		Metodo:		GetCampoDbBool
					Obtener el valor bool de una columna tipo (bit) de un recordser considerando que puede ser null
		Parametros:	(OleDbDataReader) recordset:
						Recordser desde donde se leera el valor 
					(string) nombreCampo:
						Nombre del campo a leer
					(bool) valorDefault:
						Valor a tomar si es que el campo es null.
		Retorna:    Retorna el valor obtenido. 
		***************************************************************************************************/
        public static bool GetCampoDbBool(OleDbDataReader recordset, string nombreCampo, bool valDefault = false)
        {
            bool valor = valDefault;
            try
            {
                int idxColumna = recordset.GetOrdinal(nombreCampo);
                if (!recordset.IsDBNull(idxColumna))
                    valor = recordset.GetBoolean(idxColumna);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return valor;
        }
        /**************************************************************************************************
		Metodo:		GetCampoDbFloat
					Obtener el valor float de una columna de un recordser considerando que puede ser null
		Parametros:	(OleDbDataReader) recordset:
						Recordser desde donde se leera el valor 
					(string) nombreCampo:
						Nombre del campo a leer
					(float) valorDefault:
						Valor a tomar si es que el campo es null.
		Retorna:    Retorna el valor obtenido. 
		***************************************************************************************************/
        public static float GetCampoDbFloat(OleDbDataReader recordset, string nombreCampo, float valDefault = 0.0f)
        {
            double valor = valDefault;
            try
            {
                int idxColumna = recordset.GetOrdinal(nombreCampo);
                if (!recordset.IsDBNull(idxColumna))
                {
                    valor = recordset.GetDouble(idxColumna);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return (float)valor;
        }
        /**************************************************************************************************
		Metodo:		GetCampoDbString
					Obtener el valor string de una columna de un recordser considerando que puede ser null
		Parametros:	(OleDbDataReader) recordset:
						Recordser desde donde se leera el valor 
					(string) nombreCampo:
						Nombre del campo a leer
					(string) valorDefault:
						Valor a tomar si es que el campo es null.
		Retorna:    Retorna el valor obtenido. 
		***************************************************************************************************/
        public static string GetCampoDbString(OleDbDataReader recordset, string nombreCampo, string valDefault = "")
        {
            string valor = valDefault;
            try
            {
                int idxColumna = recordset.GetOrdinal(nombreCampo);
                if (!recordset.IsDBNull(idxColumna))
                    valor = recordset.GetString(idxColumna);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return valor;
        }
        /**************************************************************************************************
		Metodo:		GetCampoDbDateTime
					Obtener el valor DateTime de una columna de un recordser considerando que puede ser null
		Parametros:	(OleDbDataReader) recordset:
						Recordser desde donde se leera el valor 
					(string) nombreCampo:
						Nombre del campo a leer
					(string) valorDefault:
						Valor a tomar si es que el campo es null.
		Retorna:    Retorna el valor obtenido. 
		***************************************************************************************************/
        public static DateTime GetCampoDbDateTime(OleDbDataReader recordset, string nombreCampo)
        {
            DateTime valor = new DateTime();
            valor = DateTime.MinValue;
            try
            {
                int idxColumna = recordset.GetOrdinal(nombreCampo);
                if (!recordset.IsDBNull(idxColumna))
                    valor = recordset.GetDateTime(idxColumna);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return valor;
        }

        /// <summary>
        /// Obtiene de forma segura el valor bool de una celda de una fila de un DataGridView
        /// </summary>
        public static bool GetCellDGVBool(DataGridViewRow rowDGV, string nameColumn, bool _default = false)
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return Convert.ToBoolean(rowDGV.Cells[nameColumn].Value);
            else
                return _default;
        }
        /// <summary>
        /// Obtiene de forma segura el valor int de una celda de una fila de un DataGridView
        /// </summary>
        public static int GetCellDGVInt(DataGridViewRow rowDGV, string nameColumn, int _default = 0)
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return Convert.ToInt32(rowDGV.Cells[nameColumn].Value);
            else
                return _default;
        }
        /// <summary>
        /// Obtiene de forma segura el valor long de una celda de una fila de un DataGridView
        /// </summary>
        public static long GetCellDGVLong(DataGridViewRow rowDGV, string nameColumn, long _default = 0)
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return Convert.ToInt64(rowDGV.Cells[nameColumn].Value);
            else
                return _default;
        }
        /// <summary>
        /// Obtiene de forma segura el valor string de una celda de una fila de un DataGridView
        /// </summary>
        public static string GetCellDGVString(DataGridViewRow rowDGV, string nameColumn, string _default = "")
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return rowDGV.Cells[nameColumn].Value.ToString();
            else
                return _default;
        }
        /// <summary>
        /// Obtiene de forma segura el valor char de una celda de una fila de un DataGridView
        /// </summary>
        public static char GetCellDGVChar(DataGridViewRow rowDGV, string nameColumn, char _default = '?')
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return Convert.ToChar(rowDGV.Cells[nameColumn].Value);
            else
                return _default;
        }
        /// <summary>
        /// Obtiene de forma segura el valor datetime de una celda de una fila de un DataGridView
        /// </summary>
        public static DateTime GetCellDGVDateTime(DataGridViewRow rowDGV, string nameColumn)
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return Convert.ToDateTime(rowDGV.Cells[nameColumn].Value);
            else
                return DateTime.MinValue;
        }
        /// <summary>
        /// Obtiene de forma segura el valor float de una celda de una fila de un DataGridView
        /// </summary>
        public static float GetCellDGVFloat(DataGridViewRow rowDGV, string nameColumn, float _default = 0.0f)
        {
            if (rowDGV.Cells[nameColumn].Value != DBNull.Value)
                return Convert.ToSingle(rowDGV.Cells[nameColumn].Value);
            else
                return _default;
        }

        /**************************************************************************************************
            Metodo:		GetValueColumn
                        Obtener el valor de una columna de tipo T de un DataRow.
            Parametros:	(DataRow) record:
                        (string) nombreCampo:
                        (T) valorDefault:
                        Valor a tomar si es que el campo es null.
            Retorna:    Retorna el valor de tipo T obtenido. 
        ***************************************************************************************************/
        public static T GetValueColumn<T>(DataRow dataRow, string nombreCampo, T valDefault = default(T))
        {
            T valor = valDefault;
            try
            {
                if (dataRow[nombreCampo] != DBNull.Value)
                {
                    if (typeof(T) == typeof(bool))
                    {
                        valor = (T)Convert.ChangeType(dataRow[nombreCampo], typeof(bool));
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        valor = (T)Convert.ChangeType(dataRow[nombreCampo], typeof(float));
                    }
                    else
                    {
                        valor = dataRow.Field<T>(nombreCampo);
                    }
                }
            }
            catch (Exception exdb)
            {
                throw new CDbException("Error en Base de Datos: " + exdb.Source + "--" + exdb.Message + " Metodo:  " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return valor;
        }

        #endregion

        #region METODOS DE USO GENERAL

        /// <summary>
        /// Llama a un store procedure y retorna su resultado en un data table.
        /// Puede tener o no parametros. Si el data table es null indica que no
        /// retorno datos
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="datatableName"></param>
        /// <param name="listParameters"></param>
        /// <returns></returns>
        public static DataTable SelectStoreProcedure(string spName, string datatableName, List<OleDbParameter> listParameters = null)
        {
            /* listParameters no puede ser reutilizada ante sucesivos llamados a este metodo, debe ser una instancia de objeto distinto
             * dado que si es el mismo se genera una exepcion por mismos parametros.
             */
            DataTable datTable = null;
            OleDbDataAdapter oleDbDataAdapter;
            int registrosObtenidos = 0;

            try
            {
                datTable = new DataTable(datatableName);
                OleDbCommand command = new OleDbCommand();
                command.Connection = CDb.m_oleDbConnection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                command.CommandTimeout = 120;

                if (listParameters != null)
                {
                    command.Parameters.AddRange(listParameters.ToArray());
                }

                oleDbDataAdapter = new OleDbDataAdapter(command);
                registrosObtenidos = oleDbDataAdapter.Fill(datTable);
                if (registrosObtenidos == 0)
                    datTable = null;
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datTable;
        }

        #endregion

    }

}
