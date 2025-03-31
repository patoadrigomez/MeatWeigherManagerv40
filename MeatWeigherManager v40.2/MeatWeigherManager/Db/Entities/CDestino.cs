namespace Db
{
    public class CDestino
    {
        int m_id;
        string m_nombre;
        public int Id { get => m_id; set => m_id = value; }
        public string Nombre { get => m_nombre; set => m_nombre = value; }

        public CDestino()
        {
            Clear();
        }
        public CDestino(CDestino cpyDestino)
        {
            m_id = cpyDestino.m_id;
            m_nombre = cpyDestino.m_nombre;
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
