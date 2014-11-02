using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class BossModel : EnemyBaseModel
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Limb", Type = typeof(LimbModel))]
        public List<LimbModel> Limbs { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Skill", Type = typeof(SkillModel))]
        public List<SkillModel> Skills { get; set; }

        public BossModel()
        {
            Limbs = new List<LimbModel>();
            Skills = new List<SkillModel>();
        }
    }
}
