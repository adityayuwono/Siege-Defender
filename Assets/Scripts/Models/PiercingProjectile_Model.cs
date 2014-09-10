using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class PiercingProjectile_Model : Projectile_Model
    {
        [XmlAttribute]
        [DefaultValue(0.75f)]
        public float DamageReduction { get; set; }

        public PiercingProjectile_Model()
        {
            DamageReduction = 0.75f;
        }
    }
}
