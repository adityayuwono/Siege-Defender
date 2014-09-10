using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    [XmlRoot("Shooter")]
    public class ShooterModel : Interval_Model
    {
        [XmlAttribute]
        public string ProjectileId { get; set; }

        [XmlElement]
        public Object_Model Source { get; set; }

        [XmlElement]
        public TargetModel Target { get; set; }

        [XmlAttribute]
        public int Index { get; set; }
    }
}
