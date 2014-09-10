using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class IntervalModel : Element_Model
    {
        [XmlAttribute]
        public float Interval { get; set; }
    }
}
