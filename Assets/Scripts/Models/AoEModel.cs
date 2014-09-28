using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class AoEModel : ProjectileModel
    {
        [XmlAttribute]
        public float Radius { get; set; }

        public AoEModel()
        {
            DeathDelay = 1f;
        }
    }
}
