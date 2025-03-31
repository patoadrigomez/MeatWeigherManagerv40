using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace INI_RW
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class CIniRw
    {
        public string m_path;
        public string m_nameFileIni;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// INIFile Constructor.
        public CIniRw(string nameFileIni)
        {
            m_nameFileIni = nameFileIni;
            m_path = Environment.CurrentDirectory + "\\" + m_nameFileIni;
        }
        /**********************************************************
        *	Write Data string to the INI File
        ***********************************************************/
        public void WriteValueString(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.m_path);
        }
        /**********************************************************
        *	Write Data Int to the INI File
        ***********************************************************/
        public void WriteValueInt(string Section, string Key, int Value)
        {
            string valuedStr = Value.ToString();
            WritePrivateProfileString(Section, Key, valuedStr, this.m_path);
        }
        /**********************************************************
        *	Write Data Float to the INI File
        ***********************************************************/
        public void WriteValueSingle(string Section, string Key, float Value)
        {
            string valuedStr = Value.ToString();
            WritePrivateProfileString(Section, Key, valuedStr, this.m_path);
        }
        /**********************************************************
        *	Write Data Bool to the INI File
        ***********************************************************/
        public void WriteValueBool(string Section, string Key, bool Value)
        {
            string valuedStr = Value.ToString();
            WritePrivateProfileString(Section, Key, valuedStr, this.m_path);
        }
        /**********************************************************
        *	Read Data string to the INI File
        ***********************************************************/
        public string ReadValueString(string Section, string Key, string def)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, def, temp, 255, this.m_path);
            return temp.ToString();
        }
        /**********************************************************
        *	Read Data int to the INI File
        ***********************************************************/
        public int ReadValueInt(string Section, string Key, int valueDefault)
        {
            int valueInt;
            StringBuilder temp = new StringBuilder(20);
            GetPrivateProfileString(Section, Key, valueDefault.ToString(), temp, 20, this.m_path);
            try
            {
                valueInt = Convert.ToInt32(temp.ToString());
            }
            catch (Exception e)
            {
                valueInt = valueDefault;
            }
            return valueInt;
        }
        /**********************************************************
        *	Read Data single to the INI File
        ***********************************************************/
        public float ReadValueSingle(string Section, string Key, float valueDefault)
        {
            float valueSingle;
            StringBuilder temp = new StringBuilder(20);
            GetPrivateProfileString(Section, Key, valueDefault.ToString(), temp, 20, this.m_path);
            try
            {
                valueSingle = Convert.ToSingle(temp.ToString());
            }
            catch (Exception e)
            {
                valueSingle = valueDefault;
            }
            return valueSingle;
        }

        /**********************************************************
        *	Read Data Bool to the INI File
        ***********************************************************/
        public bool ReadValueBool(string Section, string Key, bool valueDefault)
        {
            bool valueBool;
            StringBuilder temp = new StringBuilder(20);
            GetPrivateProfileString(Section, Key, valueDefault.ToString(), temp, 20, this.m_path);
            try
            {
                valueBool = Convert.ToBoolean(temp.ToString());
            }
            catch (Exception e)
            {
                valueBool = valueDefault;
            }
            return valueBool;
        }
        /**********************************************************
        *	Seleccionar otro archivo
        ***********************************************************/
        public string NameFileIni
        {
            get
            {
                return m_nameFileIni;
            }
            set
            {
                m_nameFileIni = value;
                m_path = Environment.CurrentDirectory + "\\" + m_nameFileIni;
            }
        }
    }
}
