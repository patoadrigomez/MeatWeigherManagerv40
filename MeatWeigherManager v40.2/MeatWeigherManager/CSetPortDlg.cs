using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO.Ports;
using System.Xml.Serialization;

namespace SetPortDlg
{
	/// <summary>
	/// Summary description for SettingsForm.
	/// </summary>
	public class CSetPortDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBoxPort;
		private System.Windows.Forms.ComboBox comboBoxBaud;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ComboBox comboBoxParity;
		private System.Windows.Forms.ComboBox comboBoxDataBits;
		private System.Windows.Forms.ComboBox comboBoxStopBits;
		private System.Windows.Forms.Button BtnAceptar;
		private System.Windows.Forms.Button BtnCancelar;
		private System.Windows.Forms.Label label_puerto;
		private System.Windows.Forms.Label label_Baudios;
		private System.Windows.Forms.Label label_dataBits;
		private System.Windows.Forms.Label label_paridad;
        private Label label1;
        private ComboBox comboBox_HandShake;
		private System.Windows.Forms.Label label_stopBits;

        public string m_namePort;
        public int m_baudios;
        public int m_dataBits;
        public StopBits m_stopBits;
        public Parity m_parity;
        public Handshake m_handShake;

        public CSetPortDlg()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSetPortDlg));
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.BtnAceptar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.label_puerto = new System.Windows.Forms.Label();
            this.label_Baudios = new System.Windows.Forms.Label();
            this.label_dataBits = new System.Windows.Forms.Label();
            this.label_paridad = new System.Windows.Forms.Label();
            this.label_stopBits = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_HandShake = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.Location = new System.Drawing.Point(58, 16);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(100, 21);
            this.comboBoxPort.TabIndex = 0;
            this.comboBoxPort.DropDown += new System.EventHandler(this.comboBoxPort_DropDown);
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaud.Items.AddRange(new object[] {
            "75",
            "110",
            "134",
            "150",
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "4800",
            "7200",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000"});
            this.comboBoxBaud.Location = new System.Drawing.Point(275, 13);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(98, 21);
            this.comboBoxBaud.TabIndex = 2;
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.Items.AddRange(new object[] {
            "none",
            "odd",
            "even",
            "mark",
            "space"});
            this.comboBoxParity.Location = new System.Drawing.Point(275, 97);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(98, 21);
            this.comboBoxParity.TabIndex = 4;
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBits.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxDataBits.Location = new System.Drawing.Point(275, 55);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(98, 21);
            this.comboBoxDataBits.TabIndex = 6;
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBits.Items.AddRange(new object[] {
            "none",
            "1",
            "2",
            "1.5"});
            this.comboBoxStopBits.Location = new System.Drawing.Point(275, 136);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(98, 21);
            this.comboBoxStopBits.TabIndex = 22;
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnAceptar.Location = new System.Drawing.Point(265, 219);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(64, 24);
            this.BtnAceptar.TabIndex = 14;
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelar.Location = new System.Drawing.Point(337, 219);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(64, 24);
            this.BtnCancelar.TabIndex = 21;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // label_puerto
            // 
            this.label_puerto.Location = new System.Drawing.Point(6, 18);
            this.label_puerto.Name = "label_puerto";
            this.label_puerto.Size = new System.Drawing.Size(48, 16);
            this.label_puerto.TabIndex = 23;
            this.label_puerto.Text = "Puerto";
            // 
            // label_Baudios
            // 
            this.label_Baudios.Location = new System.Drawing.Point(185, 16);
            this.label_Baudios.Name = "label_Baudios";
            this.label_Baudios.Size = new System.Drawing.Size(48, 16);
            this.label_Baudios.TabIndex = 24;
            this.label_Baudios.Text = "Baudios";
            // 
            // label_dataBits
            // 
            this.label_dataBits.Location = new System.Drawing.Point(185, 58);
            this.label_dataBits.Name = "label_dataBits";
            this.label_dataBits.Size = new System.Drawing.Size(72, 16);
            this.label_dataBits.TabIndex = 25;
            this.label_dataBits.Text = "Bits de Dato";
            // 
            // label_paridad
            // 
            this.label_paridad.Location = new System.Drawing.Point(185, 101);
            this.label_paridad.Name = "label_paridad";
            this.label_paridad.Size = new System.Drawing.Size(48, 16);
            this.label_paridad.TabIndex = 26;
            this.label_paridad.Text = "Paridad";
            // 
            // label_stopBits
            // 
            this.label_stopBits.Location = new System.Drawing.Point(185, 140);
            this.label_stopBits.Name = "label_stopBits";
            this.label_stopBits.Size = new System.Drawing.Size(72, 16);
            this.label_stopBits.TabIndex = 27;
            this.label_stopBits.Text = "Bits de Stop";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(185, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "HandShake";
            // 
            // comboBox_HandShake
            // 
            this.comboBox_HandShake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_HandShake.Items.AddRange(new object[] {
            "None",
            "XOnXOff",
            "RequestToSend",
            "RequestToSendXOnXOff"});
            this.comboBox_HandShake.Location = new System.Drawing.Point(275, 175);
            this.comboBox_HandShake.Name = "comboBox_HandShake";
            this.comboBox_HandShake.Size = new System.Drawing.Size(126, 21);
            this.comboBox_HandShake.TabIndex = 29;
            // 
            // CSetPortDlg
            // 
            this.AcceptButton = this.BtnAceptar;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.BtnCancelar;
            this.ClientSize = new System.Drawing.Size(413, 255);
            this.Controls.Add(this.comboBox_HandShake);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_stopBits);
            this.Controls.Add(this.label_paridad);
            this.Controls.Add(this.label_dataBits);
            this.Controls.Add(this.label_Baudios);
            this.Controls.Add(this.label_puerto);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnAceptar);
            this.Controls.Add(this.comboBoxBaud);
            this.Controls.Add(this.comboBoxDataBits);
            this.Controls.Add(this.comboBoxParity);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.comboBoxStopBits);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSetPortDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuracion Puerto COM Rs-232";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void SettingsForm_Load(object sender, System.EventArgs e)
		{
            comboBoxPort.Text = m_namePort;
            comboBoxBaud.SelectedIndex = comboBoxBaud.FindString(m_baudios.ToString());
            comboBoxDataBits.SelectedIndex = comboBoxDataBits.FindString(m_dataBits.ToString());
            comboBoxParity.SelectedIndex = (int)m_parity;
            comboBoxStopBits.SelectedIndex = (int)m_stopBits;
            comboBox_HandShake.SelectedIndex = comboBox_HandShake.FindString(m_handShake.ToString());
		}

		private void Import()
		{
			/*
            comboBoxPort.Text = settingSerialPort.port;
			comboBoxBaud.SelectedIndex = comboBoxBaud.FindString(settingSerialPort.baudRate.ToString());
			comboBoxParity.SelectedIndex = (int)settingSerialPort.parity;
			comboBoxDataBits.SelectedIndex = comboBoxDataBits.FindString(settingSerialPort.dataBits.ToString());
			comboBoxStopBits.SelectedIndex = (int)settingSerialPort.stopBits;
             */ 
		}

		private void Export()
		{
			/*
            settingSerialPort.port = comboBoxPort.Text;
			settingSerialPort.baudRate = int.Parse(comboBoxBaud.Text);
			settingSerialPort.parity = (CSerialPort.Parity)comboBoxParity.SelectedIndex;
			settingSerialPort.dataBits = int.Parse(comboBoxDataBits.Text);
			settingSerialPort.stopBits = (CSerialPort.StopBits)comboBoxStopBits.SelectedIndex;
             */ 
		}

        private void comboBoxPort_DropDown(object sender, System.EventArgs e)
		{
			if (comboBoxPort.Items.Count < 1) FillPorts(comboBoxPort);	
		}

        private void BtnAceptar_Click(object sender, System.EventArgs e)
		{
            m_namePort = comboBoxPort.Text;
            m_baudios = int.Parse(comboBoxBaud.Text);
            m_dataBits = int.Parse(comboBoxDataBits.Text);
            m_parity = (Parity)comboBoxParity.SelectedIndex;
            m_stopBits = (StopBits)comboBoxStopBits.SelectedIndex;
            m_handShake = (Handshake)comboBox_HandShake.SelectedIndex;
		}

        private void BtnCancelar_Click(object sender, System.EventArgs e)
		{
		}

		private void FillPorts(ComboBox cb)
		{
            string[] Portnames = SerialPort.GetPortNames();
            if (Portnames != null)
            {
                comboBoxPort.Items.AddRange(Portnames);
            }
            else
            {
                MessageBox.Show("No Hay Puertos COM Detectados en este equipo , agregue el Hardware correspondiente", "Validando Puerto COM Disponible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboBoxPort.Items.Clear();
            }
		}
	}
}