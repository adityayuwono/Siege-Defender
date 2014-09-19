using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class LimbModel : EnemyBaseModel
    {
        [XmlElement]
        public TriggeredModel Trigger { get; set; }
    }
}
