USE [MeatWeigherManagerv40]
GO
/****** Object:  StoredProcedure [dbo].[sp_updateDestInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_updateDestInv]
GO
/****** Object:  StoredProcedure [dbo].[sp_TESTcrearRemitoDespachoSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_TESTcrearRemitoDespachoSAC]
GO
/****** Object:  StoredProcedure [dbo].[sp_simulaCapturaInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_simulaCapturaInventario]
GO
/****** Object:  StoredProcedure [dbo].[sp_setDiasProximidadVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_setDiasProximidadVencimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_resetRegistracion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_resetRegistracion]
GO
/****** Object:  StoredProcedure [dbo].[sp_resetInicial]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_resetInicial]
GO
/****** Object:  StoredProcedure [dbo].[sp_repTotalizadoProductosPedidoPendienteVentaPorPreparacion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repTotalizadoProductosPedidoPendienteVentaPorPreparacion]
GO
/****** Object:  StoredProcedure [dbo].[sp_repResultInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repResultInventario]
GO
/****** Object:  StoredProcedure [dbo].[sp_repRendimientoTotalesPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repRendimientoTotalesPorSector]
GO
/****** Object:  StoredProcedure [dbo].[sp_repRendimientoPorTipoDeProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repRendimientoPorTipoDeProducto]
GO
/****** Object:  StoredProcedure [dbo].[sp_repRendimientoPorProductoPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repRendimientoPorProductoPorSector]
GO
/****** Object:  StoredProcedure [dbo].[sp_repProduccionTotalizadoFull]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repProduccionTotalizadoFull]
GO
/****** Object:  StoredProcedure [dbo].[sp_repProduccionDetalladoFull]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repProduccionDetalladoFull]
GO
/****** Object:  StoredProcedure [dbo].[sp_repPiezasProducidasTotalizadosPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repPiezasProducidasTotalizadosPorSector]
GO
/****** Object:  StoredProcedure [dbo].[sp_repPiezasProducidasDetallePorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repPiezasProducidasDetallePorSector]
GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnProduccionTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repInsumosEnProduccionTotalizado]
GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnProduccionDetallado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repInsumosEnProduccionDetallado]
GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnEgresosTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repInsumosEnEgresosTotalizado]
GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnEgresosDetallado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repInsumosEnEgresosDetallado]
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngProduccionTotalizadoPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repIngProduccionTotalizadoPorSector]
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngProduccionDetallePorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repIngProduccionDetallePorSector]
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaTotalizadoXDiaProveedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repIngPlantaTotalizadoXDiaProveedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repIngPlantaTotalizado]
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaDetalle]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repIngPlantaDetalle]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockTotalizado]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockInsumos]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockFullTotalizadoPorDestino]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockFullTotalizadoPorDestino]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalleProximidadVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockDetalleProximidadVencimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalle]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockDetalle]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockContenedoresTotalizadoPorDestino]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repExistenciaEnStockContenedoresTotalizadoPorDestino]
GO
/****** Object:  StoredProcedure [dbo].[sp_repEventosLog]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repEventosLog]
GO
/****** Object:  StoredProcedure [dbo].[sp_repEgresosTotalizadoXDiaCliente]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repEgresosTotalizadoXDiaCliente]
GO
/****** Object:  StoredProcedure [dbo].[sp_repEgresosTotalizadosFullPorProductoPorFecha]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repEgresosTotalizadosFullPorProductoPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[sp_repDevoluciones]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repDevoluciones]
GO
/****** Object:  StoredProcedure [dbo].[sp_repDetalleEgresosFull]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_repDetalleEgresosFull]
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarMovInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_registrarMovInsumo]
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarInsumoProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_registrarInsumoProducto]
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarEgresoPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_registrarEgresoPieza]
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarEgresoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_registrarEgresoContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarDevolucionPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_registrarDevolucionPieza]
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarDevolucionContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_registrarDevolucionContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_insertResulInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_insertResulInventario]
GO
/****** Object:  StoredProcedure [dbo].[sp_insertDbLog]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_insertDbLog]
GO
/****** Object:  StoredProcedure [dbo].[sp_insertCapturaInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_insertCapturaInv]
GO
/****** Object:  StoredProcedure [dbo].[sp_getUnidadesStockInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getUnidadesStockInsumos]
GO
/****** Object:  StoredProcedure [dbo].[sp_getUnidadesStockInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getUnidadesStockInsumo]
GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalizadoProductosPedidoPendienteVentaPorPreparacion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getTotalizadoProductosPedidoPendienteVentaPorPreparacion]
GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalesColeccionINV]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getTotalesColeccionINV]
GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalBultosEnStockVerificados]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getTotalBultosEnStockVerificados]
GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalBultosEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getTotalBultosEnStock]
GO
/****** Object:  StoredProcedure [dbo].[sp_getSaldosEgresosPorFechas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getSaldosEgresosPorFechas]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProveedoresSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProveedoresSAC]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProductosSAC]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosParaContenedores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProductosParaContenedores]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosCombo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProductosCombo]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosCaja]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProductosCaja]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProductos]
GO
/****** Object:  StoredProcedure [dbo].[sp_getProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getProducto]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPiezasEgresadasPorPedido]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getPiezasEgresadasPorPedido]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPesada]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getPesada]
GO
/****** Object:  StoredProcedure [dbo].[sp_getPedidosPendientesVenta]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getPedidosPendientesVenta]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOIsPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetOIsPieza]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOIsLote]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetOIsLote]
GO
/****** Object:  StoredProcedure [dbo].[sp_getOIs]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getOIs]
GO
/****** Object:  StoredProcedure [dbo].[sp_getMovimientos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getMovimientos]
GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_sinSTL_conSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getInv_sinSTL_conSTF]
GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_sinREG]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getInv_sinREG]
GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_piezasFueraCont_ContSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getInv_piezasFueraCont_ContSinStock]
GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_piezasFueraCont_ContEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getInv_piezasFueraCont_ContEnStock]
GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_conSTL_sinSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getInv_conSTL_sinSTF]
GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_BultosEnPedAbiertos_sinEXT]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getInv_BultosEnPedAbiertos_sinEXT]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDiasProximidadVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDiasProximidadVencimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleProductosPedidoPendienteVentaPorPreparacion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleProductosPedidoPendienteVentaPorPreparacion]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleProductosCombo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleProductosCombo]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleProductosCaja]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleProductosCaja]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleInsumosProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleInsumosProducto]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleInsumosPedido]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleInsumosPedido]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleColeccionINV]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleColeccionINV]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleBultosEgresadosPorPedidos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleBultosEgresadosPorPedidos]
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleBultosEgresadosPorPedido]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getDetalleBultosEgresadosPorPedido]
GO
/****** Object:  StoredProcedure [dbo].[sp_getClientesSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_getClientesSAC]
GO
/****** Object:  StoredProcedure [dbo].[sp_generarNuevoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_generarNuevoContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoDesarmarContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoDesarmarContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoContenedorParaEgreso]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoContenedorParaEgreso]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoContenedorParaDevolucion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoContenedorParaDevolucion]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarPiezaEnContenedorCerrado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoBorrarPiezaEnContenedorCerrado]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarPiezaEnContenedorAbierto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoBorrarPiezaEnContenedorAbierto]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoBorrarPieza]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarEgresoDePieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoBorrarEgresoDePieza]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarEgresoDeContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoBorrarEgresoDeContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoAgregarPiezaACombo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoAgregarPiezaACombo]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoAgregarPiezaACaja]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidoAgregarPiezaACaja]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaIngresoAProduccion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidaPiezaParaIngresoAProduccion]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaFraccionar]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidaPiezaParaFraccionar]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaEgreso]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidaPiezaParaEgreso]
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaDevolucion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esValidaPiezaParaDevolucion]
GO
/****** Object:  StoredProcedure [dbo].[sp_esPiezaEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esPiezaEnStock]
GO
/****** Object:  StoredProcedure [dbo].[sp_esPiezaEnProximidadDeVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esPiezaEnProximidadDeVencimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_esPiezaContenedorEgresado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esPiezaContenedorEgresado]
GO
/****** Object:  StoredProcedure [dbo].[sp_esContenedorEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esContenedorEnStock]
GO
/****** Object:  StoredProcedure [dbo].[sp_esContenedorEnProximidadDeVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esContenedorEnProximidadDeVencimiento]
GO
/****** Object:  StoredProcedure [dbo].[sp_esBultoColectadoEnInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_esBultoColectadoEnInventario]
GO
/****** Object:  StoredProcedure [dbo].[sp_desarmarContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_desarmarContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteTodoCapturasInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_deleteTodoCapturasInv]
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteCapturaInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_deleteCapturaInv]
GO
/****** Object:  StoredProcedure [dbo].[sp_crearRemitoDespachoSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_crearRemitoDespachoSAC]
GO
/****** Object:  StoredProcedure [dbo].[sp_calcularFechaVencimientoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_calcularFechaVencimientoContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_borrasMovInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_borrasMovInsumo]
GO
/****** Object:  StoredProcedure [dbo].[sp_borrarPiezaEnContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_borrarPiezaEnContenedor]
GO
/****** Object:  StoredProcedure [dbo].[sp_borrarPiezaEgresada]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_borrarPiezaEgresada]
GO
/****** Object:  StoredProcedure [dbo].[sp_borrarContenedorEgresado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_borrarContenedorEgresado]
GO
/****** Object:  StoredProcedure [dbo].[sp_ajustInv_sinSTL_conSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_ajustInv_sinSTL_conSTF]
GO
/****** Object:  StoredProcedure [dbo].[sp_ajustInv_piezasFueraCont_ContSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_ajustInv_piezasFueraCont_ContSinStock]
GO
/****** Object:  StoredProcedure [dbo].[sp_ajustInv_conSTL_sinSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_ajustInv_conSTL_sinSTF]
GO
/****** Object:  StoredProcedure [dbo].[sp_ajustarStockInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_ajustarStockInsumo]
GO
/****** Object:  StoredProcedure [dbo].[sp_actualizarUnidadesMovInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_actualizarUnidadesMovInsumo]
GO
/****** Object:  StoredProcedure [dbo].[sp_actualizarInsumoProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_actualizarInsumoProducto]
GO
/****** Object:  Index [idx_codprodsac]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_codprodsac] ON [dbo].[Productos]
GO
/****** Object:  Index [idx_fecha_hora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_fecha_hora] ON [dbo].[Pesadas]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_fechahora] ON [dbo].[Pedidos]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_fechahora] ON [dbo].[Egresos]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_fechahora] ON [dbo].[DLP]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_fechahora] ON [dbo].[Devoluciones]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_fechahora] ON [dbo].[Contenedores]
GO
/****** Object:  Table [dbo].[TiposProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[TiposProducto]
GO
/****** Object:  Table [dbo].[TiposContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[TiposContenedor]
GO
/****** Object:  Table [dbo].[TiposBulto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[TiposBulto]
GO
/****** Object:  Table [dbo].[Tipificaciones]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Tipificaciones]
GO
/****** Object:  Table [dbo].[Sectores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Sectores]
GO
/****** Object:  Table [dbo].[resultInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[resultInventario]
GO
/****** Object:  Table [dbo].[Remitos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Remitos]
GO
/****** Object:  Table [dbo].[ProductoInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[ProductoInsumos]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Pedidos]
GO
/****** Object:  Table [dbo].[Parametros]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Parametros]
GO
/****** Object:  Table [dbo].[operadores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[operadores]
GO
/****** Object:  Table [dbo].[OI]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[OI]
GO
/****** Object:  Table [dbo].[MovInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[MovInsumos]
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Inventario]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Facturas]
GO
/****** Object:  Table [dbo].[Etiquetas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Etiquetas]
GO
/****** Object:  Table [dbo].[Destinos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Destinos]
GO
/****** Object:  Table [dbo].[dbLog]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[dbLog]
GO
/****** Object:  Table [dbo].[Combos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Combos]
GO
/****** Object:  Table [dbo].[Cajas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Cajas]
GO
/****** Object:  UserDefinedFunction [dbo].[fPiezasEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fPiezasEnStock]
GO
/****** Object:  Index [idx_idcontenedorIdPesaje]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP INDEX IF EXISTS [idx_idcontenedorIdPesaje] ON [dbo].[ContenedorPiezas] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[ContenedorPiezas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[ContenedorPiezas]
GO
/****** Object:  Table [dbo].[DLP]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[DLP]
GO
/****** Object:  UserDefinedFunction [dbo].[fContenedoresEgresados]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fContenedoresEgresados]
GO
/****** Object:  UserDefinedFunction [dbo].[fBultosEgresados]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fBultosEgresados]
GO
/****** Object:  UserDefinedFunction [dbo].[fContenedoresSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fContenedoresSinStock]
GO
/****** Object:  UserDefinedFunction [dbo].[fContenedoresEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fContenedoresEnStock]
GO
/****** Object:  Table [dbo].[Contenedores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Contenedores]
GO
/****** Object:  UserDefinedFunction [dbo].[fPiezasSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fPiezasSinStock]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Productos]
GO
/****** Object:  Table [dbo].[Pesadas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Pesadas]
GO
/****** Object:  UserDefinedFunction [dbo].[fPiezasEgresadas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fPiezasEgresadas]
GO
/****** Object:  Table [dbo].[Devoluciones]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Devoluciones]
GO
/****** Object:  Table [dbo].[Egresos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Egresos]
GO
/****** Object:  UserDefinedFunction [dbo].[fGenerarFechaVencimientoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP FUNCTION IF EXISTS [dbo].[fGenerarFechaVencimientoContenedor]
GO
/****** Object:  UserDefinedTableType [dbo].[typeTableTmpOI]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP TYPE IF EXISTS [dbo].[typeTableTmpOI]
GO
/****** Object:  User [sa2]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP USER IF EXISTS [sa2]
GO
USE [master]
GO
/****** Object:  Database [MeatWeigherManagerv40]    Script Date: 17/2/2024 9:23:23 a. m. ******/
DROP DATABASE IF EXISTS [MeatWeigherManagerv40]
GO
/****** Object:  Database [MeatWeigherManagerv40]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE DATABASE [MeatWeigherManagerv40]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeatWeigherManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MeatWeigherManagerv40.mdf' , SIZE = 9216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MeatWeigherManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MeatWeigherManagerv40_1.ldf' , SIZE = 47616KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MeatWeigherManagerv40] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeatWeigherManagerv40].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET  MULTI_USER 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeatWeigherManagerv40] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MeatWeigherManagerv40] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MeatWeigherManagerv40] SET QUERY_STORE = OFF
GO
USE [MeatWeigherManagerv40]
GO
/****** Object:  User [sa2]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE USER [sa2] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedTableType [dbo].[typeTableTmpOI]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE TYPE [dbo].[typeTableTmpOI] AS TABLE(
	[idOI] [int] NULL,
	[lote] [date] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[fGenerarFechaVencimientoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 23-2-2021
/*
Description:	
Calcula la fecha de vencimiento que debe tener un contenedor
en funcion a las piezas contenidas. Toma la pieza que tenga la fecha
mas vieja y le suma los dias de vencimiento que corresponden al
articulo de la pieza. 
*/ 
-- =============================================
CREATE FUNCTION [dbo].[fGenerarFechaVencimientoContenedor] 
(	
	@idContenedor int 
)
RETURNS DATETIME

AS
BEGIN 
	DECLARE @fecha_vencimiento datetime
	
	--obtengo la fecha de vencimiento para el nuevo contenedor
	select @fecha_vencimiento=MIN(DATEADD(DAY,prd.diasvencimiento,p.fecha_hora)) 
	from ContenedorPiezas cp
	join Pesadas p on p.id=cp.idpesaje
	join Productos prd on prd.id=p.idproducto
	where idcontenedor = @idContenedor	
	return @fecha_vencimiento	 
END
GO
/****** Object:  Table [dbo].[Egresos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Egresos](
	[idPedido] [int] NOT NULL,
	[idEstacion] [int] NOT NULL,
	[idOperador] [int] NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
	[idpesaje] [int] NOT NULL,
	[idTipoBulto] [char](3) NOT NULL,
 CONSTRAINT [PK_Egresos] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC,
	[idpesaje] ASC,
	[idTipoBulto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Devoluciones]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devoluciones](
	[fecha_hora] [datetime] NOT NULL,
	[idEstacion] [int] NOT NULL,
	[idOperador] [int] NOT NULL,
	[idpesaje] [int] NOT NULL,
	[idpedido] [int] NOT NULL,
	[idTipoBulto] [char](3) NOT NULL,
 CONSTRAINT [PK_Devoluciones_1] PRIMARY KEY CLUSTERED 
(
	[idpesaje] ASC,
	[idpedido] ASC,
	[idTipoBulto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fPiezasEgresadas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha:		9-3-21	
	Obtiene las piezas que se encuentran egresadas a la fecha
	especificada.
	Los egresos se obtienen conciderando las devoluciones.
	Si la fecha = null trae los egresos en la fecha actual
*/
-- =============================================
CREATE FUNCTION [dbo].[fPiezasEgresadas] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT * FROM
	(				
		SELECT
		*,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje)
		FROM egresos
		
		WHERE 
		idTipoBulto = 'PZA'
		AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
	)pzaEgres
	WHERE pzaEgres.max_date=pzaEgres.fecha_hora
	AND NOT EXISTS   
	(
		SELECT * FROM
		(
			SELECT
			idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
			FROM devoluciones
			
			WHERE 
			idTipoBulto='PZA'
			AND idPesaje = pzaEgres.idpesaje
			AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
		)pzaDevol 
		WHERE
		pzaDevol.max_date=pzaDevol.fecha_hora
		AND pzaDevol.max_date >= pzaEgres.max_date
		AND pzaEgres.idPedido=pzaDevol.idpedido
	)
)
GO
/****** Object:  Table [dbo].[Pesadas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pesadas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idOperador] [int] NOT NULL,
	[idEstacion] [int] NOT NULL,
	[idOI] [int] NULL,
	[idDestino] [int] NULL,
	[idSector] [int] NULL,
	[fecha_hora] [datetime] NOT NULL,
	[idproducto] [int] NOT NULL,
	[unidades] [int] NOT NULL,
	[PesoNeto] [float] NOT NULL,
	[PesoTara] [float] NOT NULL,
	[idGrupo] [int] NULL,
	[idPiezaPadre] [int] NULL,
	[PesoRemitido] [float] NULL,
	[numTropa] [int] NULL,
	[idTipificacion] [int] NULL,
 CONSTRAINT [PK_PesajeDesposte] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigoProductoSAC] [varchar](12) NULL,
	[nombre] [varchar](50) NOT NULL,
	[idtipo] [int] NOT NULL,
	[numSenasa] [nvarchar](20) NULL,
	[pesoNetoPredef] [float] NULL,
	[unidadesPredef] [int] NULL,
	[pesoTaraPredef] [float] NULL,
	[diasvencimiento] [int] NULL,
	[esinsumo] [bit] NULL,
	[espesable] [bit] NULL,
	[textAuxL1] [nvarchar](100) NULL,
	[textAuxL2] [nvarchar](100) NULL,
	[nombreL1] [nvarchar](100) NULL,
	[nombreL2] [nvarchar](100) NULL,
	[nombreL3] [nvarchar](100) NULL,
	[nombreL4] [nvarchar](100) NULL,
	[nombreL5] [nvarchar](100) NULL,
	[nombreL6] [nvarchar](100) NULL,
	[rendimientoSTD] [float] NULL,
	[esCombo] [bit] NULL,
	[esCaja] [bit] NULL,
	[esTropa] [bit] NULL,
	[idEtiqueta] [int] NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fPiezasSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha: 14-2-2021
	Obtiene las piezas que se encuentran egresadas a la fecha especificada.
	Si la fecha = null trae las piezas egresadas hasta el momento
*/
-- =============================================
CREATE FUNCTION [dbo].[fPiezasSinStock] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT p.*
	FROM Pesadas p
	JOIN productos prd ON prd.id=p.idproducto
	JOIN fPiezasEgresadas(@aFecha) pe ON pe.idpesaje = p.id
)
GO
/****** Object:  Table [dbo].[Contenedores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contenedores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTipo] [char](3) NULL,
	[idProducto] [int] NULL,
	[idestacion] [int] NOT NULL,
	[idOperador] [int] NULL,
	[iddestino] [int] NULL,
	[fecha_hora] [datetime] NOT NULL,
	[pesoTara] [float] NULL,
	[pesoNeto] [float] NULL,
	[unidades] [int] NULL,
	[fecha_desarmado] [datetime] NULL,
	[fecha_vencimiento] [datetime] NULL,
 CONSTRAINT [PK_Cajas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fContenedoresEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha: 9-2-2021
	Obtiene los contenedores que se encuentran en stock a la fecha especificada.
	Se concidera en la determinacion de stock :
	Entradas a stock por tabla Contenedores,Devoluciones.
	Salidas de stock por tablas Egresos o Contenedores en estado Desarmado,
	Si la fecha = null trae el stock a la fecha actual
*/
-- =============================================
CREATE FUNCTION [dbo].[fContenedoresEnStock] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT * FROM Contenedores
	WHERE
	(@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha) 
	AND (fecha_desarmado is null 	OR 	(@aFecha is not null AND cast(fecha_desarmado as DATE) > @aFecha))
	AND id not in
	(
		SELECT idpesaje FROM
		(				
			SELECT
			idpesaje,idpedido,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje)
			FROM egresos
			
			WHERE 
     		idTipoBulto='CNT'
     		AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
		)tb
		WHERE max_date=fecha_hora
		AND NOT EXISTS   
		(
			SELECT * 
			FROM Devoluciones 
			WHERE
     		idTipoBulto='CNT'
			and idpedido=tb.idPedido 
			and idpesaje=tb.idpesaje 
			and (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
		)
	)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fContenedoresSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha: 15-2-2021
	Obtiene los contenedores que se encuentran en stock a la fecha especificada.
	Se concidera en la determinacion de stock :
	Entradas a stock por tabla Contenedores,Devoluciones.
	Salidas de stock por tablas Egresos o Contenedores en estado Desarmado,
	Si la fecha = null trae el stock a la fecha actual
*/
-- =============================================
CREATE FUNCTION [dbo].[fContenedoresSinStock] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT c.* FROM Contenedores c
	WHERE
	(@aFecha is null OR cast(c.fecha_hora as DATE) <= @aFecha) 
	AND (c.fecha_desarmado is null or (@aFecha is not null AND cast(c.fecha_desarmado as DATE) <= @aFecha))
	AND c.id not in 
		(SELECT id FROM fContenedoresEnStock(@aFecha))
)

GO
/****** Object:  UserDefinedFunction [dbo].[fBultosEgresados]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha:		15-2-21	
	Obtiene los bultos (piezas y contenedores) egresados a la fecha
	especificada.
	Los egresos se obtienen conciderando las devoluciones.
	Si la fecha = null trae los egresos en la fecha actual
*/
-- =============================================
CREATE FUNCTION [dbo].[fBultosEgresados] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT * FROM
	(				
		SELECT
		*,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
		FROM egresos
		
		WHERE 
		(@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
	)pzaEgres
	WHERE pzaEgres.max_date=pzaEgres.fecha_hora
	AND NOT EXISTS   
	(
		SELECT * FROM
		(
			SELECT
			idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
			FROM devoluciones
			
			WHERE 
			idPesaje = pzaEgres.idpesaje
			AND idpedido = pzaEgres.idpedido
			AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
		)pzaDevol 
		WHERE
		pzaDevol.max_date=pzaDevol.fecha_hora
		AND pzaDevol.max_date >= pzaEgres.max_date
	)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fContenedoresEgresados]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha:		15-2-21	
	Obtiene los contenedores que se encuentran egresados a la fecha
	especificada.
	Los egresos se obtienen conciderando las devoluciones.
	Si la fecha = null trae los egresos en la fecha actual
*/
-- =============================================
CREATE FUNCTION [dbo].[fContenedoresEgresados] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT * FROM
	(				
		SELECT
		*,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
		FROM egresos
		
		WHERE 
		idTipoBulto = 'CNT'
		AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
	)tb
	WHERE max_date=fecha_hora
	AND NOT EXISTS   
	(
		SELECT * 
		FROM Devoluciones 
		WHERE
		idTipoBulto = tb.idtipobulto
		AND idpedido=tb.idPedido 
		and idpesaje=tb.idpesaje 
		and (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
	)
)
GO
/****** Object:  Table [dbo].[DLP]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DLP](
	[idoperador] [int] NOT NULL,
	[idestacion] [int] NOT NULL,
	[idpesaje] [int] NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
	[idOI] [int] NULL,
	[LotePadre] [date] NOT NULL,
	[idSector] [int] NULL,
 CONSTRAINT [PK_DLP] PRIMARY KEY CLUSTERED 
(
	[idpesaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContenedorPiezas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContenedorPiezas](
	[idcontenedor] [int] NULL,
	[idTipoContenedor] [char](3) NULL,
	[idpesaje] [int] NOT NULL,
	[idestacion] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [idx_idcontenedorIdPesaje]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE CLUSTERED INDEX [idx_idcontenedorIdPesaje] ON [dbo].[ContenedorPiezas]
(
	[idcontenedor] ASC,
	[idpesaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fPiezasEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fasolo Sergio
/*
	Fecha: 11-3-2021
	Obtiene las piezas que se encuentran en stock a la fecha especificada.
	Se concidera en la determinacion de stock :
	Entradas a stock por tabla pesadas, devoluciones.
	Salidas de stock por tablas DLP ,Egresos,
	y ContenedorPiezas Armados.
	Si la fecha = null trae el stock a la fecha actual
*/
-- =============================================
CREATE FUNCTION [dbo].[fPiezasEnStock] 
(	
	@aFecha date =null 
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT pes.* FROM Pesadas pes
	JOIN productos prd ON pes.idproducto=prd.id
	
	WHERE
	(prd.esInsumo is null or prd.esInsumo=0)
	AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha) 
	--No debe estar en DLP
	AND pes.id not in
	(   
		SELECT idpesaje FROM DLP WHERE (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
	)
	--Tiene devolucion (toma la ultima) y no tiene egreso en fecha posterior a la devolucion y no es parte
	--de un contenedor armado con fecha posterior a la devolucion.
	AND
	(
		EXISTS 
		(
				SELECT * FROM
				(				
					SELECT
					idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
					FROM devoluciones
					
					WHERE 
					idTipoBulto='PZA'
					AND idpesaje = pes.id 
					AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
				)pzaDevol 
				WHERE
				pzaDevol.max_date=pzaDevol.fecha_hora
				AND pzaDevol.idpesaje not in
				(
					select idpesaje from Egresos
					where 
					idTipoBulto='PZA'
					and idpesaje = pzaDevol.idpesaje
					and fecha_hora > pzaDevol.fecha_hora
					AND (@aFecha is null OR cast(fecha_hora as DATE) <= @aFecha)
				)
				AND pzaDevol.idpesaje not in
				(
					SELECT cp.idpesaje 
					FROM Contenedores cn
					JOIN ContenedorPiezas cp on cp.idcontenedor=cn.id
					WHERE 
					cp.idpesaje=pzaDevol.idpesaje
					and cn.fecha_hora > pzaDevol.fecha_hora
					and (@aFecha is null OR cast(cn.fecha_hora as DATE) <= @aFecha)
					AND (fecha_desarmado is null OR	(@aFecha is not null AND cast(fecha_desarmado as DATE) > @aFecha)) 
				)
		)
	
		-- no tiene registro en egresos y no tiene registro en devolucion y no es pieza de contenedor armado). 
		OR 
		(
			pes.id not in
			(
				SELECT e.idpesaje
				FROM egresos e
				WHERE e.idTipoBulto='PZA'
				AND	(@aFecha is null OR cast(e.fecha_hora as DATE) <= @aFecha)
			)
			AND	pes.id not in
			(
				SELECT d.idpesaje FROM Devoluciones d
				WHERE d.idTipoBulto='PZA'
				AND	(@aFecha is null OR cast(d.fecha_hora as DATE) <= @aFecha)
			)
			AND pes.id not in
			(
				SELECT cp.idpesaje FROM ContenedorPiezas cp
				JOIN Contenedores c on c.id = cp.idcontenedor
				WHERE
				(@aFecha is null OR cast(c.fecha_hora as DATE) <= @aFecha)
				AND (fecha_desarmado is null OR	(@aFecha is not null AND cast(fecha_desarmado as DATE) > @aFecha)) 
			)
		)
	)
)
GO
/****** Object:  Table [dbo].[Cajas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cajas](
	[idProductoCaja] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
 CONSTRAINT [PK_Caj] PRIMARY KEY CLUSTERED 
(
	[idProductoCaja] ASC,
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Combos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combos](
	[idProductoCombo] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[unidades] [int] NOT NULL,
	[peso] [float] NOT NULL,
	[toleranciaPeso] [float] NULL,
	[validarUnds] [bit] NULL,
	[validarPeso] [bit] NULL,
 CONSTRAINT [PK_Combos] PRIMARY KEY CLUSTERED 
(
	[idProductoCombo] ASC,
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dbLog]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dbLog](
	[fecha_hora] [datetime] NOT NULL,
	[idEstacion] [int] NOT NULL,
	[idOperador] [int] NOT NULL,
	[evento] [nvarchar](100) NOT NULL,
	[contexto] [nvarchar](100) NOT NULL,
	[detalle] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_dbLog] PRIMARY KEY CLUSTERED 
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Destinos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Destinos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Destinos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etiquetas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etiquetas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](9) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[idTipoBulto] [char](3) NOT NULL,
 CONSTRAINT [PK_Etiquetas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[idoi] [int] NOT NULL,
	[Factura] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[idoi] ASC,
	[Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fechaInicioInventario] [date] NOT NULL,
	[fechaHoraCaptura] [datetime] NOT NULL,
	[idDestino] [int] NOT NULL,
	[idPieza] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Inventario] PRIMARY KEY CLUSTERED 
(
	[fechaInicioInventario] ASC,
	[idPieza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovInsumos](
	[idTipoMov] [char](3) NOT NULL,
	[idTipoProc] [char](3) NOT NULL,
	[idProc] [int] NOT NULL,
	[idPrdInsumo] [int] NOT NULL,
	[unidades] [float] NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
 CONSTRAINT [PK_MovInsumos] PRIMARY KEY CLUSTERED 
(
	[idTipoMov] ASC,
	[idTipoProc] ASC,
	[idProc] ASC,
	[idPrdInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OI]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OI](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idOperador] [int] NOT NULL,
	[idEstacion] [int] NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
	[codigoProveedorSAC] [nvarchar](12) NOT NULL,
	[idCertSanitario] [nvarchar](50) NOT NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Ingreso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[operadores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operadores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
	[pasw] [nvarchar](10) NOT NULL,
	[tipo] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_operadores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametros]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametros](
	[DiasProximidadVencimiento] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_hora] [datetime] NOT NULL,
	[idOperador] [int] NOT NULL,
	[activo] [bit] NULL,
	[CodigoClienteSAC] [varchar](12) NOT NULL,
	[CodigoPedidoSAC] [int] NOT NULL,
	[ComprobantePedidoSAC] [char](12) NULL,
	[TipoPedidoSAC] [varchar](20) NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoInsumos](
	[idProducto] [int] NOT NULL,
	[idInsumoPrimario] [int] NOT NULL,
	[idInsumoSecundario] [int] NOT NULL,
	[unidades] [float] NOT NULL,
	[requiereConfirmacion] [bit] NULL,
 CONSTRAINT [PK_ProductoInsumos] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[idInsumoPrimario] ASC,
	[idInsumoSecundario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Remitos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Remitos](
	[idoi] [int] NOT NULL,
	[remito] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Remitos] PRIMARY KEY CLUSTERED 
(
	[idoi] ASC,
	[remito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[resultInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resultInventario](
	[fechaAjuste] [datetime] NULL,
	[fechaInventario] [datetime] NOT NULL,
	[idPedidoAjuste] [int] NULL,
	[TotalPiezasVerificadasEnStock] [int] NULL,
	[pzas_sinSTLconSTF] [int] NULL,
	[ajustPzas_sinSTLconSTF] [int] NULL,
	[pzas_conSTLsinSTF] [int] NULL,
	[ajustPzas_conSTLsinSTF] [int] NULL,
	[pzas_fueraContenedorConStock] [int] NULL,
	[ajustPzas_fueraContenedorConStock] [int] NULL,
	[TotalCajasVerificadasEnStock] [int] NULL,
	[cjas_sinSTLconSTF] [int] NULL,
	[ajustCjas_sinSTLconSTF] [int] NULL,
	[cjas_conSTLsinSTF] [int] NULL,
	[ajustCjas_conSTLsinSTF] [int] NULL,
	[TotalCombosVerificadosEnStock] [int] NULL,
	[cmbs_sinSTLconSTF] [int] NULL,
	[ajustCmbs_sinSTLconSTF] [int] NULL,
	[cmbs_conSTLsinSTF] [int] NULL,
	[ajustCmbs_conSTLsinSTF] [int] NULL,
	[cantBultosNoExisten] [int] NULL,
	[pzas_fueraContenedorSinStock] [int] NULL,
	[ajustPzas_fueraContenedorSinStock] [int] NULL,
	[cantBultosEnPedidosAbiertos] [int] NULL,
 CONSTRAINT [PK_resultInventario] PRIMARY KEY CLUSTERED 
(
	[fechaInventario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sectores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sectores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Sectores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipificaciones]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipificaciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Tipificaciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposBulto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposBulto](
	[id] [char](3) NOT NULL,
	[nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_TiposBulto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposContenedor](
	[id] [char](3) NOT NULL,
	[Descripcion] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_TiposContenedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposProducto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_TipoProducto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_fechahora] ON [dbo].[Contenedores]
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_fechahora] ON [dbo].[Devoluciones]
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_fechahora] ON [dbo].[DLP]
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_fechahora] ON [dbo].[Egresos]
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idx_fechahora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_fechahora] ON [dbo].[Pedidos]
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idx_fecha_hora]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_fecha_hora] ON [dbo].[Pesadas]
(
	[fecha_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_codprodsac]    Script Date: 17/2/2024 9:23:23 a. m. ******/
