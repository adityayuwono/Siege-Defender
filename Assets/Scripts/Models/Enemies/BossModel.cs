using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class BossModel : EnemyBaseModel
    {
        [XmlElement(ElementName = "Limb", Type = typeof(LimbModel))]
        public List<LimbModel> Limbs { get; set; }


        [XmlElement(ElementName = "Skill", Type = typeof(SkillModel))]
        public List<SkillModel> Skills { get; set; }


        [XmlElement(ElementName = "Trigger", Type = typeof(TriggeredModel))]
        public List<TriggeredModel> Triggers { get; set; } 
    }
}
