using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Interval_Model : Element_Model
    {
        [XmlAttribute]
        public float Interval { get; set; }
    }
}
