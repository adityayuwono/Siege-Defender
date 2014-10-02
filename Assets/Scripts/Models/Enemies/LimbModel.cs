using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class LimbModel : LivingObjectModel
    {
        [XmlElement]
        public TriggeredModel Trigger { get; set; }

        [XmlAttribute]
        public string CollisionEffectBroken { get; set; }

        [XmlAttribute]
        [DefaultValue(1f)]
        public float DamageMultiplier { get; set; }

        public LimbModel()
        {
            DamageMultiplier = 1f;
            ProjectileLimit = 2;
        }
    }
}
