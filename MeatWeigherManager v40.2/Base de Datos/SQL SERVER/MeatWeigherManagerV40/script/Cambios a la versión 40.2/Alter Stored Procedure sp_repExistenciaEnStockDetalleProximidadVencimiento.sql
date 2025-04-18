USE [MeatWeigherManagerv40_030225]
GO
/****** Object:  StoredProcedure [dbo].[sp_repExistenciaEnStockDetalleProximidadVencimiento]    Script Date: 21/3/2025 08:49:53 ******/
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
ALTER PROCEDURE [dbo].[sp_repExistenciaEnStockDetalleProximidadVencimiento]
@idTipoProducto int=0,@idProducto int=0,@hasta date = null,@idUbicacion int=0, @diasProximidadVencimiento int=0
 
AS
BEGIN

SELECT
TIPO='PIEZA',
p.id as NRO ,
de.nombre as UBICACION,
Convert(varchar(10),CONVERT(date,p.fecha_hora,106),103) as LOTE,
prd.nombre as PRODUCTO,
p.unidades as UNIDADES,
p.PesoNeto as NETO,
p.FechaVencimiento as VENCIMIENTO
FROM fPiezasEnStock(@hasta) as p , productos as prd ,destinos as de   
WHERE 
p.idproducto = prd.id 
and de.id = p.iddestino 
and (@idTipoProducto=0 or @idTipoProducto=prd.idtipo)
and (@idProducto=0 or @idProducto=prd.id)
AND (@idUbicacion = 0 or @idUbicacion=p.iddestino)
and DATEDIFF(day,@hasta,p.FechaVencimiento) <= @diasProximidadVencimiento  
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
FROM fContenedoresEnStock(@hasta) ca    
LEFT OUTER JOIN productos prd ON ca.idproducto = prd.id    
LEFT OUTER JOIN destinos de ON ca.iddestino = de.id    
LEFT OUTER JOIN TiposContenedor tc ON ca.idtipo = tc.id
WHERE 
(@idTipoProducto=0 or @idTipoProducto=prd.idtipo)
and (@idProducto=0 or @idProducto=prd.id)
AND (@idUbicacion = 0 or @idUbicacion=ca.iddestino)
and DATEDIFF(day,@hasta,ca.fecha_vencimiento) <= @diasProximidadVencimiento  

ORDER BY VENCIMIENTO DESC

END