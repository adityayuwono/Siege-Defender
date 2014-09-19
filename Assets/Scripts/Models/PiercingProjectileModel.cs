using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class PiercingProjectileModel : ProjectileModel
    {
        [XmlAttribute]
        [DefaultValue(0.75f)]
        public float DamageReduction { get; set; }

        public PiercingProjectileModel()
        {
            DamageReduction = 0.75f;
        }
    }
}
