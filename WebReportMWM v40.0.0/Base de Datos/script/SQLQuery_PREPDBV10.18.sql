/*****************************************************************
PREPARACION DE TABLAS PARA ADAPTACION A CAMBIOS A LA VERSION 10.18
******************************************************************/

/******************************************************************
	MODIFICACIONES EN LA ESTRUCTURA DE BASE DE DATOS
*******************************************************************/

/*
	NUEVA COLUMNA FECHA_VENCIMIENTO EN TABLA CONTENEDORES. DE TIPO DATETIME PERMITE 
	NULL. LA MISMA POSEERA LA FECHA DE VENCIMIENTO DEL CONTENEDOR. ESTA SE CALCULA CON LA FECHA
	DE VENCIMIENTO MAS ANTIGUA DE LAS PIEZAS CONTENIDAS.
	
	NUEVA TABLA TIPOSBULTO ID(CHAR(3)) NOMBRE(VARCHAR(20)).
	CARGAR DOS REGISTROS CON ID=PZA NOMBRE=PIEZA ID=CNT NOMBRE=CONTENEDOR.
		
	SE DEBE AGREGAR UNA NUEVA COLUMNA IDTIPOBULTO (CHAR(3)) A LA TABLA EGRESOS .
	LA MISMA DEBE SER PARTE DE LA CLAVE PRINCIPAL IDPEDIDO-IDTIPOBULTO-IDPESAJE

	SE DEBE AGREGAR UNA NUEVA COLUMNA IDTIPOBULTO (CHAR(3)) A LA TABLA DEVOLUCIONES .
	LA MISMA DEBE SER PARTE DE LA CLAVE PRINCIPAL IDPEDIDO-IDTIPOBULTO-IDPESAJE

	SE DEBE AGREGAR UNA NUEVA COLUMNA EN LA TABLA CONTENEDORES FECHA_DESARMADO(DATETIME).
	
	ELIMINAR CLAVE PRINCIPAL IDPESAJE EN TABLA CONTENEDORPIEZAS , NO DEFINE CLAVE PRINCIPAL DADO 
	QUE DEBE SER IDCONTENEDOR-IDPESAJE PERO COMO IDCONTENEDRO DEBE PERMITIR NULL NO ES POSIBLE CREARLA.
	PARA RESOLVER ESTO CREAMOS UN INDICE CLUSTER IDXIDCONTENEDORIDPESAJE NO UNICO Y DE ESTA MANERA TENEMOS
	INDEXACION.
	
	SE MODIFICA TABLA resultInventario QUEDANDO CON LA SIGUIENTE ESTRUCTURA:
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
		
	NUEVA TABLA DE REGISTRACION DE MOVIMIENTOS DE INSUMOS MOVINSUMOS
	
		CREATE TABLE [dbo].[MovInsumos](
			[idTipoMov] [char](3) NOT NULL,
			[idTipoProc] [char](3) NOT NULL,
			[idProc] [int] NOT NULL,
			[idPrdInsumo] [int] NOT NULL,
			[unidades] [float] NOT NULL,
			[fecha_hora] [datetime] NOT NULL,
		 KEY CLUSTERED 
		(
			[idTipoMov] ASC,
			[idTipoProc] ASC,
			[idProc] ASC,
			[idPrdInsumo] ASC
		)

	NUEVA TABLA DE REGISTRACION DE EVENTOS LOG
		CREATE TABLE [dbo].[dbLog](
			[fecha_hora] [datetime] NOT NULL,
			[idEstacion] [int] NOT NULL,
			[idOperador] [int] NOT NULL,
			[evento] [nvarchar](100) NOT NULL,
			[contexto] [nvarchar](100) NOT NULL,
			[detalle] [nvarchar](100) NOT NULL,
		 CONSTRAINT [PK_dbLog] PRIMARY KEY CLUSTERED 
		(
			[fecha_hora] ASC
		) ON [PRIMARY]

	NUEVA TABLA DE VINCULACION DE INSUMOS A PRODUCTOS
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
		) ON [PRIMARY]
	
*/


