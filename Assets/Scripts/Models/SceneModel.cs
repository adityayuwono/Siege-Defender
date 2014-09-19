using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class SceneModel : ObjectModel
    {
        [XmlAttribute]
        public string Scene { get; set; }
    }
}