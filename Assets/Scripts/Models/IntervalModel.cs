using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class IntervalModel : ElementModel
    {
        [XmlAttribute]
        public float Interval { get; set; }
    }
}
