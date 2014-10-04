using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [Serializable]
    public class ObjectModel : BaseModel
    {
        // Not XML Property, this is set when we spawn it
        public string Type;

        [XmlArray]
        [DefaultValue(default(List<ElementModel>))]
        // 3D Objects
        [XmlArrayItem(ElementName = "Element", Type = typeof(ElementModel))]
        [XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
        [XmlArrayItem(ElementName = "Player", Type = typeof(PlayerModel))]
        [XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitboxModel))]
        [XmlArrayItem(ElementName = "GUIRoot", Type = typeof(RootGUIModel))]
        [XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplayGUIModel))]
        [XmlArrayItem(ElementName = "ObjectDisplay", Type = typeof(ObjectDisplayModel))]
        // GUIs
        [XmlArrayItem(ElementName = "Inventory", Type = typeof(InventoryModel))]
        [XmlArrayItem(ElementName = "Button", Type = typeof(ButtonGUIModel))]

        [XmlArrayItem(ElementName = "SpecialEffectManager", Type = typeof(SpecialEffectManagerModel))]
        public List<ElementModel> Elements { get; set; }

        [XmlAttribute]
        public string AssetId { get; set; }

        [XmlAttribute]
        [DefaultValue("0,0,0")]
        public string Position { get; set; }

        [XmlAttribute]
        public float DeathDelay { get; set; }

        public ObjectModel()
        {
            DeathDelay = 0;
            Elements = new List<ElementModel>();
            Position = "0,0,0";
        }
    }
}
