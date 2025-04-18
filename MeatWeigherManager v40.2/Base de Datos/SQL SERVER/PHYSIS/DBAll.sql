USE [PHY_WINSIFAC_01130_01_00001_0100]
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'FACProductos', N'COLUMN',N'Imputable'))
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACProductos', @level2type=N'COLUMN',@level2name=N'Imputable'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'FACProductos', N'COLUMN',N'idPlanProducto'))
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACProductos', @level2type=N'COLUMN',@level2name=N'idPlanProducto'
GO
/****** Object:  StoredProcedure [dbo].[spFACStockAuxiliaresTMP_Insert]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[spFACStockAuxiliaresTMP_Insert]
GO
/****** Object:  StoredProcedure [dbo].[spFACStockAuxiliaresTMP_Delete]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[spFACStockAuxiliaresTMP_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SpFACStock_Tmp_Insert]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[SpFACStock_Tmp_Insert]
GO
/****** Object:  StoredProcedure [dbo].[SpFACStock_Tmp_Delete]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[SpFACStock_Tmp_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SpFACStock_Insert_Update_Rem]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[SpFACStock_Insert_Update_Rem]
GO
/****** Object:  StoredProcedure [dbo].[spFacGrabarTemporalesReferenciado]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[spFacGrabarTemporalesReferenciado]
GO
/****** Object:  StoredProcedure [dbo].[spFACCopiaComprobante]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP PROCEDURE IF EXISTS [dbo].[spFACCopiaComprobante]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Impri__0B23DED7]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Alcan__79EC03D8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Compr__10A2E9F9]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Orige__6E6D022F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Permi__157B1701]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Signo__1486F2C8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Letra__1392CE8F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Acces__129EAA56]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__TipoC__11AA861D]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposComprobante]') AND type in (N'U'))
ALTER TABLE [dbo].[TiposComprobante] DROP CONSTRAINT IF EXISTS [DF__TiposComp__Afect__10B661E4]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACStockAuxiliares]') AND type in (N'U'))
ALTER TABLE [dbo].[FACStockAuxiliares] DROP CONSTRAINT IF EXISTS [DF_FACStockAuxiliares_IdSubMovimiento]
GO
/****** Object:  Table [dbo].[V_CantidadesPedidosPendientes]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[V_CantidadesPedidosPendientes]
GO
/****** Object:  Table [dbo].[TiposComprobante]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[TiposComprobante]
GO
/****** Object:  Table [dbo].[ReagrupacionCuentasAuxi]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[ReagrupacionCuentasAuxi]
GO
/****** Object:  Table [dbo].[NumeracionPrefijos]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[NumeracionPrefijos]
GO
/****** Object:  Table [dbo].[FACStockAuxiliares]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[FACStockAuxiliares]
GO
/****** Object:  Table [dbo].[FACStock]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[FACStock]
GO
/****** Object:  Table [dbo].[FACProductos]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[FACProductos]
GO
/****** Object:  Table [dbo].[FACCabeceras]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[FACCabeceras]
GO
/****** Object:  Table [dbo].[Ejercicios]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[Ejercicios]
GO
/****** Object:  Table [dbo].[CuentasAuxi]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP TABLE IF EXISTS [dbo].[CuentasAuxi]
GO
USE [master]
GO
/****** Object:  Database [PHY_WINSIFAC_01130_01_00001_0100]    Script Date: 17/2/2024 9:37:05 a. m. ******/
DROP DATABASE IF EXISTS [PHY_WINSIFAC_01130_01_00001_0100]
GO
/****** Object:  Database [PHY_WINSIFAC_01130_01_00001_0100]    Script Date: 17/2/2024 9:37:05 a. m. ******/
CREATE DATABASE [PHY_WINSIFAC_01130_01_00001_0100]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PHYSIS1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PHY_WINSIFAC_01130_01_00001_0100.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PHYSIS1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\PHY_WINSIFAC_01130_01_00001_0100_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PHY_WINSIFAC_01130_01_00001_0100].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ARITHABORT OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET  MULTI_USER 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET QUERY_STORE = OFF
GO
USE [PHY_WINSIFAC_01130_01_00001_0100]
GO
/****** Object:  Table [dbo].[CuentasAuxi]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ejercicios]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACCabeceras]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACProductos]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACStock]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACStockAuxiliares]    Script Date: 17/2/2024 9:37:05 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACStockAuxiliares](
	[IdCabecera] [int] NOT NULL,
	[IdMovimiento] [numeric](4, 0) NOT NULL,
	[IdSubMovimiento] [numeric](4, 0) NOT NULL,
	[IdPlanAuxiliar] [smallint] NOT NULL,
	[IdCtaAuxiliar] [varchar](12) NOT NULL,
 CONSTRAINT [PK_FACStockAuxiliares] PRIMARY KEY CLUSTERED 
(
	[IdCabecera] ASC,
	[IdMovimiento] ASC,
	[IdSubMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumeracionPrefijos]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReagrupacionCuentasAuxi]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposComprobante]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[V_CantidadesPedidosPendientes]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FACStockAuxiliares] ADD  CONSTRAINT [DF_FACStockAuxiliares_IdSubMovimiento]  DEFAULT ((0)) FOR [IdSubMovimiento]
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
/****** Object:  StoredProcedure [dbo].[spFACCopiaComprobante]    Script Date: 17/2/2024 9:37:05 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
 
--Realiza en Physis la copia de remitos generados a la base de datos que le corresponde 1 o 2 
CREATE PROCEDURE [dbo].[spFACCopiaComprobante] 
 
 @IdCabecera int 
 
As 
BEGIN 
 
Select @IdCabecera 
 
End 
 
GO
/****** Object:  StoredProcedure [dbo].[spFacGrabarTemporalesReferenciado]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SpFACStock_Insert_Update_Rem]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SpFACStock_Tmp_Delete]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SpFACStock_Tmp_Insert]    Script Date: 17/2/2024 9:37:05 a. m. ******/
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
/****** Object:  StoredProcedure [dbo].[spFACStockAuxiliaresTMP_Delete]    Script Date: 17/2/2024 9:37:05 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
CREATE PROCEDURE [dbo].[spFACStockAuxiliaresTMP_Delete]  
    (@Conexion int) 
AS 
     

GO
/****** Object:  StoredProcedure [dbo].[spFACStockAuxiliaresTMP_Insert]    Script Date: 17/2/2024 9:37:05 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
CREATE PROCEDURE [dbo].[spFACStockAuxiliaresTMP_Insert]  
    (@IdCabecera int, @IdMovimiento smallint, @NroOrden smallint,  
     @IdPlanAuxiliar smallint, @IdCtaAuxiliar varchar(12), @Conexion int) 
AS 
     

 

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'siempre = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACProductos', @level2type=N'COLUMN',@level2name=N'idPlanProducto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'siempre = 1 para productos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FACProductos', @level2type=N'COLUMN',@level2name=N'Imputable'
GO
USE [master]
GO
ALTER DATABASE [PHY_WINSIFAC_01130_01_00001_0100] SET  READ_WRITE 
GO
