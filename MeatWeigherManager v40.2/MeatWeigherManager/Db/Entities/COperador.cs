namespace Db
{
    public class COperador
    {
        public int m_id;
        public string m_nombre;
        public TYPE_OPERATOR m_tipo;
        public int m_idEstacion;
        public string m_pasw;

        public COperador()
        {
            m_id = 0;
            m_nombre = "";
            m_tipo = TYPE_OPERATOR.USUARIO;
            m_idEstacion = 0;
            m_pasw = "";
        }
        public COperador(COperador cpyOperador)
        {
            m_id = cpyOperador.m_id;
            m_nombre = cpyOperador.m_nombre;
            m_tipo = cpyOperador.m_tipo;
            m_idEstacion = cpyOperador.m_idEstacion;
            m_pasw = cpyOperador.m_pasw;
        }
    }

}
