using System;

namespace Db
{
    public class CPedido
    {
        int m_id;
        int m_codigoPedidoSAC;
        string m_comprobantePedidoSAC;
        CClienteSAC m_cliente;
        DateTime m_fechaHoraCreacion;
        DateTime m_fechaHoraEntrega;
        COperador m_Operador;
        bool m_activo;
        string m_tipoPedidoSAC;

        public int Id { get => m_id; set => m_id = value; }
        public CClienteSAC Cliente { get => m_cliente; set => m_cliente = value; }
        public DateTime FechaHoraCreacion { get => m_fechaHoraCreacion; set => m_fechaHoraCreacion = value; }
        public DateTime FechaHoraPreparacion { get => m_fechaHoraEntrega; set => m_fechaHoraEntrega = value; }
        public COperador Operador { get => m_Operador; set => m_Operador = value; }
        public bool Activo { get => m_activo; set => m_activo = value; }
        public int CodigoPedidoSAC { get => m_codigoPedidoSAC; set => m_codigoPedidoSAC = value; }
        public string ComprobantePedidoSAC { get => m_comprobantePedidoSAC; set => m_comprobantePedidoSAC = value; }
        public string TipoPedidoSAC { get => m_tipoPedidoSAC; set => m_tipoPedidoSAC = value; }

        public CPedido()
        {
            Clear();
        }
        public CPedido(CPedido cpyPedido)
        {
            m_id = cpyPedido.m_id;
            m_cliente = new CClienteSAC(cpyPedido.m_cliente);
            m_codigoPedidoSAC = cpyPedido.m_codigoPedidoSAC;
            m_comprobantePedidoSAC = cpyPedido.m_comprobantePedidoSAC;
            m_fechaHoraCreacion = cpyPedido.m_fechaHoraCreacion;
            m_fechaHoraEntrega = cpyPedido.m_fechaHoraEntrega;
            m_Operador = new COperador(cpyPedido.m_Operador);
            m_activo = false;
            m_tipoPedidoSAC = cpyPedido.m_tipoPedidoSAC;
        }
        public void Clear()
        {
            m_id = 0;
            m_codigoPedidoSAC = 0;
            m_comprobantePedidoSAC = "";
            m_fechaHoraCreacion = DateTime.Now;
            m_fechaHoraEntrega = DateTime.Now;
            m_Operador = new COperador();
            m_activo = false;
            m_cliente = new CClienteSAC();
            m_tipoPedidoSAC = "";
        }
    }
}
