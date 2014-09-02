using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [XmlRoot("Engine")]
    public class EngineModel : BaseModel
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Enemy", Type = typeof(EnemyBaseModel))]
        [XmlArrayItem(ElementName = "Projectile", Type = typeof(ProjectileModel))]
        [XmlArrayItem(ElementName = "AoE", Type = typeof(AoEModel))]
        [XmlArrayItem(ElementName = "ParticleAoE", Type = typeof(ParticleAoEModel))]
        [XmlArrayItem(ElementName = "DamageGUI", Type = typeof(DamageGUIModel))]
        public List<ObjectModel> Objects { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Level", Type = typeof(LevelModel))]
        public List<LevelModel> Levels { get; set; }


        [XmlElement]
        public PlayerModel Player { get; set; }

        [XmlElement]
        public EnemyManagerModel EnemyManager { get; set; }

        [XmlElement]
        public SceneModel Scene { get; set; }
    }
}
