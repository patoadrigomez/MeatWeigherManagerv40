using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HidKeyboardScannerLib
{
    /// <summary>
    /// HidScannerLib
    /// Clase de gestion de captura de datos de un escanner con Interface HOST de 
    /// tipo HID (emulacion de teclado)
    /// Requiere que el escanner este configurado con un prefijo y un sufijo:
    /// STX{data}CR
    /// 
    /// Si un mensaje de datos con este protocolo es recivido, se dispara un evento 
    /// a un form subscripto.
    /// 
    /// </summary>
    public class HidScannerLib:IDisposable
    {
        #region PUBLIC PROPERTYES
        
        /// <summary>
        /// Scanner Data Response event. This event is called when new data arrives from scanner in keyboard emulation
        /// </summary>
        public delegate void NewDataScanner(string canData);
        public event NewDataScanner OnNewDataScanner;
        
        public bool inReady { get; set; }=false;
        #endregion

        #region PRIVATE PROPERTYES
        private Form Handle;
        private Control MakeFocus;
        private string BufferReady = "";
        private const char PrefixDataReady = '\u0002'; //STX
        private const char SuffixDataReady = '\r'; //ENTER
        private const int  MaxIntervalCharDataReady = 5000;
        private Timer TimerMonitor = new Timer();
        #endregion

        public HidScannerLib(Form handle, Control makeFocus=null)
        {
            Handle = handle;
            handle.KeyPress += Handle_KeyPress;
            TimerMonitor.Interval = MaxIntervalCharDataReady;
            TimerMonitor.Tick += TimerMonitor_Tick;
            MakeFocus = makeFocus;
        }

        ~HidScannerLib()
        {
            Dispose();
        }
        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            Handle.KeyPress -= Handle_KeyPress;
        }

        private void Handle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == PrefixDataReady)
            {
                inReady = true;
                MakeFocus?.Focus();
                BufferReady = "";
                ResetTimerMonitor();
            }
            else if (inReady && e.KeyChar == SuffixDataReady)
            {
                inReady = false;
                TimerMonitor.Stop();
                
                if(OnNewDataScanner != null)
                    OnNewDataScanner(BufferReady);
            }
            else if(inReady)
            {
                BufferReady += e.KeyChar;
                ResetTimerMonitor();
            }
        }

        private void TimerMonitor_Tick(object sender, EventArgs e)
        {
            inReady = false;
            TimerMonitor.Stop();
        }
        
        private void ResetTimerMonitor()
        {
            TimerMonitor.Stop();
            TimerMonitor.Start();
        }
    }
}
