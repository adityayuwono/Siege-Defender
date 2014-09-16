using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class Triggered_Model : Base_Model
    {
        [XmlElement(ElementName = "Condition", Type = typeof(Base_ConditionModel))]
        public List<Base_ConditionModel> Conditions { get; set; }

        [XmlElement(ElementName = "LoadScene", Type = typeof(LoadScene_ActionModel))]
        [XmlElement(ElementName = "Setter", Type = typeof(Setter_ActionModel))]
        public List<Base_ActionModel> Actions { get; set; }
    }
}
