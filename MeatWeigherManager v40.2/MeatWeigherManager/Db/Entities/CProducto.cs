using System;

namespace Db
{
    public class CProducto
    {
        private int id;
        private string nombre;
        public CTipoProducto m_tipo;
        private string codSenasa;
        private float pesoNetoPredefinido;
        private float pesoTaraPredefinida;
        private int unidadesPredefinidas;
        private float rendimientoSTD;
        private int diasVencimientoPredefinido;
        private bool esInsumo;
        private bool esCombo;
        private bool esCaja;
        private bool esPesable;
        private bool esTropa;
        private string nombreEtiL1;
        private string nombreEtiL2;
        private string nombreEtiL3;
        private string nombreEtiL4;
        private string nombreEtiL5;
        private string nombreEtiL6;
        private string textAuxEtiL1;
        private string textAuxEtiL2;
        private CProductoSAC productoSAC;  //producto vinculado SINTEMA ADMINISTRATIVO CONTABLE (SAC)

        public string Nombre { get => nombre; set => nombre = value; }
        public int Id { get => id; set => id = value; }
        public string CodSenasa { get => codSenasa; set => codSenasa = value; }
        public float PesoNetoPredefinido { get => pesoNetoPredefinido; set => pesoNetoPredefinido = value; }
        public float PesoTaraPredefinida { get => pesoTaraPredefinida; set => pesoTaraPredefinida = value; }
        public int UnidadesPredefinidas { get => unidadesPredefinidas; set => unidadesPredefinidas = value; }
        public float RendimientoSTD { get => rendimientoSTD; set => rendimientoSTD = value; }
        public int DiasVencimientoPredefinido { get => diasVencimientoPredefinido; set => diasVencimientoPredefinido = value; }
        public bool EsInsumo { get => esInsumo; set => esInsumo = value; }
        public bool EsCombo { get => esCombo; set => esCombo = value; }
        public bool EsCaja { get => esCaja; set => esCaja = value; }
        public bool EsPesable { get => esPesable; set => esPesable = value; }
        public bool EsTropa { get => esTropa; set => esTropa = value; }
        public string NombreEtiL1 { get => nombreEtiL1; set => nombreEtiL1 = value; }
        public string NombreEtiL2 { get => nombreEtiL2; set => nombreEtiL2 = value; }
        public string NombreEtiL3 { get => nombreEtiL3; set => nombreEtiL3 = value; }
        public string NombreEtiL4 { get => nombreEtiL4; set => nombreEtiL4 = value; }
        public string NombreEtiL5 { get => nombreEtiL5; set => nombreEtiL5 = value; }
        public string NombreEtiL6 { get => nombreEtiL6; set => nombreEtiL6 = value; }
        public string TextAuxEtiL1 { get => textAuxEtiL1; set => textAuxEtiL1 = value; }
        public string TextAuxEtiL2 { get => textAuxEtiL2; set => textAuxEtiL2 = value; }
        public CProductoSAC ProductoSAC { get => productoSAC; set => productoSAC = value; }
        public CEtiqueta Etiqueta { get; set; }


        public CProducto()
        {
            Clear();
        }
        public CProducto(CProducto cpyProducto)
        {
            Id = cpyProducto.Id;
            Nombre = cpyProducto.Nombre;
            m_tipo = new CTipoProducto(cpyProducto.m_tipo);
            CodSenasa = cpyProducto.CodSenasa;
            PesoNetoPredefinido = cpyProducto.PesoNetoPredefinido;
            PesoTaraPredefinida = cpyProducto.PesoTaraPredefinida;
            UnidadesPredefinidas = cpyProducto.UnidadesPredefinidas;
            RendimientoSTD = cpyProducto.RendimientoSTD;
            DiasVencimientoPredefinido = cpyProducto.DiasVencimientoPredefinido;
            EsInsumo = cpyProducto.EsInsumo;
            EsPesable = cpyProducto.EsPesable;
            EsCombo = cpyProducto.EsCombo;
            EsCaja = cpyProducto.EsCaja;
            NombreEtiL1 = cpyProducto.NombreEtiL1;
            NombreEtiL2 = cpyProducto.NombreEtiL2;
            NombreEtiL3 = cpyProducto.NombreEtiL3;
            NombreEtiL4 = cpyProducto.NombreEtiL4;
            NombreEtiL5 = cpyProducto.NombreEtiL5;
            NombreEtiL6 = cpyProducto.NombreEtiL6;
            TextAuxEtiL1 = cpyProducto.TextAuxEtiL1;
            TextAuxEtiL2 = cpyProducto.TextAuxEtiL2;
            ProductoSAC = new CProductoSAC(cpyProducto.ProductoSAC);
            EsTropa = cpyProducto.EsTropa;
            Etiqueta = new CEtiqueta(cpyProducto.Etiqueta);
        }

        public void Clear()
        {
            Id = 0;
            Nombre = "";
            m_tipo = new CTipoProducto();
            CodSenasa = "";
            PesoNetoPredefinido = 0.0f;
            PesoTaraPredefinida = 0.0f;
            UnidadesPredefinidas = 0;
            RendimientoSTD = 0.0f;
            DiasVencimientoPredefinido = 0;
            EsInsumo = false;
            EsCombo = false;
            EsCaja = false;
            EsPesable = true;
            EsTropa = false;
            NombreEtiL1 = "";
            NombreEtiL2 = "";
            NombreEtiL3 = "";
            NombreEtiL4 = "";
            NombreEtiL5 = "";
            NombreEtiL6 = "";
            TextAuxEtiL1 = "";
            TextAuxEtiL2 = "";
            ProductoSAC = new CProductoSAC();
            Etiqueta=new CEtiqueta();
        }
        public override bool Equals(Object obj)
        {
            CProducto cmp = obj as CProducto;
            if (cmp == null)
                return false;
            else
                return base.Equals((CProducto)obj) && id == cmp.id;
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2) ^ id;
        }

    }

}
