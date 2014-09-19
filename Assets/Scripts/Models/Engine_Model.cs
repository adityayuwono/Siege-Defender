using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Enemies;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [XmlRoot("Engine")]
    public class Engine_Model : Base_Model
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Enemy", Type = typeof(EnemyBaseModel))]
        [XmlArrayItem(ElementName = "Boss", Type = typeof(BossModel))]
        [XmlArrayItem(ElementName = "Projectile", Type = typeof(Projectile_Model))]
        [XmlArrayItem(ElementName = "Piercing", Type = typeof(PiercingProjectile_Model))]
        [XmlArrayItem(ElementName = "AoE", Type = typeof(AoE_Model))]
        [XmlArrayItem(ElementName = "ParticleAoE", Type = typeof(ParticleAoE_Model))]
        [XmlArrayItem(ElementName = "DamageGUI", Type = typeof(Damage_GUIModel))]
        public List<Object_Model> Objects { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Level", Type = typeof(Level_Model))]
        public List<Level_Model> Levels { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Scene", Type = typeof(Scene_Model))]
        public List<Scene_Model> Scenes { get; set; }
    }
}
