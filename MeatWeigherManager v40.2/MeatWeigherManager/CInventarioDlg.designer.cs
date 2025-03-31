namespace MeatWeigherManager
{
    partial class CInventarioDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CInventarioDlg));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_Inventario = new System.Windows.Forms.DateTimePicker();
            this.button_GenerarReporte = new System.Windows.Forms.Button();
            this.groupBox_tipoFormato = new System.Windows.Forms.GroupBox();
            this.radioButton_porExcell = new System.Windows.Forms.RadioButton();
            this.radioButton_porPDF = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos = new System.Windows.Forms.RadioButton();
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor = new System.Windows.Forms.RadioButton();
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor = new System.Windows.Forms.RadioButton();
            this.radioButton_TipoBultosSinStockConExistenciaFisica = new System.Windows.Forms.RadioButton();
            this.radioButton_TipoPiezasSinRegistro = new System.Windows.Forms.RadioButton();
            this.radioButton_TipoBultosEnStockSinExistenciaFisica = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_AjustarStock = new System.Windows.Forms.Button();
            this.radioButton_tipoBultosEnStockConProximidadVencimiento = new System.Windows.Forms.RadioButton();
            this.groupBox_tipoFormato.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Inventario";
            // 
            // dateTimePicker_Inventario
            // 
            this.dateTimePicker_Inventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_Inventario.Location = new System.Drawing.Point(6, 40);
            this.dateTimePicker_Inventario.Name = "dateTimePicker_Inventario";
            this.dateTimePicker_Inventario.Size = new System.Drawing.Size(285, 23);
            this.dateTimePicker_Inventario.TabIndex = 6;
            // 
            // button_GenerarReporte
            // 
            this.button_GenerarReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_GenerarReporte.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_GenerarReporte.Location = new System.Drawing.Point(625, 275);
            this.button_GenerarReporte.Name = "button_GenerarReporte";
            this.button_GenerarReporte.Size = new System.Drawing.Size(133, 35);
            this.button_GenerarReporte.TabIndex = 12;
            this.button_GenerarReporte.Text = "Generar Reporte";
            this.button_GenerarReporte.UseVisualStyleBackColor = true;
            this.button_GenerarReporte.Click += new System.EventHandler(this.button_GenerarReporte_Click);
            // 
            // groupBox_tipoFormato
            // 
            this.groupBox_tipoFormato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_tipoFormato.Controls.Add(this.radioButton_porExcell);
            this.groupBox_tipoFormato.Controls.Add(this.radioButton_porPDF);
            this.groupBox_tipoFormato.Location = new System.Drawing.Point(625, 114);
            this.groupBox_tipoFormato.Name = "groupBox_tipoFormato";
            this.groupBox_tipoFormato.Size = new System.Drawing.Size(135, 126);
            this.groupBox_tipoFormato.TabIndex = 17;
            this.groupBox_tipoFormato.TabStop = false;
            this.groupBox_tipoFormato.Text = "Formato";
            // 
            // radioButton_porExcell
            // 
            this.radioButton_porExcell.AutoSize = true;
            this.radioButton_porExcell.Checked = true;
            this.radioButton_porExcell.Location = new System.Drawing.Point(6, 24);
            this.radioButton_porExcell.Name = "radioButton_porExcell";
            this.radioButton_porExcell.Size = new System.Drawing.Size(53, 17);
            this.radioButton_porExcell.TabIndex = 14;
            this.radioButton_porExcell.TabStop = true;
            this.radioButton_porExcell.Text = "Excell";
            this.radioButton_porExcell.UseVisualStyleBackColor = true;
            // 
            // radioButton_porPDF
            // 
            this.radioButton_porPDF.AutoSize = true;
            this.radioButton_porPDF.Location = new System.Drawing.Point(5, 59);
            this.radioButton_porPDF.Name = "radioButton_porPDF";
            this.radioButton_porPDF.Size = new System.Drawing.Size(46, 17);
            this.radioButton_porPDF.TabIndex = 15;
            this.radioButton_porPDF.Text = "PDF";
            this.radioButton_porPDF.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_tipoBultosEnStockConProximidadVencimiento);
            this.groupBox1.Controls.Add(this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos);
            this.groupBox1.Controls.Add(this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor);
            this.groupBox1.Controls.Add(this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor);
            this.groupBox1.Controls.Add(this.radioButton_TipoBultosSinStockConExistenciaFisica);
            this.groupBox1.Controls.Add(this.radioButton_TipoPiezasSinRegistro);
            this.groupBox1.Controls.Add(this.radioButton_TipoBultosEnStockSinExistenciaFisica);
            this.groupBox1.Location = new System.Drawing.Point(8, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 129);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipos de Reportes";
            // 
            // radioButton_TipoBultosSinExistenciaDePedidosAbiertos
            // 
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.AutoSize = true;
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.Location = new System.Drawing.Point(11, 78);
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.Name = "radioButton_TipoBultosSinExistenciaDePedidosAbiertos";
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.Size = new System.Drawing.Size(230, 17);
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.TabIndex = 18;
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.Text = "Bultos sin Existenciaen de Pedidos Abiertos";
            this.radioButton_TipoBultosSinExistenciaDePedidosAbiertos.UseVisualStyleBackColor = true;
            // 
            // radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor
            // 
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.AutoSize = true;
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.Location = new System.Drawing.Point(250, 78);
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.Name = "radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor";
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.Size = new System.Drawing.Size(214, 17);
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.TabIndex = 17;
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.Text = "Piezas fuera de Contenedores sin Stock";
            this.radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor.UseVisualStyleBackColor = true;
            // 
            // radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor
            // 
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.AutoSize = true;
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.Location = new System.Drawing.Point(250, 55);
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.Name = "radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor";
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.Size = new System.Drawing.Size(211, 17);
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.TabIndex = 16;
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.Text = "Piezas fuera de Contenedores en stock";
            this.radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor.UseVisualStyleBackColor = true;
            // 
            // radioButton_TipoBultosSinStockConExistenciaFisica
            // 
            this.radioButton_TipoBultosSinStockConExistenciaFisica.AutoSize = true;
            this.radioButton_TipoBultosSinStockConExistenciaFisica.Location = new System.Drawing.Point(11, 55);
            this.radioButton_TipoBultosSinStockConExistenciaFisica.Name = "radioButton_TipoBultosSinStockConExistenciaFisica";
            this.radioButton_TipoBultosSinStockConExistenciaFisica.Size = new System.Drawing.Size(197, 17);
            this.radioButton_TipoBultosSinStockConExistenciaFisica.TabIndex = 15;
            this.radioButton_TipoBultosSinStockConExistenciaFisica.Text = "Bultos sin stock con existencia fisica";
            this.radioButton_TipoBultosSinStockConExistenciaFisica.UseVisualStyleBackColor = true;
            // 
            // radioButton_TipoPiezasSinRegistro
            // 
            this.radioButton_TipoPiezasSinRegistro.AutoSize = true;
            this.radioButton_TipoPiezasSinRegistro.Location = new System.Drawing.Point(250, 33);
            this.radioButton_TipoPiezasSinRegistro.Name = "radioButton_TipoPiezasSinRegistro";
            this.radioButton_TipoPiezasSinRegistro.Size = new System.Drawing.Size(187, 17);
            this.radioButton_TipoPiezasSinRegistro.TabIndex = 15;
            this.radioButton_TipoPiezasSinRegistro.Text = "Bultos no registradas en el sistema";
            this.radioButton_TipoPiezasSinRegistro.UseVisualStyleBackColor = true;
            // 
            // radioButton_TipoBultosEnStockSinExistenciaFisica
            // 
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.AutoSize = true;
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.Checked = true;
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.Location = new System.Drawing.Point(11, 33);
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.Name = "radioButton_TipoBultosEnStockSinExistenciaFisica";
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.Size = new System.Drawing.Size(191, 17);
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.TabIndex = 14;
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.TabStop = true;
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.Text = "Bultos en stock sin existencia fisica";
            this.radioButton_TipoBultosEnStockSinExistenciaFisica.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dateTimePicker_Inventario);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 82);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // button_AjustarStock
            // 
            this.button_AjustarStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_AjustarStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_AjustarStock.ForeColor = System.Drawing.Color.Maroon;
            this.button_AjustarStock.Location = new System.Drawing.Point(8, 275);
            this.button_AjustarStock.Name = "button_AjustarStock";
            this.button_AjustarStock.Size = new System.Drawing.Size(133, 35);
            this.button_AjustarStock.TabIndex = 20;
            this.button_AjustarStock.Text = "Ajustar Stock";
            this.button_AjustarStock.UseVisualStyleBackColor = true;
            this.button_AjustarStock.Click += new System.EventHandler(this.button_AjustarStock_Click);
            // 
            // radioButton_tipoBultosEnStockConProximidadVencimiento
            // 
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.AutoSize = true;
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.Location = new System.Drawing.Point(250, 101);
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.Name = "radioButton_tipoBultosEnStockConProximidadVencimiento";
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.Size = new System.Drawing.Size(251, 17);
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.TabIndex = 19;
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.Text = "Bultos en Stock con Proximidad de Vencimiento";
            this.radioButton_tipoBultosEnStockConProximidadVencimiento.UseVisualStyleBackColor = true;
            // 
            // CInventarioDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 322);
            this.Controls.Add(this.button_AjustarStock);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_tipoFormato);
            this.Controls.Add(this.button_GenerarReporte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CInventarioDlg";
            this.Text = "Gestión de Inventario";
            this.Load += new System.EventHandler(this.CInventarioDlg_Load);
            this.groupBox_tipoFormato.ResumeLayout(false);
            this.groupBox_tipoFormato.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Inventario;
        private System.Windows.Forms.Button button_GenerarReporte;
        private System.Windows.Forms.GroupBox groupBox_tipoFormato;
        private System.Windows.Forms.RadioButton radioButton_porExcell;
        private System.Windows.Forms.RadioButton radioButton_porPDF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_TipoBultosEnStockSinExistenciaFisica;
        private System.Windows.Forms.RadioButton radioButton_TipoPiezasSinRegistro;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_TipoBultosSinStockConExistenciaFisica;
        private System.Windows.Forms.Button button_AjustarStock;
        private System.Windows.Forms.RadioButton radioButton_TipoPiezasSinStockConExistenciaFisicaSinContenedor;
        private System.Windows.Forms.RadioButton radioButton_TipoPiezasEnStockConExistenciaFisicaSinContenedor;
        private System.Windows.Forms.RadioButton radioButton_TipoBultosSinExistenciaDePedidosAbiertos;
        private System.Windows.Forms.RadioButton radioButton_tipoBultosEnStockConProximidadVencimiento;
    }
}