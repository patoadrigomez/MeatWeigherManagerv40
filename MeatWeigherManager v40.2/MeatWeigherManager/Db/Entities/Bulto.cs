using System.ComponentModel;

namespace Db
{
    public class Bulto
    {
        [DisplayName("Numero")]
        public int Id { get; set; }

        public TipoBulto Tipo { get; set; }

        public string Nombre { get; set; }

        public float Peso { get; set; }

        public CDestino Origen { get; set; }

    }

}
