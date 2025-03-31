using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using ConfigApp;
using SetPortDlg;
using EditStringTouchDlg;
using EditValueTouchDlg;
using System.Drawing.Printing;
using System.Text;
using System.Linq;
using BalanzaSerialPort;

namespace MeatWeigherManager
{
	/// <summary>
	/// Descripción breve de CConfigAppDlg.
	/// </summary>
	public class CConfigAppDlg : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_cancelar;
        private TabControl tabControlB1;
        private TabPage tabPage_ConexionB1;
        private GroupBox groupBox1;
        private Label label14;
        private TextBox textBox_maximoValorCeroB1;
        private TextBox textBox_PesoValidoMaximoB1;
        private Label label15;
        private TextBox textBox_PesoValidoMinimoB1;
        private Label label17;
        private Label label16;
        private Button button_ConfigurarComB1;
        private Label label7;
        private TextBox textBox_PUERTO_COM_B1;
        private Label label6;
        private TabPage tabPage_General;
        private Button button_AbrirSelectorDirectorio;
        private TextBox textBox_PathDirectorioReportes;
        private Label label18;
        private Label label1;
        private TextBox textBoxID_ESTACION;
        private TabPage tabPage_Empresa;
        private Button button_seleccionArchivoLogoEmpresa;
        private TextBox textBox_pathLogoEmpresa;
        private Label label31;
        private Label label64;
        private TextBox textBox_nombreEmpresa;
        private Label label10;
        private Label label11;
        private TextBox textBox_tiempoDetectEstableB1;
        private Label label8;
        private Label label9;
        private TextBox textBox_maximaDispercionEstableB1;
        private TabPage tabPage_ConexionImpresor;
        private Label label27;
        private TextBox textBox_cantEtiquetas;
        private TextBox textBox_nombreFormatoEtiquetaProducto;
        private Label label12;
        private Button button_AbrirBuscadorArchivosFormatosEtiquetas;
        private TextBox textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS;
        private Label label13;
        private Label label19;
        private TextBox textBox_NOMBRE_IMPRESORA;
        private GroupBox groupBox_Etiqueta;
        private TabPage tabPage_ConexionB2;
        private Button button_ConfigurarComB2;
        private Label label22;
        private TextBox textBox_PUERTO_COM_B2;
        private GroupBox groupBox2;
        private Label label23;
        private Label label24;
        private TextBox textBox_tiempoDetectEstableB2;
        private Label label25;
        private Label label26;
        private TextBox textBox_maximaDispercionEstableB2;
        private Label label28;
        private Label label29;
        private Label label30;
        private TextBox textBox_PesoValidoMaximoB2;
        private Label label32;
        private TextBox textBox_PesoValidoMinimoB2;
        private Label label33;
        private TextBox textBox_maximoValorCeroB2;
        private TabPage tabPage_ConexionB3;
        private Button button_ConfigurarComB3;
        private Label label34;
        private TextBox textBox_PUERTO_COM_B3;
        private GroupBox groupBox3;
        private Label label35;
        private Label label36;
        private TextBox textBox_tiempoDetectEstableB3;
        private Label label37;
        private Label label38;
        private TextBox textBox_maximaDispercionEstableB3;
        private Label label39;
        private Label label40;
        private Label label41;
        private TextBox textBox_PesoValidoMaximoB3;
        private Label label42;
        private TextBox textBox_PesoValidoMinimoB3;
        private Label label43;
        private TextBox textBox_maximoValorCeroB3;
        private GroupBox groupBoxB1;
        private CheckBox checkBox_B1Habilitada;
        private Label label44;
        private TextBox textBox_nombreB1;
        private Label label45;
        private CheckBox checkBox_B2Habilitada;
        private TextBox textBox_nombreB2;
        private GroupBox groupBoxB2;
        private Label label46;
        private CheckBox checkBox_B3Habilitada;
        private TextBox textBox_nombreB3;
        private GroupBox groupBoxB3;
        private Button button_buscarImpresora;
        private TabPage tabPage_scanner;
        private Label label20;
        private TextBox textBox_modeloEscannerZebra;
        private TextBox textBox_nombreFormatoEtiquetaCaja;
        private Label label50;
        private TextBox textBox_nombreFormatoEtiquetaContenedor;
        private Label label49;
        private TabPage tabPage_pesajeDeCajas;
        private Label label51;
        private Label label52;
        private TextBox textBox_toleranciaPesajeCaja;
        private TextBox textBox_LineaDeTextoSuperiorDeEtiqueta;
        private Label label53;
        private TextBox textBox_LineaDeTextoInferiorDeEtiqueta;
        private Label label54;
        private Label label55;
        private ComboBox comboBox_EncodingPrinter;
        private TabPage tabPage_BaseDatos;
        private CheckBox checkBox_tipoDeSeguridadSQL_SSPI;
        private TextBox textBox_baseDeDatos;
        private TextBox textBox_passwordDB;
        private TextBox textBox_usuarioDB;
        private TextBox textBox_servidor;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label_cantDecimalesBal1;
        private ComboBox comboBox_cantDecimalesBalanza1;
        private Label label57;
        private ComboBox comboBox_protocoloBalanza1;
        private Label label_cantDecimalesBal2;
        private ComboBox comboBox_cantDecimalesBalanza2;
        private Label label58;
        private ComboBox comboBox_protocoloBalanza2;
        private Label label_cantDecimalesBal3;
        private ComboBox comboBox_cantDecimalesBalanza3;
        private Label label60;
        private ComboBox comboBox_protocoloBalanza3;
        private TabPage tabPage_ingresoAPlanta;
        private Label label59;
        private Label label61;
        private TextBox textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta;
        private CheckBox checkBox_pesoUnidadesRemitidoObligatorio;
        private TextBox textBox_nombreFormatoEtiquetaPedido;
        private Label label47;
        private CheckBox checkBox_mantenerUltimaTipificacionEntrePesajes;
        private CheckBox checkBox_mantenerUltimaTropaEntrePesadas;
        private TabPage tabPage_Egresos;
        private CheckBox checkBox_permitirColectarMasUnidadesQueLasPedidas;
        private CheckBox checkBox_permiteSimularLecturaScanner;
        private Label label21;
        private ComboBox comboBox_tipoInterfaceHostScanner;

        //Variables de configuracion

        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public CConfigAppDlg()
		{
			InitializeComponent();
			CargarDialogo();
		}

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
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

