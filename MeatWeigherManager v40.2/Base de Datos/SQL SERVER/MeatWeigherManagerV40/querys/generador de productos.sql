/*
Generar nuevos productos a partir de productos SAC
cuyo codigo SAC no exista en la tabla productos.
*/

insert MeatWeigherManagerv20.dbo.Productos
([codigoProductoSAC]
           ,[nombre]
           ,[idtipo]
           ,[numSenasa]
           ,[pesoNetoPredef]
           ,[unidadesPredef]
           ,[pesoTaraPredef]
           ,[diasvencimiento]
           ,[esinsumo]
           ,[espesable]
           ,[textAuxL1]
           ,[textAuxL2]
           ,[nombreL1]
           ,[nombreL2]
           ,[nombreL3]
           ,[nombreL4]
           ,[nombreL5]
           ,[nombreL6]
           ,[rendimientoSTD]
           ,[esCombo]
           ,[esCaja])
 select idProducto,Descripcion,1,'-',0.0,1,0.0,30,0,1,'','','',Descripcion,'','','','',0.0,0,0
 from PHY_WINSIFAC_00991_01_10001_0100.dbo.FACProductos fp
where fp.idProducto not in (select codigoProductoSAC from MeatWeigherManagerv20.dbo.Productos)


 
          


