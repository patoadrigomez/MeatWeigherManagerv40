using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMCR.WaitCursor
{
    /// <summary>
    /// Clase que encapsula la llamada a activar un WaitCursor con la creacion de
    /// la instancia y se desactiva cuando la instancia se pone disponible.
    /// El uso es el siguiente:
    /// using (new WaitCursor())
    /// {
    ///     codigo que realiza una tarea.
    /// }
    /// al salir del bloque using se pone disponible la instancia de WaitCursor y
    /// se desabilita el cursor WaitCursor.
    /// 
    /// </summary>
    public class WaitCursor:IDisposable
    {
        public WaitCursor()
        {
            Application.UseWaitCursor = true;
        }

        public void Dispose()
        {
            Application.UseWaitCursor = false;
        }
    }
}
