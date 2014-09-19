using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class EnemyBaseModel : ObjectModel
    {
        [XmlAttribute]
        public int Health { get; set; }

        [XmlAttribute]
        public float Speed { get; set; }

        [XmlElement]
        public ProjectileModel Projectile { get; set; }

        [XmlAttribute]
        public float Rotation { get; set; }
    }
}