CREATE NONCLUSTERED INDEX [idx_codprodsac] ON [dbo].[Productos]
(
	[codigoProductoSAC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_actualizarInsumoProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 01-3-2021
/* Description:
	Realiza la actualizacion del valor de unidades de un insumo vinculado
	a un producto.
	Si los parametros de productoInsumoPrimario = productoInsumoSecundario se 
	estaria actualizando un insumo primario y si si los parametros son 
	distintos se estaria actualizando las unidades de un insumo secundario.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_actualizarInsumoProducto]
	 @idProducto int,@idInsumoPrimario as int, @idInsumoSecundario as int,@unidades as float,@requiereConfirmacion as bit ,@result as bit out
AS
SET NOCOUNT OFF 

BEGIN
		set @result=0
		
		UPDATE ProductoInsumos set unidades = @unidades ,requiereConfirmacion=@requiereConfirmacion
		where
		idProducto=@idProducto 
		and idInsumoPrimario=@idInsumoPrimario
		and idInsumoSecundario=@idInsumoSecundario
		
		SET @result = @@ROWCOUNT
		
END

GO
/****** Object:  StoredProcedure [dbo].[sp_actualizarUnidadesMovInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 01-3-2021
/* Description:
	Realiza la tarea de actualizar el campo unidades de un insumo ya registrado en 
	la tabla de movimientos de insumos.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_actualizarUnidadesMovInsumo]
		@idTipoMov char(3),				--'ING' , 'EGR'
		@idTipoProc char(3),			--'IPL' , 'PZA' , 'CNT' , 'PED'
		@idProc int,					-- identificador unico en el tipo de proceso
		@idPrdInsumo int ,				-- codigo de producto de tipo insumo
		@unidades float ,				-- unidades a actualizar
		@result as bit out				-- = 1 si ok
AS
SET NOCOUNT OFF 

BEGIN
		set @result=0
		UPDATE MovInsumos set unidades = @unidades ,fecha_hora=CURRENT_TIMESTAMP
		WHERE 		
		idTipoMov =@idTipoMov 
		and idTipoProc=@idTipoProc
		and idProc=@idProc
		and idPrdInsumo=@idPrdInsumo

		SET @result = @@ROWCOUNT
		
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ajustarStockInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 18-6-2021
-- Description:	Realiza un ajuste de unidades de stock 
--				para un dado insumo.
-- Parametros:  @idInsumo			Codigo del insumo a modificar.
--				@unidadesEstablecer	Unidades a establecer como stock
--              @result				Indica si la actualizacion fue exitosa
-- =============================================
CREATE PROCEDURE [dbo].[sp_ajustarStockInsumo]
@idPrdInsumo as int,@unidadesEstablecer as float,@result as bit out		-- = 1 si ok
 
AS
BEGIN
	declare @unidadesAjuste float;
	declare @idTipoMov varchar(10);
	declare @idProc as int;
	declare @unidadesEnExistencia as float;
	set @result=0

	exec dbo.sp_getUnidadesStockInsumo @idPrdInsumo,@unidadesEnExistencia out ,@result out
	
	if(@result=1 )
	BEGIN

		if(@unidadesEstablecer <> @unidadesEnExistencia)
		BEGIN
			set @result=0;
	
			if (@unidadesEstablecer - @unidadesEnExistencia) > 0
				set @idTipoMov = 'ING'
			else
				set @idTipoMov = 'EGR'

			set @unidadesAjuste = ABS(@unidadesEstablecer - @unidadesEnExistencia);

			select @idProc= isnull(max(idProc),0)+1 from MovInsumos where idTipoMov=@idTipoMov and idTipoProc='AJU' and idPrdInsumo= @idPrdInsumo;

			exec sp_registrarMovInsumo @idTipoMov,'AJU',@idProc,@idPrdInsumo,@unidadesAjuste,@result out
		END
	END
END


GO
/****** Object:  StoredProcedure [dbo].[sp_ajustInv_conSTL_sinSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
-- Description:	ajusta stock en funcion a inventario para 
--				las piezas,cajas o combos que estan en el stock logico
--              y no estan en el stock fisico.
--				Genera un nuevo pedido para generar los egresos de estos 
--				bultos que se encuentran en este estado
-- =============================================
CREATE PROCEDURE [dbo].[sp_ajustInv_conSTL_sinSTF]
	 @fechainventario datetime, 
	 @idoperador int,
	 @idestacion int,
	 @piezasAjustadas int out,
	 @cajasAjustadas int out,
	 @combosAjustados int out,
	 @newIdPedido int out
AS
BEGIN
	SET NOCOUNT ON;

	declare @idinventario int
	declare @piezasParaAjustar int
	declare @cajasParaAjustar int
	declare @combosParaAjustar int
	
	declare @rt as table
	(
		tipo varchar(20),
		nro int,
		pesoneto float,
		lote varchar(20),
		producto varchar(100)
	)

	set @piezasAjustadas=0
	set @cajasAjustadas=0
	set @combosAjustados=0
	
	set @idinventario= YEAR(@fechainventario) + MONTH(@fechainventario) * 10000 + DAY(@fechainventario)*1000000 
	

	insert into @rt exec sp_getInv_conSTL_sinSTF @fechainventario

	select @piezasParaAjustar = count(*) from @rt where tipo='PIEZA' 
	select @cajasParaAjustar = count(*) from @rt where tipo='CAJA' 
	select @combosParaAjustar = count(*) from @rt where tipo='COMBO' 

	IF ((@piezasParaAjustar is not null AND @piezasParaAjustar > 0)
		OR 
	   (@cajasParaAjustar is not null AND @cajasParaAjustar > 0)
		OR 
	   (@combosParaAjustar is not null AND @combosParaAjustar > 0))
		BEGIN
			--generar nuevo pedido para asociar egresos de bultos
			INSERT INTO Pedidos (fecha_hora,idOperador,activo,CodigoClienteSAC,CodigoPedidoSAC,ComprobantePedidoSAC)
			values(CURRENT_TIMESTAMP,@idoperador,0,'0000',0,'000000000000')
			select @newIdPedido = max(id) from Pedidos 

			INSERT INTO Egresos (idPedido,idEstacion,idOperador,fecha_hora,idpesaje,idtipobulto)
			SELECT	IDPEDIDO=@newIdPedido,
					IDESTACION=@idestacion,
					IDOPERADOR=@idoperador,
					FECHA_HORA=CURRENT_TIMESTAMP,
					nro,
					'PZA'
			FROM @rt WHERE tipo='PIEZA'
			set @piezasAjustadas= @@ROWCOUNT

			INSERT INTO Egresos (idPedido,idEstacion,idOperador,fecha_hora,idpesaje,idtipobulto)
			SELECT	IDPEDIDO=@newIdPedido,
					IDESTACION=@idestacion,
					IDOPERADOR=@idoperador,
					FECHA_HORA=CURRENT_TIMESTAMP,
					nro,
					'CNT'
			FROM @rt WHERE tipo='CAJA'
			set @cajasAjustadas= @@ROWCOUNT

			INSERT INTO Egresos (idPedido,idEstacion,idOperador,fecha_hora,idpesaje,idtipobulto)
			SELECT	IDPEDIDO=@newIdPedido,
					IDESTACION=@idestacion,
					IDOPERADOR=@idoperador,
					FECHA_HORA=CURRENT_TIMESTAMP,
					nro,
					'CNT'
			FROM @rt WHERE tipo='COMBO'
			set @combosAjustados= @@ROWCOUNT

		END

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ajustInv_piezasFueraCont_ContSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
-- Description:	ajusta piezas encontradas fuera de contenedores
--				que no tienen stock.
--				El ajuste consiste en realizar una devolucion de la pieza
--				y utilizar como referencia de pedido (idpedido) al que posee el 
--				contenedor padre en el egreso.
-- =============================================
CREATE PROCEDURE [dbo].[sp_ajustInv_piezasFueraCont_ContSinStock]
	 @fechainventario datetime,@idOperador int,@idEstacion int,@piezasAjustadas int out
AS
BEGIN
	SET NOCOUNT ON;

	declare @idinventario int
	declare @piezasParaAjustar int
	
	DECLARE @tmpTable TABLE
	(
		PIEZA_NRO int,
		PIEZA_LOTE int,
		PIEZA_PESO float,
		PIEZA_PRODUCTO varchar(100),
		CONTENEDOR_TIPO varchar(50),
		CONTENEDOR_NRO int,
		CONTENEDOR_LOTE int,
		CONTENEDOR_PRODUCTO varchar(100),
		IDPEDIDO int null
	);
      
    
	set @piezasAjustadas=0
	
	insert into @tmpTable exec sp_getInv_piezasFueraCont_ContSinStock @fechainventario
	
	set @piezasParaAjustar = @@ROWCOUNT
	
	IF (@piezasParaAjustar is not null AND @piezasParaAjustar > 0)
		BEGIN 
			--inserto devoluciones para las piezas fisicas en stock
			INSERT INTO Devoluciones (idpedido,idOperador,idpesaje,idEstacion,fecha_hora,idtipobulto)
			SELECT tp.IDPEDIDO,@idOperador, tp.PIEZA_NRO,@idEstacion,CURRENT_TIMESTAMP,'PZA'
			FROM @tmpTable tp
			WHERE tp.IDPEDIDO is not null
			and not Exists
			(
				Select d.* from Devoluciones d 
				where 
				d.idpesaje= tp.PIEZA_NRO
				and d.idpedido=tp.IDPEDIDO
				and d.idtipobulto = 'PZA'   
			) 
			set @piezasAjustadas = @@ROWCOUNT
			 
		END
END


GO
/****** Object:  StoredProcedure [dbo].[sp_ajustInv_sinSTL_conSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
-- Description:	ajusta stock en funcion a inventario.
--				Realiza una devolucion de la pieza,cajas o combos
--				que esta egresada y se encuentra fisicamente en stock.
--				Es decir todas las piezas que no tenia stock logico
--				y por inventario tienen stock fisico.
--
--				Tambien realiza un ajuste a todas las piezas que estan el el
--				inventario y tienen registro en la tabla de ingresos a produccion (DLP).
--				En este caso se eliminan las piezas de la tabla DLP.
-- =============================================
CREATE PROCEDURE [dbo].[sp_ajustInv_sinSTL_conSTF]
	 @fechaInventario datetime,
	 @idOperador int,
	 @idEstacion int,
	 @piezasAjustadas int out,
	 @cajasAjustadas int out,
	 @combosAjustados int out,
 	 @piezasAjustadasPorIngProduccion int out

AS
BEGIN
	SET NOCOUNT ON;
	
	declare @piezasParaAjustar int
	declare @cajasParaAjustar int
	declare @combosParaAjustar int
	declare @piezasParaAjustarPorIngProduccion int
	
	declare @rt as table
	(
		tipo varchar(20),
		nro int,
		pesoneto float,
		lote varchar(20),
		producto varchar(100),
		idpedido int 
	)

	set @piezasAjustadas=0
	set @cajasAjustadas=0
	set @combosAjustados=0
	set @piezasAjustadasPorIngProduccion=0

	insert into @rt exec sp_getInv_sinSTL_conSTF @fechainventario

	select @piezasParaAjustar = count(*) from @rt where tipo='PIEZA' 
	select @cajasParaAjustar = count(*) from @rt where tipo='CAJA' 
	select @combosParaAjustar = count(*) from @rt where tipo='COMBO'
	select @piezasParaAjustarPorIngProduccion = count(*) from @rt where tipo='INGPRODUCCION'
	
	IF ((@piezasParaAjustar is not null AND @piezasParaAjustar > 0)
		OR 
	   (@cajasParaAjustar is not null AND @cajasParaAjustar > 0)
		OR 
	   (@combosParaAjustar is not null AND @combosParaAjustar > 0)
		OR 
	   (@piezasParaAjustarPorIngProduccion is not null AND @piezasParaAjustarPorIngProduccion > 0))
		BEGIN
			INSERT INTO Devoluciones (idpedido,idEstacion,idOperador,fecha_hora,idpesaje,idTipoBulto)
			SELECT	IDPEDIDO,
					IDESTACION=@idestacion,
					IDOPERADOR=@idoperador,
					FECHA_HORA=CURRENT_TIMESTAMP,
					nro,
					'PZA'
			FROM @rt WHERE tipo='PIEZA'
			set @piezasAjustadas= @@ROWCOUNT

			INSERT INTO Devoluciones (idpedido,idEstacion,idOperador,fecha_hora,idpesaje,idTipoBulto)
			SELECT	IDPEDIDO,
					IDESTACION=@idestacion,
					IDOPERADOR=@idoperador,
					FECHA_HORA=CURRENT_TIMESTAMP,
					nro,
					'CNT'
			FROM @rt WHERE tipo='CAJA'
			set @cajasAjustadas= @@ROWCOUNT

			INSERT INTO Devoluciones (idpedido,idEstacion,idOperador,fecha_hora,idpesaje,idTipoBulto)
			SELECT	IDPEDIDO,
					IDESTACION=@idestacion,
					IDOPERADOR=@idoperador,
					FECHA_HORA=CURRENT_TIMESTAMP,
					nro,
					'CNT'
			FROM @rt WHERE tipo='COMBO'
			set @combosAjustados= @@ROWCOUNT
			
			Delete DLP WHERE idpesaje in (SELECT nro FROM @rt WHERE tipo='INGPRODUCCION')
			set @piezasAjustadasPorIngProduccion= @@ROWCOUNT
		END
END



GO
/****** Object:  StoredProcedure [dbo].[sp_borrarContenedorEgresado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	borra de los movimientos de egresos un contenedor 
--				correspondiente de un pedido
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_borrarContenedorEgresado]
	 @idContenedor int,@idPedido int ,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	
	delete e
	FROM Egresos e
	WHERE 
	idpesaje=@idContenedor 
	and idTipoBulto='CNT'
	and idPedido=@idPedido
	and not exists
	(
		select * 
		from Devoluciones 
		where 
		idpesaje = e.idpesaje 
		and idpedido=e.idPedido 
		and idTipoBulto=e.idTipoBulto
	)
	set @result = @@ROWCOUNT
	IF @result =0
	   set @error='No fue posible eliminar el contenedor. Verifique que este egresado y que no tenga una devolución. '
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_borrarPiezaEgresada]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	borra de los movimientos de egresos una 
--				pieza egresada para un dado pedido
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_borrarPiezaEgresada]
	 @idPieza int,@idPedido int ,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	
	delete e
	FROM Egresos e
	WHERE 
	idpesaje=@idPieza 
	and idTipoBulto='PZA'
	and idPedido=@idPedido
	and not exists
	(
		select * 
		from Devoluciones 
		where 
		idpesaje = e.idpesaje 
		and idpedido=e.idPedido 
		and idTipoBulto=e.idTipoBulto
	)
	set @result = @@ROWCOUNT
	IF @result =0
	   set @error='No fue posible eliminar la pieza. Verifique que este egresada y que no tenga una devolución. '
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_borrarPiezaEnContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	borrar una pieza contenida en un contenedor 
--				especificando el numero de pieza y tipo (CAJ o CMB)
--				Si tipo='' busca en todos los tipos.
--				Actualiza los acumulados del contenedor.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_borrarPiezaEnContenedor]
	 @idPieza int,@idTipoContenedor varchar(10) ,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	--resta la pieza a eliminar de los acumulados de unidades y peso del contenedor
	UPDATE c 
	set c.PesoNeto = c.pesoNeto-p.pesoNeto ,
		c.unidades = c.unidades - 1,c.fecha_vencimiento= dbo.fGenerarFechaVencimientoContenedor(c.id) 
	FROM PESADAS p, CONTENEDORES c, CONTENEDORPIEZAS cp 
	WHERE 
	cp.idpesaje = @idPieza 
	and c.fecha_desarmado is null
	and p.id = cp.idpesaje 
	and c.id = cp.idcontenedor 
	and (@idTipoContenedor = '' or c.idTipo = @idTipoContenedor)
	
	--elimina la pieza del contenedor
	DELETE cp
	FROM ContenedorPiezas cp
	JOIN Contenedores c on c.id=cp.idcontenedor AND fecha_desarmado is null AND (@idTipoContenedor = '' or idTipo = @idTipoContenedor)
	WHERE cp.idpesaje = @idpieza
	set @result = @@ROWCOUNT
	
	--actualizar fecha de vencimiento del contenedor
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_borrasMovInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 02-3-2021
/* Description:
	Elimina de la tabla de registracion de movimientos de insumos "MovInsumos"
	las registraciones de movimientos que fueron generadas por un proceso de
	ingreso a planta , pesaje de piezas , conformacion de contenedores o preparacion de 
	pedidos.
	La clave del movimiento para eliminar sera idTipoProc y idProc.
	
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_borrasMovInsumo]
		@idTipoProc char(3),			--'IPL' , 'PZA' , 'CNT' , 'PED'
		@idProc int,					-- identificador unico en el tipo de proceso
		@result as bit out				-- = 1 si ok
AS
SET NOCOUNT OFF 

BEGIN
		set @result=0
		DELETE MovInsumos 
		WHERE 		
			idTipoProc=@idTipoProc
			and idProc=@idProc
		SET @result = @@ROWCOUNT
END

GO
/****** Object:  StoredProcedure [dbo].[sp_calcularFechaVencimientoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 23-2-2021
/*
Description:	
Calcula la fecha de vencimiento que debe tener un contenedor
en funcion a las piezas contenidas. Toma la pieza que tenga la fecha
mas vieja y le suma los dias de vencimiento que corresponden al
articulo de la pieza. 
*/ 
-- =============================================
CREATE PROCEDURE [dbo].[sp_calcularFechaVencimientoContenedor]
	 @idContenedor int ,
	 @fecha_vencimiento datetime out
AS
BEGIN
	SET NOCOUNT ON;
	
	--obtengo la fecha de vencimiento para el nuevo contenedor
	select @fecha_vencimiento=MIN(DATEADD(DAY,prd.diasvencimiento,p.fecha_hora)) 
	from ContenedorPiezas cp
	join Pesadas p on p.id=cp.idpesaje
	join Productos prd on prd.id=p.idproducto
	where idcontenedor =@idContenedor	
	 
 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_crearRemitoDespachoSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 03-11-2022
/* Description:

	Realiza la creacion de un remito de venta para el sistema PHYSIS en donde sus items
	corresponderan a las piezas colectadas en un proceso de un pedido de produccion en el sector
	de despachos. El sistema de produccion conoce la clave de un pedido del sistema PHYSIS que es 
	conformada por IdCabecera,IdMovimiento,IdCtaAux. 
	Sumado a esto ya existen registradas piezas bajo esa misma clave de physis en el sistema de produccion. 
	La sumatoria de las registraciones y la informacion sobre el pedido de physis podran generar este proceso de creacion de un remito. 		
	
	En funcion del tipo de PEDIDO a remitir se generan dos posibles tipos de remitos.
	Si el tipo de pedido es PED se genera un remito tipo RECE.
	Si el tipo de pedido es PED2 se genera un remito tipo REM.

	Retorna un string con el resultado de la operacion. Si la cadena comienza con OK la operacion
	fue exitosa , y si no lo fue comenzara con 'ERROR'y a continuacion se sumara una descripcion.
	
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_crearRemitoDespachoSAC]
	 @CodigoPedidoSAC int=0, @CodigoClienteSAC varchar(12)='',@idPedido int=0,@idEstacion int=0,@result varchar(100) output
AS
SET NOCOUNT ON   

BEGIN
declare
	@IdMovimiento     smallint =null,      
	@NroOrden      numeric(4, 0) =1,      
	@Producto      char (12),      
	@IdAuxiPropietario    smallint=null,   
	@IdCtaAuxiPropietario   varchar(12)=null,   
	@Partida      char (10),      
	@UM       char (5),      
	@CantidadUM     numeric(13, 4),      
	@CantidadUMP     numeric(13, 4),      
	@PrecioUnitario    float,      
	@Descuento      numeric(6, 3),      
	@DescuentoProducto      numeric(6, 3),      
	@Descuento1Cabecera      money,      
	@idCDescuento1Cabecera   varchar(12),
	@Descuento2Cabecera      money,      
	@idCDescuento2Cabecera   varchar(12),
	@PrecioUnitarioNeto   float,      
	@PrecioNeto     float,      
	@ImpuestosInternos    money,     
	@FechaVencimiento    datetime,      
	@Observaciones     varchar (2048)='',      
	@AcumulaProducto    bit=0,   
	@PedIdCabecera    int=null,   
	@PedIdMovimiento    numeric(4,0),      
	@PedCantidad     numeric(13, 4),      
	@RemIdCabecera    int = null,   
	@RemIdMovimiento    numeric(4,0)=null,      
	@RemCantidad     numeric(13, 4)=null,      
	@FacIdCabecera    int=null,   
	@FacIdMovimiento    numeric(4,0)=null,      
	@FacCantidad     numeric(13,4)=null,   
	@IdLiquidoProducto    int=null,   
	@IdCabeceraViaje   int=null,   
	@IdMovimientoViaje   numeric(4,0)=null,   
	@IdConexion     int = @@spid*-1-@idEstacion,      
	@FacClase                Char(4) = Null,  
	@ProductoConjunto     char (12) = Null, 
	@NivelConjunto     int = Null, 
	@IdPlanProducto    smallint = 2,   
	@RecuperoKgLimpio    money = 0, 
	@IdDeposito     char(5) = '',  
	@CantidadUMRemesa   numeric(13, 4) = 0, 
	@CantidadUMDif     numeric(13, 4) = 0, 
	@CantidadUMPorc    numeric(13, 4) = 0, 
	@CantidadUMPRemesa    numeric(13, 4) = 0, 
	@CantidadUMPDif    numeric(13, 4) = 0, 
	@CantidadUMPPorc    numeric(13, 4) = 0, 
	@CodCampo     int = Null, 
	@CodLote     int = Null, 
	@NroDTA      varchar(9) = Null, 
	@Estado      int = 0, 
	@FechaEstado    datetime = Null, 
	@PrecioImpuestoInterno  money = 0, 
	@ProductoDesdoblado   bit = 0,  
	@CantidadUMPDestino   numeric(13, 4) = 0,  
	@ITC      money = 0, 
	@PrecioITC     money = 0,  
	@IdItem      smallint = 0, 
	@IdUbicacion    varchar(12) = Null, 
	@PesoBruto     numeric(13,4) = Null,      
	@IdAuxiComprador    smallint = Null,   
	@IdCtaAuxiComprador   varchar(12) = Null,      
	@IdCabeceraServicio   int = Null,   
	@IdMovimientoServicio   int = Null,
	@TasaIVA money =0,
	@IdTipoComprobantePedido char(8) ='PED',
	@CantidadPesoPedido  numeric(13, 4),
	@IdPlanAuxiliar  smallint = 0,
	@IdCtaAuxiliar varchar(12) =''


/******************************************************************************
Limpiar tablas temporales de physis que se destinan a la creacion de remitos.
*******************************************************************************/
exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStock_Tmp_Delete @idconexion
exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStockAuxiliaresTmp_Delete @idconexion

/********************************************************************************
Cursor para recorrer registros de registraciones de piezas en proceso de despacho
bajo la clave idCabecera,IdMovimiento,IdPedido
*********************************************************************************/
declare @PrecioFinal as float = 0

DECLARE cursor_egresos CURSOR
FOR SELECT
 fs.IdCabecera as IDCABECERA,
 fs.idPartida as IDPARTIDA,
 fs.IdMovimiento as IDMOVIMIENTO,
 fp.idProducto as IDPRODUCTO,
 fs.idUM as IDUM,
 fs.CantidadUMPrimaria as CANTIDADPESOPEDIDO,
 fs.PrecioUnitario as PRECIOUNITARIO,
 fc.Descuento1 as DESCUENTO,
 fs.PDescuento as DESCUENTO_PRODUCTO,
 fc.IdCDescuento1 as IDCDESCUENTO1,
 fc.Descuento2 as DESCUENTO2,
 fc.IdCDescuento2 as IDCDESCUENTO2,
 fs.PrecioUnitarioNeto as PRECIOUNITARIONETO,
 fs.PrecioNeto as PRECIONETO,
 fs.ImpuestosInternos as IMPUESTOSINTERNOS,
 fs.FechaVencimiento as FECHAVENCIMIENTO,
 fs.iddeposito as IDDEPOSITO,
 fs.TasaIVA as TASAIVA,
 fs.ProductoDesdoblado as PRODUCTODESDOBLADO,
 fc.IdTipoComprobante as IDTIPOCOMPROBANTE,
 fsa.IdPlanAuxiliar as IDPLANAUXILIAR,
 fsa.IdCtaAuxiliar as IDCTAAUXILIAR,

 (SELECT SUM(UNIDADES) FROM(
	SELECT SUM(pe.unidades) AS UNIDADES 
	FROM Pesadas pe, Egresos eg ,Productos prd2 
	WHERE pe.idproducto = prd2.id 
	and	prd2.codigoProductoSAC = fp.idProducto  
	and eg.idpesaje = pe.id 
	and eg.idPedido = ped.id
	and eg.idTipoBulto = 'PZA'
	UNION
    SELECT count(distinct c.id ) as UNIDADES 
	FROM Contenedores c,Egresos eg ,Productos prd2 
	WHERE
	eg.idPedido = ped.id
	and eg.idTipoBulto = 'CNT'
	and eg.idpesaje = c.id 
	and prd2.id=c.idProducto
	and prd2.codigoProductoSAC=fp.idProducto
	and (c.idTipo='CMB' OR c.idTipo='CAJ'))T
	WHERE T.UNIDADES is not null) as UNDS_COL,  

(SELECT SUM(PESONETO) FROM(
	SELECT distinct pe.id as ID ,pe.PesoNeto AS PESONETO 
	FROM Pesadas pe, Egresos eg ,Productos prd2 
	WHERE pe.idproducto = prd2.id 
	and eg.idTipoBulto = 'PZA'
	and	prd2.codigoProductoSAC = fp.idProducto  
	and eg.idpesaje = pe.id 
	and eg.idPedido = ped.id
	UNION ALL
    SELECT distinct c.id AS ID,c.pesoNeto as PESONETO 
	FROM Contenedores c,Egresos eg ,Productos prd2 
	WHERE
	eg.idPedido = ped.id
	and eg.idTipoBulto = 'CNT'
	and eg.idpesaje = c.id 
	and prd2.id=c.idProducto
	and prd2.codigoProductoSAC=fp.idProducto
	and (c.idTipo='CMB' OR c.idTipo='CAJ'))T
	WHERE T.PESONETO is not null) as PESO_COL  


 FROM pedidos as ped 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
									ped.CodigoPedidoSAC = fc.IdCabecera 
									and fc.idTipoComprobante = ped.TipoPedidoSAC 
									and fc.idPpal=1  
 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
									ped.CodigoPedidoSAC = fs.IdCabecera 
									and fs.idTipoComprobante = fc.idTipoComprobante
									and fs.idPpal=1  
 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  fs.idProducto = fp.idProducto and fp.Imputable = 1 and fp.idPlanProducto=2 

 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACStockAuxiliares fsa ON  
									fs.IdCabecera = fsa.IdCabecera 
									and fs.IdMovimiento = fsa.IdMovimiento
									and fs.IdSubMovimiento = fsa.IdSubMovimiento
 
 JOIN Productos prd ON 	fp.idProducto = prd.codigoProductoSAC and fs.idProducto=prd.codigoProductoSAC 

 WHERE ped.id = @idPedido
 GROUP BY ped.id,fs.IdCabecera,fs.idPartida, fs.IdMovimiento,fp.idProducto ,fs.idUM,fs.CantidadUMPrimaria,fs.PrecioUnitario,fc.Descuento1,fs.PDescuento,
 fc.IdCDescuento1,fc.Descuento2,fc.IdCDescuento2,fs.PrecioUnitarioNeto,
 fs.PrecioNeto,fs.ImpuestosInternos,fs.FechaVencimiento,fs.iddeposito,fs.TasaIVA,fs.ProductoDesdoblado,fc.IdTipoComprobante,fsa.IdPlanAuxiliar,fsa.IdCtaAuxiliar


OPEN cursor_egresos

FETCH NEXT FROM cursor_egresos INTO 
    @PedidCabecera, 
	@Partida,
	@PedIdMovimiento,
	@Producto,
	@UM,
	@CantidadPesoPedido,
	@PrecioUnitario,      
	@Descuento,      
	@DescuentoProducto,      
	@idCDescuento1Cabecera,
	@Descuento2Cabecera,
	@idCDescuento2Cabecera,
	@PrecioUnitarioNeto,      
	@PrecioNeto,      
	@ImpuestosInternos,     
	@FechaVencimiento,      
	@IdDeposito,
	@TasaIVA,
	@ProductoDesdoblado,
	@IdTipoComprobantePedido,
	@IdPlanAuxiliar,
	@IdCtaAuxiliar,
	@CantidadUM,
	@CantidadUMP



IF @@FETCH_STATUS > 0
BEGIN
		CLOSE cursor_egresos;
		DEALLOCATE cursor_egresos;
		SET @result ='ERROR: LA CONSULTA SOBRE REGISTRACIONES DE PIEZAS NO OBTUVO RESULTADOS'
		return 0
END
ELSE
BEGIN
	WHILE @@FETCH_STATUS = 0
		BEGIN
			
			set @CantidadPesoPedido = isnull(@CantidadPesoPedido,0)

			set @Descuento1Cabecera=@Descuento

			IF IsNull(@DescuentoProducto,0) > 0
				set @Descuento = @DescuentoProducto;

			set @PedCantidad= @CantidadUM*-1
			set @PrecioUnitarioNeto = @PrecioUnitario - (@PrecioUnitario * (IsNull(@Descuento,0)/100))

			set @PrecioNeto = @PrecioUnitarioNeto * @CantidadUMP
			
			--solo si el tipo de pedido es 1 ('PED') aplico IVA
			if 	@IdTipoComprobantePedido='PED'
				set @PrecioFinal = @PrecioFinal + (@PrecioNeto * ((IsNull(@TasaIVA,10.5)/100)+1))
			else
				set @PrecioFinal = @PrecioFinal + @PrecioNeto

			SELECT @PedidCabecera as IDCABECERA, 
				@Partida as PARTIDA,
				@PedIdMovimiento as IDMOVIMIENTO,
				@Producto as IDPRODUCTO,
				@UM as UM,
				@CantidadPesoPedido as CANTIDADPESOPEDIDO,
				@CantidadUM as CANUND,
				@CantidadUMP as CANTPESO,
				@Descuento as DESCUENTO,
				@Descuento1Cabecera as DESCUENTO1CABECERA,
				@Descuento2Cabecera as DESCUENTO2CABECERA,
				@idCDescuento1Cabecera as IDCDESCUENTO1CABECERA,
				@idCDescuento2Cabecera as IDCDESCUENTO2CABECERA,
				@PrecioUnitario as PRECIOUNITARIO,
				@PrecioUnitarioNeto as PRECIOUNITARIONETO,
				@PrecioNeto as PRECIONETO,
				@PrecioFinal as PRECIOFINAL,
				@ProductoDesdoblado as PRODUCTODESDOBLADO,
				@IdTipoComprobantePedido as IDTIPOCOMPROBANTE,
				@IdPlanAuxiliar as IDPLANAUXILIAR,
				@IdCtaAuxiliar as IDCTAAUXILIAR

			set @CantidadUM = isnull(@CantidadUM,0)
			set @CantidadUMP = isnull(@CantidadUMP,0)
			
			exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStock_Tmp_Insert    
				@IdMovimiento,      
				@NroOrden,      
				@Producto,      
				@IdAuxiPropietario,   
				@IdCtaAuxiPropietario,   
				@Partida,      
				@UM,      
				@CantidadUM,      
				@CantidadUMP,      
				@PrecioUnitario,      
				@Descuento,      
				@PrecioUnitarioNeto,      
				@PrecioNeto,      
				@ImpuestosInternos,     
				@FechaVencimiento,      
				@Observaciones,      
				@AcumulaProducto,   
				@PedIdCabecera,   
				@PedIdMovimiento,      
				@PedCantidad,      
				@RemIdCabecera,   
				@RemIdMovimiento,      
				@RemCantidad,      
				@FacIdCabecera,   
				@FacIdMovimiento,      
				@FacCantidad,   
				@IdLiquidoProducto,   
				@IdCabeceraViaje,   
				@IdMovimientoViaje,   
				@IdConexion,      
				@FacClase,  
				@ProductoConjunto, 
				@NivelConjunto, 
				@IdPlanProducto,   
				@RecuperoKgLimpio, 
				@IdDeposito,  
				@CantidadUMRemesa, 
				@CantidadUMDif, 
				@CantidadUMPorc, 
				@CantidadUMPRemesa, 
				@CantidadUMPDif, 
				@CantidadUMPPorc, 
				@CodCampo, 
				@CodLote, 
				@NroDTA , 
				@Estado , 
				@FechaEstado, 
				@PrecioImpuestoInterno, 
				@ProductoDesdoblado,  
				@CantidadUMPDestino,  
				@ITC, 
				@PrecioITC  

			exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStockAuxiliaresTmp_Insert
				@PedIdCabecera,
				@IdMovimiento,
				@NroOrden,
				@IdPlanAuxiliar,
				@IdCtaAuxiliar,
				@IdConexion
		
			FETCH NEXT FROM cursor_egresos INTO 
				@PedidCabecera, 
				@Partida,
				@PedIdMovimiento,
				@Producto,
				@UM,
				@CantidadPesoPedido,
				@PrecioUnitario,      
				@Descuento,   
				@DescuentoProducto,      
				@idCDescuento1Cabecera,
				@Descuento2Cabecera,
				@idCDescuento2Cabecera,
				@PrecioUnitarioNeto,      
				@PrecioNeto,      
				@ImpuestosInternos,     
				@FechaVencimiento,      
				@IdDeposito,
				@TasaIVA,
				@ProductoDesdoblado,
				@IdTipoComprobantePedido,
				@IdPlanAuxiliar,
				@IdCtaAuxiliar,
				@CantidadUM,
				@CantidadUMP
			
			SET @NroOrden = @NroOrden+1
		END;
	   
	CLOSE cursor_egresos;
	DEALLOCATE cursor_egresos;


	/************************************************************************
	Preparación antes de llamar al ST de creacion de remito 
	*************************************************************************/

declare 
	@ABMD        char(1)='A', /* Alta */   
	@IdCabecera  int =0,       
    @IdEjercicio   smallint,          
    @Sucursal        char(4)='0000',      
    @Fecha        datetime,      
    @IdTipoComprobante   char(8)= 'RECE',      
    @Numero        varchar(12),      
    @IdAuxi        smallint=1,      
    @IdCtaAuxi        varchar(12)=@CodigoClienteSAC,      
    @IdTipoDocumento   varchar(5),      
    @NumeroDocumento   varchar(12),      
    @NombreTercero   varchar(40),      
    @CategoriaIVA   varchar(2),      
    @Observaciones2   varchar(500)='',        
    @IddDeposito   char(05),        
    @IdaDeposito   char(05)=null,        
    @IdAuxiListaPrecios   smallint=null,   
    @IdReagListaPrecios   smallint=null,   
    @IdListaPrecios   char(12)=null,        
    @IdReagVendedor   smallint=null,   
    @IdVendedor        char(12)=null,   
    @IdReagTransporte   smallint=null,        
    @IdTransporte   char(12)=null,   
    @IdReagDescuento   smallint=null,        
    @IdDescuento1   char(12)=@idCDescuento1Cabecera,        
    @Descuento1        money=@Descuento1Cabecera,        
    @IdDescuento2   char(12)=@idCDescuento2Cabecera,        
    @Descuento2        money=@Descuento2Cabecera,      
    @IdReagObservaciones   smallint=null,     
    @IdCodObservaciones   char(12)=null,        
    @Referencia    char(20),   
    @IdReagCondPago   smallint,   
    @IdCondPago    char(12),        
    @FormaCosteo   char(5)='PPPP',    
    @Alcance    tinyint=3,   
    @ModoCarga    tinyint=1,   
    @IdMoneda    char(5)=null,   
    @Serie    tinyint=null,   
    @TasaCambio    float=1,   
    @ImporteTotal   money=@PrecioFinal,       
    @IdUsuario    smallint=1000, /*editar este valor cuando en Physis se cree un usuario para el sistema de produccion*/         
    @CodCampania    smallint = 0, 
    @Planta    Bit= 1, 
    @FechaExt        datetime = null,          
    @IdTipoComprobanteExt   char(8) = null,          
    @NumeroExt        varchar(12) = null, 
    @FechaVencimientoCAI   datetime = null, 
    @IdPais               smallint = 1,                
    @IdProvincia          smallint = 2,  
    @IdCabeceraRepl    int = 0, 
    @NumeroCAI            varchar(14) = Null, 
    @NroGuia             varchar(14) = Null, 
	@IdIdioma     Int = Null, 
	@IdMonedaPrint    Char(5) = Null,        
    @SeriePrint     TinyInt = Null,        
    @TasaPrint     float = Null,      
	@ModoFacturacion int = cast(isnull(@ProductoDesdoblado,0) as int)+1  

	--requerido por physis
	exec PHY_WINSIFAC_01130_01_00001_0100.dbo.spFacGrabarTemporalesReferenciado 
	@CodigoPedidoSAC,
	@IdUsuario,
	@IdConexion


	--establecer como fecha del nuevo remito la fecha del dia siguiente al dia actual y coloca la hora 00:00:00.
	set @Fecha = DATEADD(HOUR,0, CAST(CAST(DATEADD(day,1,getdate()) AS DATE) AS DATETIME))
	--obtener IdEjercicio
	select @IdEjercicio = idejercicio from PHY_WINSIFAC_01130_01_00001_0100.dbo.ejercicios where @Fecha between fechainicio and fechacierre

	--obtener propiedades del pedido original
	select	@idTipoDocumento=idTipoDocumento,
			@NumeroDocumento=NumeroDocumento,
			@NombreTercero=	NombreTercero,
			@CategoriaIva = CategoriaIva,
			@Observaciones2= Observaciones,
			@iddDeposito=iddDeposito,
			@Referencia=Referencia,
			@IdReagCondPago=IdReagCondPago,
			@IdCondPago=IdCondPago,
			@IdReagTransporte=IdReagTransporte,
			@IdTransporte=IdTransporte,
			@IdReagListaPrecios=IdReagListaPrecios,
			@IdAuxiListaPrecios=IdAuxiListaPrecios,
			@IdListaPrecios=IdListaPrecios,
			@CategoriaIVA = CategoriaIVA,
			@idReagVendedor = IdReagVendedor,
			@IdVendedor=IdVendedor

			from PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras 
			where idCabecera=@CodigoPedidoSAC 
			and idAuxi=1 and idCtaAuxi=@CodigoClienteSAC	   
	 
	--Aplicar tipo de remito en funcion del tipo de pedido.
	--Si pedido es PED el tipo de remito a crear debe ser RECE.
	--Si el pedido es PED2 el tipo de remito  a crear debe ser REM.
	--RECE Modo facturacion 1 , REM modo facturacion 2 
	
	set @IdTipoComprobante = case @IdTipoComprobantePedido 
									when 'PED' then 'RECE'
									when 'PED2' then 'REM'
									else 'RECE' end;
	
	--generar numero de comprobante en funcion del tipo de remito
	IF @IdTipoComprobante = 'RECE'
		BEGIN 
			Select @Numero='0005'+Right('00000000' + CONVERT(varchar(8), numero+1), 8) 
			from PHY_WINSIFAC_01130_01_00001_0100.dbo.NumeracionPrefijos  
			where idnumerador = 72 and idprefijo = 5 and idEjercicio = @IdEjercicio
		END
	ELSE
		BEGIN 
			Select @Numero='0009'+Right('00000000' + CONVERT(varchar(8), numero+1), 8) 
			from PHY_WINSIFAC_01130_01_00001_0100.dbo.NumeracionPrefijos  
			where idnumerador = 42 and idprefijo = 9 and idEjercicio = @IdEjercicio
		END
	
	--chequear que el transportista no este vacio 
	IF @IdTransporte=''
	BEGIN
	set @IdTransporte =NULL
	SET @IdReagTransporte=NULL
	END
	
	select @IdTipoComprobanteExt = tc.idtipocomprobanteafip from 
	PHY_WINSIFAC_01130_01_00001_0100.dbo.TiposComprobante tc 
	where tc.IdTipoComprobante = @IdTipoComprobante
	
	--preparo tabla de resultados
	declare @tableResult table(NumeroDefinitivo varchar(12), NewIdCabecera int)

	--ejecutar sp que crea el remito y salvo los resultados en la tabla variable
	insert into @tableResult exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStock_Insert_Update_Rem 
	@ABMD,          
	@IdCabecera,   
	@IdEjercicio,          
	@Sucursal,      
	@Fecha,      
	@IdTipoComprobante,      
	@Numero,      
	@IdAuxi,      
	@IdCtaAuxi,      
	@IdTipoDocumento,      
	@NumeroDocumento,      
	@NombreTercero,      
	@CategoriaIVA,      
	@Observaciones2,        
	@IddDeposito,        
	@IdaDeposito,        
	@IdAuxiListaPrecios,   
	@IdReagListaPrecios,   
	@IdListaPrecios,        
	@IdReagVendedor,   
	@IdVendedor,   
	@IdReagTransporte,        
	@IdTransporte,   
	@IdReagDescuento,        
	@IdDescuento1, 
	@Descuento1,   
	@IdDescuento2, 
	@Descuento2,   
	@IdReagObservaciones,     
	@IdCodObservaciones,        
	@Referencia,   
	@IdReagCondPago,   
	@IdCondPago,        
	@FormaCosteo,    
	@Alcance,   
	@ModoCarga,   
	@IdMoneda,   
	@Serie,   
	@TasaCambio,   
	@ImporteTotal ,       
	@IdUsuario,          
	@IdConexion,  
	@CodCampania, 
	@Planta, 
	@FechaExt,          
	@IdTipoComprobanteExt,          
	@NumeroExt, 
	@FechaVencimientoCAI, 
	@IdPais,                
	@IdProvincia,  
	@IdCabeceraRepl, 
	@NumeroCAI, 
	@NroGuia, 
	@IdIdioma, 
	@IdMonedaPrint,        
	@SeriePrint,        
	@TasaPrint      

	select @IdCabecera= NewIdCabecera from @tableResult
	if(@IdCabecera is not null and @IdCabecera <> 0)
		begin
			set @result='OK - Creación de Remito Exitosa'
			--llama a este SP para que physis coloque una copia del remito creado en la base de datos que corresponde 1 o 2
			exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACCopiaComprobante @IdCabecera
			return 1  
		end
	else
		begin
			set @result='Error al generar el Remito en Physis'
			return 0  
		end
END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteCapturaInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP borra una pieza o combo capturado
--				para un inventario desde la tabla
--				Inventario para una dada fecha 
--				de inventario dada y numero de pieza
-- =============================================
CREATE PROCEDURE [dbo].[sp_deleteCapturaInv]
	 @fechaInventario datetime,@idPieza varchar(12)
AS
BEGIN
	SET NOCOUNT ON;
	DELETE Inventario WHERE @fechaInventario = fechaInicioInventario and @idPieza=idPieza
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_deleteTodoCapturasInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP borra todas las capturas registradas
--              para una dada fecha de inventario 
-- =============================================
CREATE PROCEDURE [dbo].[sp_deleteTodoCapturasInv]
	 @fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;
	DELETE Inventario WHERE @fechaInventario = fechaInicioInventario
	DELETE resultInventario WHERE @fechaInventario = fechaInventario
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_desarmarContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	coloca a un contenedor en estado DESARMADO
-- =============================================
CREATE PROCEDURE [dbo].[sp_desarmarContenedor]
	 @idContenedor int,@result bit =0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	update Contenedores set fecha_desarmado = CURRENT_TIMESTAMP where id=@idContenedor		
	set @result = @@ROWCOUNT
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esBultoColectadoEnInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP que verifica si una pieza o contenedor
--				para un dado inventario ya esta registrada. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_esBultoColectadoEnInventario]
	 @fechaInventario datetime ,@idPieza varchar(12),@esColectada bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If Exists(select * from Inventario where idPieza=@idPieza and cast(@fechaInventario as DATE) = fechaInicioInventario)
	set @esColectada=1
	else
	set @esColectada=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esContenedorEnProximidadDeVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si un contenedor se encuentra en proximidad de Vencimineto
--				segun los dias de proximidad de vencimineto definidos en parametro global.
-- =============================================
CREATE PROCEDURE [dbo].[sp_esContenedorEnProximidadDeVencimiento]
	 @idContenedor int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @diasProximidadVencimiento int=0;
	SELECT @diasProximidadVencimiento=DiasProximidadVencimiento FROM Parametros


	If Exists
	(
		select * 
		from contenedores c
		join Productos prd on prd.id=c.idproducto
		where
		c.id = @idContenedor
		and DATEDIFF(day,getdate(),c.fecha_vencimiento) <= @diasProximidadVencimiento
	)		 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esContenedorEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si un contenedor se encuentra en stock
-- =============================================
CREATE PROCEDURE [dbo].[sp_esContenedorEnStock]
	 @idContenedor int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If Exists
	(
		select * 
		from fContenedoresEnStock(null) where id = @idContenedor
	)		 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esPiezaContenedorEgresado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si una pieza indicada es parte
--				de un contenedor que se encuentra egresado.
-- =============================================
CREATE PROCEDURE [dbo].[sp_esPiezaContenedorEgresado]
	 @idPieza int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If Exists
	(
		select * 
		from fContenedoresEgresados(null) ce
		JOIN ContenedorPiezas cp on cp.idcontenedor = ce.idpesaje AND cp.idpesaje = @idPieza
	)		 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esPiezaEnProximidadDeVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si una pieza se encuentra en proximidad de Vencimineto
--				segun los dias de proximidad de vencimineto definidos en parametro global.
-- =============================================
CREATE PROCEDURE [dbo].[sp_esPiezaEnProximidadDeVencimiento]
	 @idPieza int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @diasProximidadVencimiento int=0;
	SELECT @diasProximidadVencimiento=DiasProximidadVencimiento FROM Parametros


	If Exists
	(
		select * 
		from pesadas p
		join Productos prd on prd.id=p.idproducto
		where
		p.id = @idPieza
		and DATEDIFF(day,getdate(),DATEADD(day,prd.diasvencimiento,p.fecha_hora)) <= @diasProximidadVencimiento
	)		 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esPiezaEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si una pieza se encuentra en stock
-- =============================================
CREATE PROCEDURE [dbo].[sp_esPiezaEnStock]
	 @idPieza int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If Exists
	(
		select * 
		from fPiezasEnStock(null) where id = @idPieza
	)		 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaDevolucion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica que una pieza sea valida para generar una devolucion
--				Se debe cumplir que la pieza no este en stock.
--				Si es parte de un contenedor que el mismo no tenga stock.
--				Y que el pedido del egreso de la pieza de forma individual o 
--				por contenedor este cerrado.
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidaPiezaParaDevolucion]
	 @idPieza int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If Exists
	(
		select idpesaje as PIEZA 
		from fPiezasEgresadas(null)
		JOIN Pedidos ped on ped.id = idPedido AND ped.activo =0
		where
		idpesaje = @idPieza
	)	OR
	Exists
	(
		select cp.idpesaje as PIEZA 
		from fContenedoresEgresados(null) ce
		JOIN ContenedorPiezas cp on cp.idcontenedor = ce.idpesaje AND cp.idpesaje = @idPieza		
		JOIN Pedidos ped on ce.idPedido=ped.id AND ped.activo=0
	)	 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaEgreso]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 7-8-2022
