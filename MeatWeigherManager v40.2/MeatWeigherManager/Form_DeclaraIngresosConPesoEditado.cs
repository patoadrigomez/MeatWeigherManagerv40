using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ListViewItemExt;
using Db;
using Db.Entities;
using Extensions;
using EditValueTouchDlg;

namespace ListViewExtendedItem
{
    /// <summary>
    /// Dialogo de Edicion de Ingresos de mercaderia con edicion manual del valor de peso neto.
    /// </summary>
    public partial class Form_DeclaraIngresosConPesoEditado : Form
    {
        #region PUBLIC PROPERTYES

        public List<IngresoConPesoEditado> Ingresos { get; set; } = new List<IngresoConPesoEditado>();

        #endregion

        #region PRIVATE PROPERTYES

        private string ContextProductoNombre { get; set; }
        private bool ContextProductoEsTropa { get; set; }

        private int TotalUnidadesIngresos
        {
            get 
            {
                return label_totalUnidadesIngresos.GetValue<int>(0); 
            }
            set
            {
                label_totalUnidadesIngresos.SetValue<int>(value);
            } 
        }
        private float PesoTotalNeto
        {
            get
            {
                return label_pesoTotalNeto.GetValue<float>(0.0f);
            }
            set
            {
                label_pesoTotalNeto.SetValue<float>(value);
            }
        }
        private float PesoTotalBruto
        {
            get
            {
                return label_pesoTotalBruto.GetValue<float>(0.0f);
            }
            set
            {
                label_pesoTotalBruto.SetValue<float>(value);
            }
        }

        private int Bultos
        {
            get
            {
                return textBox_bultos.GetValue<int>(0);
            }
            set
            {
                textBox_bultos.SetValue<int>(value);
            }
        }
        #endregion

        public Form_DeclaraIngresosConPesoEditado(string contextProductName, bool contextProductoEsTropa=false)
        {
            InitializeComponent();

            LoadComboBoxTipificaciones();
            colProductoNombre.Name = "colProducto";
            colPesoNeto.Name = "colPesoNeto";
            colPesoTara.Name = "colPesoTara";
            colTropa.Name = "colTropa";
            colTipificacion.Name = "colTipificacion";
            colUnidades.Name = "colUnidades";

            ContextProductoNombre = contextProductName;
            ContextProductoEsTropa = contextProductoEsTropa;

            LoadComboBoxTipificaciones();
            EnableControlsTropa(ContextProductoEsTropa);
            TotalUnidadesIngresos = 0;
            PesoTotalNeto = 0.0f;
            PesoTotalBruto = 0.0f;
        }


        /// <summary>
        /// habilita los controles que corresponden a la seleccion de tropa y tipificacion
        /// </summary>
        /// <param name="productoEsTropa"></param>
        private void EnableControlsTropa(bool productoEsTropa)
        {
            label_tropa.Visible = productoEsTropa;
            textBox_tropa.Visible = productoEsTropa;

            label_Tipificacion.Visible = productoEsTropa;
            comboBox_tipificaciones.Visible = productoEsTropa;

            if(!productoEsTropa)
            {
                ListView_Ingresos.Columns[colTropa.Name].Width = 0;
                ListView_Ingresos.Columns[colTipificacion.Name].Width = 0;
            }
        }

