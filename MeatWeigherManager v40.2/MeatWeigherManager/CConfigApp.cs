using System;
using BalanzaSerialPort;
using INI_RW;
using MeatWeigherManager;

namespace ConfigApp
{
	/// <summary>
	/// CConfigApp.
	/// Gestor de Datos de configuracion de la aplicacion.
	/// Se ocupa tambien de la exportacion e importacion de los datos a un archivo ini.
	/// </summary>
    public class CConfigApp
	{
		//Base de datos
		public static string    m_servidorDB;
		public static string    m_nombreDB;
		public static string    m_userDB;
		public static string    m_passwordDB;
        public static bool      m_tipoSeguridadConexionDB_SSPI;

        //Conexion con Balanzas
        public static ScaleCnfg[] m_scalesCnfg = new ScaleCnfg[3] { new ScaleCnfg(), new ScaleCnfg() , new ScaleCnfg() };

       
        //conexion impresor de etiquetas 
        public static string    m_nombreImpresora;

        //conexion scanner zebra 
        public static string    m_modeloScannerZebra;
        public static bool      m_permiteSimularLecturaScanner;
        public static HostInterfaceScanner m_hostInterfaceScaneer;

        //propiedades Imprecion Etiqueta
        public static string    m_pathArchivoFormatosEtiquetas;
        public static string    m_nombreFormatoEtiquetaProducto;
        public static string    m_nombreFormatoEtiquetaContenedor;
        public static string    m_nombreFormatoEtiquetaCaja;
        public static string    m_nombreFormatoEtiquetaPedido;
        public static int       m_cantidadEtiquetasPorPesada;
        public static string    m_lineaDeTextoSuperiorDeEtiqueta;
        public static string    m_lineaDeTextoInferiorDeEtiqueta;
        public static string    m_encodingNameOutputPrinter;
        //Configuracion general
        public static int       m_idEstacion;
        public static string    m_pathDirectorioReportes;
        public static float     m_toleranceWeightBox;
        //Configuracion Operacion Ingreso a Planta
        public static bool      m_pesoUnidadesRemitidoObligatorio;
        public static float     m_toleranciaPesoPredefPesoBalanza_IngresoAPlanta;
        public static bool      m_mantenerUltimaTropaEntrePesajes;
        public static bool      m_mantenerUltimaTipificacionEntrePesajes;

        //Configuracion Operacion EGRESOS (Despachos)
        public static bool      m_permitirColectarMasCantidadesQueLasPedidasEnDespachos;


        //Empresa
        public static string    m_razonSocialEmpresa;
        public static string    m_pathLogoEmpresa;

        //Modo de Trabajo - ETIQUETADO
        public static bool      m_ingresoAPlanta_WeightLabelEnable;
        public static bool      m_pesajeEnProduccion_WeightLabelEnable;
        public static bool      m_pesajeEnProduccion_UnitsLabelEnable;
        public static bool      m_imprimirLineasTextoSuperiorInferiorDeEtiqueta;

        public CConfigApp()
		{
		}
		
