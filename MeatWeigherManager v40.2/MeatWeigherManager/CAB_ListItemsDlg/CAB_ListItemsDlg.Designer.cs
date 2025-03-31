namespace AB_ListItemsDlg
{
    partial class CAB_ListItemsDlg
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CAB_ListItemsDlg));
            this.textBox_Item = new System.Windows.Forms.TextBox();
            this.label_Item = new System.Windows.Forms.Label();
            this.listBox_Items = new System.Windows.Forms.ListBox();
            this.button_EliminarItem = new System.Windows.Forms.Button();
            this.button_AgregarItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Item
            // 
            this.textBox_Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Item.Location = new System.Drawing.Point(12, 38);
            this.textBox_Item.Name = "textBox_Item";
            this.textBox_Item.Size = new System.Drawing.Size(171, 24);
            this.textBox_Item.TabIndex = 0;
            // 
            // label_Item
            // 
            this.label_Item.AutoSize = true;
            this.label_Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Item.Location = new System.Drawing.Point(12, 9);
            this.label_Item.Name = "label_Item";
            this.label_Item.Size = new System.Drawing.Size(33, 16);
            this.label_Item.TabIndex = 1;
            this.label_Item.Text = "Item";
            // 
            // listBox_Items
            // 
            this.listBox_Items.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_Items.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Items.FormattingEnabled = true;
            this.listBox_Items.ItemHeight = 16;
            this.listBox_Items.Location = new System.Drawing.Point(263, 38);
            this.listBox_Items.Name = "listBox_Items";
            this.listBox_Items.Size = new System.Drawing.Size(165, 180);
            this.listBox_Items.TabIndex = 2;
            // 
            // button_EliminarItem
            // 
            this.button_EliminarItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_EliminarItem.BackgroundImage")));
            this.button_EliminarItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_EliminarItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_EliminarItem.ForeColor = System.Drawing.Color.Green;
            this.button_EliminarItem.Location = new System.Drawing.Point(200, 90);
            this.button_EliminarItem.Name = "button_EliminarItem";
            this.button_EliminarItem.Size = new System.Drawing.Size(42, 39);
            this.button_EliminarItem.TabIndex = 3;
            this.button_EliminarItem.TabStop = false;
            this.button_EliminarItem.UseVisualStyleBackColor = true;
            this.button_EliminarItem.Click += new System.EventHandler(this.button_EliminarItem_Click);
            // 
            // button_AgregarItem
            // 
            this.button_AgregarItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_AgregarItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_AgregarItem.BackgroundImage")));
            this.button_AgregarItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_AgregarItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_AgregarItem.ForeColor = System.Drawing.Color.Transparent;
            this.button_AgregarItem.Location = new System.Drawing.Point(200, 38);
            this.button_AgregarItem.Name = "button_AgregarItem";
            this.button_AgregarItem.Size = new System.Drawing.Size(42, 39);
            this.button_AgregarItem.TabIndex = 1;
            this.button_AgregarItem.UseVisualStyleBackColor = true;
            this.button_AgregarItem.Click += new System.EventHandler(this.button_AgregarItem_Click);
            // 
            // CAB_ListItemsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 234);
            this.Controls.Add(this.button_EliminarItem);
            this.Controls.Add(this.button_AgregarItem);
            this.Controls.Add(this.listBox_Items);
            this.Controls.Add(this.label_Item);
            this.Controls.Add(this.textBox_Item);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CAB_ListItemsDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.CAB_ListItemsDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Item;
        private System.Windows.Forms.Label label_Item;
        private System.Windows.Forms.ListBox listBox_Items;
        private System.Windows.Forms.Button button_EliminarItem;
        private System.Windows.Forms.Button button_AgregarItem;
    }
}

