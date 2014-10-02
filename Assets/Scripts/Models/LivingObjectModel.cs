using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class LivingObjectModel : ObjectModel
    {
        [XmlAttribute]
        public int Health { get; set; }

        [XmlAttribute]
        public string CollisionEffectNormal { get; set; }

        public int ProjectileLimit { get; protected set; }

        public LivingObjectModel()
        {
            ProjectileLimit = 3;
        }
    }
}
