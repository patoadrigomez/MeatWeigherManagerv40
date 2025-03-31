using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Db;
using EditValueTouchDlg;

namespace MeatWeigherManager
{
    public partial class CParametrosDlg : Form
    {
        protected OleDbDataAdapter m_oleDbDataAdapter;
        //parametros
        public int DiasProximidadVencimiento { get; set; } = 0;

        int idPrdInsumoSelected = 0;

        DataTable dtInsumos =null;

        public CParametrosDlg()
        {
            InitializeComponent();
        }

        private void CABM_CParametrosDlg_Load(object sender, EventArgs e)
        {
            CargarParametros();
        }

        private void CargarParametros()
        {
            try
            {
                DiasProximidadVencimiento = CDb.GetParametroDiasProximidadVencimiento();
                textBox_diasProximidadVencimiento.Text = DiasProximidadVencimiento.ToString();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error de Base de Datos al cargar los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarParaActualizar()
        {
            bool validadoOk = false;
            if (textBox_diasProximidadVencimiento.Text == "")
            {
                MessageBox.Show("No ha editado los Dias de Proximidad de Vencimiento", "Validacion de Campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                validadoOk = true;
            return validadoOk;
        }

        private void button_Actualizar_Click(object sender, EventArgs e)
        {
            if (ValidarParaActualizar())
            {
                if (ActualizarParametros())
                {
                    CargarParametros();
                }
            }
        }

        private bool ActualizarParametros()
        {

            bool actualizadoOk = false;
            try
            {
                if(DiasProximidadVencimiento != Convert.ToInt32(textBox_diasProximidadVencimiento.Text))
                {
                    CDb.SetParametroDiasProximidadVencimiento(Convert.ToInt32(textBox_diasProximidadVencimiento.Text));
                    CDb.RegistrarEventoLog(TYPE_EVENT_DBLOG.EdicionParametroDiasProximidadVencimiento, TYPE_CONTEXT_DBLOG.EdicionParametros,
                                            string.Format("Se Modifico el Parametro Dias Proximidad Vencimiento : {0} al valor : {1}", DiasProximidadVencimiento, textBox_diasProximidadVencimiento.Text));
                    DiasProximidadVencimiento = Convert.ToInt32(textBox_diasProximidadVencimiento.Text);
                    MessageBox.Show("Parametro Dias de Proximidad de Vencimiento ACTUALIZADO !!!", "Actualización de Valor de Parametro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message, "Error en Base de Datos al Intentar Actualizar el Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return actualizadoOk;
        }

        private void textBox_diasproximidadvencimiento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CEditValueTouchDlg dlg = new CEditValueTouchDlg(((TextBox)sender).Text, "Dias", "Editar nuevo valor de Dias", CEditValueTouchDlg.TYPE_VALUE.NUMERIC, ((TextBox)sender).MaxLength);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = dlg.VALUE;
            }
        }

        private void textBox_diasproximidadvencimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9' ) || e.KeyChar == '\b')
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }
    }
}
