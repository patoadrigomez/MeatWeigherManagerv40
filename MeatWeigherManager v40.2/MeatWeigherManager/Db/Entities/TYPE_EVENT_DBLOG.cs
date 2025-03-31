using System.ComponentModel;

namespace Db
{
    public enum TYPE_EVENT_DBLOG
    {
        [Description("ELIMINACIÓN DE ORDEN DE INGRESO")]
        EliminacionOrdenDeIngreso,
        [Description("CIERRE DE ORDEN DE INGRESO")]
        CierreDeOrdenDeIngreso,
        [Description("CIERRE DE PEDIDO")]
        CierreDePedido,
        [Description("ELIMINACIÓN DE PIEZA")]
        EliminacionPieza,
        [Description("ELIMINACIÓN DE EGRESO DE PIEZA")]
        EliminacionEgresoPieza,
        [Description("ELIMINACIÓN DE EGRESO DE CONTENEDOR")]
        EliminacionEgresoContenedor,
        [Description("DESARMADO DE CONTENEDOR")]
        DesarmadoDeContenedor,
        [Description("MODIFICACION DE ESTADO DE PEDIDO")]
        ModificacionEstadoPedido,
        [Description("ELIMINACION BULTO EGRESADO")]
        EliminacionBultoEgresado,
        [Description("ELIMINACIÓN DE CONTENEDOR COLECTADO")]
        EliminacionContenedorColectado,
        [Description("ELIMINACIÓN DE PIEZA COLECTADA")]
        EliminacionPiezaColectada,
        [Description("ELIMINACIÓN DE PIEZA QUE INGRESÓ A PRODUCCIÓN")]
        EliminacionPiezaEnIngresoAProduccion,
        [Description("SE REALIZÓ AJUSTE MANUAL DE STOCK PARA INSUMO")]
        AjusteManualDeStockDeInsumo,
        [Description("SE REALIZÓ EDICIÓN DE PARAMETRO DIAS DE PROXIMIDAD DE VENCIMIENTO")]
        EdicionParametroDiasProximidadVencimiento,
        [Description("TRANSFERENCIA DE DEPÓSITO")]
        TransferenciaDeDeposito
    }

    public enum TYPE_CONTEXT_DBLOG
    {
        [Description("INGRESO A PLANTA")]
        IngresoAPlanta,
        [Description("INGRESO A PRODUCCION")]
        IngresoAProduccion,
        [Description("PESAJE EN PRODUCCION")]
        PesajeEnProduccion,
        [Description("CONFORMADO DE CAJAS")]
        ConformadoDeCajas,
        [Description("CONFORMADO DE COMBOS")]
        ConformadoDeCombos,
        [Description("FRACCIONAMIENTO")]
        Fraccionamiento,
        [Description("EGRESOS")]
        Egresos,
        [Description("DEVOLUCIONES")]
        Devoluciones,
        [Description("INVENTARIO")]
        Inventario,
        [Description("AJUSTE DE STOCK DE INSUMOS")]
        AjusteStockInsumos,
        [Description("EDICION DE PARAMETROS")]
        EdicionParametros,
        [Description("TRANSFERENCIAS DEPÓSITO")]
        TransferenciasDeposito
    }

}
