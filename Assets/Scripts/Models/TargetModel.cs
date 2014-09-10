using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class TargetModel : Object_Model
    {
        [XmlAttribute]
        public int Index { get; set; }
    }
}