-- Description:	verifica que una pieza sea valida para generar un Egreso
--				en un pedido especifico
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidaPiezaParaEgreso]
	 @idPieza int,@idPedido int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;

	declare @diasProximidadVencimiento int=0;
	SELECT @diasProximidadVencimiento=DiasProximidadVencimiento FROM Parametros

	set @result = 0
	set @error=''
	If Exists
	(
		select * 
		from egresos 
		where
		idpesaje = @idPieza
		and idTipoBulto='PZA'
		and idPedido=@idPedido
	)	
		set @error = 'La Pieza ya esta Egresada!!!'
	else If not Exists
	(
		select * 
		from fPiezasEnStock(null)
		where id = @idPieza
	)	
		set @error = 'La Pieza no esta en Stock!!!'

	else If not Exists
	(  -- verifica si el producto de la pieza a egresar esta dentro de los productos pedidos.
		SELECT fs.idProducto

		FROM Pedidos ped 

		JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON
		ped.CodigoPedidoSAC=fc.IdCabecera and ped.CodigoClienteSAC=fc.IdCtaAuxi
		 
		JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
			fc.IdCabecera = fs.IdCabecera
			and fc.IdAuxi=fs.IdAuxi
			and fc.IdCtaAuxi=fs.IdCtaAuxi 
			and fs.idTipoComprobante = fc.IdTipoComprobante 

		JOIN Pesadas p on p.id = @idPieza
		 
		JOIN productos as prd ON  
			prd.codigoProductoSAC = fs.idProducto AND prd.id = p.idproducto 

		WHERE 
		(fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')  
		and fc.idPpal=1
		AND (prd.esCombo is null or prd.esCombo=0) 
		AND (prd.esCaja is null or prd.esCaja=0)
		and ped.id=@idPedido
	)	
		set @error = 'El producto colectado no es requerido en este pedido !!'

	else If Exists
	(  -- verifica si la pieza posee fecha de vencimineto proxima a vencer.
		SELECT * 
		FROM pesadas p
		join Productos prd on prd.id=p.idproducto
		WHERE
		p.id = @idPieza
		and DATEDIFF(day,getdate(),DATEADD(day,prd.diasvencimiento,p.fecha_hora)) <= @diasProximidadVencimiento		
	)	
		set @error = 'La Pieza colectada se encuentra proxima a vencer !!'

	else
		set @result=1
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaFraccionar]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica que una pieza sea valida para ser fraccionada
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidaPiezaParaFraccionar]
	 @idPieza int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0

	If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from dlp
		where
		idpesaje = @idPieza
	)
		set @error = 'La Pieza tiene ingresado a Producción!!!'
	
	else If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from egresos
		where
		idpesaje = @idPieza
		and idTipoBulto='PZA'
	)OR
	Exists
	(
		select cp.idpesaje as PIEZA 
		from fContenedoresEgresados(null) ce
		JOIN ContenedorPiezas cp on cp.idcontenedor = ce.idpesaje AND cp.idpesaje = @idPieza		
		JOIN Pedidos ped on ce.idPedido=ped.id AND ped.activo=0
	)	 
		set @error = 'La Pieza ya participo de un Egreso!!!'
	
	else If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from devoluciones
		where
		idpesaje = @idPieza
		and idTipoBulto='PZA'
	)
		set @error = 'La Pieza ya participo de una Devolución!!!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidaPiezaParaIngresoAProduccion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica que una pieza sea valida para ser fraccionada
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidaPiezaParaIngresoAProduccion]
	 @idPieza int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0

	If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from dlp
		where
		idpesaje = @idPieza
	)
		set @error = 'La Pieza ya esta ingresada a Producción!!!'
	
	else If not Exists
	(
		--si fue parte de un egreso 
		select * 
		from fPiezasEnStock(null)
		where
		id = @idPieza
	)	 
		set @error = 'La Pieza no esta en Stock!!!'

	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoAgregarPiezaACaja]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si es valido agregar una pieza a
--				un contenedor caja de un articulo determinado.
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoAgregarPiezaACaja]
	 @idPieza int,@idProductoCaja int ,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0
	If 	not Exists
	(
		select * from pesadas where id = @idPieza 
	)
		set @error = 'La Pieza Colectada no Existe !!!'
	else If Exists
	(
		select * from dlp where idpesaje = @idPieza 
	)
		set @error = 'La Pieza Colectada esta registrada en Ingreso a Producción !!!'
	else If Exists
	(
		--pieza en proceso de un contenedor o ya registrada
		select idcontenedor 
		from ContenedorPiezas 
		where
		idcontenedor is null
		and idpesaje = @idPieza
	)
		set @error = 'La Pieza Colectada ya fue registrada !!!'
	else If not Exists
	(
		select * 
		from fPiezasEnStock(null) 
		where id = @idPieza
	)
		set @error = 'La Pieza Colectada no esta en Stock !!!'
	else If not Exists
	(
		SELECT p.id as ID
		FROM cajas c
		JOIN Pesadas p ON p.idproducto = c.idProducto and p.id = @idPieza
		WHERE c.idProductoCaja = @idProductoCaja
	)
		set @error = 'La pieza es de un producto que no pertenece a la Caja !!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoAgregarPiezaACombo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si es valido agregar una pieza a
--				un contenedor combo de un articulo determinado.
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoAgregarPiezaACombo]
	 @idPieza int,@idProductoCombo int ,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0
	If 	not Exists
	(
		select * from pesadas where id = @idPieza 
	)
		set @error = 'La Pieza Colectada no Existe !!!'
	else If Exists
	(
		select * from dlp where idpesaje = @idPieza 
	)
		set @error = 'La Pieza Colectada esta registrada en Ingreso a Producción !!!'
	else If Exists
	(
		--pieza en proceso de un contenedor
		select idcontenedor 
		from ContenedorPiezas 
		where
		idcontenedor is null
		and idpesaje = @idPieza
	)
		set @error = 'La Pieza Colectada ya fue registrada !!!'
	else If not Exists
	(
		select * 
		from fPiezasEnStock(null) 
		where id = @idPieza
	)
		set @error = 'La Pieza Colectada no esta en Stock !!!'
	else If not Exists
	(
		SELECT p.id as ID
		FROM Combos c
		JOIN Pesadas p ON p.idproducto = c.idProducto and p.id = @idPieza
		WHERE c.idProductoCombo = @idProductoCombo
	)
		set @error = 'La pieza es de un producto que no pertenece al Combo !!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarEgresoDeContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si es valido borrar un registro 
--				de egreso de un contenedor para un pedido dado.
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoBorrarEgresoDeContenedor]
	 @idContenedor int,@idPedido int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0
	If 	not Exists
	(
		SELECT *
		FROM Egresos e
		WHERE 
		idpesaje=@idContenedor 
		and idTipoBulto='CNT'
		and idPedido=@idPedido
		and not exists
		(
			select * 
			from Devoluciones 
			where 
			idpesaje = e.idpesaje 
			and idpedido=e.idPedido 
			and idTipoBulto=e.idTipoBulto
		)
	)
		set @error = 'El Contenedor no esta egresado o ya tiene una devolucion !!!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarEgresoDePieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si es valido borrar un registro 
--				de egreso de una pieza para un pedido dado.
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoBorrarEgresoDePieza]
	 @idPieza int,@idPedido int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0
	If 	not Exists
	(
		SELECT *
		FROM Egresos e
		WHERE 
		idpesaje=@idPieza 
		and idTipoBulto='PZA'
		and idPedido=@idPedido
		and not exists
		(
			select * 
			from Devoluciones 
			where 
			idpesaje = e.idpesaje 
			and idpedido=e.idPedido 
			and idTipoBulto=e.idTipoBulto
		)
	)
		set @error = 'La Pieza no esta egresada o ya tiene una devolucion !!!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica que una pieza puede ser eliminada del registro de pesajes
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoBorrarPieza]
	 @idPieza int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0

	If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from dlp
		where
		idpesaje = @idPieza
	)
		set @error = 'La Pieza tiene ingresado a Producción!!!'
	
	else If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from egresos
		where
		idpesaje = @idPieza
		and idTipoBulto='PZA'
	)OR
	Exists
	(
		select cp.idpesaje as PIEZA 
		from fContenedoresEgresados(null) ce
		JOIN ContenedorPiezas cp on cp.idcontenedor = ce.idpesaje AND cp.idpesaje = @idPieza		
		JOIN Pedidos ped on ce.idPedido=ped.id AND ped.activo=0
	)	 
		set @error = 'La Pieza ya participo de un Egreso!!!'
	
	else If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from devoluciones
		where
		idpesaje = @idPieza
		and idTipoBulto='PZA'
	)
		set @error = 'La Pieza ya participo de una Devolución!!!'
	else If Exists
	(
		--si fue parte de un egreso 
		select idpesaje as PIEZA 
		from ContenedorPiezas
		where
		idpesaje = @idPieza
	)
		set @error = 'La Pieza es o fué parte de un contenedor!!!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarPiezaEnContenedorAbierto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si es valido borrar una pieza 
--				en contenedor abierto.
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoBorrarPiezaEnContenedorAbierto]
	 @idPieza int,@idEstacion int ,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0
	If 	not Exists
	(
		select * from pesadas where id = @idPieza 
	)
		set @error = 'La Pieza Colectada no Existe !!!'
	else If Exists
	(
		select * from dlp where idpesaje = @idPieza 
	)
		set @error = 'La Pieza Colectada esta registrada en Ingreso a Producción !!!'
	else If not Exists
	(
		SELECT *
		FROM CONTENEDORPIEZAS
		WHERE 
		idcontenedor is null 
		AND idpesaje =@idPieza
		AND idestacion=@idEstacion
	)
		set @error = 'La Pieza Colectada no esta registrada en el Contenedor!!!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoBorrarPiezaEnContenedorCerrado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si es valido borrar una pieza 
--				en contenedor cerrado.
--				retorna @result=1 si es valido o 
--				@result=0 si no es valido y @error con el detalle  
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoBorrarPiezaEnContenedorCerrado]
	 @idPieza int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	set @result = 0
	If 	not Exists
	(
		select * from pesadas where id = @idPieza 
	)
		set @error = 'La Pieza Colectada no Existe !!!'
	else If Exists
	(
		select * from dlp where idpesaje = @idPieza 
	)
		set @error = 'La Pieza Colectada esta registrada en Ingreso a Producción !!!'
	else If not Exists
	(
		SELECT *
		FROM Contenedores c
		JOIN CONTENEDORPIEZAS cp on cp.idcontenedor=c.id
		WHERE 
		cp.idpesaje =@idPieza
		and c.fecha_desarmado is null
	)
		set @error = 'La Pieza Colectada no esta registrada en el Contenedor!!!'

	else If Exists
	(
		SELECT COUNT(*)
		FROM CONTENEDORPIEZAS cp
		join Contenedores c on c.id=cp.idcontenedor
		WHERE 
		c.id in (select idcontenedor from ContenedorPiezas where idpesaje=@idPieza)
		and c.fecha_desarmado is null
		having COUNT(*)=1

	)
		set @error = 'No puede eliminar la unica pieza que posee el contenedor !!!'
	else
		set @result=1
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoContenedorParaDevolucion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica que una contenedor sea valido para generar una devolucion
--				Se debe cumplir que el contenedor este egresado y que el pedido 
--				del egreso este cerrado.
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoContenedorParaDevolucion]
	 @idContenedor int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If 	Exists
	(
		select ce.idpesaje as CONTENEDOR 
		from fContenedoresEgresados(null) ce
		JOIN Pedidos ped on ce.idPedido=ped.id AND ped.activo=0
		WHERE ce.idpesaje = @idContenedor
	)	 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoContenedorParaEgreso]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 04-8-2021
-- Description:	verifica que una contenedor sea valido para generar un Egreso
--				en un pedido especifico
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoContenedorParaEgreso]
	 @idContenedor int,@idPedido int,@result bit=0 out,@error varchar(100) ='' out
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @diasProximidadVencimiento int=0;
	SELECT @diasProximidadVencimiento=DiasProximidadVencimiento FROM Parametros

	set @result = 0

	If Exists
	(
		select * 
		from egresos 
		where
		idpesaje = @idContenedor
		and idTipoBulto='CNT'
		and idPedido=@idPedido
	)	
		set @error = 'El Contenedor ya esta Egresado!!!'
	else If not Exists
	(
		select * 
		from fContenedoresEnStock(null)
		where id = @idContenedor
	)	
		set @error = 'El Contenedor no esta en Stock!!!'

	else If not Exists
	(  -- verifica si el producto del contenedor a egresar esta dentro de los productos pedidos.
		SELECT fs.idProducto

		FROM Pedidos ped 

		JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON
		ped.CodigoPedidoSAC=fc.IdCabecera and ped.CodigoClienteSAC=fc.IdCtaAuxi
		 
		JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
			fc.IdCabecera = fs.IdCabecera
			and fc.IdAuxi=fs.IdAuxi
			and fc.IdCtaAuxi=fs.IdCtaAuxi 
			and fs.idTipoComprobante = fc.idTipoComprobante 

		JOIN Contenedores c on c.id = @idContenedor
		 
		JOIN productos as prd ON  
			prd.codigoProductoSAC = fs.idProducto AND prd.id = c.idproducto 

		WHERE 
		(fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2') 
		and fc.idPpal=1
		AND (prd.esCombo = 1 or prd.esCaja =1)
		and ped.id=@idPedido

	)	
		set @error = 'El producto colectado no es requerido en este pedido !!'

	else If Exists
	(  -- verifica si el contenedor posee fecha de vencimineto proxima a vencer.
		SELECT * 
		FROM Contenedores c
		join Productos prd on prd.id=c.idproducto
		WHERE
		c.id = @idContenedor
		and DATEDIFF(day,getdate(),c.fecha_vencimiento) <= @diasProximidadVencimiento		
	)	
		set @error = 'El Contenedor colectado se encuentra proximo a vencer !!'

	else
		set @result=1
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_esValidoDesarmarContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	verifica si un contenedor puede ser Desarmado
--				
--				Se debe cumplir que el contenedor este en stock. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_esValidoDesarmarContenedor]
	 @idContenedor int,@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	
	If 	Exists
	(
		select ce.id as CONTENEDOR 
		from fContenedoresEnStock(null) ce
		WHERE ce.id = @idContenedor
	)	 
	set @result=1
	else
	set @result=0
		
END


GO
/****** Object:  StoredProcedure [dbo].[sp_generarNuevoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 23-2-2021
/*
Description:	
Genera un nuevo contenedor y relaciona las piezas que 
debe contener 
*/ 
-- =============================================
CREATE PROCEDURE [dbo].[sp_generarNuevoContenedor]
	 @idEstacion int ,
	 @pesoTara float,
	 @pesoNeto float,
	 @idDestino int,
	 @idTipo char(3) ,
	 @idProducto int,
	 @unidades int,
	 @idOperador int,
	 @idNuevoContenedor int out,
	 @fecha_vencimiento datetime out
AS
BEGIN
	SET NOCOUNT ON;
	
	--obtengo la fecha de vencimiento para el nuevo contenedor
	select @fecha_vencimiento=MIN(DATEADD(DAY,prd.diasvencimiento,p.fecha_hora)) 
	from ContenedorPiezas cp
	join Pesadas p on p.id=cp.idpesaje
	join Productos prd on prd.id=p.idproducto
	where idcontenedor is null and cp.idestacion=@idEstacion and cp.idTipoContenedor=@idTipo	
	 
	
	INSERT INTO CONTENEDORES
	 (fecha_hora,pesoTara,pesoNeto,idestacion,iddestino,idTipo,idProducto,unidades,idOperador,fecha_vencimiento)
	 
	 values 
	 (CURRENT_TIMESTAMP,
	 @pesoTara,
	 @pesoNeto,
	 @idEstacion,
	 @idDestino,
	 @idTipo,
	 @idProducto,
	 @unidades,
	 @idOperador,
	 @fecha_vencimiento)
	 
	 select @idNuevoContenedor= MAX(id) from Contenedores where idestacion = @idEstacion
	 
	 update ContenedorPiezas set idcontenedor = @idNuevoContenedor
	 where idcontenedor is null and idestacion=@idEstacion
	 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getClientesSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 26-8-2020
-- Description:	SP obtiene los clientes del sistema administrativo
--              contable. Admite parametro de filtro por aproximacion
--              de nombre. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getClientesSAC]
	 @nameFilter nvarchar(100)=''
AS
BEGIN


SELECT idCtaAuxi as CODIGO , nombre as NOMBRE 
FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi 
WHERE (@nameFilter = '' OR UPPER(nombre) like '%'+UPPER(@nameFilter)+'%' ) AND idPpal=1 AND idAuxi=1 AND Imputable=1

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleBultosEgresadosPorPedido]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 11-02-2021
-- Description:	SP obtiene el detalle de piezas,
--                 cajasy combos egresados para un
--				   dado pedido. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleBultosEgresadosPorPedido]
@idPedido int
 
AS
BEGIN
SELECT
	TIPO='PIEZA',
	p.id as NRO,
	('(' + REPLACE(codigoProductoSAC, ' ', '') +') ' + prd.nombre) as PRODUCTO,
	(REPLACE(STR(DAY(p.fecha_hora),2,0),' ','0') + REPLACE(STR(MONTH(p.fecha_hora),2,0),' ','0') + REPLACE(STR(YEAR(p.fecha_hora),4,0),' ','0'))as LOTE, 
	p.unidades as UNDS , 
	p.pesoNeto as NETO,  
	e.fecha_hora as FECHA_EGRESO,
	ope.nombre as OPERADOR  
FROM EGRESOS as e
	JOIN Pedidos ped ON ped.id = e.idPedido 
	LEFT OUTER JOIN operadores ope ON e.idOperador = ope.id  
	LEFT OUTER JOIN PESADAS as p ON p.id = e.idpesaje  
	LEFT OUTER JOIN Productos as prd ON p.idproducto = prd.id  
WHERE
	e.idpedido = @idPedido
	and e.idtipobulto ='PZA' 

UNION

SELECT distinct
	tc.descripcion as TIPO,
	c.id as NRO,
	('(' + REPLACE(codigoProductoSAC, ' ', '') +') ' + prd.nombre) as PRODUCTO,
	(REPLACE(STR(DAY(c.fecha_hora),2,0),' ','0') + REPLACE(STR(MONTH(c.fecha_hora),2,0),' ','0') + REPLACE(STR(YEAR(c.fecha_hora),4,0),' ','0'))as LOTE, 
	c.unidades as UNDS , 
	c.pesoNeto as NETO,  
	e.fecha_hora as FECHA_EGRESO,
	ope.nombre as OPERADOR  
FROM contenedores as c  
	JOIN ContenedorPiezas cp ON cp.idcontenedor = c.id  
	JOIN Egresos as e ON e.idpesaje=c.id 
	JOIN Pedidos ped ON ped.id = e.idPedido 
	JOIN TiposContenedor tc on tc.id=c.idTipo
	LEFT OUTER JOIN operadores ope ON e.idOperador = ope.id  
	LEFT OUTER JOIN Productos as prd ON prd.id = c.idProducto  
WHERE 
	e.idpedido = @idPedido
	and e.idTipoBulto='CNT'

END


GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleBultosEgresadosPorPedidos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2021
-- Description:	SP obtiene el detalle de piezas,
--              cajas y combos egresados para 
--              todos los pedidos. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleBultosEgresadosPorPedidos]
 
AS
BEGIN
SELECT
	e.idpedido as IDPEDIDO,
	TIPO='PIEZA',
	p.id as NRO,
	('(' + REPLACE(codigoProductoSAC, ' ', '') +') ' + prd.nombre) as PRODUCTO,
	(REPLACE(STR(DAY(p.fecha_hora),2,0),' ','0') + REPLACE(STR(MONTH(p.fecha_hora),2,0),' ','0') + REPLACE(STR(YEAR(p.fecha_hora),4,0),' ','0'))as LOTE, 
	p.unidades as UNDS , 
	p.pesoNeto as NETO,  
	e.fecha_hora as FECHA_EGRESO,
	ope.nombre as OPERADOR  
FROM egresos as e  
	LEFT OUTER JOIN operadores ope ON e.idOperador = ope.id  
	LEFT OUTER JOIN PESADAS as p ON p.id = e.idpesaje  
	LEFT OUTER JOIN Productos as prd ON p.idproducto = prd.id  
WHERE	e.idTipoBulto = 'PZA'

UNION

SELECT distinct
	e.idpedido as IDPEDIDO,
	tc.Descripcion as TIPO,
	c.id as NRO,
	('(' + REPLACE(codigoProductoSAC, ' ', '') +') ' + prd.nombre) as PRODUCTO,
	(REPLACE(STR(DAY(c.fecha_hora),2,0),' ','0') + REPLACE(STR(MONTH(c.fecha_hora),2,0),' ','0') + REPLACE(STR(YEAR(c.fecha_hora),4,0),' ','0'))as LOTE, 
	c.unidades as UNDS , 
	c.pesoNeto as NETO,  
	e.fecha_hora as FECHA_EGRESO,
	ope.nombre as OPERADOR  
FROM contenedores as c  
	JOIN ContenedorPiezas cp ON cp.idcontenedor = c.id  
	JOIN egresos as e ON e.idpesaje=c.id 
	LEFT OUTER JOIN operadores ope ON e.idOperador = ope.id  
	LEFT OUTER JOIN TiposContenedor tc on tc.id=c.idTipo
	LEFT OUTER JOIN Productos as prd ON prd.id = c.idProducto  
WHERE 
	e.idTipoBulto='CNT'

END


GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleColeccionINV]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP obtiene todas las piezas o contenedores
--				colectados para un dado inventario.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleColeccionINV]
	 @fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
	TIPO='PIEZA',
	p.id as NRO,
	inv.fechaHoraCaptura as FECHA,	
	prd.nombre as PRODUCTO,
	d.nombre as UBICACION,
	p.PesoNeto as PESO
	
	FROM Inventario inv
	LEFT OUTER JOIN Pesadas p on isnumeric(idpieza)=1 and p.id = CONVERT(INT, replace(inv.idPieza,'A','')) 
	LEFT OUTER JOIN Productos prd on prd.id = p.idproducto 
	LEFT OUTER JOIN Destinos d on d.id = inv.idDestino
	WHERE isnumeric(inv.idPieza)=1 and  inv.fechaInicioInventario = CAST(@fechaInventario as DATE)
	
	UNION 
	
	SELECT distinct
	tc.Descripcion as TIPO,
	c.id as NRO,
	inv.fechaHoraCaptura as FECHA,	
	prd.nombre as PRODUCTO,
	d.nombre as UBICACION,
	c.pesoNeto as PESO
			
	FROM Inventario inv
	INNER JOIN Contenedores c ON isnumeric(inv.idpieza)=0 and c.id = CONVERT(INT, replace(inv.idPieza,'A',''))
	INNER JOIN TiposContenedor tc ON tc.id=c.idTipo
	INNER JOIN Productos prd on prd.id = c.idproducto
	LEFT OUTER JOIN Destinos d on d.id = inv.idDestino
	WHERE inv.fechaInicioInventario = CAST(@fechaInventario as DATE)
	
	ORDER BY FECHA desc
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleInsumosPedido]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 25-2-2021
-- Description:	SP obtiene los insumos que estan registrados para 
--              un dado pedido en la tabla de movimientos de insumos.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleInsumosPedido]
@idPedido int=0

AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.escombo as ESCOMBO,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
mins.unidades as UNIDADES_INSUMO,
mins.idProc as IDPEDIDO
 

FROM MovInsumos mins
JOIN  Productos p on p.id=mins.idPrdInsumo and p.esinsumo=1  
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
WHERE mins.idProc=@idPedido
and mins.idTipoMov='EGR' 
and mins.idTipoProc='PED'

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleInsumosProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 25-2-2021
-- Description:	SP obtiene los insumos que integran 
--              un producto. un primario y los secundarios 
--              si los posee.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleInsumosProducto]
@idProducto int=0

AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.escombo as ESCOMBO,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
pins.unidades as UNIDADES_INSUMO,
pins.idInsumoPrimario as IDINSUMO_PRIMARIO,
pins.requiereConfirmacion as REQUIERE_CONFIRMACION
 

