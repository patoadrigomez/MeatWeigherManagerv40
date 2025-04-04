
GO
/****** Object:  StoredProcedure [dbo].[sp_getPesada]    Script Date: 03/02/2025 16:00:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 03-02-2025
-- Description:	SP obtiene una pesada con todos sus datos vinculados.
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPesada]
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
tip.nombre as NOMBRE_TIPIFICACION,
pe.FechaVencimiento as FECHA_VENCIMIENTO

FROM pesadas as pe  
LEFT OUTER JOIN operadores ope ON pe.idOperador = ope.id
LEFT OUTER JOIN OI ON pe.idOi = OI.id  
LEFT OUTER JOIN Productos as prd ON pe.idproducto = prd.id  
LEFT OUTER JOIN TiposProducto as tp ON prd.idtipo = tp.id  
LEFT OUTER JOIN Etiquetas e ON prd.idEtiqueta = e.id
LEFT OUTER JOIN operadores ooi ON OI.idOperador = ooi.id
LEFT OUTER JOIN Tipificaciones tip ON pe.idTipificacion=tip.id
LEFT OUTER JOIN PHY_WINSIFAC_01025_01_00001_0100.dbo.CuentasAuxi as prove ON prove.idCtaAuxi = oi.codigoProveedorSAC and prove.idAuxi=1 and prove.idPpal=1 
and prove.Imputable=1 
LEFT OUTER JOIN destinos de ON de.id = pe.iddestino  
LEFT OUTER JOIN sectores sec ON sec.id = pe.idsector  
LEFT OUTER JOIN PHY_WINSIFAC_01025_01_00001_0100.dbo.FACProductos as pk ON pk.idProducto = prd.codigoProductoSAC AND pk.idPlanProducto=1 AND pk.Imputable=1 
WHERE pe.id = @idPesada


END
