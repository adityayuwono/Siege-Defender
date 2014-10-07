using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class SkillModel : BaseModel
    {
        [XmlAttribute]
        public bool IsQueuedable { get; set; }

        [XmlAttribute]
        public bool IsInterrupt { get; set; }

        [XmlElement(ElementName = "Setter", Type = typeof(SetterActionModel))]
        [XmlElement(ElementName = "MoveAction", Type = typeof(MoveActionModel))]
        public List<BaseActionModel> Actions { get; set; }
    }
}
