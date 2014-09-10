using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class PlayerModel : Element_Model
    {
        [XmlElement]
        public PlayerHitboxModel PlayerHitbox { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Shooter", Type = typeof(ShooterModel))]
        public List<ShooterModel> Shooters { get; set; }

        public PlayerModel()
        {
            Shooters = new List<ShooterModel>();
        }
    }
}
