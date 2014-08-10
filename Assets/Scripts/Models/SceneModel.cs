using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    public class SceneModel : ObjectModel
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
        [XmlArrayItem(ElementName = "Player", Type = typeof(PlayerModel))]
        [XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitboxModel))]
        public List<ElementModel> Elements = new List<ElementModel>();
    }
}
