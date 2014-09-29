using System;
using System.Collections.Generic;
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
        public List<BaseActionModel> Actions { get; set; }
    }
}
