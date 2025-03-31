using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BalanzaSerialPort;
using System.IO.Ports;

namespace ScaleSerialCtrl
{
    public partial class ScaleSerialCtrl : UserControl
    {

        /// <summary>Response data event. This event is called when new data arrives</summary>
        public delegate void NewWeight(object sender ,CDatScale datScale);
        /// <summary>Response data event. This event is called when new data arrives</summary>
        public event NewWeight OnNewWeight;

        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public delegate void ExceptionData(object sender, EXCEPTION_CBALANZASERIALPORT exception);
        /// <summary>Exception data event. This event is called when the data is incorrect</summary>
        public event ExceptionData OnException;

        string m_nameScale = "";
        CBalanzaSerialPort m_connectionScale;
        bool m_automaticWeighting = false;


        public CBalanzaSerialPort ConnectionScale { get => m_connectionScale; set => m_connectionScale = value; }
        public CDatScale DatScale { get => m_connectionScale.DatScale;}
        public bool IsConnected { get => m_connectionScale != null && m_connectionScale.IsOpen();}

        [Description("Nombre a Establecer a la Balanza"),Category("Appearance"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(""),Browsable(true)]
        public string NameScale { get { return m_nameScale; } set { groupBox_Balanza.Text = value; m_nameScale = value; } }


        public bool AutomaticWeighting { get => m_automaticWeighting; set => m_automaticWeighting = value; }

        public ScaleSerialCtrl()
        {
            InitializeComponent();
            HandleDestroyed += ScaleSerialCtrl_HandleDestroyed;
        }


        public bool Connect(CCnfgScale cnfgScale)
        {
            bool conectada = false;

            try
            {
                if (ConnectionScale == null || !ConnectionScale.IsOpen())
                {
                    ConnectionScale = new CBalanzaSerialPort(cnfgScale);

                    ConnectionScale.OnNewWeight += ConnectionScale_OnNewWeight;
                    ConnectionScale.OnException += ConnectionScale_OnException;

                    if (ConnectionScale.Connect())
                    {
                        SetButtonLed(button_ledConexionBalanza, true, Color.Green, Color.Red);
                        conectada = true;
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido establecer coneccion con el puerto COM de la Balanza", "CONEXIÓN SERIE CON LA BALANZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SetButtonLed(button_ledConexionBalanza, false, Color.Green, Color.Red);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR DE CONEXIÓN CON EL PUERTO COM DE LA BALANZA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conectada;

        }
        public void Disconnect()
        {
            if(ConnectionScale != null)
            {
                ConnectionScale.Disconnect();
            }
        }
        private void ScaleSerialCtrl_HandleDestroyed(object sender, EventArgs e)
        {
            if (ConnectionScale != null)
            {
                ConnectionScale.Disconnect();
                System.Threading.Thread.Sleep(500);
            }
        }

        private void ConnectionScale_OnException(EXCEPTION_CBALANZASERIALPORT exception)
        {
            try
            {
                if (exception == EXCEPTION_CBALANZASERIALPORT.NO_RECEPTION)
                {
                    SetTextCtrlSecure(textBox_displayBalanza,"REC. ERROR");
                    SetButtonLed(button_ledConexionBalanza, false, Color.Green, Color.Red);
                }
                else if (exception == EXCEPTION_CBALANZASERIALPORT.PROTOCOL_ERROR)
                {
                    SetTextCtrlSecure(textBox_displayBalanza,"PROT. ERROR");
                    SetButtonLed(button_ledConexionBalanza, false, Color.Green, Color.Red);
                }
                else if (exception == EXCEPTION_CBALANZASERIALPORT.PORT_ERROR)
                {
                    SetTextCtrlSecure(textBox_displayBalanza, "COM ERROR");
                    SetButtonLed(button_ledConexionBalanza, false, Color.Green, Color.Red);
                }

                OnException?.Invoke(this,exception);
            }
            catch (SystemException e)
            {
            }
        }

        private void ConnectionScale_OnNewWeight(CDatScale datPesaje)
        {
            try
            {
                SetTextCtrlSecure(textBox_displayBalanza,datPesaje.PesoNeto.ToString());
                SetButtonLed(button_ledEstable, datPesaje.isWeightStable);
                SetButtonLed(button_ledZero, datPesaje.isWeightZero);
                SetButtonLed(button_ledTara, datPesaje.isTareActive, Color.Red, Color.Green);
                SetButtonLed(button_ledConexionBalanza, true, Color.Green, Color.Red);
                SetButtonLed(button_LedPesar, datPesaje.IsPesoNetoValido, Color.Green, Color.Red);
                if (AutomaticWeighting && datPesaje.IsPesoOk())
                {
                    ConnectionScale.ResetPasoPorCero();
                    OnNewWeight?.Invoke(this,datPesaje);
                }
                if(!AutomaticWeighting && datPesaje.IsPesoOk())
                {
                    SetEnableButtonSecure(button_Pesar,true);
                }
                else if(!AutomaticWeighting && datPesaje.IsPesoNetoValido)
                {
                    SetEnableButtonSecure(button_Pesar, false);
                }
            }
            catch (SystemException e)
            {

            }

        }

        private void button_Pesar_Click(object sender, EventArgs e)
        {
            ConnectionScale.ResetPasoPorCero();
            OnNewWeight?.Invoke(this,new CDatScale(DatScale));
            SetEnableButtonSecure(button_Pesar, false);
        }

        private void SetButtonLed(Button ctrlButton, bool Encender, Color colorEncendido, Color colorApagado)
        {
            SetColorButtonSecure(ctrlButton, Encender ? colorEncendido : colorApagado);
        }
        private void SetButtonLed(Button ctrlButton, bool Encender)
        {
            SetColorButtonSecure(ctrlButton, Encender ? Color.Green : Color.White);

        }
        private void SetColorButtonSecure(Button ctrlButton, Color color)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctrlButton.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetColorButtonSecure(ctrlButton, color); }));
            }
            else
            {
                ctrlButton.BackColor = color;
            }
        }
        private void SetEnableButtonSecure(Button ctrlButton, bool enable)
        {
            if (ctrlButton.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetEnableButtonSecure(ctrlButton, enable); }));
            }
            else
            {
                ctrlButton.Enabled = enable;
            }
        }
        private void SetTextCtrlSecure(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                { SetTextCtrlSecure(ctrl, text); }));
            }
            else
            {
                ctrl.Text = text;
            }
        }

        private void checkBox_modoPesajeAutomatico_CheckedChanged(object sender, EventArgs e)
        {
            AutomaticWeighting = checkBox_modoPesajeAutomatico.Checked;
            UpdateStatePesajeAutomatico();
        }
        private void UpdateStatePesajeAutomatico()
        {
            SetEnableButtonSecure(button_Pesar, false);
            if (AutomaticWeighting)
            {
                ledCtrl_pesajeAutomatico.FlasherLedStart();
            }
            else
            {
                ledCtrl_pesajeAutomatico.FlasherLedStop();
                ledCtrl_pesajeAutomatico.LedStatus = false;
            }
        }

    }
}
