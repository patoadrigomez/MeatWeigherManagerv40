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
using Db;
using StatusListProgressBar;
using System.Threading;

namespace MeatWeigherManager
{
	/// <summary>
	/// Descripción breve de CBackupDlg.
	/// </summary>
	public class CBackupDlg : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_cancelar;
        private TabPage tabPage_CrearBackup;
        private TabControl tabControlBackup;
        private TextBox textBox_PathDirectorioBackup;
        private Label label13;
        private PictureBox pictureBox_CrearBackup;
        private Label label1;

        //Variables de configuracion

        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public CBackupDlg()
		{
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CBackupDlg));
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_cancelar = new System.Windows.Forms.Button();
            this.tabPage_CrearBackup = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_CrearBackup = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_PathDirectorioBackup = new System.Windows.Forms.TextBox();
            this.tabControlBackup = new System.Windows.Forms.TabControl();
            this.tabPage_CrearBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CrearBackup)).BeginInit();
            this.tabControlBackup.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(452, 257);
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
            this.button_cancelar.Location = new System.Drawing.Point(532, 257);
            this.button_cancelar.Name = "button_cancelar";
            this.button_cancelar.Size = new System.Drawing.Size(75, 23);
            this.button_cancelar.TabIndex = 2;
            this.button_cancelar.Text = "Cancelar";
            // 
            // tabPage_CrearBackup
            // 
            this.tabPage_CrearBackup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage_CrearBackup.Controls.Add(this.label1);
            this.tabPage_CrearBackup.Controls.Add(this.pictureBox_CrearBackup);
            this.tabPage_CrearBackup.Controls.Add(this.label13);
            this.tabPage_CrearBackup.Controls.Add(this.textBox_PathDirectorioBackup);
            this.tabPage_CrearBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage_CrearBackup.Location = new System.Drawing.Point(4, 22);
            this.tabPage_CrearBackup.Name = "tabPage_CrearBackup";
            this.tabPage_CrearBackup.Size = new System.Drawing.Size(603, 225);
            this.tabPage_CrearBackup.TabIndex = 1;
            this.tabPage_CrearBackup.Text = "Backup";
            this.tabPage_CrearBackup.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(238, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 46;
            this.label1.Text = "GENERAR";
            // 
            // pictureBox_CrearBackup
            // 
            this.pictureBox_CrearBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_CrearBackup.BackgroundImage = global::MeatWeigherManager.Properties.Resources.backup;
            this.pictureBox_CrearBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_CrearBackup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_CrearBackup.Location = new System.Drawing.Point(229, 112);
            this.pictureBox_CrearBackup.Name = "pictureBox_CrearBackup";
            this.pictureBox_CrearBackup.Size = new System.Drawing.Size(101, 63);
            this.pictureBox_CrearBackup.TabIndex = 45;
            this.pictureBox_CrearBackup.TabStop = false;
            this.pictureBox_CrearBackup.Click += new System.EventHandler(this.pictureBox_CrearBackup_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(270, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Directorio destino del Archivo del Backup en el Servidor";
            // 
            // textBox_PathDirectorioBackup
            // 
            this.textBox_PathDirectorioBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PathDirectorioBackup.Location = new System.Drawing.Point(8, 33);
            this.textBox_PathDirectorioBackup.MaxLength = 90;
            this.textBox_PathDirectorioBackup.Name = "textBox_PathDirectorioBackup";
            this.textBox_PathDirectorioBackup.Size = new System.Drawing.Size(546, 20);
            this.textBox_PathDirectorioBackup.TabIndex = 42;
            // 
            // tabControlBackup
            // 
            this.tabControlBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlBackup.Controls.Add(this.tabPage_CrearBackup);
            this.tabControlBackup.Location = new System.Drawing.Point(0, 0);
            this.tabControlBackup.Multiline = true;
            this.tabControlBackup.Name = "tabControlBackup";
            this.tabControlBackup.SelectedIndex = 0;
            this.tabControlBackup.Size = new System.Drawing.Size(611, 251);
            this.tabControlBackup.TabIndex = 0;
            // 
            // CBackupDlg
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.button_cancelar;
            this.ClientSize = new System.Drawing.Size(616, 292);
            this.Controls.Add(this.button_cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.tabControlBackup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CBackupDlg";
            this.Text = "Creación de Backup de la Base de Datos";
            this.Load += new System.EventHandler(this.CBackupDlg_Load);
            this.tabPage_CrearBackup.ResumeLayout(false);
            this.tabPage_CrearBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CrearBackup)).EndInit();
            this.tabControlBackup.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


        private void button_Aceptar_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void pictureBox_CrearBackup_Click(object sender, EventArgs e)
        {
            if(textBox_PathDirectorioBackup.Text != "" )
            {
                try
                {
                    if (CDb.MakeBackup(CConfigApp.m_servidorDB, CConfigApp.m_userDB, CConfigApp.m_passwordDB, CConfigApp.m_nombreDB, textBox_PathDirectorioBackup.Text,
                        CConfigApp.m_tipoSeguridadConexionDB_SSPI ? CDb.TypeSecurity.SSPI : CDb.TypeSecurity.SQL))
                    {
                        MessageBox.Show("Se ha realizado con Exito el Backup de la Base de Datos !!!.", "Notificación de Backup Realizado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Se ha producidor un error en la creación del backup de la Base de Datos, consulta de este incidente al administrador del sistema.", "Error en el Proceso del Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch(CDbException dbex)
                {
                    MessageBox.Show("Se ha producidor un error en la creación del backup de la Base de Datos, consulta de este incidente al administrador del sistema. Detalle del error: "+dbex.Message, "Error en el Proceso del Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("El directorio seleccionado para el destino del Backup no es correcto.", "Validación de Directorio del Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CBackupDlg_Load(object sender, EventArgs e)
        {
            textBox_PathDirectorioBackup.Text = CDb.GetPathDirectoryDefaulBackupSqlServer();
        }
    }
}
