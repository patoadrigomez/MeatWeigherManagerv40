UPDATE dbo.Pesadas 
SET FechaVencimiento = DATEADD(day, p.diasvencimiento, pes.fecha_hora)
FROM dbo.Pesadas as pes JOIN dbo.Productos as p ON pes.idproducto = p.id