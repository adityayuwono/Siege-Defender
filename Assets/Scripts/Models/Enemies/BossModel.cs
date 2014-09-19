using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class BossModel : EnemyBaseModel
    {
        [XmlElement(ElementName = "Limb", Type = typeof(LimbModel))]
        public List<LimbModel> Limbs { get; set; }


        [XmlElement(ElementName = "Skill", Type = typeof(SkillModel))]
        public List<SkillModel> Skills { get; set; }


        [XmlElement(ElementName = "Phase", Type = typeof(PhaseModel))]
        public List<PhaseModel> Phases { get; set; } 
    }
}
