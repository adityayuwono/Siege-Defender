using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Scripts.Models;
using UnityEngine;

namespace Scripts.Helpers
{
    public static class Deserializer<T> where T : class
    {
        public static T GetObjectFromXML(string xml)
        {
            var reader = new StringReader(xml);
            var deserializer = new XmlSerializer(typeof(T));
            var xmlFromText = new XmlTextReader(reader); // We need to use XmlTextReader for AOT support on iOS
            var obj = (T)deserializer.Deserialize(xmlFromText);
            reader.Close();
            return obj;
        }
    }

    public static class Serializer
    {
        public static void SaveObjectToXML(Engine_Model model)
        {
            var serializer = new XmlSerializer(typeof(Engine_Model));

            var sw = new StreamWriter(FilePaths.Saving + "/Engine.xml");
            serializer.Serialize(sw, model);
            sw.Close();
        }
    }
}
