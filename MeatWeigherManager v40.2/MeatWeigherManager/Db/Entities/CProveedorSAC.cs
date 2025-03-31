namespace Db
{
    public class CProveedorSAC
    {
        string m_nombre;
        string m_codigo;
        public string Nombre { get => m_nombre; set => m_nombre = value; }
        public string Codigo { get => m_codigo; set => m_codigo = value; }


        public CProveedorSAC()
        {
            Clear();
        }
        public CProveedorSAC(CProveedorSAC cpyProveedor)
        {
            m_nombre = cpyProveedor.m_nombre;
            m_codigo = cpyProveedor.m_codigo;
        }
        public void Clear()
        {
            m_nombre = "";
            m_codigo = "";
        }
    }

}
