using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class EnemyBase_Model : Object_Model
    {
        [XmlAttribute]
        public int Health { get; set; }

        [XmlAttribute]
        public float Speed { get; set; }

        [XmlElement]
        public Projectile_Model Projectile { get; set; }

        [XmlAttribute]
        public float Rotation { get; set; }
    }
}
