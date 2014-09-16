using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class Boss_Model : EnemyBase_Model
    {
        [XmlElement(ElementName = "Limb", Type = typeof(Limb_Model))]
        public List<Limb_Model> Limbs { get; set; }


        [XmlElement(ElementName = "Skill", Type = typeof(Skill_Model))]
        public List<Skill_Model> Skills { get; set; }


        [XmlElement(ElementName = "Phase", Type = typeof(Phase_Model))]
        public List<Phase_Model> Phases { get; set; } 
    }
}