BEGIN TRY
	BEGIN TRANSACTION 


/******************************************************************
	PROCESO DE ASIGNACION DE FECHA DE VENCIMIENTO A CONTENEDORES 
	Dado que inicialmente en la version 10.18 poseen un nuevo campo
	fecha_vencimiento y los mismos estan en null , se los debe cargar
	con la fecha de vencimiento que se calcula tomando la fecha mas vieja
	de las piezas contenidas y se le suma los dias de vencimiento que
	posee el articulo definido el articulo de la pieza.
*******************************************************************/
	UPDATE c
	SET c.fecha_vencimiento = t.vencimiento 
	FROM Contenedores c
	JOIN
	 (
		SELECT cp.idcontenedor,MIN(DATEADD(DAY,prd.diasvencimiento,p.fecha_hora)) as vencimiento 
		FROM ContenedorPiezas cp
		join Pesadas p on p.id=cp.idpesaje
		join Productos prd on prd.id=p.idproducto
		GROUP BY cp.idcontenedor
	 ) t on t.idcontenedor=c.id
	 
	 --Eliminar contenedores huerfanos es decir que no tienen ninguna pieza contenida.
	 DELETE Contenedores where id not in (select idcontenedor from ContenedorPiezas)
	 --Asigno fecha del dia para contenedores que pueden llegar a tener null en la fecha de vencimiento.
	 UPDATE Contenedores set fecha_vencimiento = CURRENT_TIMESTAMP where fecha_vencimiento is null
	 
/******************************************************************
	PROCESO DE CONVERCION DE TABLA EGRESOS PARA NUEVA VERSION 10.18
	Los bultos egresados tipo piezas se identifican en la columna idTipoBulto
	con el id 'PZA'.
	Se crean e insertan nuevos registros de egresos de contenedores en funcion
	a las piezas egresadas de contenidos de contenedores.
	Se eliminan las piezas de contenido de contenedores. 
*******************************************************************/
	
	/*
	Cada registro en egresos que no forme parte de un contenedor
	sera marcado con tipo de bulto='PZA'
	*/
	print 'Marcando registros de piezas en Egresos'
	update e
	set e.idTipoBulto='PZA'
	from Egresos e
	where e.idpesaje not in (select idpesaje from contenedorpiezas)

	/*
	Genero registros de egresos de contenedore en la tabla Egresos.
	Lo hace detectando piezas que son partes de un contenedor.
	*/
	print 'Generando registros de contenedor en tabla Egresos'
	insert into egresos (idTipoBulto,idPedido,idEstacion,idOperador,fecha_hora,idpesaje)
	select 'CNT',e.idpedido,e.idestacion,e.idoperador,max(e.fecha_hora),cp.idcontenedor
	from Egresos e
	join ContenedorPiezas cp on cp.idpesaje=e.idpesaje
	where idTipoBulto <> 'PZA'
	group by e.idpedido,e.idestacion,e.idoperador,cp.idcontenedor
	

	print 'Borrando los registros de piezas de contenedor en la tabla egresos'
	delete Egresos
	where 
	(idtipobulto is null or idtipobulto = '   ')
	and 
	idpesaje in (select idpesaje from contenedorpiezas) 
	
/******************************************************************
	PROCESO DE CONVERCION DE TABLA DEVOLUCIONES PARA NUEVA VERSION 10.18
	Todos los registros en devoluciones borraran.
	El sistema de ajuste de inventario se ocupara de cargar las devoluciones 
	que correspondan. 
*******************************************************************/

	print 'Eliminando devoluciones que no poseen Egresos'
	delete devoluciones 

	COMMIT TRANSACTION
END TRY


BEGIN CATCH
	print 'Error en el proceso de adaptacion a version 10.18'
	ROLLBACK TRANSACTION
END CATCH


