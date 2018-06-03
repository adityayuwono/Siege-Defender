using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class EnemyModel : LivingObjectModel
    {
        [XmlAttribute]
        public string Target { get; set; }

        [XmlAttribute]
        public float Speed { get; set; }

        [XmlElement]
        public ProjectileModel Projectile { get; set; }

        [XmlAttribute]
        public float Rotation { get; set; }



        [XmlAttribute]
        public string Damage { get; set; }

        [XmlAttribute]
        public float AttackSpeed { get; set; }



        public EnemyModel()
        {
            DeathDelay = 2f;
        }
    }
}
