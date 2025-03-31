namespace Db
{
    public class CClienteSAC
    {
        string m_codigo;
        string m_nombre;
        public string Nombre { get => m_nombre; set => m_nombre = value; }
        public string Codigo { get => m_codigo; set => m_codigo = value; }

        public CClienteSAC()
        {
            Clear();
        }
        public CClienteSAC(CClienteSAC cpyCliente)
        {
            m_nombre = cpyCliente.m_nombre;
            m_codigo = cpyCliente.m_codigo;
        }
        public void Clear()
        {
            m_nombre = "";
            m_codigo = "";
        }
    }

}