		#region Código generado por el Diseñador de Windows Forms
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CConfigAppDlg));
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_cancelar = new System.Windows.Forms.Button();
            this.tabControlB1 = new System.Windows.Forms.TabControl();
            this.tabPage_BaseDatos = new System.Windows.Forms.TabPage();
            this.checkBox_tipoDeSeguridadSQL_SSPI = new System.Windows.Forms.CheckBox();
            this.textBox_baseDeDatos = new System.Windows.Forms.TextBox();
            this.textBox_passwordDB = new System.Windows.Forms.TextBox();
            this.textBox_usuarioDB = new System.Windows.Forms.TextBox();
            this.textBox_servidor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage_ConexionB1 = new System.Windows.Forms.TabPage();
            this.label44 = new System.Windows.Forms.Label();
            this.checkBox_B1Habilitada = new System.Windows.Forms.CheckBox();
            this.textBox_nombreB1 = new System.Windows.Forms.TextBox();
            this.groupBoxB1 = new System.Windows.Forms.GroupBox();
            this.label_cantDecimalesBal1 = new System.Windows.Forms.Label();
            this.comboBox_cantDecimalesBalanza1 = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.comboBox_protocoloBalanza1 = new System.Windows.Forms.ComboBox();
            this.button_ConfigurarComB1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_tiempoDetectEstableB1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_maximaDispercionEstableB1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_PesoValidoMaximoB1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_PesoValidoMinimoB1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_maximoValorCeroB1 = new System.Windows.Forms.TextBox();
            this.textBox_PUERTO_COM_B1 = new System.Windows.Forms.TextBox();
            this.tabPage_ConexionB2 = new System.Windows.Forms.TabPage();
            this.label45 = new System.Windows.Forms.Label();
            this.checkBox_B2Habilitada = new System.Windows.Forms.CheckBox();
            this.textBox_nombreB2 = new System.Windows.Forms.TextBox();
            this.groupBoxB2 = new System.Windows.Forms.GroupBox();
            this.label_cantDecimalesBal2 = new System.Windows.Forms.Label();
            this.comboBox_cantDecimalesBalanza2 = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.comboBox_protocoloBalanza2 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.button_ConfigurarComB2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox_tiempoDetectEstableB2 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox_maximaDispercionEstableB2 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox_PesoValidoMaximoB2 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox_PesoValidoMinimoB2 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox_maximoValorCeroB2 = new System.Windows.Forms.TextBox();
            this.textBox_PUERTO_COM_B2 = new System.Windows.Forms.TextBox();
            this.tabPage_ConexionB3 = new System.Windows.Forms.TabPage();
            this.label46 = new System.Windows.Forms.Label();
            this.checkBox_B3Habilitada = new System.Windows.Forms.CheckBox();
            this.textBox_nombreB3 = new System.Windows.Forms.TextBox();
            this.groupBoxB3 = new System.Windows.Forms.GroupBox();
            this.label_cantDecimalesBal3 = new System.Windows.Forms.Label();
            this.comboBox_cantDecimalesBalanza3 = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.comboBox_protocoloBalanza3 = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.button_ConfigurarComB3 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox_tiempoDetectEstableB3 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.textBox_maximaDispercionEstableB3 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox_PesoValidoMaximoB3 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.textBox_PesoValidoMinimoB3 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.textBox_maximoValorCeroB3 = new System.Windows.Forms.TextBox();
            this.textBox_PUERTO_COM_B3 = new System.Windows.Forms.TextBox();
            this.tabPage_ConexionImpresor = new System.Windows.Forms.TabPage();
            this.label55 = new System.Windows.Forms.Label();
            this.comboBox_EncodingPrinter = new System.Windows.Forms.ComboBox();
            this.button_buscarImpresora = new System.Windows.Forms.Button();
            this.groupBox_Etiqueta = new System.Windows.Forms.GroupBox();
            this.textBox_nombreFormatoEtiquetaPedido = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox_LineaDeTextoInferiorDeEtiqueta = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox_LineaDeTextoSuperiorDeEtiqueta = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.textBox_nombreFormatoEtiquetaCaja = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox_nombreFormatoEtiquetaContenedor = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_cantEtiquetas = new System.Windows.Forms.TextBox();
            this.button_AbrirBuscadorArchivosFormatosEtiquetas = new System.Windows.Forms.Button();
            this.textBox_nombreFormatoEtiquetaProducto = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox_NOMBRE_IMPRESORA = new System.Windows.Forms.TextBox();
            this.tabPage_scanner = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox_tipoInterfaceHostScanner = new System.Windows.Forms.ComboBox();
            this.checkBox_permiteSimularLecturaScanner = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_modeloEscannerZebra = new System.Windows.Forms.TextBox();
            this.tabPage_ingresoAPlanta = new System.Windows.Forms.TabPage();
            this.checkBox_mantenerUltimaTipificacionEntrePesajes = new System.Windows.Forms.CheckBox();
            this.checkBox_mantenerUltimaTropaEntrePesadas = new System.Windows.Forms.CheckBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta = new System.Windows.Forms.TextBox();
            this.checkBox_pesoUnidadesRemitidoObligatorio = new System.Windows.Forms.CheckBox();
            this.tabPage_pesajeDeCajas = new System.Windows.Forms.TabPage();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.textBox_toleranciaPesajeCaja = new System.Windows.Forms.TextBox();
            this.tabPage_Egresos = new System.Windows.Forms.TabPage();
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas = new System.Windows.Forms.CheckBox();
            this.tabPage_Empresa = new System.Windows.Forms.TabPage();
            this.button_seleccionArchivoLogoEmpresa = new System.Windows.Forms.Button();
            this.textBox_pathLogoEmpresa = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.textBox_nombreEmpresa = new System.Windows.Forms.TextBox();
            this.tabPage_General = new System.Windows.Forms.TabPage();
            this.button_AbrirSelectorDirectorio = new System.Windows.Forms.Button();
            this.textBox_PathDirectorioReportes = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxID_ESTACION = new System.Windows.Forms.TextBox();
            this.tabControlB1.SuspendLayout();
            this.tabPage_BaseDatos.SuspendLayout();
            this.tabPage_ConexionB1.SuspendLayout();
            this.groupBoxB1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage_ConexionB2.SuspendLayout();
            this.groupBoxB2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage_ConexionB3.SuspendLayout();
            this.groupBoxB3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage_ConexionImpresor.SuspendLayout();
            this.groupBox_Etiqueta.SuspendLayout();
            this.tabPage_scanner.SuspendLayout();
            this.tabPage_ingresoAPlanta.SuspendLayout();
            this.tabPage_pesajeDeCajas.SuspendLayout();
            this.tabPage_Egresos.SuspendLayout();
            this.tabPage_Empresa.SuspendLayout();
            this.tabPage_General.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(695, 427);
            this.button_Aceptar.Name = "button_Aceptar";
            this.button_Aceptar.Size = new System.Drawing.Size(75, 23);
            this.button_Aceptar.TabIndex = 1;
            this.button_Aceptar.Text = "Aceptar";
            this.button_Aceptar.Click += new System.EventHandler(this.button_Aceptar_Click);
            // 
            // button_cancelar
            // 
            this.button_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancelar.Location = new System.Drawing.Point(775, 427);
            this.button_cancelar.Name = "button_cancelar";
            this.button_cancelar.Size = new System.Drawing.Size(75, 23);
            this.button_cancelar.TabIndex = 2;
            this.button_cancelar.Text = "Cancelar";
            // 
            // tabControlB1
            // 
            this.tabControlB1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlB1.Controls.Add(this.tabPage_BaseDatos);
            this.tabControlB1.Controls.Add(this.tabPage_ConexionB1);
            this.tabControlB1.Controls.Add(this.tabPage_ConexionB2);
            this.tabControlB1.Controls.Add(this.tabPage_ConexionB3);
            this.tabControlB1.Controls.Add(this.tabPage_ConexionImpresor);
            this.tabControlB1.Controls.Add(this.tabPage_scanner);
            this.tabControlB1.Controls.Add(this.tabPage_ingresoAPlanta);
            this.tabControlB1.Controls.Add(this.tabPage_pesajeDeCajas);
            this.tabControlB1.Controls.Add(this.tabPage_Egresos);
            this.tabControlB1.Controls.Add(this.tabPage_Empresa);
            this.tabControlB1.Controls.Add(this.tabPage_General);
            this.tabControlB1.Location = new System.Drawing.Point(0, 0);
            this.tabControlB1.Multiline = true;
            this.tabControlB1.Name = "tabControlB1";
            this.tabControlB1.SelectedIndex = 0;
            this.tabControlB1.Size = new System.Drawing.Size(854, 421);
            this.tabControlB1.TabIndex = 0;
            // 
            // tabPage_BaseDatos
            // 
            this.tabPage_BaseDatos.Controls.Add(this.checkBox_tipoDeSeguridadSQL_SSPI);
            this.tabPage_BaseDatos.Controls.Add(this.textBox_baseDeDatos);
            this.tabPage_BaseDatos.Controls.Add(this.textBox_passwordDB);
            this.tabPage_BaseDatos.Controls.Add(this.textBox_usuarioDB);
            this.tabPage_BaseDatos.Controls.Add(this.textBox_servidor);
            this.tabPage_BaseDatos.Controls.Add(this.label5);
            this.tabPage_BaseDatos.Controls.Add(this.label4);
            this.tabPage_BaseDatos.Controls.Add(this.label3);
            this.tabPage_BaseDatos.Controls.Add(this.label2);
            this.tabPage_BaseDatos.Location = new System.Drawing.Point(4, 40);
            this.tabPage_BaseDatos.Name = "tabPage_BaseDatos";
            this.tabPage_BaseDatos.Size = new System.Drawing.Size(846, 377);
            this.tabPage_BaseDatos.TabIndex = 1;
            this.tabPage_BaseDatos.Text = "Base de Datos";
            this.tabPage_BaseDatos.UseVisualStyleBackColor = true;
            // 
            // checkBox_tipoDeSeguridadSQL_SSPI
            // 
            this.checkBox_tipoDeSeguridadSQL_SSPI.AutoSize = true;
            this.checkBox_tipoDeSeguridadSQL_SSPI.Location = new System.Drawing.Point(16, 99);
            this.checkBox_tipoDeSeguridadSQL_SSPI.Name = "checkBox_tipoDeSeguridadSQL_SSPI";
            this.checkBox_tipoDeSeguridadSQL_SSPI.Size = new System.Drawing.Size(232, 17);
            this.checkBox_tipoDeSeguridadSQL_SSPI.TabIndex = 3;
            this.checkBox_tipoDeSeguridadSQL_SSPI.Text = "Tipo de Seguridad de Conecxion SQL SSPI";
            this.checkBox_tipoDeSeguridadSQL_SSPI.UseVisualStyleBackColor = true;
            this.checkBox_tipoDeSeguridadSQL_SSPI.CheckedChanged += new System.EventHandler(this.checkBox_tipoDeSeguridadSQL_SSPI_CheckedChanged);
            // 
            // textBox_baseDeDatos
            // 
            this.textBox_baseDeDatos.Location = new System.Drawing.Point(16, 72);
            this.textBox_baseDeDatos.Name = "textBox_baseDeDatos";
            this.textBox_baseDeDatos.Size = new System.Drawing.Size(184, 20);
            this.textBox_baseDeDatos.TabIndex = 2;
            this.textBox_baseDeDatos.DoubleClick += new System.EventHandler(this.textBox_baseDeDatos_DoubleClick);
            // 
            // textBox_passwordDB
            // 
            this.textBox_passwordDB.Location = new System.Drawing.Point(16, 175);
            this.textBox_passwordDB.Name = "textBox_passwordDB";
            this.textBox_passwordDB.PasswordChar = '*';
            this.textBox_passwordDB.Size = new System.Drawing.Size(184, 20);
            this.textBox_passwordDB.TabIndex = 5;
            this.textBox_passwordDB.DoubleClick += new System.EventHandler(this.textBox_passwordDB_DoubleClick);
            // 
            // textBox_usuarioDB
            // 
            this.textBox_usuarioDB.Location = new System.Drawing.Point(16, 135);
            this.textBox_usuarioDB.Name = "textBox_usuarioDB";
            this.textBox_usuarioDB.Size = new System.Drawing.Size(184, 20);
            this.textBox_usuarioDB.TabIndex = 4;
            this.textBox_usuarioDB.DoubleClick += new System.EventHandler(this.textBox_usuarioDB_DoubleClick);
            // 
            // textBox_servidor
            // 
            this.textBox_servidor.Location = new System.Drawing.Point(16, 27);
            this.textBox_servidor.Name = "textBox_servidor";
            this.textBox_servidor.Size = new System.Drawing.Size(184, 20);
            this.textBox_servidor.TabIndex = 1;
            this.textBox_servidor.DoubleClick += new System.EventHandler(this.textBox_servidor_DoubleClick);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Base de Datos";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Servidor";
            // 
            // tabPage_ConexionB1
            // 
            this.tabPage_ConexionB1.Controls.Add(this.label44);
            this.tabPage_ConexionB1.Controls.Add(this.checkBox_B1Habilitada);
            this.tabPage_ConexionB1.Controls.Add(this.textBox_nombreB1);
            this.tabPage_ConexionB1.Controls.Add(this.groupBoxB1);
            this.tabPage_ConexionB1.Location = new System.Drawing.Point(4, 40);
            this.tabPage_ConexionB1.Name = "tabPage_ConexionB1";
            this.tabPage_ConexionB1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ConexionB1.Size = new System.Drawing.Size(846, 377);
            this.tabPage_ConexionB1.TabIndex = 2;
            this.tabPage_ConexionB1.Text = "Configuración Balanza 1";
            this.tabPage_ConexionB1.UseVisualStyleBackColor = true;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(167, 23);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(80, 13);
            this.label44.TabIndex = 26;
            this.label44.Text = "DESCRIPCIÓN";
            // 
            // checkBox_B1Habilitada
            // 
            this.checkBox_B1Habilitada.AutoSize = true;
            this.checkBox_B1Habilitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_B1Habilitada.Location = new System.Drawing.Point(8, 19);
            this.checkBox_B1Habilitada.Name = "checkBox_B1Habilitada";
            this.checkBox_B1Habilitada.Size = new System.Drawing.Size(100, 21);
            this.checkBox_B1Habilitada.TabIndex = 26;
            this.checkBox_B1Habilitada.Text = "Habilitada";
            this.checkBox_B1Habilitada.UseVisualStyleBackColor = true;
            this.checkBox_B1Habilitada.CheckedChanged += new System.EventHandler(this.checkBox_B1Habilitada_CheckedChanged);
            // 
            // textBox_nombreB1
            // 
            this.textBox_nombreB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombreB1.Location = new System.Drawing.Point(253, 19);
            this.textBox_nombreB1.MaxLength = 20;
            this.textBox_nombreB1.Name = "textBox_nombreB1";
            this.textBox_nombreB1.Size = new System.Drawing.Size(178, 21);
            this.textBox_nombreB1.TabIndex = 25;
            this.textBox_nombreB1.Text = "BALANZA 1";
            this.textBox_nombreB1.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // groupBoxB1
            // 
            this.groupBoxB1.Controls.Add(this.label_cantDecimalesBal1);
            this.groupBoxB1.Controls.Add(this.comboBox_cantDecimalesBalanza1);
            this.groupBoxB1.Controls.Add(this.label57);
            this.groupBoxB1.Controls.Add(this.comboBox_protocoloBalanza1);
            this.groupBoxB1.Controls.Add(this.button_ConfigurarComB1);
            this.groupBoxB1.Controls.Add(this.label7);
            this.groupBoxB1.Controls.Add(this.groupBox1);
            this.groupBoxB1.Controls.Add(this.textBox_PUERTO_COM_B1);
            this.groupBoxB1.Location = new System.Drawing.Point(8, 42);
            this.groupBoxB1.Name = "groupBoxB1";
            this.groupBoxB1.Size = new System.Drawing.Size(423, 281);
            this.groupBoxB1.TabIndex = 25;
            this.groupBoxB1.TabStop = false;
            // 
            // label_cantDecimalesBal1
            // 
            this.label_cantDecimalesBal1.AutoSize = true;
            this.label_cantDecimalesBal1.Location = new System.Drawing.Point(17, 73);
            this.label_cantDecimalesBal1.Name = "label_cantDecimalesBal1";
            this.label_cantDecimalesBal1.Size = new System.Drawing.Size(126, 13);
            this.label_cantDecimalesBal1.TabIndex = 37;
            this.label_cantDecimalesBal1.Text = "CANTIDAD DECIMALES";
            // 
            // comboBox_cantDecimalesBalanza1
            // 
            this.comboBox_cantDecimalesBalanza1.FormattingEnabled = true;
            this.comboBox_cantDecimalesBalanza1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox_cantDecimalesBalanza1.Location = new System.Drawing.Point(162, 70);
            this.comboBox_cantDecimalesBalanza1.Name = "comboBox_cantDecimalesBalanza1";
            this.comboBox_cantDecimalesBalanza1.Size = new System.Drawing.Size(45, 21);
            this.comboBox_cantDecimalesBalanza1.TabIndex = 36;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(69, 46);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(74, 13);
            this.label57.TabIndex = 35;
            this.label57.Text = "PROTOCOLO";
            // 
            // comboBox_protocoloBalanza1
            // 
            this.comboBox_protocoloBalanza1.FormattingEnabled = true;
            this.comboBox_protocoloBalanza1.Location = new System.Drawing.Point(162, 43);
            this.comboBox_protocoloBalanza1.Name = "comboBox_protocoloBalanza1";
            this.comboBox_protocoloBalanza1.Size = new System.Drawing.Size(147, 21);
            this.comboBox_protocoloBalanza1.TabIndex = 34;
            this.comboBox_protocoloBalanza1.SelectedIndexChanged += new System.EventHandler(this.comboBox_protocoloBalanza1_SelectedIndexChanged);
            // 
            // button_ConfigurarComB1
            // 
            this.button_ConfigurarComB1.Location = new System.Drawing.Point(245, 14);
            this.button_ConfigurarComB1.Name = "button_ConfigurarComB1";
            this.button_ConfigurarComB1.Size = new System.Drawing.Size(75, 23);
            this.button_ConfigurarComB1.TabIndex = 7;
            this.button_ConfigurarComB1.Text = "Configurar";
            this.button_ConfigurarComB1.UseVisualStyleBackColor = true;
            this.button_ConfigurarComB1.Click += new System.EventHandler(this.button_ConfigurarComB1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "PUERTO COM BALANZA";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_tiempoDetectEstableB1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox_maximaDispercionEstableB1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.textBox_PesoValidoMaximoB1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBox_PesoValidoMinimoB1);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox_maximoValorCeroB1);
            this.groupBox1.Location = new System.Drawing.Point(6, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 171);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PESAJE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(334, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "ms";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(222, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "TIEMPO DE DETECCIÓN DE ESTABILIDAD";
            // 
            // textBox_tiempoDetectEstableB1
            // 
            this.textBox_tiempoDetectEstableB1.Location = new System.Drawing.Point(257, 127);
            this.textBox_tiempoDetectEstableB1.Name = "textBox_tiempoDetectEstableB1";
            this.textBox_tiempoDetectEstableB1.Size = new System.Drawing.Size(71, 20);
            this.textBox_tiempoDetectEstableB1.TabIndex = 23;
            this.textBox_tiempoDetectEstableB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_tiempoDetectEstableB1.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_tiempoDetectEstableB1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericEdit_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "kg";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(169, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "MAXIMA DISPERCIÓN ESTABLE";
            // 
            // textBox_maximaDispercionEstableB1
            // 
            this.textBox_maximaDispercionEstableB1.Location = new System.Drawing.Point(257, 95);
            this.textBox_maximaDispercionEstableB1.Name = "textBox_maximaDispercionEstableB1";
            this.textBox_maximaDispercionEstableB1.Size = new System.Drawing.Size(71, 20);
            this.textBox_maximaDispercionEstableB1.TabIndex = 20;
            this.textBox_maximaDispercionEstableB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maximaDispercionEstableB1.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_maximaDispercionEstableB1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericFloatEdit_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "kg";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(334, 69);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "kg";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(230, 69);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "kg";
            // 
            // textBox_PesoValidoMaximoB1
            // 
            this.textBox_PesoValidoMaximoB1.Location = new System.Drawing.Point(257, 62);
            this.textBox_PesoValidoMaximoB1.Name = "textBox_PesoValidoMaximoB1";
            this.textBox_PesoValidoMaximoB1.Size = new System.Drawing.Size(71, 20);
            this.textBox_PesoValidoMaximoB1.TabIndex = 10;
            this.textBox_PesoValidoMaximoB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PesoValidoMaximoB1.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_PesoValidoMaximoB1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_PesoMaximoValido_Validating);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "RANGO PESO VALIDO";
            // 
            // textBox_PesoValidoMinimoB1
            // 
            this.textBox_PesoValidoMinimoB1.Location = new System.Drawing.Point(147, 62);
            this.textBox_PesoValidoMinimoB1.Name = "textBox_PesoValidoMinimoB1";
            this.textBox_PesoValidoMinimoB1.Size = new System.Drawing.Size(77, 20);
            this.textBox_PesoValidoMinimoB1.TabIndex = 9;
            this.textBox_PesoValidoMinimoB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PesoValidoMinimoB1.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_PesoValidoMinimoB1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_PesoValidoMinimo_Validating);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(218, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "MAXIMO VALOR ADMINTIDO PARA CERO";
            // 
            // textBox_maximoValorCeroB1
            // 
            this.textBox_maximoValorCeroB1.Location = new System.Drawing.Point(257, 29);
            this.textBox_maximoValorCeroB1.Name = "textBox_maximoValorCeroB1";
            this.textBox_maximoValorCeroB1.Size = new System.Drawing.Size(71, 20);
            this.textBox_maximoValorCeroB1.TabIndex = 8;
            this.textBox_maximoValorCeroB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maximoValorCeroB1.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_maximoValorCeroB1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_maximoValorCero_Validating);
            // 
            // textBox_PUERTO_COM_B1
            // 
            this.textBox_PUERTO_COM_B1.Location = new System.Drawing.Point(162, 15);
            this.textBox_PUERTO_COM_B1.Name = "textBox_PUERTO_COM_B1";
            this.textBox_PUERTO_COM_B1.Size = new System.Drawing.Size(77, 20);
            this.textBox_PUERTO_COM_B1.TabIndex = 6;
            this.textBox_PUERTO_COM_B1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PUERTO_COM_B1.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // tabPage_ConexionB2
            // 
            this.tabPage_ConexionB2.Controls.Add(this.label45);
            this.tabPage_ConexionB2.Controls.Add(this.checkBox_B2Habilitada);
            this.tabPage_ConexionB2.Controls.Add(this.textBox_nombreB2);
            this.tabPage_ConexionB2.Controls.Add(this.groupBoxB2);
            this.tabPage_ConexionB2.Location = new System.Drawing.Point(4, 40);
            this.tabPage_ConexionB2.Name = "tabPage_ConexionB2";
            this.tabPage_ConexionB2.Size = new System.Drawing.Size(846, 377);
            this.tabPage_ConexionB2.TabIndex = 6;
            this.tabPage_ConexionB2.Text = "Configuración Balanza 2";
            this.tabPage_ConexionB2.UseVisualStyleBackColor = true;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(167, 23);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(80, 13);
            this.label45.TabIndex = 32;
            this.label45.Text = "DESCRIPCIÓN";
            // 
            // checkBox_B2Habilitada
            // 
            this.checkBox_B2Habilitada.AutoSize = true;
            this.checkBox_B2Habilitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_B2Habilitada.Location = new System.Drawing.Point(8, 19);
            this.checkBox_B2Habilitada.Name = "checkBox_B2Habilitada";
            this.checkBox_B2Habilitada.Size = new System.Drawing.Size(100, 21);
            this.checkBox_B2Habilitada.TabIndex = 31;
            this.checkBox_B2Habilitada.Text = "Habilitada";
            this.checkBox_B2Habilitada.UseVisualStyleBackColor = true;
            this.checkBox_B2Habilitada.CheckedChanged += new System.EventHandler(this.checkBox_B2Habilitada_CheckedChanged);
            // 
            // textBox_nombreB2
            // 
            this.textBox_nombreB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombreB2.Location = new System.Drawing.Point(253, 19);
            this.textBox_nombreB2.MaxLength = 20;
            this.textBox_nombreB2.Name = "textBox_nombreB2";
            this.textBox_nombreB2.Size = new System.Drawing.Size(178, 21);
            this.textBox_nombreB2.TabIndex = 30;
            this.textBox_nombreB2.Text = "BALANZA 2";
            this.textBox_nombreB2.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // groupBoxB2
            // 
            this.groupBoxB2.Controls.Add(this.label_cantDecimalesBal2);
            this.groupBoxB2.Controls.Add(this.comboBox_cantDecimalesBalanza2);
            this.groupBoxB2.Controls.Add(this.label58);
            this.groupBoxB2.Controls.Add(this.comboBox_protocoloBalanza2);
            this.groupBoxB2.Controls.Add(this.label22);
            this.groupBoxB2.Controls.Add(this.button_ConfigurarComB2);
            this.groupBoxB2.Controls.Add(this.groupBox2);
            this.groupBoxB2.Controls.Add(this.textBox_PUERTO_COM_B2);
            this.groupBoxB2.Location = new System.Drawing.Point(8, 46);
            this.groupBoxB2.Name = "groupBoxB2";
            this.groupBoxB2.Size = new System.Drawing.Size(423, 280);
            this.groupBoxB2.TabIndex = 29;
            this.groupBoxB2.TabStop = false;
            // 
            // label_cantDecimalesBal2
            // 
            this.label_cantDecimalesBal2.AutoSize = true;
            this.label_cantDecimalesBal2.Location = new System.Drawing.Point(13, 78);
            this.label_cantDecimalesBal2.Name = "label_cantDecimalesBal2";
            this.label_cantDecimalesBal2.Size = new System.Drawing.Size(126, 13);
            this.label_cantDecimalesBal2.TabIndex = 39;
            this.label_cantDecimalesBal2.Text = "CANTIDAD DECIMALES";
            // 
            // comboBox_cantDecimalesBalanza2
            // 
            this.comboBox_cantDecimalesBalanza2.FormattingEnabled = true;
            this.comboBox_cantDecimalesBalanza2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox_cantDecimalesBalanza2.Location = new System.Drawing.Point(153, 75);
            this.comboBox_cantDecimalesBalanza2.Name = "comboBox_cantDecimalesBalanza2";
            this.comboBox_cantDecimalesBalanza2.Size = new System.Drawing.Size(42, 21);
            this.comboBox_cantDecimalesBalanza2.TabIndex = 38;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(67, 51);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(74, 13);
            this.label58.TabIndex = 37;
            this.label58.Text = "PROTOCOLO";
            // 
            // comboBox_protocoloBalanza2
            // 
            this.comboBox_protocoloBalanza2.FormattingEnabled = true;
            this.comboBox_protocoloBalanza2.Location = new System.Drawing.Point(155, 48);
            this.comboBox_protocoloBalanza2.Name = "comboBox_protocoloBalanza2";
            this.comboBox_protocoloBalanza2.Size = new System.Drawing.Size(147, 21);
            this.comboBox_protocoloBalanza2.TabIndex = 36;
            this.comboBox_protocoloBalanza2.SelectedIndexChanged += new System.EventHandler(this.comboBox_protocoloBalanza2_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(131, 13);
            this.label22.TabIndex = 28;
            this.label22.Text = "PUERTO COM BALANZA";
            // 
            // button_ConfigurarComB2
            // 
            this.button_ConfigurarComB2.Location = new System.Drawing.Point(238, 19);
            this.button_ConfigurarComB2.Name = "button_ConfigurarComB2";
            this.button_ConfigurarComB2.Size = new System.Drawing.Size(75, 23);
            this.button_ConfigurarComB2.TabIndex = 26;
            this.button_ConfigurarComB2.Text = "Configurar";
            this.button_ConfigurarComB2.UseVisualStyleBackColor = true;
            this.button_ConfigurarComB2.Click += new System.EventHandler(this.button_ConfigurarComB2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.textBox_tiempoDetectEstableB2);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.textBox_maximaDispercionEstableB2);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.textBox_PesoValidoMaximoB2);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.textBox_PesoValidoMinimoB2);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.textBox_maximoValorCeroB2);
            this.groupBox2.Location = new System.Drawing.Point(6, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 171);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PESAJE";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(334, 134);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(20, 13);
            this.label23.TabIndex = 25;
            this.label23.Text = "ms";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 130);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(222, 13);
            this.label24.TabIndex = 24;
            this.label24.Text = "TIEMPO DE DETECCIÓN DE ESTABILIDAD";
            // 
            // textBox_tiempoDetectEstableB2
            // 
            this.textBox_tiempoDetectEstableB2.Location = new System.Drawing.Point(257, 127);
            this.textBox_tiempoDetectEstableB2.Name = "textBox_tiempoDetectEstableB2";
            this.textBox_tiempoDetectEstableB2.Size = new System.Drawing.Size(71, 20);
            this.textBox_tiempoDetectEstableB2.TabIndex = 23;
            this.textBox_tiempoDetectEstableB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_tiempoDetectEstableB2.DoubleClick += new System.EventHandler(this.textBox_valorNumericTouch_DoubleClick);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(334, 102);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(19, 13);
            this.label25.TabIndex = 22;
            this.label25.Text = "kg";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 98);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(169, 13);
            this.label26.TabIndex = 21;
            this.label26.Text = "MAXIMA DISPERCIÓN ESTABLE";
            // 
            // textBox_maximaDispercionEstableB2
            // 
            this.textBox_maximaDispercionEstableB2.Location = new System.Drawing.Point(257, 95);
            this.textBox_maximaDispercionEstableB2.Name = "textBox_maximaDispercionEstableB2";
            this.textBox_maximaDispercionEstableB2.Size = new System.Drawing.Size(71, 20);
            this.textBox_maximaDispercionEstableB2.TabIndex = 20;
            this.textBox_maximaDispercionEstableB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maximaDispercionEstableB2.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(334, 36);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(19, 13);
            this.label28.TabIndex = 18;
            this.label28.Text = "kg";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(334, 69);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(19, 13);
            this.label29.TabIndex = 17;
            this.label29.Text = "kg";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(230, 69);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(19, 13);
            this.label30.TabIndex = 16;
            this.label30.Text = "kg";
            // 
            // textBox_PesoValidoMaximoB2
            // 
            this.textBox_PesoValidoMaximoB2.Location = new System.Drawing.Point(257, 62);
            this.textBox_PesoValidoMaximoB2.Name = "textBox_PesoValidoMaximoB2";
            this.textBox_PesoValidoMaximoB2.Size = new System.Drawing.Size(71, 20);
            this.textBox_PesoValidoMaximoB2.TabIndex = 10;
            this.textBox_PesoValidoMaximoB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PesoValidoMaximoB2.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_PesoValidoMaximoB2.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_PesoMaximoValido_Validating);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 62);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(120, 13);
            this.label32.TabIndex = 10;
            this.label32.Text = "RANGO PESO VALIDO";
            // 
            // textBox_PesoValidoMinimoB2
            // 
            this.textBox_PesoValidoMinimoB2.Location = new System.Drawing.Point(147, 62);
            this.textBox_PesoValidoMinimoB2.Name = "textBox_PesoValidoMinimoB2";
            this.textBox_PesoValidoMinimoB2.Size = new System.Drawing.Size(77, 20);
            this.textBox_PesoValidoMinimoB2.TabIndex = 9;
            this.textBox_PesoValidoMinimoB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PesoValidoMinimoB2.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_PesoValidoMinimoB2.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_PesoValidoMinimo_Validating);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 36);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(218, 13);
            this.label33.TabIndex = 8;
            this.label33.Text = "MAXIMO VALOR ADMINTIDO PARA CERO";
            // 
            // textBox_maximoValorCeroB2
            // 
            this.textBox_maximoValorCeroB2.Location = new System.Drawing.Point(257, 29);
            this.textBox_maximoValorCeroB2.Name = "textBox_maximoValorCeroB2";
            this.textBox_maximoValorCeroB2.Size = new System.Drawing.Size(71, 20);
            this.textBox_maximoValorCeroB2.TabIndex = 8;
            this.textBox_maximoValorCeroB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maximoValorCeroB2.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_maximoValorCeroB2.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_maximoValorCero_Validating);
            // 
            // textBox_PUERTO_COM_B2
            // 
            this.textBox_PUERTO_COM_B2.Location = new System.Drawing.Point(155, 20);
            this.textBox_PUERTO_COM_B2.Name = "textBox_PUERTO_COM_B2";
            this.textBox_PUERTO_COM_B2.Size = new System.Drawing.Size(77, 20);
            this.textBox_PUERTO_COM_B2.TabIndex = 25;
            this.textBox_PUERTO_COM_B2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PUERTO_COM_B2.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // tabPage_ConexionB3
            // 
            this.tabPage_ConexionB3.Controls.Add(this.label46);
            this.tabPage_ConexionB3.Controls.Add(this.checkBox_B3Habilitada);
            this.tabPage_ConexionB3.Controls.Add(this.textBox_nombreB3);
            this.tabPage_ConexionB3.Controls.Add(this.groupBoxB3);
            this.tabPage_ConexionB3.Location = new System.Drawing.Point(4, 40);
            this.tabPage_ConexionB3.Name = "tabPage_ConexionB3";
            this.tabPage_ConexionB3.Size = new System.Drawing.Size(846, 377);
            this.tabPage_ConexionB3.TabIndex = 7;
            this.tabPage_ConexionB3.Text = "Configuración Balanza 3";
            this.tabPage_ConexionB3.UseVisualStyleBackColor = true;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(166, 23);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(80, 13);
            this.label46.TabIndex = 36;
            this.label46.Text = "DESCRIPCIÓN";
            // 
            // checkBox_B3Habilitada
            // 
            this.checkBox_B3Habilitada.AutoSize = true;
            this.checkBox_B3Habilitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_B3Habilitada.Location = new System.Drawing.Point(8, 19);
            this.checkBox_B3Habilitada.Name = "checkBox_B3Habilitada";
            this.checkBox_B3Habilitada.Size = new System.Drawing.Size(100, 21);
            this.checkBox_B3Habilitada.TabIndex = 35;
            this.checkBox_B3Habilitada.Text = "Habilitada";
            this.checkBox_B3Habilitada.UseVisualStyleBackColor = true;
            this.checkBox_B3Habilitada.CheckedChanged += new System.EventHandler(this.checkBox_B3Habilitada_CheckedChanged);
            // 
            // textBox_nombreB3
            // 
            this.textBox_nombreB3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_nombreB3.Location = new System.Drawing.Point(252, 19);
            this.textBox_nombreB3.MaxLength = 20;
            this.textBox_nombreB3.Name = "textBox_nombreB3";
            this.textBox_nombreB3.Size = new System.Drawing.Size(178, 21);
            this.textBox_nombreB3.TabIndex = 34;
            this.textBox_nombreB3.Text = "BALANZA 3";
            this.textBox_nombreB3.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // groupBoxB3
            // 
            this.groupBoxB3.Controls.Add(this.label_cantDecimalesBal3);
            this.groupBoxB3.Controls.Add(this.comboBox_cantDecimalesBalanza3);
            this.groupBoxB3.Controls.Add(this.label60);
            this.groupBoxB3.Controls.Add(this.comboBox_protocoloBalanza3);
            this.groupBoxB3.Controls.Add(this.label34);
            this.groupBoxB3.Controls.Add(this.button_ConfigurarComB3);
            this.groupBoxB3.Controls.Add(this.groupBox3);
            this.groupBoxB3.Controls.Add(this.textBox_PUERTO_COM_B3);
            this.groupBoxB3.Location = new System.Drawing.Point(3, 46);
            this.groupBoxB3.Name = "groupBoxB3";
            this.groupBoxB3.Size = new System.Drawing.Size(427, 280);
            this.groupBoxB3.TabIndex = 33;
            this.groupBoxB3.TabStop = false;
            // 
            // label_cantDecimalesBal3
            // 
            this.label_cantDecimalesBal3.AutoSize = true;
            this.label_cantDecimalesBal3.Location = new System.Drawing.Point(16, 76);
            this.label_cantDecimalesBal3.Name = "label_cantDecimalesBal3";
            this.label_cantDecimalesBal3.Size = new System.Drawing.Size(126, 13);
            this.label_cantDecimalesBal3.TabIndex = 43;
            this.label_cantDecimalesBal3.Text = "CANTIDAD DECIMALES";
            // 
            // comboBox_cantDecimalesBalanza3
            // 
            this.comboBox_cantDecimalesBalanza3.FormattingEnabled = true;
            this.comboBox_cantDecimalesBalanza3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox_cantDecimalesBalanza3.Location = new System.Drawing.Point(156, 73);
            this.comboBox_cantDecimalesBalanza3.Name = "comboBox_cantDecimalesBalanza3";
            this.comboBox_cantDecimalesBalanza3.Size = new System.Drawing.Size(42, 21);
            this.comboBox_cantDecimalesBalanza3.TabIndex = 42;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(70, 49);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(74, 13);
            this.label60.TabIndex = 41;
            this.label60.Text = "PROTOCOLO";
            // 
            // comboBox_protocoloBalanza3
            // 
            this.comboBox_protocoloBalanza3.FormattingEnabled = true;
            this.comboBox_protocoloBalanza3.Location = new System.Drawing.Point(158, 46);
            this.comboBox_protocoloBalanza3.Name = "comboBox_protocoloBalanza3";
            this.comboBox_protocoloBalanza3.Size = new System.Drawing.Size(147, 21);
            this.comboBox_protocoloBalanza3.TabIndex = 40;
            this.comboBox_protocoloBalanza3.SelectedIndexChanged += new System.EventHandler(this.comboBox_protocoloBalanza3_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(8, 22);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(131, 13);
            this.label34.TabIndex = 32;
            this.label34.Text = "PUERTO COM BALANZA";
            // 
            // button_ConfigurarComB3
            // 
            this.button_ConfigurarComB3.Location = new System.Drawing.Point(241, 17);
            this.button_ConfigurarComB3.Name = "button_ConfigurarComB3";
            this.button_ConfigurarComB3.Size = new System.Drawing.Size(75, 23);
            this.button_ConfigurarComB3.TabIndex = 30;
            this.button_ConfigurarComB3.Text = "Configurar";
            this.button_ConfigurarComB3.UseVisualStyleBackColor = true;
            this.button_ConfigurarComB3.Click += new System.EventHandler(this.button_ConfigurarComB3_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.textBox_tiempoDetectEstableB3);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.textBox_maximaDispercionEstableB3);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.textBox_PesoValidoMaximoB3);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.textBox_PesoValidoMinimoB3);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.textBox_maximoValorCeroB3);
            this.groupBox3.Location = new System.Drawing.Point(11, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(386, 170);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PESAJE";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(334, 134);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(20, 13);
            this.label35.TabIndex = 25;
            this.label35.Text = "ms";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 130);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(222, 13);
            this.label36.TabIndex = 24;
            this.label36.Text = "TIEMPO DE DETECCIÓN DE ESTABILIDAD";
            // 
            // textBox_tiempoDetectEstableB3
            // 
            this.textBox_tiempoDetectEstableB3.Location = new System.Drawing.Point(257, 127);
            this.textBox_tiempoDetectEstableB3.Name = "textBox_tiempoDetectEstableB3";
            this.textBox_tiempoDetectEstableB3.Size = new System.Drawing.Size(71, 20);
            this.textBox_tiempoDetectEstableB3.TabIndex = 23;
            this.textBox_tiempoDetectEstableB3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_tiempoDetectEstableB3.DoubleClick += new System.EventHandler(this.textBox_valorNumericTouch_DoubleClick);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(334, 102);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(19, 13);
            this.label37.TabIndex = 22;
            this.label37.Text = "kg";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 98);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(169, 13);
            this.label38.TabIndex = 21;
            this.label38.Text = "MAXIMA DISPERCIÓN ESTABLE";
            // 
            // textBox_maximaDispercionEstableB3
            // 
            this.textBox_maximaDispercionEstableB3.Location = new System.Drawing.Point(257, 95);
            this.textBox_maximaDispercionEstableB3.Name = "textBox_maximaDispercionEstableB3";
            this.textBox_maximaDispercionEstableB3.Size = new System.Drawing.Size(71, 20);
            this.textBox_maximaDispercionEstableB3.TabIndex = 20;
            this.textBox_maximaDispercionEstableB3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maximaDispercionEstableB3.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(334, 36);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(19, 13);
            this.label39.TabIndex = 18;
            this.label39.Text = "kg";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(334, 69);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(19, 13);
            this.label40.TabIndex = 17;
            this.label40.Text = "kg";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(230, 69);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(19, 13);
            this.label41.TabIndex = 16;
            this.label41.Text = "kg";
            // 
            // textBox_PesoValidoMaximoB3
            // 
            this.textBox_PesoValidoMaximoB3.Location = new System.Drawing.Point(257, 62);
            this.textBox_PesoValidoMaximoB3.Name = "textBox_PesoValidoMaximoB3";
            this.textBox_PesoValidoMaximoB3.Size = new System.Drawing.Size(71, 20);
            this.textBox_PesoValidoMaximoB3.TabIndex = 10;
            this.textBox_PesoValidoMaximoB3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PesoValidoMaximoB3.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_PesoValidoMaximoB3.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_PesoMaximoValido_Validating);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 62);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(120, 13);
            this.label42.TabIndex = 10;
            this.label42.Text = "RANGO PESO VALIDO";
            // 
            // textBox_PesoValidoMinimoB3
            // 
            this.textBox_PesoValidoMinimoB3.Location = new System.Drawing.Point(147, 62);
            this.textBox_PesoValidoMinimoB3.Name = "textBox_PesoValidoMinimoB3";
            this.textBox_PesoValidoMinimoB3.Size = new System.Drawing.Size(77, 20);
            this.textBox_PesoValidoMinimoB3.TabIndex = 9;
            this.textBox_PesoValidoMinimoB3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_PesoValidoMinimoB3.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_PesoValidoMinimoB3.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_PesoValidoMinimo_Validating);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 36);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(218, 13);
            this.label43.TabIndex = 8;
            this.label43.Text = "MAXIMO VALOR ADMINTIDO PARA CERO";
            // 
            // textBox_maximoValorCeroB3
            // 
            this.textBox_maximoValorCeroB3.Location = new System.Drawing.Point(257, 29);
            this.textBox_maximoValorCeroB3.Name = "textBox_maximoValorCeroB3";
            this.textBox_maximoValorCeroB3.Size = new System.Drawing.Size(71, 20);
            this.textBox_maximoValorCeroB3.TabIndex = 8;
            this.textBox_maximoValorCeroB3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maximoValorCeroB3.DoubleClick += new System.EventHandler(this.textBox_valorFloatTouch_DoubleClick);
            this.textBox_maximoValorCeroB3.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_maximoValorCero_Validating);
            // 
            // textBox_PUERTO_COM_B3
            // 
            this.textBox_PUERTO_COM_B3.Location = new System.Drawing.Point(158, 18);
            this.textBox_PUERTO_COM_B3.Name = "textBox_PUERTO_COM_B3";
            this.textBox_PUERTO_COM_B3.Size = new System.Drawing.Size(77, 20);
            this.textBox_PUERTO_COM_B3.TabIndex = 29;
            this.textBox_PUERTO_COM_B3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage_ConexionImpresor
            // 
            this.tabPage_ConexionImpresor.Controls.Add(this.label55);
            this.tabPage_ConexionImpresor.Controls.Add(this.comboBox_EncodingPrinter);
            this.tabPage_ConexionImpresor.Controls.Add(this.button_buscarImpresora);
            this.tabPage_ConexionImpresor.Controls.Add(this.groupBox_Etiqueta);
            this.tabPage_ConexionImpresor.Controls.Add(this.label19);
            this.tabPage_ConexionImpresor.Controls.Add(this.textBox_NOMBRE_IMPRESORA);
            this.tabPage_ConexionImpresor.Location = new System.Drawing.Point(4, 40);
            this.tabPage_ConexionImpresor.Name = "tabPage_ConexionImpresor";
            this.tabPage_ConexionImpresor.Size = new System.Drawing.Size(846, 377);
            this.tabPage_ConexionImpresor.TabIndex = 5;
            this.tabPage_ConexionImpresor.Text = "Conexión Impresor";
            this.tabPage_ConexionImpresor.UseVisualStyleBackColor = true;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(484, 21);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(130, 13);
            this.label55.TabIndex = 48;
            this.label55.Text = "MAPA DE CARACTERES";
            // 
            // comboBox_EncodingPrinter
            // 
            this.comboBox_EncodingPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_EncodingPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_EncodingPrinter.FormattingEnabled = true;
            this.comboBox_EncodingPrinter.Location = new System.Drawing.Point(631, 16);
            this.comboBox_EncodingPrinter.Name = "comboBox_EncodingPrinter";
            this.comboBox_EncodingPrinter.Size = new System.Drawing.Size(153, 23);
            this.comboBox_EncodingPrinter.TabIndex = 47;
            // 
            // button_buscarImpresora
            // 
            this.button_buscarImpresora.Location = new System.Drawing.Point(442, 14);
            this.button_buscarImpresora.Name = "button_buscarImpresora";
            this.button_buscarImpresora.Size = new System.Drawing.Size(26, 23);
            this.button_buscarImpresora.TabIndex = 46;
            this.button_buscarImpresora.Text = "...";
            this.button_buscarImpresora.UseVisualStyleBackColor = true;
            this.button_buscarImpresora.Click += new System.EventHandler(this.button_buscarImpresora_Click);
            // 
            // groupBox_Etiqueta
            // 
            this.groupBox_Etiqueta.Controls.Add(this.textBox_nombreFormatoEtiquetaPedido);
            this.groupBox_Etiqueta.Controls.Add(this.label47);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_LineaDeTextoInferiorDeEtiqueta);
            this.groupBox_Etiqueta.Controls.Add(this.label54);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_LineaDeTextoSuperiorDeEtiqueta);
            this.groupBox_Etiqueta.Controls.Add(this.label53);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_nombreFormatoEtiquetaCaja);
            this.groupBox_Etiqueta.Controls.Add(this.label50);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_nombreFormatoEtiquetaContenedor);
            this.groupBox_Etiqueta.Controls.Add(this.label49);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS);
            this.groupBox_Etiqueta.Controls.Add(this.label27);
            this.groupBox_Etiqueta.Controls.Add(this.label13);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_cantEtiquetas);
            this.groupBox_Etiqueta.Controls.Add(this.button_AbrirBuscadorArchivosFormatosEtiquetas);
            this.groupBox_Etiqueta.Controls.Add(this.textBox_nombreFormatoEtiquetaProducto);
            this.groupBox_Etiqueta.Controls.Add(this.label12);
            this.groupBox_Etiqueta.Location = new System.Drawing.Point(11, 43);
            this.groupBox_Etiqueta.Name = "groupBox_Etiqueta";
            this.groupBox_Etiqueta.Size = new System.Drawing.Size(677, 331);
            this.groupBox_Etiqueta.TabIndex = 45;
            this.groupBox_Etiqueta.TabStop = false;
            this.groupBox_Etiqueta.Text = "Etiqueta";
            // 
            // textBox_nombreFormatoEtiquetaPedido
            // 
            this.textBox_nombreFormatoEtiquetaPedido.Location = new System.Drawing.Point(296, 152);
            this.textBox_nombreFormatoEtiquetaPedido.Name = "textBox_nombreFormatoEtiquetaPedido";
            this.textBox_nombreFormatoEtiquetaPedido.Size = new System.Drawing.Size(127, 20);
            this.textBox_nombreFormatoEtiquetaPedido.TabIndex = 54;
            this.textBox_nombreFormatoEtiquetaPedido.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 155);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(247, 13);
            this.label47.TabIndex = 53;
            this.label47.Text = "NOMBRE DE FORMATO DE ETIQUETA PEDIDO";
            // 
            // textBox_LineaDeTextoInferiorDeEtiqueta
            // 
            this.textBox_LineaDeTextoInferiorDeEtiqueta.Location = new System.Drawing.Point(9, 288);
            this.textBox_LineaDeTextoInferiorDeEtiqueta.MaxLength = 90;
            this.textBox_LineaDeTextoInferiorDeEtiqueta.Name = "textBox_LineaDeTextoInferiorDeEtiqueta";
            this.textBox_LineaDeTextoInferiorDeEtiqueta.Size = new System.Drawing.Size(632, 20);
            this.textBox_LineaDeTextoInferiorDeEtiqueta.TabIndex = 52;
            this.textBox_LineaDeTextoInferiorDeEtiqueta.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 268);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(240, 13);
            this.label54.TabIndex = 51;
            this.label54.Text = "LINEA DE TEXTO INFERIOR DE LA ETIQUETA";
            // 
            // textBox_LineaDeTextoSuperiorDeEtiqueta
            // 
            this.textBox_LineaDeTextoSuperiorDeEtiqueta.Location = new System.Drawing.Point(9, 231);
            this.textBox_LineaDeTextoSuperiorDeEtiqueta.Name = "textBox_LineaDeTextoSuperiorDeEtiqueta";
            this.textBox_LineaDeTextoSuperiorDeEtiqueta.Size = new System.Drawing.Size(632, 20);
            this.textBox_LineaDeTextoSuperiorDeEtiqueta.TabIndex = 50;
            this.textBox_LineaDeTextoSuperiorDeEtiqueta.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(6, 212);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(245, 13);
            this.label53.TabIndex = 49;
            this.label53.Text = "LINEA DE TEXTO SUPERIOR DE LA ETIQUETA";
            // 
            // textBox_nombreFormatoEtiquetaCaja
            // 
            this.textBox_nombreFormatoEtiquetaCaja.Location = new System.Drawing.Point(296, 126);
            this.textBox_nombreFormatoEtiquetaCaja.Name = "textBox_nombreFormatoEtiquetaCaja";
            this.textBox_nombreFormatoEtiquetaCaja.Size = new System.Drawing.Size(127, 20);
            this.textBox_nombreFormatoEtiquetaCaja.TabIndex = 48;
            this.textBox_nombreFormatoEtiquetaCaja.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 129);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(232, 13);
            this.label50.TabIndex = 47;
            this.label50.Text = "NOMBRE DE FORMATO DE ETIQUETA CAJA";
            // 
            // textBox_nombreFormatoEtiquetaContenedor
            // 
            this.textBox_nombreFormatoEtiquetaContenedor.Location = new System.Drawing.Point(296, 101);
            this.textBox_nombreFormatoEtiquetaContenedor.Name = "textBox_nombreFormatoEtiquetaContenedor";
            this.textBox_nombreFormatoEtiquetaContenedor.Size = new System.Drawing.Size(127, 20);
            this.textBox_nombreFormatoEtiquetaContenedor.TabIndex = 46;
            this.textBox_nombreFormatoEtiquetaContenedor.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 104);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(282, 13);
            this.label49.TabIndex = 45;
            this.label49.Text = "NOMBRE DE FORMATO DE ETIQUETA CONTENEDOR";
            // 
            // textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS
            // 
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Location = new System.Drawing.Point(6, 44);
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.MaxLength = 90;
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Name = "textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS";
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Size = new System.Drawing.Size(525, 20);
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.TabIndex = 40;
            this.textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 186);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(216, 13);
            this.label27.TabIndex = 44;
            this.label27.Text = "CANTIDAD DE ETIQUETAS POR PESADA";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(267, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "RUTA AL ARCHIVO DE FORMATOS DE ETIQUETAS";
            // 
            // textBox_cantEtiquetas
            // 
            this.textBox_cantEtiquetas.Location = new System.Drawing.Point(294, 183);
            this.textBox_cantEtiquetas.Name = "textBox_cantEtiquetas";
            this.textBox_cantEtiquetas.Size = new System.Drawing.Size(29, 20);
            this.textBox_cantEtiquetas.TabIndex = 43;
            this.textBox_cantEtiquetas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_cantEtiquetas.DoubleClick += new System.EventHandler(this.textBox_valorNumericTouch_DoubleClick);
            this.textBox_cantEtiquetas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericEdit_KeyPress);
            this.textBox_cantEtiquetas.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_cantEtiquetas_Validating);
            // 
            // button_AbrirBuscadorArchivosFormatosEtiquetas
            // 
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.Location = new System.Drawing.Point(537, 42);
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.Name = "button_AbrirBuscadorArchivosFormatosEtiquetas";
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.Size = new System.Drawing.Size(26, 23);
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.TabIndex = 41;
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.Text = "...";
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.UseVisualStyleBackColor = true;
            this.button_AbrirBuscadorArchivosFormatosEtiquetas.Click += new System.EventHandler(this.button_AbrirBuscadorArchivosFormatosEtiquetas_Click);
            // 
            // textBox_nombreFormatoEtiquetaProducto
            // 
            this.textBox_nombreFormatoEtiquetaProducto.Location = new System.Drawing.Point(296, 77);
            this.textBox_nombreFormatoEtiquetaProducto.Name = "textBox_nombreFormatoEtiquetaProducto";
            this.textBox_nombreFormatoEtiquetaProducto.Size = new System.Drawing.Size(127, 20);
            this.textBox_nombreFormatoEtiquetaProducto.TabIndex = 42;
            this.textBox_nombreFormatoEtiquetaProducto.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(267, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "NOMBRE DE FORMATO DE ETIQUETA PRODUCTO";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(155, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "NOMBRE DE LA IMPRESORA";
            // 
            // textBox_NOMBRE_IMPRESORA
            // 
            this.textBox_NOMBRE_IMPRESORA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_NOMBRE_IMPRESORA.Location = new System.Drawing.Point(198, 16);
            this.textBox_NOMBRE_IMPRESORA.Name = "textBox_NOMBRE_IMPRESORA";
            this.textBox_NOMBRE_IMPRESORA.ReadOnly = true;
            this.textBox_NOMBRE_IMPRESORA.Size = new System.Drawing.Size(236, 21);
            this.textBox_NOMBRE_IMPRESORA.TabIndex = 35;
            this.textBox_NOMBRE_IMPRESORA.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // tabPage_scanner
            // 
            this.tabPage_scanner.Controls.Add(this.label21);
            this.tabPage_scanner.Controls.Add(this.comboBox_tipoInterfaceHostScanner);
            this.tabPage_scanner.Controls.Add(this.checkBox_permiteSimularLecturaScanner);
            this.tabPage_scanner.Controls.Add(this.label20);
            this.tabPage_scanner.Controls.Add(this.textBox_modeloEscannerZebra);
            this.tabPage_scanner.Location = new System.Drawing.Point(4, 40);
            this.tabPage_scanner.Name = "tabPage_scanner";
            this.tabPage_scanner.Size = new System.Drawing.Size(846, 377);
            this.tabPage_scanner.TabIndex = 8;
            this.tabPage_scanner.Text = "Escaner";
            this.tabPage_scanner.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(121, 13);
            this.label21.TabIndex = 50;
            this.label21.Text = "TIPO INTERFAZ HOST";
            // 
            // comboBox_tipoInterfaceHostScanner
            // 
            this.comboBox_tipoInterfaceHostScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tipoInterfaceHostScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_tipoInterfaceHostScanner.FormattingEnabled = true;
            this.comboBox_tipoInterfaceHostScanner.Location = new System.Drawing.Point(200, 10);
            this.comboBox_tipoInterfaceHostScanner.Name = "comboBox_tipoInterfaceHostScanner";
            this.comboBox_tipoInterfaceHostScanner.Size = new System.Drawing.Size(236, 23);
            this.comboBox_tipoInterfaceHostScanner.TabIndex = 49;
            this.comboBox_tipoInterfaceHostScanner.SelectedIndexChanged += new System.EventHandler(this.comboBox_tipoInterfaceHostScanner_SelectedIndexChanged);
            // 
            // checkBox_permiteSimularLecturaScanner
            // 
            this.checkBox_permiteSimularLecturaScanner.AutoSize = true;
            this.checkBox_permiteSimularLecturaScanner.Location = new System.Drawing.Point(13, 74);
            this.checkBox_permiteSimularLecturaScanner.Name = "checkBox_permiteSimularLecturaScanner";
            this.checkBox_permiteSimularLecturaScanner.Size = new System.Drawing.Size(294, 17);
            this.checkBox_permiteSimularLecturaScanner.TabIndex = 39;
            this.checkBox_permiteSimularLecturaScanner.Text = "Permitir simular la lectura del scanner por edición manual.";
            this.checkBox_permiteSimularLecturaScanner.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 46);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(146, 13);
            this.label20.TabIndex = 38;
            this.label20.Text = "MODELO ESCANER ZEBRA";
            // 
            // textBox_modeloEscannerZebra
            // 
            this.textBox_modeloEscannerZebra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_modeloEscannerZebra.Location = new System.Drawing.Point(200, 43);
            this.textBox_modeloEscannerZebra.Name = "textBox_modeloEscannerZebra";
            this.textBox_modeloEscannerZebra.Size = new System.Drawing.Size(236, 21);
            this.textBox_modeloEscannerZebra.TabIndex = 37;
            this.textBox_modeloEscannerZebra.DoubleClick += new System.EventHandler(this.textBox_modeloEscannerZebra_DoubleClick);
            // 
            // tabPage_ingresoAPlanta
            // 
            this.tabPage_ingresoAPlanta.Controls.Add(this.checkBox_mantenerUltimaTipificacionEntrePesajes);
            this.tabPage_ingresoAPlanta.Controls.Add(this.checkBox_mantenerUltimaTropaEntrePesadas);
            this.tabPage_ingresoAPlanta.Controls.Add(this.label59);
            this.tabPage_ingresoAPlanta.Controls.Add(this.label61);
            this.tabPage_ingresoAPlanta.Controls.Add(this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta);
            this.tabPage_ingresoAPlanta.Controls.Add(this.checkBox_pesoUnidadesRemitidoObligatorio);
            this.tabPage_ingresoAPlanta.Location = new System.Drawing.Point(4, 40);
            this.tabPage_ingresoAPlanta.Name = "tabPage_ingresoAPlanta";
            this.tabPage_ingresoAPlanta.Size = new System.Drawing.Size(846, 377);
            this.tabPage_ingresoAPlanta.TabIndex = 10;
            this.tabPage_ingresoAPlanta.Text = "Ingreso a Planta";
            this.tabPage_ingresoAPlanta.UseVisualStyleBackColor = true;
            // 
            // checkBox_mantenerUltimaTipificacionEntrePesajes
            // 
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.AutoSize = true;
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.Location = new System.Drawing.Point(8, 99);
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.Name = "checkBox_mantenerUltimaTipificacionEntrePesajes";
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.Size = new System.Drawing.Size(288, 17);
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.TabIndex = 24;
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.Text = "Mantener ultima selección de Tipificacion entre Pesajes";
            this.checkBox_mantenerUltimaTipificacionEntrePesajes.UseVisualStyleBackColor = true;
            // 
            // checkBox_mantenerUltimaTropaEntrePesadas
            // 
            this.checkBox_mantenerUltimaTropaEntrePesadas.AutoSize = true;
            this.checkBox_mantenerUltimaTropaEntrePesadas.Location = new System.Drawing.Point(8, 76);
            this.checkBox_mantenerUltimaTropaEntrePesadas.Name = "checkBox_mantenerUltimaTropaEntrePesadas";
            this.checkBox_mantenerUltimaTropaEntrePesadas.Size = new System.Drawing.Size(251, 17);
            this.checkBox_mantenerUltimaTropaEntrePesadas.TabIndex = 23;
            this.checkBox_mantenerUltimaTropaEntrePesadas.Text = "Mantener ultima edición de Tropa entre Pesajes";
            this.checkBox_mantenerUltimaTropaEntrePesadas.UseVisualStyleBackColor = true;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(91, 44);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(15, 13);
            this.label59.TabIndex = 22;
            this.label59.Text = "%";
            // 
            // label61
            // 
            this.label61.Location = new System.Drawing.Point(112, 44);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(334, 17);
            this.label61.TabIndex = 21;
            this.label61.Text = "Tolerancia admitida entre el Peso Predefinido y el Peso de la Balanza";
            // 
            // textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta
            // 
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.Location = new System.Drawing.Point(8, 41);
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.Name = "textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta";
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.Size = new System.Drawing.Size(77, 20);
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.TabIndex = 20;
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.DoubleClick += new System.EventHandler(this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta_DoubleClick);
            this.textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericFloatEdit_KeyPress);
            // 
            // checkBox_pesoUnidadesRemitidoObligatorio
            // 
            this.checkBox_pesoUnidadesRemitidoObligatorio.AutoSize = true;
            this.checkBox_pesoUnidadesRemitidoObligatorio.Location = new System.Drawing.Point(8, 18);
            this.checkBox_pesoUnidadesRemitidoObligatorio.Name = "checkBox_pesoUnidadesRemitidoObligatorio";
            this.checkBox_pesoUnidadesRemitidoObligatorio.Size = new System.Drawing.Size(208, 17);
            this.checkBox_pesoUnidadesRemitidoObligatorio.TabIndex = 4;
            this.checkBox_pesoUnidadesRemitidoObligatorio.Text = "Peso y Unidades Remitidas Obligatorio";
            this.checkBox_pesoUnidadesRemitidoObligatorio.UseVisualStyleBackColor = true;
            // 
            // tabPage_pesajeDeCajas
            // 
            this.tabPage_pesajeDeCajas.Controls.Add(this.label51);
            this.tabPage_pesajeDeCajas.Controls.Add(this.label52);
            this.tabPage_pesajeDeCajas.Controls.Add(this.textBox_toleranciaPesajeCaja);
            this.tabPage_pesajeDeCajas.Location = new System.Drawing.Point(4, 40);
            this.tabPage_pesajeDeCajas.Name = "tabPage_pesajeDeCajas";
            this.tabPage_pesajeDeCajas.Size = new System.Drawing.Size(846, 377);
            this.tabPage_pesajeDeCajas.TabIndex = 9;
            this.tabPage_pesajeDeCajas.Text = "Pesaje Cajas";
            this.tabPage_pesajeDeCajas.UseVisualStyleBackColor = true;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(91, 21);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(15, 13);
            this.label51.TabIndex = 19;
            this.label51.Text = "%";
            // 
            // label52
            // 
            this.label52.Location = new System.Drawing.Point(112, 21);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(340, 20);
            this.label52.TabIndex = 18;
            this.label52.Text = "Tolerancia Admitida entre Peso Neto de Caja y su Contenido";
            // 
            // textBox_toleranciaPesajeCaja
            // 
            this.textBox_toleranciaPesajeCaja.Location = new System.Drawing.Point(8, 18);
            this.textBox_toleranciaPesajeCaja.Name = "textBox_toleranciaPesajeCaja";
            this.textBox_toleranciaPesajeCaja.Size = new System.Drawing.Size(77, 20);
            this.textBox_toleranciaPesajeCaja.TabIndex = 17;
            this.textBox_toleranciaPesajeCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_toleranciaPesajeCaja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numericFloatEdit_KeyPress);
            this.textBox_toleranciaPesajeCaja.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_toleranciaPesajeCaja_MouseDoubleClick);
            // 
            // tabPage_Egresos
            // 
            this.tabPage_Egresos.Controls.Add(this.checkBox_permitirColectarMasUnidadesQueLasPedidas);
            this.tabPage_Egresos.Location = new System.Drawing.Point(4, 40);
            this.tabPage_Egresos.Name = "tabPage_Egresos";
            this.tabPage_Egresos.Size = new System.Drawing.Size(846, 377);
            this.tabPage_Egresos.TabIndex = 11;
            this.tabPage_Egresos.Text = "Egresos";
            this.tabPage_Egresos.UseVisualStyleBackColor = true;
            // 
            // checkBox_permitirColectarMasUnidadesQueLasPedidas
            // 
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.AutoSize = true;
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.Location = new System.Drawing.Point(17, 15);
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.Name = "checkBox_permitirColectarMasUnidadesQueLasPedidas";
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.Size = new System.Drawing.Size(249, 17);
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.TabIndex = 24;
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.Text = "Permitir colectar mas unidades que las pedidas.";
            this.checkBox_permitirColectarMasUnidadesQueLasPedidas.UseVisualStyleBackColor = true;
            // 
            // tabPage_Empresa
            // 
            this.tabPage_Empresa.Controls.Add(this.button_seleccionArchivoLogoEmpresa);
            this.tabPage_Empresa.Controls.Add(this.textBox_pathLogoEmpresa);
            this.tabPage_Empresa.Controls.Add(this.label31);
            this.tabPage_Empresa.Controls.Add(this.label64);
            this.tabPage_Empresa.Controls.Add(this.textBox_nombreEmpresa);
            this.tabPage_Empresa.Location = new System.Drawing.Point(4, 40);
            this.tabPage_Empresa.Name = "tabPage_Empresa";
            this.tabPage_Empresa.Size = new System.Drawing.Size(846, 377);
            this.tabPage_Empresa.TabIndex = 4;
            this.tabPage_Empresa.Text = "Empresa";
            this.tabPage_Empresa.UseVisualStyleBackColor = true;
            // 
            // button_seleccionArchivoLogoEmpresa
            // 
            this.button_seleccionArchivoLogoEmpresa.Location = new System.Drawing.Point(490, 76);
            this.button_seleccionArchivoLogoEmpresa.Name = "button_seleccionArchivoLogoEmpresa";
            this.button_seleccionArchivoLogoEmpresa.Size = new System.Drawing.Size(27, 23);
            this.button_seleccionArchivoLogoEmpresa.TabIndex = 25;
            this.button_seleccionArchivoLogoEmpresa.Text = "...";
            this.button_seleccionArchivoLogoEmpresa.UseVisualStyleBackColor = true;
            this.button_seleccionArchivoLogoEmpresa.Click += new System.EventHandler(this.button_seleccionArchivoLogoEmpresa_Click);
            // 
            // textBox_pathLogoEmpresa
            // 
            this.textBox_pathLogoEmpresa.Location = new System.Drawing.Point(8, 78);
            this.textBox_pathLogoEmpresa.Name = "textBox_pathLogoEmpresa";
            this.textBox_pathLogoEmpresa.Size = new System.Drawing.Size(476, 20);
            this.textBox_pathLogoEmpresa.TabIndex = 23;
            this.textBox_pathLogoEmpresa.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 60);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(114, 13);
            this.label31.TabIndex = 24;
            this.label31.Text = "Archivo Logo Empresa";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(8, 10);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(70, 13);
            this.label64.TabIndex = 21;
            this.label64.Text = "Razon Social";
            // 
            // textBox_nombreEmpresa
            // 
            this.textBox_nombreEmpresa.Location = new System.Drawing.Point(8, 28);
            this.textBox_nombreEmpresa.Name = "textBox_nombreEmpresa";
            this.textBox_nombreEmpresa.Size = new System.Drawing.Size(304, 20);
            this.textBox_nombreEmpresa.TabIndex = 22;
            this.textBox_nombreEmpresa.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // tabPage_General
            // 
            this.tabPage_General.Controls.Add(this.button_AbrirSelectorDirectorio);
            this.tabPage_General.Controls.Add(this.textBox_PathDirectorioReportes);
            this.tabPage_General.Controls.Add(this.label18);
            this.tabPage_General.Controls.Add(this.label1);
            this.tabPage_General.Controls.Add(this.textBoxID_ESTACION);
            this.tabPage_General.Location = new System.Drawing.Point(4, 40);
            this.tabPage_General.Name = "tabPage_General";
            this.tabPage_General.Size = new System.Drawing.Size(846, 377);
            this.tabPage_General.TabIndex = 3;
            this.tabPage_General.Text = "General";
            this.tabPage_General.UseVisualStyleBackColor = true;
            // 
            // button_AbrirSelectorDirectorio
            // 
            this.button_AbrirSelectorDirectorio.Location = new System.Drawing.Point(425, 80);
            this.button_AbrirSelectorDirectorio.Name = "button_AbrirSelectorDirectorio";
            this.button_AbrirSelectorDirectorio.Size = new System.Drawing.Size(27, 23);
            this.button_AbrirSelectorDirectorio.TabIndex = 24;
            this.button_AbrirSelectorDirectorio.Text = "...";
            this.button_AbrirSelectorDirectorio.UseVisualStyleBackColor = true;
            this.button_AbrirSelectorDirectorio.Click += new System.EventHandler(this.button_AbrirSelectorDirectorioReportes_Click);
            // 
            // textBox_PathDirectorioReportes
            // 
            this.textBox_PathDirectorioReportes.Location = new System.Drawing.Point(9, 81);
            this.textBox_PathDirectorioReportes.Name = "textBox_PathDirectorioReportes";
            this.textBox_PathDirectorioReportes.Size = new System.Drawing.Size(411, 20);
            this.textBox_PathDirectorioReportes.TabIndex = 23;
            this.textBox_PathDirectorioReportes.DoubleClick += new System.EventHandler(this.textBox_valorStringTouch_DoubleClick);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(150, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "Directorio destino de Reportes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Numero de Estacion de Pesaje";
            // 
            // textBoxID_ESTACION
            // 
            this.textBoxID_ESTACION.Location = new System.Drawing.Point(9, 33);
            this.textBoxID_ESTACION.Name = "textBoxID_ESTACION";
            this.textBoxID_ESTACION.Size = new System.Drawing.Size(49, 20);
            this.textBoxID_ESTACION.TabIndex = 22;
            this.textBoxID_ESTACION.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxID_ESTACION.DoubleClick += new System.EventHandler(this.textBox_valorNumericTouch_DoubleClick);
            this.textBoxID_ESTACION.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxID_ESTACION_Validating);
            // 
            // CConfigAppDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.button_cancelar;
            this.ClientSize = new System.Drawing.Size(859, 462);
            this.Controls.Add(this.button_cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.tabControlB1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CConfigAppDlg";
            this.Text = "Configuracion del Sistema";
            this.tabControlB1.ResumeLayout(false);
            this.tabPage_BaseDatos.ResumeLayout(false);
            this.tabPage_BaseDatos.PerformLayout();
            this.tabPage_ConexionB1.ResumeLayout(false);
            this.tabPage_ConexionB1.PerformLayout();
            this.groupBoxB1.ResumeLayout(false);
            this.groupBoxB1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage_ConexionB2.ResumeLayout(false);
            this.tabPage_ConexionB2.PerformLayout();
            this.groupBoxB2.ResumeLayout(false);
            this.groupBoxB2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage_ConexionB3.ResumeLayout(false);
            this.tabPage_ConexionB3.PerformLayout();
            this.groupBoxB3.ResumeLayout(false);
            this.groupBoxB3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage_ConexionImpresor.ResumeLayout(false);
            this.tabPage_ConexionImpresor.PerformLayout();
            this.groupBox_Etiqueta.ResumeLayout(false);
            this.groupBox_Etiqueta.PerformLayout();
            this.tabPage_scanner.ResumeLayout(false);
            this.tabPage_scanner.PerformLayout();
            this.tabPage_ingresoAPlanta.ResumeLayout(false);
            this.tabPage_ingresoAPlanta.PerformLayout();
            this.tabPage_pesajeDeCajas.ResumeLayout(false);
            this.tabPage_pesajeDeCajas.PerformLayout();
            this.tabPage_Egresos.ResumeLayout(false);
            this.tabPage_Egresos.PerformLayout();
            this.tabPage_Empresa.ResumeLayout(false);
            this.tabPage_Empresa.PerformLayout();
            this.tabPage_General.ResumeLayout(false);
            this.tabPage_General.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public void CargarDialogo()
		{
            comboBox_EncodingPrinter.DataSource = Encoding.GetEncodings().Select(p => p.Name).ToList();
            comboBox_EncodingPrinter.SelectedItem = CConfigApp.m_encodingNameOutputPrinter;

            comboBox_protocoloBalanza1.DataSource = Enum.GetValues(typeof(BALANZA_SERIAL_PORT_PROTOCOLO));
            comboBox_protocoloBalanza2.DataSource = Enum.GetValues(typeof(BALANZA_SERIAL_PORT_PROTOCOLO));
            comboBox_protocoloBalanza3.DataSource = Enum.GetValues(typeof(BALANZA_SERIAL_PORT_PROTOCOLO));
            comboBox_cantDecimalesBalanza1.SelectedIndex = 0;
            comboBox_cantDecimalesBalanza2.SelectedIndex = 0;
            comboBox_cantDecimalesBalanza3.SelectedIndex = 0;


            textBox_servidor.Text = CConfigApp.m_servidorDB;
			textBox_baseDeDatos.Text = CConfigApp.m_nombreDB;
			textBox_usuarioDB.Text = CConfigApp.m_userDB;
			textBox_passwordDB.Text = CConfigApp.m_passwordDB;
            checkBox_tipoDeSeguridadSQL_SSPI.Checked = CConfigApp.m_tipoSeguridadConexionDB_SSPI;

            checkBox_permitirColectarMasUnidadesQueLasPedidas.Checked = CConfigApp.m_permitirColectarMasCantidadesQueLasPedidasEnDespachos;
            checkBox_permiteSimularLecturaScanner.Checked = CConfigApp.m_permiteSimularLecturaScanner;


            textBox_NOMBRE_IMPRESORA.Text = CConfigApp.m_nombreImpresora;

            textBox_modeloEscannerZebra.Text = CConfigApp.m_modeloScannerZebra;
            comboBox_tipoInterfaceHostScanner.DataSource = Enum.GetValues(typeof(HostInterfaceScanner));
            comboBox_tipoInterfaceHostScanner.SelectedItem = CConfigApp.m_hostInterfaceScaneer;

            textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Text = CConfigApp.m_pathArchivoFormatosEtiquetas;
            textBox_nombreFormatoEtiquetaProducto.Text = CConfigApp.m_nombreFormatoEtiquetaProducto;
            textBox_nombreFormatoEtiquetaContenedor.Text = CConfigApp.m_nombreFormatoEtiquetaContenedor;
            textBox_nombreFormatoEtiquetaCaja.Text = CConfigApp.m_nombreFormatoEtiquetaCaja;
            textBox_nombreFormatoEtiquetaPedido.Text = CConfigApp.m_nombreFormatoEtiquetaPedido;
            textBox_cantEtiquetas.Text = CConfigApp.m_cantidadEtiquetasPorPesada.ToString();
            textBox_LineaDeTextoSuperiorDeEtiqueta.Text = CConfigApp.m_lineaDeTextoSuperiorDeEtiqueta;
            textBox_LineaDeTextoInferiorDeEtiqueta.Text = CConfigApp.m_lineaDeTextoInferiorDeEtiqueta;

            textBox_PUERTO_COM_B1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].ComName;
            textBox_maximoValorCeroB1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].MaximoRangoZero.ToString();
            textBox_PesoValidoMinimoB1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].RangoPesoValidoInferior.ToString();
            textBox_PesoValidoMaximoB1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].RangoPesoValidoSuperior.ToString();
            textBox_maximaDispercionEstableB1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].MaximaDispercionEstable.ToString();
            textBox_tiempoDetectEstableB1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].TiempoDeteccionEstable.ToString();
            checkBox_B1Habilitada.Checked = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Enable;
            checkBox_B1Habilitada_CheckedChanged(checkBox_B1Habilitada, null);
            textBox_nombreB1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Name;
            comboBox_protocoloBalanza1.SelectedItem = (BALANZA_SERIAL_PORT_PROTOCOLO)CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Protocolo;
            comboBox_cantDecimalesBalanza1.SelectedIndex = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Decimales;


            textBox_PUERTO_COM_B2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].ComName;
            textBox_maximoValorCeroB2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].MaximoRangoZero.ToString();
            textBox_PesoValidoMinimoB2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].RangoPesoValidoInferior.ToString();
            textBox_PesoValidoMaximoB2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].RangoPesoValidoSuperior.ToString();
            textBox_maximaDispercionEstableB2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].MaximaDispercionEstable.ToString();
            textBox_tiempoDetectEstableB2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].TiempoDeteccionEstable.ToString();
            checkBox_B2Habilitada.Checked = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Enable;
            checkBox_B2Habilitada_CheckedChanged(checkBox_B2Habilitada, null);
            textBox_nombreB2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Name;
            comboBox_protocoloBalanza2.SelectedItem = (BALANZA_SERIAL_PORT_PROTOCOLO)CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Protocolo;
            comboBox_cantDecimalesBalanza2.SelectedIndex = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Decimales;

            textBox_PUERTO_COM_B3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].ComName;
            textBox_maximoValorCeroB3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].MaximoRangoZero.ToString();
            textBox_PesoValidoMinimoB3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].RangoPesoValidoInferior.ToString();
            textBox_PesoValidoMaximoB3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].RangoPesoValidoSuperior.ToString();
            textBox_maximaDispercionEstableB3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].MaximaDispercionEstable.ToString();
            textBox_tiempoDetectEstableB3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].TiempoDeteccionEstable.ToString();
            checkBox_B3Habilitada.Checked = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Enable;
            checkBox_B3Habilitada_CheckedChanged(checkBox_B3Habilitada, null);
            textBox_nombreB3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Name;
            comboBox_protocoloBalanza3.SelectedItem = (BALANZA_SERIAL_PORT_PROTOCOLO)CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Protocolo;
            comboBox_cantDecimalesBalanza3.SelectedIndex = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Decimales;

            textBox_toleranciaPesajeCaja.Text = CConfigApp.m_toleranceWeightBox.ToString();


            textBoxID_ESTACION.Text = CConfigApp.m_idEstacion.ToString();
            textBox_PathDirectorioReportes.Text = CConfigApp.m_pathDirectorioReportes;

            textBox_nombreEmpresa.Text = CConfigApp.m_razonSocialEmpresa;
            textBox_pathLogoEmpresa.Text = CConfigApp.m_pathLogoEmpresa;

            checkBox_pesoUnidadesRemitidoObligatorio.Checked = CConfigApp.m_pesoUnidadesRemitidoObligatorio;
            textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.Text = CConfigApp.m_toleranciaPesoPredefPesoBalanza_IngresoAPlanta.ToString();
            checkBox_mantenerUltimaTropaEntrePesadas.Checked = CConfigApp.m_mantenerUltimaTropaEntrePesajes;
            checkBox_mantenerUltimaTipificacionEntrePesajes.Checked = CConfigApp.m_mantenerUltimaTipificacionEntrePesajes;
        }

        public void SalvarDialogo()
		{
            BALANZA_SERIAL_PORT_PROTOCOLO protocoloBalanzaSelect;
            HostInterfaceScanner hostInterfaceScanner;

            CConfigApp.m_encodingNameOutputPrinter = (string)comboBox_EncodingPrinter.SelectedValue;
            CConfigApp.m_servidorDB = textBox_servidor.Text;
			CConfigApp.m_nombreDB = textBox_baseDeDatos.Text;
			CConfigApp.m_userDB = textBox_usuarioDB.Text;
			CConfigApp.m_passwordDB = textBox_passwordDB.Text;
            CConfigApp.m_tipoSeguridadConexionDB_SSPI = checkBox_tipoDeSeguridadSQL_SSPI.Checked;

            CConfigApp.m_nombreImpresora = textBox_NOMBRE_IMPRESORA.Text;
            CConfigApp.m_modeloScannerZebra = textBox_modeloEscannerZebra.Text;

            Enum.TryParse<HostInterfaceScanner>(comboBox_tipoInterfaceHostScanner.SelectedValue.ToString(), out hostInterfaceScanner);
            CConfigApp.m_hostInterfaceScaneer = hostInterfaceScanner;

            CConfigApp.m_pathArchivoFormatosEtiquetas = textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Text;
            CConfigApp.m_nombreFormatoEtiquetaProducto = textBox_nombreFormatoEtiquetaProducto.Text;
            CConfigApp.m_nombreFormatoEtiquetaContenedor = textBox_nombreFormatoEtiquetaContenedor.Text;
            CConfigApp.m_nombreFormatoEtiquetaCaja = textBox_nombreFormatoEtiquetaCaja.Text;
            CConfigApp.m_nombreFormatoEtiquetaPedido = textBox_nombreFormatoEtiquetaPedido.Text;
            CConfigApp.m_cantidadEtiquetasPorPesada = Convert.ToInt32(textBox_cantEtiquetas.Text);
            CConfigApp.m_lineaDeTextoSuperiorDeEtiqueta = textBox_LineaDeTextoSuperiorDeEtiqueta.Text;
            CConfigApp.m_lineaDeTextoInferiorDeEtiqueta = textBox_LineaDeTextoInferiorDeEtiqueta.Text;

            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].ComName = textBox_PUERTO_COM_B1.Text;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].MaximoRangoZero = Convert.ToSingle(textBox_maximoValorCeroB1.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].RangoPesoValidoInferior = Convert.ToSingle(textBox_PesoValidoMinimoB1.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].RangoPesoValidoSuperior = Convert.ToSingle(textBox_PesoValidoMaximoB1.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].MaximaDispercionEstable = Convert.ToSingle(textBox_maximaDispercionEstableB1.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].TiempoDeteccionEstable = Convert.ToInt32(textBox_tiempoDetectEstableB1.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Enable = checkBox_B1Habilitada.Checked;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Name = textBox_nombreB1.Text;
            Enum.TryParse<BALANZA_SERIAL_PORT_PROTOCOLO>(comboBox_protocoloBalanza1.SelectedValue.ToString(), out protocoloBalanzaSelect);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Protocolo = protocoloBalanzaSelect;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].Decimales = comboBox_cantDecimalesBalanza1.SelectedIndex;


            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].ComName = textBox_PUERTO_COM_B2.Text;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].MaximoRangoZero = Convert.ToSingle(textBox_maximoValorCeroB2.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].RangoPesoValidoInferior = Convert.ToSingle(textBox_PesoValidoMinimoB2.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].RangoPesoValidoSuperior = Convert.ToSingle(textBox_PesoValidoMaximoB2.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].MaximaDispercionEstable = Convert.ToSingle(textBox_maximaDispercionEstableB2.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].TiempoDeteccionEstable = Convert.ToInt32(textBox_tiempoDetectEstableB2.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Enable = checkBox_B2Habilitada.Checked;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Name = textBox_nombreB2.Text;
            Enum.TryParse<BALANZA_SERIAL_PORT_PROTOCOLO>(comboBox_protocoloBalanza2.SelectedValue.ToString(), out protocoloBalanzaSelect);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Protocolo = protocoloBalanzaSelect;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].Decimales = comboBox_cantDecimalesBalanza2.SelectedIndex;

            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].ComName = textBox_PUERTO_COM_B3.Text;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].MaximoRangoZero = Convert.ToSingle(textBox_maximoValorCeroB3.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].RangoPesoValidoInferior = Convert.ToSingle(textBox_PesoValidoMinimoB3.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].RangoPesoValidoSuperior = Convert.ToSingle(textBox_PesoValidoMaximoB3.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].MaximaDispercionEstable = Convert.ToSingle(textBox_maximaDispercionEstableB3.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].TiempoDeteccionEstable = Convert.ToInt32(textBox_tiempoDetectEstableB3.Text);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Enable = checkBox_B3Habilitada.Checked;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Name = textBox_nombreB3.Text;
            Enum.TryParse<BALANZA_SERIAL_PORT_PROTOCOLO>(comboBox_protocoloBalanza3.SelectedValue.ToString(), out protocoloBalanzaSelect);
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Protocolo = protocoloBalanzaSelect;
            CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].Decimales = comboBox_cantDecimalesBalanza3.SelectedIndex;

            CConfigApp.m_idEstacion = Convert.ToInt16(textBoxID_ESTACION.Text);
            CConfigApp.m_pathDirectorioReportes = textBox_PathDirectorioReportes.Text;

            CConfigApp.m_razonSocialEmpresa = textBox_nombreEmpresa.Text;
            CConfigApp.m_pathLogoEmpresa = textBox_pathLogoEmpresa.Text;

            CConfigApp.m_toleranceWeightBox = Convert.ToSingle(textBox_toleranciaPesajeCaja.Text);

            CConfigApp.m_pesoUnidadesRemitidoObligatorio = checkBox_pesoUnidadesRemitidoObligatorio.Checked;
            CConfigApp.m_toleranciaPesoPredefPesoBalanza_IngresoAPlanta = Convert.ToSingle(textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta.Text);
            CConfigApp.m_mantenerUltimaTropaEntrePesajes = checkBox_mantenerUltimaTropaEntrePesadas.Checked;
            CConfigApp.m_mantenerUltimaTipificacionEntrePesajes  = checkBox_mantenerUltimaTipificacionEntrePesajes.Checked;

            CConfigApp.m_permitirColectarMasCantidadesQueLasPedidasEnDespachos = checkBox_permitirColectarMasUnidadesQueLasPedidas.Checked;
            CConfigApp.m_permiteSimularLecturaScanner = checkBox_permiteSimularLecturaScanner.Checked;

            CConfigApp.Exportar();
        }

        /************************************************************************************************
         * Funcion:     CheckIfTextBoxNumeric
         * Parametro:   control TextBox a chequear.
         * Retorno:     True si es un valor numerico.
         * Descripcion: Chequea que el dato editado en un TextBox sea numerico.
         * **********************************************************************************************/
        private bool CheckIfTextBoxNumeric(TextBox myTextBox)
        {
            bool isValid = true;
            if (myTextBox.Text == "")
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < myTextBox.Text.Length; i++)
                {
                    if (!(System.Char.IsNumber(myTextBox.Text[i])))
                    {
                        myTextBox.Text = "";
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }

        /************************************************************************************************
         * Funcion:     CheckIfTextBoxFloat
         * Parametro:   control TextBox a chequear.
         * Retorno:     True si es un valor float.
         * Descripcion: Chequea que el dato editado en un TextBox sea un valido float (puede tener 
         * o no punto decimal)
         * **********************************************************************************************/
        private bool CheckIfTextBoxFloat(TextBox myTextBox)
        {
            bool isValid = true;
            if (myTextBox.Text == "")
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < myTextBox.Text.Length; i++)
                {
                    if (!(System.Char.IsNumber(myTextBox.Text[i])) && (myTextBox.Text[i] != '.') && (myTextBox.Text[i] != ','))
                    {
                        myTextBox.Text = "";
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }
        /************************************************************************************************
         * Funcion:     CheckIfTextBoxFloat
         * Parametro:   control TextBox a chequear.
         * Parametro:   Valor minimo valido.
         * Parametro:   Valor Maximo valido.
         * Retorno:     True si es un valor numerico float y se encuentra comprendido entre los valores minimo
         *              y maximo indicado.
         * Descripcion: Chequea que el dato editado en un TextBox sea numerico float y este dentro de un rango.
         * **********************************************************************************************/
        private bool CheckIfTextBoxFloat(TextBox myTextBox, float minValor, float maxValor)
        {
            bool isValid = false;
            if (CheckIfTextBoxFloat(myTextBox))
            {
                float valor = Convert.ToSingle(myTextBox.Text);
                if (valor >= minValor && valor <= maxValor)
                    isValid = true;
            }
            return isValid;
        }

        /************************************************************************************************
         * Funcion:     CheckIfTextBoxNumeric
         * Parametro:   control TextBox a chequear.
         * Parametro:   Valor minimo valido.
         * Parametro:   Valor Maximo valido.
         * Retorno:     True si es un valor numerico y se encuentra comprendido entre los valores minimo
         *              y maximo indicado.
         * Descripcion: Chequea que el dato editado en un TextBox sea numerico y este dentro de un rango.
         * **********************************************************************************************/
        private bool CheckIfTextBoxNumeric(TextBox myTextBox,long minValor,long maxValor)
        {
            bool isValid = false;
            if (CheckIfTextBoxNumeric(myTextBox))
            {
                long valor = Convert.ToInt64(myTextBox.Text);
                if (valor >= minValor && valor <= maxValor)
                    isValid = true;
            }
            return isValid;
        }
        
		private void button_Aceptar_Click(object sender, System.EventArgs e)
		{
            if (CheckRangosDePesoBalanza1() && CheckRangosDePesoBalanza2() && CheckRangosDePesoBalanza3())
            {
                SalvarDialogo();
                Close();
            }
        }

        private bool IsValidIPAddress(string IP)
        {
            bool result = true;
            byte temp;
            char[] separador = new char[1];
            separador[0] = '.';
            string[] values = IP.Split(separador); //keep empty strings when splitting
            result &= values.Length == 4; // aka string has to be like "xx.xx.xx.xx"
            if(result)
            {
                for (int i = 0; i < 4; i++)
                {
                    result &= byte.TryParse(values[i], out temp); //each "xx" must be a byte (0-255)
                }
            }
            return result;
        }

        private void checkBox_tipoDeSeguridadSQL_SSPI_CheckedChanged(object sender, EventArgs e)
        {
            textBox_passwordDB.Enabled = !checkBox_tipoDeSeguridadSQL_SSPI.Checked;
            textBox_usuarioDB.Enabled = !checkBox_tipoDeSeguridadSQL_SSPI.Checked;
        }

        private void textBoxID_ESTACION_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckIfTextBoxNumeric(textBoxID_ESTACION, 1, 255))
            {
                MessageBox.Show("El numero de Estacion de Pesaje debe ser entre 1 y 255", "Error de Edicion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void textBox_maximoValorCero_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckIfTextBoxFloat((TextBox)sender, 0.00f, 999999.0f))
            {
                MessageBox.Show("El valor de peso Maximo para el Rango de Zero no es valido ", "Error de Edicion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void textBox_PesoMaximoValido_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckIfTextBoxFloat((TextBox)sender, 0.00f, 999999.0f))
            {
                MessageBox.Show("El valor de PESO VALIDO SUPERIOR no es correcto. ", "Error de Edicion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void textBox_PesoValidoMinimo_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckIfTextBoxFloat((TextBox)sender, 0.00f, 999999.999f))
            {
                MessageBox.Show("El valor de PESO VALIDO INFERIOR no es correcto. ", "Error de Edicion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /*****************************************************************************
         * CheckRangosDePesoBalanza1
         * Chequea que los rangos esten bien editados.
         *****************************************************************************/ 
        private bool CheckRangosDePesoBalanza1()
        {
            bool checkOk = false;
            float pesoMinimo = Convert.ToSingle(textBox_PesoValidoMinimoB1.Text);
            float pesoMaximo = Convert.ToSingle(textBox_PesoValidoMaximoB1.Text);
            float pesoMaximoCero = Convert.ToSingle(textBox_maximoValorCeroB1.Text);
            if (pesoMinimo >= pesoMaximo)
                MessageBox.Show("El valor de PESO Minimo no puede ser mayor o igual al de Peso Maximo. Corrija estos valores.", "Error de Edicion de Rangos de Peso (BALANZA1)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (pesoMinimo < pesoMaximoCero)
                MessageBox.Show("El valor de PESO Minimo no puede ser mennor al valor de Peso Maximo de Cero. ", "Error de Edicion de Rangos de Peso (BALANZA1)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                checkOk = true;
            return checkOk;
        }
        /*****************************************************************************
         * CheckRangosDePesoBalanza2
         * Chequea que los rangos esten bien editados.
         *****************************************************************************/
        private bool CheckRangosDePesoBalanza2()
        {
            bool checkOk = false;
            float pesoMinimo = Convert.ToSingle(textBox_PesoValidoMinimoB2.Text);
            float pesoMaximo = Convert.ToSingle(textBox_PesoValidoMaximoB2.Text);
            float pesoMaximoCero = Convert.ToSingle(textBox_maximoValorCeroB2.Text);
            if (pesoMinimo >= pesoMaximo)
                MessageBox.Show("El valor de PESO Minimo no puede ser mayor o igual al de Peso Maximo. Corrija estos valores.", "Error de Edicion de Rangos de Peso (BALANZA2)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (pesoMinimo < pesoMaximoCero)
                MessageBox.Show("El valor de PESO Minimo no puede ser mennor al valor de Peso Maximo de Cero. ", "Error de Edicion de Rangos de Peso (BALANZA2)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                checkOk = true;
            return checkOk;
        }
        /*****************************************************************************
         * CheckRangosDePesoBalanza3
         * Chequea que los rangos esten bien editados.
         *****************************************************************************/
        private bool CheckRangosDePesoBalanza3()
        {
            bool checkOk = false;
            float pesoMinimo = Convert.ToSingle(textBox_PesoValidoMinimoB3.Text);
            float pesoMaximo = Convert.ToSingle(textBox_PesoValidoMaximoB3.Text);
            float pesoMaximoCero = Convert.ToSingle(textBox_maximoValorCeroB3.Text);
            if (pesoMinimo >= pesoMaximo)
                MessageBox.Show("El valor de PESO Minimo no puede ser mayor o igual al de Peso Maximo. Corrija estos valores.", "Error de Edicion de Rangos de Peso (BALANZA3)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (pesoMinimo < pesoMaximoCero)
                MessageBox.Show("El valor de PESO Minimo no puede ser mennor al valor de Peso Maximo de Cero. ", "Error de Edicion de Rangos de Peso (BALANZA3)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                checkOk = true;
            return checkOk;
        }

        private void button_AbrirSelectorDirectorioReportes_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = textBox_PathDirectorioReportes.Text;
            fbd.Description = "Seleccione el Directorio Destino de Los Reportes";
            if(fbd.ShowDialog() == DialogResult.OK)
                textBox_PathDirectorioReportes.Text = fbd.SelectedPath;
        }

        private void button_ConfigurarComB1_Click(object sender, EventArgs e)
        {
            ConfigurarCOMBalanza(SCALES.SCALE1);
            textBox_PUERTO_COM_B1.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE1].ComName;
        }
        private void button_ConfigurarComB2_Click(object sender, EventArgs e)
        {
            ConfigurarCOMBalanza(SCALES.SCALE2);
            textBox_PUERTO_COM_B2.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE2].ComName;
        }
        private void button_ConfigurarComB3_Click(object sender, EventArgs e)
        {
            ConfigurarCOMBalanza(SCALES.SCALE3);
            textBox_PUERTO_COM_B3.Text = CConfigApp.m_scalesCnfg[(int)SCALES.SCALE3].ComName;
        }

        private void ConfigurarCOMBalanza(SCALES balanza)
        {
            CSetPortDlg spdlg = new CSetPortDlg();
            spdlg.m_namePort = CConfigApp.m_scalesCnfg[(int)balanza].ComName;
            spdlg.m_baudios = CConfigApp.m_scalesCnfg[(int)balanza].Baudios;
            spdlg.m_dataBits = CConfigApp.m_scalesCnfg[(int)balanza].DataBits;
            spdlg.m_parity = (System.IO.Ports.Parity)CConfigApp.m_scalesCnfg[(int)balanza].Parity;
            spdlg.m_stopBits = (System.IO.Ports.StopBits)CConfigApp.m_scalesCnfg[(int)balanza].StopBits;
            spdlg.m_handShake = (System.IO.Ports.Handshake)CConfigApp.m_scalesCnfg[(int)balanza].HandShake;

            if (spdlg.ShowDialog() == DialogResult.OK)
            {
                CConfigApp.m_scalesCnfg[(int)balanza].ComName = spdlg.m_namePort;
                CConfigApp.m_scalesCnfg[(int)balanza].Baudios = spdlg.m_baudios;
                CConfigApp.m_scalesCnfg[(int)balanza].DataBits = spdlg.m_dataBits;
                CConfigApp.m_scalesCnfg[(int)balanza].Parity = (int)spdlg.m_parity;
                CConfigApp.m_scalesCnfg[(int)balanza].StopBits = (int)spdlg.m_stopBits;
                CConfigApp.m_scalesCnfg[(int)balanza].HandShake = (int)spdlg.m_handShake;
            }
        }

        private void textBox_numericFloatEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b' || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }


        private void textBox_numericEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void textBox_servidor_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Nombre de Servidor de Base de Datos", "Nombre", textBox_servidor.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
                textBox_servidor.Text = dlg.VALUE;
        }

        private void textBox_baseDeDatos_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Nombre de Base de Datos ", "Nombre", textBox_baseDeDatos.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
                textBox_baseDeDatos.Text = dlg.VALUE;
        }

        private void textBox_usuarioDB_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Usuario de Base de Datos ", "Nombre", textBox_usuarioDB.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
                textBox_usuarioDB.Text = dlg.VALUE;
        }

        private void textBox_passwordDB_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Password de Usuario de Base de Datos ", "Password", textBox_passwordDB.Text);
            if (dlg.ShowDialog() == DialogResult.OK)
                textBox_passwordDB.Text = dlg.VALUE;
        }

        private void textBox_valorStringTouch_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Valor ", "Valor", ((TextBox)sender).Text);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void textBox_valorFloatTouch_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text,"Valor","Editar Valor ",CEditValueTouchDlg.TYPE_VALUE.FLOAT );
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void textBox_valorNumericTouch_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Valor", "Editar Valor ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void button_seleccionArchivoLogoEmpresa_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = textBox_pathLogoEmpresa.Text;
            dlg.Title = "Seleccione el Archivo del Logo de la Empresa";
            dlg.Filter = "archivo de mapa de bits (*.bmp)|*.bmp|JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pathLogoEmpresa.Text = dlg.FileName;
            }
        }

        private void textBox_cantEtiquetas_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckIfTextBoxNumeric(textBox_cantEtiquetas, 1, 5))
            {
                MessageBox.Show("El numero valido de Etiquetas por pesada es de 1 y 5", "Error de Edicion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void button_AbrirBuscadorArchivosFormatosEtiquetas_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Text;
            dlg.Title = "Seleccione el Archivo de Formatos de Etiquetas";
            dlg.Filter = "prn files (*.prn)|*.prn|txt files (*.txt)|*.txt";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_PATH_ARCHIVO_FORMATOS_ETIQUETAS.Text = dlg.FileName;
            }
        }


        private void checkBox_B1Habilitada_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxB1.Enabled = checkBox_B1Habilitada.Checked;
        }

        private void checkBox_B2Habilitada_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxB2.Enabled = checkBox_B2Habilitada.Checked;
        }

        private void checkBox_B3Habilitada_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxB3.Enabled = checkBox_B3Habilitada.Checked;
        }

        private void button_buscarImpresora_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                  textBox_NOMBRE_IMPRESORA.Text = pd.PrinterSettings.PrinterName;
            }
        }

        private void textBox_modeloEscannerZebra_DoubleClick(object sender, EventArgs e)
        {
            CEditStringTouchDlg dlg = new CEditStringTouchDlg("Editar Modelo de Escaner Zebra", "Modelo", ((TextBox)sender).Text);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void textBox_toleranciaPesajeCaja_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Valor", "Editar Valor ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void comboBox_protocoloBalanza1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_cantDecimalesBal1.Visible = comboBox_cantDecimalesBalanza1.Visible = (BALANZA_SERIAL_PORT_PROTOCOLO)comboBox_protocoloBalanza1.SelectedItem == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A;
        }

        private void comboBox_protocoloBalanza2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_cantDecimalesBal2.Visible = comboBox_cantDecimalesBalanza2.Visible = (BALANZA_SERIAL_PORT_PROTOCOLO)comboBox_protocoloBalanza2.SelectedItem == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A;
        }

        private void comboBox_protocoloBalanza3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_cantDecimalesBal3.Visible = comboBox_cantDecimalesBalanza3.Visible = (BALANZA_SERIAL_PORT_PROTOCOLO)comboBox_protocoloBalanza3.SelectedItem == BALANZA_SERIAL_PORT_PROTOCOLO.IT_EL05A;
        }

        private void textBox_toleranciaPesoPredefPesoBalanzaIngresoPlanta_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Valor", "Editar Valor ", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dlg.VALUE;
        }

        private void comboBox_tipoInterfaceHostScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_modeloEscannerZebra.Enabled = (HostInterfaceScanner)comboBox_tipoInterfaceHostScanner.SelectedIndex == HostInterfaceScanner.SNAPI_CoreScanner;
        }
    }
}
