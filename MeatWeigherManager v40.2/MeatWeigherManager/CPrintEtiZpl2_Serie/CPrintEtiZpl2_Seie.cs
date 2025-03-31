using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.IO.Ports;


namespace PrintEtiZpl2_Serie
{
    public class CPrintEtiZpl2_Serie: SerialPort
    {
        string m_pathFileFormat;
        string m_ultimoError;

        byte[] m_responsePrinter = new byte[500];

        const int LONG_BYTES_RESPONSE_STATUS_PRINTER = 144;
        const int INDEX_POS_RESPONSE_STATUS_PRINTER = 88;
        
        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public delegate void StatusPrinter(STATUS_PRINTER status);
        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public event StatusPrinter OnStatusPrinter;

        public enum STATUS_PRINTER
        {
            LOST_CONEXION,
            CONNECTED
        }

        public enum RETURN_PRINTER_ERROR
        {
            NONE=0,
            ERROR,
            NON_RESPONSE,
            INVALIDE_TRAME_RESPONSE,
            WAIT_IN_PROCESS
        }

        public string PathArchivoFormatoEtiquetas
        {
            get { return m_pathFileFormat; }
            set { m_pathFileFormat = value;}
        }

        public string UltimoError
        {
            get{ return m_ultimoError;}
        }

        public CPrintEtiZpl2_Serie(string pathFileFormat, string _portName, int _baudRate = 9600, short _dataBits = 8, StopBits _stopBits = StopBits.One, Handshake _handShaque = Handshake.None, Parity _parity = Parity.None)
        {
                m_ultimoError = "";
                m_pathFileFormat = pathFileFormat;
                PortName = _portName;
                BaudRate = _baudRate;
                DataBits = _dataBits;
                StopBits = _stopBits;
                Handshake = _handShaque;
                Parity = _parity;
                ReadTimeout = 2000;
        }

