namespace Db
{
    public class CItemCBoxProducto : CItemCBoxTable
    {
        private string m_nombreL2;
        private string m_nombreL3;
        private string m_nombreL4;

        public string NombreL1
        {
            get { return Nombre; }
            set { Nombre = value; }
        }
        public string NombreL2
        {
            get { return m_nombreL2; }
            set { m_nombreL2 = value; }
        }
        public string NombreL3
        {
            get { return m_nombreL3; }
            set { m_nombreL3 = value; }
        }
        public string NombreL4
        {
            get { return m_nombreL4; }
            set { m_nombreL4 = value; }
        }


        public CItemCBoxProducto()
        {
            NombreL2 = "";
            NombreL3 = "";
            NombreL4 = "";
        }

        public CItemCBoxProducto(int id, string nombre, string nombreL2, string nombreL3, string nombreL4)
            : base(id, nombre)
        {
            Id = id;
            Nombre = nombre;
            NombreL2 = nombreL2;
            NombreL3 = nombreL3;
            NombreL4 = nombreL4;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }

}
