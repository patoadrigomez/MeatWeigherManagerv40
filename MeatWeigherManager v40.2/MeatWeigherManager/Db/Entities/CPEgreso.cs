using System;

namespace Db
{
    /// <summary>
    /// Clase de informacion de una pieza que egresa de planta
    /// </summary>
    public class CPEgreso : CPedido
    {
        public int IdOperadorRegistration { get; set; }
        public int IdEstacionRegistration { get; set; }
        public CPesada Pieza { get; set; } = null;
        public int IdPedido { get { return base.Id; } }
        public DateTime FechaEgreso { get; set; } = DateTime.Now;

        public CPEgreso(CPedido pedido)
        {
            base.Id = pedido.Id;
            base.Operador = new COperador(pedido.Operador);
            base.CodigoPedidoSAC = pedido.CodigoPedidoSAC;
            base.ComprobantePedidoSAC = pedido.ComprobantePedidoSAC;
            base.Cliente = new CClienteSAC(pedido.Cliente);
            base.Activo = pedido.Activo;
            base.FechaHoraPreparacion = pedido.FechaHoraPreparacion;
            base.TipoPedidoSAC = pedido.TipoPedidoSAC;
        }
    }

}
