USE [MeatWeigherManagerv40_030225]
GO
/****** Object:  StoredProcedure [dbo].[sp_repIngPlantaDetalle]    Script Date: 06/02/2025 03:01:07 p. m. ******/
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
ALTER PROCEDURE [dbo].[sp_repIngPlantaDetalle]
@desde date ,@hasta date,@idProveedor varchar(12) ='',@idProducto int=0,@idTipoProducto int =0,@numTropa int=0, @manual int=2
 
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
		pe.FechaVencimiento as VENCIMIENTO, 
		CASE WHEN pe.Manual = 0 OR pe.Manual IS NULL THEN 'NO' ELSE 'SI' END as MANUAL,
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
 AND (@manual=2 or pe.Manual = @manual)
 ORDER BY pe.FECHA_HORA 
 
END
