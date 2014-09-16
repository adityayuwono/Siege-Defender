using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class Limb_Model : EnemyBase_Model
    {
        [XmlElement]
        public Triggered_Model Trigger { get; set; }
    }
}
