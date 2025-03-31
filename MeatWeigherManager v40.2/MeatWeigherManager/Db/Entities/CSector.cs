namespace Db
{
    public class CSector
    {
        int m_id;
        string m_nombre;
        public int Id { get => m_id; set => m_id = value; }
        public string Nombre { get => m_nombre; set => m_nombre = value; }

        public CSector()
        {
            Clear();
        }
        public CSector(CSector cpySector)
        {
            m_id = cpySector.m_id;
            m_nombre = cpySector.m_nombre;
        }
        public void Clear()
        {
            m_id = 0;
            m_nombre = "";
        }
    }

}
