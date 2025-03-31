using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Logger
{
    public static class CLogger
    {
        static readonly TextWriter tw;

        static CLogger()
        {
            tw = TextWriter.Synchronized(File.AppendText("Logger.txt"));
        }

        public static void Write(string logMessage)
        {
            try
            {
                Log(logMessage, tw);
            }
            catch (IOException e)
            {
                tw.Close();
            }
        }
        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("{0} {1}", DateTime.Now.ToLongDateString(),DateTime.Now.ToLongTimeString());
            w.WriteLine("Descripcion:");
            w.WriteLine("   {0}", logMessage);
            w.WriteLine("-------------------------------");
            w.Flush();
        }
    }
}
