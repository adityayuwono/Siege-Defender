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

        [XmlElement(ElementName = "LoadScene", Type = typeof(LoadSceneActionModel))]
        [XmlElement(ElementName = "Setter", Type = typeof(SetterActionModel))]
        public List<BaseActionModel> Actions { get; set; }
    }
}
