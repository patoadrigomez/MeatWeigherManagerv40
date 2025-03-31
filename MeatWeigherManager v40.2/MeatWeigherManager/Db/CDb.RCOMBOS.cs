using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Pesajes de Cajas
    /// </summary>
    public static partial class CDb
    {
        #region OPERACIONES DE REGISTRACION DE COMBOS
        /***************************************************************************************
         * Metodo:	    AgregarPiezaComboTemp
         *              Inserta un nuevo registro de vinculo de pieza para un combo que aun no
         *              fue creado . Se inserta en la tabla CONTENEDORPIEZAS, la columna idcontenedor=null y
         *              se carga el idpieza ,el idestacion y idTipoContenedor='CMB'.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool AgregarPiezaComboTemp(int idPieza)
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" INSERT INTO CONTENEDORPIEZAS(IDPESAJE,IDESTACION,IDTIPOCONTENEDOR)" +
                                          " VALUES({0},{1},'{2}')", idPieza, m_OperadorActivo.m_idEstacion,"CMB");

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
         * Metodo:	    GetCombo
         *              Obtiene todos los datos de un Combo indicando su ID
         * Parametro:   int idCombo
         * Retorna:     (CContenedor) instancia de la clase que contiene los datos del Combo.
        *****************************************************************************************/
        public static CContenedor GetCombo(int idCombo)
        {
            OleDbDataReader recordSet;
            CContenedor datCombo = null;
            string strCmd = String.Format(
                " SELECT c.fecha_hora as CREADO,c.pesoNeto as NETO , c.pesoTara as TARA,c.idestacion as IDESTACION,d.id as IDDESTINO,d.nombre as NOMBRE_DESTINO ,"+
                " c.idproducto as IDPRODUCTO ,c.idtipo as IDTIPO ,(select COUNT(*) from ContenedorPiezas where idcontenedor=c.id) as UNDS ,c.fecha_vencimiento as FECHA_VENCIMIENTO " +
                " FROM CONTENEDORES c ,Destinos d " +
                " WHERE c.id = {0} AND c.idtipo = 'CMB' AND c.iddestino=d.id ", idCombo);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();

                    datCombo = new CContenedor();
                    datCombo.Id = idCombo;
                    datCombo.m_fechaHoraCreacion = GetCampoDbDateTime(recordSet, "CREADO");
                    datCombo.m_fechaVencimiento = GetCampoDbDateTime(recordSet, "FECHA_VENCIMIENTO");
                    datCombo.PesoNeto = GetCampoDbFloat(recordSet, "NETO");
                    datCombo.PesoTara = GetCampoDbFloat(recordSet, "TARA");
                    datCombo.m_idEstacion = GetCampoDbInt(recordSet, "IDESTACION");
                    datCombo.Producto = GetProducto(GetCampoDbInt(recordSet, "IDPRODUCTO"));
                    datCombo.Destino = new CDestino()
                    {
                        Id = GetCampoDbInt(recordSet, "IDDESTINO"),
                        Nombre = GetCampoDbString(recordSet, "NOMBRE_DESTINO")
                    };
                    datCombo.IdTipo = GetCampoDbString(recordSet, "IDTIPO");
                    datCombo.m_undsContenidas = GetCampoDbInt(recordSet, "UNDS");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datCombo;
        }
        /***************************************************************************************
         * Metodo:	    GetComboDesdePieza
         *              Obtiene todos los datos de un Combo indicando una pieza contenida
         * Parametro:   int idPieza
         * Retorna:     (CCombo) instancia de la clase que contiene los datos de la Caja.
        *****************************************************************************************/
        public static CContenedor GetComboDesdePieza(int idPieza)
        {
            OleDbDataReader recordSet;
            CContenedor datCombo = null;
            string strCmd = String.Format(
                " SELECT TOP 1 c.id as IDCOMBO,c.fecha_hora as CREADO,c.pesoNeto as NETO , c.pesoTara as TARA,c.idestacion as IDESTACION,d.id as IDDESTINO,"+
                " d.nombre as NOMBRE_DESTINO ,c.idproducto as IDPRODUCTO ,c.idtipo as IDTIPO ,(select COUNT(*) from ContenedorPiezas where idcontenedor=c.id) as UNDS ,c.fecha_vencimiento as FECHA_VENCIMIENTO" +
                " FROM CONTENEDORES c , CONTENEDORPIEZAS cp,Pesadas p ,Destinos d " +
                " WHERE cp.idpesaje={0} AND  c.id = cp.idcontenedor AND c.idtipo = 'CMB' AND cp.idpesaje = p.id AND c.iddestino = d.id ", idPieza);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();

                    datCombo = new CContenedor();
                    datCombo.Id = GetCampoDbInt(recordSet, "IDCOMBO"); ;
                    datCombo.m_fechaHoraCreacion = GetCampoDbDateTime(recordSet, "CREADO");
                    datCombo.m_fechaVencimiento = GetCampoDbDateTime(recordSet, "FECHA_VENCIMIENTO");
                    datCombo.PesoNeto = GetCampoDbFloat(recordSet, "NETO");
                    datCombo.PesoTara = GetCampoDbFloat(recordSet, "TARA");
                    datCombo.m_idEstacion = GetCampoDbInt(recordSet, "IDESTACION");
                    datCombo.Producto = GetProducto(GetCampoDbInt(recordSet, "IDPRODUCTO"));
                    datCombo.Destino = new CDestino()
                    {
                        Id = GetCampoDbInt(recordSet, "IDDESTINO"),
                        Nombre = GetCampoDbString(recordSet, "NOMBRE_DESTINO")
                    };
                    datCombo.IdTipo = GetCampoDbString(recordSet, "IDTIPO");
                    datCombo.m_undsContenidas = GetCampoDbInt(recordSet, "UNDS");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datCombo;
        }

        /***************************************************************************************
         * Metodo:	    EliminarPiezaComboTemp
         *              Elimina un nuevo registro de vinculo de pieza para una combo que aun no
         *              fue creado .
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarPiezaComboTemp(int idPieza)
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE CONTENEDORPIEZAS WHERE IDPESAJE = {0} AND IDESTACION ={1} AND IDCONTENEDOR is null AND IDTIPOCONTENEDOR= 'CMB' ", idPieza, m_OperadorActivo.m_idEstacion);

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
         * Metodo:	    EliminarPiezasComboTemp
         *              Elimina todas las piezas que tenga colectadas un proceso de combo de forma
         *              temporal. Basado en la estacion actual en que se encuentra.
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarPiezasComboTemp()
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE CONTENEDORPIEZAS WHERE IDESTACION ={0} AND IDCONTENEDOR is null AND IDTIPOCONTENEDOR= 'CMB' ", m_OperadorActivo.m_idEstacion);

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
         * Metodo:	    EliminarPiezaCombo
         *              Elimina una pieza contenida en un combo indicando el id de la pieza.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarPiezaCombo(int idPieza)
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE CONTENEDORPIEZAS WHERE IDPESAJE = {0} AND IDCONTENEDOR is not null AND IDTIPOCONTENEDOR= 'CMB' ", idPieza);

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
         * Metodo:	    ActualizarPesoNetoComboPorEliminarPieza
         *              Actualiza el peso de un combo por eliminar de la misma una pieza.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool ActualizarPesoNetoComboPorEliminarPieza(int idPieza)
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" UPDATE c set c.PesoNeto = c.pesoNeto-p.pesoNeto , c.unidades = c.unidades - 1 FROM PESADAS p, CONTENEDORES c, CONTENEDORPIEZAS cp WHERE cp.idpesaje = {0} and p.id = cp.idpesaje and c.id = cp.idcontenedor and c.idtipo ='CMB' ", idPieza);

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
         * Metodo:	    DesarmarContenedor
         *              Marca a un contenedor como desarmado
         * Parametro:   int idCombo
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool DesarmarContenedor(int idContenedor)
        {
            bool desarmadoOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_desarmarContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idContenedor",
                        DbType = DbType.Int32,
                        Value = idContenedor
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    desarmadoOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return desarmadoOk;

        }

        /***************************************************************************************
         * Metodo:	    IsValidPartForIncludeToCombo
         *              Verifica si la pieza indicada es valida para ser incluida en un combo.
         * Parametro:   int idProductoCombo,int idPieza,string detailResult
         * Retorna:     true si la pieza el valida para incluirce en el combo.
         *              false si no es valida y el detalle de la no validacion es detailResult
        *****************************************************************************************/
        public static bool IsValidPartForIncludeToCombo(int idProductoCombo, int idPieza,out string detailResult)
        {
            bool isValid = false;
            detailResult = "";
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoAgregarPiezaACombo", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProductoCombo",
                        DbType = DbType.Int32,
                        Value = idProductoCombo
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
                    isValid = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "": dbCommand.Parameters["@error"].Value.ToString());
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return isValid;
        }
        /***************************************************************************************
         * Metodo:	    IsValidPartForDeleteToOpenContainer
         *              Verifica si la pieza indicada es valida para ser eliminada de un contenedor abierto
         *              (en proceso).
         * Parametro:   int idPieza,string detailResult
         * Retorna:     true si la pieza el valida para ser borrada del contenedor.
         *              false si no es valida y el detalle de la no validacion es detailResult
        *****************************************************************************************/
        public static bool IsValidPartForDeleteToOpenContainer(int idPieza, out string detailResult)
        {
            bool isValid = false;
            detailResult = "";
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoBorrarPiezaEnContenedorAbierto", m_oleDbConnection);

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
                    isValid = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                    detailResult = (dbCommand.Parameters["@error"].Value == DBNull.Value ? "" : dbCommand.Parameters["@error"].Value.ToString());
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return isValid;
        }

        /***************************************************************************************
         * Metodo:	    GetTotalNetoPartsContainTempCombo
         *              Obtiene el peso neto total de las piezas registradas a vincular para 
         *              un combo temporal.
         * Parametro:   
         * Retorna:     valor de peso neto total. Si es 0 es porque no hay registros de piezas a 
         *              vincular en el combo.
        *****************************************************************************************/
        public static float GetTotalNetoPartsContainTempCombo()
        {
            OleDbDataReader recordSet;
            float totalNetoContain = 0.0f;

            string strCmd = String.Format(" SELECT SUM(pe.pesoneto) as TOTAL_NETO FROM CONTENEDORPIEZAS cp,PESADAS pe " +
                                          " WHERE cp.idestacion = {0} AND cp.idcontenedor is null AND cp.idtipocontenedor = 'CMB' AND cp.idpesaje = pe.id ",m_OperadorActivo.m_idEstacion);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    totalNetoContain = GetCampoDbFloat(recordSet, "TOTAL_NETO");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return totalNetoContain;
        }
        /***************************************************************************************
         * Metodo:	    GetTotalUndsPartsContainTempCombo
         *              Obtiene la cantidad total de unidades contenidas en un combo temporal.
         * Parametro:   
         * Retorna:     cantidad de unidades. Si es 0 es porque no hay registros de piezas.
        *****************************************************************************************/
        public static int GetTotalUndsPartsContainTempCombo()
        {
            OleDbDataReader recordSet;
            int undsContenidas = 0;

            string strCmd = String.Format(" SELECT COUNT(*) as UNDS FROM CONTENEDORPIEZAS cp,PESADAS pe " +
                                          " WHERE cp.idestacion = {0} AND cp.idcontenedor is null AND cp.idtipocontenedor = 'CMB' AND cp.idpesaje = pe.id ", m_OperadorActivo.m_idEstacion);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    undsContenidas = GetCampoDbInt(recordSet, "UNDS");
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return undsContenidas;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE REGISTRACIONES DE COMBOS

        /***************************************************************************************
        Metodo:		GetDatSet_PiezasContenidasComboAbierto
                    Crea un dataset con las piezas contenidas en un combo temporal.
                    Informacion de los productos que componen el combo en proceso y las cantidades
                    colectadas en lada uno de ellos
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasContenidasComboAbierto(int idProductoCombo,out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //IDPRODUCTO,PRODUCTO,UNDS_CMB,PESO_CMB,UND_COL,PESO_COL
                    sqlQuery = String.Format(
                    " SELECT prd.id as IDPRODUCTO,prd.nombre as PRODUCTO,c.unidades as UNDS_CMB,c.peso as PESO_CMB,COUNT(pes.id) as UNDS_COL,SUM(pes.pesoneto) as PESO_COL ," +
                    " c.validarUnds as VALID_UNDS , c.validarPeso as VALID_PESO ,c.toleranciaPeso as TOL_PESO "+
                    " FROM Combos c " +
                    " JOIN Productos prdcmb on c.idProductoCombo = prdcmb.id "+
                    " JOIN Productos prd on c.idProducto = prd.id "+
                    " LEFT OUTER JOIN ContenedorPiezas cp on cp.idcontenedor is null and cp.idTipoContenedor = 'CMB' and cp.idestacion = {1} "+
                    " LEFT OUTER JOIN Pesadas pes on pes.id = cp.idpesaje and pes.idproducto = prd.id "+
                    " WHERE c.idProductoCombo = {0} "+
                    " GROUP BY prd.id, prd.nombre, c.unidades, c.peso,c.validarUnds,c.validarPeso,c.toleranciaPeso  ", idProductoCombo,m_OperadorActivo.m_idEstacion);

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
        Metodo:		GetDatSet_PiezasContenidasComboCerradoIdPieza
                    Crea un dataset con las piezas contenidas en un combo que esta cerrado.
                    Indicando solo el id de una pieza contenida
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasContenidasComboCerradoIdPieza(int idPieza,out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //IDPRODUCTO,PRODUCTO,UNDS_CMB,PESO_CMB,UNDS_COL,PESO_COL
                    sqlQuery = String.Format(
                    " SELECT prd.id as IDPRODUCTO,prd.nombre as PRODUCTO,c.unidades as UNDS_CMB,c.peso as PESO_CMB,COUNT(pes.id) as UNDS_COL,"+
                    " SUM(pes.pesoneto) as PESO_COL, "+
                    " c.validarUnds as VALID_UNDS , c.validarPeso as VALID_PESO ,c.toleranciaPeso as TOL_PESO " +
                    " FROM Combos c " +
                    " JOIN Productos prdcmb on c.idProductoCombo = prdcmb.id "+
                    " JOIN Productos prd on c.idProducto = prd.id "+
                    " LEFT OUTER JOIN ContenedorPiezas cp on cp.idcontenedor is not null "+
                    " and cp.idcontenedor = (select idcontenedor from contenedorpiezas where idpesaje = {0}) "+
                    " and cp.idTipoContenedor = 'CMB' "+
                    " LEFT OUTER JOIN Pesadas pes on pes.id = cp.idpesaje and pes.idproducto = prd.id "+
                    " LEFT OUTER JOIN Contenedores cnt on cnt.id = cp.idcontenedor " +
                    " WHERE c.idProductoCombo = cnt.idProducto GROUP BY prd.id, prd.nombre, c.unidades, c.peso  ,idProductoCombo,c.validarUnds,c.validarPeso,c.toleranciaPeso", idPieza);

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
        Metodo:		GetDatSet_PiezasContenidasComboCerradoIdCombo
                    Crea un dataset con las piezas contenidas en un combo que esta cerrado.
                    Indicando solo el id del combo
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasContenidasComboCerradoIdCombo(int idCombo, out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //IDPRODUCTO,PRODUCTO,UNDS_CMB,PESO_CMB,UNDS_COL,PESO_COL
                    sqlQuery = String.Format(
                    " SELECT prd.id as IDPRODUCTO,prd.nombre as PRODUCTO,c.unidades as UNDS_CMB,c.peso as PESO_CMB,COUNT(pes.id) as UNDS_COL," +
                    " SUM(pes.pesoneto) as PESO_COL, " +
                    " c.validarUnds as VALID_UNDS , c.validarPeso as VALID_PESO ,c.toleranciaPeso as TOL_PESO " +
                    " FROM Combos c " +
                    " JOIN Productos prdcmb on c.idProductoCombo = prdcmb.id " +
                    " JOIN Productos prd on c.idProducto = prd.id " +
                    " LEFT OUTER JOIN ContenedorPiezas cp on cp.idcontenedor = {0} and cp.idTipoContenedor = 'CMB' " +
                    " LEFT OUTER JOIN Pesadas pes on pes.id = cp.idpesaje and pes.idproducto = prd.id " +
                    " LEFT OUTER JOIN Contenedores cnt on cnt.id = cp.idcontenedor " +
                    " WHERE c.idProductoCombo = cnt.idProducto GROUP BY prd.id, prd.nombre, c.unidades, c.peso  ,idProductoCombo,c.validarUnds,c.validarPeso,c.toleranciaPeso", idCombo);

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
        Metodo:		GetDatSet_RegistracionesComboLote
                    Crea un dataset con las registraciones de Combo existente en un Lote de Produccion. 
                    Las columnas son de minima informacion dado que es solo para visualizar en una grilla.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_RegistracionesComboLote(DateTime dateLote, out DataSet dsRegistraciones)
        {
            bool obtenidoSinerrorOk = false;
            dsRegistraciones = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //CMB_NRO,COMBO,DESTINO,CREADO,UNIDADES,BRUTO,TARA,NETO
                    sqlQuery = String.Format(
                    " SELECT ca.id as CMB_NRO,prdc.nombre as COMBO,de.nombre as DESTINO, ca.fecha_hora as CREADO,ca.fecha_vencimiento as FECHA_VENCIMIENTO,"+
                    " ca.unidades as UNIDADES, (ca.pesoNeto + ca.pesoTara) BRUTO, ca.pesoTara as TARA, ca.pesoNeto as NETO "+
                    " FROM contenedores ca "+
                    " LEFT OUTER JOIN productos prdc ON ca.idproducto = prdc.id " +
                    " LEFT OUTER JOIN destinos de ON ca.iddestino = de.id " +
                    " WHERE ca.idestacion = {0} AND ca.idtipo='CMB' AND CAST(ca.fecha_hora as DATE) = {{ d '{1}'}} " +
                    " ORDER BY ca.fecha_hora desc ",m_OperadorActivo.m_idEstacion,dateLote.ToString("yyyy-MM-dd"));

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "Cajas"
                    rowsPDESFills = oleDbDataAdapter.Fill(dsRegistraciones, "REGISTRACIONES");
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
        Metodo:		GetConsultaCompletaRegistracionesCombos
                    Crea un dataset con toda la informacion de Combos y sus piezas 
                    contenidas
        Parametros:	out DataSet (infoCombos)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaCompletaRegistracionesCombos(out DataSet dsInfoCombos)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoCombos = new DataSet();
            string sqlQuery;

            DataColumn[] columnsCombosKeys = null;
            DataColumn[] columnsPiezasKeys = null;
            DataRelation datRelacionCombo_pieza;

            try
            {
                int rowsCajasFills = 0;
                int rowsPiezasFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //CMB_NRO,COMBO,DESTINO,CREADO,UNIDADES,BRUTO,TARA,NETO
                    sqlQuery =
                    " SELECT ca.id as CMB_NRO,prdc.nombre as COMBO,de.nombre as DESTINO, ca.fecha_hora as CREADO,"+
                    " (case when ca.fecha_desarmado is null then 'ARM' else 'DES' end) as EST,"+
                    " count(*) as UNIDADES, (ca.pesoNeto + ca.pesoTara) BRUTO, ca.pesoTara as TARA, ca.pesoNeto as NETO " +
                    " FROM contenedores ca " +
                    " LEFT OUTER JOIN ContenedorPiezas cp ON cp.idcontenedor = ca.id " +
                    " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                    " LEFT OUTER JOIN productos prdc ON ca.idproducto = prdc.id " +
                    " LEFT OUTER JOIN destinos de ON ca.iddestino = de.id " +
                    " WHERE ca.idtipo='CMB' " +
                    " GROUP BY ca.id ,prdc.nombre,de.nombre,ca.fecha_hora,ca.fecha_desarmado,ca.pesoTara,ca.pesoNeto " +
                    " ORDER BY ca.fecha_hora desc ";

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsCajasFills = oleDbDataAdapter.Fill(dsInfoCombos, "COMBOS");

                    if (rowsCajasFills != 0)
                    {
                        //CMB_NRO,PRODUCTO,PIEZA,NETO
                        sqlQuery =
                        " SELECT cp.idcontenedor as CMB_NRO,prd.nombre as PRODUCTO,pe.id as PIEZA,pe.PesoNeto as NETO " +
                        " FROM ContenedorPiezas cp " +
                        " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                        " LEFT OUTER JOIN productos prd ON pe.idproducto = prd.id " +
                        " WHERE cp.idtipocontenedor='CMB' " +
                        " order by cp.idcontenedor desc ";


                        //ejecuto el dataadapter con el nuevo query
                        oleDbDataAdapter.SelectCommand.CommandText = sqlQuery;
                        //cargo el resultado del query en el DataSet como tabla "PESADAS"
                        rowsPiezasFills = oleDbDataAdapter.Fill(dsInfoCombos, "PIEZAS");

                        columnsCombosKeys = new DataColumn[] { dsInfoCombos.Tables["COMBOS"].Columns["CMB_NRO"] };

                        columnsPiezasKeys = new DataColumn[] { dsInfoCombos.Tables["PIEZAS"].Columns["CMB_NRO"] };
                        //creo el objeto relacion 
                        datRelacionCombo_pieza = new DataRelation("COMBOS_PIEZAS", columnsCombosKeys, columnsPiezasKeys, false);
                        //sumo el objeto relacion al dataset    
                        dsInfoCombos.Relations.Add(datRelacionCombo_pieza);

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
         * GetCombosFromSelectedDGVCombos
         * Obtiene una lista de clases CContenedor tipo Combo construidas desde una seleccion de 
         * registros desde un DGV.
         * retorna (List<CContenedor>)
        ***************************************************************************************/
        public static List<CContenedor> GetCombosFromSelectedDGVCombos(DataGridView dgv)
        {
            List<CContenedor> listContenedoresCombo = new List<CContenedor>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idContenedor;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        idContenedor = GetCellDGVInt(dgvr, "CMB_NRO");
                        CContenedor cp = GetCombo(idContenedor);
                        listContenedoresCombo.Add(cp);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listContenedoresCombo;
        }
        /***************************************************************************************
         * GetCombosFromSelectedDGVCombos
         * Obtiene una lista de clases CContenedor tipo Combo construidas desde una seleccion de 
         * registros desde un DGV.
         * retorna (List<CContenedor>)
        ***************************************************************************************/
        public static List<CContenedor> GetCombosFromSelectedDGVCombos(DataGridView dgv,string nameFieldNumberCombo)
        {
            List<CContenedor> listContenedoresCombo = new List<CContenedor>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idContenedor;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        idContenedor = GetCellDGVInt(dgvr, nameFieldNumberCombo);
                        CContenedor cp = GetCombo(idContenedor);
                        listContenedoresCombo.Add(cp);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listContenedoresCombo;
        }
        /***************************************************************************************
         * GetCombosFromSelectedDGVCombos
         * Obtiene una lista de clases CContenedor tipo Combo construidas desde una seleccion de 
         * registros desde un DGV.
         * retorna (List<CContenedor>)
        ***************************************************************************************/
        public static List<CContenedor> GetCombosFromSelectedDGVCombos(DataGridView dgv, string nameFieldNumberCombo,string nameFieldType,string valueFieldType)
        {
            List<CContenedor> listContenedoresCombo = new List<CContenedor>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idContenedor;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        if (GetCellDGVString(dgvr, nameFieldType)==valueFieldType)
                        {
                            idContenedor = GetCellDGVInt(dgvr, nameFieldNumberCombo);
                            CContenedor cp = GetCombo(idContenedor);
                            listContenedoresCombo.Add(cp);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listContenedoresCombo;
        }

        #endregion

    }
}
