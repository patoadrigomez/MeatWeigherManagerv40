using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace SerializerClass
{
    /// <summary>
    /// CSerializerClass
    /// Permite salvar o recuperar los datos de una clase por el metodo de 
    /// serializacion XML 
    /// Si se decea que siertos mientros de la clase a guardar no sean tenidos
    /// en cuenta para el salvado o lectura, se debe poner el comando [NonSerialized]
    /// delante de la declaracion de la variable miembro.
    /// </summary>
    public class CSerializerClass
    {
        string m_pathFile;
        string m_nameFile;
        object m_classToSerializer;
        XmlSerializer m_xmlSerialization;

        public string PathFile
        {
            get { return m_pathFile; }
            set { m_pathFile = value; }
        }
        public string NameFile
        {
            get { return m_nameFile; }
            set { m_nameFile = value; }
        }

        public object ObjectClass
        {
            get { return m_classToSerializer; }
        }

        public CSerializerClass(string pathFile, string nameFile, object classToSerializer )
        {
            m_pathFile = pathFile;
            m_nameFile = nameFile;
            m_classToSerializer = classToSerializer;
            try
            {
                m_xmlSerialization = new XmlSerializer(m_classToSerializer.GetType());
            }
            catch (XmlException e)
            {
                throw(new Exception("Error Creando la Clase de Serializacion XML: "+ e.Message));
            }
        }

        public void Serialize()
        {
            Stream stream = null;
            try
            {
                stream = File.Open(m_pathFile + "\\" + m_nameFile, FileMode.OpenOrCreate);
                m_xmlSerialization.Serialize(stream, m_classToSerializer);
                stream.Close();
            }
            catch (XmlException e)
            {
                throw (new Exception("Error Salvando Datos de la Aplicacion por Serializacion XML: " + e.Message));
                stream.Close();
            }
            catch (IOException ioe)
            {
                throw (new Exception("Error Salvando Datos de la Aplicacion por Serializacion XML: " + ioe.Message));
            }
        }

        public void Deserialize()
        {
            Stream stream = null;
            try
            {
                stream = File.Open(m_pathFile + "\\" + m_nameFile, FileMode.OpenOrCreate);
                if (stream.Length == 0)
                {   // el archivo no fue encontrado y se creo uno vacio.
                    stream.Close();
                    Serialize();
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(stream);
                    XmlNodeReader xmlNderd = new XmlNodeReader(xmlDoc);
                    m_classToSerializer = m_xmlSerialization.Deserialize(xmlNderd);
                    stream.Close();
                }
            }
            catch (XmlException e)
            {
                stream.Close();
                System.IO.File.WriteAllText(m_pathFile + "\\" + m_nameFile, string.Empty);
                Serialize();
            }
            catch (IOException ioe)
            {
                throw (new Exception("Error Recuperando Datos de la Aplicacion por Serializacion XML: " + ioe.Message));
            }
        }
    }
}
