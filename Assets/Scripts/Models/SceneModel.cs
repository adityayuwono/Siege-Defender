using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class SceneModel : Object_Model
    {
        [XmlAttribute]
        public string Scene { get; set; }
    }
}