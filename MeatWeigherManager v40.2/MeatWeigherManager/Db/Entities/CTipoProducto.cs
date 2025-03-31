namespace Db
{
    public class CTipoProducto
    {
        private int id = 0;
        private string nombre = "";

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public CTipoProducto()
        {
        }

        public CTipoProducto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public CTipoProducto(CTipoProducto cpyTipoProducto)
        {
            Id = cpyTipoProducto.Id;
            Nombre = cpyTipoProducto.Nombre;
        }
    }

}
