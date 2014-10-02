using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Enemies;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [XmlRoot("Engine")]
    public class EngineModel : BaseModel
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Enemy", Type = typeof(EnemyBaseModel))]
        [XmlArrayItem(ElementName = "Object", Type = typeof(ObjectModel))]
        [XmlArrayItem(ElementName = "SpecialEffect", Type = typeof(SpecialEffectModel))]
        [XmlArrayItem(ElementName = "Boss", Type = typeof(BossModel))]
        [XmlArrayItem(ElementName = "Projectile", Type = typeof(ProjectileModel))]
        [XmlArrayItem(ElementName = "Piercing", Type = typeof(PiercingProjectileModel))]
        [XmlArrayItem(ElementName = "AoE", Type = typeof(AoEModel))]
        [XmlArrayItem(ElementName = "ParticleAoE", Type = typeof(ParticleAoEModel))]
        [XmlArrayItem(ElementName = "DamageGUI", Type = typeof(DamageGUIModel))]
        public List<ObjectModel> Objects { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Level", Type = typeof(LevelModel))]
        public List<LevelModel> Levels { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Scene", Type = typeof(SceneModel))]
        public List<SceneModel> Scenes { get; set; }
    }
}
