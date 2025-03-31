namespace Db
{
    public class CEtiqueta
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string IdTipoBulto { get; set; } = "";

        public CEtiqueta()
        {
        }
        public CEtiqueta(CEtiqueta cpy)
        {
            Id = cpy.Id;
            Nombre = cpy.Nombre;
            Descripcion = cpy.Descripcion;
            IdTipoBulto = cpy.IdTipoBulto;
        }
        public void Clear()
        {
            Id = 0;
            Nombre  = "";
            Descripcion = "";
            IdTipoBulto = "";
        }
    }
}
