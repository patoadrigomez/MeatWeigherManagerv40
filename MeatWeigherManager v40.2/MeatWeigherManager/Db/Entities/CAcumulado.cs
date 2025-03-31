namespace Db
{
    public class CAcumulado
    {
        public int m_pesadas = 0;
        public int m_unidades = 0;
        public float m_bruto = 0.0f;
        public float m_neto = 0.0f;

        public CAcumulado()
        {
        }

        public CAcumulado(int pesadas, int unidades, float bruto, float neto)
        {
            m_pesadas = pesadas;
            m_unidades = unidades;
            m_bruto = bruto;
            m_neto = neto;
        }
    }

}
