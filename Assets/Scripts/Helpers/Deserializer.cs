using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace CEP.Core
{
    public class Deserializer<T> where T : class
    {
        public static T GetObject(string xmlPath)
        {
            var deserializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(xmlPath);
            var obj = (T)deserializer.Deserialize(textReader);
            textReader.Close();

            return obj;
        }

        public static T GetObjectFromXML(string xml)
        {
            var reader = new StringReader(xml);
            var deserializer = new XmlSerializer(typeof(T));
            var xmlFromText = new XmlTextReader(reader); // We need to use XmlTextReader for AOT support on iOS
            var obj = (T)deserializer.Deserialize(xmlFromText);
            reader.Close();
            return obj;
        }

        public static T GetObjectFromXML(string xml, string rootName)
        {
            var xRoot = new XmlRootAttribute { ElementName = rootName };

            var reader = new StringReader(xml);
            var deserializer = new XmlSerializer(typeof(T), xRoot);
            var xmlFromText = new XmlTextReader(reader); // We need to use XmlTextReader for AOT support on iOS
            var obj = (T)deserializer.Deserialize(xmlFromText);
            reader.Close();
            return obj;
        }

        public static object GetObjectFromXML(string xml, Type type)
        {
            var reader = new StringReader(xml);
            var deserializer = new XmlSerializer(type);
            var xmlFromText = new XmlTextReader(reader); // We need to use XmlTextReader for AOT support on iOS
            var obj = deserializer.Deserialize(xmlFromText);
            reader.Close();
            return obj;
        }
    }
}
