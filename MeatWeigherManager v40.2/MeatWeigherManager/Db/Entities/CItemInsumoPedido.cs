namespace Db
{
    public class CItemInsumoPedido : CProducto
    {
        private float unidades;
        private int idPedido;

        public CItemInsumoPedido()
        {
            Unidades = 0.0f;
            IdPedido = 0;

        }

        public float Unidades { get => unidades; set => unidades = value; }
        public int IdPedido { get => idPedido; set => idPedido = value; }
    }

}
