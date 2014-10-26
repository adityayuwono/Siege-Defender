using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class TriggeredModel : BaseModel
    {
        [XmlElement(ElementName = "Condition", Type = typeof(ValueConditionModel))]
        [XmlElement(ElementName = "RandomCondition", Type = typeof(RandomConditionModel))]
        public List<BaseConditionModel> Conditions { get; set; }

        [XmlElement(ElementName = "LoadScene", Type = typeof(LoadSceneActionModel))]
        [XmlElement(ElementName = "Setter", Type = typeof(SetterActionModel))]
        [XmlElement(ElementName = "MoveAction", Type = typeof(MoveActionModel))]
        public List<BaseActionModel> Actions { get; set; }

        [XmlAttribute]
        [DefaultValue(true)]
        public bool TriggerOnce;

        public TriggeredModel()
        {
            TriggerOnce = true;
        }
    }

    [Serializable]
    public class EventTriggeredModel : TriggeredModel
    {
        [XmlAttribute]
        public Event Event { get; set; }
    }

    public enum Event
    {
        None,
        Interrupt,
        Click,
        Break,
        Spawn,
        Attack,
        GameOver
    }
}
