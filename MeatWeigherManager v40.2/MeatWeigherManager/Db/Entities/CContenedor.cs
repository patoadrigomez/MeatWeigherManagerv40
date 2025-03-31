using System;

namespace Db
{
    public class CContenedor
    {
        private int id;
        private string idTipo;
        public DateTime m_fechaHoraCreacion;
        public DateTime m_fechaVencimiento;
        public int m_idEstacion;
        private float pesoTara;
        private float pesoNeto;
        private CProducto producto;
        private CDestino destino;
        public int m_undsContenidas;

        public int Id { get => id; set => id = value; }
        public string IdTipo { get => idTipo; set => idTipo = value; }
        public CProducto Producto { get => producto; set => producto = value; }
        public CDestino Destino { get => destino; set => destino = value; }
        public float PesoNeto { get => pesoNeto; set => pesoNeto = value; }
        public float PesoTara { get => pesoTara; set => pesoTara = value; }

        public CContenedor()
        {
            InItialize();
        }
        public CContenedor(CContenedor cpyContenedor)
        {
            Id = cpyContenedor.Id;
            IdTipo = cpyContenedor.IdTipo;
            m_fechaHoraCreacion = cpyContenedor.m_fechaHoraCreacion;
            m_fechaVencimiento = cpyContenedor.m_fechaVencimiento;
            PesoTara = cpyContenedor.PesoTara;
            PesoNeto = cpyContenedor.PesoNeto;
            m_idEstacion = cpyContenedor.m_idEstacion;
            Destino = new CDestino(cpyContenedor.Destino);
            Producto = new CProducto(cpyContenedor.Producto);
            m_undsContenidas = cpyContenedor.m_undsContenidas;
        }

        public void InItialize()
        {
            Id = 0;
            IdTipo = "";
            m_fechaHoraCreacion = DateTime.Now;
            m_fechaVencimiento = DateTime.Now;
            PesoNeto = 0.0f;
            PesoTara = 0.0f;
            m_idEstacion = 0;
            Producto = new CProducto();
            Destino = new CDestino();
            m_undsContenidas = 0;
        }
    }

}
