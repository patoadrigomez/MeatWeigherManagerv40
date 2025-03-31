namespace Db
{
    public class CItemCBoxTable
    {
        private int m_id;
        private string m_nombre;
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public string Nombre
        {
            get { return m_nombre; }
            set { m_nombre = value; }
        }
        public CItemCBoxTable()
        {
            Id = 0;
            Nombre = "";
        }
        public CItemCBoxTable(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }

}
