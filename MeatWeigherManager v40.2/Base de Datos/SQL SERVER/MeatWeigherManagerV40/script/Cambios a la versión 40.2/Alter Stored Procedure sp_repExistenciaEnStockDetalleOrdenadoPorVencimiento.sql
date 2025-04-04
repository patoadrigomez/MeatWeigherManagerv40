
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento]    Script Date: 03/02/2025 16:00:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sergio Fasolo
-- Create date: 03-02-2025
-- Description:	retorna un detalle de cada bulto (pieza o contenedor)
--				que se encuentre en stock a la fecha ordenado
--				por fecha de vencimiento .  
-- =============================================
ALTER PROCEDURE [dbo].[sp_repExistenciaEnStockDetalleOrdenadoPorVencimiento]
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
/*DATEADD(day,prd.diasvencimiento,p.fecha_hora)*/ p.FechaVencimiento as VENCIMIENTO
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