        public static bool Importar()
		{
			CIniRw iniFile = new CIniRw("Setup.ini");
            m_servidorDB = iniFile.ReadValueString("BASE DE DATOS", "SERVIDOR", "SERVIDOR");
            m_nombreDB = iniFile.ReadValueString("BASE DE DATOS", "BASE_DE_DATOS", "MeatWeigherManagerv40");
            m_userDB = iniFile.ReadValueString("BASE DE DATOS", "USUARIO", "sa");
            m_passwordDB = iniFile.ReadValueString("BASE DE DATOS", "PASSWORD", "");
            m_tipoSeguridadConexionDB_SSPI = iniFile.ReadValueBool("BASE DE DATOS", "TIPO DE SEGURIDAD DE ACCESO A BASE DE DATOS SSPI", false);

            int idxScale = 1;
            foreach (ScaleCnfg sc in m_scalesCnfg)
            {
                sc.Name = iniFile.ReadValueString("CONEXION BALANZA", "NOMBRE_BALANZA"+idxScale, "BALANZA" + idxScale);
                sc.ComName = iniFile.ReadValueString("CONEXION BALANZA", "PUERTO_COM_BALANZA"+idxScale, "COM" + idxScale);
                sc.Protocolo = (BALANZA_SERIAL_PORT_PROTOCOLO)iniFile.ReadValueInt("CONEXION BALANZA", "PROTOCOLO_BALANZA" + idxScale, (int)BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO);
                sc.Decimales = iniFile.ReadValueInt("CONEXION BALANZA", "CANTIDAD_DECIMALES" + idxScale, 0);
                sc.Baudios = iniFile.ReadValueInt("CONEXION BALANZA", "BAUDIOS_COM_BALANZA" + idxScale, 9600);
                sc.DataBits = iniFile.ReadValueInt("CONEXION BALANZA", "BITS_DATOS_COM_BALANZA" + idxScale, 8);
                sc.Parity = iniFile.ReadValueInt("CONEXION BALANZA", "PARIDAD_COM_BALANZA" + idxScale, 0);
                sc.StopBits = iniFile.ReadValueInt("CONEXION BALANZA", "BITS_STOP_COM_BALANZA" + idxScale, 1);
                sc.HandShake = iniFile.ReadValueInt("CONEXION BALANZA", "HANDSHAKE_COM_BALANZA" + idxScale, 0);
                sc.Enable = iniFile.ReadValueBool("CONEXION BALANZA", "HABILITADA_BALANZA" + idxScale, false);
                sc.MaximoRangoZero = Convert.ToSingle(iniFile.ReadValueString("CONFIGURACION PESAJE", "RANGO_CERO_BALANZA" + idxScale, "0.00"));
                sc.RangoPesoValidoInferior = Convert.ToSingle(iniFile.ReadValueString("CONFIGURACION PESAJE", "RANGO_PESO_VALIDO_INFERIOR_BALANZA" + idxScale, "10.00"));
                sc.RangoPesoValidoSuperior = Convert.ToSingle(iniFile.ReadValueString("CONFIGURACION PESAJE", "RANGO_PESO_VALIDO_SUPERIOR_BALANZA" + idxScale, "100.00"));
                sc.MaximaDispercionEstable = Convert.ToSingle(iniFile.ReadValueString("CONFIGURACION PESAJE", "MAXIMA_DISPERCION_ESTABLE_BALANZA" + idxScale, "0.00"));
                sc.TiempoDeteccionEstable = iniFile.ReadValueInt("CONFIGURACION PESAJE", "TIEMPO_MS_DETECCION_ESTABLE_BALANZA" + idxScale, 1000);

                idxScale++;
            }

            m_nombreImpresora = iniFile.ReadValueString("CONEXION IMPRESOR", "NOMBRE_IMPRESORA", "");
            m_modeloScannerZebra = iniFile.ReadValueString("CONEXION SCANNER", "MODELO_SCANNER", "");
            m_hostInterfaceScaneer = (HostInterfaceScanner)iniFile.ReadValueInt("CONEXION SCANNER", "HOST INTERFACE TIPO", (int)HostInterfaceScanner.SNAPI_CoreScanner);

            m_permiteSimularLecturaScanner = iniFile.ReadValueBool("SIMULACION SCANNER", "PERMITE SIMULAR LECTURA SCANNER", false);

            m_pathArchivoFormatosEtiquetas = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "PATH_ARCHIVO_FORMATOS_ETIQUETAS", "C:\\FORMATOS_MTT.PRN");
            m_nombreFormatoEtiquetaProducto = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_SELLO", "PZAS");
            m_nombreFormatoEtiquetaContenedor = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_CONT", "CNTS");
            m_nombreFormatoEtiquetaCaja = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_CAJA", "CAJAS");
            m_nombreFormatoEtiquetaPedido = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_PEDIDO", "PED");
            m_cantidadEtiquetasPorPesada = iniFile.ReadValueInt("PROPIEDADES ETIQUETA", "CANTIDAD_ETIQUETAS", 1);
            m_lineaDeTextoSuperiorDeEtiqueta = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "LINEA DE TEXTO SUPERIOR DE ETIQUETA", "--");
            m_lineaDeTextoInferiorDeEtiqueta = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "LINEA DE TEXTO INFERIOR DE ETIQUETA", "--");
           
            /*  La siguiente configuracion establece en que mapa de caracteres (Encoding) se le debe hablar a la impresora.
            *   Si la impresora es una ZT230 el comando ^CI28 en el formato de etiqueta establece UTF-8 y por ende 
            *   el parametro de configuracion debe ser "utf-8".
            *   Si la impresora no soporta o no tiene este mapa de caracteres se podra usar ^CI13 (IBM 850) y el parametro
            *   de configuracion debe tener el valor "ibm850".
            */
            m_encodingNameOutputPrinter = iniFile.ReadValueString("PROPIEDADES ETIQUETA", "MAPA DE DE CARACTERES ACTIVO EN IMPRESORA", "utf-8");

            m_idEstacion = iniFile.ReadValueInt("GENERAL", "ID_ESTACION", 1);
            m_pathDirectorioReportes = iniFile.ReadValueString("GENERAL", "PATH_DIRECTORIO_REPORTES", "C:\\");

            m_razonSocialEmpresa = iniFile.ReadValueString("EMPRESA", "RAZON_SOCIAL", "EMPRESA S.A.");
            m_pathLogoEmpresa = iniFile.ReadValueString("EMPRESA", "PATH_LOGO_EMPRESA", "c:\\logo.jpeg");

            m_toleranceWeightBox = iniFile.ReadValueSingle("PESAJE DE CAJA", "TOLERANCIA_PORCENTUAL_PESAJE_CAJA",20.0f);

            m_ingresoAPlanta_WeightLabelEnable = iniFile.ReadValueBool("MODO DE TRABAJO", "ETIQUETA_DE_PRODUCTO_CON_PESO_EN_INGRESO_A_PLANTA", true);
            m_pesajeEnProduccion_WeightLabelEnable = iniFile.ReadValueBool("MODO DE TRABAJO", "ETIQUETA_DE_PRODUCTO_CON_PESO_EN_PESAJES_EN_PRODUCCION", true);
            m_pesajeEnProduccion_UnitsLabelEnable = iniFile.ReadValueBool("MODO DE TRABAJO", "ETIQUETA_DE_PRODUCTO_CON_UNIDADES_EN_PESAJES_EN_PRODUCCION", true);
            m_imprimirLineasTextoSuperiorInferiorDeEtiqueta = iniFile.ReadValueBool("MODO DE TRABAJO", "IMPRIME LINEA DE TEXTO SUPERIOR E INFERIOR EN LA ETIQUETA", false);

            m_toleranciaPesoPredefPesoBalanza_IngresoAPlanta = iniFile.ReadValueSingle("PESAJE INGRESO A PLANTA", "TOLERANCIA_PESOPREDEF_PESOBALANZA_INGPLANTA", 100.0f);
            m_pesoUnidadesRemitidoObligatorio = iniFile.ReadValueBool("PESAJE INGRESO A PLANTA", "PESOREMITIDO_OBLIGATORIO", true);
            m_mantenerUltimaTropaEntrePesajes = iniFile.ReadValueBool("PESAJE INGRESO A PLANTA", "MANTENER_TROPA_ENTRE_PESAJES", false);
            m_mantenerUltimaTipificacionEntrePesajes = iniFile.ReadValueBool("PESAJE INGRESO A PLANTA", "MANTENER_TIPIFICACION_ENTRE_PESAJES", false);

            m_permitirColectarMasCantidadesQueLasPedidasEnDespachos = iniFile.ReadValueBool("CONFIGURACION EGRESOS", "PERMITE_COLECTAR_MAS_PIEZAS_QUE_LAS_PEDIDAS", false);

            return true;
		}

        public static bool Exportar()
		{
			CIniRw iniFile = new CIniRw("Setup.ini");

            iniFile.WriteValueString("BASE DE DATOS", "SERVIDOR", m_servidorDB);
            iniFile.WriteValueString("BASE DE DATOS", "BASE_DE_DATOS", m_nombreDB);
            iniFile.WriteValueString("BASE DE DATOS", "USUARIO", m_userDB);
            iniFile.WriteValueString("BASE DE DATOS", "PASSWORD", m_passwordDB);
            iniFile.WriteValueBool("BASE DE DATOS", "TIPO DE SEGURIDAD DE ACCESO A BASE DE DATOS SSPI", m_tipoSeguridadConexionDB_SSPI);

            int idxScale = 1;
            foreach (ScaleCnfg sc in m_scalesCnfg)
            {
                iniFile.WriteValueString("CONEXION BALANZA", "NOMBRE_BALANZA" + idxScale,sc.Name);
                iniFile.WriteValueString("CONEXION BALANZA", "PUERTO_COM_BALANZA" + idxScale, sc.ComName);
                iniFile.WriteValueInt("CONEXION BALANZA", "PROTOCOLO_BALANZA" + idxScale, (int)sc.Protocolo);
                iniFile.WriteValueInt("CONEXION BALANZA", "CANTIDAD_DECIMALES" + idxScale, sc.Decimales);
                iniFile.WriteValueInt("CONEXION BALANZA", "BAUDIOS_COM_BALANZA" + idxScale, sc.Baudios);
                iniFile.WriteValueInt("CONEXION BALANZA", "BITS_DATOS_COM_BALANZA" + idxScale, sc.DataBits);
                iniFile.WriteValueInt("CONEXION BALANZA", "PARIDAD_COM_BALANZA" + idxScale, sc.Parity);
                iniFile.WriteValueInt("CONEXION BALANZA", "BITS_STOP_COM_BALANZA" + idxScale, sc.StopBits);
                iniFile.WriteValueInt("CONEXION BALANZA", "HANDSHAKE_COM_BALANZA" + idxScale, sc.HandShake);
                iniFile.WriteValueBool("CONEXION BALANZA", "HABILITADA_BALANZA" + idxScale, sc.Enable);
                iniFile.WriteValueString("CONFIGURACION PESAJE", "RANGO_CERO_BALANZA" + idxScale, sc.MaximoRangoZero.ToString());
                iniFile.WriteValueString("CONFIGURACION PESAJE", "RANGO_PESO_VALIDO_INFERIOR_BALANZA" + idxScale, sc.RangoPesoValidoInferior.ToString());
                iniFile.WriteValueString("CONFIGURACION PESAJE", "RANGO_PESO_VALIDO_SUPERIOR_BALANZA" + idxScale,sc.RangoPesoValidoSuperior.ToString());
                iniFile.WriteValueString("CONFIGURACION PESAJE", "MAXIMA_DISPERCION_ESTABLE_BALANZA" + idxScale,sc.MaximaDispercionEstable.ToString());
                iniFile.WriteValueInt("CONFIGURACION PESAJE", "TIEMPO_MS_DETECCION_ESTABLE_BALANZA" + idxScale, sc.TiempoDeteccionEstable);

                idxScale++;
            }

            iniFile.WriteValueString("CONEXION IMPRESOR", "NOMBRE_IMPRESORA", m_nombreImpresora);

            iniFile.WriteValueString("CONEXION SCANNER", "MODELO_SCANNER", m_modeloScannerZebra);
            iniFile.WriteValueBool("SIMULACION SCANNER", "PERMITE SIMULAR LECTURA SCANNER", m_permiteSimularLecturaScanner);
            iniFile.WriteValueInt("CONEXION SCANNER", "HOST INTERFACE TIPO", (int)m_hostInterfaceScaneer);


            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "PATH_ARCHIVO_FORMATOS_ETIQUETAS", m_pathArchivoFormatosEtiquetas);
            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_SELLO", m_nombreFormatoEtiquetaProducto);
            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_CONT", m_nombreFormatoEtiquetaContenedor);
            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_CAJA", m_nombreFormatoEtiquetaCaja);
            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "NOMBRE_FORMATO_ETIQUETA_PEDIDO", m_nombreFormatoEtiquetaPedido);
            iniFile.WriteValueInt("PROPIEDADES ETIQUETA", "CANTIDAD_ETIQUETAS", m_cantidadEtiquetasPorPesada);

            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "LINEA DE TEXTO SUPERIOR DE ETIQUETA", m_lineaDeTextoSuperiorDeEtiqueta);
            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "LINEA DE TEXTO INFERIOR DE ETIQUETA", m_lineaDeTextoInferiorDeEtiqueta);
            iniFile.WriteValueString("PROPIEDADES ETIQUETA", "MAPA DE DE CARACTERES ACTIVO EN IMPRESORA", m_encodingNameOutputPrinter);


            iniFile.WriteValueInt("GENERAL", "ID_ESTACION",m_idEstacion);
            iniFile.WriteValueString("GENERAL", "PATH_DIRECTORIO_REPORTES", m_pathDirectorioReportes);

            iniFile.WriteValueString("EMPRESA", "RAZON_SOCIAL", m_razonSocialEmpresa);
            iniFile.WriteValueString("EMPRESA", "PATH_LOGO_EMPRESA", m_pathLogoEmpresa);

            iniFile.WriteValueSingle("PESAJE DE CAJA", "TOLERANCIA_PORCENTUAL_PESAJE_CAJA",m_toleranceWeightBox);

            iniFile.WriteValueBool("MODO DE TRABAJO", "ETIQUETA_DE_PRODUCTO_CON_PESO_EN_INGRESO_A_PLANTA", m_ingresoAPlanta_WeightLabelEnable);
            iniFile.WriteValueBool("MODO DE TRABAJO", "ETIQUETA_DE_PRODUCTO_CON_PESO_EN_PESAJES_EN_PRODUCCION", m_pesajeEnProduccion_WeightLabelEnable);
            iniFile.WriteValueBool("MODO DE TRABAJO", "ETIQUETA_DE_PRODUCTO_CON_UNIDADES_EN_PESAJES_EN_PRODUCCION", m_pesajeEnProduccion_UnitsLabelEnable);
            iniFile.WriteValueBool("MODO DE TRABAJO", "IMPRIME LINEA DE TEXTO SUPERIOR E INFERIOR EN LA ETIQUETA", m_imprimirLineasTextoSuperiorInferiorDeEtiqueta);

            iniFile.WriteValueSingle("PESAJE INGRESO A PLANTA", "TOLERANCIA_PESOPREDEF_PESOBALANZA_INGPLANTA", m_toleranciaPesoPredefPesoBalanza_IngresoAPlanta);
            iniFile.WriteValueBool("PESAJE INGRESO A PLANTA", "PESOREMITIDO_OBLIGATORIO", m_pesoUnidadesRemitidoObligatorio);
            iniFile.WriteValueBool("PESAJE INGRESO A PLANTA", "MANTENER_TROPA_ENTRE_PESAJES", m_mantenerUltimaTropaEntrePesajes);
            iniFile.WriteValueBool("PESAJE INGRESO A PLANTA", "MANTENER_TIPIFICACION_ENTRE_PESAJES", m_mantenerUltimaTipificacionEntrePesajes);


            iniFile.WriteValueBool("CONFIGURACION EGRESOS", "PERMITE_COLECTAR_MAS_PIEZAS_QUE_LAS_PEDIDAS", m_permitirColectarMasCantidadesQueLasPedidasEnDespachos);

            return true;
		}

	}
    public enum SCALES
    {
        SCALE1,
        SCALE2,
        SCALE3
    }

    public class ScaleCnfg
    {
        string  m_name ="Balanza1";
        string  m_comName="COM1";
        BALANZA_SERIAL_PORT_PROTOCOLO m_protocolo = BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO;
        int     m_decimales = 0;
        int     m_baudios=9600;
        int     m_dataBits=8;
        int     m_parity=0;
        int     m_stopBits=1;
        int     m_handShake=0;
        bool    m_enable=false;
        float   m_maximoRangoZero=0.02f;
        float   m_rangoPesoValidoInferior=5.0f;
        float   m_rangoPesoValidoSuperior=1000.0f;
        float   m_maximaDispercionEstable=0.02f;
        int     m_tiempoDeteccionEstable=1000;


        public ScaleCnfg()
        {
        }

        public ScaleCnfg(ScaleCnfg cpy)
        {
            m_name = cpy.m_name;
            m_comName = cpy.m_comName;
            m_baudios = cpy.m_baudios;
            m_dataBits = cpy.m_dataBits;
            m_parity = cpy.Parity;
            m_stopBits = cpy.m_stopBits;
            m_handShake = cpy.m_handShake;
            m_enable = cpy.m_enable;
            m_maximoRangoZero = cpy.m_maximoRangoZero;
            m_rangoPesoValidoInferior = cpy.RangoPesoValidoInferior;
            m_rangoPesoValidoSuperior = cpy.RangoPesoValidoSuperior;
            m_maximaDispercionEstable = cpy.m_maximaDispercionEstable;
            m_tiempoDeteccionEstable = cpy.m_tiempoDeteccionEstable;
            m_protocolo = cpy.Protocolo;
            m_decimales = cpy.m_decimales;
        }

        public string Name { get => m_name; set => m_name = value; }
        public string ComName { get => m_comName; set => m_comName = value; }
        public int Baudios { get => m_baudios; set => m_baudios = value; }
        public int DataBits { get => m_dataBits; set => m_dataBits = value; }
        public int Parity { get => m_parity; set => m_parity = value; }
        public int StopBits { get => m_stopBits; set => m_stopBits = value; }
        public int HandShake { get => m_handShake; set => m_handShake = value; }
        public bool Enable { get => m_enable; set => m_enable = value; }
        public float MaximoRangoZero { get => m_maximoRangoZero; set => m_maximoRangoZero = value; }
        public float RangoPesoValidoInferior { get => m_rangoPesoValidoInferior; set => m_rangoPesoValidoInferior = value; }
        public float RangoPesoValidoSuperior { get => m_rangoPesoValidoSuperior; set => m_rangoPesoValidoSuperior = value; }
        public float MaximaDispercionEstable { get => m_maximaDispercionEstable; set => m_maximaDispercionEstable = value; }
        public int TiempoDeteccionEstable { get => m_tiempoDeteccionEstable; set => m_tiempoDeteccionEstable = value; }
        public BALANZA_SERIAL_PORT_PROTOCOLO Protocolo { get => m_protocolo; set => m_protocolo = value; }
        public int Decimales { get => m_decimales; set => m_decimales = value; }
    }
}
