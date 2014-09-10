using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [XmlRoot("Engine")]
    public class EngineModel : Base_Model
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Enemy", Type = typeof(EnemyBaseModel))]
        [XmlArrayItem(ElementName = "Projectile", Type = typeof(ProjectileModel))]
        [XmlArrayItem(ElementName = "Piercing", Type = typeof(PiercingProjectile_Model))]
        [XmlArrayItem(ElementName = "AoE", Type = typeof(AoEModel))]
        [XmlArrayItem(ElementName = "ParticleAoE", Type = typeof(ParticleAoEModel))]
        [XmlArrayItem(ElementName = "DamageGUI", Type = typeof(DamageGUIModel))]
        public List<Object_Model> Objects { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Level", Type = typeof(LevelModel))]
        public List<LevelModel> Levels { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Scene", Type = typeof(SceneModel))]
        public List<SceneModel> Scenes { get; set; }
    }
}