FROM ProductoInsumos pins
JOIN  Productos p on p.id=pins.idInsumoSecundario and p.esinsumo=1  
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
WHERE pins.idProducto=@idProducto

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleProductosCaja]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-11-2022
-- Description:	SP obtiene los productos que integran 
--              un producto del tipo caja.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleProductosCaja]
@idProductoCaja int=0

AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.estropa as ESTROPA,
p.escombo as ESCOMBO,
p.escaja as ESCAJA,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA

FROM Productos p
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
JOIN Cajas c ON c.idProductoCaja=@idProductoCaja AND c.idProducto=p.id


END
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleProductosCombo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 26-8-2020
-- Description:	SP obtiene los productos que integran 
--              un producto del tipo combo.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleProductosCombo]
@idProductoCombo int=0

AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.estropa as ESTROPA,
p.escombo as ESCOMBO,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA,
c.unidades as UNIDADES_COMBO,
c.peso  as PESO_COMBO,
c.validarUnds as VALIDAR_UNDS,
c.validarPeso as VALIDAR_PESO,
c.toleranciaPeso as TOLERANCIA_PESO
FROM Productos p
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
JOIN Combos c ON c.idProductoCombo=@idProductoCombo AND c.idProducto=p.id


END
GO
/****** Object:  StoredProcedure [dbo].[sp_getDetalleProductosPedidoPendienteVentaPorPreparacion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-08-22
/*
Description:
Obtiene todos los items (piezas o contenedores) de compocision
de todos los pedidos que sean de la misma fecha de preparacion
especificada.Expresando la cantidad pedida , la cantidad preparada y el resto.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDetalleProductosPedidoPendienteVentaPorPreparacion]
@fechaPreparacion date,@comprobantePedido varchar(12)='',@exactComprobante bit=false
 
AS
BEGIN
SELECT  
fc.numero as Comprobante,
fc.nombreTercero as Cliente,
fs.idProducto as CodigoSAC,
fp.Descripcion as ProductoSAC,
fs.Detalle as Observacion,
sum(fs.cantidad) as Unds_PED,
SUM(pes.unidades) as Unds_PREP,
(sum(fs.cantidad) -SUM(pes.unidades)) as Unds_REST,
sum(fs.CantidadUMPrimaria) as Peso_PED,
SUM(pes.PesoNeto) as Peso_PREP,
(sum(fs.CantidadUMPrimaria) -SUM(pes.PesoNeto)) as Peso_REST

FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.V_CantidadesPedidosPendientes as pp
JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	pp.PedIdCabecera = fc.IdCabecera and 
	pp.idAuxi=fc.idAuxi and 
	pp.idCtaAuxi=fc.idCtaAuxi and
	(fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')  
	and fc.idPpal=1

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
	fc.IdCabecera = fs.IdCabecera 
	and pp.PedIdMovimiento = fs.IdMovimiento 
	and fs.idTipoComprobante = fc.IdTipoComprobante 

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  
	fs.idProducto = fp.idProducto and 
	fp.Imputable = 1 and
	fp.idPlanProducto=2 

JOIN productos as prd ON  
	prd.codigoProductoSAC = fp.idProducto 
left outer join Pedidos ped ON ped.CodigoPedidoSAC=fc.IdCabecera
left outer join Egresos e ON e.idpedido = ped.id and e.idTipoBulto = 'PZA'
left outer join Pesadas pes ON pes.id = e.idpesaje and pes.idproducto=prd.id and prd.codigoProductoSAC=fp.idProducto
WHERE 
pp.idAuxi=1
AND (prd.esCombo is null or prd.esCombo=0) 
AND (prd.esCaja is null or prd.esCaja=0)
AND cast(fs.fechaVencimiento as DATE) = @fechaPreparacion
AND (@comprobantePedido = '' or  (@exactComprobante=1 and fc.numero=@comprobantePedido) or (@exactComprobante=0 and fc.numero like '%'+@comprobantePedido+'%'))
AND (not exists (select * from Pedidos where CodigoClienteSAC=fc.idCtaAuxi and CodigoPedidoSAC=fc.IdCabecera) or (ped.activo=1 or ped.activo is null))
GROUP BY fc.numero,fc.nombreTercero,fs.idProducto,fp.Descripcion,fs.detalle

UNION

SELECT  
fc.numero as Comprobante,
fc.nombreTercero as Cliente,
fs.idProducto as CodigoSAC,
fp.Descripcion as ProductoSAC,
fs.Detalle as Observacion,
sum(fs.cantidad) as Unds_PED,
COUNT(cnt.id) as Unds_PREP,
(sum(fs.cantidad) -COUNT(cnt.id)) as Unds_REST,
sum(fs.CantidadUMPrimaria) as Peso_PED,
SUM(cnt.PesoNeto) as Peso_PREP,
(sum(fs.CantidadUMPrimaria) -SUM(cnt.PesoNeto)) as Peso_REST

FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.V_CantidadesPedidosPendientes as pp
JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	pp.PedIdCabecera = fc.IdCabecera and 
	pp.idAuxi=fc.idAuxi and 
	pp.idCtaAuxi=fc.idCtaAuxi and
	(fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')  
	and fc.idPpal=1

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
	fc.IdCabecera = fs.IdCabecera and 
	pp.PedIdMovimiento = fs.IdMovimiento and 
	fs.idTipoComprobante = fc.IdTipoComprobante 

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  fs.idProducto = fp.idProducto and fp.Imputable = 1 and fp.idPlanProducto=2 
JOIN productos as prd ON  prd.codigoProductoSAC = fp.idProducto 
left outer join Pedidos ped ON ped.CodigoPedidoSAC=fc.IdCabecera
left outer join Egresos e ON e.idpedido = ped.id and e.idTipoBulto = 'CNT'
left outer join Contenedores cnt ON cnt.id = e.idpesaje and cnt.idproducto=prd.id 
WHERE 
pp.idAuxi=1
AND (prd.esCombo =1 or prd.esCaja=1)
AND cast(fs.fechaVencimiento as DATE) = @fechaPreparacion
AND (@comprobantePedido = '' or  (@exactComprobante=1 and fc.numero=@comprobantePedido) or (@exactComprobante=0 and fc.numero like '%'+@comprobantePedido+'%'))
AND (not exists (select * from Pedidos where CodigoClienteSAC=fc.idCtaAuxi and CodigoPedidoSAC=fc.IdCabecera) or (ped.activo=1 or ped.activo is null))
GROUP BY fc.numero,fc.nombreTercero,fs.idProducto,fp.Descripcion,fs.detalle


ORDER BY
Comprobante,ProductoSAC

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getDiasProximidadVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 4-8-2021
-- Description:	Obtiene los dia de proximidad de vencimiento desde el 
--				parametro global Parametros.DiasproximidadVencimiento. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getDiasProximidadVencimiento]
	 AS
BEGIN
	SET NOCOUNT ON;
	declare @dias as int;

	SELECT @dias=DiasProximidadVencimiento FROM Parametros
	return @dias
END

GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_BultosEnPedAbiertos_sinEXT]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 09-3-2021
-- Description:	
/*
	Obtiene en funcion a un inventario los bultos que estan
	en pedidos abiertos y no se encuentran fisicamente en el
	inventario.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_getInv_BultosEnPedAbiertos_sinEXT]
		@fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
	TIPO='PIEZA',
	pes.id as NRO,
	pes.PesoNeto as PESO_NETO,
	(YEAR(pes.fecha_hora) + MONTH(pes.fecha_hora) * 10000 + DAY(pes.fecha_hora)*1000000)as LOTE
	,prd.nombre as PRODUCTO,
	ped.id as PEDIDO,
	ped.ComprobantePedidoSAC as COMPROBANTE
	
	FROM egresos egr
	INNER JOIN Pesadas pes on pes.id=egr.idpesaje
	INNER JOIN Pedidos ped on ped.id = egr.idPedido
	INNER JOIN Productos prd on prd.id = pes.idproducto 
	WHERE 
	egr.idTipoBulto='PZA'
	and (ped.activo is null or ped.activo=1)
	and pes.id not in (SELECT	CONVERT(INT, replace(idPieza,'A','') )  
					 FROM Inventario 
					 WHERE isnumeric(idpieza)=1 AND fechaInicioInventario = CAST(@fechaInventario as DATE)) 
	
	UNION 
	
	SELECT 
	tc.Descripcion as TIPO,
	cn.id as NRO,
	cn.pesoNeto as PESO_NETO,
	(YEAR(cn.fecha_hora) + MONTH(cn.fecha_hora) * 10000 + DAY(cn.fecha_hora)*1000000)as LOTE
	,prd.nombre as PRODUCTO,
	ped.id as PEDIDO,
	ped.ComprobantePedidoSAC as COMPROBANTE

			
	FROM Contenedores cn
	INNER JOIN Egresos egr on egr.idpesaje = cn.id and egr.idTipoBulto='CNT'
	INNER JOIN Pedidos ped on ped.id = egr.idPedido
	INNER JOIN TiposContenedor tc ON tc.id=cn.idTipo
	INNER JOIN Productos prd on prd.id = cn.idproducto
	WHERE 
	(ped.activo is null or ped.activo=1)
	and cn.id not in (select  CONVERT(INT, replace(inv.idPieza,'A','') ) 
					 FROM Inventario inv 
					 WHERE isnumeric(inv.idpieza)=0 AND inv.fechaInicioInventario = CAST(@fechaInventario as DATE)) 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_conSTL_sinSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 09-2-2021
-- Description:	SP obtiene en funcion a un inventario 
--				los bultos que estan en el stock logico
--              y no estan en el stock fisico.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getInv_conSTL_sinSTF]
		@fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
	TIPO='PIEZA',
	p.id as NRO,
	p.PesoNeto as PESO_NETO,
	(YEAR(p.fecha_hora) + MONTH(p.fecha_hora) * 10000 + DAY(p.fecha_hora)*1000000)as LOTE
	,prd.nombre as PRODUCTO
	
	FROM fPiezasEnStock(null) p
	INNER JOIN Productos prd on prd.id = p.idproducto 
	WHERE 
	p.id not in (SELECT	CONVERT(INT, replace(idPieza,'A','') )  
					 FROM Inventario 
					 WHERE isnumeric(idpieza)=1 AND fechaInicioInventario = CAST(@fechaInventario as DATE)) 
	
	UNION 
	
	SELECT 
	tc.Descripcion as TIPO,
	c.id as NRO,
	c.pesoNeto as PESO_NETO,
	(YEAR(c.fecha_hora) + MONTH(c.fecha_hora) * 10000 + DAY(c.fecha_hora)*1000000)as LOTE
	,prd.nombre as PRODUCTO
			
	FROM fContenedoresEnStock(null) c
	INNER JOIN TiposContenedor tc ON tc.id=c.idTipo
	INNER JOIN Productos prd on prd.id = c.idproducto
	WHERE 
	c.id not in (select  CONVERT(INT, replace(inv.idPieza,'A','') ) 
					 FROM Inventario inv 
					 WHERE isnumeric(inv.idpieza)=0 AND inv.fechaInicioInventario = CAST(@fechaInventario as DATE)) 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_piezasFueraCont_ContEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 12-2-2021
-- Description:	SP obtienen las piezas que se encontraron en 
--				el inventario fuera de contenedores
--				que se encuentran en stock.
--
-- =============================================
CREATE PROCEDURE [dbo].[sp_getInv_piezasFueraCont_ContEnStock]
		@fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
	p.id as PIEZA_NRO,
	(YEAR(p.fecha_hora) + MONTH(p.fecha_hora) * 10000 + DAY(p.fecha_hora)*1000000)as PIEZA_LOTE,
	p.PesoNeto as PIEZA_PESO,
	prd.nombre as PIEZA_PRODUCTO,
	tc.Descripcion as CONTENEDOR_TIPO,
	c.id as CONTENEDOR_NRO,
	(YEAR(c.fecha_hora) + MONTH(c.fecha_hora) * 10000 + DAY(c.fecha_hora)*1000000)as CONTENEDOR_LOTE,
	prdc.nombre as CONTENEDOR_PRODUCTO
	
	FROM fContenedoresEnStock(null) c
	INNER JOIN ContenedorPiezas cp on cp.idcontenedor = c.id  
	INNER JOIN pesadas p on cp.idpesaje = p.id
	INNER JOIN Productos prdc on prdc.id = c.idproducto
	INNER JOIN Productos prd on prd.id = p.idproducto AND (prd.esinsumo = 0 or prd.esinsumo is null)
	INNER JOIN TiposContenedor tc on tc.id = c.idTipo
	INNER JOIN Inventario i on (isnumeric(i.idpieza)=1 AND CONVERT(INT, idPieza)=p.id) 		   
	WHERE 
	i.fechaInicioInventario = CAST(@fechaInventario as DATE) 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_piezasFueraCont_ContSinStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-02-2021
-- Description:	SP obtienen las piezas que se encontraron en 
--				el inventario fuera de contenedores
--				que se encuentran sin stock.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getInv_piezasFueraCont_ContSinStock]
		@fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
	p.id as PIEZA_NRO,
	(YEAR(p.fecha_hora) + MONTH(p.fecha_hora) * 10000 + DAY(p.fecha_hora)*1000000)as PIEZA_LOTE,
	p.PesoNeto as PIEZA_PESO,
	prd.nombre as PIEZA_PRODUCTO,
	tc.Descripcion as CONTENEDOR_TIPO,
	c.id as CONTENEDOR_NRO,
	(YEAR(c.fecha_hora) + MONTH(c.fecha_hora) * 10000 + DAY(c.fecha_hora)*1000000)as CONTENEDOR_LOTE,
	prdc.nombre as CONTENEDOR_PRODUCTO,
	e.idPedido as IDPEDIDO
	
	FROM fContenedoresSinStock(null) c
	INNER JOIN ContenedorPiezas cp on cp.idcontenedor = c.id
	INNER JOIN Pesadas p on p.id=cp.idpesaje  
	INNER JOIN Productos prd on prd.id = p.idproducto
	INNER JOIN Productos prdc on prdc.id = c.idproducto
	INNER JOIN TiposContenedor tc on tc.id = c.idTipo
	INNER JOIN 
	(
		SELECT * FROM
		(				
			SELECT
			idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
			FROM egresos
			
			WHERE 
			idTipoBulto='CNT'
		)cntEgres
		WHERE max_date = fecha_hora
	)e on e.idpesaje=c.id  	
	
	INNER JOIN Inventario i on (isnumeric(i.idpieza)=1 AND CONVERT(INT, idPieza)=p.id)
	
	WHERE
	i.fechaInicioInventario = CAST(@fechaInventario as DATE) 
	AND c.id not in (SELECT	cast( replace(I.idPieza,'A','') as int)  
					 FROM Inventario I
					 WHERE isnumeric(idpieza)=0 AND fechaInicioInventario = CAST(@fechaInventario as DATE)) 
	AND not exists
	(
		SELECT * 
		FROM Devoluciones dev
		WHERE dev.idpesaje = p.id and dev.idTipoBulto='PZA' AND dev.idpedido=e.idPedido
	) 

END


GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_sinREG]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP obtiene en funcion a un inventario 
--				las piezas que no tiene registro en el sistema
-- =============================================
CREATE PROCEDURE [dbo].[sp_getInv_sinREG]
		@fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;
 SELECT 
 TIPO='PIEZA',
 idPieza as NRO,
 d.nombre as DESTINO 
 
 FROM Inventario
 LEFT OUTER JOIN Destinos as d ON d.id = Inventario.idDestino
 
 WHERE
 Inventario.fechaInicioInventario = Cast(@fechaInventario as DATE) 
 AND ISNUMERIC(idpieza) = 1  AND CAST(idPieza as INT) not in (select id from Pesadas)  

UNION

 SELECT 
 TIPO='CONTENEDOR',
 idPieza as NRO,
 d.nombre as DESTINO 
 
 FROM Inventario
 LEFT OUTER JOIN Destinos as d ON d.id = Inventario.idDestino
 
 WHERE
 Inventario.fechaInicioInventario = Cast(@fechaInventario as DATE) 
 AND ISNUMERIC(idpieza) = 0 AND cast(replace(idPieza, 'A', '') as int) not in (select id from contenedores)

 ORDER BY d.nombre;
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getInv_sinSTL_conSTF]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
-- Description:	SP que obtiene ,en funcion a inventario, 
--				las piezas o contenedores que no tiene stock logico por
--				estar egresadas y estan en el stock fisico.
--				Tambien obtiene las piezas que se encuentran en el
--				inventario y estan registradas en ingreso a produccion.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getInv_sinSTL_conSTF]
			@fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
	TIPO='PIEZA',
	p.id as NRO,
	(YEAR(p.fecha_hora) + MONTH(p.fecha_hora) * 10000 + DAY(p.fecha_hora)*1000000)as LOTE,
	p.PesoNeto as PESO_NETO,
	prd.nombre as PRODUCTO,
	pe.idPedido as IDPEDIDO
	
	FROM fPiezasEgresadas(null) pe
	INNER JOIN Pesadas p on p.id = pe.idpesaje
	INNER JOIN Productos prd on prd.id=p.idproducto
	INNER JOIN Inventario i on (isnumeric(i.idpieza)=1 AND CONVERT(INT, idPieza)=pe.idpesaje) 		 
	
	WHERE
	i.fechaInicioInventario = CAST(@fechaInventario as DATE) 
	
	UNION

	SELECT distinct
	tc.Descripcion as TIPO,
	c.id as NRO,
	(YEAR(c.fecha_hora) + MONTH(c.fecha_hora) * 10000 + DAY(c.fecha_hora)*1000000)as LOTE,
	c.pesoNeto as PESO_NETO,
	prd.nombre as PRODUCTO,
	ce.idPedido as IDPEDIDO

			
	FROM fContenedoresEgresados(null) ce
	INNER JOIN Contenedores c on c.id=ce.idpesaje
	INNER JOIN TiposContenedor tc ON tc.id=c.idTipo
	INNER JOIN Productos prd on prd.id = c.idproducto
	INNER JOIN Inventario i on (isnumeric(i.idpieza)=0 AND cast( replace(i.idPieza,'A','') as int)=c.id) 		 
	WHERE 
	i.fechaInicioInventario = CAST(@fechaInventario as DATE) 
	
	UNION
	SELECT 
	TIPO='INGPRODUCCION',
	p.id as NRO,
	(YEAR(p.fecha_hora) + MONTH(p.fecha_hora) * 10000 + DAY(p.fecha_hora)*1000000)as LOTE,
	p.PesoNeto as PESO_NETO,
	prd.nombre as PRODUCTO,
	IDPEDIDO=null
	
	FROM pesadas p
	INNER JOIN DLP d on p.id = d.idpesaje
	INNER JOIN Productos prd on prd.id=p.idproducto
	INNER JOIN Inventario i on (isnumeric(i.idpieza)=1 AND CONVERT(INT, idPieza)=p.id) 		 
	
	WHERE
	i.fechaInicioInventario = CAST(@fechaInventario as DATE) 

END


GO
/****** Object:  StoredProcedure [dbo].[sp_getMovimientos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 06-08-2022
-- Description:	SP obtiene todos los movimientos que pudo haber 
-- tenido una pieza , caja o combo incluyendo todos los posibles
-- ciclos del sistema como ser Ingresos a Planta, Ingreso a Produccion,
-- pesajes en produccion , inclucion en caja , inclucion en Combo , despacho 
-- y devoluciones. 
--Se indica como parametro el id de la pieza, caja o combo. El segundo parametro
--describe si el ID es de una pieza o un contenedor 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getMovimientos]
@id int,@esContenedor bit=0
 
AS
BEGIN
SELECT  
MOV='INGRESO A PLANTA',
pes.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,pes.fecha_hora,106),103) as LOTE,
pes.idOI as OI,
prv.Nombre as PROVEEDOR,
oi.idCertSanitario as SANITARIO,
CONTENEDOR=null,
des.nombre as DESTINO,
CLIENTE=null,
COMPROBANTE=null,
SECTOR=null,
op.nombre as OPERADOR,
pes.idEstacion as ESTACION,
pes.unidades as UNIDADES,
pes.PesoNeto as NETO,
pes.PesoTara as TARA ,
pes.PesoRemitido as PESO_REMITIDO

FROM Pesadas pes
LEFT OUTER JOIN Productos prd ON prd.id=pes.idproducto
LEFT OUTER JOIN OI oi ON oi.id = pes.idOI
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi prv ON prv.idCtaAuxi= oi.codigoProveedorSAC and idPpal=1 AND idAuxi=1 
AND Imputable=1 
LEFT OUTER JOIN Destinos des ON des.id=pes.idDestino
LEFT OUTER JOIN operadores op ON op.id = pes.idOperador

WHERE 
@esContenedor=0 
and pes.id=@id
and pes.idOI is not null 

UNION

SELECT  
MOV='INGRESO A PRODUCCIÓN',
dlp.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,pes.fecha_hora,106),103) as LOTE,
pes.idOI as OI,
prv.Nombre as PROVEEDOR,
oi.idCertSanitario as SANITARIO,
CONTENEDOR=null,
DESTINO=null,
CLIENTE=null,
COMPROBANTE=null,
sec.nombre as SECTOR,
op.nombre as OPERADOR,
dlp.idEstacion as ESTACION,
pes.unidades as UNIDADES,
pes.PesoNeto as NETO,
pes.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Pesadas pes
JOIN DLP dlp ON dlp.idpesaje = pes.id
LEFT OUTER JOIN sectores sec ON sec.id = dlp.idSector
LEFT OUTER JOIN Productos prd ON prd.id=pes.idproducto
LEFT OUTER JOIN OI oi ON oi.id = pes.idOI
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi prv ON prv.idCtaAuxi= oi.codigoProveedorSAC and idPpal=1 AND idAuxi=1 
AND Imputable=1 
LEFT OUTER JOIN Destinos des ON des.id=pes.idDestino
LEFT OUTER JOIN operadores op ON op.id = pes.idOperador

WHERE 
@esContenedor=0 
and pes.id=@id

UNION

SELECT  
MOV='PESAJE EN PRODUCCIÓN',
pes.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,pes.fecha_hora,106),103) as LOTE,
OI = null,
PROVEEDOR =null,
SANITARIO=null,
CONTENEDOR=null,
des.nombre as DESTINO,
CLIENTE=null,
COMPROBANTE=null,
sec.nombre as SECTOR,
op.nombre as OPERADOR,
pes.idEstacion as ESTACION,
pes.unidades as UNIDADES,
pes.PesoNeto as NETO,
pes.PesoTara as TARA ,
PESO_REMITIDO =null

FROM Pesadas pes
LEFT OUTER JOIN sectores sec ON sec.id = pes.idSector
LEFT OUTER JOIN Productos prd ON prd.id=pes.idproducto
LEFT OUTER JOIN Destinos des ON des.id=pes.idDestino
LEFT OUTER JOIN operadores op ON op.id = pes.idOperador

WHERE 
@esContenedor=0 
and pes.id=@id
and pes.idOI is null

UNION

SELECT  
MOV='EN CONTENEDOR',
c.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,c.fecha_hora,106),103) as LOTE,
OI = null,
PROVEEDOR =null,
SANITARIO=null,
c.id as CONTENEDOR,
des.nombre as DESTINO,
CLIENTE=null,
COMPROBANTE=null,
SECTOR=null,
op.nombre as OPERADOR,
c.idEstacion as ESTACION,
c.unidades as UNIDADES,
c.PesoNeto as NETO,
c.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Pesadas pes
JOIN ContenedorPiezas cp ON cp.idpesaje=pes.id
JOIN Contenedores c ON c.id=cp.idcontenedor
LEFT OUTER JOIN Productos prd ON prd.id=c.idproducto
LEFT OUTER JOIN Destinos des ON c.iddestino=des.id
LEFT OUTER JOIN operadores op ON op.id = c.idOperador

WHERE 
@esContenedor=0 
and pes.id=@id

UNION

SELECT  
MOV='CONTENEDOR '+
	 case 
		when c.idTipo='CAJ' then 'CAJA' 
		when c.idTipo='CMB' then 'COMBO' 
		else '??'
	 end, 
c.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,c.fecha_hora,106),103) as LOTE,
OI = null,
PROVEEDOR =null,
SANITARIO=null,
c.id as CONTENEDOR,
des.nombre as DESTINO,
CLIENTE=null,
COMPROBANTE=null,
SECTOR=null,
op.nombre as OPERADOR,
c.idEstacion as ESTACION,
c.unidades as UNIDADES,
c.PesoNeto as NETO,
c.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Contenedores c 
LEFT OUTER JOIN Productos prd ON prd.id=c.idproducto
LEFT OUTER JOIN Destinos des ON c.iddestino=des.id
LEFT OUTER JOIN operadores op ON op.id = c.idOperador

WHERE 
@esContenedor=1 
and c.id=@id

UNION

-- EGRESO PARA PIEZA
SELECT  
MOV='EGRESO PIEZA',
e.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,pes.fecha_hora,106),103) as LOTE,
OI=null,
PROVEEDOR=null,
SANITARIO=null,
CONTENEDO=null,
DESTINO = null,
(CASE WHEN fc.NombreTercero is null THEN 'AJUST_INV' ELSE  fc.NombreTercero END) as CLIENTE,
(CASE WHEN fc.Numero is null THEN 'AJUST_INV' ELSE  fc.Numero END) as COMPROBANTE,
SECTOR=null,
op.nombre as OPERADOR,
e.idEstacion as ESTACION,
pes.unidades as UNIDADES,
pes.PesoNeto as NETO,
pes.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Egresos e
JOIN Pesadas pes ON pes.id=e.idpesaje
JOIN Pedidos ped ON ped.id = e.idPedido
LEFT OUTER JOIN Productos prd ON prd.id=pes.idproducto
LEFT OUTER JOIN operadores op ON op.id = pes.idOperador
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	ped.CodigoPedidoSAC= fc.IdCabecera 
	and fc.idAuxi=1 
	and ped.CodigoClienteSAC=fc.idCtaAuxi 
	and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')
	and fc.idPpal=1

WHERE 
e.idTipoBulto='PZA'
AND @esContenedor=0 
and pes.id=@id


UNION
-- EGRESO PARA CONTENEDOR
SELECT 
MOV='EGRESO ' +
	 case 
		when c.idTipo='CAJ' then 'CAJA' 
		when c.idTipo='CMB' then 'COMBO' 
		else '??'
	 end,
e.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,c.fecha_hora,106),103) as LOTE,
OI=null,
PROVEEDOR=null,
SANITARIO=null,
c.id as CONTENEDOR,
DESTINO = null,
(CASE WHEN fc.NombreTercero is null THEN 'AJUST_INV' ELSE  fc.NombreTercero END) as CLIENTE,
(CASE WHEN fc.Numero is null THEN 'AJUST_INV' ELSE  fc.Numero END) as COMPROBANTE,
SECTOR=null,
op.nombre as OPERADOR,
e.idEstacion as ESTACION,
c.unidades as UNIDADES,
c.PesoNeto as NETO,
c.PesoTara as TARA ,
PESO_REMITIDO=null
FROM Egresos e
JOIN Contenedores c ON c.id = e.idpesaje and e.idTipoBulto='CNT'
JOIN Pesadas pes ON pes.id=e.idpesaje
JOIN Pedidos ped ON ped.id = e.idPedido
LEFT OUTER JOIN Productos prd ON prd.id=c.idproducto
LEFT OUTER JOIN operadores op ON op.id = pes.idOperador
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	ped.CodigoPedidoSAC= fc.IdCabecera 
	and fc.idAuxi=1 
	and ped.CodigoClienteSAC=fc.idCtaAuxi 
	and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')
	and fc.idPpal=1

WHERE 
@esContenedor=1 
and c.id=@id

UNION

-- DEVOLUCION PARA PIEZA
SELECT  
MOV='DEVOLUCIÓN PIEZA',
d.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,pes.fecha_hora,106),103) as LOTE,
OI=null,
PROVEEDOR=null,
SANITARIO=null,
CONTENEDOR=null,
DESTINO = null,
(CASE WHEN fc.NombreTercero is null THEN 'AJUST_INV' ELSE  fc.NombreTercero END) as CLIENTE,
(CASE WHEN fc.Numero is null THEN 'AJUST_INV' ELSE  fc.Numero END) as COMPROBANTE,
SECTOR=null,
op.nombre as OPERADOR,
d.idEstacion as ESTACION,
pes.unidades as UNIDADES,
pes.PesoNeto as NETO,
pes.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Devoluciones d
JOIN Pesadas pes ON pes.id=d.idpesaje
JOIN Pedidos ped ON ped.id = d.idPedido
LEFT OUTER JOIN Productos prd ON prd.id=pes.idproducto
LEFT OUTER JOIN operadores op ON op.id = d.idOperador
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	ped.CodigoPedidoSAC= fc.IdCabecera 
	and fc.idAuxi=1 
	and ped.CodigoClienteSAC=fc.idCtaAuxi 
	and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')
	and fc.idPpal=1

WHERE
d.idTipoBulto='PZA'
AND @esContenedor=0 
and pes.id=@id


UNION
-- DEVOLUCION PARA CONTENEDOR
SELECT distinct  
MOV='DEVOLUCIÓN ' +
	 case 
		when c.idTipo='CAJ' then 'CAJA' 
		when c.idTipo='CMB' then 'COMBO' 
		else '??'
	 end,
d.fecha_hora as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,c.fecha_hora,106),103) as LOTE,
OI=null,
PROVEEDOR=null,
SANITARIO=null,
c.id as CONTENEDOR,
DESTINO = null,
fc.NombreTercero as CLIENTE,
fc.Numero as COMPROBANTE,
SECTOR=null,
op.nombre as OPERADOR,
d.idEstacion as ESTACION,
c.unidades as UNIDADES,
c.PesoNeto as NETO,
c.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Contenedores c
JOIN Devoluciones d ON d.idpesaje =c.id and d.idTipoBulto='CNT'
JOIN Pedidos ped ON ped.id = d.idPedido
LEFT OUTER JOIN Productos prd ON prd.id=c.idproducto
LEFT OUTER JOIN operadores op ON op.id = d.idOperador
JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	ped.CodigoPedidoSAC= fc.IdCabecera 
	and fc.idAuxi=1 
	and ped.CodigoClienteSAC=fc.idCtaAuxi 
	and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')
	and fc.idPpal=1

WHERE 
d.idTipoBulto='CNT'
AND @esContenedor=1 
and c.id=@id

UNION
-- DESARMADO CONTENEDOR
SELECT distinct  
MOV='DESARMADO ' +
	 case 
		when c.idTipo='CAJ' then 'CAJA' 
		when c.idTipo='CMB' then 'COMBO' 
		else '??'
	 end,
c.fecha_desarmado as FECHA,
prd.nombre as PRODUCTO,
Convert(varchar(10),CONVERT(date,c.fecha_hora,106),103) as LOTE,
OI=null,
PROVEEDOR=null,
SANITARIO=null,
c.id as CONTENEDOR,
DESTINO = null,
CLIENTE=null,
COMPROBANTE=null,
SECTOR=null,
op.nombre as OPERADOR,
c.idEstacion as ESTACION,
c.unidades as UNIDADES,
c.PesoNeto as NETO,
c.PesoTara as TARA ,
PESO_REMITIDO=null

FROM Contenedores c
LEFT OUTER JOIN Productos prd ON prd.id=c.idproducto
LEFT OUTER JOIN operadores op ON op.id = c.idOperador
WHERE 
@esContenedor=1 
and c.id=@id
and c.fecha_desarmado is not null

order by FECHA


END
GO
/****** Object:  StoredProcedure [dbo].[sp_getOIs]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 26-8-2020
-- Description:	SP obtiene las ordenes de ingresos con 
--              filtros para solo activas =1 o todas =0 
--              y para una OI especifica o 0 para todas 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getOIs]
	 @soloActivas bit=1,@idOI int =0
AS
BEGIN
 SELECT 
 oing.id as OI ,
 p.Nombre as PROVEEDOR,
 oing.idCertSanitario as CERT_SANITARIO,
 oing.fecha_hora as FECHA_HORA,
 oing.idEstacion as ESTACION,
 oing.idOperador as IDOPERADOR,
 o.nombre as OPERADOR,
 o.pasw as PASW_OPERADOR,
 o.tipo as TIPO_OPERADOR, 
 oing.codigoProveedorSAC as CODIGO_PROVEEDOR,
 (CASE WHEN oing.activo = 0 OR oing.activo is null THEN 'CERRADA' ELSE 'ABIERTA' END)as ESTADO ,
 oing.activo as ACTIVO  
 FROM OI oing  
 LEFT OUTER JOIN operadores as o ON oing.idOperador = o.id  
 LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as p ON p.idCtaAuxi = oing.codigoProveedorSAC AND p.idAuxi=1 AND p.Imputable=1 
 WHERE (@soloActivas = 0 OR (oing.Activo=1 or oing.Activo is null)) AND (@idOI=0 OR @idOI=oing.id) 
 ORDER BY oing.fecha_hora desc

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOIsLote]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetOIsLote] 
	@lote date 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF OBJECT_ID( N'tempdb..#tmpOI') IS NOT NULL
DROP TABLE #tmpOI;

CREATE TABLE #tmpOI 
    ( idOI   INT,lote date,idPesaje INT );


	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI,DLP.idpesaje 
	FROM	DLP
	WHERE	cast(DLP.fecha_hora as DATE) = @lote

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI ,DLP.idpesaje
	FROM	DLP
	WHERE	cast(DLP.fecha_hora as DATE) in (select lote from #tmpOI) 
			and not exists (select * from #tmpOI where lote=DLP.LotePadre and idoi=DLP.idOI and idPesaje=DLP.idpesaje ) 

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI ,DLP.idpesaje
	FROM	DLP
	WHERE	cast(DLP.fecha_hora as DATE) in (select lote from #tmpOI) 
			and not exists (select * from #tmpOI where lote=DLP.LotePadre and idoi=DLP.idOI and idPesaje=DLP.idpesaje ) 

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI ,DLP.idpesaje
	FROM	DLP
	WHERE	cast(DLP.fecha_hora as DATE) in (select lote from #tmpOI) 
			and not exists (select * from #tmpOI where lote=DLP.LotePadre and idoi=DLP.idOI and idPesaje=DLP.idpesaje ) 

	SELECT distinct OI.id as OI,oi.fecha_hora as INGRESO, prv.Nombre as PROVEEDOR,OI.idCertSanitario as SANITARIO ,prd.nombre as PRODUCTO,pe.numTropa as TROPA
	FROM #tmpOI
	JOIN OI  ON #tmpOI.idOI = OI.id 
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prv ON OI.CodigoProveedorSAC=prv.idCtaAuxi  AND prv.idPpal=1 AND prv.idAuxi=1 AND prv.Imputable=1
	JOIN Pesadas as pe ON OI.id=pe.idOI
	JOIN Productos prd ON pe.idproducto=prd.id
	where #tmpOI.idoi is not null  AND #tmpOI.idoi > 0 AND pe.id=#tmpOI.idPesaje

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOIsPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* =============================================
Author:		<Author,,Name>
Create date: <Create Date,,>

Description:	
Desde un numero de pieza se obtienen todas las Ordenens de Ingresos 
de las piezas que ingresaron a produccion en la misma fecha de produccion  
la pieza indicada.

Es decir : Conocer los proveedores y certificados sanitarios de las piezas que ingresaron a produccion
el mismo dia que se produjo la pieza indicada.

-- =============================================*/
CREATE PROCEDURE [dbo].[sp_GetOIsPieza] 
	@numPieza int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF OBJECT_ID( N'tempdb..#tmpOI') IS NOT NULL
