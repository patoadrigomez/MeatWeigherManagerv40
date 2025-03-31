using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;

namespace Commons
{
    public class CCommons
    {
        public static bool CheckIfTextBoxFloat(TextBox myTextBox)
        {
            bool isValid = true;
            float result;
            return float.TryParse(myTextBox.Text, out result);
        }

        public static float GetFloatSecureFromTextBox(TextBox myTextBox)
        {
            float valf = 0.0f;
            float.TryParse(myTextBox.Text, out valf);
            return valf;
        }

        /************************************************************************************************
            * Funcion:     CheckIfTextBoxNumeric
            * Parametro:   control TextBox a chequear.
            * Retorno:     True si es un valor numerico.
            * Descripcion: Chequea que el dato editado en un TextBox sea numerico.
            * **********************************************************************************************/
        public static bool CheckIfTextBoxNumeric(TextBox myTextBox)
        {
            bool isValid = true;
            if (myTextBox.Text == "")
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < myTextBox.Text.Length; i++)
                {
                    if (!(System.Char.IsNumber(myTextBox.Text[i])))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }

        /************************************************************************************************
         * Funcion:     CheckIfTextBoxFloat
         * Parametro:   control TextBox a chequear.
         * Parametro:   Valor minimo valido.
         * Parametro:   Valor Maximo valido.
         * Retorno:     True si es un valor numerico float y se encuentra comprendido entre los valores minimo
         *              y maximo indicado.
         * Descripcion: Chequea que el dato editado en un TextBox sea numerico float y este dentro de un rango.
         * **********************************************************************************************/
        public static bool CheckIfTextBoxFloat(TextBox myTextBox, float minValor, float maxValor)
        {
            bool isValid = false;
            if (CheckIfTextBoxFloat(myTextBox))
            {
                float valor = Convert.ToSingle(myTextBox.Text);
                if (valor >= minValor && valor <= maxValor)
                    isValid = true;
            }
            return isValid;
        }

        /****************************************************************************
        *  Funcion:        isAliveIP() 
        *  Descripcion:    Verifica que el equipo remoto responda un ping.
        ***************************************************************************/
        public static bool isAliveIP(string ip)
        {
            bool success = false;
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128, 
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 300;
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                success = (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
            }
            return success;
        }

        public static bool IsValidIP(string addr)
        {
            bool valid = false;
            //create our match pattern
            string pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            if (addr == "")
                valid = false;
            else
            {
                valid = check.IsMatch(addr,0);
            }
            return valid;
        }
        public static void SetToolStripStatusLabel(ToolStripStatusLabel ts, string text, Color color)
        {
            ts.Text = text;
            ts.ForeColor = color;
        }

        /// <summary>
        /// valida si un string posee un formato correcto de fecha hora
        /// en sus posibles alternativas { "dd-MM-yyyy", "dd/MM/yyyy","ddMMyyyy"}
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsValidDateTimeString(string dateTime)
        {
            string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy","ddMMyyyy"};
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("es-ES"),
                                           DateTimeStyles.None, out parsedDateTime);
        }
        /// <summary>
        /// Realiza una conversion a DateTime desde un string con posibles formatos
        /// como ser { "dd-MM-yyyy", "dd/MM/yyyy", "ddMMyyyy" }
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns>DateTime value</returns>
        public static DateTime GetDateTimeFromString(string strDateTime)
        {
            string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy", "ddMMyyyy" };
            DateTime parsedDateTime;
            DateTime.TryParseExact(strDateTime, formats, new CultureInfo("es-ES"),
                                           DateTimeStyles.None, out parsedDateTime);
            return parsedDateTime;
        }
    }
}
