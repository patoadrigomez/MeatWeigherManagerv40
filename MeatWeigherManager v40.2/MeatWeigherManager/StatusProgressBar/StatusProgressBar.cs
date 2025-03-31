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

namespace StatusProgressBar
{
	// The SplashScreen class definition.  AKO Form
	public partial class CStatusProgressBar : Form
	{
		#region Member Variables
		// Threading
		private static CStatusProgressBar ms_frmSplash = null;
		private static Thread ms_oThread = null;

		// Fade in and out.
		private double m_dblOpacityIncrement = .05;
		private double m_dblOpacityDecrement = .08;
		private const int TIMER_INTERVAL = 50;

		// Status
		private string m_sStatusAction;
        // StatusResult
        private string m_sStatusResult;

        private Color m_colorStatusResult = Color.Teal;

		#endregion Member Variables

		/// <summary>
		/// Constructor
		/// </summary>
		public CStatusProgressBar()
		{
			InitializeComponent();
			this.Opacity = 0.0;
			UpdateTimer.Interval = TIMER_INTERVAL;
			UpdateTimer.Start();
			//this.ClientSize = this.BackgroundImage.Size;
		}

		#region Public Static Methods
		// A static method to create the thread and 
		// launch the SplashScreen.
		static public void ShowStatusProgressBar(string initialStatusAction)
		{
			// Make sure it's only launched once.
			if (ms_frmSplash != null)
				return;
			ms_oThread = new Thread(new ThreadStart(CStatusProgressBar.ShowForm));
			ms_oThread.IsBackground = true;
			ms_oThread.SetApartmentState(ApartmentState.STA);
			ms_oThread.Start();
			while (ms_frmSplash == null || ms_frmSplash.IsHandleCreated == false)
			{
				System.Threading.Thread.Sleep(TIMER_INTERVAL);
			}
            SetStatusAction(initialStatusAction);
		}

		// Close the form without setting the parent.
		static public void CloseForm()
		{
			if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
			{
				// Make it start going away.
				ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
			}
			ms_oThread = null;	// we don't need these any more.
			ms_frmSplash = null;
		}

		// A static method to set the status and update.
		static public void SetStatusAction(string newStatus)
		{
            if (ms_frmSplash == null)
                return;

            ms_frmSplash.m_sStatusAction = newStatus;
        }
        // A static method to set the result of the status and update.
        static public void SetResultAction(string newResult)
        {
            SetResultAction(newResult, Color.Green);
        }
        static public void SetResultAction(string newResult, Color colorStatusResult)
        {
            if (ms_frmSplash == null)
                return;
            ms_frmSplash.m_sStatusResult = newResult;
            ms_frmSplash.m_colorStatusResult = colorStatusResult;
        }

		#endregion Public Static Methods

		#region Private Methods

		// A private entry point for the thread.
		static private void ShowForm()
		{
			ms_frmSplash = new CStatusProgressBar();
			Application.Run(ms_frmSplash);
		}

		public static CStatusProgressBar GetSplashScreen()
		{
			return ms_frmSplash;
		}
        
        static public void ResetProgressBar()
        {
            if (ms_frmSplash != null)
            {
                ms_frmSplash.Invoke((MethodInvoker)delegate
                {
                    ms_frmSplash.progressBar_Splash.Value = 0; // runs on UI thread
                });
            }
        }
		
        #endregion Private Methods

		#region Event Handlers
		// Tick Event handler for the Timer control.  Handle fade in and fade out and paint progress bar. 
		private void UpdateTimer_Tick(object sender, System.EventArgs e)
		{
			lblStatusAction.Text = m_sStatusAction;
            lblStatusResult.Text = m_sStatusResult;
            lblStatusResult.ForeColor = m_colorStatusResult;

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
			else // Closing down splash screen
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
		private void StatusProgressBar_DoubleClick(object sender, System.EventArgs e)
		{
			// Use the overload that doesn't set the parent form to this very window.
			CloseForm();
		}
		#endregion Event Handlers
	}
}

