using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Target_Model : Object_Model
    {
        [XmlAttribute]
        public int Index { get; set; }
    }
}
