using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CoreScanner;

namespace ZebraScannerLib
{
    /// <summary>
    /// Libreria que utiliza la libreria COM CoreScanner provista por zebra
    /// para el control de su linea de Escanners.
    /// "Zebra_CoreScanner_Driver_(64bit)_v3.04.0007.zip"
    /// La Libreria ZebraScannerLib provee los eventos de lecturas de escanner 
    /// y los eventos de Attach y Desatach del dispositivo escanner USB.
    /// Se podra controlar el tipo de beep y led del escaner. Como tambien se podra
    /// especificar que codigos de barras se permiten su lectura .
    /// </summary>
    public class CZebraScannerLib:IDisposable
    {

        enum SUBSCRIBE_EVENT_TYPE
        {
            BARCODE = 1,
            IMAGE = 2,
            VIDEO = 4,
            RMD = 8,
            PNP = 16,
            OTHER = 32
        }

        enum EVENT_PNP_TYPE
        {
            ATTACHED = 0,
            DETACHED = 1
        }

        enum OPCODES
        {
            SUBSCRIBE_EVENTS=1001,
            BEEPLED=6000
        }
        enum STATUS
        {
            ERROR=-1,
            OK=0
        }

        string m_modelScanner;
        int m_numScaner;
        bool m_isOpen = false;
        CCoreScannerClass cCoreScannerClass;

        public string ModelScanner { get => m_modelScanner; set => m_modelScanner = value; }
        /// <summary>
        /// Indica con true que el driver se pudo abrir y que hay conexion con el escaner.
        /// </summary>
        public bool IsOpen { get => m_isOpen; set => m_isOpen = value; }
        public int NumScaner { get => m_numScaner; set => m_numScaner = value; }
        /// <summary>
        /// Lista de codigo de barras habilitados para ser leidos. Si la lista esta vacia se permiten todos.
        /// </summary>
        public List<CODBAR_TYPE> ListCodBarEnables = new List<CODBAR_TYPE>();

        /// <summary>Scanner Data Response event. This event is called when new data arrives from scanner</summary>
        public delegate void NewDataScanner(string canData);
        public event NewDataScanner OnNewDataScanner;
        /// <summary>Scanner Attached Response event. This event is called when the scanner Attached</summary>
        public delegate void AttachedScanner();
        public event AttachedScanner OnAttachedScanner;
        /// <summary>Scanner Detached Response event. This event is called when the scanner Detached</summary>
        public delegate void DetachedScanner();
        public event DetachedScanner OnDetachedScanner;
        /// <summary>Scanner Data Exception Response event. This event is called when the data is not valid</summary>
        public delegate void ScannerDataException(string infoException);
        public event ScannerDataException OnScannerDataException;

        public CZebraScannerLib(string modelScanner)
        {
            //Instantiate CoreScanner Class
            ModelScanner = modelScanner;
            cCoreScannerClass = new CCoreScannerClass();
            cCoreScannerClass.BarcodeEvent += CCoreScannerClass_BarcodeEvent;
            cCoreScannerClass.PNPEvent += CCoreScannerClass_PNPEvent;
            Open();
        }

        ~CZebraScannerLib()
        {
            Dispose();
        }
        public void Dispose()
        {
            Close();
        }

        private void CCoreScannerClass_PNPEvent(short eventType, ref string ppnpData)
        {
            if (eventType == (short)EVENT_PNP_TYPE.ATTACHED)
            {
                OnAttachedScanner?.Invoke();
            }
            else
            {
                OnDetachedScanner?.Invoke();
            }
        }

        private void CCoreScannerClass_BarcodeEvent(short eventType, ref string pscanData)
        {

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pscanData);
            XmlNodeList xnList = xml.SelectNodes("/outArgs/arg-xml/scandata");
            string typeCodBar;
            string valueCodBar="";
            foreach (XmlNode node in xnList)
            {
                typeCodBar = node["datatype"].InnerText;
                if (ListCodBarEnables.Count==0 || ListCodBarEnables.Contains((CODBAR_TYPE)Convert.ToInt32(typeCodBar)))
                {
                    valueCodBar = node["datalabel"].InnerText ?? "0";
                    valueCodBar= ConvertHexString_toString(valueCodBar);
                    OnNewDataScanner?.Invoke(valueCodBar);
                }
                else
                {
                    OnScannerDataException?.Invoke("Tipo de Codio de Barras no Permitido");
                }
            }
        }

        public bool Open()
        {
            bool subscribeEventsOK = false;
            short[] scannerTypes = new short[1]; // Scanner Types you are interested in
            scannerTypes[0] = 1; // 1 for all scanner types
            short numberOfScannerTypes = 1; // Size of the scannerTypes array
            int status= (int)STATUS.ERROR; // Extended API return code
           
            //Call Open API
            cCoreScannerClass.Open(0, scannerTypes, numberOfScannerTypes, out status);
            if ((status == (int)STATUS.OK))
            {
                NumScaner = GetNumberScanerConnect();
                IsOpen = (NumScaner != 0);
                //subscribir aqui los eventos que el escanner debe generar.
                string outXML; // XML Output
                string inXML = String.Format(
                    "<inArgs>" +
                    "<cmdArgs>" +
                    "<arg-int>{0}</arg-int>" + // Number of events you want to subscribe
                    "<arg-int>{1},{2}</arg-int>" + // Comma separated event IDs
                    "</cmdArgs>" +
                    "</inArgs>", 2, (int)SUBSCRIBE_EVENT_TYPE.BARCODE, (int)SUBSCRIBE_EVENT_TYPE.PNP);
                cCoreScannerClass.ExecCommand((int)OPCODES.SUBSCRIBE_EVENTS, ref inXML, out outXML, out status);
                subscribeEventsOK = (status == (int)STATUS.OK);
            }            
            return IsOpen;
        }

