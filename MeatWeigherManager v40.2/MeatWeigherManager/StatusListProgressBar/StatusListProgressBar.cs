using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace StatusListProgressBar
{
	// The SplashScreen class definition.  AKO Form
	public partial class CStatusListProgressBar : Form
	{
		#region Member Variables
		// Threading
		private static CStatusListProgressBar ms_frmInstance = null;
		private static Thread ms_oThread = null;

		// Fade in and out.
		private double m_dblOpacityIncrement = .05;
		private double m_dblOpacityDecrement = .08;
		private const int TIMER_INTERVAL = 50;

		#endregion Member Variables

		/// <summary>
		/// Constructor
		/// </summary>
		public CStatusListProgressBar()
		{
			InitializeComponent();
			this.Opacity = 0.0;
			UpdateTimer.Interval = TIMER_INTERVAL;
			UpdateTimer.Start();
		}

        #region Public Static Methods
        // A static method to create the thread and 
        // launch the StatusListProgressBar.
        static public void ShowStatusListProgressBar(string initialStatusAction)
		{
			// Make sure it's only launched once.
			if (ms_frmInstance != null)
				return;
			ms_oThread = new Thread(new ThreadStart(CStatusListProgressBar.ShowForm));
			ms_oThread.IsBackground = true;
			ms_oThread.SetApartmentState(ApartmentState.STA);
			ms_oThread.Start();
			while (ms_frmInstance == null || ms_frmInstance.IsHandleCreated == false)
			{
				System.Threading.Thread.Sleep(TIMER_INTERVAL);
			}
            SetTextAction(initialStatusAction);
		}

		// Close the form without setting the parent.
		static public void CloseForm()
		{
			if (ms_frmInstance != null && ms_frmInstance.IsDisposed == false)
			{
				// Make it start going away.
				ms_frmInstance.m_dblOpacityIncrement = -ms_frmInstance.m_dblOpacityDecrement;
			}
			ms_oThread = null;	// we don't need these any more.
			ms_frmInstance = null;
		}

        /// <summary>
        /// Texto a colocar como titulo de Accion
        /// </summary>
        /// <param name="newStatus">linea de texto como titulo de Accion </param>
		static public void SetTextAction(string newStatus)
		{
            if (ms_frmInstance == null)
                return;

			ms_frmInstance.Invoke((MethodInvoker)delegate
			{
				ms_frmInstance.lblStatusAction.Text=newStatus;
			});
        }
        /// <summary>
        /// Inserta una nueva linea de texto en el listbox de status
        /// si parte de este texto contiene una cadena "error" , si
        /// pinta en rojo.
        /// </summary>
        /// <param name="newResult">linea de texto a agregar al listbox de status </param>
        static public void SetTextActivityAction(string newResult)
        {
            if (ms_frmInstance == null)
                return;
			ms_frmInstance.Invoke((MethodInvoker)delegate
			{
				ms_frmInstance.listBox_statusResult.Items.Add(newResult);
				ms_frmInstance.listBox_statusResult.SetSelected(ms_frmInstance.listBox_statusResult.Items.Count - 1, true);
			});
        }

		#endregion Public Static Methods

		#region Private Methods

		// A private entry point for the thread.
		static private void ShowForm()
		{
			ms_frmInstance = new CStatusListProgressBar();
			Application.Run(ms_frmInstance);
		}

		public static CStatusListProgressBar GetInstanceFrm()
		{
			return ms_frmInstance;
		}
        
        static public void ResetProgressBar()
        {
            if (ms_frmInstance != null)
            {
                ms_frmInstance.Invoke((MethodInvoker)delegate
                {
                    ms_frmInstance.progressBar_Splash.Value = 0; // runs on UI thread
                });
            }
        }
        static public void FillProgressBar()
        {
            if (ms_frmInstance != null)
            {
                ms_frmInstance.Invoke((MethodInvoker)delegate
                {
                    ms_frmInstance.progressBar_Splash.Value = ms_frmInstance.progressBar_Splash.Maximum; // runs on UI thread
                });
            }
        }

        #endregion Private Methods

        #region Event Handlers
        // Tick Event handler for the Timer control.  Handle fade in and fade out and paint progress bar. 
        private void UpdateTimer_Tick(object sender, System.EventArgs e)
		{

            if(progressBar_Splash.Value < progressBar_Splash.Maximum)
                progressBar_Splash.Value += 1; 
            //Informar porcentual de avance del progressBar
            lblTimeRemaining.Text = ((((float)progressBar_Splash.Value) / ((float)progressBar_Splash.Maximum)) * 100.0f).ToString() + "%";

			// Calculate opacity
			if (m_dblOpacityIncrement > 0)		// Starting up splash screen
			{
				if (this.Opacity < 1)
					this.Opacity += m_dblOpacityIncrement;
			}
			else // Closing screen
			{
				if (this.Opacity > 0)
					this.Opacity += m_dblOpacityIncrement;
				else
				{
					UpdateTimer.Stop();
					this.Close();
				}
			}
		}

		// Close the form if they double click on it.
		private void StatusListProgressBar_DoubleClick(object sender, System.EventArgs e)
		{
			// Use the overload that doesn't set the parent form to this very window.
			CloseForm();
		}
        private void button_close_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        /// <summary>
        /// Se evalua cada item a dibujar en el listbox , si parte del texto
        /// contiene la cadena "error" todo el item se pinta en rojo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_statusResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();
                Brush myBrush = Brushes.White;

                string value = ((ListBox)sender).Items[e.Index].ToString();
                if (value.ToLower().Contains("error"))
                {
                    myBrush = Brushes.Red;

                }
                else
                {
                    myBrush = Brushes.White;
                }

                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }
            catch
            {

            }
        }
        #endregion Event Handlers

    }
}

