namespace Db
{
    public class CTropa
    {
        int m_numero;
        Tipificacion m_Tipificacion;

        public CTropa()
        {
            Clear();
        }
        public CTropa(CTropa cpyTropa)
        {
            Numero = cpyTropa.Numero;
            Tipificacion = new Tipificacion()
            {
                Id = cpyTropa.Tipificacion.Id,
                Nombre = cpyTropa.Tipificacion.Nombre
            };
        }

        public int Numero { get => m_numero; set => m_numero = value; }
        public Tipificacion Tipificacion { get => m_Tipificacion; set => m_Tipificacion = value; }

        public void Clear()
        {
            Numero = 0;
            Tipificacion = new Tipificacion();
        }
    }

}
