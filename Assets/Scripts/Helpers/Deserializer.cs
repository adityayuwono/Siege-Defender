using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Scripts.Models;

namespace Scripts.Helpers
{
    // TODO: Implement encryption

    public static class Deserializer<T> where T : class
    {
        public static T GetObjectFromXML(string xml)
        {
            var reader = new StringReader(xml);
            var deserializer = new XmlSerializer(typeof(T));
            var xmlFromText = new XmlTextReader(reader);
            var obj = (T)deserializer.Deserialize(xmlFromText);
            reader.Close();
            return obj;
        }
    }

    public static class Serializer
    {
        public static void SaveObjectToXML(PlayerSettingsModel model)
        {
            var serializer = new XmlSerializer(typeof(PlayerSettingsModel));

            var sw = new StreamWriter(FilePaths.Saving + Values.Defaults.PlayerProgressFileName);
            serializer.Serialize(sw, model);
            sw.Close();
        }
    }
}
