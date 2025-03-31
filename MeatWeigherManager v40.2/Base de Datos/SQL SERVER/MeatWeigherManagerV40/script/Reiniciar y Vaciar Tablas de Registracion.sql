/*
	Script de reinicio de tablas del sistema MeatWeigherManager.
	Vacia todas las tablas de registracion de movimientos y reinicia
	los campos de identidad a 0 para los casos de tablas que tengan
	columnas identidad.
*/



--Borrar contenedores y reiniciar identidad. 
delete Contenedores
DBCC CHECKIDENT(contenedores, RESEED,0)
--Borrar contenedorPiezas
delete ContenedorPiezas
--Borrar cajas
delete cajas 
--Borrar combos.
delete combos
--Borrar dblog.
delete dblog
--Borrar devoluciones
delete Devoluciones
--Borrar DLP.
delete dlp
--Borrar Egresos
delete egresos
--borrar facturas.
delete facturas
--borrar remitos.
delete remitos
--borrar Inventario y reiniciar identidad.
delete Inventario
DBCC CHECKIDENT(Inventario, RESEED,0)
--borrar MovInsumos
delete MovInsumos
--borrar OI y reiniciar identidad.
delete OI
DBCC CHECKIDENT(oi, RESEED,0)
--Borrar pedidos y reiniciar identidad.
delete Pedidos
DBCC CHECKIDENT(pedidos, RESEED,0)
--Borrar pesadas y reiniciar identidad.
delete pesadas
DBCC CHECKIDENT(pesadas, RESEED,0)
--Borrar productosinsumos.
delete ProductoInsumos
--borrar resultinventario
delete resultInventario