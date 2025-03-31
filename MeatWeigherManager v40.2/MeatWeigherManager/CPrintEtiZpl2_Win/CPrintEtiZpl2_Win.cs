using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using RawPrinterHelper;

namespace PrintEtiZpl2_Win
{
    /// <summary>
    /// Esta Clase permite enviar un archivo de formato de etiquetas y variables de formato
    /// de etiqueta a una impresora Zebra con protocolo ZPL2 que se encuentre instalada en windows
    /// con interfaz USB,LPT o Ethernet. Solo se requiere el nombre de la impresora a la hora de
    /// imprimir en ella.
    /// </summary>
    public class CPrintEtiZpl2_Win : CRawPrinterHelper
    {
        public static bool Print(string namePrinter,string nameFormat, CListVariables listaVariables, string encodingNameOutput, short cantEtiquetasGenerar)
        {
            bool printOK = true;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            Int32 lengthData;

            string dataSendLabel;
            dataSendLabel = String.Format("^XA\r\n^XFE:{0}\r\n", nameFormat);
            dataSendLabel += listaVariables.Get();
            dataSendLabel += String.Format("^PQ{0}^XZ\r\n", cantEtiquetasGenerar);

            char[] charsString = dataSendLabel.ToCharArray();
            //convierto string a bytes
            Byte[] bytes = Encoding.GetEncoding(encodingNameOutput).GetBytes(charsString);
            lengthData = bytes.Length;
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(lengthData);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, lengthData);
            // Send the unmanaged bytes to the printer.
            printOK = CRawPrinterHelper.SendBytesToPrinter(namePrinter,pUnmanagedBytes, lengthData);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return printOK;
        }

        public static bool SendFormatToPrinter(string namePrinter,string pathfileFormat)
        {
            return CRawPrinterHelper.SendFileToPrinter(namePrinter, pathfileFormat);
        }
        public static bool SendFormatToPrinter(string namePrinter, string pathfileFormat,string nameEncodingOutput)
        {
            return CRawPrinterHelper.SendFileToPrinter(namePrinter, pathfileFormat,nameEncodingOutput);
        }
    }

    public class CListVariables
    {
        int idxVariable = 0;
        string m_listVariables;

        public CListVariables()
        {
        }
        public void Add(String strVariable)
        {
            m_listVariables += String.Format("^FN{0}^FD{1}^FS\r\n", ++idxVariable, strVariable);
        }
        public string Get()
        {
            return m_listVariables;
        }
    }

}
