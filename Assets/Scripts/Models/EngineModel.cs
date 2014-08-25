using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [XmlRoot("Engine")]
    public class EngineModel : BaseModel
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Enemy", Type = typeof(EnemyBaseModel))]
        [XmlArrayItem(ElementName = "Projectile", Type = typeof(ProjectileModel))]
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
