--obtiene los ultimos egresos de las piezas en funcion del rango de fechas indicado.
select max(cast(fecha_hora as DATE)),idpesaje
from Egresos
where cast(fecha_hora as date) between {d '2020-10-17'} and {d '2020-11-20'} 
group by idpesaje
---------------------------------------------------------------------------------

--obtiene las piezas egresadas en un rango de fechas conciderando las devoluciones.
select max(cast(e.fecha_hora as DATE)),e.idpesaje
from Egresos e
where cast(e.fecha_hora as date) between {d '2020-10-17'} and {d '2020-11-20'} 
AND e.idpesaje not in 
(
	select idpesaje 
	from devoluciones 
	where cast(fecha_hora as date) between {d '2020-10-17'} and {d '2020-11-20'}
	AND cast(e.fecha_hora as DATE) <= cast(fecha_hora as date) 
)   
group by idpesaje
-------------------------------------------------------------------------------------

--obtiene el ultimo registro de egreso de una pieza
select * from
(
SELECT
  *,
  max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje)
FROM egresos
where idpesaje=240)tb
where max_date=fecha_hora
----------------------------------------------------------------------------------- 

/* 
Determinar si una pieza esta egresada .
Retorna el registro si esta egresada.
*/
select * from
(
SELECT
  *,
  max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje)
FROM egresos
where idpesaje=240)tb
where max_date=fecha_hora
and not exists   (select * from Devoluciones where idpedido=tb.idPedido and idpesaje=tb.idpesaje )


-------------------------------------------------------------------------------------
/* 
Lista piezas que estan Egresadas conciderando Egresos y Devoluciones.

*/
SELECT idpesaje FROM
(
	SELECT
	*,
	max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje)
	FROM egresos
)tb
WHERE max_date=fecha_hora
AND NOT EXISTS   
(SELECT * FROM Devoluciones WHERE idpedido=tb.idPedido and idpesaje=tb.idpesaje )


select * from pesadas p
inner join 
(SELECT idpesaje FROM
(
	SELECT
	*,
	max_date = MAX(fecha_hora) OVER (PARTITION BY idpesaje)
	FROM egresos
)tb
WHERE max_date=fecha_hora
AND NOT EXISTS   
(SELECT * FROM Devoluciones WHERE idpedido=tb.idPedido and idpesaje=tb.idpesaje )) as egrs ON egrs.idpesaje=p.id

