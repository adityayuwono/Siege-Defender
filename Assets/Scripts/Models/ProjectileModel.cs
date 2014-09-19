using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class ProjectileModel : ObjectModel
    {
        [XmlAttribute]
        public string BulletAssetId { get; set; }


        [XmlAttribute][DefaultValue("1")]
        public string Damage { get; set; }

        [XmlAttribute][DefaultValue(1f)]
        public float RoF { get; set; }

        [XmlAttribute]
        public string AoEId { get; set; }

        [XmlAttribute]
        [DefaultValue(-1)]
        public int Ammunition { get; set; }

        [XmlAttribute]
        [DefaultValue(3f)]
        public float ReloadTime { get; set; }

        [XmlAttribute]
        [DefaultValue(0.9f)]
        public float Accuracy { get; set; }

        [XmlAttribute]
        [DefaultValue(0f)]
        public float Deviation { get; set; }

        public ProjectileModel()
        {
            Ammunition = -1;
            ReloadTime = 3f;
            Accuracy = 0.9f;
            Deviation = 0f;
            Damage = "1";
            RoF = 1f;
        }
    }
}
