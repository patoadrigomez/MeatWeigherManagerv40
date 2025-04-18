USE [PHY_WINSIFAC_00991_01_10001_0100]
GO
/****** Object:  Table [dbo].[CuentasAuxi]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentasAuxi](
	[idAuxi] [smallint] NOT NULL,
	[idCtaAuxi] [varchar](12) NOT NULL,
	[idPpal] [smallint] NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Imputable] [bit] NOT NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[idAuxi] ASC,
	[idCtaAuxi] ASC,
	[Imputable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ejercicios]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ejercicios](
	[IdEjercicio] [smallint] NOT NULL,
	[IdPpal] [smallint] NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaCierre] [datetime] NOT NULL,
	[Nombre] [varchar](40) NULL,
	[Estado] [varchar](1) NULL,
	[FechaCierreIntermedio] [datetime] NULL,
	[FechaCierreProvisorio] [datetime] NULL,
	[IdUsuario] [smallint] NULL,
	[FechaHora] [datetime] NULL,
	[Oculto] [bit] NULL,
	[FechaDesdeAjuste] [datetime] NULL,
	[FechaUltimoRei] [datetime] NULL,
	[FechaUltimaDC] [datetime] NULL,
	[Invisible] [bit] NULL,
 CONSTRAINT [XPKEjercicios] PRIMARY KEY CLUSTERED 
(
	[IdEjercicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACCabeceras]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACCabeceras](
	[IdCabecera] [int] NOT NULL,
	[IdEjercicio] [smallint] NULL,
	[IdComprobante] [int] NULL,
	[IdPpal] [smallint] NOT NULL,
	[Sucursal] [char](4) NULL,
	[Fecha] [datetime] NULL,
	[IdTipoComprobante] [char](8) NOT NULL,
	[Numero] [char](12) NULL,
	[IdAuxi] [smallint] NOT NULL,
	[IdCtaAuxi] [char](12) NOT NULL,
	[IdTipoDocumento] [char](5) NULL,
	[NumeroDocumento] [char](12) NULL,
	[NombreTercero] [char](40) NULL,
	[CategoriaIVA] [varchar](2) NULL,
	[FechaVencimientoCAI] [datetime] NULL,
	[FechaExterno] [datetime] NULL,
	[TipoExterno] [char](8) NULL,
	[NumeroExterno] [char](12) NULL,
	[FechaIVA] [datetime] NULL,
	[TipoIVA] [char](8) NULL,
	[NumeroIVA] [char](12) NULL,
	[Observaciones] [varchar](500) NULL,
	[Afectacion] [tinyint] NULL,
	[IdReagVendedor] [smallint] NULL,
	[IdVendedor] [varchar](12) NULL,
	[IdaDeposito] [char](5) NULL,
	[IddDeposito] [char](5) NULL,
	[IdAuxiListaPrecios] [smallint] NULL,
	[IdReagListaPrecios] [smallint] NULL,
	[IdListaPrecios] [char](12) NULL,
	[IdReagCondPago] [smallint] NULL,
	[IdCondPago] [char](12) NULL,
	[IdReagDescuento] [smallint] NULL,
	[IdCDescuento1] [varchar](12) NULL,
	[Descuento1] [money] NULL,
	[IdCDescuento2] [varchar](12) NULL,
	[Descuento2] [money] NULL,
	[IdImputacion] [char](12) NULL,
	[IdReagObservaciones] [smallint] NULL,
	[IdCodObservaciones] [varchar](12) NULL,
	[IdReagTransporte] [smallint] NULL,
	[IdTransporte] [varchar](12) NULL,
	[CantPaquetes] [numeric](4, 0) NULL,
	[ValorDeclarado] [money] NULL,
	[ImporteTotal] [money] NULL,
	[ImporteNoGravado] [money] NULL,
	[ImporteGravado] [money] NULL,
	[ImporteIVA] [money] NULL,
	[ImportePercepcion] [money] NULL,
	[ImporteIIII] [money] NULL,
	[ImporteOtros] [money] NULL,
	[Terminal] [char](6) NULL,
	[ActPrecioRep] [char](1) NULL,
	[ActPrecioUltCompra] [char](1) NULL,
	[Depurado] [bit] NULL,
	[Referencia] [char](20) NULL,
	[IdLotePeriodo] [int] NULL,
	[Alcance] [tinyint] NULL,
	[ModoCarga] [tinyint] NULL,
	[IdMoneda] [char](5) NULL,
	[Serie] [tinyint] NULL,
	[Tasa] [float] NULL,
	[CtaOrden] [bit] NULL,
	[Origen] [tinyint] NULL,
	[IdUsuario] [smallint] NULL,
	[FechaHora] [datetime] NULL,
	[Anulado] [bit] NULL,
	[FechaBaja] [smalldatetime] NULL,
	[ImporteExento] [money] NULL,
	[IdClienteEventual] [int] NULL,
	[CodCampania] [int] NULL,
	[IdEstado] [smallint] NULL,
	[IdMonedaPrint] [char](5) NULL,
	[SeriePrint] [tinyint] NULL,
	[TasaPrint] [float] NULL,
	[IdConcepto] [int] NULL,
	[IdPais] [smallint] NULL,
	[IdProvincia] [smallint] NULL,
	[NumeroCAI] [varchar](14) NULL,
	[NroGUIA] [varchar](20) NULL,
	[IdIdioma] [int] NULL,
	[ImpresoFiscal] [bit] NULL,
 CONSTRAINT [PK_FACCabeceras1] PRIMARY KEY CLUSTERED 
(
	[IdCabecera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACProductos]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACProductos](
	[idProducto] [char](12) NOT NULL,
	[idPlanProducto] [smallint] NULL,
	[Descripcion] [char](40) NOT NULL,
	[Imputable] [bit] NOT NULL,
	[alias] [varchar](50) NULL,
	[EsServicio] [bit] NOT NULL,
 CONSTRAINT [PK_FACProductos] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC,
	[Imputable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACStock]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACStock](
	[IdEjercicio] [smallint] NULL,
	[IdComprobante] [int] NULL,
	[IdPpal] [smallint] NOT NULL,
	[IdCabecera] [int] NOT NULL,
	[Sucursal] [char](4) NULL,
	[Fecha] [datetime] NULL,
	[IdTipoComprobante] [char](8) NOT NULL,
	[Numero] [char](12) NULL,
	[IdAuxi] [smallint] NOT NULL,
	[IdCtaAuxi] [char](12) NOT NULL,
	[IdTipoDocumento] [char](5) NULL,
	[NumeroDocumento] [char](12) NULL,
	[IdMovimiento] [numeric](4, 0) NOT NULL,
	[IdSubMovimiento] [numeric](4, 0) NULL,
	[NroOrden] [numeric](4, 0) NULL,
	[PedIdCabecera] [int] NULL,
	[PedIdMovimiento] [numeric](4, 0) NULL,
	[PedCantidad] [numeric](13, 4) NULL,
	[RemIdCabecera] [int] NULL,
	[RemIdMovimiento] [numeric](4, 0) NULL,
	[RemCantidad] [numeric](13, 4) NULL,
	[FacIdCabecera] [int] NULL,
	[FacIdMovimiento] [numeric](4, 0) NULL,
	[FacCantidad] [numeric](13, 4) NULL,
	[FacClase] [char](4) NULL,
	[IdDeposito] [char](5) NULL,
	[IdPlanProducto] [smallint] NULL,
	[IdProducto] [char](12) NOT NULL,
	[IdPartida] [varchar](12) NULL,
	[NumeroSerie] [char](20) NULL,
	[IdUM] [char](5) NULL,
	[Cantidad] [numeric](13, 4) NULL,
	[IdUMPrimaria] [char](5) NULL,
	[CantidadUMPrimaria] [numeric](13, 4) NULL,
	[PrecioUnitario] [float] NULL,
	[PDescuento] [numeric](6, 3) NULL,
	[PrecioUnitarioNeto] [float] NULL,
	[PrecioNeto] [float] NULL,
	[ImpuestosInternos] [money] NULL,
	[TasaIVA] [money] NULL,
	[FechaVencimiento] [datetime] NULL,
	[AcumulaProducto] [bit] NULL,
	[Stock] [bit] NULL,
	[IdUMStock] [char](5) NULL,
	[CantidadUMStock] [numeric](13, 4) NULL,
	[PrecioUMStock] [money] NULL,
	[CostoUMStock] [money] NULL,
	[FechaCostoUMStock] [datetime] NULL,
	[CostoUMStockFunc] [money] NULL,
	[IdOrigenAplicacion] [char](12) NULL,
	[IdAuxiPropietario] [smallint] NULL,
	[IdCtaAuxiPropietario] [varchar](12) NULL,
	[IdLiquidoProducto] [int] NULL,
	[IdCabeceraViaje] [int] NULL,
	[IdMovimientoViaje] [numeric](4, 0) NULL,
	[Detalle] [varchar](2048) NULL,
	[Terminal] [char](6) NULL,
	[Depurado] [bit] NULL,
	[IdUsuario] [smallint] NULL,
	[FechaHora] [datetime] NULL,
	[Anulado] [bit] NULL,
	[FechaBaja] [datetime] NULL,
	[CodCampania] [int] NULL,
	[IdProductoConjunto] [char](12) NULL,
	[NivelConjunto] [smallint] NULL,
	[PrecioRecupero] [money] NULL,
	[CantidadUMRemesa] [money] NULL,
	[CantidadUMDif] [money] NULL,
	[CantidadUMPorc] [money] NULL,
	[CantidadUMPRemesa] [money] NULL,
	[CantidadUMPDif] [money] NULL,
	[CantidadUMPPorc] [money] NULL,
	[CodCampo] [int] NULL,
	[CodLote] [int] NULL,
	[NroDTA] [varchar](9) NULL,
	[Estado] [int] NULL,
	[FechaEstado] [datetime] NULL,
	[PrecioImpuestoInterno] [money] NULL,
	[ProductoDesdoblado] [bit] NULL,
	[CantidadUMPDestino] [money] NULL,
	[PrecioITC] [money] NULL,
	[ITC] [money] NULL,
	[IdItem] [smallint] NULL,
	[IdUbicacion] [varchar](12) NULL,
	[PesoBruto] [numeric](13, 4) NULL,
	[IdAuxiComprador] [smallint] NULL,
	[IdCtaAuxiComprador] [varchar](12) NULL,
 CONSTRAINT [PK_FACStock1_1] PRIMARY KEY CLUSTERED 
(
	[IdCabecera] ASC,
	[IdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumeracionPrefijos]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NumeracionPrefijos](
	[IdEjercicio] [smallint] NOT NULL,
	[IdPpal] [smallint] NOT NULL,
	[IdNumerador] [smallint] NOT NULL,
	[IdPrefijo] [smallint] NOT NULL,
	[Numero] [int] NOT NULL,
	[IdUsuario] [smallint] NULL,
	[FechaHora] [datetime] NULL,
 CONSTRAINT [XPKNumeracionPrefijos] PRIMARY KEY CLUSTERED 
(
	[IdEjercicio] ASC,
	[IdPpal] ASC,
	[IdNumerador] ASC,
	[IdPrefijo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReagrupacionCuentasAuxi]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReagrupacionCuentasAuxi](
	[IdPpal] [smallint] NOT NULL,
	[IdAuxi] [smallint] NOT NULL,
	[IdReagAuxi] [smallint] NOT NULL,
	[IdCtaReagAuxi] [varchar](12) NOT NULL,
	[IdCtaAuxi] [varchar](12) NOT NULL,
 CONSTRAINT [XPKReagrupacionCuentasAuxi] PRIMARY KEY CLUSTERED 
(
	[IdPpal] ASC,
	[IdAuxi] ASC,
	[IdReagAuxi] ASC,
	[IdCtaReagAuxi] ASC,
	[IdCtaAuxi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposComprobante]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposComprobante](
	[IdPpal] [smallint] NOT NULL,
	[IdTipoComprobante] [varchar](8) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Mascara] [tinyint] NOT NULL,
	[IdNumerador] [smallint] NULL,
	[FechaAlta] [datetime] NULL,
	[FechaBaja] [datetime] NULL,
	[Copias] [tinyint] NOT NULL,
	[Externo] [bit] NULL,
	[IdTipoComprobanteAFIP] [varchar](8) NULL,
	[IVA] [varchar](1) NOT NULL,
	[PorDefecto] [bit] NULL,
	[Observaciones] [varchar](500) NULL,
	[IdUsuario] [smallint] NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[IdAuxi] [smallint] NULL,
	[IVAAFIP] [varchar](1) NULL,
	[Afectacion] [tinyint] NOT NULL,
	[TipoCarga] [tinyint] NOT NULL,
	[Acceso] [tinyint] NOT NULL,
	[IdSistema] [char](19) NULL,
	[IdImpresion] [char](3) NULL,
	[Letra] [char](1) NOT NULL,
	[Signo] [char](1) NOT NULL,
	[PermiteDuplic] [bit] NOT NULL,
	[FechaAuditoria] [datetime] NULL,
	[Origen] [tinyint] NOT NULL,
	[ComprRetAFIP] [tinyint] NOT NULL,
	[ComprobRestitucion] [bit] NULL,
	[IdTipoFormato] [smallint] NULL,
	[IdFormato] [smallint] NULL,
	[Alcance] [tinyint] NOT NULL,
	[ImprimeEnProceso] [bit] NOT NULL,
	[TerceroAdicional] [bit] NULL,
	[PerfeccionaPorPresupuesto] [bit] NULL,
	[InformaIIBBPropios] [bit] NULL,
	[Servidor] [sysname] NULL,
	[NoRegistrable] [bit] NULL,
	[BaseRelacionada] [sysname] NULL,
	[ImprimeAsientoEnProceso] [bit] NULL,
	[EsReciboCobrador] [bit] NULL,
	[IdTipoFormatoCobrador] [smallint] NULL,
	[IdFormatoCobrador] [smallint] NULL,
	[CodOperacionAFIP] [varchar](1) NULL,
	[SoloDeposito] [bit] NULL,
	[IdLibro] [smallint] NULL,
 CONSTRAINT [XPKTiposComprobante] PRIMARY KEY CLUSTERED 
(
	[IdPpal] ASC,
	[IdTipoComprobante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[V_CantidadesPedidosPendientes]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[V_CantidadesPedidosPendientes](
	[idAuxi] [smallint] NOT NULL,
	[idCtaAuxi] [varchar](12) NOT NULL,
	[PedIdCabecera] [int] NOT NULL,
	[PedIdMovimiento] [numeric](4, 0) NOT NULL,
	[Cantidad] [numeric](38, 4) NOT NULL,
	[CantUMP] [numeric](38, 8) NULL,
 CONSTRAINT [PK_V_CantidadesPedidosPendientes] PRIMARY KEY CLUSTERED 
(
	[idAuxi] ASC,
	[idCtaAuxi] ASC,
	[PedIdCabecera] ASC,
	[PedIdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Afect__10B661E4]  DEFAULT ((1)) FOR [Afectacion]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__TipoC__11AA861D]  DEFAULT ((1)) FOR [TipoCarga]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Acces__129EAA56]  DEFAULT ((1)) FOR [Acceso]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Letra__1392CE8F]  DEFAULT ('') FOR [Letra]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Signo__1486F2C8]  DEFAULT ('') FOR [Signo]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Permi__157B1701]  DEFAULT ((0)) FOR [PermiteDuplic]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Orige__6E6D022F]  DEFAULT ((0)) FOR [Origen]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Compr__10A2E9F9]  DEFAULT ((0)) FOR [ComprRetAFIP]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Alcan__79EC03D8]  DEFAULT ((0)) FOR [Alcance]
GO
ALTER TABLE [dbo].[TiposComprobante] ADD  CONSTRAINT [DF__TiposComp__Impri__0B23DED7]  DEFAULT ((0)) FOR [ImprimeEnProceso]
GO
/****** Object:  StoredProcedure [dbo].[spFacGrabarTemporalesReferenciado]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

/* spFacGrabarTemporalesReferenciado 223425, -1, 999 */
CREATE PROCEDURE [dbo].[spFacGrabarTemporalesReferenciado]  
    @IdCabeceraOrigen Int,  
    @IdUsuario	smallint,  
	@IdConexion Int  
