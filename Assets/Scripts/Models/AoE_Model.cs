using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class AoE_Model : Projectile_Model
    {
        [XmlAttribute]
        public float Radius { get; set; }
    }
}
