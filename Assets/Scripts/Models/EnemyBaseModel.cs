using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class EnemyBaseModel : LivingObjectModel
    {
        [XmlAttribute]
        public float Speed { get; set; }

        [XmlElement]
        public ProjectileModel Projectile { get; set; }

        [XmlAttribute]
        public float Rotation { get; set; }
    }
}
