using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace BalanzaSerialPort
{
    public class CBalanzaSerialPort : SerialPort
    {

        private const int LARGO_TRAMA_PROTOCOLO_TBE_CONTINUO = 18;
        private const int LARGO_TRAMA_PROTOCOLO_IT_EL05A = 8;
        private const int LARGO_TRAMA_PROTOCOLO_IT_EL05B = 14;
        private const int LARGO_TRAMA_PROTOCOLO_PANTHER = 17;
        private const int LARGO_TRAMA_PROTOCOLO_GSE = 14;

        //timeout de recepcion
        private const int TIME_OUT_NON_RECEPTION = 3000;
        //deteccion y proceso de trama
        int largoTramaModoContinuo = LARGO_TRAMA_PROTOCOLO_TBE_CONTINUO;

        private List<byte> tramaModoContinuo = new List<byte>();

        private System.Timers.Timer _timerEventNonReception;
        private CDatScale m_DatScale;
        private CCnfgScale m_CnfgScale;
        BALANZA_SERIAL_PORT_PROTOCOLO m_activeProtocolo = BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO;
        int m_cantidadDecimalesDisplay = 0;

        public CCnfgScale CnfgScale { get => m_CnfgScale; set => m_CnfgScale = value; }
        public CDatScale DatScale { get => m_DatScale; set => m_DatScale = value; }
        public BALANZA_SERIAL_PORT_PROTOCOLO ActiveProtocolo { get => m_activeProtocolo; set => m_activeProtocolo = value; }
        public int CantidadDecimalesDisplay { get => m_cantidadDecimalesDisplay; set => m_cantidadDecimalesDisplay = value; }
        public int LargoTramaModoContinuo { get => largoTramaModoContinuo; set => largoTramaModoContinuo = value; }


        // ------------------------------------------------------------------------
        /// <summary>Response data event. This event is called when new data arrives</summary>
        public delegate void NewWeight(CDatScale datScale);
        /// <summary>Response data event. This event is called when new data arrives</summary>
        public event NewWeight OnNewWeight;

        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public delegate void ExceptionData(EXCEPTION_CBALANZASERIALPORT exception);
        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public event ExceptionData OnException;


        public CBalanzaSerialPort(CCnfgScale cnfgScale)
        {
            DatScale = new CDatScale()
            {
                PROTOCOLO = cnfgScale.Protocol,
                CantidadDecimales = cnfgScale.CountDecimals
            };
            
            CnfgScale = cnfgScale;

            PortName = cnfgScale.PortName;
            BaudRate = cnfgScale.BaudRate;
            DataBits = cnfgScale.DataBits;
            StopBits = cnfgScale.StopBits;
            Handshake = cnfgScale.HandShaque;
            Parity = cnfgScale.Parity;
            ActiveProtocolo = cnfgScale.Protocol;
            CantidadDecimalesDisplay = cnfgScale.CountDecimals;
            LargoTramaModoContinuo = ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO ?
                LARGO_TRAMA_PROTOCOLO_TBE_CONTINUO : ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A ?
                LARGO_TRAMA_PROTOCOLO_IT_EL05A : ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05B ?
                LARGO_TRAMA_PROTOCOLO_IT_EL05B : ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.PANTHER ? LARGO_TRAMA_PROTOCOLO_PANTHER : LARGO_TRAMA_PROTOCOLO_GSE;

            ReadTimeout = TIME_OUT_NON_RECEPTION;
            DataReceived += new SerialDataReceivedEventHandler(m_serialPort_DataReceived);
            InicializeEventTimerNonRecepcion();
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
                    ActiveEventTimerNonRecepcion(true);
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Abriendo el Puerto COM de la Balanza: " + ex.Message));
            }
            return isopen;
        }

        /********************************************************************************************
         * Function:    ReConnect
         * Description: Reconecta o Reabre un puerto Com especificando todas las propiedades del puerto
         *              a abrir 
         * ******************************************************************************************/
        public bool ReConnect()
        {
            bool isreconnect = false;
            try
            {
                Disconnect();
                Thread.Sleep(500);
                PortName = CnfgScale.PortName;
                BaudRate = CnfgScale.BaudRate;
                DataBits = CnfgScale.DataBits;
                StopBits = CnfgScale.StopBits;
                Handshake = CnfgScale.HandShaque;
                Parity = CnfgScale.Parity;
                ReadTimeout = TIME_OUT_NON_RECEPTION;
                ActiveProtocolo = CnfgScale.Protocol;
                CantidadDecimalesDisplay = CnfgScale.CountDecimals;
                LargoTramaModoContinuo = ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO ?
                    LARGO_TRAMA_PROTOCOLO_TBE_CONTINUO : ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A ?
                    LARGO_TRAMA_PROTOCOLO_IT_EL05A : ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05B ?
                    LARGO_TRAMA_PROTOCOLO_IT_EL05B : ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.PANTHER ? LARGO_TRAMA_PROTOCOLO_PANTHER : LARGO_TRAMA_PROTOCOLO_GSE;

                isreconnect = Connect();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Abriendo el Puerto COM de la Balanza: " + ex.Message));
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
                CloseDown.Join(1000);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error Cerrando el Puerto COM de la Balanza: " + ex.Message));
            }

        }

        private void CloseSerialOnClose()
        {
            try
            {
                if (base.IsOpen)
                {
                    base.Close();
                    while (base.IsOpen) ;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public new bool IsOpen()
        {
            return base.IsOpen;
        }


        internal void CallException(EXCEPTION_CBALANZASERIALPORT exception)
        {
            if (OnException != null) OnException(exception);
        }

        private void InicializeEventTimerNonRecepcion()
        {
            // Create a timer with a ten second interval.
            _timerEventNonReception = new System.Timers.Timer(TIME_OUT_NON_RECEPTION);

            // Hook up the Elapsed event for the timer.
            _timerEventNonReception.Elapsed += new System.Timers.ElapsedEventHandler(_timerEventNonReception_Elapsed);

            // Set the Interval to 2 seconds (2000 milliseconds).
            _timerEventNonReception.Interval = TIME_OUT_NON_RECEPTION;
            _timerEventNonReception.Enabled = false;
        }

        private void ActiveEventTimerNonRecepcion(bool active)
        {
            _timerEventNonReception.Interval = TIME_OUT_NON_RECEPTION;
            _timerEventNonReception.Enabled = active;
        }

        void _timerEventNonReception_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerEventNonReception.Stop();
            CallException(EXCEPTION_CBALANZASERIALPORT.NO_RECEPTION);
        }

        public new void Dispose()
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
                DataReceived -= m_serialPort_DataReceived;
                base.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        /********************************************************************************************
         * Function:    m_serialPort_DataReceived
         * Description: Evento de Dato Recivido por el puerto com
         * ******************************************************************************************/
        void m_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                ActiveEventTimerNonRecepcion(false);

                SerialPort sp = (SerialPort)sender;
                // Read data from the remote device.
                int bytesRead = sp.BytesToRead;

                if (bytesRead > 0)
                {// hay mas datos para leer
                    //leo todo lo que hay en el buffer de recepcion
                    byte[] bufferDatosRecibidos = new byte[bytesRead];
                    sp.Read(bufferDatosRecibidos, 0, bytesRead);

                    //creo el array que tendra la trama detectada y validada.
                    byte[] arrayTramaValida = new byte[largoTramaModoContinuo];
                    //sumo al final del buffer de confeccion de trama los bytes recividos
                    tramaModoContinuo.AddRange(bufferDatosRecibidos);

                    if (ExtraerTrama(arrayTramaValida))
                    {
                        m_DatScale.Update(arrayTramaValida, m_CnfgScale);

                        if (OnNewWeight != null)
                        {
                            OnNewWeight(new CDatScale(m_DatScale));
                        }
                    }
                    else
                    {
                        if (bytesRead >= largoTramaModoContinuo)
                            CallException(EXCEPTION_CBALANZASERIALPORT.PROTOCOL_ERROR);
                    }

                    ActiveEventTimerNonRecepcion(true);
                }
                else
                {
                    CallException(EXCEPTION_CBALANZASERIALPORT.PORT_ERROR);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /****************************************************************************
         *  Funcion:        ExtraerTrama() 
         *  Descripcion:    busca en el buffer global tramaModoContinuo una trama valida
         *                  de modo continuo y la guarda en el buffer pasado como parametro.
         *                  Si la trama esta iniciada y no completa, deja el parcial de trama
         *                  en el buffer global y en la siguiente recepcion lo completa.
         ***************************************************************************/
        private bool ExtraerTrama(byte[] arrayTramaExtraida)
        {
            int idxStx = -1;
            bool extraidaOk = false;
            if (ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO)
            {
                for (int i = 0; i < tramaModoContinuo.Count; i++)
                {
                    if (tramaModoContinuo[i] == 0x02)
                    {
                        idxStx = i;
                        break;
                    }
                }
                if (idxStx != -1 && ((tramaModoContinuo.Count - idxStx) >= largoTramaModoContinuo))
                {
                    //copia la trama extraida
                    tramaModoContinuo.CopyTo(idxStx, arrayTramaExtraida, 0,largoTramaModoContinuo);

                    //verifico que la trama sea valida con un CR y LF al final.
                    if (arrayTramaExtraida[16] == 0x0d && arrayTramaExtraida[17] == 0x0a)
                        extraidaOk = true;
                    //eliminar toda la trama extraida y los bytes de adelante del buffer de trama.
                    if (tramaModoContinuo.Count > largoTramaModoContinuo)
                        tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
                }
                //por las dudas para que no se infle demaciado la lista cuando los datos recividos no poseen 
                //un protocolo valido. dejo solo los ultimos 18 bytes.
                if (tramaModoContinuo.Count > (largoTramaModoContinuo * 3))
                    tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
            }
            else if (ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A)
            {
                for (int i = 0; i < tramaModoContinuo.Count; i++)
                {
                    if (tramaModoContinuo[i] >= 0x40 && tramaModoContinuo[i] <= 0x4f)
                    {
                        idxStx = i;
                        break;
                    }
                }
                if (idxStx != -1 && ((tramaModoContinuo.Count - idxStx) >= largoTramaModoContinuo))
                {
                    //copia la trama extraida
                    tramaModoContinuo.CopyTo(idxStx, arrayTramaExtraida, 0, largoTramaModoContinuo);

                    //verifico que la trama sea valida con un CR y LF al final.
                    if (arrayTramaExtraida[7] == 0x0d)
                        extraidaOk = true;
                    //eliminar toda la trama extraida y los bytes de adelante del buffer de trama.
                    if (tramaModoContinuo.Count > largoTramaModoContinuo)
                        tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
                }
                //por las dudas para que no se infle demaciado la lista cuando los datos recividos no poseen 
                //un protocolo valido. dejo solo los ultimos (largo de trama) bytes.
                if (tramaModoContinuo.Count > (largoTramaModoContinuo * 3))
                    tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
            }
            else if (ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05B)
            {
                for (int i = 0; i < tramaModoContinuo.Count; i++)
                {
                    if (tramaModoContinuo[i] == 0x02)
                    {
                        idxStx = i;
                        break;
                    }
                }
                if (idxStx != -1 && ((tramaModoContinuo.Count - idxStx) >= largoTramaModoContinuo))
                {
                    //copia la trama extraida
                    tramaModoContinuo.CopyTo(idxStx, arrayTramaExtraida, 0, largoTramaModoContinuo);

                    //verifico que la trama sea valida con un CR y LF al final.
                    if (arrayTramaExtraida[12] == 0x0d && arrayTramaExtraida[13] == 0x0a && arrayTramaExtraida[11] != 'O')
                        extraidaOk = true;
                    //eliminar toda la trama extraida y los bytes de adelante del buffer de trama.
                    if (tramaModoContinuo.Count > largoTramaModoContinuo)
                        tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
                }
                //por las dudas para que no se infle demaciado la lista cuando los datos recividos no poseen 
                //un protocolo valido. dejo solo los ultimos 18 bytes.
                if (tramaModoContinuo.Count > (largoTramaModoContinuo * 3))
                    tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
            }
            else if (ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.PANTHER)
            {
                for (int i = 0; i < tramaModoContinuo.Count; i++)
                {
                    if (tramaModoContinuo[i] == 0x02)
                    {
                        idxStx = i;
                        break;
                    }
                }
                if (idxStx != -1 && ((tramaModoContinuo.Count - idxStx) >= largoTramaModoContinuo))
                {
                    //copia la trama extraida
                    tramaModoContinuo.CopyTo(idxStx, arrayTramaExtraida, 0, largoTramaModoContinuo);

                    //verifico que la trama sea valida con un CR al final.
                    if (arrayTramaExtraida[16] == 0x0d)
                        extraidaOk = true;
                    //eliminar toda la trama extraida y los bytes de adelante del buffer de trama.
                    if (tramaModoContinuo.Count > largoTramaModoContinuo)
                        tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
                }
                //por las dudas para que no se infle demaciado la lista cuando los datos recividos no poseen 
                //un protocolo valido. dejo solo los ultimos largo de trama bytes.
                if (tramaModoContinuo.Count > (largoTramaModoContinuo * 3))
                    tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
            }
            else if (ActiveProtocolo == BALANZA_SERIAL_PORT_PROTOCOLO.GSE)
            {
                for (int i = 0; i < tramaModoContinuo.Count; i++)
                {
                    if (tramaModoContinuo[i] == 0x02)
                    {
                        idxStx = i;
                        break;
                    }
                }
                if (idxStx != -1 && ((tramaModoContinuo.Count - idxStx) >= largoTramaModoContinuo))
                {
                    //copia la trama extraida
                    tramaModoContinuo.CopyTo(idxStx, arrayTramaExtraida, 0, largoTramaModoContinuo);

                    //verifico que la trama sea valida con un CR y LF al final.
                    if (arrayTramaExtraida[12] == 0x0d && arrayTramaExtraida[13] == 0x0a)
                        extraidaOk = true;
                    //eliminar toda la trama extraida y los bytes de adelante del buffer de trama.
                    if (tramaModoContinuo.Count > largoTramaModoContinuo)
                        tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
                }
                //por las dudas para que no se infle demaciado la lista cuando los datos recividos no poseen 
                //un protocolo valido. dejo solo los ultimos 18 bytes.
                if (tramaModoContinuo.Count > (largoTramaModoContinuo * 3))
                    tramaModoContinuo.RemoveRange(0, tramaModoContinuo.Count - largoTramaModoContinuo);
            }

            return extraidaOk;
        }
        public void ResetPasoPorCero()
        {
            m_DatScale.IsPasoPorCero = false;
        }

    }

    public enum EXCEPTION_CBALANZASERIALPORT
    {
        PORT_ERROR,
        NO_RECEPTION,
        PROTOCOL_ERROR
    };

    public enum BALANZA_SERIAL_PORT_PROTOCOLO
    {
        TBE_CONTINUO,
        IT_EL05A,
        IT_EL05B,
        PANTHER,
        GSE
    };

    #region Clase CCnfgBalanza
    /// <summary>
    /// CCnfgScale
    /// Clase que contiene los datos necesarios de configuracion para poder establecer
    /// una conexion serie con la balanza y sus parametros de pesaje necesarios.
    /// </summary>
    public class CCnfgScale
    {
        private float m_pesoMaximoCero = 1.0f;
        private float m_pesoMinimoValido = 5.0f;
        private float m_pesoMaximoValido = 1000.0f;
        private float m_maximaDispercionEstable = 0.2f;
        private long m_msDeteccionEstable = 1000;
        private string m_portName = "COM1";
        private int m_baudRate = 9600;
        private short m_dataBits = 8;
        private StopBits m_stopBits = StopBits.One;
        private Handshake m_handShaque = Handshake.None;
        private Parity m_parity = Parity.None;
        private BALANZA_SERIAL_PORT_PROTOCOLO m_protocol = BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO;
        private int m_countDecimals = 0;



        public float PesoMaximoCero { get => m_pesoMaximoCero; set => m_pesoMaximoCero = value; }
        public float PesoMinimoValido { get => m_pesoMinimoValido; set => m_pesoMinimoValido = value; }
        public float PesoMaximoValido { get => m_pesoMaximoValido; set => m_pesoMaximoValido = value; }
        public float MaximaDispercionEstable { get => m_maximaDispercionEstable; set => m_maximaDispercionEstable = value; }
        public long MsDeteccionEstable { get => m_msDeteccionEstable; set => m_msDeteccionEstable = value; }
        public string PortName { get => m_portName; set => m_portName = value; }
        public int BaudRate { get => m_baudRate; set => m_baudRate = value; }
        public short DataBits { get => m_dataBits; set => m_dataBits = value; }
        public StopBits StopBits { get => m_stopBits; set => m_stopBits = value; }
        public Handshake HandShaque { get => m_handShaque; set => m_handShaque = value; }
        public Parity Parity { get => m_parity; set => m_parity = value; }
        public BALANZA_SERIAL_PORT_PROTOCOLO Protocol { get => m_protocol; set => m_protocol = value; }
        public int CountDecimals { get => m_countDecimals; set => m_countDecimals = value; }

        public CCnfgScale()
        {
        }
    }
    #endregion



    #region Clase CDatScale
    /// <summary>
    /// CDatScale
    /// Clase que contiene datos recividos desde la balanza
    /// </summary>
    public class CDatScale
    {
        private float m_pesoNeto;
        private float m_pesoTara;
        private int m_cantidadDecimales;
        private bool m_isTeclaImpPulsada;
        private bool m_isTaraActiva;
        private bool m_isPesoEstable;
        private bool m_isPesoZero;
        private bool m_isPesoNegativo;
        private bool m_isPesadaAcumulada;

        private bool m_isPasoPorCero;
        private bool m_isPesajeEstable;
        private bool m_isPesoNetoValido;
        private float m_ultimoPesoNeto;
        private float m_pesoNetoAnterior;
        private long m_msAnterior;

        private BALANZA_SERIAL_PORT_PROTOCOLO m_activeProtocolo;

        public BALANZA_SERIAL_PORT_PROTOCOLO PROTOCOLO
        {
            get { return m_activeProtocolo; }
            set { m_activeProtocolo = value; }
        }


        public float PesoNeto
        {
            get { return m_pesoNeto; }
            set { m_pesoNeto = value; }
        }
        public float PesoTara
        {
            get { return m_pesoTara; }
            set { m_pesoTara = value; }
        }
        public bool isKeyImpDown
        {
            get { return m_isTeclaImpPulsada; }
            set { m_isTeclaImpPulsada = value; }
        }
        public bool isTareActive
        {
            get { return m_isTaraActiva; }
            set { m_isTaraActiva = value; }
        }
        public bool isWeightStable
        {
            get { return m_isPesoEstable; }
            set { m_isPesoEstable = value; }
        }
        public bool isWeightZero
        {
            get { return m_isPesoZero; }
            set { m_isPesoZero = value; }
        }
        public bool isWeightNegative
        {
            get { return m_isPesoNegativo; }
            set { m_isPesoNegativo = value; }
        }
        public bool isWeightAcumulated
        {
            get { return m_isPesadaAcumulada; }
            set { m_isPesadaAcumulada = value; }
        }
        public int CantDecimales
        {
            get { return CantidadDecimales; }
        }

        public bool IsPasoPorCero { get => m_isPasoPorCero; set => m_isPasoPorCero = value; }
        public bool IsPesajeEstable { get => m_isPesajeEstable; set => m_isPesajeEstable = value; }
        public bool IsPesoNetoValido { get => m_isPesoNetoValido; set => m_isPesoNetoValido = value; }
        public float UltimoPesoNeto { get => m_ultimoPesoNeto; set => m_ultimoPesoNeto = value; }
        public float PesoNetoAnterior { get => m_pesoNetoAnterior; set => m_pesoNetoAnterior = value; }
        public long MsAnterior { get => m_msAnterior; set => m_msAnterior = value; }
        public int CantidadDecimales { get => m_cantidadDecimales; set => m_cantidadDecimales = value; }

        public CDatScale()
        {
            Clear();
        }
        
        public CDatScale(byte[] tramaMsgBalanza, BALANZA_SERIAL_PORT_PROTOCOLO _protocolo, int cantDecimalesForzar = 0)
        {
            Clear();
            PROTOCOLO = _protocolo;
            CantidadDecimales = cantDecimalesForzar;
            LoadTrama(tramaMsgBalanza);
        }

        public CDatScale(CDatScale icpy)
        {
            m_pesoNeto = icpy.m_pesoNeto;
            m_pesoTara = icpy.m_pesoTara;
            CantidadDecimales = icpy.CantidadDecimales;
            m_isTeclaImpPulsada = icpy.m_isTeclaImpPulsada;
            m_isTaraActiva = icpy.m_isTaraActiva;
            m_isPesoEstable = icpy.m_isPesoEstable;
            m_isPesoZero = icpy.m_isPesoZero;
            m_isPesoNegativo = icpy.m_isPesoNegativo;
            m_isPesadaAcumulada = icpy.m_isPesadaAcumulada;
            m_isPasoPorCero = icpy.m_isPasoPorCero;
            m_isPesoNetoValido = icpy.m_isPesoNetoValido;
            m_ultimoPesoNeto = icpy.m_ultimoPesoNeto;
            m_isPesajeEstable = icpy.m_isPesajeEstable;
            m_msAnterior = icpy.m_msAnterior;
            m_pesoNetoAnterior = icpy.m_pesoNetoAnterior;

        }

        public void LoadTrama(byte[] tramaMsgBalanza)
        {
            if (PROTOCOLO == BALANZA_SERIAL_PORT_PROTOCOLO.TBE_CONTINUO)
            {
                int decimales = ((tramaMsgBalanza[1] & 0x07) >= 2) && ((tramaMsgBalanza[1] & 0x07) <= 5) ? (tramaMsgBalanza[1] & 0x07) : 5;
                CantidadDecimales = 5 - decimales;

                m_isPesadaAcumulada = Convert.ToBoolean(tramaMsgBalanza[2] & 0x04);
                m_isPesoNegativo = Convert.ToBoolean(tramaMsgBalanza[2] & 0x02);
                m_isPesoEstable = !Convert.ToBoolean(tramaMsgBalanza[2] & 0x08);
                m_isTaraActiva = Convert.ToBoolean(tramaMsgBalanza[2] & 0x01);
                m_isTeclaImpPulsada = Convert.ToBoolean(tramaMsgBalanza[3] & 0x08);
                m_pesoNeto = Convert.ToSingle(Encoding.UTF8.GetString(tramaMsgBalanza, 4, 6));
                if (CantidadDecimales > 0)
                    m_pesoNeto /= (Single)Math.Pow(10.0f, (Double)CantidadDecimales);
                if (m_isPesoNegativo)
                    m_pesoNeto *= -1.0f;
                m_pesoTara = Convert.ToSingle(Encoding.UTF8.GetString(tramaMsgBalanza, 10, 6));
                if (CantidadDecimales > 0)
                    m_pesoTara /= (Single)Math.Pow(10.0f, (Double)CantidadDecimales);

                m_isPesoZero = (m_pesoNeto == 0.0f);
            }
            else if (PROTOCOLO == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A)
            {
                m_isPesoNegativo = Convert.ToBoolean(tramaMsgBalanza[0] & 0x08);
                m_isPesoEstable = Convert.ToBoolean(tramaMsgBalanza[0] & 0x04);
                m_isTaraActiva = Convert.ToBoolean(tramaMsgBalanza[0] & 0x01);
                m_pesoNeto = Convert.ToSingle(Encoding.UTF8.GetString(tramaMsgBalanza, 1, 6));
                if (CantidadDecimales > 0)
                    m_pesoNeto /= (Single)Math.Pow(10.0f, (Double)CantidadDecimales);
                if (m_isPesoNegativo)
                    m_pesoNeto *= -1.0f;
                m_isPesoZero = (m_pesoNeto == 0.0f);
            }
            else if (PROTOCOLO == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05B)
            {
                m_isPesoNegativo = tramaMsgBalanza[1] == '-';
                m_isPesoEstable = tramaMsgBalanza[11] == ' ';
                m_isTaraActiva = tramaMsgBalanza[10] == 'N';
                m_pesoNeto = Convert.ToSingle((Encoding.UTF8.GetString(tramaMsgBalanza, 2, 7)).Replace('.', ','));
                if (m_isPesoNegativo)
                    m_pesoNeto *= -1.0f;
                m_isPesoZero = (m_pesoNeto == 0.0f);
            }
        }

        public void Clear()
        {
            m_pesoNeto = 0.0f;
            m_pesoTara = 0.0f;
            CantidadDecimales = 0;
            m_isTeclaImpPulsada = false;
            m_isTaraActiva = false;
            m_isPesoZero = true;
            m_isPesoEstable = true;
            m_isPesoNegativo = false;
            m_isPesadaAcumulada = false;
            m_isPasoPorCero = true;
            m_isPesoNetoValido = false;
            m_ultimoPesoNeto = 9999.9999f;
            m_isPesajeEstable = false;
            m_msAnterior = 0;
            m_pesoNetoAnterior = 0.0f;
        }

        public override string ToString()
        {
            return m_pesoNeto.ToString();
        }

        /*****************************************************************************************
         * Update(CCnfgScale)
         * Descripcion: Llamar perioricamente a este metodo actualizar las variables y banderas de estado 
         *              del pesaje.
         * 
         * Parametro:   (CCnfgScale) clase que contiene la configuracion que determina la estabilidad y 
         *              otros parametros del pesaje.
         ****************************************************************************************/
        public void Update(byte[] tramaMsgBalanza, CCnfgScale datCnfg)
        {
            //carga las variables miembro de estado de peso con la trama recivida
            LoadTrama(tramaMsgBalanza);

            long msActual = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            m_isPesoNetoValido = (PesoNeto >= datCnfg.PesoMinimoValido && PesoNeto <= datCnfg.PesoMaximoValido);

            if (PesoNeto <= datCnfg.PesoMaximoCero && m_ultimoPesoNeto > datCnfg.PesoMaximoCero)
                m_isPasoPorCero = true;

            m_ultimoPesoNeto = PesoNeto;

            // determinar valor de peso estable
            if (Math.Abs(m_pesoNetoAnterior - PesoNeto) <= datCnfg.MaximaDispercionEstable)
            {
                if (!m_isPesajeEstable && (msActual - m_msAnterior) >= datCnfg.MsDeteccionEstable)
                {
                    m_isPesajeEstable = true;
                }
            }
            else
            {
                m_pesoNetoAnterior = PesoNeto;
                m_msAnterior = msActual;
                m_isPesajeEstable = false;
            }
        }
        /// <summary>
        /// Indica si el peso es valido, esta estable y habia pasado por cero desde el ultimo
        /// reset.
        /// </summary>
        /// <returns></returns>
        public bool IsPesoOk()
        {
            return (m_isPasoPorCero && m_isPesajeEstable && m_isPesoNetoValido);
        }

    }
    #endregion
}
