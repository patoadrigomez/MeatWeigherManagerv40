namespace Db
{
    public class Tipificacion
    {
        int m_id;
        string m_nombre;
        public int Id { get => m_id; set => m_id = value; }
        public string Nombre { get => m_nombre; set => m_nombre = value; }

        public Tipificacion()
        {
            Clear();
        }
        public Tipificacion(Tipificacion cpyTipificacion)
        {
            m_id = cpyTipificacion.m_id;
            m_nombre = cpyTipificacion.m_nombre;
        }
        public void Clear()
        {
            m_id = 0;
            m_nombre = "";
        }
        public override string ToString()
        {
            return Nombre;
        }
    }

}
