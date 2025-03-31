using System.Collections.Generic;

namespace Db
{

    public class CItemInsumoProductoEnProceso
    {
        private bool esConfirmado;
        private int idProductoSelected;
        private int idInsumoSelected;
        private List<CItemInsumoProducto> insumosAlternativos;
        private float unidades;

        public CItemInsumoProductoEnProceso()
        {
            EsConfirmado = false;
        }

        public bool EsConfirmado { get => esConfirmado; set => esConfirmado = value; }
        public int IdProductoSelected { get => idProductoSelected; set => idProductoSelected = value; }
        public int IdInsumoSelected { get => idInsumoSelected; set => idInsumoSelected = value; }
        public List<CItemInsumoProducto> InsumosAlternativos { get => insumosAlternativos; set => insumosAlternativos = value; }
        public float Unidades { get => unidades; set => unidades = value; }
    }
    /*
    public class CItemInsumoProductoEnProceso : CItemInsumoProducto
    {
        private bool esConfirmado;

        public CItemInsumoProductoEnProceso()
        {
            EsConfirmado = false;
        }

        public bool EsConfirmado { get => esConfirmado; set => esConfirmado = value; }
    }
    */
}
