using System;
using System.Collections.Generic;

namespace Db
{
    public class COi
    {
        public int m_id;
        public COperador m_Operador;
        public int m_idEstacion;
        public DateTime m_fechaHoraCreacion;
        public string m_idCertSanitario;
        public CProveedorSAC m_proveedor;
        public List<string> m_remitos;
        public List<string> m_facturas;
        public bool m_activo;

        public COi()
        {
            InItialize();
        }
        public COi(COi cpyOI)
        {
            m_id = cpyOI.m_id;
            m_fechaHoraCreacion = cpyOI.m_fechaHoraCreacion;
            m_activo = cpyOI.m_activo;
            m_Operador = new COperador(cpyOI.m_Operador);
            m_idEstacion = cpyOI.m_idEstacion;
            m_idCertSanitario = cpyOI.m_idCertSanitario;
            m_proveedor = new CProveedorSAC(cpyOI.m_proveedor);
            m_remitos = new List<string>(cpyOI.m_remitos);
            m_facturas = new List<string>(cpyOI.m_facturas);
        }

        public void InItialize()
        {
            m_id = 0;
            m_fechaHoraCreacion = DateTime.Now;
            m_activo = true;
            m_Operador = new COperador();
            m_idEstacion = 0;
            m_idCertSanitario = "";
            m_proveedor = new CProveedorSAC();
            m_remitos = new List<string>();
            m_facturas = new List<string>();
        }
    }

}
