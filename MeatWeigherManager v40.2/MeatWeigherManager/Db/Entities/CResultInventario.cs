namespace Db
{
    public class CResultInventario
    {
        public int IdPedidoAjuste { get; set; } = 0;
        public CTotalesBultos Stock { get; set; } = new CTotalesBultos();
        public CTotalesBultos StockVerificado { get; set; } = new CTotalesBultos();
        public int BultosSinRegistracion { get; set; } = 0;
        public int BultosEnPedidosAbiertosNoExisten { get; set; } = 0;
        public CTotalesBultos BultosConSTLsinSTF { get; set; } = new CTotalesBultos();
        public CTotalesBultos BultosSinSTLconSTF { get; set; } = new CTotalesBultos();
        public CTotalesBultos BultosConSTLsinSTFAjustados { get; set; } = new CTotalesBultos();
        public CTotalesBultos BultosSinSTLconSTFAjustados { get; set; } = new CTotalesBultos();
        public int PiezasFueraContenedorEnStock { get; set; } = 0;
        public int PiezasFueraContenedorSinStock { get; set; } = 0;
        public int PiezasFueraContenedorEnStockAjustadas { get; set; } = 0;
        public int PiezasFueraContenedorSinStockAjustadas { get; set; } = 0;
        public int BultosActualizadosEnHubicacion { get; set; } = 0;
        public CTotalesBultos StockDespuesAjuste { get; set; } = new CTotalesBultos();

    }

}
