namespace ProductoInsumosCtrl
{
    partial class ProductoInsumosCtrl
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView_DetalleDeInsumos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetalleDeInsumos)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_DetalleDeInsumos
            // 
            this.dataGridView_DetalleDeInsumos.AllowUserToAddRows = false;
            this.dataGridView_DetalleDeInsumos.AllowUserToDeleteRows = false;
            this.dataGridView_DetalleDeInsumos.AllowUserToOrderColumns = true;
            this.dataGridView_DetalleDeInsumos.AllowUserToResizeRows = false;
            this.dataGridView_DetalleDeInsumos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_DetalleDeInsumos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView_DetalleDeInsumos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_DetalleDeInsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DetalleDeInsumos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_DetalleDeInsumos.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_DetalleDeInsumos.Name = "dataGridView_DetalleDeInsumos";
            this.dataGridView_DetalleDeInsumos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_DetalleDeInsumos.Size = new System.Drawing.Size(313, 184);
            this.dataGridView_DetalleDeInsumos.StandardTab = true;
            this.dataGridView_DetalleDeInsumos.TabIndex = 15;
            this.dataGridView_DetalleDeInsumos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_DetalleDeInsumos_CellDoubleClick);
            this.dataGridView_DetalleDeInsumos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_DetalleDeInsumos_CellValidating);
            this.dataGridView_DetalleDeInsumos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_DetalleDeInsumos_EditingControlShowing);
            // 
            // ProductoInsumosCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView_DetalleDeInsumos);
            this.Name = "ProductoInsumosCtrl";
            this.Size = new System.Drawing.Size(313, 184);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetalleDeInsumos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_DetalleDeInsumos;
    }
}
