namespace Db
{
    public class CItemIdTextCBoxTable
    {
        private string m_id;
        private string m_nombre;
        public string Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public string Nombre
        {
            get { return m_nombre; }
            set { m_nombre = value; }
        }
        public CItemIdTextCBoxTable()
        {
            Id = "";
            Nombre = "";
        }
        public CItemIdTextCBoxTable(string id, string nombre)
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