        /********************************************************************************************
         * Function:    Connect
         * Description: Conecta (Open) el puerto com configurado en la clase Serial Port
         * ******************************************************************************************/
        public bool Connect()
        {
            bool isopen = false;
            try
            {
                if (!base.IsOpen)
                {
                    base.Open();
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Abriendo el Puerto COM del Impresor: " + ex.Message));
            }
            return isopen;
        }

        /********************************************************************************************
         * Function:    ReConnect
         * Description: Reconecta o Reabre un puerto Com especificando todas las propiedades del puerto
         *              a abrir 
         * ******************************************************************************************/
        public bool ReConnect(string pathFileFormat, string _portName, int _baudRate, short _dataBits = 8, StopBits _stopBits = StopBits.One, Handshake _handShaque = Handshake.None, Parity _parity = Parity.None)
        {
            bool isreconnect = false;
            try
            {
                Disconnect();
                m_pathFileFormat = pathFileFormat;
                PortName = _portName;
                BaudRate = _baudRate;
                DataBits = _dataBits;
                StopBits = _stopBits;
                Handshake = _handShaque;
                Parity = _parity;
                isreconnect = Connect();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Abriendo el Puerto COM del Impresor: " + ex.Message));
            }
            return isreconnect;
        }

        /********************************************************************************************
         * Function:    Disconnect
         * Description: Desconecta o Cierra el puerto com activo
         *              a abrir 
         * ******************************************************************************************/
        public void Disconnect()
        {
            try
            {
                Thread CloseDown = new Thread(new ThreadStart(CloseSerialOnClose)); //close port in new thread 
                CloseDown.Start();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Cerrando el Puerto COM del Impresor: " + ex.Message));
            }
        }

        private void CloseSerialOnClose()
        {
            try
            {
                if (base.IsOpen)
                    base.Close();
            }
            catch (Exception ex)
            {
            }
        }

        
        public bool IsOpen()
        {
            return base.IsOpen;
        }

        /***********************************************************************************
         * Funcion:     WritePrinter 
         * Descripcion: Escrive datos en el impresor
         ***********************************************************************************/
        public bool WritePrinter(char[] cadenaChars)
        {
            bool writeok = false;
            try
            {
                base.Write(Encoding.GetEncoding("ibm850").GetBytes(cadenaChars),0,cadenaChars.Length);
                writeok = true;
            }
            catch (Exception e)
            {
                m_ultimoError = e.Message;
            }
            return writeok;
        }

        public bool Print(string nameFormat, CListVariables listaVariables, short cantEtiquetasGenerar)
        {
            bool printOK = true;
            string dataSendLabel;
            dataSendLabel = String.Format("^XA\r\n^XFE:{0}\r\n", nameFormat);
            dataSendLabel += listaVariables.Get();
            dataSendLabel += String.Format("^PQ{0}^XZ\r\n", cantEtiquetasGenerar);
            printOK = WritePrinter(dataSendLabel.ToCharArray());
            return printOK;
        }

        public bool SendFormatToPrinter()
        {
            bool formatoEnviadoOk = false;
            m_ultimoError = "";
            try
            {
                if (File.Exists(m_pathFileFormat))
                {
                    //StreamReader sr = File.OpenText(m_pathFileFormat);
                    StreamReader sr = new StreamReader(m_pathFileFormat, System.Text.Encoding.UTF8, true);
                    char[] formatChars = sr.ReadToEnd().ToCharArray();
                    if (formatChars.GetLength(0) != 0)
                    {
                        formatoEnviadoOk = WritePrinter(formatChars);
                        if (!formatoEnviadoOk)
                        {
                            if (OnStatusPrinter != null) OnStatusPrinter(STATUS_PRINTER.LOST_CONEXION);
                            m_ultimoError = "Error enviado el archivo de formato de etiqueta a la impresora";
                        }
                    }
                    else
                    {
                        m_ultimoError = String.Format("El Archivo {0} esta vacio", m_pathFileFormat);
                    }
                    sr.Close();
                }
                else
                {
                    m_ultimoError = String.Format("Archivo de Formato de etiqueta {0} no encontrado", m_pathFileFormat);
                }
            }
            catch (Exception xcp)
            {
                m_ultimoError = xcp.Message;
            }
            return formatoEnviadoOk;
        }

        public bool IsOnlinePrinter()
        {
            bool isonline = false;

            if (GetRequestStatusError() == RETURN_PRINTER_ERROR.NONE)
            {
                isonline = true;
            }
            else
            {
                m_ultimoError = "El Impresor no esta ON LINE o en estado de ERROR";
            }
            return isonline;
        }

        private bool SendCommandPrinter_GetStatus()
        {
            return WritePrinter(new char[] { '~', 'H', 'Q', 'E', 'S', '\r', '\n' });
        }

        private RETURN_PRINTER_ERROR GetRequestStatusError()
        {
            RETURN_PRINTER_ERROR codeStatusError = RETURN_PRINTER_ERROR.NON_RESPONSE;

            SendCommandPrinter_GetStatus();
            try
            {
                int bytesEnBuffer;
                int bytesLeidos = 0;
                do
                {
                    bytesEnBuffer = Read(m_responsePrinter, bytesLeidos, LONG_BYTES_RESPONSE_STATUS_PRINTER-bytesLeidos);
                    bytesLeidos += bytesEnBuffer;
                }while(bytesLeidos > 0 && bytesLeidos < LONG_BYTES_RESPONSE_STATUS_PRINTER);

                if (bytesLeidos == LONG_BYTES_RESPONSE_STATUS_PRINTER && m_responsePrinter[0] == 0x02 && m_responsePrinter[LONG_BYTES_RESPONSE_STATUS_PRINTER-1] == 0x03)
                {
                    if (m_responsePrinter[INDEX_POS_RESPONSE_STATUS_PRINTER] - 0x30 == 0)
                        codeStatusError = RETURN_PRINTER_ERROR.NONE;
                    else
                        codeStatusError = RETURN_PRINTER_ERROR.ERROR;
                }
                else
                {
                    codeStatusError = RETURN_PRINTER_ERROR.INVALIDE_TRAME_RESPONSE;
                }
            }catch(TimeoutException toex)
            {
                codeStatusError = RETURN_PRINTER_ERROR.NON_RESPONSE;
            }
            return codeStatusError;
        }

        public void Dispose()
        {
            //el cierre seguro de un SerialPort se debe hacer en un Thread Distinto
            //si no se hace esto se queda colgado y detiene a la aplicacion.
            Thread CloseDown = new Thread(new ThreadStart(CloseSerialOnDestroid)); //close port in new thread 
            CloseDown.Start();
        }

        private void CloseSerialOnDestroid()
        {
            try
            {
                base.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class CListVariables
    {
        int idxVariable = 0;
        string m_listVariables;
        
        public CListVariables()
        {
        }
        public void Add(String strVariable)
        {
            m_listVariables += String.Format("^FN{0}^FD{1}^FS\r\n",++idxVariable,strVariable);
        }
        public string Get()
        {
            return m_listVariables;
        }
    }
}
