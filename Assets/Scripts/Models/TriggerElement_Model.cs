using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
    [Serializable]
    public class TriggerElement_Model : Base_Model
    {
        [XmlElement]
        public Triggered_Model Trigger { get; set; }
    }
}
