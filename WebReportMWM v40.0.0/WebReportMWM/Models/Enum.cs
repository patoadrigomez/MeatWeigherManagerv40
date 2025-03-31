using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebReportMWM.Models
{
    public class Enum
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }

        public enum TYPE_CONTEXT
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
}