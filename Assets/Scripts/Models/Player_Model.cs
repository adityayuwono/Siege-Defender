using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Player_Model : Element_Model
    {
        [XmlElement]
        public PlayerHitbox_Model PlayerHitbox { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Shooter", Type = typeof(Shooter_Model))]
        public List<Shooter_Model> Shooters { get; set; }

        public Player_Model()
        {
            Shooters = new List<Shooter_Model>();
        }
    }
}