        public void Close()
        {
            int appHandle = 0;
            int status = (int)STATUS.ERROR;
            try
            {
                cCoreScannerClass.Close(appHandle, out status);
                IsOpen = (status == (int)STATUS.OK)? false:true;
            }
            catch(Exception exp)
            {
            }
        }

        private int GetNumberScanerConnect()
        {
            int numberScanner = 0;
            int status; // Extended API return code
            // Lets list down all the scanners connected to the host
            short numberOfScanners; // Number of scanners expect to be used
            int[] connectedScannerIDList = new int[255];
            // List of scanner IDs to be returned
            string outXML; //Scanner details output
            cCoreScannerClass.GetScanners(out numberOfScanners, connectedScannerIDList,
            out outXML, out status);
            if (status == (int)STATUS.OK)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(outXML);
                XmlNodeList xnList = xml.SelectNodes("/scanners/scanner");
                String valueElemet;
                foreach (XmlNode node in xnList)
                {
                    valueElemet = node["modelnumber"]?.InnerText?.ToLower() ?? "";
                    if (valueElemet.Contains(ModelScanner.ToLower()))
                    {
                        valueElemet = node["scannerID"].InnerText ?? "0";
                        Int32.TryParse(valueElemet, out numberScanner);
                    }
                }

            }
            return numberScanner;
        }

        public bool CheckScanerPresent()
        {
            int status = -1; // Extended API return code
            // Lets list down all the scanners connected to the host
            short numberOfScanners; // Number of scanners expect to be used
            int[] connectedScannerIDList = new int[255];
            // List of scanner IDs to be returned
            string outXML; //Scanner details output
            cCoreScannerClass.GetScanners(out numberOfScanners, connectedScannerIDList,
            out outXML, out status);
            if (status == (int)STATUS.OK)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(outXML);
                XmlNodeList xnList = xml.SelectNodes("/scanners/scanner");
                status = (xnList.Count > 0) ? (int)STATUS.OK : (int)STATUS.ERROR;
            }
            return (status == (int)STATUS.OK);
        }


        public bool Beep(BEEPLED_TYPE beepLedType)
        {
            int status = (int)STATUS.ERROR;
            string outXML; // Output
            string inXML =String.Format( 
            "<inArgs>" +
            "<scannerID>{0}</scannerID>" + // The scanner you need to beep
            "<cmdArgs>" +
            "<arg-int>{1}</arg-int>" + // 4 high short beep pattern
            "</cmdArgs>" +
            "</inArgs>",NumScaner,(int)beepLedType);
            cCoreScannerClass.ExecCommand((int)OPCODES.BEEPLED, ref inXML, out outXML, out status);
            return (status == (int)STATUS.OK);
        }
        /// <summary>
        /// Convierte un string con caracteres en hexadecimal a un string estandar.
        /// Formato de entrada : "0x30 0x41 0x35"
        /// Formato de Salida:   "0A5".
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public string ConvertHexString_toString(string hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 5)
                {
                    string hs = string.Empty;

                    hs = hexString.Substring(i+2, 2);
                    ulong decval = Convert.ToUInt64(hs, 16);
                    long deccc = Convert.ToInt64(hs, 16);
                    char character = Convert.ToChar(deccc);
                    ascii += character;
                }
                return ascii;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }

    }
    /// <summary>
    /// Tipos de BEEP y LEDS
    /// </summary>
    public enum BEEPLED_TYPE
    {
        _1_HIGH_SHORT_BEEP,
        _2_HIGH_SHORT_BEEPS,
        _3_HIGH_SHORT_BEEPS,
        _4_HIGH_SHORT_BEEPS,
        _5_HIGH_SHORT_BEEPS,
        _1_LOW_SHORT_BEEP,
        _2_LOW_SHORT_BEEPS,
        _3_LOW_SHORT_BEEPS,
        _4_LOW_SHORT_BEEPS,
        _5_LOW_SHORT_BEEPS,
        _1_HIGH_LONG_BEEP,
        _2_HIGH_LONG_BEEPS,
        _3_HIGH_LONG_BEEPS,
        _4_HIGH_LONG_BEEPS,
        _5_HIGH_LONG_BEEPS,
        _1_LOW_LONG_BEEP,
        _2_LOW_LONG_BEEPS,
        _3_LOW_LONG_BEEPS,
        _4_LOW_LONG_BEEPS,
        _5_LOW_LONG_BEEPS,
        FAST_WARBLE_BEEP,
        SLOW_WARBLE_BEEP,
        HIGH_LOW_BEEP,
        LOW_HIGH_BEEP,
        HIGH_LOW_HIGH_BEEP,
        LOW_HIGH_LOW_BEEP,
        HIGH_HIGH_LOW_LOW_BEEP,
        GREEN_LED_OFF = 42,
        GREEN_LED_ON,
        YELLOW_LED_ON = 45,
        YELLOW_LED_OFF,
        RED_LED_ON,
        RED_LED_OFF
    }
    /// <summary>
    /// Tipos de Codigos de Barras.
    /// </summary>
    public enum CODBAR_TYPE
    {
        COD39 = 1,
        CODBAR = 2,
        COD128 = 3,
        INTERLEAVED2OF5 = 6,
        CODE93 = 7,
        UPC_A = 8,
        EAN_8 = 10,
        EAN13 = 11,
        EAN128 = 15,
        QR = 28
    }//hay mas codigos para colocar ver manual SDK SCANNER ZEBRA

}
