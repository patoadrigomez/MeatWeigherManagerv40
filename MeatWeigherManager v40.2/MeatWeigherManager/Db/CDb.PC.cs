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
        #region OPERACIONES DE PESAJES DE CAJAS
        /***************************************************************************************
         * Metodo:	    AgregarPiezaCajaTemp
         *              Inserta un nuevo registro de vinculo de pieza para una caja que aun no
         *              fue creada . Se inserta en la tabla CONTENEDORPIEZAS, la columna idcaja=null y
         *              se carga el idpieza y el idestacion.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool AgregarPiezaCajaTemp(int idPieza)
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" INSERT INTO CONTENEDORPIEZAS(IDPESAJE,IDESTACION,IDTIPOCONTENEDOR)" +
                                          " VALUES({0},{1},'{2}')", idPieza, m_OperadorActivo.m_idEstacion,"CAJ");

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
         * Metodo:	    GetCaja
         *              Obtiene todos los datos de una Caja indicando su ID
         * Parametro:   int idCaja
         * Retorna:     (CCaja) instancia de la clase que contiene los datos de la Caja.
        *****************************************************************************************/
        public static CContenedor GetCaja(int idCaja)
        {
            OleDbDataReader recordSet;
            CContenedor datCaja = null;
            string strCmd = String.Format(
                " SELECT c.fecha_hora as CREADA,c.pesoNeto as NETO , c.pesoTara as TARA,c.idestacion as IDESTACION,d.id as IDDESTINO,d.nombre as NOMBRE_DESTINO ,"+
                " c.idproducto as IDPRODUCTO,c.idtipo as IDTIPO,c.unidades as UNIDADES ,c.fecha_vencimiento as FECHA_VENCIMIENTO" +
                " FROM CONTENEDORES c ,Destinos d " +
                " WHERE c.id = {0} AND c.idtipo = 'CAJ' AND c.iddestino=d.id ", idCaja);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();

                    datCaja = new CContenedor();
                    datCaja.Id = idCaja;
                    datCaja.m_fechaHoraCreacion = GetCampoDbDateTime(recordSet, "CREADA");
                    datCaja.m_fechaVencimiento = GetCampoDbDateTime(recordSet, "FECHA_VENCIMIENTO");
                    datCaja.PesoNeto = GetCampoDbFloat(recordSet, "NETO");
                    datCaja.PesoTara = GetCampoDbFloat(recordSet, "TARA");
                    datCaja.m_idEstacion = GetCampoDbInt(recordSet, "IDESTACION");
                    datCaja.Producto = GetProducto(GetCampoDbInt(recordSet, "IDPRODUCTO"));
                    datCaja.Destino = new CDestino()
                    {
                        Id = GetCampoDbInt(recordSet, "IDDESTINO"),
                        Nombre = GetCampoDbString(recordSet, "NOMBRE_DESTINO")
                    };
                    datCaja.IdTipo = GetCampoDbString(recordSet, "IDTIPO");
                    datCaja.m_undsContenidas = GetCampoDbInt(recordSet, "UNIDADES"); ;
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datCaja;
        }
        /***************************************************************************************
         * Metodo:	    GetCajaDesdePieza
         *              Obtiene todos los datos de una Caja indicando una pieza contenida
         * Parametro:   int idPieza
         * Retorna:     (CCaja) instancia de la clase que contiene los datos de la Caja.
        *****************************************************************************************/
        public static CContenedor GetCajaDesdePieza(int idPieza)
        {
            OleDbDataReader recordSet;
            CContenedor datCaja = null;
            string strCmd = String.Format(
                " SELECT TOP 1 c.id as IDCAJA,c.fecha_hora as CREADA,c.pesoNeto as NETO , c.pesoTara as TARA,c.idestacion as IDESTACION,d.id as IDDESTINO,d.nombre as NOMBRE_DESTINO ,"+
                " c.idproducto as IDPRODUCTO,c.idtipo as IDTIPO , c.unidades as UNIDADES,c.fecha_vencimiento as FECHA_VENCIMIENTO " +
                " FROM CONTENEDORES c , CONTENEDORPIEZAS cp,Pesadas p ,Destinos d " +
                " WHERE c.fecha_desarmado is null AND cp.idpesaje={0} AND  c.id = cp.idcontenedor AND c.idtipo = 'CAJ' AND cp.idpesaje = p.id AND c.iddestino=d.id ", idPieza);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();

                    datCaja = new CContenedor();
                    datCaja.Id = GetCampoDbInt(recordSet, "IDCAJA"); ;
                    datCaja.m_fechaHoraCreacion = GetCampoDbDateTime(recordSet, "CREADA");
                    datCaja.m_fechaVencimiento = GetCampoDbDateTime(recordSet, "FECHA_VENCIMIENTO");
                    datCaja.PesoNeto = GetCampoDbFloat(recordSet, "NETO");
                    datCaja.PesoTara = GetCampoDbFloat(recordSet, "TARA");
                    datCaja.m_idEstacion = GetCampoDbInt(recordSet, "IDESTACION");
                    datCaja.Producto = GetProducto(GetCampoDbInt(recordSet, "IDPRODUCTO"));
                    datCaja.Destino = new CDestino()
                    {
                        Id = GetCampoDbInt(recordSet, "IDDESTINO"),
                        Nombre = GetCampoDbString(recordSet, "NOMBRE_DESTINO")
                    };
                    datCaja.IdTipo = GetCampoDbString(recordSet, "IDTIPO");
                    datCaja.m_undsContenidas = GetCampoDbInt(recordSet, "UNIDADES");

                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return datCaja;
        }

        /***************************************************************************************
         * Metodo:	    EliminarPiezaCajaTemp
         *              Elimina un nuevo registro de vinculo de pieza para una caja que aun no
         *              fue creada .
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarPiezaCajaTemp(int idPieza)
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE CONTENEDORPIEZAS WHERE IDPESAJE = {0} AND IDESTACION ={1} AND IDCONTENEDOR is null AND IDTIPOCONTENEDOR= 'CAJ' ", idPieza, m_OperadorActivo.m_idEstacion);

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
         * Metodo:	    EliminarPiezasCajaTemp
         *              Elimina todas las piezas temporales para una caja en proceso.
         * Parametro:   
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarPiezasCajaTemp()
        {
            bool registracionOk = false;
            int regAfectados;
            string strCmd = String.Format(" DELETE CONTENEDORPIEZAS WHERE IDESTACION ={0} AND IDCONTENEDOR is null AND IDTIPOCONTENEDOR= 'CAJ' ",m_OperadorActivo.m_idEstacion);

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
         * Metodo:	    EliminarPiezaContenedor
         *              Elimina una pieza contenida en una contenedor indicando el id de la pieza
         *              y el tipo de contenedor. Tambien actualiza el valor de peso y unidades 
         *              en el contenedor.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool EliminarPiezaContenedor(int idPieza,string idTipoContenedor="")
        {
            bool eliminadaOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_borrarPiezaEnContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idPieza",
                        DbType = DbType.Int32,
                        Value = idPieza
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipoContenedor",
                        DbType = DbType.String,
                        Size=10,
                        Value = idTipoContenedor
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@result",
                        DbType = DbType.Boolean,
                        Direction = ParameterDirection.Output,
                    });

                    dbCommand.ExecuteNonQuery();
                    eliminadaOk = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return eliminadaOk;
        }
        /***************************************************************************************
         * Metodo:	    IsPartInStock
         *              Verifica si una pieza se encuentra en stock.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsPartInStock(int idPieza)
        {
            bool enStock = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esPiezaEnStock", m_oleDbConnection);

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

                    dbCommand.ExecuteNonQuery();
                    enStock = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return enStock;
        }
        /***************************************************************************************
         * Metodo:	    IsValidDisarmContainer
         *              Verifica si un contenedor es valido para eliminar.
         *              Se debe cumplir que el contenedor se encuentre en stock.
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidDisarmContainer(int idContenedor)
        {
            bool esValido = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoDesarmarContenedor", m_oleDbConnection);

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
                    esValido = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return esValido;
        }

        /***************************************************************************************
         * Metodo:	    BorrarContenedorEgresadoDelPedido
         *              Elimina un contenedor de un pedido especifico de la tabla egresos
         * Parametro:   int idCaja
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool BorrarContenedorEgresadoDelPedido(int idContenedor,int idPedido,out string detailResult)
        {
            detailResult="";
            bool borradoOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_borrarContenedorEgresado", m_oleDbConnection);

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
         * Metodo:	    InsertNewContainer
         *              Genera un registro de un nuevo contenedor y vincula las piezas colectadas
         *              hasta el momento en la tabla CONTENEDORPIEZAS. Estas piezas mientras que estan en
         *              modo temporal poseen el campo idcaja=null , cuando se crea el contenedor ese campo
         *              se actualiza con el valor del id del nuevo contenedor.
         *              
         * Parametro:   CContenedor datContenedor (datos del nuevo contenedor a crear).
         * 
         * Retorna:     (CContenedor) instancia de la clase que contiene los datos.
        *****************************************************************************************/
        public static bool InsertNewContainer(ref CContenedor datContenedor)
        {
            bool registracionOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_generarNuevoContenedor", m_oleDbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idEstacion",
                        DbType = DbType.Int32,
                        Value = CDb.m_OperadorActivo.m_idEstacion
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@pesoTara",
                        DbType = DbType.Single,
                        Value = datContenedor.PesoTara
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@pesoNeto",
                        DbType = DbType.Single,
                        Value = datContenedor.PesoNeto
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idDestino",
                        DbType = DbType.Int32,
                        Value = datContenedor.Destino.Id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idTipo",
                        DbType = DbType.String,
                        Size=3,
                        Value = datContenedor.IdTipo
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idProducto",
                        DbType = DbType.Int32,
                        Value = datContenedor.Producto.Id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@unidades",
                        DbType = DbType.Int32,
                        Value = datContenedor.m_undsContenidas
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idOperador",
                        DbType = DbType.Int32,
                        Value = CDb.m_OperadorActivo.m_id
                    });

                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@idNuevoContenedor",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output,
                    });
                    dbCommand.Parameters.Add(new OleDbParameter()
                    {
                        ParameterName = "@fecha_vencimiento",
                        DbType = DbType.DateTime,
                        Direction = ParameterDirection.Output,
                    });


                    dbCommand.ExecuteNonQuery();
                    datContenedor.Id = Convert.ToInt32(dbCommand.Parameters["@idNuevoContenedor"].Value);
                    datContenedor.m_fechaVencimiento = dbCommand.Parameters["@fecha_vencimiento"].Value == DBNull.Value ? 
                                                            datContenedor.m_fechaHoraCreacion.AddDays(datContenedor.Producto.DiasVencimientoPredefinido) : 
                                                            Convert.ToDateTime(dbCommand.Parameters["@fecha_vencimiento"].Value);
                    registracionOk = true;
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return registracionOk;
        }
        /***************************************************************************************
         * Metodo:	    RelatePartsToNewCaja
         *              vincula a la nueva caja todas las piezas que se encuentran en modo temporal
         *              en la actual estacion de pesaje.
         *              
         * Parametro:   int idnewcaja.
         * 
         * Retorna:     true if ok.
        *****************************************************************************************/
        public static bool RelatePartsToNewCaja(int idNewCaja)
        {
            bool registracionOk = false;

            int regAfectados;

            string strCmd = String.Format(" UPDATE CONTENEDORPIEZAS set IDCONTENEDOR = {0} WHERE idcontenedor is null AND idestacion = {1} and idtipocontenedor = 'CAJ' ", idNewCaja, m_OperadorActivo.m_idEstacion);
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
         * Metodo:	    GetFirstPartContainCaja
         *              Obtiene los datos del primer registro de pieza contenida en la caja.
         * Parametro:   int idCaja
         * Retorna:     (CPesada) infoPieza.
        *****************************************************************************************/
        public static CPesada GetFirstPartContainCaja(int idCaja)
        {
            OleDbDataReader recordSet;
            CPesada infoPieza = null;
            int undsContenidas = 0;
            int idPieza = 0;

            string strCmd = String.Format(" SELECT TOP 1 IDPESAJE ,(select count(*) from contenedorpiezas where idcontenedor={0}) as UNDS_CONTENIDAS FROM CONTENEDORPIEZAS WHERE idcontenedor= {0} ORDER BY idpesaje ASC ", idCaja);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    idPieza = GetCampoDbInt(recordSet, "IDPESAJE");
                    undsContenidas= GetCampoDbInt(recordSet, "UNDS_CONTENIDAS");
                    if (idPieza != 0)
                    {
                        infoPieza = CDb.GetPesada(idPieza);
                        infoPieza.Unidades = undsContenidas;
                    }
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return infoPieza;
        }
        /***************************************************************************************
         * Metodo:	    IsValidProductForCaja
         *              Verifica si el producto de la pieza indicada es el que debe contener
         *              la caja.
         * Parametro:   int idProductoCaja,int idPieza
         * Retorna:     true si el producto de la pieza indicada es igual a las contenidas o si la tabla
         *              de registracion de piezas contenidas esta vacia para la estacion en proceso.
        *****************************************************************************************/
        public static bool IsValidProductForCaja(int idProductoCaja,int idPieza)
        {
            OleDbDataReader recordSet;
            bool isValid = false;
            int idPrd = 0;

            string strCmd = String.Format(" SELECT ca.idproducto as IDPRODUCTO FROM PESADAS pe,CAJAS ca  " +
                                          " WHERE pe.id = {0} AND ca.idproductocaja = {1} AND ca.idproducto = pe.idproducto " , idPieza,idProductoCaja);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    idPrd = GetCampoDbInt(recordSet, "IDPRODUCTO");
                    isValid = (idPrd != 0);
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return isValid;
        }
        /***************************************************************************************
         * Metodo:	    GetProductoAContenerPorLaCaja
         *              Obtiene el producto que debe contener un producto tipo CAJA
         * Parametro:   int idProductoCaja
         * Retorna:     CProducto si la consulta resulta OK y null si hay inconsistencias para obtener
         *              el producto.
        *****************************************************************************************/
        public static CProducto GetProductoAContenerPorLaCaja(int idProductoCaja)
        {
            OleDbDataReader recordSet;
            CProducto prdContener = null;

            string strCmd = String.Format(" SELECT idproducto as IDPRODUCTO_CONTENER FROM CAJAS WHERE idproductocaja = {0}", idProductoCaja);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    int idPrd = GetCampoDbInt(recordSet, "IDPRODUCTO_CONTENER");
                    prdContener = GetProducto(idPrd);
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return prdContener;
        }

        /***************************************************************************************
         * Metodo:	    IsValidPartForIncludeToCaja
         *              Verifica si la pieza indicada es valida para ser incluida en una caja.
         * Parametro:   int idProductoCaja,int idPieza,string detailResult
         * Retorna:     true si la pieza el valida para incluirce en la caja.
         *              false si no es valida y el detalle de la no validacion es detailResult
        *****************************************************************************************/
        public static bool IsValidPartForIncludeToCaja(int idProductoCaja, int idPieza, out string detailResult)
        {
            bool isValid = false;
            detailResult = "";
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoAgregarPiezaACaja", m_oleDbConnection);

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
                        Value = idProductoCaja
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
         * Metodo:	    IsValidPartForDeleteToClosedContainer
         *              Verifica si la pieza indicada es valida para ser eliminada de un contenedor cerrado
         *              (en proceso).
         * Parametro:   int idPieza,string detailResult
         * Retorna:     true si la pieza es valida para ser borrada del contenedor.
         *              false si no es valida y el detalle de la no validacion es detailResult
        *****************************************************************************************/
        public static bool IsValidPartForDeleteToClosedContainer(int idPieza, out string detailResult)
        {
            bool isValid = false;
            detailResult = "";
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoBorrarPiezaEnContenedorCerrado", m_oleDbConnection);

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
         * Metodo:	    IsContainerOut
         *              Verifica si un contenedor es valido para realizar una devolucion , es decir 
         *              que este egresado y su pedido esta cerrado.
         * Parametro:   int idContenedor
         * Retorna:     true si esta egresado.
        *****************************************************************************************/
        public static bool IsValidContainerForReturn(int idContenedor)
        {
            bool esValida = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidoContenedorParaDevolucion", m_oleDbConnection);

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
                    esValida = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return esValida;
        }
        /***************************************************************************************
         * Metodo:	    IsAnyPartInConteinerNotInProduction
         *              Verifica si alguna de las piezas de un contenedor no se encuentra en produccion
         *    
         * Parametro:   int idContenedor
         * Retorna:     true si alguna de las piezas de una caja no se encuentra en produccion.
        *****************************************************************************************/
        public static bool IsAnyPartInConteinerNotInProduction(int idContenedor)
        {
            OleDbDataReader recordSet;
            bool partExist = false;
            int id = 0;

            string strCmd = String.Format(" SELECT TOP 1 cp.idpesaje AS ID FROM CONTENEDORES c,ContenedorPiezas cp  WHERE c.id = {0} and cp.idcontenedor = c.id AND cp.idpesaje not in (select idpesaje from DLP) ", idContenedor);
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(strCmd, m_oleDbConnection);
                recordSet = dbCommand.ExecuteReader();
                if (recordSet.HasRows)
                {
                    recordSet.Read();
                    id = GetCampoDbInt(recordSet, "ID");
                    partExist = (id != 0);
                }
                recordSet.Close();
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return partExist;
        }
        /***************************************************************************************
         * Metodo:	    GetTotalNetoPartsContainCaja
         *              Obtiene el peso neto total de las piezas registradas a vincular como contenido
         *              de la caja para la estacion local.
         * Parametro:   
         * Retorna:     valor de peso neto total. Si es 0 es porque no hay registros de piezas a 
         *              vincular en la caja.
        *****************************************************************************************/
        public static float GetTotalNetoPartsContainCaja()
        {
            OleDbDataReader recordSet;
            float totalNetoContain = 0.0f;

            string strCmd = String.Format(" SELECT SUM(pe.pesoneto) as TOTAL_NETO FROM CONTENEDORPIEZAS cp,PESADAS pe " +
                                          " WHERE cp.idestacion = {0} AND cp.idcontenedor is null AND cp.idtipocontenedor = 'CAJ' AND cp.idpesaje = pe.id ",m_OperadorActivo.m_idEstacion);
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
         * Metodo:	    GetTotalUndsPartsContainTempCaja
         *              Obtiene la cantidad total de unidades contenidas en la caja en estado temporal.
         * Parametro:   
         * Retorna:     cantidad de unidades. Si es 0 es porque no hay registros de piezas.
        *****************************************************************************************/
        public static int GetTotalUndsPartsContainTempCaja()
        {
            OleDbDataReader recordSet;
            int undsContenidas = 0;

            string strCmd = String.Format(" SELECT COUNT(*) as UNDS FROM CONTENEDORPIEZAS cp,PESADAS pe " +
                                          " WHERE cp.idestacion = {0} AND cp.idcontenedor is null AND cp.idtipocontenedor = 'CAJ' AND cp.idpesaje = pe.id ", m_OperadorActivo.m_idEstacion);
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

        /***************************************************************************************
         * GetWeightingFromSelectedDGVCajas
         * Obtiene una lista de clases CContenedor construidas desde una seleccion de pesadas de Cajas
         * en una grilla de tipo DataGridView.
         * retorna (List<CContenedor>)
        ***************************************************************************************/
        public static List<CContenedor> GetWeightingFromSelectedDGVCajas(DataGridView dgv)
        {
            List<CContenedor> listPesadasCajas = new List<CContenedor>();
            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int idCaja;
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        idCaja = GetCellDGVInt(dgvr, "CAJA");
                        CContenedor caja = GetCaja(idCaja);
                        if (caja != null)
                        {
                            listPesadasCajas.Add(caja);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new CDbException("Error Obtieniendo las Pesadas Seleccionadas en la grilla: " + exp.Source + "--" + exp.Message);
            }
            return listPesadasCajas;
        }

        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES DE PESAJES DE CAJAS

        /***************************************************************************************
        Metodo:		GetDatSet_PiezasContenidasCajaAbierta
                    Crea un dataset con las piezas contenidas en una caja temporal. Informacion minima
                    dado que es solo para visualizar en una grilla.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasContenidasCajaAbierta(out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //PIEZA,PRODUCTO,PESO_NETO
                    sqlQuery = String.Format(
                    " SELECT pe.id as PIEZA,prd.nombre as PRODUCTO,pe.PesoNeto as PESO_NETO " +
                    " FROM ContenedorPiezas cp " +
                    " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                    " LEFT OUTER JOIN productos prd ON pe.idproducto = prd.id  " +
                    " WHERE cp.idcontenedor is null AND cp.idtipocontenedor = 'CAJ' AND cp.idestacion = {0} order by cp.idcontenedor desc  ",m_OperadorActivo.m_idEstacion);

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
        Metodo:		GetDatSet_PiezasContenidasCajaCerradaIdPieza
                    Crea un dataset con las piezas contenidas en una caja que esta cerrada.
                    Indicando solo el id de una pieza contenida
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasContenidasCajaCerradaPorIdPieza(int idPieza,out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //PIEZA,PRODUCTO,PESO_NETO
                    sqlQuery = String.Format(
                    " SELECT pe.id as PIEZA,prd.nombre as PRODUCTO,pe.PesoNeto as PESO_NETO " +
                    " FROM CONTENEDORPIEZAS cp " +
                    " JOIN Contenedores c on cp.idcontenedor = c.id "+
                    " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                    " LEFT OUTER JOIN productos prd ON pe.idproducto = prd.id  " +
                    " WHERE c.fecha_desarmado is null " +
                    " AND cp.idcontenedor in ( select idcontenedor from CONTENEDORPIEZAS where idpesaje={0} and idtipocontenedor='CAJ' ) order by cp.idcontenedor desc  ", idPieza);

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
        Metodo:		GetDatSet_PiezasContenidasCajaCerradaIdCaja
                    Crea un dataset con las piezas contenidas en una caja que esta cerrada.
                    Indicando solo el id de la caja
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PiezasContenidasCajaCerradaPorIdCaja(int idCaja, out DataSet dsPiezas)
        {
            bool obtenidoSinerrorOk = false;
            dsPiezas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //PIEZA,PRODUCTO,PESO_NETO
                    sqlQuery = String.Format(
                    " SELECT pe.id as PIEZA,prd.nombre as PRODUCTO,pe.PesoNeto as PESO_NETO " +
                    " FROM CONTENEDORPIEZAS cp " +
                    " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                    " LEFT OUTER JOIN productos prd ON pe.idproducto = prd.id  " +
                    " WHERE cp.idcontenedor = {0} AND cp.idtipocontenedor ='CAJ' order by cp.idcontenedor desc  ", idCaja);

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
        Metodo:		GetDatSet_PesadasCajasLote
                    Crea un dataset con las pesadas de Cajas existente en un Lote de Produccion. Las columnas son de 
                    minima informacion dado que es solo para visualizar en una grilla.
        Parametros:	out DataSet 
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetDatSet_PesadasCajasLote(DateTime dateLote, out DataSet dsPesadas)
        {
            bool obtenidoSinerrorOk = false;
            dsPesadas = new DataSet();
            string sqlQuery;

            try
            {
                int rowsPDESFills = 0;

                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    //CAJA,DESTINO,CREADA,PRODUCTO,UNIDADES,BRUTO,TARA,NETO
                    sqlQuery = String.Format(
                    " SELECT ca.id as CAJA,de.nombre as DESTINO, ca.fecha_hora as CREADA,prd.nombre as PRODUCTO, count(*) as UNIDADES, (ca.pesoNeto + ca.pesoTara) BRUTO, ca.pesoTara as TARA, ca.pesoNeto as NETO "+
                    " FROM contenedores ca "+
                    " LEFT OUTER JOIN ContenedorPiezas cp ON cp.idcontenedor = ca.id "+
                    " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id "+
                    " LEFT OUTER JOIN productos prd ON pe.idproducto = prd.id "+
                    " LEFT OUTER JOIN destinos de ON ca.iddestino = de.id " +
                    " WHERE ca.idestacion = {0} AND ca.idtipo='CAJ' AND CAST(ca.fecha_hora as DATE) = {{ d '{1}'}} " +
                    " GROUP BY ca.id ,de.nombre,ca.fecha_hora,prd.nombre,ca.pesoTara,ca.pesoNeto "+
                    " ORDER BY ca.fecha_hora desc ",m_OperadorActivo.m_idEstacion,dateLote.ToString("yyyy-MM-dd"));

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
        Metodo:		GetConsultaCompletaPesajesCajas
                    Crea un dataset con toda la informacion de las CAJAS pesadas y sus piezas 
                    contenidas
        Parametros:	out DataSet (infoCajas)
        Retorna:    true si se pudo obtener el dataser sin errores de base de datos. 
        *****************************************************************************************/
        public static bool GetConsultaCompletaPesajesCajas(out DataSet dsInfoCajas)
        {
            bool obtenidoSinerrorOk = false;
            dsInfoCajas = new DataSet();
            string sqlQuery;

            DataColumn[] columnsCajasKeys = null;
            DataColumn[] columnsPiezasKeys = null;
            DataRelation datRelacionCaja_pieza;

            try
            {
                int rowsCajasFills = 0;
                int rowsPiezasFills = 0;
                if (m_oleDbConnection.State == ConnectionState.Open)
                {   //CAJA,CREADA,PRODUCTO,DESTINO,UNIDADES,BRUTO,TARA,NETO
                    sqlQuery =
                    " SELECT ca.id as CAJA, ca.fecha_hora as CREADA,"+
                    " (case when ca.fecha_desarmado is null then 'ARM' else 'DES' end) as EST," +
                    " prd.nombre as PRODUCTO,de.nombre as DESTINO, count(*) as UNIDADES, (ca.pesoNeto + ca.pesoTara) BRUTO, ca.pesoTara as TARA, ca.pesoNeto as NETO " +
                    " FROM contenedores ca " +
                    " LEFT OUTER JOIN ContenedorPiezas cp ON cp.idcontenedor = ca.id " +
                    " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                    " LEFT OUTER JOIN productos prd ON pe.idproducto = prd.id " +
                    " LEFT OUTER JOIN destinos de ON ca.iddestino = de.id " +
                    " WHERE ca.idtipo='CAJ' "+
                    " GROUP BY ca.id ,de.nombre,ca.fecha_hora,ca.fecha_desarmado,prd.nombre,ca.pesoTara,ca.pesoNeto " +
                    " ORDER BY ca.fecha_hora desc ";

                    //creo un objeto OleDbDataAdapter a partir del query y la conexion existente con la Db.
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sqlQuery, m_oleDbConnection);
                    //sumo al dataSet el resultado del query como tabla "HC"
                    rowsCajasFills = oleDbDataAdapter.Fill(dsInfoCajas, "CAJAS");

                    if (rowsCajasFills != 0)
                    {
                        //CAJA,PIEZA,NETO
                        sqlQuery =
                        " SELECT cp.idcontenedor as CAJA,pe.id as PIEZA,pe.PesoNeto as NETO " +
                        " FROM ContenedorPiezas cp " +
                        " LEFT OUTER JOIN pesadas pe ON cp.idpesaje = pe.id " +
                        " order by cp.idcontenedor desc ";


                        //ejecuto el dataadapter con el nuevo query
                        oleDbDataAdapter.SelectCommand.CommandText = sqlQuery;
                        //cargo el resultado del query en el DataSet como tabla "PESADAS"
                        rowsPiezasFills = oleDbDataAdapter.Fill(dsInfoCajas, "PIEZAS");

                        columnsCajasKeys = new DataColumn[] { dsInfoCajas.Tables["CAJAS"].Columns["CAJA"] };

                        columnsPiezasKeys = new DataColumn[] { dsInfoCajas.Tables["PIEZAS"].Columns["CAJA"] };
                        //creo el objeto relacion 
                        datRelacionCaja_pieza = new DataRelation("CAJAS_PIEZAS", columnsCajasKeys, columnsPiezasKeys, false);
                        //sumo el objeto relacion al dataset    
                        dsInfoCajas.Relations.Add(datRelacionCaja_pieza);

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
         * Metodo:	    GetConsultaReporte_ExistenciaEnStockContenedoresPorDestino
         *              Obtiene un DataSet con una consulta de Existencia contenedores Cajas o Combos
         *              en Stock por producto y Destino.
         * Parametro    
         * Parametro    
         * Retorna:     DataSet 
        *****************************************************************************************/
        public static DataTable GetConsultaReporte_ExistenciaEnStockContenedoresPorDestino(int idTipoProducto,DateTime fechaHasta,int idProducto, int idUbicacion)
        {
            //CONTENEDOR,PRODUCTO,DESTINO,BULTOS,UNIDADES,PESONETO
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
                //PRODUCTO,DESTINO,CAJAS,UNIDADES,NETO
                dt = SelectStoreProcedure("sp_repExistenciaEnStockContenedoresTotalizadoPorDestino", "MOV_PESADAS", lparam);
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return dt;
        }
        /***************************************************************************************
         * Metodo:	    EsContenedorEnStock
         *              Verifica si un contenedor se encuentra en stock.
         * Parametro:   int idContenedor
         * Retorna:     true si esta en stock.
        *****************************************************************************************/
        public static bool EsContenedorEnStock(int idContenedor)
        {
            bool esValido = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esContenedorEnStock", m_oleDbConnection);

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
                    esValido = Convert.ToBoolean(dbCommand.Parameters["@result"].Value);
                }
            }
            catch (OleDbException e)
            {
                throw new CDbException("Error en Base de Datos: " + e.Source + "--" + e.Message);
            }
            return esValido;
        }

        #endregion

    }
}