DROP TABLE #tmpOI;

CREATE TABLE #tmpOI 
    ( idOI   INT,lote date,idPesaje INT );
DECLARE @loteBuscado as date
DECLARE @idTipoProducto as int

SELECT @loteBuscado= pe.fecha_hora,@idTipoProducto= prd.idtipo from Pesadas pe,Productos prd WHERE pe.id = @numPieza AND pe.idproducto = prd.id

IF @loteBuscado is not null AND @loteBuscado in (select cast(dlp.fecha_hora as date) from DLP)  
BEGIN

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI,Pesadas.id 
	FROM	DLP,Pesadas,productos
	WHERE	cast(DLP.fecha_hora as DATE) = @loteBuscado AND 
			Pesadas.id=DLP.idpesaje AND 
			Pesadas.idproducto = Productos.id and 
			Productos.idtipo = @idTipoProducto 	

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI ,Pesadas.id
	FROM	DLP,Pesadas,productos
	WHERE	cast(DLP.fecha_hora as DATE) in (select lote from #tmpOI) AND 
			Pesadas.id=DLP.idpesaje AND 
			Pesadas.idproducto = Productos.id and 
			Productos.idtipo = @idTipoProducto
			and not exists (select * from #tmpOI where lote=DLP.LotePadre and idoi=DLP.idOI and idPesaje=Pesadas.id ) 

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI ,Pesadas.id
	FROM	DLP,Pesadas,productos
	WHERE	cast(DLP.fecha_hora as DATE) in (select lote from #tmpOI) AND
			Pesadas.id=DLP.idpesaje AND 
			Pesadas.idproducto = Productos.id and 
			Productos.idtipo = @idTipoProducto
			and not exists (select * from #tmpOI where lote=DLP.LotePadre and idoi=DLP.idOI and idPesaje=Pesadas.id ) 

	INSERT into #tmpOI (lote,idoi,idPesaje)
	SELECT distinct DLP.LotePadre, DLP.idOI ,Pesadas.id
	FROM	DLP,Pesadas,productos
	WHERE	cast(DLP.fecha_hora as DATE) in (select lote from #tmpOI) AND 
			Pesadas.id=DLP.idpesaje AND 
			Pesadas.idproducto = Productos.id and 
			Productos.idtipo = @idTipoProducto
			and not exists (select * from #tmpOI where lote=DLP.LotePadre and idoi=DLP.idOI and idPesaje=Pesadas.id ) 

	SELECT distinct OI.id as OI,oi.fecha_hora as INGRESO, prv.Nombre as PROVEEDOR,OI.idCertSanitario as SANITARIO ,prd.nombre as PRODUCTO,pe.numTropa as TROPA
	FROM #tmpOI
	JOIN OI  ON #tmpOI.idOI = OI.id 
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prv ON OI.CodigoProveedorSAC=prv.idCtaAuxi  AND prv.idPpal=1 AND prv.idAuxi=1 AND prv.Imputable=1
	JOIN Pesadas as pe ON OI.id=pe.idOI
	JOIN Productos prd ON pe.idproducto=prd.id
	where #tmpOI.idoi is not null  AND #tmpOI.idoi > 0 AND prd.idtipo = @idTipoProducto and pe.id=#tmpOI.idPesaje

END;
ELSE

BEGIN

	SELECT OI.id as OI,oi.fecha_hora as INGRESO, prv.Nombre as PROVEEDOR,OI.idCertSanitario as SANITARIO ,prd.nombre as PRODUCTO,pe.numTropa as TROPA
	FROM Pesadas pe
	JOIN OI  ON pe.idOI = OI.id 
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prv ON OI.CodigoProveedorSAC=prv.idCtaAuxi  AND prv.idPpal=1 AND prv.idAuxi=1 AND prv.Imputable=1
	JOIN Productos prd ON pe.idproducto=prd.id
	WHERE pe.id=@numPieza
END;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getPedidosPendientesVenta]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 06-8-2022
-- Description:	SP obtiene todos los pedidos de venta
-- pendientes para una fecha de preparacion del pedido 
-- especificada o para todos los pedidos pendientes si 
--@fechaPreparacion=''. El formato de la fecha debe ser 'yyyy-MM-dd'
-- Informa solo los datos de la cabecera del peidod del SAC y el pedido
-- del sistema de gestion de eproduccion.
-- Se podra tambien aplicar el filtro del numero de comprobante que 
-- segun valor del tercer parametro podra ser por comprobante exacto
-- o por aproximacion.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPedidosPendientesVenta]
@fechaPreparacion varchar(12)='',@comprobantePedido varchar(12)='',@exactComprobante bit=0
 
AS
BEGIN

SELECT DISTINCT 
pp.idCtaAuxi as CodigoClienteSAC,
fc.nombreTercero as CLIENTE,
pp.PedIdCabecera as CodigoPedidoSAC,
fc.IdTipoComprobante as TipoPedidoSAC,
fc.numero as COMPROBANTE,
fc.fecha as FECHA_CREACION,
fs.fechaVencimiento as FECHA_PREPARACION,
p.id as ID,
p.activo as ACTIVO,
o.id as IDOPERADOR,
o.nombre as OPERADOR,
o.pasw as PASW_OPERADOR,
o.tipo as TIPO_OPERADOR

FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.V_CantidadesPedidosPendientes as pp

	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
			pp.PedIdCabecera = fc.IdCabecera 
			and pp.idAuxi=fc.idAuxi 
			and pp.idCtaAuxi=fc.idCtaAuxi 

	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
			fc.IdCabecera = fs.IdCabecera 
			and fc.idTipoComprobante=fs.idTipoComprobante 
			and fs.idPpal=1 

	LEFT OUTER JOIN pedidos as p ON  
			p.CodigoPedidoSAC = pp.PedIdCabecera 
			and p.CodigoClienteSAC= fc.idCtaAuxi 

	LEFT OUTER JOIN Operadores as o ON p.idoperador = o.id 
WHERE
	(	p.activo = 1 
		OR 
		NOT EXISTS(	select * 
					from Pedidos p2 
					where 
					p2.CodigoPedidoSAC = fc.IdCabecera 
					AND p2.CodigoClienteSAC=fc.idCtaAuxi 
					)
	) 
	AND pp.idAuxi=1 
	AND fc.idPpal=1 
	AND (fc.idTipoComprobante='PED' OR fc.idTipoComprobante='PED2')  
	AND ((@fechaPreparacion ='') or cast(fs.fechaVencimiento as DATE)= convert(date,@fechaPreparacion,102)) 
	AND (@comprobantePedido = '' or ((@exactComprobante=1 and fc.numero = @comprobantePedido) or (@exactComprobante=0 and fc.numero like '%'+@comprobantePedido+'%'))) 

ORDER BY FECHA_PREPARACION asc

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getPesada]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 26-8-2020
-- Description:	SP obtiene una pesada con todos sus datos vinculados.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPesada]
	 @idPesada int
AS
BEGIN

SELECT
pe.id as ID,
pe.fecha_hora as FECHA_HORA_PESADA,
pe.idestacion as IDESTACION_PESADA,
pe.idoi as IDOI,
pe.unidades as UNIDADES_PESADA,
pe.pesoNeto as PESO_NETO,
pe.pesoTARA as PESO_TARA,
pe.pesoRemitido as PESO_REMITIDO,
pe.idoperador as IDOPERADOR_PESADA, 
ope.nombre as NOMBRE_OPERADOR_PESADA,
ope.pasw as PASW_OPERADOR_PESADA,
ope.tipo as TIPO_OPERADOR_PESADA,
OI.idoperador as IDOPERADOR_OI,
ooi.nombre as NOMBRE_OPERADOR_OI,
ooi.pasw as PASW_OPERADOR_OI,
ooi.tipo as TIPO_OPERADOR_OI,
OI.codigoProveedorSAC as CODIGO_PROVEEDOR,
prove.Nombre as NOMBRE_PROVEEDOR,
OI.fecha_hora as FECHA_HORA_OI,
OI.idestacion as IDESTACION_OI,
OI.idCertSanitario as CERT_SANITARIO,
OI.Activo as ACTIVO,
pe.idproducto as IDPRODUCTO,
pk.idProducto as CODIGO_PRODUCTO_SAC,
pk.Descripcion as NOMBRE_PRODUCTO_SAC, 
pk.Alias as ALIAS_PRODUCTO_SAC, 
prd.nombre as NOMBRE_PRODUCTO, 
prd.numSenasa as NUMSENASA_PRODUCTO,
prd.pesoNetoPredef as PESONETOPREDEF_PRODUCTO,
prd.pesoTaraPredef as PESOTARAPREDEF_PRODUCTO,
prd.rendimientoSTD as REND_PRODUCTO,
prd.unidadesPredef as UNIDADESPREDEF_PRODUCTO,
prd.diasvencimiento as DIASVENCIMIENTO_PRODUCTO,
prd.esinsumo as ESINSUMO_PRODUCTO,
prd.espesable as ESPESABLE_PRODUCTO,
prd.escombo as ESCOMBO_PRODUCTO,
prd.escaja as ESCAJA_PRODUCTO,
prd.esTropa as ESTROPA_PRODUCTO,
prd.nombreL1 as NOMBREL1_PRODUCTO,
prd.nombreL2 as NOMBREL2_PRODUCTO,
prd.nombreL3 as NOMBREL3_PRODUCTO,
prd.nombreL4 as NOMBREL4_PRODUCTO,
prd.nombreL5 as NOMBREL5_PRODUCTO,
prd.nombreL6 as NOMBREL6_PRODUCTO,
prd.textauxl1 as TEXTAUXL1_PRODUCTO,
prd.textauxl2 as TEXTAUXL2_PRODUCTO,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA,
prd.idtipo as IDTIPO_PRODUCTO,
tp.nombre as TIPO_PRODUCTO,
pe.iddestino as IDDESTINO,
de.nombre as DESTINO,
pe.idsector as IDSECTOR , 
sec.nombre as SECTOR ,
pe.idpiezapadre as IDPIEZAPADRE,
pe.numTropa as NUMTROPA,
tip.id as IDTIPIFICACION,
tip.nombre as NOMBRE_TIPIFICACION

FROM pesadas as pe  
LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id
LEFT OUTER JOIN OI ON pe.idOi = OI.id  
LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id  
LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id  
LEFT OUTER JOIN Etiquetas e ON prd.idEtiqueta = e.id
LEFT OUTER JOIN operadores ooi ON OI.idOperador = ooi.id
LEFT OUTER JOIN Tipificaciones tip ON pe.idTipificacion=tip.id
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prove ON prove.idCtaAuxi = oi.codigoProveedorSAC and prove.idAuxi=1 and prove.idPpal=1 
and prove.Imputable=1 
LEFT OUTER JOIN destinos de ON de.id = pe.iddestino  
LEFT OUTER JOIN sectores sec ON sec.id = pe.idsector  
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = prd.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
WHERE pe.id = @idPesada


END
GO
/****** Object:  StoredProcedure [dbo].[sp_getPiezasEgresadasPorPedido]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-08-2022
-- Description:	SP obtiene el detalle totalizado 
--              de piezas egresadas para un 
--              pedido de produccion especifico.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPiezasEgresadasPorPedido]
@idPedido int
 
AS
BEGIN

 SELECT
 fs.IdMovimiento as ITEM_PRD_SAC,
 fp.idProducto as CODIGO_SAC,
 fp.Descripcion as PRODUCTO_SAC,
 fs.detalle as OBSERVACION,
 fs.cantidad as UNDS_PED,
 fs.cantidadUMPrimaria as PESO_PED,

(SELECT SUM(UNIDADES) FROM(
	SELECT SUM(pe.unidades) AS UNIDADES 
	FROM Pesadas pe, Egresos eg ,Productos prd2 
	WHERE
	eg.idTipoBulto='PZA' 
	and pe.idproducto = prd2.id 
	and	prd2.codigoProductoSAC = fp.idProducto  
	and eg.idpesaje = pe.id 
	and eg.idPedido = ped.id
	UNION
    SELECT count(*) as UNIDADES 
	FROM Contenedores c,Egresos eg ,Productos prd2 
	WHERE
	eg.idTipoBulto='CNT'
	and eg.idPedido = ped.id
	and c.id=eg.idpesaje
	and prd2.id=c.idProducto
	and prd2.codigoProductoSAC=fp.idProducto
	and (c.idTipo='CMB' OR c.idTipo='CAJ'))T
	WHERE T.UNIDADES is not null) as UNDS_COL,  

(SELECT SUM(PESONETO) FROM(
	SELECT distinct pe.id as ID,pe.PesoNeto AS PESONETO 
	FROM Pesadas pe, Egresos eg ,Productos prd2 
	WHERE 
	eg.idTipoBulto='PZA'
	and pe.idproducto = prd2.id 
	and	prd2.codigoProductoSAC = fp.idProducto  
	and eg.idpesaje = pe.id 
	and eg.idPedido = ped.id
	UNION ALL
    SELECT distinct c.id AS ID,c.pesoNeto as PESONETO 
	FROM Contenedores c,Egresos eg ,Productos prd2 
	WHERE
	eg.idTipoBulto='CNT'
	and eg.idPedido = ped.id
	and c.id=eg.idpesaje
	and prd2.id=c.idProducto
	and prd2.codigoProductoSAC=fp.idProducto
	and (c.idTipo='CMB' OR c.idTipo='CAJ'))T
	WHERE T.PESONETO is not null) as PESO_COL  

 FROM pedidos as ped 
 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
		ped.CodigoPedidoSAC = fs.IdCabecera and 
		(fs.idTipoComprobante = 'PED' or fs.idTipoComprobante = 'PED2') and 
		fs.idPpal=1  
 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON 
	fs.idProducto = fp.idProducto and 
	fp.Imputable = 1 and fp.idPlanProducto=2 
 
 JOIN Productos prd ON fp.idProducto = prd.codigoProductoSAC and fs.idProducto=prd.codigoProductoSAC 
 
 WHERE 
 ped.id = @idPedido
 
 GROUP BY fs.IdMovimiento,ped.id,fp.idProducto ,fp.Descripcion,fs.cantidad ,fs.cantidadUMPrimaria,fs.Detalle

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-11-2022
-- Description:	SP obtiene un producto del sistema de produccion
--              informando tambien que articulo SAC 
--              posee vinculado.
--              Incluye filtro por aproximacion de nombre de producto.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProducto]
	 @idProducto int
AS
BEGIN
SELECT
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.nombre as NOMBRE,
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.escombo as ESCOMBO,
p.escaja as ESCAJA,
p.esTropa as ESTROPA,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxL1 as TEXTAUX_L1,
p.textauxL2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA

FROM Productos p 
LEFT OUTER JOIN TiposProducto tp ON tp.id = p.idtipo
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
WHERE p.id = @idProducto

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-11-2022
-- Description:	SP obtiene los productos del sistema de produccion
--              informando tambien los articulos del sistema SAC 
--              que poseen vinculados.
--              Incluye filtro por aproximacion de nombre de producto.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProductos]
	 @prodFilter nvarchar(100)=''
AS
BEGIN

SELECT
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.id as ID,p.nombre as NOMBRE,
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.esTropa as ESTROPA,
p.esCombo as ESCOMBO,
p.esCaja as ESCAJA,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA

FROM Productos p
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
WHERE @prodFilter = '' OR UPPER(p.nombre) like '%'+UPPER(@prodFilter)+'%' 


END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosCaja]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-11-2022
-- Description:	SP obtiene los productos del sistema de produccion
--              del tipo caja.
--              Incluye filtro por aproximacion de nombre de producto.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProductosCaja]
	 @prodFilter nvarchar(100)=''
AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.estropa as ESTROPA,
p.escombo as ESCOMBO,
p.escaja as ESCAJA,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA

FROM Productos p
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
WHERE (@prodFilter = '' OR UPPER(p.nombre) like '%'+UPPER(@prodFilter)+'%') 
AND p.escaja =1

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosCombo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-11-2022
-- Description:	SP obtiene los productos del sistema de produccion
--              del tipo combo.
--              Incluye filtro por aproximacion de nombre de producto.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProductosCombo]
	 @prodFilter nvarchar(100)=''
AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.estropa as ESTROPA,
p.escombo as ESCOMBO,
p.escaja as ESCAJA,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA

FROM Productos p
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
WHERE (@prodFilter = '' OR UPPER(p.nombre) like '%'+UPPER(@prodFilter)+'%') 
AND p.escombo =1

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosParaContenedores]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-11-2022
-- Description:	SP obtiene los productos del sistema de produccion
--              que pueden ser utilizados para la creacion de Cajas o Combos.
--              Incluye filtro por aproximacion de nombre de producto.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProductosParaContenedores]
	 @prodFilter nvarchar(100)=''
AS
BEGIN

SELECT 
p.codigoProductoSAC as CODIGO_SAC,
pk.Descripcion as NOMBRE_SAC,
pk.alias as ALIAS_SAC,
p.id as ID,
p.nombre as NOMBRE, 
tp.id as IDTIPO,
tp.nombre as TIPO,
p.numSenasa as NUMSENASA,
p.pesonetopredef as NETO_PREDEF,
p.pesotarapredef as TARA_PREDEF,
p.unidadesPredef as UNIDADES_PREDEF,
p.rendimientoSTD as REND,
p.diasvencimiento as DIAS_VENCIMIENTO,
(CASE WHEN p.esinsumo = 0 OR p.esinsumo is null THEN 'NO' ELSE 'SI' END)as INS,
(CASE WHEN p.espesable = 0 OR p.espesable is null THEN 'NO' ELSE 'SI' END) as PES,
p.esinsumo as ESINSUMO,
p.espesable as ESPESABLE,
p.estropa as ESTROPA,
p.escombo as ESCOMBO,
p.escaja as ESCAJA,
p.nombreL1 as NOMBRE_L1,
p.nombreL2 as NOMBRE_L2,
p.nombreL3 as NOMBRE_L3,
p.nombreL4 as NOMBRE_L4,
p.nombreL5 as NOMBRE_L5,
p.nombreL6 as NOMBRE_L6,
p.textauxl1 as TEXTAUX_L1,
p.textauxl2 as TEXTAUX_L2,
e.id as IDETIQUETA,
e.Nombre as NOMBRE_ETIQUETA,
e.Descripcion as DESCRIPCION_ETIQUETA,
e.idTipoBulto as IDTIPOBULTO_ETIQUETA

FROM Productos p
LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = p.codigoProductoSAC AND pk.idPlanProducto=2 AND pk.Imputable=1 
LEFT OUTER JOIN TiposProducto as tp ON p.idtipo=tp.id 
LEFT OUTER JOIN Etiquetas e ON p.idEtiqueta = e.id
WHERE (@prodFilter = '' OR UPPER(p.nombre) like '%'+UPPER(@prodFilter)+'%') 
AND (p.escombo =0 or p.esCombo is null) AND (p.escaja =0 or p.esCaja is null) AND (p.esinsumo =0 or p.esinsumo is null)  

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProductosSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 26-8-2020
-- Description:	SP obtiene los productos del sistema administrativo
--              contable. Admite parametro de filtro por aproximacion
--              de nombre. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProductosSAC]
	 @nombreFiltro nvarchar(100)=''
AS
BEGIN


SELECT 
	idProducto as CODIGO , 
	Descripcion as NOMBRE,
	cast(alias as varchar(12)) as ALIAS 
FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos 
WHERE 
	(@nombreFiltro = '' OR UPPER(Descripcion) like '%'+UPPER(@nombreFiltro)+'%' ) 
	--AND alias not like '%[^0-9]%' and alias <> ''
	AND idPlanProducto=2 
	AND Imputable=1 
	AND EsServicio=0

END
GO
/****** Object:  StoredProcedure [dbo].[sp_getProveedoresSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 24-2-2021
-- Description:	SP obtiene los proveedores del sistema administrativo
--              contable. Admite parametro de filtro por aproximacion
--              de nombre. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_getProveedoresSAC]
	 @nameFilter nvarchar(100)=''
AS
BEGIN


SELECT 
	idCtaAuxi as CODIGO , 
	nombre as NOMBRE 
	FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi 
	WHERE 
	idPpal=1 
	AND idAuxi=1 
	AND Imputable=1
	AND (@nameFilter = '' OR UPPER(nombre) like '%'+UPPER(@nameFilter)+'%' ) 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getSaldosEgresosPorFechas]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 07-8-2022
-- Description:	Obtiene el detalle de piezas y contenedores egresados 
--				con saldos de unidades y kg agrupadas
--				por pedidos, con filtro por 
--				rango de fechas.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getSaldosEgresosPorFechas]
@cliente varchar(40)='',@desde varchar(10)='' , @hasta varchar(10)='',@comprobantePedido char(12)='',@lote varchar(10)=''
 
AS
BEGIN
	SELECT
	TIPO ='PIEZA',
	fc.Numero as COMPROBANTE,
	fc.nombreTercero as CLIENTE,
	prd.codigoProductoSAC as COD_PRD,
	prd.nombre as PRODUCTO,  
	cast(pd.cantidad as integer) as UNDS_PED,
	pd.cantidadUMPrimaria as PESO_PED,  
	SUM(p.unidades) as UNDS_EGR,
	SUM(p.pesoNeto) as PESO_EGR,
	(cast(pd.cantidad as integer) - SUM(p.unidades)) AS SALDO_UNDS,
	(pd.cantidadUMPrimaria - SUM(p.pesoneto)) AS SALDO_PESO   
	
	FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock pd   
	JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  pd.IdCabecera = fc.IdCabecera 
	JOIN Productos as prd ON pd.idProducto = prd.codigoProductoSAC     
	JOIN Pedidos as ped ON pd.IdCabecera = ped.CodigoPedidoSAC     
	LEFT OUTER JOIN Egresos as e ON ped.id = e.idPedido and e.idTipoBulto='PZA'   
	LEFT OUTER JOIN Pesadas as p ON e.idpesaje = p.id
    and fc.idPpal=1  
	
	WHERE 
	(@cliente = '' or (@cliente = fc.nombreTercero))
	and (@desde ='' or @comprobantePedido <> '' 
			or cast(e.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102))
	and (@comprobantePedido='' or  @comprobantePedido=ped.ComprobantePedidoSAC) 
	and (@lote ='' or convert(date,@lote,102) = cast(e.fecha_hora as date) )
	and pd.idPpal=1 
	and (pd.idTipoComprobante='PED' or pd.idTipoComprobante='PED2')  
	and	ped.CodigoClienteSAC=fc.idCtaAuxi 
	and ped.CodigoClienteSAC=fc.idCtaAuxi 
	and fc.idAuxi=1
	and fc.idTipoComprobante=pd.IdTipoComprobante
	
	GROUP BY fc.numero,fc.nombreTercero, prd.codigoProductoSAC,prd.nombre, pd.cantidad,pd.cantidadUMPrimaria   

	UNION

	SELECT
		TIPO,
		COMPROBANTE,
		CLIENTE,
		COD_PRD,
		PRODUCTO,
		UNDS_PED,
		PESO_PED,
		COUNT(*) as UNDS_EGR,
		SUM(PESO_EGR) as PESO_EGR,
		(UNDS_PED - COUNT(*) ) AS SALDO_UNDS,
		(PESO_PED - SUM(PESO_EGR)) AS SALDO_PESO   
 	FROM (
		SELECT distinct 
		tc.Descripcion as TIPO,
		fc.numero as COMPROBANTE,
		fc.nombreTercero as CLIENTE,
		prd.codigoProductoSAC as COD_PRD,
		prd.nombre as PRODUCTO,  
		cast(pd.cantidad as integer) as UNDS_PED,
		pd.cantidadUMPrimaria as PESO_PED,  
		c.pesoNeto as PESO_EGR
		
		FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock pd   
		JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  pd.IdCabecera = fc.IdCabecera 
		JOIN Pedidos as ped ON pd.IdCabecera = ped.CodigoPedidoSAC     
		JOIN Productos as prd ON pd.idProducto = prd.codigoProductoSAC     
		LEFT OUTER JOIN Egresos as e ON ped.id = e.idPedido and e.idTipoBulto ='CNT'   
		LEFT OUTER JOIN Contenedores as c ON c.id = e.idpesaje and c.idProducto=prd.id 
		LEFT OUTER JOIN TiposContenedor as tc ON c.idTipo = tc.id
		and fc.idPpal=1  
		
		WHERE
		(@cliente = '' or (@cliente = fc.nombreTercero))
		and (@desde ='' or @comprobantePedido <> '' 
			or cast(e.fecha_hora as date) between convert(date,@desde,102) and convert(date,	@hasta,102))
		and (@comprobantePedido='' or  @comprobantePedido=ped.ComprobantePedidoSAC)
		and (@lote ='' or convert(date,@lote,102) = cast(e.fecha_hora as date) )
		and pd.idPpal=1 
		and (pd.idTipoComprobante='PED' or pd.idTipoComprobante='PED2')  
		and	ped.CodigoClienteSAC=fc.idCtaAuxi 
		and fc.idAuxi=1
		and fc.idTipoComprobante=pd.idTipoComprobante) T      
	WHERE TIPO is not null
	GROUP BY T.TIPO,T.COMPROBANTE,T.CLIENTE,T.COD_PRD,T.PRODUCTO,T.UNDS_PED,T.PESO_PED   
	ORDER BY TIPO,COMPROBANTE	


END
GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalBultosEnStock]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 13-2-2021
-- Description:	SP obtiene el total de piezas , Cajas y Combos 
--				que se encuentran en stock.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getTotalBultosEnStock]
@totalPiezas int =0 out,@totalCajas int = 0 out,@totalCombos int = 0 out

AS

BEGIN
	SET NOCOUNT ON;
  SELECT @totalPiezas = COUNT(*) 
  FROM fPiezasEnStock(null)     
 
  SELECT @totalCajas = COUNT(*) 
  FROM fContenedoresEnStock(null)
  WHERE idTipo='CAJ'

  SELECT @totalCombos = COUNT(*) 
  FROM fContenedoresEnStock(null)
  WHERE idTipo='CMB'

END


GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalBultosEnStockVerificados]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 19-2-2021
-- Description:	SP obtiene los totales de piezas ,cajas y combos
--				en stock y los totales de los mismos pero verificados
--				con el inventario.
-- =============================================
CREATE PROCEDURE [dbo].[sp_getTotalBultosEnStockVerificados]
@fechaInventario dateTime ,
@totalPiezas int =0 out,
@totalCajas int = 0 out,
@totalCombos int = 0 out,
@totalPiezasVerificadas int =0 out,
@totalCajasVerificadas int = 0 out,
@totalCombosVerificados int = 0 out

AS

BEGIN
	SET NOCOUNT ON;

  SET NOCOUNT ON;
  SELECT @totalPiezas = COUNT(*) 
  FROM fPiezasEnStock(null)     
 
  SELECT @totalCajas = COUNT(*) 
  FROM fContenedoresEnStock(null)
  WHERE idTipo='CAJ'

  SELECT @totalCombos = COUNT(*) 
  FROM fContenedoresEnStock(null)
  WHERE idTipo='CMB'

  SELECT 
  @totalCajasVerificadas= COUNT(*) 
  FROM fContenedoresEnStock(null) c   
  INNER JOIN Inventario inv on (ISNUMERIC(inv.idPieza)=0 AND cast(replace(inv.idPieza, 'A', '') as int) = c.id)
  WHERE
  inv.fechaInicioInventario = CAST(@fechaInventario as DATE)
  AND c.idTipo= 'CAJ'    

  SELECT 
  @totalCombosVerificados= COUNT(*) 
  FROM fContenedoresEnStock(null) c   
  INNER JOIN Inventario inv on (ISNUMERIC(inv.idPieza)=0 AND cast(replace(inv.idPieza, 'A', '') as int) = c.id)
  WHERE
  inv.fechaInicioInventario = CAST(@fechaInventario as DATE)
  AND c.idTipo= 'CMB'    


  SELECT 
  @totalPiezasVerificadas = COUNT(*) 
  FROM fPiezasEnStock(null) p   
  INNER JOIN Inventario inv on (ISNUMERIC(inv.idPieza) = 1 AND CONVERT(INT, inv.idPieza) =p.id) 

  WHERE
  inv.fechaInicioInventario = CAST(@fechaInventario as DATE)


END


GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalesColeccionINV]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP obtiene un totalizado de unidades y peso
--				de todas las piezas y contenedores colectados
--				para un inventario.
--				Agrupado por producto e ubicacion
-- =============================================
CREATE PROCEDURE [dbo].[sp_getTotalesColeccionINV]
	 @fechaInventario datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
	TIPO='PIEZA',
	prd.nombre as PRODUCTO,
	d.nombre as UBICACION,
	COUNT(*) as BULTOS,
	SUM(p.pesoneto) as PESO
	
	FROM Inventario inv
	LEFT OUTER JOIN Pesadas p on isnumeric(idpieza)=1 and p.id = CONVERT(INT, replace(inv.idPieza,'A','')) 
	LEFT OUTER JOIN Productos prd on prd.id = p.idproducto 
	LEFT OUTER JOIN Destinos d on d.id = inv.idDestino
	WHERE isnumeric(inv.idPieza)=1 and  inv.fechaInicioInventario = CAST(@fechaInventario as DATE)
	GROUP BY prd.nombre,d.nombre 
	
	UNION 
	
	SELECT distinct
	tc.Descripcion as TIPO,
	prd.nombre as PRODUCTO,
	d.nombre as UBICACION,
	COUNT(*) as BULTOS,
	SUM(c.pesoneto) as PESO
			
	FROM Inventario inv
	INNER JOIN Contenedores c ON isnumeric(inv.idpieza)=0 and c.id = CONVERT(INT, replace(inv.idPieza,'A',''))
	INNER JOIN TiposContenedor tc ON tc.id=c.idTipo
	INNER JOIN Productos prd on prd.id = c.idproducto
	LEFT OUTER JOIN Destinos d on d.id = inv.idDestino
	WHERE inv.fechaInicioInventario = CAST(@fechaInventario as DATE)
	GROUP BY tc.Descripcion, prd.nombre,d.nombre 
	
	ORDER BY TIPO,UBICACION,PRODUCTO desc
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getTotalizadoProductosPedidoPendienteVentaPorPreparacion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 06-08-2022
/*

Description:	
SP obtiene todos los items de compocision (piezas o contenedores) de
todos los pedidos que sean de la misma fecha de preparacionun 
especificada. Expresando la cantidad pedida , la cantidad preparada 
y el resto.

*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_getTotalizadoProductosPedidoPendienteVentaPorPreparacion]
@fechaPreparacion date
 
AS
BEGIN
SELECT  
fs.idProducto as CodigoSAC,
fp.Descripcion as ProductoSAC,
fs.Detalle as Observacion,
SUM(fs.cantidad) as Unds_PED,
SUM(pes.unidades) as Unds_PREP,
(SUM(fs.cantidad) -SUM(pes.unidades)) as Unds_REST,
SUM(fs.CantidadUMPrimaria) as Peso_PED,
SUM(pes.PesoNeto) as Peso_PREP,
(SUM(fs.CantidadUMPrimaria) -SUM(pes.PesoNeto)) as Peso_REST

FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.V_CantidadesPedidosPendientes as pp

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	pp.PedIdCabecera = fc.IdCabecera 
	and pp.idAuxi=fc.idAuxi 
	and pp.idCtaAuxi=fc.idCtaAuxi 
	and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')  
	and fc.idPpal=1

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
	fc.IdCabecera = fs.IdCabecera 
	and pp.PedIdMovimiento = fs.IdMovimiento 
	and fs.idTipoComprobante = fc.idTipoComprobante 

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  
	fs.idProducto = fp.idProducto 
	and fp.Imputable = 1 and fp.idPlanProducto=2 

JOIN productos as prd ON  prd.codigoProductoSAC = fp.idProducto 
left outer join Pedidos ped ON ped.CodigoPedidoSAC=fc.IdCabecera
left outer join Egresos e ON e.idpedido = ped.id and e.idTipoBulto='PZA'
left outer join Pesadas pes ON pes.id = e.idpesaje and pes.idproducto=prd.id and prd.codigoProductoSAC=fp.idProducto

WHERE
(prd.esCaja is null or prd.esCaja=0) 
and (prd.esCombo is null or prd.esCombo=0) 
and (prd.esinsumo is null or prd.esinsumo=0) 
and pp.idAuxi=1
AND cast(fs.fechaVencimiento as DATE) = @fechaPreparacion
AND (not exists (select * from Pedidos where CodigoClienteSAC=fc.idCtaAuxi and CodigoPedidoSAC=fc.IdCabecera) or (ped.activo=1 or ped.activo is null))
GROUP BY fs.idProducto,fp.Descripcion,fs.detalle

UNION 
SELECT  
fs.idProducto as CodigoSAC,
fp.Descripcion as ProductoSAC,
fs.Detalle as Observacion,
SUM(fs.cantidad) as Unds_PED,
COUNT(distinct cont.id) as Unds_PREP,
(SUM(fs.cantidad) -COUNT(distinct cont.id)) as Unds_REST,
SUM(fs.CantidadUMPrimaria) as Peso_PED,
SUM(cont.PesoNeto) as Peso_PREP,
(SUM(fs.CantidadUMPrimaria) -SUM(cont.PesoNeto)) as Peso_REST

FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.V_CantidadesPedidosPendientes as pp

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
	pp.PedIdCabecera = fc.IdCabecera 
	and pp.idAuxi=fc.idAuxi 
	and pp.idCtaAuxi=fc.idCtaAuxi 
	and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2')  
	and fc.idPpal=1

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
	fc.IdCabecera = fs.IdCabecera 
	and pp.PedIdMovimiento = fs.IdMovimiento 
	and fs.idTipoComprobante = fc.idTipoComprobante  

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  
	fs.idProducto = fp.idProducto 
	and fp.Imputable = 1 
	and fp.idPlanProducto=2

JOIN productos as prd ON  prd.codigoProductoSAC = fp.idProducto 
left outer join Pedidos ped ON ped.CodigoPedidoSAC=fc.IdCabecera
left outer join Egresos e ON e.idpedido = ped.id and e.idTipoBulto='CNT'
left outer join Contenedores cont ON cont.id = e.idpesaje and cont.idproducto=prd.id and prd.codigoProductoSAC=fp.idProducto

WHERE 
pp.idAuxi=1
and (prd.esCombo=1 or prd.esCaja=1)  
AND cast(fs.fechaVencimiento as DATE) = @fechaPreparacion
AND (not exists (select * from Pedidos where CodigoClienteSAC=fc.idCtaAuxi and CodigoPedidoSAC=fc.IdCabecera) or (ped.activo=1 or ped.activo is null))
GROUP BY fs.idProducto,fp.Descripcion,fs.detalle


ORDER BY
ProductoSAC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getUnidadesStockInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 18-6-2021
-- Description:	retorna las unidades en stock de un dado insumo
-- =============================================
CREATE PROCEDURE [dbo].[sp_getUnidadesStockInsumo]
@idPrdInsumo as int,@unidades as float out,@result as bit out

AS
BEGIN
 set @result=0;
 set @unidades=0;

SELECT 
	@unidades= (ISNULL((select sum(unidades) from MovInsumos where idPrdInsumo=prd.id and idTipoMov='ING'),0)) - 
	ISNULL((select sum(unidades) from MovInsumos where idPrdInsumo=prd.id and idTipoMov='EGR'),0) 
FROM Productos prd
WHERE prd.id=@idPrdInsumo

set @result=@@ROWCOUNT

END


GO
/****** Object:  StoredProcedure [dbo].[sp_getUnidadesStockInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 18-6-2021
-- Description:	retorna las unidades en stock de insumos
-- =============================================
CREATE PROCEDURE [dbo].[sp_getUnidadesStockInsumos]

AS
BEGIN


SELECT 
prd.id as ID,
prd.nombre as INSUMO,
ISNULL((select sum(unidades) from MovInsumos where idPrdInsumo=prd.id and idTipoMov='ING'),0) - 
ISNULL((select sum(unidades) from MovInsumos where idPrdInsumo=prd.id and idTipoMov='EGR'),0) as UNDS
FROM Productos prd
WHERE prd.esinsumo=1


END


GO
/****** Object:  StoredProcedure [dbo].[sp_insertCapturaInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 13-2-2021
/*
Description:	
SP inserta en la tabla Inventario una pieza o 
contenedor colectado en el proceso de inventario
*/ 
-- =============================================
CREATE PROCEDURE [dbo].[sp_insertCapturaInv]
	 @fechaInventario datetime ,@idUbicacion int,@idPieza varchar(12)
AS
BEGIN
	SET NOCOUNT ON;
	
	If Not Exists(select * from Inventario where idPieza=@idPieza and cast(@fechaInventario as DATE) = fechaInicioInventario)
	Begin
		INSERT INTO Inventario (fechaInicioInventario,fechaHoraCaptura,idDestino,idPieza)
		values(@fechaInventario,GETDATE(),@idUbicacion,@idPieza)
	End
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_insertDbLog]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 5-3-2021
/*

Description:	
	Inserta un registro en la tabla de logs

*/ 
-- =============================================
CREATE PROCEDURE [dbo].[sp_insertDbLog]
	 @idOperador int ,@idEstacion int,@evento nvarchar(100),@contexto nvarchar(100),@detalle nvarchar(300),@result bit=0 out
AS
BEGIN
	SET NOCOUNT ON;
	set @result=0
	
	INSERT INTO dbLog (fecha_hora,idoperador,idestacion,evento,contexto,detalle)
	values(CURRENT_TIMESTAMP,@idOperador,@idEstacion,@evento,@contexto,@detalle)
	set @result= @@ROWCOUNT
END


GO
/****** Object:  StoredProcedure [dbo].[sp_insertResulInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 22-2-2021
-- Description:	SP inserta en la tabla resultInventario 
--				un nuevo registro con los resultados de 
--				un inventario. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_insertResulInventario]
		@fechaInventario datetime ,
		@idPedidoAjuste int, --id del pedido que se tuvo que generar para realizar egresos por el ajuste
		@totalPiezasVerificadasEnStock int,
		@pzas_sinSTLconSTF int,
		@pzas_conSTLsinSTF int,
		@ajustPzas_sinSTLconSTF int,
		@ajustPzas_conSTLsinSTF int,
		@pzas_fueraContenedor_SinStock int,
		@pzas_fueraContenedor_ConStock int,
		@ajustPzas_fueraContenedor_SinStock int,
		@ajustPzas_fueraContenedor_ConStock int,
		@totalCajasVerificadasEnStock int,
		@cajas_sinSTLconSTF int,
		@cajas_conSTLsinSTF int,
		@ajustCajas_sinSTLconSTF int,
		@ajustCajas_conSTLsinSTF int,
		@totalCombosVerificadosEnStock int,
		@combos_sinSTLconSTF int,
		@combos_conSTLsinSTF int,
		@ajustCombos_sinSTLconSTF int,
		@ajustCombos_conSTLsinSTF int,
		@cantBultosNoExisten int,
		@cantBultosEnPedidosAbiertos int
AS
BEGIN
	--SET NOCOUNT ON;

	--borra el resultado anterior si es que existe la misma fecha de inventario
	DELETE resultInventario WHERE CAST(fechaInventario as DATE) = CAST(@fechaInventario as DATE)
	
	INSERT INTO resultInventario 
		(fechaAjuste,
		fechaInventario,
		idPedidoAjuste,
		TotalPiezasVerificadasEnStock,
		pzas_sinSTLconSTF,
		pzas_conSTLsinSTF,
		ajustPzas_sinSTLconSTF,
		ajustPzas_conSTLsinSTF,
		TotalCajasVerificadasEnStock,
		cjas_sinSTLconSTF,
		cjas_conSTLsinSTF,
		ajustCjas_sinSTLconSTF,
		ajustCjas_conSTLsinSTF,
		TotalCombosVerificadosEnStock,
		cmbs_sinSTLconSTF,
		cmbs_conSTLsinSTF,
		ajustCmbs_sinSTLconSTF,
		ajustCmbs_conSTLsinSTF,
		cantBultosNoExisten,
		pzas_fueraContenedorSinStock,
		pzas_fueraContenedorConStock,
		ajustPzas_fueraContenedorSinStock,
		ajustPzas_fueraContenedorConStock,
		cantBultosEnPedidosAbiertos)
	
	values(	GetDate(),
			@fechainventario,
			@idPedidoAjuste,
			@totalPiezasVerificadasEnStock,
			@pzas_sinSTLconSTF,
			@pzas_conSTLsinSTF,
			@ajustPzas_sinSTLconSTF,
			@ajustPzas_conSTLsinSTF,
			@totalCajasVerificadasEnStock,
			@cajas_sinSTLconSTF,
			@cajas_conSTLsinSTF,
			@ajustCajas_sinSTLconSTF,
			@ajustCajas_conSTLsinSTF,
			@totalCombosVerificadosEnStock,
			@combos_sinSTLconSTF,
			@combos_conSTLsinSTF,
			@ajustCombos_sinSTLconSTF,
			@ajustCombos_conSTLsinSTF,
			@cantBultosNoExisten,
			@pzas_fueraContenedor_SinStock,
			@pzas_fueraContenedor_ConStock,
			@ajustPzas_fueraContenedor_SinStock,
			@ajustPzas_fueraContenedor_ConStock,
			@cantBultosEnPedidosAbiertos)
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarDevolucionContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
/* Description:

	Realiza la tarea de registrar un contenedor como Devolucion.
	Para el registro del contenedor en la tabla Devoluciones se 
	hereda el idpedido que tiene el contenedor en su ultimo egreso.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_registrarDevolucionContenedor]
	 @idContenedor int,@idEstacion as int, @idOperador as int,@rowsAfected as int =0 out
AS
SET NOCOUNT OFF 

BEGIN
		INSERT INTO Devoluciones (fecha_hora,idEstacion,idOperador,idpesaje,idpedido,idTipoBulto) 
		SELECT CURRENT_TIMESTAMP ,@idEstacion,@idOperador,@idContenedor,e.idPedido,'CNT'
		FROM  
		(
			SELECT * 
			FROM
			(	--ultimo contenedor egresado 			
				SELECT
				idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
				FROM egresos
				WHERE 
				idTipoBulto='CNT'
				AND idpesaje=@idContenedor
			)cntEgres
			WHERE
			cntEgres.max_date=cntEgres.fecha_hora
		)e 
		WHERE 
		NOT EXISTS
		(
			select * from devoluciones
			where idpedido = e.idPedido and idpesaje=@idContenedor and idTipoBulto='CNT' 
		)
    SET @rowsAfected = @@ROWCOUNT
END

GO
/****** Object:  StoredProcedure [dbo].[sp_registrarDevolucionPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
/* Description:

	Realiza la tarea de reingresar una pieza a stock por una devolucion.
	Una pieza puede tener devolucion en dos situaciones:
	-Porque egreso como pieza.
	-Porque egreso como contenedor.
	Si la pieza que requiere una devolucion egresó como pieza , la devolucion se realiza con las propiedad IDPEDIDO
	del egreso.
	Si la pieza que requiere una devolucion egresó como contenido de una contenedor , la devolucion se realiza con las propiedad
	IDPEDIDO del contenedor.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_registrarDevolucionPieza]
	 @idPieza int,@idEstacion as int, @idOperador as int,@result as bit =0 out
AS
SET NOCOUNT OFF 

BEGIN
set @result=0

--Si la pieza es parte de un contenedor activo y el contenedor esta egresado
--y la pieza no tiene egreso con fecha superior al egreso del contenedor.
IF EXISTS	(	
				SELECT * 
				FROM Contenedores c
				JOIN ContenedorPiezas cp on c.id=cp.idcontenedor
				JOIN 
				(
					SELECT * 
					FROM
					(	--ultimo contenedor egresado 			
						SELECT
						idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
						FROM egresos
						WHERE 
						idTipoBulto='CNT'
					)cntEgres
					WHERE
					cntEgres.max_date=cntEgres.fecha_hora
				)e ON c.id= e.idpesaje
				WHERE 
				cp.idpesaje = @idPieza
				AND c.fecha_desarmado is null
				AND cp.idpesaje not in 
				(
					SELECT idpesaje
					FROM egresos
					WHERE
					idTipoBulto='PZA'
					AND idpesaje=@idPieza
					AND fecha_hora > e.fecha_hora
				)
			)
	BEGIN
		INSERT INTO Devoluciones (fecha_hora,idEstacion,idOperador,idpesaje,idpedido,idTipoBulto) 
		SELECT CURRENT_TIMESTAMP ,@idEstacion,@idOperador,@idPieza,e.idPedido,'PZA'
		FROM Contenedores c
		JOIN ContenedorPiezas cp on c.id=cp.idcontenedor
		JOIN 
		(
			SELECT * 
			FROM
			(	--ultimo contenedor egresado 			
				SELECT
				idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
				FROM egresos
				WHERE 
				idTipoBulto='CNT'
			)cntEgres
			WHERE
			cntEgres.max_date=cntEgres.fecha_hora
		)e ON c.id= e.idpesaje
		WHERE 
		cp.idpesaje = @idPieza
		AND c.fecha_desarmado is null
		AND NOT EXISTS
		(
			select * from devoluciones
			where idpedido = e.idPedido and idpesaje=@idPieza and idTipoBulto='PZA' 
		)
	END
ELSE -- si la pieza ya posee egreso como pieza.
	BEGIN
		INSERT INTO Devoluciones (fecha_hora,idEstacion,idOperador,idpesaje,idpedido,idTipoBulto) 
		SELECT CURRENT_TIMESTAMP ,@idEstacion,@idOperador,@idPieza,e.idPedido,'PZA'
		FROM
		(   --ultima pieza egresada
			SELECT * FROM
			(				
				SELECT
				idpesaje,idpedido,idtipobulto,fecha_hora,max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje,idtipobulto)
				FROM egresos
				
				WHERE 
				idTipoBulto='PZA'
				AND idpesaje = @idPieza 
			)pzaEgres
			WHERE
			pzaEgres.max_date=pzaEgres.fecha_hora
		)e
		WHERE NOT EXISTS
		(
			select * from devoluciones
			where idpedido = e.idPedido and idpesaje=@idPieza and idTipoBulto='PZA' 
		)
	END

    SET @result = @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarEgresoContenedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 19-2-2021
/* Description:

	Realiza la tarea de generar un registro de egreso para 
	un contenedor.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_registrarEgresoContenedor]
	 @idContenedor int,@idPedido int ,@idEstacion as int, @idOperador as int,@result as bit =0 out
AS
SET NOCOUNT OFF 

BEGIN
set @result=0

		INSERT INTO Egresos (idpesaje,idPedido,fecha_hora,idEstacion,idOperador,idTipoBulto) 
		values(@idContenedor,@idPedido,CURRENT_TIMESTAMP,@idEstacion,@idOperador,'CNT')

    SET @result = @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarEgresoPieza]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 19-2-2021
/* Description:

	Realiza la tarea de generar un registro de egreso para 
	una pieza.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_registrarEgresoPieza]
	 @idPieza int,@idPedido int ,@idEstacion as int, @idOperador as int,@result as bit =0 out
AS
SET NOCOUNT OFF 

BEGIN
set @result=0

		INSERT INTO Egresos (idpesaje,idPedido,fecha_hora,idEstacion,idOperador,idTipoBulto) 
		values(@idPieza,@idPedido,CURRENT_TIMESTAMP,@idEstacion,@idOperador,'PZA')

    SET @result = @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarInsumoProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 01-3-2021
/* Description:
	Realiza la tarea de registrar una nueva asociacion de un insumo para 
	un producto.
	Si los parametros de productoInsumoPrimario = productoInsumoSecundario se 
	trata de un insumo primario del producto padre
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_registrarInsumoProducto]
	 @idProducto int,@idInsumoPrimario as int, @idInsumoSecundario as int,@unidades as float,@requiereConfirmacion as bit,@result as bit out
AS
SET NOCOUNT OFF 

BEGIN
		set @result=0
		
		IF NOT EXISTS
		(
			SELECT * from ProductoInsumos
			WHERE
			idProducto=@idProducto 
			and idInsumoPrimario=@idInsumoPrimario
			and idInsumoSecundario=@idInsumoSecundario
		)
		BEGIN
			INSERT INTO ProductoInsumos (idProducto,idInsumoPrimario,idInsumoSecundario,unidades,requiereConfirmacion) 
			values (@idProducto,@idInsumoPrimario,@idInsumoSecundario,@unidades,@requiereConfirmacion)
			SET @result = @@ROWCOUNT
		END
		
END

GO
/****** Object:  StoredProcedure [dbo].[sp_registrarMovInsumo]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 02-3-2021
/* Description:
	Registra un movimiento de ingreso/egreso de un insumo generado por los
	posibles procesos:
	- proceo de ingreso a planta
	- pesaje de piezas
	- conformado de contenedores
	- preparacion de pedidos.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_registrarMovInsumo]
		@idTipoMov char(3),				--'ING' , 'EGR'
		@idTipoProc char(3),			--'IPL' , 'PZA' , 'CNT' , 'PED'
		@idProc int,					-- identificador unico en el tipo de proceso
		@idPrdInsumo int ,				-- codigo de producto de tipo insumo
		@unidades float ,				-- unidades a registrar
		@result as bit out				-- = 1 si ok
AS
SET NOCOUNT OFF 

BEGIN
		set @result=0
		
		IF NOT EXISTS
		(
			SELECT * from MovInsumos
			WHERE
			idTipoMov =@idTipoMov 
			and idTipoProc=@idTipoProc
			and idProc=@idProc
			and idPrdInsumo=@idPrdInsumo
		)
		BEGIN
			INSERT INTO MovInsumos (idTipoMov,idTipoProc,idProc,idPrdInsumo,unidades,fecha_hora) 
			values (@idTipoMov,@idTipoProc,@idProc,@idPrdInsumo,@unidades,CURRENT_TIMESTAMP)
			SET @result = @@ROWCOUNT
		END
		ELSE
		BEGIN
			UPDATE MovInsumos set unidades = unidades + @unidades ,fecha_hora=CURRENT_TIMESTAMP
			WHERE 		
			idTipoMov =@idTipoMov 
			and idTipoProc=@idTipoProc
			and idProc=@idProc
			and idPrdInsumo=@idPrdInsumo
			SET @result = @@ROWCOUNT
		END
		
END

GO
/****** Object:  StoredProcedure [dbo].[sp_repDetalleEgresosFull]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 06-08-2022
-- Description:	obtiene el informe del detalle de las piezas , cajas y combos egresados
-- informando los datos del pedido y de la pieza.
-- Se podran aplicar filtros por Nombre de cliente y/o rango de fechas
-- Numero de comprobante del pedido y Numero de Lote.

--Parametros:
/*
@cliente: Nombre del cliente. Si es '' deshabilita rango de fechas 
@desde:					Fecha desde en formato yyyy-mm-dd . colocar '' si no debe 
						especificar rango de fechas para un cliente especificado.
@hasta:					Fecha hasta en formato yyyy-mm-dd . colocar '' si no aplica.
@comprobantePedido: 	declara numero de Comprobante o '' si no es requerido. 
@lote: 					Declara numero de Lote o '' si no es requerido. 

Si cliente esta definido se podra hacer uso o no del rango de fechas.
Si cliente no esta definido(='') no tiene efecto el rango de fechas.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_repDetalleEgresosFull]
@cliente varchar(40)='',@desde varchar(10)='' , @hasta varchar(10)='',@comprobantePedido char(12)='',@lote varchar(10)='',
@tipoBulto as varchar(10)='',@idTipoProducto as int =0,@idProducto int =0
 
AS
BEGIN

SELECT * FROM 
(
	SELECT 
	TIPO='PIEZA',
	pe.id as NRO,
	(YEAR(pe.fecha_hora) + MONTH(pe.fecha_hora) * 10000  + DAY(pe.fecha_hora)*1000000)as LOTE,
	p.ComprobantePedidoSAC  as PEDIDO,
	(CASE WHEN p.activo is null or p.activo=1 THEN 'ABIERTO' ELSE 'CERRADO' END ) as ESTADO,
	fc.nombreTercero as CLIENTE,
	prd.codigoProductoSAC as COD_PRD,
	tp.nombre as TIPO_PRD,
	prd.nombre as PRODUCTO,
	pe.numTropa as TROPA,
	tip.nombre as TIPIF,
	de.nombre as ORIGEN,
	e.fecha_hora as EGRESO,   
	pe.unidades as UNDS,
	pe.pesoNeto as NETO,
	ope.nombre as OPERADOR    
	FROM EGRESOS as e   
	LEFT OUTER JOIN operadores ope ON e.idOperador = ope.id   
	LEFT OUTER JOIN Pesadas as pe ON e.idpesaje = pe.id   
	LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id
	LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id
	LEFT OUTER JOIN Pedidos as p ON e.idpedido = p.id   
	LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id
	LEFT OUTER JOIN Tipificaciones as tip ON pe.idTipificacion = tip.id
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
		p.CodigoPedidoSAC = fc.IdCabecera 
		and fc.idAuxi=1 
		and p.CodigoClienteSAC=fc.idCtaAuxi 
		and (fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2') 
		and fc.idPpal=1
	WHERE
	e.idTipoBulto ='PZA'
	and (@cliente = '' or (@cliente = fc.nombreTercero))
	and ((@desde ='') or @comprobantePedido <> '' or cast(e.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102))
	and (@cliente <> '' or  (@comprobantePedido='' or  @comprobantePedido=p.ComprobantePedidoSAC)) 
	and (@lote ='' or convert(date,@lote,102) = cast(pe.fecha_hora as date) )
	and (@idTipoProducto=0 OR @idTipoProducto=prd.idtipo)
	and (@idProducto=0 OR @idProducto=prd.id)

	UNION

	SELECT 
	tc.Descripcion as TIPO,
	ca.id as NRO,
	(YEAR(ca.fecha_hora) + MONTH(ca.fecha_hora) * 10000  + DAY(ca.fecha_hora)*1000000)as LOTE,
	p.ComprobantePedidoSAC  as PEDIDO,
	(CASE WHEN p.activo is null or p.activo=1 THEN 'ABIERTO' ELSE 'CERRADO' END ) as ESTADO,
	fc.nombreTercero as CLIENTE,
	prd.codigoProductoSAC as COD_PRD,
	tp.nombre as TIPO_PRD,
	prd.nombre as PRODUCTO,
	TROPA=null,
	TIPIF=null,
	de.nombre as ORIGEN,
	e.fecha_hora as EGRESO,   
	ca.unidades as UNDS,
	ca.pesoNeto as NETO,
	ope.nombre as OPERADOR    
	FROM CONTENEDORES ca    
	JOIN EGRESOS as e ON e.idpesaje = ca.id
	JOIN TiposContenedor tc ON tc.id=ca.idTipo   
	LEFT OUTER JOIN operadores ope ON e.idOperador = ope.id   
	LEFT OUTER JOIN Productos as prd ON ca.idproducto = prd.id   
	LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id
	LEFT OUTER JOIN Pedidos as p ON e.idpedido = p.id   
	LEFT OUTER JOIN Destinos as de ON ca.iddestino = de.id    
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
		p.CodigoPedidoSAC = fc.IdCabecera and 
		fc.idAuxi=1 and 
		p.CodigoClienteSAC	=fc.idCtaAuxi and 
		(fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2') and 
		fc.idPpal=1
	WHERE
	e.idtipobulto='CNT'
	and (@cliente = '' or (@cliente = fc.nombreTercero))
	and (@desde ='' or @comprobantePedido <>'' or cast(e.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102)) 
	and (@cliente <> '' or  (@comprobantePedido='' or  @comprobantePedido=p.ComprobantePedidoSAC)) 
	and (@lote ='' or convert(date,@lote,102) = cast(ca.fecha_hora as date) )
	and (@idTipoProducto=0 OR @idTipoProducto=prd.idtipo)
	and (@idProducto=0 OR @idProducto=prd.id)
)T
ORDER BY T.EGRESO desc


END
GO
/****** Object:  StoredProcedure [dbo].[sp_repDevoluciones]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
/*
	Author:			Sergio Fasolo
	Create date:	01-02-2021
	Description:	Obtiene las devoluciones existentes coincidentes
	segun filtros de fechas , cliente y comprobante.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_repDevoluciones]
@cliente varchar(40)='',@desde varchar(10)='' , @hasta varchar(10)='',@comprobantePedido char(12)=''

AS
BEGIN
SELECT
  TIPO = 'PIEZA',
  d.idpesaje as NRO,
  d.fecha_hora as FECHA,
  p.PesoNeto as PESO_NETO,
  prd.nombre as PRODUCTO,
  fc.NombreTercero as CLIENTE,
  ped.ComprobantePedidoSAC as COMPROBANTE

  FROM DEVOLUCIONES d   
  JOIN Pesadas as p ON d.idpesaje = p.id   
  JOIN Pedidos as ped ON d.idpedido = ped.id   
  LEFT OUTER JOIN Productos as prd ON p.idproducto = prd.id   
  LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  ped.CodigoPedidoSAC = fc.IdCabecera 

  WHERE 
  d.idtipobulto='PZA'
  and ((@comprobantePedido='' and
  (@cliente = '' or (@cliente = fc.nombreTercero))
  and ((@desde ='') or cast(d.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102)))
  or (@comprobantePedido <> '' and @comprobantePedido=fc.numero)) 

  UNION
  
  SELECT 
	tc.Descripcion as TIPO,
	d.idpesaje as NRO,
	d.fecha_hora as FECHA,
	ca.pesoNeto as PESO_NETO,
	prd.nombre as PRODUCTO,
	fc.NombreTercero as CLIENTE,
	ped.ComprobantePedidoSAC as COMPROBANTE
	
	FROM DEVOLUCIONES d    
	JOIN Contenedores ca ON ca.id=d.idpesaje
	JOIN TiposContenedor tc ON ca.idtipo = tc.id
    JOIN Pedidos as ped ON d.idpedido = ped.id  
	LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  ped.CodigoPedidoSAC = fc.IdCabecera 
	
	WHERE 
	d.idtipobulto='CNT'
	and ((@comprobantePedido='' and
	(@cliente = '' or (@cliente = fc.nombreTercero))
	and ((@desde ='') or cast(d.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102)))
	or (@comprobantePedido <> '' and @comprobantePedido=fc.numero)) 

  ORDER BY TIPO,FECHA

END
GO
/****** Object:  StoredProcedure [dbo].[sp_repEgresosTotalizadosFullPorProductoPorFecha]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
/*
	Author:			Sergio Fasolo
	Create date:	15-2-2021
	Description:	Obtiene el totalizado de egresos de despachos
	informando piezas,cajas y combos con agrupado por productos.  
	
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_repEgresosTotalizadosFullPorProductoPorFecha]
@cliente varchar(40)='',@desde varchar(10)='' , @hasta varchar(10)='',@comprobantePedido char(12)='',
@tipoBulto as varchar(10)='',@idTipoProducto as int =0,@idProducto int =0

 
AS
BEGIN
SELECT
  ped.ComprobantePedidoSAC as PEDIDO,
  fc.nombreTercero as CLIENTE,
  TIPO = 'PIEZA',
  tp.nombre as TIPO_PRD,
  prd.nombre as PRODUCTO,
  prd.codigoProductoSAC as CODIGO,
  COUNT(*) as BULTOS,
  SUM(p.unidades) as UNIDADES,
  SUM(p.PesoNeto) as PESO_NETO    
  FROM EGRESOS e   
  JOIN Pesadas as p ON e.idpesaje = p.id   
  JOIN Pedidos as ped ON e.idpedido = ped.id   
  LEFT OUTER JOIN Productos as prd ON p.idproducto = prd.id
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id
  LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  ped.CodigoPedidoSAC = fc.IdCabecera 

  WHERE
  e.idTipoBulto='PZA' 
  and (@cliente = '' or (@cliente = fc.nombreTercero))
  and (@desde ='' or @comprobantePedido <> '' 
	or cast(e.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102))
  and (@comprobantePedido = '' or @comprobantePedido=ped.ComprobantePedidoSAC) 
  AND (@idTipoProducto=0 OR @idTipoProducto=prd.idtipo)
  AND (@idProducto=0 OR @idProducto=prd.id)
  AND (@tipoBulto='' OR @tipoBulto='PIEZA')
 
  GROUP BY prd.codigoProductoSAC,tp.nombre, prd.nombre,ped.ComprobantePedidoSAC,fc.nombreTercero


  UNION
  
  SELECT 
    ped.ComprobantePedidoSAC as PEDIDO,
	fc.nombreTercero as CLIENTE,
	tc.Descripcion as TIPO,
	tp.nombre as TIPO_PRD,
	prd.nombre as PRODUCTO,
	prd.codigoProductoSAC as CODIGO,
	COUNT(*) as BULTOS,
	SUM(ca.unidades) as UNIDADES,
	SUM(ca.pesoNeto) as PESO_NETO    
	
	FROM CONTENEDORES ca    
	JOIN Egresos e ON e.idpesaje = ca.id AND e.idTipoBulto = 'CNT'
	JOIN TiposContenedor tc ON ca.idtipo = tc.id
    JOIN Pedidos as ped ON e.idpedido = ped.id  
	LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
    LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id
	LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  ped.CodigoPedidoSAC = fc.IdCabecera 
	
	WHERE 
	(@cliente = '' or (@cliente = fc.nombreTercero))
	and (@desde ='' or @comprobantePedido <> '' 
		or cast(e.fecha_hora as date) between convert(date,@desde,102) and convert(date,@hasta,102))
	and (@comprobantePedido = '' or @comprobantePedido=ped.ComprobantePedidoSAC) 
	AND (@idTipoProducto=0 OR @idTipoProducto=prd.idtipo)
	AND (@idProducto=0 OR @idProducto=prd.id)
	AND (@tipoBulto='' OR @tipoBulto=tc.Descripcion)

  GROUP BY tc.Descripcion,tp.nombre,prd.nombre,prd.codigoProductoSAC,ped.ComprobantePedidoSAC,fc.nombreTercero

  ORDER BY ped.ComprobantePedidoSAC,TIPO,prd.nombre

END
GO
/****** Object:  StoredProcedure [dbo].[sp_repEgresosTotalizadoXDiaCliente]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	retorna el totalizado de unidades y peso
--				de los articulos por Dia-Cliente en el rango
--				de fechas especificado 
-- =============================================
CREATE PROCEDURE [dbo].[sp_repEgresosTotalizadoXDiaCliente]
@desde date ,@hasta date,@idCliente varchar(12) =''
 
AS
BEGIN
  SELECT 
  cast(DIA as datetime) as DIA,
  CLIENTE,
  SUM(UNDS) as UNDS,
  SUM(NETO) as NETO
  FROM
  (
	  SELECT 
	  cast(e.fecha_hora as date) as DIA,
	  cli.Nombre as CLIENTE,
	  pe.unidades as UNDS,
	  pe.pesoNeto as NETO
	  FROM pesadas as pe    
	  JOIN Egresos as e ON pe.id=e.idpesaje and e.idTipoBulto='PZA'
	  JOIN Pedidos as ped ON e.idPedido=ped.id
	  JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as cli ON 
		ped.CodigoClienteSAC=cli.idCtaAuxi  AND 
		cli.idPpal=1 AND 
		cli.idAuxi=1 AND 
		cli.Imputable=1
	  WHERE
	  cast(e.fecha_hora as DATE) between @desde and @hasta 
	  AND (@idCliente='' or cli.idCtaAuxi = @idCliente)
	  UNION ALL
	  SELECT 
	  cast(e.fecha_hora as date) as DIA,
	  cli.Nombre as CLIENTE,
	  cont.unidades as UNDS,
	  cont.pesoNeto as NETO
	  FROM Contenedores as cont    
	  JOIN Egresos as e ON cont.id=e.idpesaje and e.idTipoBulto='CNT'
	  JOIN Pedidos as ped ON e.idPedido=ped.id
	  JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as cli ON 
		ped.CodigoClienteSAC=cli.idCtaAuxi  AND 
		cli.idPpal=1 AND 
		cli.idAuxi=1 AND
		cli.Imputable=1
	  WHERE
	  cast(e.fecha_hora as DATE) between @desde and @hasta 
	  AND (@idCliente='' or cli.idCtaAuxi = @idCliente)
  )tb
   
  GROUP BY 
  DIA,
  CLIENTE
  ORDER BY 
  DIA
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repEventosLog]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 4-3-2021
-- Description:
/*				Genera un reporte basado en todos los eventos de log 
				regisrados en un rango de fechas indicado.
				Se podra buscar un texto de aproximacion para la columna
				detalle.  
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_repEventosLog]
@desde date ,@hasta date,@detalle varchar(100) =''
 
AS
BEGIN
	SELECT lg.fecha_hora as FECHA,o.nombre as OPERADOR, lg.idEstacion as ESTACION,lg.evento as EVENTO,lg.contexto as CONTEXTO,lg.detalle as DETALLE
	FROM dbLog lg
	JOIN operadores o on o.id=lg.idoperador
	WHERE
	cast(lg.fecha_hora as date)  between @desde and @hasta
	AND (@detalle = '' OR lg.detalle  like '%'+@detalle+'%')
	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockContenedoresTotalizadoPorDestino]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
-- Description:	retorna los contenedores (cajas o combos) 
--              existentes en stock con totalizado en cantidad 
--              de contenedores por tipo(caja-combo) por producto y por destino.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockContenedoresTotalizadoPorDestino]
@idTipoProducto int=0,@idProducto int=0, @hasta date = null,@idUbicacion int=0
 
AS
BEGIN
declare @fechaHasta date = ISNULL(@hasta,GetDate())
declare @idTipoPrd int = @idTipoProducto
declare @idPrd int = @idProducto
declare @idUbi int = @idUbicacion


SELECT 
tc.Descripcion as CONTENEDOR,
prd.nombre as PRODUCTO,
de.nombre as DESTINO,
COUNT(*) as BULTOS,
sum(ca.unidades) as UNIDADES,
sum(ca.pesoNeto) as PESONETO    
FROM fContenedoresEnStock(@fechaHasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN destinos de ON ca.iddestino = de.id    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
(@idTipoPrd = 0 or @idTipoPrd=prd.idtipo)
AND (@idPrd = 0 or @idPrd=prd.id)
AND (@idUbi=0 or @idUbi=ca.iddestino)

GROUP BY tc.Descripcion,prd.nombre,de.nombre
ORDER BY tc.Descripcion,prd.nombre,de.nombre asc
END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalle]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-02-2021
-- Description:	retorna un detalle de cada bulto (pieza o contenedor)
--				que se encuentre en stock a la fecha.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockDetalle]
@idTipoProducto int=0,@idProducto int=0,@hasta date = null,@idUbicacion int=0
 
AS
BEGIN
declare @fechaHasta date = ISNULL(@hasta,GetDate())
declare @tipoPrd int = @idTipoProducto
declare @idPrd int = @idProducto
declare @idUbi int = @idUbicacion
	
SELECT
TIPO='PIEZA',
p.id as NRO ,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,p.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
p.numTropa as TROPA,
tip.nombre as TIPIF,
p.unidades as UNIDADES,
p.PesoNeto as NETO    

FROM fPiezasEnStock(@fechaHasta) as p 
	JOIN productos as prd ON prd.id = p.idproducto
	JOIN destinos as de ON de.id = p.idDestino
	LEFT OUTER JOIN Tipificaciones as tip ON tip.id=p.idTipificacion
WHERE 
	(@tipoPrd=0 or @tipoPrd=prd.idtipo)
	and (@idPrd=0 or @idPrd=prd.id)
	and (@idUbi=0 or @idUbi=p.idDestino)

UNION 

SELECT   
tc.Descripcion as TIPO,
ca.id as NRO,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,ca.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
TROPA=null,
TIPIF=null,
ca.unidades as UNIDADES,
ca.pesoneto as PESONETO    

FROM fContenedoresEnStock(@fechaHasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN destinos de ON ca.iddestino = de.id    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
(@tipoPrd=0 or @tipoPrd=prd.idtipo)
and (@idPrd=0 or @idPrd=prd.id)
and (@idUbi=0 or @idUbi=ca.idDestino)

ORDER BY TIPO,UBICACION asc

END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-02-2021
-- Description:	retorna un detalle de cada bulto (pieza o contenedor)
--				que se encuentre en stock a la fecha ordenado
--				por fecha de vencimiento .  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento]
@idTipoProducto int=0,@idProducto int=0,@hasta date = null,@idUbicacion int=0
 
AS
BEGIN
declare @fechaHasta date = ISNULL(@hasta,GetDate())
declare @tipoPrd int = @idTipoProducto
declare @idPrd int = @idProducto
declare @idUbi int = @idUbicacion

SELECT
TIPO='PIEZA',
p.id as NRO ,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,p.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
p.unidades as UNIDADES,
p.PesoNeto as NETO,
DATEADD(day,prd.diasvencimiento,p.fecha_hora) as VENCIMIENTO
FROM fPiezasEnStock(@fechaHasta) as p , productos as prd ,destinos as de   
WHERE 
p.idproducto = prd.id 
and de.id = p.iddestino 
and (@tipoPrd=0 or @tipoPrd=prd.idtipo)
and (@idPrd=0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=p.iddestino)


UNION 

SELECT   
tc.Descripcion as TIPO,
ca.id as NRO,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,ca.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
ca.unidades as UNIDADES,
ca.pesoneto as PESONETO,
ca.fecha_vencimiento as VENCIMIENTO

FROM fContenedoresEnStock(@fechaHasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN destinos de ON ca.iddestino = de.id    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
(@tipoPrd=0 or @tipoPrd=prd.idtipo)
and (@idPrd=0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=ca.iddestino)

ORDER BY VENCIMIENTO asc

END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalleProximidadVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 4-08-2021
-- Description:	retorna un detalle de cada bulto (pieza o contenedor)
--				que se encuentre en stock en donde su fecha de venimiento esta
--				dentro de los dias proximos a vencer especificados en el parametro
--				global DiasProximidadVencimiento.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockDetalleProximidadVencimiento]
@idTipoProducto int=0,@idProducto int=0,@hasta date = null,@idUbicacion int=0
 
AS
BEGIN
declare @fechaHasta date = ISNULL(@hasta,GetDate())
declare @tipoPrd int = @idTipoProducto
declare @idPrd int = @idProducto
declare @idUbi int = @idUbicacion

declare @diasProximidadVencimiento int=0;
SELECT @diasProximidadVencimiento=DiasProximidadVencimiento FROM Parametros


SELECT
TIPO='PIEZA',
p.id as NRO ,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,p.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
p.unidades as UNIDADES,
p.PesoNeto as NETO,
DATEADD(day,prd.diasvencimiento,p.fecha_hora) as VENCIMIENTO
FROM fPiezasEnStock(@fechaHasta) as p , productos as prd ,destinos as de   
WHERE 
p.idproducto = prd.id 
and de.id = p.iddestino 
and (@tipoPrd=0 or @tipoPrd=prd.idtipo)
and (@idPrd=0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=p.iddestino)
and DATEDIFF(day,@fechaHasta,DATEADD(day,prd.diasvencimiento,p.fecha_hora)) <= @diasProximidadVencimiento  
UNION 

SELECT   
tc.Descripcion as TIPO,
ca.id as NRO,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,ca.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
ca.unidades as UNIDADES,
ca.pesoneto as PESONETO,    
ca.fecha_vencimiento as VENCIMIENTO
FROM fContenedoresEnStock(@fechaHasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN destinos de ON ca.iddestino = de.id    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
(@tipoPrd=0 or @tipoPrd=prd.idtipo)
and (@idPrd=0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=ca.iddestino)
and DATEDIFF(day,@fechaHasta,ca.fecha_vencimiento) <= @diasProximidadVencimiento  

ORDER BY VENCIMIENTO asc

END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockFullTotalizadoPorDestino]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-02-2021
-- Description:	retorna las piezas y contenedores (cajas o combos) 
--              existentes en stock
--				con totalizado en cantidad de bultos,unidades y peso 
--				por producto y por destino.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockFullTotalizadoPorDestino]
@idTipoProducto int=0,@idProducto int=0,@hasta date = null,@idUbicacion int=0
 
AS
BEGIN
declare @fechaHasta date = ISNULL(@hasta,GetDate())
declare @idTipoPrd int = @idTipoProducto
declare @idPrd int = @idProducto
declare @idUbi int = @idUbicacion


SELECT * FROM(
SELECT 
tc.Descripcion as TIPO,
prd.nombre as PRODUCTO,
prd.codigoProductoSAC as CODIGO,
de.nombre as UBICACION,
COUNT(*) as BULTOS,
SUM(ca.unidades) as UNIDADES,
SUM(ca.pesoNeto) as PESONETO    

FROM fContenedoresEnStock(@fechaHasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN destinos de ON ca.iddestino = de.id    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
cast(ca.fecha_hora as DATE) <= @fechaHasta 
AND (@idTipoPrd = 0 or @idTipoPrd=prd.idtipo)
AND (@idPrd = 0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=ca.iddestino)
GROUP BY tc.Descripcion,prd.nombre,prd.codigoProductoSAC,de.nombre

UNION 

SELECT
TIPO='PIEZA',
prd.nombre as PRODUCTO,
prd.codigoProductoSAC as CODIGO,
de.nombre as UBICACION ,
COUNT(*)as BULTOS,
SUM(p.unidades)as TOTAL_UNDS,
SUM(p.PesoNeto) as PESONETO

FROM fPiezasEnStock(@fechaHasta) as p,productos as prd ,destinos as de    
WHERE  
p.idproducto = prd.id and p.iddestino=de.id  
AND (@idTipoPrd = 0 or @idTipoPrd=prd.idtipo)
AND (@idPrd = 0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=p.iddestino)

group by prd.codigoProductoSAC, prd.nombre,de.nombre    
)EX
order by TIPO,UBICACION,PRODUCTO
END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockInsumos]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	retorna los insumos en stock
--				hasta fecha.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockInsumos]
@hasta date = null,@idPrdInsumo int=0
 
AS
BEGIN
SET @hasta = ISNULL(@hasta,GetDate())

SELECT prd.nombre as INSUMO , (SUM(t.UNIDADES_ING) - SUM(t.UNIDADES_EGR)) as UNDS
FROM 
(
	SELECT idPrdInsumo as IDPRDINSUMO,SUM(unidades) as UNIDADES_ING,UNIDADES_EGR=0 
	FROM MovInsumos
	WHERE idTipoMov='ING'
	AND (@idPrdInsumo=0 or @idPrdInsumo=idPrdInsumo)
	AND cast(fecha_hora as DATE) <= @hasta

	GROUP BY idPrdInsumo
	UNION
	SELECT idPrdInsumo as IDPRDINSUMO,UNIDADES_ING=0,SUM(unidades) as UNIDADES_EGR 
	FROM MovInsumos
	WHERE idTipoMov='EGR'
	AND (@idPrdInsumo=0 or @idPrdInsumo=idPrdInsumo)
	AND cast(fecha_hora as DATE) <= @hasta
	GROUP BY idPrdInsumo
)t
JOIN Productos prd on prd.id=t.IDPRDINSUMO
GROUP BY prd.nombre

END


GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 15-2-2021
-- Description:	retorna las piezas existentes en stock
--				con totalizado por producto y sector
--              fecha hasta .  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repExistenciaEnStockTotalizado]
@idTipoProducto int=0,@idProducto int=0,@hasta date = null,@idUbicacion int=0
 
AS
BEGIN
declare @fechaHasta date = ISNULL(@hasta,GetDate())
declare @idTipoPrd int = @idTipoProducto
declare @idPrd int = @idProducto
declare @idUbi int = @idUbicacion

SELECT 
TIPO='PIEZA',
tp.nombre as TIPO_PRD,
prd.nombre as PRODUCTO,
prd.codigoProductoSAC as CODIGO,
COUNT(*)as BULTOS,   
SUM(p.unidades)as UNIDADES,
SUM(p.PesoNeto) as PESO_NETO
FROM fPiezasEnStock(@fechaHasta) as p,productos as prd,TiposProducto as tp    
WHERE 
p.idproducto = prd.id
AND prd.idtipo=tp.id 
AND (@idTipoPrd=0 or @idTipoPrd=prd.idtipo)
AND (@idPrd=0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=p.iddestino)
GROUP BY prd.codigoProductoSAC, tp.nombre, prd.nombre 

UNION 

SELECT 
tc.Descripcion as TIPO,
tp.nombre as TIPO_PRD,
prd.nombre as PRODUCTO,
prd.codigoProductoSAC as CODIGO,
COUNT(*) as BULTOS,
SUM(ca.unidades) as UNIDADES,
sum(ca.pesoNeto) as PESO_NETO    

FROM fContenedoresEnStock(@fechaHasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN TiposProducto tp ON tp.id = prd.idtipo    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
(@idTipoPrd=0 or @idTipoPrd=prd.idtipo)
and (@idPrd=0 or @idPrd=prd.id)
AND (@idUbi = 0 or @idUbi=ca.iddestino)

GROUP BY tc.Descripcion,tp.nombre ,prd.nombre,prd.codigoProductoSAC

order by TIPO,TIPO_PRD
END


GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaDetalle]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 31-8-2022
-- Description:	retorna todos los ingresos a planta
--				segun rango de fechas,proveedor y producto.
--				si proveedor = "" o producto = 0 equivale a todos.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repIngPlantaDetalle]
@desde date ,@hasta date,@idProveedor varchar(12) ='',@idProducto int=0,@idTipoProducto int =0,@numTropa int=0
 
AS
BEGIN
 SELECT pe.id as IDPIEZA,
		OI.id as IDOI,
		prove.Nombre as PROVEEDOR,
		OI.idCertSanitario as SANITARIO,
		prd.codigoProductoSAC as CODIGO_PRD,
		prd.nombre as PRODUCTO, 
		tp.nombre as TIPO_PRD,
		pe.numTropa as TROPA,
		tip.nombre as TIPIF,
		pe.fecha_hora as PESADA,
		de.nombre as DESTINO, 
		pe.unidades as UNDS,
		pe.pesoNeto as NETO,
		pe.pesoTARA as TARA,
		pe.PesoRemitido as REMITIDO,
		ope.nombre as OPERADOR  
 FROM pesadas as pe  
 LEFT OUTER JOIN operadores ope ON pe.idOperador=ope.id  
 LEFT OUTER JOIN OI ON pe.idOi=OI.id  
 LEFT OUTER JOIN Productos as prd ON pe.idproducto=prd.id  
 LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo=tp.id  
 LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prove ON OI.CodigoProveedorSAC=prove.idCtaAuxi  AND prove.idPpal=1 AND prove.idAuxi=1 AND prove.Imputable=1
 LEFT OUTER JOIN destinos de ON pe.iddestino=de.id  
 LEFT OUTER JOIN tipificaciones tip ON pe.idTipificacion=tip.id  
 WHERE pe.idoi is not null 
 AND cast(pe.fecha_hora as DATE) between @desde and @hasta
 AND (@idProveedor='' or prove.idCtaAuxi = @idProveedor)
 AND (@idProducto=0 or prd.id = @idProducto)   
 AND (@idTipoProducto=0 or prd.idtipo = @idTipoProducto)
 AND (@numTropa=0 or pe.numTropa = @numTropa)
 ORDER BY pe.FECHA_HORA 
 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	retorna todos los ingresos a planta
--				segun rango de fechas.proveedor ,producto 
-- =============================================
CREATE PROCEDURE [dbo].[sp_repIngPlantaTotalizado]
@desde date ,@hasta date,@idProveedor varchar(12) ='',@idProducto int=0,@idTipoProducto int =0
 
AS
BEGIN
  SELECT 
  OI.id as IDOI,
  prove.Nombre as PROVEEDOR,
  OI.idCertSanitario as SANITARIO,
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  SUM(pe.unidades) as UNDS,
  SUM(pe.pesoNeto) as NETO,
  SUM(pe.PesoTara) as TARA,
  SUM(pe.pesoRemitido) as REMITIDO    
  FROM pesadas as pe    
  LEFT OUTER JOIN operadores ope ON pe.idOperador=ope.id    
  LEFT OUTER JOIN OI ON pe.idOi=OI.id    
  LEFT OUTER JOIN Productos as prd ON pe.idproducto=prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo=tp.id    
  LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prove ON OI.CodigoProveedorSAC=prove.idCtaAuxi  AND prove.idPpal=1 AND prove.idAuxi=1 AND prove.Imputable=1
  WHERE
  pe.idoi is not null 
  AND cast(pe.fecha_hora as DATE) between @desde and @hasta 
  AND (@idProveedor='' or prove.idCtaAuxi = @idProveedor)
  AND (@idProducto=0 or prd.id = @idProducto)   
  AND (@idTipoProducto=0 or prd.idtipo = @idTipoProducto)   
   
  GROUP BY 
  OI.id,
  prove.Nombre,
  OI.idCertSanitario,
  prd.codigoProductoSAC,
  prd.nombre 
  ORDER BY 
  OI.ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaTotalizadoXDiaProveedor]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	retorna el totalizado de unidades y peso
--				de los articulos por Dia-Proveedor en el rango
--				de fechas especificado 
-- =============================================
CREATE PROCEDURE [dbo].[sp_repIngPlantaTotalizadoXDiaProveedor]
@desde date ,@hasta date,@idProveedor varchar(12) =''
 
AS
BEGIN
  SELECT 
  cast(DIA as datetime) as DIA,
  PROVEEDOR,
  SUM(UNDS) as UNDS,
  SUM(NETO) as NETO,
  SUM(TARA) as TARA,
  SUM(REMITIDO) as REMITIDO
  FROM
  (
	  SELECT 
	  cast(pe.fecha_hora as date) as DIA,
	  prove.Nombre as PROVEEDOR,
	  pe.unidades as UNDS,
	  pe.pesoNeto as NETO,
	  pe.PesoTara as TARA,
	  pe.pesoRemitido as REMITIDO    
	  FROM pesadas as pe    
	  LEFT OUTER JOIN OI ON pe.idOi=OI.id    
	  LEFT OUTER JOIN Productos as prd ON pe.idproducto=prd.id    
	  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo=tp.id    
	  LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.CuentasAuxi as prove ON OI.CodigoProveedorSAC=prove.idCtaAuxi  AND prove.idPpal=1 AND prove.idAuxi=1 AND prove.Imputable=1
	  WHERE
	  pe.idoi is not null 
	  AND cast(pe.fecha_hora as DATE) between @desde and @hasta 
	  AND (@idProveedor='' or prove.idCtaAuxi = @idProveedor)
  )tb
   
  GROUP BY 
  DIA,
  PROVEEDOR
  ORDER BY 
  DIA
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngProduccionDetallePorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 16-09-2022
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repIngProduccionDetallePorSector]
@desde date,@hasta date,@idSector int=0,@idTipoProducto int=0,@idProducto int =0,@numTropa int=0
 
AS
BEGIN

SELECT 
pe.id as IDPIEZA,
DLP.idoi as IDOI,
prd.codigoProductoSAC as COD_PRD,
prd.nombre as PRODUCTO,
tp.nombre as TIPO_PRD,
pe.numTropa as TROPA,
tip.nombre as TIPIF,
DLP.fecha_hora as INGRESO,
s.nombre as SECTOR,
pe.unidades as UNDS,
pe.pesoNeto as NETO,   
pe.pesoTARA as TARA,
DLP.idestacion as PUESTO,
ope.nombre as OPERADOR   
FROM pesadas as pe   
LEFT OUTER JOIN DLP ON pe.id = DLP.idpesaje    
LEFT OUTER JOIN operadores ope ON DLP.idOperador = ope.id    
LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id    
LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
LEFT OUTER JOIN Sectores as s ON DLP.idsector = s.id 
LEFT OUTER JOIN Tipificaciones as tip ON pe.idTipificacion = tip.id 
WHERE 
DLP.idoi is not null 
AND CAST(DLP.fecha_hora as date) between @desde and @hasta 
AND ((@idSector=0) or (s.id=@idSector))   
AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
AND ((@idProducto=0) or (prd.id=@idProducto))   
AND ((@numTropa=0) or (pe.numTropa=@numTropa))   
ORDER BY 
pe.FECHA_HORA

END

GO
/****** Object:  StoredProcedure [dbo].[sp_repIngProduccionTotalizadoPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repIngProduccionTotalizadoPorSector]
@desde date,@hasta date,@idSector int=0,@idTipoProducto int=0,@idProducto int =0
 
AS
BEGIN

SELECT Convert(varchar(10),CONVERT(date,dlp.fecha_hora,106),103) as LOTE,
s.nombre as SECTOR,
prd.codigoProductoSAC as COD_PRD,
prd.nombre as PRODUCTO,
pe.numTropa as TROPA,
tip.nombre as TIPIF,
SUM(pe.unidades) as UNDS,
SUM(pe.pesoNeto) as NETO    
FROM pesadas as pe   
LEFT OUTER JOIN DLP ON pe.id = DLP.idpesaje   
LEFT OUTER JOIN operadores ope ON DLP.idOperador = ope.id   
LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id   
LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id   
LEFT OUTER JOIN Sectores as s ON DLP.idsector = s.id    
LEFT OUTER JOIN Tipificaciones as tip ON pe.idTipificacion = tip.id    
WHERE DLP.idoi is not null 
AND CAST(DLP.fecha_hora as date) between @desde and @hasta 
AND ((@idSector=0) or (s.id=@idSector))   
AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
AND ((@idProducto=0) or (prd.id=@idProducto))   
GROUP BY 
Convert(varchar(10),CONVERT(date,dlp.fecha_hora,106),103),
s.nombre,
prd.codigoProductoSAC,
prd.nombre,
pe.numTropa,
tip.nombre

ORDER BY
Convert(varchar(10),CONVERT(date,dlp.fecha_hora,106),103),
s.nombre,
prd.codigoProductoSAC,
prd.nombre,
pe.numTropa,
tip.nombre

END


GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnEgresosDetallado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 04-3-2021
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repInsumosEnEgresosDetallado]
@desde date,@hasta date,@comprobantePedido varchar(20)='',@cliente varchar(50)='',@idPrdInsumo int=0
 
AS
BEGIN

  SELECT
  pe.id as PEDIDO,
  pe.ComprobantePedidoSAC as COMPROBANTE,
  fc.nombreTercero as CLIENTE, 
  prdi.nombre as INSUMO,
  mins.unidades as UNDS,
  mins.fecha_hora as FECHA  
  
  FROM MovInsumos as mins    
  JOIN Pedidos as pe ON pe.id = mins.idProc    
  JOIN Productos as prdi ON mins.idPrdInsumo = prdi.id    
  LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  pe.CodigoPedidoSAC = fc.IdCabecera 

  WHERE 
  mins.idTipoMov ='EGR'
  and mins.idTipoProc='PED' 
  AND CAST(mins.fecha_hora as date) between @desde and @hasta
  AND (@idPrdInsumo=0 or @idPrdInsumo=mins.idPrdInsumo)
  AND (@comprobantePedido='' or @comprobantePedido = pe.ComprobantePedidoSAC)
  AND (@cliente='' or @cliente = fc.NombreTercero)

  ORDER BY
  pe.id,mins.fecha_hora desc 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnEgresosTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 04-3-2021
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repInsumosEnEgresosTotalizado]
@desde date,@hasta date,@cliente varchar(50)='',@idPrdInsumo int=0
 
AS
BEGIN

  SELECT
  prdi.nombre as INSUMO,
  sum(mins.unidades) as UNDS
  
  FROM MovInsumos as mins    
  JOIN Pedidos as pe ON pe.id = mins.idProc    
  JOIN Productos as prdi ON mins.idPrdInsumo = prdi.id    
  LEFT OUTER JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  pe.CodigoPedidoSAC = fc.IdCabecera 

  WHERE 
  mins.idTipoMov ='EGR'
  and mins.idTipoProc='PED' 
  AND CAST(mins.fecha_hora as date) between @desde and @hasta
  AND (@idPrdInsumo=0 or @idPrdInsumo=mins.idPrdInsumo)
  AND (@cliente='' or @cliente = fc.NombreTercero)
  GROUP BY prdi.nombre
  ORDER BY
  prdi.nombre 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnProduccionDetallado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 04-3-2021
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repInsumosEnProduccionDetallado]
@desde date,@hasta date,@idPrdInsumo int=0,@tipoBulto varchar(20)=''
 
AS
BEGIN

  SELECT
  TIPO='PIEZA',
  pe.id as NRO, 
  Convert(varchar(10),CONVERT(date,pe.fecha_hora,106),103) as LOTE,
  prd.nombre as PRODUCTO,
  pe.pesoNeto as NETO,
  prdi.nombre as INSUMO,
  mins.unidades as UNDS,
  mins.fecha_hora as FECHA  
  
  FROM MovInsumos as mins    
  JOIN Pesadas as pe ON pe.id = mins.idProc    
  LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id    
  LEFT OUTER JOIN Productos as prdi ON mins.idPrdInsumo = prdi.id    
  WHERE 
  mins.idTipoMov ='EGR'
  and mins.idTipoProc='PZA' 
  AND CAST(mins.fecha_hora as date) between @desde and @hasta
  AND (@idPrdInsumo=0 or @idPrdInsumo=mins.idPrdInsumo)
  AND (@tipoBulto='' or @tipoBulto = 'PIEZA')

  UNION 
  
  SELECT
  tc.Descripcion as TIPO ,
  cn.id as NRO, 
  Convert(varchar(10),CONVERT(date,cn.fecha_hora,106),103) as LOTE,
  prd.nombre as PRODUCTO,
  cn.pesoNeto as NETO,
  prdi.nombre as INSUMO,
  mins.unidades as UNDS,
  mins.fecha_hora as FECHA  

  FROM MovInsumos as mins    
  JOIN Contenedores as cn ON cn.id = mins.idProc
  join TiposContenedor tc on tc.id=cn.idTipo
  LEFT OUTER JOIN Productos as prd ON cn.idproducto = prd.id    
  LEFT OUTER JOIN Productos as prdi ON mins.idPrdInsumo = prdi.id    
  WHERE 
  mins.idTipoMov ='EGR'
  and mins.idTipoProc='CNT' 
  AND CAST(mins.fecha_hora as date) between @desde and @hasta
  AND (@idPrdInsumo=0 or @idPrdInsumo=mins.idPrdInsumo)
  AND (@tipoBulto='' or @tipoBulto = tc.Descripcion)
 
  
  ORDER BY
  TIPO, NRO, PRODUCTO,INSUMO,mins.fecha_hora 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_repInsumosEnProduccionTotalizado]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repInsumosEnProduccionTotalizado]
@desde date,@hasta date,@idPrdInsumo int=0,@tipoBulto varchar(20)=''
 
AS
BEGIN

  SELECT
  TIPO='PIEZA',
  prdi.nombre as INSUMO,
  sum(mins.unidades) as UNDS
 
  FROM MovInsumos as mins    
  LEFT OUTER JOIN Productos as prdi ON mins.idPrdInsumo = prdi.id    
  WHERE 
  mins.idTipoMov ='EGR'
  and mins.idTipoProc='PZA' 
  AND CAST(mins.fecha_hora as date) between @desde and @hasta
  AND (@idPrdInsumo=0 or @idPrdInsumo=mins.idPrdInsumo)
  AND (@tipoBulto='' or @tipoBulto = 'PIEZA')
  GROUP BY prdi.nombre,mins.idPrdInsumo 
  
  UNION 
  
  SELECT
  tc.Descripcion as TIPO ,
  prdi.nombre as INSUMO,
  sum(mins.unidades) as UNDS

  FROM MovInsumos as mins    
  JOIN Contenedores as cn ON cn.id = mins.idProc
  join TiposContenedor tc on tc.id=cn.idTipo
  LEFT OUTER JOIN Productos as prdi ON mins.idPrdInsumo = prdi.id    
  WHERE 
  mins.idTipoMov ='EGR'
  and mins.idTipoProc='CNT' 
  AND CAST(mins.fecha_hora as date) between @desde and @hasta
  AND (@idPrdInsumo=0 or @idPrdInsumo=mins.idPrdInsumo)
  AND (@tipoBulto='' or @tipoBulto = tc.Descripcion)
  GROUP BY tc.Descripcion,prdi.nombre,mins.idPrdInsumo 
 
  
  ORDER BY
  TIPO, INSUMO 
END





GO
/****** Object:  StoredProcedure [dbo].[sp_repPiezasProducidasDetallePorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repPiezasProducidasDetallePorSector]
@desde date,@hasta date,@idSector int=0,@idTipoProducto int=0,@idProducto int =0
 
AS
BEGIN

--  IDPIEZA,LOTE,COD_PRD,PRODUCTO,TIPO_PRD,PESADA,DESTINO,SECTOR,PUESTO,UNDS,NETO,TARA    

  SELECT 
  pe.id as IDPIEZA,
  Convert(varchar(10),CONVERT(date,pe.fecha_hora,106),103) as LOTE,
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  tp.nombre as TIPO_PRD, 
  pe.fecha_hora as PESADA,
  de.nombre as DESTINO,
  se.nombre as SECTOR,   
  pe.idestacion as PUESTO,
  pe.unidades as UNDS,
  pe.pesoNeto as NETO,
  pe.pesoTARA as TARA    
  FROM pesadas as pe    
  LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
  LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id    
  LEFT OUTER JOIN Sectores as se ON pe.idsector = se.id    
  WHERE 
  pe.idoi is null 
  AND pe.id not in (select idpesaje from DLP) 
  AND CAST(pe.fecha_hora as date) between @desde and @hasta 
  AND (prd.esinsumo is null OR prd.esinsumo=0)
  AND ((@idSector=0) or (pe.idSector=@idSector))   
  AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
  AND ((@idProducto=0) or (prd.id=@idProducto))   
  ORDER BY
  pe.FECHA_HORA 
END

GO
/****** Object:  StoredProcedure [dbo].[sp_repPiezasProducidasTotalizadosPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repPiezasProducidasTotalizadosPorSector]
@desde date,@hasta date,@idSector int=0,@idTipoProducto int=0,@idProducto int=0
 
AS
BEGIN

  SELECT Convert(varchar(10),
  CONVERT(date,pe.fecha_hora,106),103) as LOTE,
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  SUM(pe.unidades) as UNDS, 
  SUM(pe.pesoNeto) as NETO   
  FROM pesadas as pe    
  LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id    
  LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
  WHERE pe.idoi is null 
  AND pe.id not in (select idpesaje from DLP) 
  AND CAST(pe.fecha_hora as date) between @desde and @hasta 
  AND (prd.esinsumo is null OR prd.esinsumo =0) 
  AND ((@idSector=0) or (pe.idSector=@idSector))   
  AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
  AND ((@idProducto=0) or (prd.id=@idProducto))   

  GROUP BY 
  Convert(varchar(10),
  CONVERT(date,pe.fecha_hora,106),103),
  prd.codigoProductoSAC,
  prd.nombre 
  ORDER BY LOTE 
END


GO
/****** Object:  StoredProcedure [dbo].[sp_repProduccionDetalladoFull]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 03-11-2022
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repProduccionDetalladoFull]
@desde date,@hasta date,@idSector int=0,@idTipoProducto int=0,
@idProducto int =0,@tipo varchar(20)=''

 
AS
BEGIN

--  TIPO,NRO,LOTE,COD_PRD,PRODUCTO,TIPO_PRD,CREADA,DESTINO,SECTOR,PUESTO,OPERADOR,UNDS,NETO,TARA    

   SELECT 
  TIPO = 'PIEZA',
  pe.id as NRO,
  Convert(varchar(10),CONVERT(date,pe.fecha_hora,106),103) as LOTE,
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  tp.nombre as TIPO_PRD, 
  pe.fecha_hora as CREADA,
  de.nombre as DESTINO,
  se.nombre as SECTOR,   
  pe.idestacion as PUESTO,
  op.nombre as OPERADOR,
  pe.unidades as UNDS,
  pe.pesoNeto as NETO,
  pe.pesoTARA as TARA    
  FROM pesadas as pe    
  LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
  LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id    
  LEFT OUTER JOIN Sectores as se ON pe.idsector = se.id    
  LEFT OUTER JOIN operadores as op ON pe.idOperador = op.id
  WHERE
  (@tipo = '' or @tipo='PIEZA') 
  AND pe.idoi is null 
  AND pe.id not in (select idpesaje from DLP) 
  AND CAST(pe.fecha_hora as date) between @desde and @hasta 
  AND (prd.esinsumo is null OR prd.esinsumo=0)
  AND ((@idSector=0) or (pe.idSector=@idSector))   
  AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
  AND ((@idProducto=0) or (prd.id=@idProducto))   

UNION

  SELECT 
  tc.Descripcion as TIPO,
  c.id as NRO,
  Convert(varchar(10),CONVERT(date,c.fecha_hora,106),103) as LOTE,
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  tp.nombre as TIPO_PRD, 
  c.fecha_hora as CREADA,
  de.nombre as DESTINO,
  SECTOR='----',   
  c.idestacion as PUESTO,
  op.nombre as OPERADOR,
  c.unidades as UNDS,
  c.pesoNeto as NETO,
  c.pesoTara as TARA    

  FROM contenedores c
  JOIN ContenedorPiezas cp ON cp.idcontenedor=c.id
  JOIN TiposContenedor tc ON tc.id=c.idTipo    
  LEFT OUTER JOIN Productos as prd ON c.idproducto = prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
  LEFT OUTER JOIN Destinos as de ON c.iddestino = de.id    
  LEFT OUTER JOIN operadores as op ON c.idOperador = op.id
  WHERE 
  (@tipo = '' or @tipo=tc.Descripcion ) 
  AND  CAST(c.fecha_hora as date) between @desde and @hasta 
  AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
  AND ((@idProducto=0) or (prd.id=@idProducto))   

  ORDER BY
  CREADA 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_repProduccionTotalizadoFull]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repProduccionTotalizadoFull]
@desde date,@hasta date,@idSector int=0,@idTipoProducto int=0,
@idProducto int =0,@tipo varchar(20)=''

 
AS
BEGIN

--  TIPO,COD_PRD,PRODUCTO,TIPO_PRD,BULTOS,UNDS,NETO    

   SELECT 
  TIPO = 'PIEZA',
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  tp.nombre as TIPO_PRD, 
  COUNT(*) as BULTOS,
  sum(pe.unidades) as UNDS,
  sum(pe.pesoNeto) as NETO
  FROM pesadas as pe    
  LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
  LEFT OUTER JOIN Destinos as de ON pe.iddestino = de.id    
  LEFT OUTER JOIN Sectores as se ON pe.idsector = se.id    
  WHERE
  (@tipo = '' or @tipo='PIEZA') 
  AND pe.idoi is null 
  AND CAST(pe.fecha_hora as date) between @desde and @hasta 
  AND (prd.esinsumo is null OR prd.esinsumo=0)
  AND ((@idSector=0) or (pe.idSector=@idSector))   
  AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
  AND ((@idProducto=0) or (prd.id=@idProducto))   
  GROUP BY prd.codigoProductoSAC,prd.nombre,tp.nombre
UNION

  SELECT 
  tc.Descripcion as TIPO,
  prd.codigoProductoSAC as COD_PRD,
  prd.nombre as PRODUCTO,
  tp.nombre as TIPO_PRD, 
  COUNT(*) as BULTOS,
  sum(c.unidades) as UNDS,
  sum(c.pesoNeto) as NETO

  FROM contenedores c
  JOIN TiposContenedor tc ON tc.id=c.idTipo    
  LEFT OUTER JOIN Productos as prd ON c.idproducto = prd.id    
  LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id    
  LEFT OUTER JOIN Destinos as de ON c.iddestino = de.id    
  WHERE @idSector = 0 AND
  (@tipo = '' or @tipo=tc.Descripcion ) 
  AND  CAST(c.fecha_hora as date) between @desde and @hasta 
  AND ((@idTipoProducto=0) or (prd.idtipo=@idTipoProducto))   
  AND ((@idProducto=0) or (prd.id=@idProducto))   
  GROUP BY tc.Descripcion,prd.codigoProductoSAC,prd.nombre,tp.nombre

  ORDER BY TIPO,PRODUCTO 


END


GO
/****** Object:  StoredProcedure [dbo].[sp_repRendimientoPorProductoPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repRendimientoPorProductoPorSector]
@desde date,@hasta date,@idSector int
 
AS
BEGIN

  DECLARE @totalIngresado as float
  SET @totalIngresado = (SELECT SUM(pes.PesoNeto) 
						 FROM DLP dl, Pesadas pes 
						 WHERE CAST(dl.fecha_hora as date) between @desde and @hasta 
						 and dl.idSector = @idSector 
						 and dl.idpesaje = pes.id)
						    
  SELECT 
	prd.codigoProductoSAC as COD_PRODUCTO,
	prd.nombre as PRODUCTO,
	COUNT(*) as CANTIDAD,
	SUM(p.pesoneto) as PESO_TOTAL,
	(SUM(p.pesoneto) / COUNT(*)) as PESO_PROM,   
	ROUND((SUM(p.pesoneto) / @totalIngresado)*100,2) as INCIDENTE ,   
	prd.rendimientoSTD as RENDIMIENTO_STD,  
	(ROUND((SUM(p.pesoneto) / @totalIngresado), 2) - prd.rendimientoSTD) as DESVIO   
  FROM Pesadas p    
  LEFT outer join Productos prd ON prd.id = p.idproducto    
  WHERE CAST(p.fecha_hora as date) between @desde and @hasta  and p.idOI is null    
  AND p.id not in (select idpesaje from DLP ) AND p.idSector = @idSector    
  GROUP BY prd.codigoProductoSAC,prd.nombre,prd.rendimientoSTD  
END

GO
/****** Object:  StoredProcedure [dbo].[sp_repRendimientoPorTipoDeProducto]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	retorna informacion de rendimiento por tipo de 
--              producto si es especificado y si no todos.  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repRendimientoPorTipoDeProducto]
@desde date,@hasta date,@idTipoProducto int = null
 
AS
BEGIN
IF @idTipoProducto = null or @idTipoProducto =0
BEGIN
	DECLARE @totalesTable TABLE(total float, idtipoPrd int)   
	INSERT INTO @totalesTable
	SELECT SUM(p.PesoNeto),tp.id FROM DLP dp   
	LEFT OUTER JOIN Pesadas as p ON p.id = dp.idpesaje    
	LEFT OUTER JOIN Productos as prd ON prd.id = p.idproducto    
	LEFT OUTER JOIN TiposProducto as tp ON tp.id = prd.idtipo    
	WHERE cast(dp.fecha_hora as DATE) between @desde and @hasta     
	GROUP BY tp.id   
  
	SELECT 
	tp.nombre as TIPO,
	prd.nombre as PRODUCTO,
	tot.total as TOTAL_TIPO,
	SUM(p.PesoNeto) as TOTAL_PRODUCIDO ,
	SUM(p.unidades) as UNDS_PRODUCIDAS,
	(SUM(p.PesoNeto) / sum(p.unidades)) as PESO_UNITARIO,   
	(((SUM(p.PesoNeto) / sum(p.unidades))/NULLIF(prd.pesoNetoPredef, 0))  * 100) as RENDIMIENTO ,
	prd.pesoNetoPredef as STD     
	FROM pesadas p    
	LEFT OUTER JOIN Productos as prd ON prd.id = p.idproducto    
	LEFT OUTER JOIN TiposProducto as tp ON tp.id = prd.idtipo    
	LEFT OUTER JOIN @totalesTable as tot ON tot.idtipoPrd = tp.id    
	WHERE cast(p.fecha_hora as DATE) between @desde and @hasta
	and(p.idOI is null or p.idOI = 0) 
	GROUP BY prd.nombre,tp.nombre,prd.pesoNetoPredef,tot.total 
	ORDER BY tp.nombre
END

ELSE

BEGIN
	DECLARE @totalTipo float   
	SELECT @totalTipo = SUM(p.PesoNeto)    
	FROM DLP dp    
	LEFT OUTER JOIN Pesadas as p ON p.id = dp.idpesaje    
	LEFT OUTER JOIN Productos as prd ON prd.id = p.idproducto    
	LEFT OUTER JOIN TiposProducto as tp ON tp.id = prd.idtipo    
	WHERE cast(dp.fecha_hora as DATE) between @desde and @hasta and prd.idtipo = @idTipoProducto    
	GROUP BY tp.id     
  
	SELECT
	tp.nombre as TIPO,
	prd.nombre as PRODUCTO,
	@totalTipo as TOTAL_TIPO,
	SUM(p.PesoNeto) as TOTAL_PRODUCIDO,
	SUM(p.unidades) as UNDS_PRODUCIDAS,
	(SUM(p.PesoNeto) / sum(p.unidades)) as PESO_UNITARIO,   
	(((SUM(p.PesoNeto) / sum(p.unidades))/NULLIF(prd.pesoNetoPredef, 0))  * 100) as RENDIMIENTO ,
	prd.pesoNetoPredef as STD     
	FROM pesadas p    
	LEFT OUTER JOIN Productos as prd ON prd.id = p.idproducto    
	LEFT OUTER JOIN TiposProducto as tp ON tp.id = prd.idtipo    
	WHERE cast(p.fecha_hora as DATE) between @desde and @hasta  
	and(p.idOI is null or p.idOI = 0) and prd.idtipo = @idTipoProducto 
	GROUP BY tp.nombre, prd.nombre, tp.nombre,prd.pesoNetoPredef 
	ORDER BY tp.nombre

END


END
GO
/****** Object:  StoredProcedure [dbo].[sp_repRendimientoTotalesPorSector]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 27-8-2020
-- Description:	  
-- =============================================
CREATE PROCEDURE [dbo].[sp_repRendimientoTotalesPorSector]
@desde date,@hasta date,@idSector int
 
AS
BEGIN

	SELECT
	SUM(p.PesoNeto) as TOTAL_INGRESADO,
	(SELECT 
		SUM(PesoNeto) 
		FROM Pesadas 
		WHERE CAST(fecha_hora as date) between @desde and @hasta
		and idSector = @idSector
		and idOI is null 
		and id not in (SELECT idpesaje FROM dlp))as TOTAL_PRODUCIDO,   
	ROUND((((SELECT SUM(PesoNeto) 
			 FROM Pesadas 
			 WHERE CAST(fecha_hora as date) between @desde and @hasta 
			 and idOI is null 
			 and idSector = @idSector
			 and id not in (SELECT idpesaje FROM dlp))/ SUM(p.PesoNeto))*100),2) as TOTAL_RENDIMIENTO    
	FROM dlp as d   
	left outer join pesadas p ON p.id = d.idpesaje   
	left outer join Sectores s ON s.id = d.idSector   
	WHERE CAST(d.fecha_hora as date) between @desde and @hasta 
	and d.idsector =  @idSector    
	GROUP BY s.nombre
END



GO
/****** Object:  StoredProcedure [dbo].[sp_repResultInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
/*
Author:			Sergio Fasolo
Create date:	15-02-2020
Description:	
	obtiene el informe de los resultados obtenidos en los ajustes de
	de stock por inventario. En la tabla resultInventario cada registro posee
	informacion sobre el resultado como ser
	Fecha de ejecucion del ajuste.
	Fecha de inventario.
	Total de piezas , cajas y combos verificados en stock.
	Total de piezas Ajustadas etc.
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_repResultInventario]
@desde datetime , @hasta datetime
 
AS
BEGIN
	select
	fechaAjuste as FECHA,
	fechaInventario as INVENTARIO,
	TotalPiezasVerificadasEnStock as PIEZAS_VERIF_EN_STOCK,
	pzas_conSTLsinSTF as PIEZAS_CON_STOCK_NO_EXISTEN,
	pzas_sinSTLconSTF as PIEZAS_SIN_STOCK_EXISTEN,
	ajustPzas_conSTLsinSTF as PIEZAS_CON_STOCK_NO_EXISTEN_AJUSTADAS,
	ajustPzas_sinSTLconSTF as PIEZAS_SIN_STOCK_EXISTEN_AJUSTADAS,
	pzas_fueraContenedorConStock as PIEZAS_FUERA_CONTENEDOR_ENSTOCK,
	pzas_fueraContenedorSinStock as PIEZAS_FUERA_CONTENEDOR_SINSTOCK,
	ajustPzas_fueraContenedorConStock as PIEZAS_FUERA_CONTENEDOR_ENSTOCK_AJUSTADAS,
	ajustPzas_fueraContenedorSinStock as PIEZAS_FUERA_CONTENEDOR_SINSTOCK_AJUSTADAS,
	cantBultosNoExisten as SIN_REGISTRAR,
	TotalCajasVerificadasEnStock as CAJAS_VERIF_EN_STOCK,
	cjas_conSTLsinSTF as CAJAS_CON_STOCK_NO_EXISTEN,
	cjas_sinSTLconSTF as CAJAS_SIN_STOCK_EXISTEN,
	ajustCjas_conSTLsinSTF as CAJAS_CON_STOCK_NO_EXISTEN_AJUSTADAS,
	ajustCjas_sinSTLconSTF as CAJAS_SIN_STOCK_EXISTEN_AJUSTADAS,
	TotalCombosVerificadosEnStock as COMBOS_VERIF_EN_STOCK,
	cmbs_conSTLsinSTF as COMBOS_CON_STOCK_NO_EXISTEN,
	cmbs_sinSTLconSTF as COMBOS_SIN_STOCK_EXISTEN,
	ajustCmbs_conSTLsinSTF as COMBOS_CON_STOCK_NO_EXISTEN_AJUSTADOS,
	ajustCmbs_sinSTLconSTF as COMBOS_SIN_STOCK_EXISTEN_AJUSTADOS,
	cantBultosEnPedidosAbiertos as BULTOS_EN_PEDIDOS_ABIERTOS
	from resultInventario
	where cast(fechaInventario as DATE) between cast(@desde as DATE) and cast(@hasta as DATE)


END
GO
/****** Object:  StoredProcedure [dbo].[sp_repTotalizadoProductosPedidoPendienteVentaPorPreparacion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 6-8-22
-- Description:	SP obtiene todos los items de compocision de 
-- todos los pedidos activos que sean del rango de fecha indicado
-- Expresando la cantidad pedida la cantidad preparada y el resto.
-- =============================================
CREATE PROCEDURE [dbo].[sp_repTotalizadoProductosPedidoPendienteVentaPorPreparacion]
@desde date,@hasta date
 
AS
BEGIN
SELECT  
fs.idProducto as CodigoSAC,
fp.Descripcion as ProductoSAC,
fs.Detalle as Observacion,
SUM(cast(fs.cantidad as integer)) as Unds_PED,
SUM(fs.CantidadUMPrimaria) as Peso_PED

FROM PHY_WINSIFAC_01130_01_00001_0100.dbo.V_CantidadesPedidosPendientes as pp
JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
pp.PedIdCabecera = fc.IdCabecera and pp.idAuxi=fc.idAuxi and pp.idCtaAuxi=fc.idCtaAuxi and
(fc.idTipoComprobante = 'PED' or fc.idTipoComprobante = 'PED2') and fc.idPpal=1

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
fc.IdCabecera = fs.IdCabecera and pp.PedIdMovimiento = fs.IdMovimiento 
and fs.idTipoComprobante = fc.idTipoComprobante 

JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  
fs.idProducto = fp.idProducto and 
fp.Imputable = 1 and
fp.idPlanProducto=2 

left outer join Pedidos ped ON ped.CodigoPedidoSAC=fc.IdCabecera

WHERE 
pp.idAuxi=1
AND cast(fs.fechaVencimiento as DATE) between @desde and @hasta
AND (not exists (select * from Pedidos where CodigoClienteSAC=fc.idCtaAuxi and CodigoPedidoSAC=fc.IdCabecera) or (ped.activo=1 or ped.activo is null))
GROUP BY fs.idProducto,fp.Descripcion,fs.Detalle
ORDER BY
fp.Descripcion
END
GO
/****** Object:  StoredProcedure [dbo].[sp_resetInicial]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 24-9-2020
-- Description:	Borra toda la informacion en tablas de 
--				registracion de datos y reinicia numeradores
--				ids automaticos.
-- =============================================
CREATE PROCEDURE [dbo].[sp_resetInicial]
	
AS
BEGIN
SET NOCOUNT, XACT_ABORT ON;
BEGIN TRY
	BEGIN TRANSACTION;

		PRINT 'INICIO DE TRANSACCIÓN';

		delete combos
		delete cajas
		delete Contenedores
		delete ContenedorPiezas
		delete dbLog
		delete Destinos
		delete Devoluciones
		delete DLP
		delete Egresos
		delete Facturas
		delete Remitos
		delete Inventario
		delete oi
		delete Pedidos
		delete Pesadas
		delete resultInventario
		delete Productos
		delete ProductoInsumos
		delete MovInsumos
		
		DBCC CHECKIDENT (inventario, RESEED, 0)
		DBCC CHECKIDENT (Destinos, RESEED, 0)
		DBCC CHECKIDENT (Pesadas, RESEED, 0)
		DBCC CHECKIDENT (Contenedores, RESEED, 0)
		DBCC CHECKIDENT (OI, RESEED, 0)
		DBCC CHECKIDENT (Pedidos, RESEED, 0)
		DBCC CHECKIDENT (Productos, RESEED, 0)

		PRINT 'TRANSACCIÓN REALIZADA OK !!!';

	COMMIT;
END TRY

--BLOQUE CATCH
BEGIN CATCH
	IF @@trancount > 0
		ROLLBACK TRANSACTION
	PRINT 'ERROR';
END CATCH
--END BLOQUE CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[sp_resetRegistracion]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-08-2022
-- Description:	Borra todas las registraciones de movimientos 
--				como ser los pesajes , ingresos a produccion, 
--				contenedores,pedidos,egresos,devoluciones ,dblog, ordenes de ingresos,
--				remitos , facturas , inventarios,movinsumos,resultados de inventarios.
--				ids automaticos.
-- =============================================
CREATE PROCEDURE [dbo].[sp_resetRegistracion]
	
AS
BEGIN
SET NOCOUNT, XACT_ABORT ON;
BEGIN TRY
	BEGIN TRANSACTION;

		PRINT 'INICIO DE TRANSACCIÓN';

		delete Contenedores
		delete ContenedorPiezas
		delete dbLog
		delete Devoluciones
		delete DLP
		delete Egresos
		delete Facturas
		delete Remitos
		delete Inventario
		delete oi
		delete Pedidos
		delete Pesadas
		delete resultInventario
		delete MovInsumos
		
		DBCC CHECKIDENT (inventario, RESEED, 0)
		DBCC CHECKIDENT (Pesadas, RESEED, 0)
		DBCC CHECKIDENT (Contenedores, RESEED, 0)
		DBCC CHECKIDENT (OI, RESEED, 0)
		DBCC CHECKIDENT (Pedidos, RESEED, 0)

		PRINT 'TRANSACCIÓN REALIZADA OK !!!';

	COMMIT;
END TRY

--BLOQUE CATCH
BEGIN CATCH
	IF @@trancount > 0
		ROLLBACK TRANSACTION
	PRINT 'ERROR';
END CATCH
--END BLOQUE CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[sp_setDiasProximidadVencimiento]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 4-8-2021
-- Description:	Establece los dia de proximidad de vencimiento para el 
--				parametro global Parametros.DiasproximidadVencimiento. 
-- =============================================
CREATE PROCEDURE [dbo].[sp_setDiasProximidadVencimiento]
	 @dias int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Parametros set DiasProximidadVencimiento = @dias
END

GO
/****** Object:  StoredProcedure [dbo].[sp_simulaCapturaInventario]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
/*
	Author:			Sergio Fasolo
	Fecha:			15-02-20
Coloca todas las piezas en stock en la tabla inventario	
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_simulaCapturaInventario]
 
AS
BEGIN
delete Inventario
insert Inventario (fechaHoraCaptura,fechaInicioInventario,idDestino,idPieza)
select getdate(),getDate(),p.idDestino,p.id
from fPiezasEnStock(null) p

insert Inventario (fechaHoraCaptura,fechaInicioInventario,idDestino,idPieza)
select getdate(),getDate(),c.idDestino,'A'+cast(c.id as varchar(10))+'A'
from fContenedoresEnStock(null) c
END

GO
/****** Object:  StoredProcedure [dbo].[sp_TESTcrearRemitoDespachoSAC]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 9-08-2022
/* Description:

	Realiza el TEST de creacion de un remito de venta para el sistema PHYSIS en donde sus items
	corresponderan a las piezas colectadas en un proceso de un pedido de produccion en el sector
	de despachos. El sistema de produccion conoce la clave de un pedido del sistema PHYSIS que es 
	conformada por IdCabecera,IdMovimiento,IdCtaAux. 
	Sumado a esto ya existen registradas piezas bajo esa misma clave de physis en el sistema de produccion. 
	La sumatoria de las registraciones y la informacion sobre el pedido de physis podran generar este proceso de creacion de un remito. 		
	
	En funcion del tipo de PEDIDO a remitir se generan dos posibles tipos de remitos.
	Si el tipo de pedido es PED se genera un remito tipo RECE.
	Si el tipo de pedido es PED2 se genera un remito tipo REM.

	Retorna un string con el resultado de la operacion. Si la cadena comienza con OK la operacion
	fue exitosa , y si no lo fue comenzara con 'ERROR'y a continuacion se sumara una descripcion.
	
*/
-- =============================================
CREATE PROCEDURE [dbo].[sp_TESTcrearRemitoDespachoSAC]
	 @CodigoPedidoSAC int=0, @CodigoClienteSAC varchar(12)='',@idPedido int=0,@idEstacion int=0,@result varchar(100) output
AS
SET NOCOUNT ON   

BEGIN
declare
	@IdMovimiento     smallint =null,      
	@NroOrden      numeric(4, 0) =1,      
	@Producto      char (12),      
	@IdAuxiPropietario    smallint=null,   
	@IdCtaAuxiPropietario   varchar(12)=null,   
	@Partida      char (10),      
	@UM       char (5),      
	@CantidadUM     numeric(13, 4),      
	@CantidadUMP     numeric(13, 4),      
	@PrecioUnitario    float,      
	@Descuento      numeric(6, 3),      
	@DescuentoProducto      numeric(6, 3),      
	@Descuento1Cabecera      money,      
	@idCDescuento1Cabecera   varchar(12),
	@Descuento2Cabecera      money,      
	@idCDescuento2Cabecera   varchar(12),
	@PrecioUnitarioNeto   float,      
	@PrecioNeto     float,      
	@ImpuestosInternos    money,     
	@FechaVencimiento    datetime,      
	@Observaciones     varchar (2048)='',      
	@AcumulaProducto    bit=0,   
	@PedIdCabecera    int=null,   
	@PedIdMovimiento    numeric(4,0),      
	@PedCantidad     numeric(13, 4),      
	@RemIdCabecera    int = null,   
	@RemIdMovimiento    numeric(4,0)=null,      
	@RemCantidad     numeric(13, 4)=null,      
	@FacIdCabecera    int=null,   
	@FacIdMovimiento    numeric(4,0)=null,      
	@FacCantidad     numeric(13,4)=null,   
	@IdLiquidoProducto    int=null,   
	@IdCabeceraViaje   int=null,   
	@IdMovimientoViaje   numeric(4,0)=null,   
	@IdConexion     int = @@spid*-1-@idEstacion,      
	@FacClase                Char(4) = Null,  
	@ProductoConjunto     char (12) = Null, 
	@NivelConjunto     int = Null, 
	@IdPlanProducto    smallint = 2,   
	@RecuperoKgLimpio    money = 0, 
	@IdDeposito     char(5) = '',  
	@CantidadUMRemesa   numeric(13, 4) = 0, 
	@CantidadUMDif     numeric(13, 4) = 0, 
	@CantidadUMPorc    numeric(13, 4) = 0, 
	@CantidadUMPRemesa    numeric(13, 4) = 0, 
	@CantidadUMPDif    numeric(13, 4) = 0, 
	@CantidadUMPPorc    numeric(13, 4) = 0, 
	@CodCampo     int = Null, 
	@CodLote     int = Null, 
	@NroDTA      varchar(9) = Null, 
	@Estado      int = 0, 
	@FechaEstado    datetime = Null, 
	@PrecioImpuestoInterno  money = 0, 
	@ProductoDesdoblado   bit = 0,  
	@CantidadUMPDestino   numeric(13, 4) = 0,  
	@ITC      money = 0, 
	@PrecioITC     money = 0,  
	@IdItem      smallint = 0, 
	@IdUbicacion    varchar(12) = Null, 
	@PesoBruto     numeric(13,4) = Null,      
	@IdAuxiComprador    smallint = Null,   
	@IdCtaAuxiComprador   varchar(12) = Null,      
	@IdCabeceraServicio   int = Null,   
	@IdMovimientoServicio   int = Null,
	@TasaIVA money =0,
	@IdTipoComprobantePedido char(8) ='PED'

/******************************************************************************
Limpiar tablas temporales de physis que se destinan a la creacion de remitos.
*******************************************************************************/

Select IDCONEXION = @IdConexion
Select CODIGO_CLIENTE_SAC = @CodigoClienteSAC
Select CODIGO_PEDIDO_SAC = @CodigoPedidoSAC
Select ID_PEDIDO = @idPedido

exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStock_Tmp_Delete @idconexion

/********************************************************************************
Cursor para recorrer registros de registraciones de piezas en proceso de despacho
bajo la clave idCabecera,IdMovimiento,IdPedido
*********************************************************************************/
declare @PrecioFinal as float = 0

DECLARE cursor_egresos CURSOR
FOR SELECT
 fs.IdCabecera as IDCABECERA,
 fs.idPartida as IDPARTIDA,
 fs.IdMovimiento as IDMOVIMIENTO,
 fp.idProducto as IDPRODUCTO,
 fs.idUM as IDUM,
 fs.PrecioUnitario as PRECIOUNITARIO,
 fc.Descuento1 as DESCUENTO,
 fs.PDescuento as DESCUENTO_PRODUCTO,
 fc.IdCDescuento1 as IDCDESCUENTO1,
 fc.Descuento2 as DESCUENTO2,
 fc.IdCDescuento2 as IDCDESCUENTO2,
 fs.PrecioUnitarioNeto as PRECIOUNITARIONETO,
 fs.PrecioNeto as PRECIONETO,
 fs.ImpuestosInternos as IMPUESTOSINTERNOS,
 fs.FechaVencimiento as FECHAVENCIMIENTO,
 fs.iddeposito as IDDEPOSITO,
 fs.TasaIVA as TASAIVA,
 fs.ProductoDesdoblado as PRODUCTODESDOBLADO,
 fc.IdTipoComprobante as IDTIPOCOMPROBANTE,

 (SELECT SUM(UNIDADES) FROM(
	SELECT SUM(pe.unidades) AS UNIDADES 
	FROM Pesadas pe, Egresos eg ,Productos prd2 
	WHERE pe.idproducto = prd2.id 
	and	prd2.codigoProductoSAC = fp.idProducto  
	and eg.idpesaje = pe.id 
	and eg.idPedido = ped.id
	and eg.idTipoBulto = 'PZA'
	UNION
    SELECT count(distinct c.id ) as UNIDADES 
	FROM Contenedores c,Egresos eg ,Productos prd2 
	WHERE
	eg.idPedido = ped.id
	and eg.idTipoBulto = 'CNT'
	and eg.idpesaje = c.id 
	and prd2.id=c.idProducto
	and prd2.codigoProductoSAC=fp.idProducto
	and (c.idTipo='CMB' OR c.idTipo='CAJ'))T
	WHERE T.UNIDADES is not null) as UNDS_COL,  

(SELECT SUM(PESONETO) FROM(
	SELECT distinct pe.id as ID ,pe.PesoNeto AS PESONETO 
	FROM Pesadas pe, Egresos eg ,Productos prd2 
	WHERE pe.idproducto = prd2.id 
	and eg.idTipoBulto = 'PZA'
	and	prd2.codigoProductoSAC = fp.idProducto  
	and eg.idpesaje = pe.id 
	and eg.idPedido = ped.id
	UNION ALL
    SELECT distinct c.id AS ID,c.pesoNeto as PESONETO 
	FROM Contenedores c,Egresos eg ,Productos prd2 
	WHERE
	eg.idPedido = ped.id
	and eg.idTipoBulto = 'CNT'
	and eg.idpesaje = c.id 
	and prd2.id=c.idProducto
	and prd2.codigoProductoSAC=fp.idProducto
	and (c.idTipo='CMB' OR c.idTipo='CAJ'))T
	WHERE T.PESONETO is not null) as PESO_COL  


 FROM pedidos as ped 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras fc ON  
									ped.CodigoPedidoSAC = fc.IdCabecera 
									and fc.idTipoComprobante = ped.TipoPedidoSAC 
									and fc.idPpal=1  
 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FacStock fs ON  
									ped.CodigoPedidoSAC = fs.IdCabecera 
									and fs.idTipoComprobante = fc.idTipoComprobante
									and fs.idPpal=1  
 
 JOIN PHY_WINSIFAC_01130_01_00001_0100.dbo.FACProductos fp ON  fs.idProducto = fp.idProducto and fp.Imputable = 1 and fp.idPlanProducto=2 
 
 JOIN Productos prd ON 	fp.idProducto = prd.codigoProductoSAC and fs.idProducto=prd.codigoProductoSAC 

 WHERE ped.id = @idPedido
 GROUP BY ped.id,fs.IdCabecera,fs.idPartida, fs.IdMovimiento,fp.idProducto ,fs.idUM,fs.PrecioUnitario,fc.Descuento1,fs.PDescuento,
 fc.IdCDescuento1,fc.Descuento2,fc.IdCDescuento2,fs.PrecioUnitarioNeto,
 fs.PrecioNeto,fs.ImpuestosInternos,fs.FechaVencimiento,fs.iddeposito,fs.TasaIVA,fs.ProductoDesdoblado,fc.IdTipoComprobante


OPEN cursor_egresos

FETCH NEXT FROM cursor_egresos INTO 
    @PedidCabecera, 
	@Partida,
	@PedIdMovimiento,
	@Producto,
	@UM,
	@PrecioUnitario,      
	@Descuento,      
	@DescuentoProducto,      
	@idCDescuento1Cabecera,
	@Descuento2Cabecera,
	@idCDescuento2Cabecera,
	@PrecioUnitarioNeto,      
	@PrecioNeto,      
	@ImpuestosInternos,     
	@FechaVencimiento,      
	@IdDeposito,
	@TasaIVA,
	@ProductoDesdoblado,
	@IdTipoComprobantePedido,
	@CantidadUM,
	@CantidadUMP



IF @@FETCH_STATUS > 0
BEGIN
		CLOSE cursor_egresos;
		DEALLOCATE cursor_egresos;
		SET @result ='ERROR: LA CONSULTA SOBRE REGISTRACIONES DE PIEZAS NO OBTUVO RESULTADOS'
		return 0
END
ELSE
BEGIN
	WHILE @@FETCH_STATUS = 0
		BEGIN
			
			set @Descuento1Cabecera=@Descuento

			IF IsNull(@DescuentoProducto,0) > 0
				set @Descuento = @DescuentoProducto;

			set @PedCantidad= @CantidadUM*-1
			set @PrecioUnitarioNeto = @PrecioUnitario - (@PrecioUnitario * (IsNull(@Descuento,0)/100))
			set @PrecioNeto = @PrecioUnitarioNeto * @CantidadUMP
			set @PrecioFinal = @PrecioFinal + (@PrecioNeto * ((IsNull(@TasaIVA,10.5)/100)+1))

			SELECT @PedidCabecera as IDCABECERA, 
				@Partida as PARTIDA,
				@PedIdMovimiento as IDMOVIMIENTO,
				@Producto as IDPRODUCTO,
				@UM as UM,
				@CantidadUM as CANUND,
				@CantidadUMP as CANTPESO,
				@Descuento as DESCUENTO,
				@Descuento1Cabecera as DESCUENTO1CABECERA,
				@Descuento2Cabecera as DESCUENTO2CABECERA,
				@idCDescuento1Cabecera as IDCDESCUENTO1CABECERA,
				@idCDescuento2Cabecera as IDCDESCUENTO2CABECERA,
				@PrecioUnitario as PRECIOUNITARIO,
				@PrecioUnitarioNeto as PRECIOUNITARIONETO,
				@PrecioNeto as PRECIONETO,
				@PrecioFinal as PRECIOFINAL,
				@ProductoDesdoblado as PRODUCTODESDOBLADO,
				@IdTipoComprobantePedido as IDTIPOCOMPROBANTE

			set @CantidadUM = isnull(@CantidadUM,0)
			set @CantidadUMP = isnull(@CantidadUMP,0)
			
			exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStock_Tmp_Insert    
				@IdMovimiento,      
				@NroOrden,      
				@Producto,      
				@IdAuxiPropietario,   
				@IdCtaAuxiPropietario,   
				@Partida,      
				@UM,      
				@CantidadUM,      
				@CantidadUMP,      
				@PrecioUnitario,      
				@Descuento,      
				@PrecioUnitarioNeto,      
				@PrecioNeto,      
				@ImpuestosInternos,     
				@FechaVencimiento,      
				@Observaciones,      
				@AcumulaProducto,   
				@PedIdCabecera,   
				@PedIdMovimiento,      
				@PedCantidad,      
				@RemIdCabecera,   
				@RemIdMovimiento,      
				@RemCantidad,      
				@FacIdCabecera,   
				@FacIdMovimiento,      
				@FacCantidad,   
				@IdLiquidoProducto,   
				@IdCabeceraViaje,   
				@IdMovimientoViaje,   
				@IdConexion,      
				@FacClase,  
				@ProductoConjunto, 
				@NivelConjunto, 
				@IdPlanProducto,   
				@RecuperoKgLimpio, 
				@IdDeposito,  
				@CantidadUMRemesa, 
				@CantidadUMDif, 
				@CantidadUMPorc, 
				@CantidadUMPRemesa, 
				@CantidadUMPDif, 
				@CantidadUMPPorc, 
				@CodCampo, 
				@CodLote, 
				@NroDTA , 
				@Estado , 
				@FechaEstado, 
				@PrecioImpuestoInterno, 
				@ProductoDesdoblado,  
				@CantidadUMPDestino,  
				@ITC, 
				@PrecioITC  
		
			FETCH NEXT FROM cursor_egresos INTO 
				@PedidCabecera, 
				@Partida,
				@PedIdMovimiento,
				@Producto,
				@UM,
				@PrecioUnitario,      
				@Descuento,   
				@DescuentoProducto,      
				@idCDescuento1Cabecera,
				@Descuento2Cabecera,
				@idCDescuento2Cabecera,
				@PrecioUnitarioNeto,      
				@PrecioNeto,      
				@ImpuestosInternos,     
				@FechaVencimiento,      
				@IdDeposito,
				@TasaIVA,
				@ProductoDesdoblado,
				@IdTipoComprobantePedido,
				@CantidadUM,
				@CantidadUMP
			
			SET @NroOrden = @NroOrden+1
		END;
	   
	CLOSE cursor_egresos;
	DEALLOCATE cursor_egresos;


	/************************************************************************
	Preparación antes de llamar al ST de creacion de remito 
	*************************************************************************/

declare 
	@ABMD        char(1)='A', /* Alta */   
	@IdCabecera  int =0,       
    @IdEjercicio   smallint,          
    @Sucursal        char(4)='0000',      
    @Fecha        datetime,      
    @IdTipoComprobante   char(8)= 'RECE',      
    @Numero        varchar(12),      
    @IdAuxi        smallint=2,      
    @IdCtaAuxi        varchar(12)=@CodigoClienteSAC,      
    @IdTipoDocumento   varchar(5),      
    @NumeroDocumento   varchar(12),      
    @NombreTercero   varchar(40),      
    @CategoriaIVA   varchar(2),      
    @Observaciones2   varchar(500)='',        
    @IddDeposito   char(05),        
    @IdaDeposito   char(05)=null,        
    @IdAuxiListaPrecios   smallint=null,   
    @IdReagListaPrecios   smallint=null,   
    @IdListaPrecios   char(12)=null,        
    @IdReagVendedor   smallint=null,   
    @IdVendedor        char(12)=null,   
    @IdReagTransporte   smallint=null,        
    @IdTransporte   char(12)=null,   
    @IdReagDescuento   smallint=null,        
    @IdDescuento1   char(12)=@idCDescuento1Cabecera,        
    @Descuento1        money=@Descuento1Cabecera,        
    @IdDescuento2   char(12)=@idCDescuento2Cabecera,        
    @Descuento2        money=@Descuento2Cabecera,      
    @IdReagObservaciones   smallint=null,     
    @IdCodObservaciones   char(12)=null,        
    @Referencia    char(20),   
    @IdReagCondPago   smallint,   
    @IdCondPago    char(12),        
    @FormaCosteo   char(5)='PPPP',    
    @Alcance    tinyint=3,   
    @ModoCarga    tinyint=1,   
    @IdMoneda    char(5)=null,   
    @Serie    tinyint=null,   
    @TasaCambio    float=1,   
    @ImporteTotal   money=@PrecioFinal,       
    @IdUsuario    smallint=1000, /*editar este valor cuando en Physis se cree un usuario para el sistema de produccion*/         
    @CodCampania    smallint = 0, 
    @Planta    Bit= 1, 
    @FechaExt        datetime = null,          
    @IdTipoComprobanteExt   char(8) = null,          
    @NumeroExt        varchar(12) = null, 
    @FechaVencimientoCAI   datetime = null, 
    @IdPais               smallint = 1,                
    @IdProvincia          smallint = 2,  
    @IdCabeceraRepl    int = 0, 
    @NumeroCAI            varchar(14) = Null, 
    @NroGuia             varchar(14) = Null, 
	@IdIdioma     Int = Null, 
	@IdMonedaPrint    Char(5) = Null,        
    @SeriePrint     TinyInt = Null,        
    @TasaPrint     float = Null,      
	@ModoFacturacion int = cast(isnull(@ProductoDesdoblado,0) as int)+1  

	--requerido por physis
	exec PHY_WINSIFAC_01130_01_00001_0100.dbo.spFacGrabarTemporalesReferenciado 
	@CodigoPedidoSAC,
	@IdUsuario,
	@IdConexion


	--establecer como fecha del nuevo remito la fecha del dia siguiente al dia actual y coloca la hora 00:00:00.
	set @Fecha = DATEADD(HOUR,0, CAST(CAST(DATEADD(day,1,getdate()) AS DATE) AS DATETIME))
	--obtener IdEjercicio
	select @IdEjercicio = idejercicio from PHY_WINSIFAC_01130_01_00001_0100.dbo.ejercicios where @Fecha between fechainicio and fechacierre

	--obtener propiedades del pedido original
	select	@idTipoDocumento=idTipoDocumento,
			@NumeroDocumento=NumeroDocumento,
			@NombreTercero=	NombreTercero,
			@CategoriaIva = CategoriaIva,
			@Observaciones2= Observaciones,
			@iddDeposito=iddDeposito,
			@Referencia=Referencia,
			@IdReagCondPago=IdReagCondPago,
			@IdCondPago=IdCondPago,
			@IdReagTransporte=IdReagTransporte,
			@IdTransporte=IdTransporte,
			@IdReagListaPrecios=IdReagListaPrecios,
			@IdAuxiListaPrecios=IdAuxiListaPrecios,
			@IdListaPrecios=IdListaPrecios,
			@CategoriaIVA = CategoriaIVA
			from PHY_WINSIFAC_01130_01_00001_0100.dbo.FacCabeceras 
			where idCabecera=@CodigoPedidoSAC 
			and idAuxi=1 and idCtaAuxi=@CodigoClienteSAC	   
	 
	--Aplicar tipo de remito en funcion del tipo de pedido.
	--Si pedido es PED el tipo de remito a crear debe ser RECE.
	--Si el pedido es PED2 el tipo de remito  a crear debe ser REM.
	--RECE Modo facturacion 1 , REM modo facturacion 2 
	
	set @IdTipoComprobante = case @IdTipoComprobantePedido 
									when 'PED' then 'RECE'
									when 'PED2' then 'REM'
									else 'RECE' end;
	Select TIPO_COMPROBANTE_REMITO = @IdTipoComprobante

	--generar numero de comprobante en funcion del tipo de remito
	IF @IdTipoComprobante = 'RECE'
		BEGIN 
			Select @Numero='0005'+Right('00000000' + CONVERT(varchar(8), numero+1), 8) 
			from PHY_WINSIFAC_01130_01_00001_0100.dbo.NumeracionPrefijos  
			where idnumerador = 72 and idprefijo = 5 and idEjercicio = @IdEjercicio
		END
	ELSE
		BEGIN 
			Select @Numero='0009'+Right('00000000' + CONVERT(varchar(8), numero+1), 8) 
			from PHY_WINSIFAC_01130_01_00001_0100.dbo.NumeracionPrefijos  
			where idnumerador = 42 and idprefijo = 9 and idEjercicio = @IdEjercicio
		END
	Select NUMERO_DE_COMPROBANTE = @Numero

	--chequear que el transportista no este vacio 
	IF @IdTransporte=''
	BEGIN
	set @IdTransporte =NULL
	SET @IdReagTransporte=NULL
	END
	
	select @IdTipoComprobanteExt = tc.idtipocomprobanteafip 
	from PHY_WINSIFAC_01130_01_00001_0100.dbo.TiposComprobante tc 
	where tc.IdTipoComprobante = @IdTipoComprobante
	
	SELECT ID_TIPO_COMPROBANTE_EXT = @IdTipoComprobanteExt

	--ejecutar sp que crea el remito
	exec PHY_WINSIFAC_01130_01_00001_0100.dbo.SpFACStock_Insert_Update_Rem 
	@ABMD,          
	@IdCabecera,   
	@IdEjercicio,          
	@Sucursal,      
	@Fecha,      
	@IdTipoComprobante,      
	@Numero,      
	@IdAuxi,      
	@IdCtaAuxi,      
	@IdTipoDocumento,      
	@NumeroDocumento,      
	@NombreTercero,      
	@CategoriaIVA,      
	@Observaciones2,        
	@IddDeposito,        
	@IdaDeposito,        
	@IdAuxiListaPrecios,   
	@IdReagListaPrecios,   
	@IdListaPrecios,        
	@IdReagVendedor,   
	@IdVendedor,   
	@IdReagTransporte,        
	@IdTransporte,   
	@IdReagDescuento,        
	@IdDescuento1, 
	@Descuento1,   
	@IdDescuento2, 
	@Descuento2,   
	@IdReagObservaciones,     
	@IdCodObservaciones,        
	@Referencia,   
	@IdReagCondPago,   
	@IdCondPago,        
	@FormaCosteo,    
	@Alcance,   
	@ModoCarga,   
	@IdMoneda,   
	@Serie,   
	@TasaCambio,   
	@ImporteTotal ,       
	@IdUsuario,          
	@IdConexion,  
	@CodCampania, 
	@Planta, 
	@FechaExt,          
	@IdTipoComprobanteExt,          
	@NumeroExt, 
	@FechaVencimientoCAI, 
	@IdPais,                
	@IdProvincia,  
	@IdCabeceraRepl, 
	@NumeroCAI, 
	@NroGuia, 
	@IdIdioma, 
	@IdMonedaPrint,        
	@SeriePrint,        
	@TasaPrint      


	set @result='OK - Creación de Remito Exitosa'
	return 1  
END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_updateDestInv]    Script Date: 17/2/2024 9:23:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 17-2-2020
-- Description:	SP actualiza los destinos de piezas y contenedores 
--				en funcion a los destinos de piezas y contenedores
--				que estan en el inventario
-- =============================================
CREATE PROCEDURE [dbo].[sp_updateDestInv]
	 @fechaInventario datetime ,@bultosAjustados int out
AS
BEGIN
	SET NOCOUNT ON;
	set @bultosAjustados =0

	UPDATE Pesadas set pesadas.idDestino = Inventario.iddestino
	FROM Pesadas
	INNER JOIN Inventario ON CAST(Inventario.idPieza as INT) = Pesadas.id
	WHERE ISNUMERIC(inventario.idpieza) = 1 AND  Inventario.fechaInicioInventario = CAST(@fechaInventario as DATE)
	set @bultosAjustados = @bultosAjustados + @@ROWCOUNT
	
	UPDATE contenedores set contenedores.idDestino = Inventario.iddestino
	FROM Contenedores
	INNER JOIN Inventario ON cast( replace(idPieza,'A','') as int) = Contenedores.id
	WHERE ISNUMERIC(inventario.idpieza) = 0 AND  Inventario.fechaInicioInventario = CAST(@fechaInventario as DATE)	
	set @bultosAjustados = @bultosAjustados + @@ROWCOUNT
END

GO
USE [master]
GO
ALTER DATABASE [MeatWeigherManagerv40] SET  READ_WRITE 
GO
