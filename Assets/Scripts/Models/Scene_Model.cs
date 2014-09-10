using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Scene_Model : Object_Model
    {
        [XmlAttribute]
        public string Scene { get; set; }
    }
}