AS  
SET NOCOUNT ON
/*
SE quito el cuerpo de este SP dado que es utilizado a modo simulacion por no estar en contexto Physis.
*/


GO
/****** Object:  StoredProcedure [dbo].[SpFACStock_Insert_Update_Rem]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpFACStock_Insert_Update_Rem] 
	@ABMD        char(1), /* Alta Modificacion */          	@IdCabecera        int,   	@IdEjercicio   smallint,          	@Sucursal        char(4),      	@Fecha        datetime,      	@IdTipoComprobante   char(8),      	@Numero        varchar(12),      	@IdAuxi        smallint,      	@IdCtaAuxi        varchar(12),      	@IdTipoDocumento   varchar(5),      	@NumeroDocumento   varchar(12),      	  	@NombreTercero   varchar(40),      	@CategoriaIVA   varchar(2),      	  	@Observaciones   varchar(500),        	@IddDeposito   char(05),        	@IdaDeposito   char(05),        	@IdAuxiListaPrecios   smallint,   	@IdReagListaPrecios   smallint,   	@IdListaPrecios   char(12),        	@IdReagVendedor   smallint,   	@IdVendedor        char(12),   	@IdReagTransporte   smallint,        	@IdTransporte   char(12),   	@IdReagDescuento   smallint,        	@IdDescuento1   char(12),        	@Descuento1        money,        	@IdDescuento2   char(12),        	@Descuento2        money,      	@IdReagObservaciones   smallint,     	@IdCodObservaciones   char(12),        	@Referencia    char(20),   	     	@IdReagCondPago   smallint,   	@IdCondPago    char(12),        	  	@FormaCosteo   char(5),    	@Alcance    tinyint,   	@ModoCarga    tinyint,   	@IdMoneda    char(5),   	@Serie    tinyint,   	@TasaCambio    float,   	    	@ImporteTotal   money,       	@IdUsuario    smallint,          	@IdConexion    int,  	@CodCampania    smallint = Null, 	@Planta    Bit= 1, 	@FechaExt        datetime = null,          	@IdTipoComprobanteExt   char(8) = null,          	@NumeroExt        varchar(12) = null, 	@FechaVencimientoCAI   datetime = null, 	@IdPais               smallint = null,                	@IdProvincia          smallint = null,  	@IdCabeceraRepl    int = 0, 	@NumeroCAI            varchar(14) = Null, 	@NroGuia             varchar(14) = Null, 	@IdIdioma     Int = Null, 	@IdMonedaPrint    Char(5) = Null,        	@SeriePrint     TinyInt = Null,        	@TasaPrint     float = Null      
