namespace Db
{
    public class CItemInsumoProducto : CProducto
    {
        private float unidades;
        private int idInsumoPrimario;
        private bool requiereConfirmacion;



        public CItemInsumoProducto()
        {
            Unidades = 0.0f;
            idInsumoPrimario = 0;
            RequiereConfirmacion = true;
        }

        public float Unidades { get => unidades; set => unidades = value; }
        public int IdInsumoPrimario { get => idInsumoPrimario; set => idInsumoPrimario = value; }
        public bool RequiereConfirmacion { get => requiereConfirmacion; set => requiereConfirmacion = value; }
    }

}