        private void LoadComboBoxTipificaciones()
        {
            try
            {
                CDb.FillComboBox(comboBox_tipificaciones, "SELECT id as ID, nombre as DESCRIPCION FROM TIPIFICACIONES ");
                comboBox_tipificaciones.Items.Insert(0, new CItemCBoxTable(0, ""));
                comboBox_tipificaciones.SelectedIndex = 0;
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Error al intentar cargar el combo box de Tipificaciones desde la base de datos : {ex.Message}", "Cargando la lista de Tipificaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            IngresoToControls(new IngresoConPesoEditado()
            {
                PesoNeto=0.0f,
                PesoTara = 0.0f,
                Producto = ContextProductoNombre,
                Tropa=0,
                Unidades=1,
                Tipificacion=new Tipificacion()
                {
                    Id=0,
                    Nombre=""
                }
            });
            Bultos = 1;
            tableLayoutPanel_ingreso.Visible = true;
            btnAdd.Enabled = true;
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (EsDatosControlesIngresoValidos())
            {
                btnAdd.Enabled = false;

                do
                {
                    ListViewItemExt.ListViewItemExt item = new ListViewItemExt.ListViewItemExt(ListView_Ingresos, ControlsToIngreso());
                    ListView_Ingresos.Items.Add(item);
                } while (--Bultos > 0);

                TotalUnidadesIngresos = ListView_Ingresos.Items.Count;
                
                PesoTotalNeto = ListView_Ingresos.Items.Cast<ListViewItemExt.ListViewItemExt>().Select(x => x.Data).Cast<IngresoConPesoEditado>().Select(y=>y.PesoNeto).Sum();
                PesoTotalBruto = ListView_Ingresos.Items.Cast<ListViewItemExt.ListViewItemExt>().Select(x => x.Data).Cast<IngresoConPesoEditado>().Select(y => y.PesoTara).Sum() + PesoTotalNeto;

                //tableLayoutPanel_ingreso.Visible = false;
                Bultos = 1;

                ListView_Ingresos.SelectedItems.Clear();
                ListView_Ingresos.Items[ListView_Ingresos.Items.Count-1].Selected =true;
                ListView_Ingresos.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ListView_Ingresos.SelectedItems.Count == 1)
            {
                if (EsDatosControlesIngresoValidos())
                {
                    ((ListViewItemExt.ListViewItemExt)ListView_Ingresos.SelectedItems[0]).Update(ControlsToIngreso());
                    tableLayoutPanel_ingreso.Visible = true;
                    ListView_Ingresos.Focus();
                }
            }
            else
            {
                MessageBox.Show("No hay ningun registro seleccionado para poder realizar esta tarea.", "Actualizando un Registro de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Las propiedades de un objeto DecIngresoSinPesar son asigandas a los
        /// controles de edicion de ingreso.
        /// </summary>
        /// <param name="ingreso"></param>
        private void IngresoToControls(IngresoConPesoEditado ingreso)
        {
            label_nombreProducto.Text = ingreso.Producto;
            textBox_tropa.SetValue<int>(ingreso.Tropa);
            CDb.SelectItemComboDb(comboBox_tipificaciones,ingreso.Tipificacion.Id);
            textBox_pesoNeto.SetValue<float>(ingreso.PesoNeto);
            textBox_pesoTara.SetValue<float>(ingreso.PesoTara);
            textBox_unidadesContenidas.SetValue<int>(ingreso.Unidades);
        }

        /// <summary>
        /// Obtiene los valores editados en los controles de datos de 
        /// ingreso y los asigna a una nueva instancia del objeto
        /// DecIngresoSinPesar como retorno.
        /// </summary>
        /// <returns></returns>
        private IngresoConPesoEditado ControlsToIngreso()
        {
            IngresoConPesoEditado ingreso = new IngresoConPesoEditado();
            ingreso.Producto=label_nombreProducto.Text;
            ingreso.Tropa = textBox_tropa.GetValue<int>(0);
            ingreso.Unidades = textBox_unidadesContenidas.GetValue<int>(1);
            ingreso.PesoNeto = textBox_pesoNeto.GetValue<float>(0.0f);
            ingreso.PesoTara = textBox_pesoTara.GetValue<float>(0.0f);
            var objCB = comboBox_tipificaciones.SelectedItem as CItemCBoxTable;
            ingreso.Tipificacion = new Tipificacion() {Id=objCB.Id,Nombre=objCB.Nombre };

            return ingreso;
        }

        private bool EsDatosControlesIngresoValidos()
        {
            bool validos = false;
            if(textBox_pesoNeto.GetValue<float>(0.0f) ==  0.0f)
            {
                MessageBox.Show($"El Peso Neto no puede ser cero.", "Validación de datos de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(textBox_unidadesContenidas.GetValue<int>(0) == 0)
            {
                MessageBox.Show($"Las unidades contenidas no pueden ser cero.", "Validación de datos de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Bultos == 0)
            {
                MessageBox.Show($"La cantidad de bultos a generar ser cero.", "Validación de datos de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ContextProductoEsTropa && textBox_tropa.GetValue<int>()==0)
            {
                MessageBox.Show($"Es obligatorio la edición del numero de tropa dado que el articulo asi lo requiere por ser tipo Tropa.", "Validación de datos de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ContextProductoEsTropa && (comboBox_tipificaciones.SelectedIndex == -1 || CDb.GetSelectItemIdComboDb(comboBox_tipificaciones)==0))
            {
                MessageBox.Show($"Es obligatorio la seleccion de una tipificación dado que el articulo asi lo requiere por ser tipo Tropa.", "Validación de datos de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                validos = true;
            return validos;
        }

        private void ListViewIngresos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView_Ingresos.SelectedItems.Count == 1)
            {
                btnUpdate.Enabled = true;
                btnAdd.Enabled = true;
                ListViewItemExt.ListViewItemExt item = (ListViewItemExt.ListViewItemExt)ListView_Ingresos.SelectedItems[0];
                IngresoToControls((IngresoConPesoEditado)item.Data);
                tableLayoutPanel_ingreso.Visible = true;
            }
            else
            {
                tableLayoutPanel_ingreso.Visible = false;
            }
        }

        private void toolStripMenuItem_Eliminar_Click(object sender, EventArgs e)
        {
            if(ListView_Ingresos.SelectedItems.Count > 0)
            {
                foreach(ListViewItemExt.ListViewItemExt item in ListView_Ingresos.SelectedItems)
                {
                    ListView_Ingresos.Items.Remove(item);
                }

                TotalUnidadesIngresos = ListView_Ingresos.Items.Count;
                if(ListView_Ingresos.Items.Count > 0)
                {
                    PesoTotalNeto = ListView_Ingresos.Items.Cast<ListViewItemExt.ListViewItemExt>().Select(x => x.Data).Cast<IngresoConPesoEditado>().Select(y => y.PesoNeto).Sum();
                    PesoTotalBruto = ListView_Ingresos.Items.Cast<ListViewItemExt.ListViewItemExt>().Select(x => x.Data).Cast<IngresoConPesoEditado>().Select(y => y.PesoTara).Sum() + PesoTotalNeto;
                }
                else
                {
                    PesoTotalNeto = 0.0f;
                    PesoTotalBruto = 0.0f;
                }

                ListView_Ingresos.SelectedItems.Clear();
                tableLayoutPanel_ingreso.Visible = false;
                if (ListView_Ingresos.Items.Count == 0)
                {
                    btnUpdate.Enabled = false;
                    btnAdd.Enabled = false;
                }
            }
        }

        private void ListViewIngresos_MouseUp(object sender, MouseEventArgs e)
        {
            if (ListView_Ingresos.SelectedItems.Count == 0)
            {
                tableLayoutPanel_ingreso.Visible = false;
                btnUpdate.Enabled = false;
                btnAdd.Enabled = false;
            }
        }

        private void textBox_numericInteger_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox_numericFloat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ',' || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }

        }

        private void textBox_unidadesContenidas_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_unidadesContenidas.Text, "Unidades", "Editar las unidades contenidas en el bulto", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_unidadesContenidas.Text = dlg.VALUE;
            }
        }

        private void textBox_tropa_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_tropa.Text, "Tropa", "Editar el numero de Tropa ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_tropa.Text = dlg.VALUE;
            }
        }

        private void textBox_pesoNeto_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoNeto.Text, "Peso Neto", "Editar el Peso Neto del Bulto", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoNeto.Text = dlg.VALUE;
            }
        }

        private void textBox_pesoTara_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_pesoTara.Text, "Peso Tara", "Editar el Peso Tara del Bulto", CEditValueTouchDlg.TYPE_VALUE.FLOAT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_pesoTara.Text = dlg.VALUE;
            }
        }

        private void textBox_bultos_DoubleClick(object sender, EventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(textBox_bultos.Text, "Bultos", "Editar el numero de Bultos a Generar ", CEditValueTouchDlg.TYPE_VALUE.NUMERIC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_bultos.Text = dlg.VALUE;
            }
        }

        private void Form_DeclaraIngresosConPesoEditado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult == DialogResult.OK)
            {
                if(ListView_Ingresos.Items.Count > 0)
                {
                    var message = $"Ustede generará un total de {TotalUnidadesIngresos} ingresos , por un total de Peso Bruto: {PesoTotalBruto}kg " +
                        $"y un total de Peso Neto: {PesoTotalNeto}kg del producto {ContextProductoNombre.Trim()} con edición de peso Manual. Continua ?.";
                    
                    if( MessageBox.Show(message, "Autorización para generar Ingresos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        Ingresos = ListView_Ingresos.Items.Cast<ListViewItemExt.ListViewItemExt>().Select(x => x.Data).Cast<IngresoConPesoEditado>().ToList();
                    }
                }
                else
                {
                    if (MessageBox.Show("Usted no tiene ningun ingreso declarado , desea salir de este dialogo ? ", "Autorización para generar Ingresos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    { 
                        e.Cancel = true; 
                    }

                }
            }
        }
    }
}
