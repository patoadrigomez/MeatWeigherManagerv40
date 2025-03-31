using System;

namespace Db
{
    /// <summary>
    /// Clase de informacion de una pieza que ingresa a produccion
    /// </summary>
    public class CPIP : CPesada
    {
        //ocultar acceso
        //private new DateTime m_fechaHora;

        public int IdOperadorRegistration { get; set; }
        public int IdEstacionRegistration { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public int? IdOI
        {
            get { return Oi?.m_id ?? null; }
        }

        public DateTime LotePadre
        {
            get { return base.FechaHora; }
        }
        public CPIP(CPesada part)
        {
            base.Id = part.Id;
            base.Operador = new COperador(part.Operador);
            base.IdEstacion = part.IdEstacion;
            base.Oi = new COi(part.Oi);
            base.FechaHora = part.FechaHora;
            base.Producto = new CProducto(part.Producto);
            base.Unidades = part.Unidades;
            base.PesoNeto = part.PesoNeto;
            base.PesoRemitido = part.PesoRemitido;
            base.PesoTara = part.PesoTara;
            base.IdPiezaPadre = part.IdPiezaPadre;
            base.Destino = new CDestino(part.Destino);
            base.Tropa = new CTropa(part.Tropa);
        }
    }

}
