using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class Limb_Model : EnemyBaseModel
    {
        [XmlElement]
        public Triggered_Model Trigger { get; set; }
    }
}