AS
BEGIN
set @idcabecera=9999
select NumeroDefinitivo=@numero,IdCabecera=@idCabecera
END
GO
/****** Object:  StoredProcedure [dbo].[SpFACStock_Tmp_Delete]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpFACStock_Tmp_Delete] 
	@IdConexion int   
AS
BEGIN
select 'TEMPORALES BORRADOS PARA IDCONEXION:  ' +cast(@IdConexion as varchar(10))
END
GO
/****** Object:  StoredProcedure [dbo].[SpFACStock_Tmp_Insert]    Script Date: 17/8/2022 12:21:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpFACStock_Tmp_Insert] 
	@IdMovimiento     smallint,      	@NroOrden      numeric(4, 0),      	@Producto      char (12),      	@IdAuxiPropietario    smallint,   	@IdCtaAuxiPropietario   varchar(12),   	@Partida      char (10),      	@UM       char (5),      	@CantidadUM     numeric(13, 4),      	@CantidadUMP     numeric(13, 4),      	@PrecioUnitario    float,      	@Descuento      numeric(6, 3),      	@PrecioUnitarioNeto   float,      	@PrecioNeto     float,      	@ImpuestosInternos    money,     	@FechaVencimiento    datetime,      	@Observaciones     varchar (2048),      	@AcumulaProducto    bit,   	@PedIdCabecera    int,   	@PedIdMovimiento    numeric(4,0),      	@PedCantidad     numeric(13, 4),      	@RemIdCabecera    int,   	@RemIdMovimiento    numeric(4,0),      	@RemCantidad     numeric(13, 4),      	@FacIdCabecera    int,   	@FacIdMovimiento    numeric(4,0),      	@FacCantidad     numeric(13,4),   	@IdLiquidoProducto    int,   	@IdCabeceraViaje   int,   	@IdMovimientoViaje   numeric(4,0),   	@IdConexion     int,      	@FacClase                Char(4) = Null,  	@ProductoConjunto     char (12) = Null, 	@NivelConjunto     int = Null, 	@IdPlanProducto    smallint = Null,   	@RecuperoKgLimpio    money = 0, 	@IdDeposito     char(5) = '',  	@CantidadUMRemesa   numeric(13, 4) = 0, 	@CantidadUMDif     numeric(13, 4) = 0, 	@CantidadUMPorc    numeric(13, 4) = 0, 	@CantidadUMPRemesa    numeric(13, 4) = 0, 	@CantidadUMPDif    numeric(13, 4) = 0, 	@CantidadUMPPorc    numeric(13, 4) = 0, 	@CodCampo     int = Null, 	@CodLote     int = Null, 	@NroDTA      varchar(9) = Null, 	@Estado      int = Null, 	@FechaEstado    datetime = Null, 	@PrecioImpuestoInterno  money = 0, 	@ProductoDesdoblado   bit = 0,  	@CantidadUMPDestino   numeric(13, 4) = 0,  	@ITC      money = 0, 	@PrecioITC     money = 0,  	@IdItem      smallint = 0, 	@IdUbicacion    varchar(12) = Null, 	@PesoBruto     numeric(13,4) = Null,      	@IdAuxiComprador    smallint = Null,   	@IdCtaAuxiComprador   varchar(12) = Null,      	@IdCabeceraServicio   int = Null,   	@IdMovimientoServicio   int = Null   	
AS
BEGIN
select @pedIdCabecera as IdCabecera,@pedidmovimiento as IdMovimiento,@pedCantidad as CantidadUnds,@CantidadUM as UNDS_REM,@CantidadUMP as PESO_REM
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'siempre = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACProductos', @level2type=N'COLUMN',@level2name=N'idPlanProducto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'siempre = 1 para productos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACProductos', @level2type=N'COLUMN',@level2name=N'Imputable'
GO
