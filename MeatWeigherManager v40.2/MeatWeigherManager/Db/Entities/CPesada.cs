using System;
using System.Collections.Generic;

namespace Db
{
    public class CPesada
    {
        private int id;
        private COi oi;
        private int idEstacion;
        private COperador operador;
        private DateTime fechaHora;
        private CProducto producto;
        private int unidades;
        private float pesoTara;
        private float pesoNeto;
        private float pesoRemitido;
        private int bultos;
        private CDestino destino;
        private CSector sector;
        private int idPiezaPadre;
        private List<CItemInsumoProductoEnProceso> insumos;
        private CTropa tropa;
        private DateTime fechaVencimiento;
        private bool manual;

        /// <summary>
        /// Identifica a que grupo de una Operacion de Pesaje Agrupado pertenece 
        /// </summary>
        private int idGrupo;

        public int Id { get => id; set => id = value; }
        public COi Oi { get => oi; set => oi = value; }
        public int IdEstacion { get => idEstacion; set => idEstacion = value; }
        public COperador Operador { get => operador; set => operador = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public CProducto Producto { get => producto; set => producto = value; }
        public int Unidades { get => unidades; set => unidades = value; }
        public float PesoTara { get => pesoTara; set => pesoTara = value; }
        public float PesoNeto { get => pesoNeto; set => pesoNeto = value; }
        public float PesoRemitido { get => pesoRemitido; set => pesoRemitido = value; }
        public int Bultos { get => bultos; set => bultos = value; }
        public CDestino Destino { get => destino; set => destino = value; }
        public CSector Sector { get => sector; set => sector = value; }
        public int IdPiezaPadre { get => idPiezaPadre; set => idPiezaPadre = value; }
        public int IdGrupo { get => idGrupo; set => idGrupo = value; }
        public DateTime FechaVencimiento { get => fechaVencimiento; set => fechaVencimiento = value; }
        public List<CItemInsumoProductoEnProceso> Insumos { get => insumos; set => insumos = value; }
        public CTropa Tropa { get => tropa; set => tropa = value; }
        public bool Manual { get => manual; set => manual = value; }

        public CPesada()
        {
            Clear();
        }
        public CPesada(CPesada cpyPesada)
        {
            Id = cpyPesada.Id;
            Oi = new COi(cpyPesada.Oi);
            Producto = new CProducto(cpyPesada.Producto);
            IdEstacion = cpyPesada.IdEstacion;
            Operador = new COperador(cpyPesada.Operador);
            FechaHora = cpyPesada.FechaHora;
            Unidades = cpyPesada.Unidades;
            PesoTara = cpyPesada.PesoTara;
            PesoNeto = cpyPesada.PesoNeto;
            PesoRemitido = cpyPesada.PesoRemitido;
            Bultos = cpyPesada.Bultos;
            IdGrupo = cpyPesada.IdGrupo;
            Destino = new CDestino(cpyPesada.Destino);
            Sector = new CSector(cpyPesada.Sector);
            IdPiezaPadre = cpyPesada.IdPiezaPadre;
            Insumos = new List<CItemInsumoProductoEnProceso>(cpyPesada.Insumos);
            Tropa = new CTropa(cpyPesada.Tropa);
            FechaVencimiento = cpyPesada.FechaVencimiento;
            Manual = cpyPesada.manual;
        }
        public void Clear()
        {
            Id = 0;
            Oi = new COi();
            Producto = new CProducto();
            IdEstacion = 0;
            Operador = new COperador();
            FechaHora = DateTime.Now;
            Unidades = 0;
            PesoTara = 0.0f;
            PesoNeto = 0.0f;
            PesoRemitido = 0.0f;
            Bultos = 1;
            IdGrupo = 0;
            Destino = new CDestino();
            Sector = new CSector();
            IdPiezaPadre = 0;
            Insumos = new List<CItemInsumoProductoEnProceso>();
            Tropa = new CTropa();
            fechaVencimiento = DateTime.Now;
            Manual = false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return (obj.GetType() == GetType()) && ((CPesada)obj).Id == Id;
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }

}
