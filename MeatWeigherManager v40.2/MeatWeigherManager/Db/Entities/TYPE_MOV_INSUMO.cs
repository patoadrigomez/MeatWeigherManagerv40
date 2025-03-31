using System.ComponentModel;

namespace Db
{
    public enum TYPE_INSUMO_MOV
    {
        [Description("ING")]
        Ingreso,
        [Description("EGR")]
        Egreso
    }

    public enum TYPE_INSUMO_PROC
    {
        [Description("IPL")]
        IngresoPlanta,
        [Description("PZA")]
        PesajePieza,
        [Description("CNT")]
        ConformadoContenedor,
        [Description("PED")]
        PreparacionPedido
    }

}
