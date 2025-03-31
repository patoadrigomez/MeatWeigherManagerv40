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

namespace MeatWeigherManager
{
	/// <summary>
	/// Descripción breve de CConfigWorkMode.
	/// </summary>
	public class CConfigWorkMode : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button button_Aceptar;
        private System.Windows.Forms.Button button_cancelar;
        private TabPage tabPage_Etiquetado;
        private CheckBox checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta;
        private TabControl tabControlB1;
        private GroupBox groupBox1;
        private CheckBox checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion;
        private CheckBox checkBox_imprimirLineasDeTextoSuperiorEInferior;
        private CheckBox checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion;

        //Variables de configuracion

        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public CConfigWorkMode()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CConfigWorkMode));
            this.button_Aceptar = new System.Windows.Forms.Button();
            this.button_cancelar = new System.Windows.Forms.Button();
            this.tabPage_Etiquetado = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion = new System.Windows.Forms.CheckBox();
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior = new System.Windows.Forms.CheckBox();
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta = new System.Windows.Forms.CheckBox();
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion = new System.Windows.Forms.CheckBox();
            this.tabControlB1 = new System.Windows.Forms.TabControl();
            this.tabPage_Etiquetado.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControlB1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Aceptar
            // 
            this.button_Aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Aceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Aceptar.Location = new System.Drawing.Point(471, 307);
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
            this.button_cancelar.Location = new System.Drawing.Point(551, 307);
            this.button_cancelar.Name = "button_cancelar";
            this.button_cancelar.Size = new System.Drawing.Size(75, 23);
            this.button_cancelar.TabIndex = 2;
            this.button_cancelar.Text = "Cancelar";
            // 
            // tabPage_Etiquetado
            // 
            this.tabPage_Etiquetado.Controls.Add(this.groupBox1);
            this.tabPage_Etiquetado.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Etiquetado.Name = "tabPage_Etiquetado";
            this.tabPage_Etiquetado.Size = new System.Drawing.Size(622, 275);
            this.tabPage_Etiquetado.TabIndex = 1;
            this.tabPage_Etiquetado.Text = "Etiquetado";
            this.tabPage_Etiquetado.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion);
            this.groupBox1.Controls.Add(this.checkBox_imprimirLineasDeTextoSuperiorEInferior);
            this.groupBox1.Controls.Add(this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta);
            this.groupBox1.Controls.Add(this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion);
            this.groupBox1.Location = new System.Drawing.Point(8, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 131);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Etiqueta Producto";
            // 
            // checkBox_imprimirUnidadesEnProduccion
            // 
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.AutoSize = true;
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.Location = new System.Drawing.Point(6, 75);
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.Name = "checkBox_imprimirUnidadesEnProduccion";
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.Size = new System.Drawing.Size(236, 17);
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.TabIndex = 6;
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.Text = "Imprimir Unidades en Pesajes en Producción";
            this.checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.UseVisualStyleBackColor = true;
            // 
            // checkBox_imprimirLineasDeTextoSuperiorEInferior
            // 
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.AutoSize = true;
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.Location = new System.Drawing.Point(6, 98);
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.Name = "checkBox_imprimirLineasDeTextoSuperiorEInferior";
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.Size = new System.Drawing.Size(226, 17);
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.TabIndex = 5;
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.Text = "Imprimir Lineas de Texto Superior e Inferior";
            this.checkBox_imprimirLineasDeTextoSuperiorEInferior.UseVisualStyleBackColor = true;
            // 
            // checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta
            // 
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.AutoSize = true;
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.Location = new System.Drawing.Point(6, 29);
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.Name = "checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta";
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.Size = new System.Drawing.Size(183, 17);
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.TabIndex = 3;
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.Text = "Imprimir Peso en Ingreso a Planta";
            this.checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.UseVisualStyleBackColor = true;
            // 
            // checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion
            // 
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.AutoSize = true;
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.Location = new System.Drawing.Point(6, 52);
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.Name = "checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion";
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.Size = new System.Drawing.Size(215, 17);
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.TabIndex = 4;
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.Text = "Imprimir Peso en Pesajes en Producción";
            this.checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.UseVisualStyleBackColor = true;
            // 
            // tabControlB1
            // 
            this.tabControlB1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlB1.Controls.Add(this.tabPage_Etiquetado);
            this.tabControlB1.Location = new System.Drawing.Point(0, 0);
            this.tabControlB1.Multiline = true;
            this.tabControlB1.Name = "tabControlB1";
            this.tabControlB1.SelectedIndex = 0;
            this.tabControlB1.Size = new System.Drawing.Size(630, 301);
            this.tabControlB1.TabIndex = 0;
            // 
            // CConfigWorkMode
            // 
            this.AcceptButton = this.button_Aceptar;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.button_cancelar;
            this.ClientSize = new System.Drawing.Size(635, 342);
            this.Controls.Add(this.button_cancelar);
            this.Controls.Add(this.button_Aceptar);
            this.Controls.Add(this.tabControlB1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CConfigWorkMode";
            this.Text = "Configuracion del Modo de Trabajo";
            this.tabPage_Etiquetado.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlB1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void CargarDialogo()
		{
            checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.Checked = CConfigApp.m_ingresoAPlanta_WeightLabelEnable;
            checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.Checked = CConfigApp.m_pesajeEnProduccion_WeightLabelEnable;
            checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.Checked = CConfigApp.m_pesajeEnProduccion_WeightLabelEnable;
            checkBox_imprimirLineasDeTextoSuperiorEInferior.Checked = CConfigApp.m_imprimirLineasTextoSuperiorInferiorDeEtiqueta;
        }

        public void SalvarDialogo()
		{
            CConfigApp.m_ingresoAPlanta_WeightLabelEnable = checkBox_imprimirPesoEtiquetaProducto_IngresoAPlanta.Checked;
            CConfigApp.m_pesajeEnProduccion_WeightLabelEnable = checkBox_imprimirPesoEtiquetaProducto_PesajesEnProduccion.Checked;
            CConfigApp.m_pesajeEnProduccion_UnitsLabelEnable = checkBox_imprimirUnidadesEtiquetaProducto_PesajesEnProduccion.Checked;
            CConfigApp.m_imprimirLineasTextoSuperiorInferiorDeEtiqueta = checkBox_imprimirLineasDeTextoSuperiorEInferior.Checked;
            CConfigApp.Exportar();
        }
        private void button_Aceptar_Click(object sender, System.EventArgs e)
        {
            SalvarDialogo();
            Close();
        }

    }
}
