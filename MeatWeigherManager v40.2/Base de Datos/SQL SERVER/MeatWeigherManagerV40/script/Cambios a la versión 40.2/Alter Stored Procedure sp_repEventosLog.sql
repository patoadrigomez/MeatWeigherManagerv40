USE [MeatWeigherManagerv40_030225]
GO
/****** Object:  StoredProcedure [dbo].[sp_repEventosLog]    Script Date: 28/02/2025 02:44:14 p. m. ******/
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
ALTER PROCEDURE [dbo].[sp_repEventosLog]
@desde date ,@hasta date,@contexto varchar(100) = '',@evento varchar(100) = '', @detalle varchar(100) =''
 
AS
BEGIN
	SELECT lg.fecha_hora as FECHA,o.nombre as OPERADOR, lg.idEstacion as ESTACION,lg.evento as EVENTO,lg.contexto as CONTEXTO,lg.detalle as DETALLE
	FROM dbLog lg
	JOIN operadores o on o.id=lg.idoperador
	WHERE
	cast(lg.fecha_hora as date)  between @desde and @hasta
	AND (@contexto = '' OR lg.contexto like '%' +@contexto+'%')
	AND (@evento = '' OR lg.evento like '%' + @evento + '%')
	AND (@detalle = '' OR lg.detalle  like '%'+@detalle+'%')
	
END

