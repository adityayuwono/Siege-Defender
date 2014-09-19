using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
    [Serializable]
    public class TriggerElementModel : BaseModel
    {
        [XmlElement]
        public TriggeredModel Trigger { get; set; }
    }
}
