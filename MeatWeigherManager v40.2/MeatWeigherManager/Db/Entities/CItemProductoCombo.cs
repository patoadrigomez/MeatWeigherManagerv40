namespace Db
{
    public class CItemProductoCombo : CProducto
    {
        private int unidadesCombo;
        private float pesoCombo;
        private bool validarUnidades;
        private bool validarPeso;
        private float toleranciaPeso;

        public int UnidadesCombo { get => unidadesCombo; set => unidadesCombo = value; }
        public float PesoCombo { get => pesoCombo; set => pesoCombo = value; }
        public bool ValidarUnidades { get => validarUnidades; set => validarUnidades = value; }
        public bool ValidarPeso { get => validarPeso; set => validarPeso = value; }
        public float ToleranciaPeso { get => toleranciaPeso; set => toleranciaPeso = value; }

        public CItemProductoCombo()
        {
            UnidadesCombo = 0;
            PesoCombo = 0.0f;
            ValidarUnidades = true;
            ValidarPeso = true;
            ToleranciaPeso = 30.0f;
        }
    }

}
