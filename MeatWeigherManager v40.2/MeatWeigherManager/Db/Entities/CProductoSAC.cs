namespace Db
{
    public class CProductoSAC
    {
        private string codigo;
        private string nombre;
        private string alias;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Alias { get => alias; set => alias = value; }

        public CProductoSAC()
        {
            Clear();
        }
        public CProductoSAC(CProductoSAC cpyProducto)
        {
            Codigo = cpyProducto.Codigo;
            Nombre = cpyProducto.Nombre;
            Alias = cpyProducto.Alias;
        }

        public void Clear()
        {
            Codigo = "";
            Nombre = "";
            Alias = "";
        }
    }

}
