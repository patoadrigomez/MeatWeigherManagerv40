using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Db
{
    /// <summary>
    /// CLASE PARCIAL CDb con metodos para funciones de Fraccionamientos
    /// </summary>
    public static partial class CDb
    {

        #region OPERACIONES DE FRACCIONAMIENTOS
        #endregion

        #region CONSULTAS PARA VISTAS Y REPORTES

        /***************************************************************************************
         * Metodo:	    IsValidPartForDivicion
         *              Verifica si una pieza es valida para tener un proceso de fraccionamiento
         * Parametro:   int idPieza
         * Retorna:     true si no tuvo error.
        *****************************************************************************************/
        public static bool IsValidPartForDivicion(int idPieza, out string detailResult)
        {
            detailResult = "";
            bool validOk = false;
            try
            {
                if (m_oleDbConnection.State == ConnectionState.Open)
                {
                    OleDbCommand dbCommand = new OleDbCommand("sp_esValidaPiezaParaFraccionar", m_oleDbConnection);

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


        #endregion

    }
}
