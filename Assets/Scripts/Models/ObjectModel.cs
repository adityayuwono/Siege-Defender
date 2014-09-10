﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [Serializable]
    public class ObjectModel : Base_Model
    {
        // Not XML Property, this is set when we spawn it
        public string Type;

        [XmlArray]
        // 3D Objects
        [XmlArrayItem(ElementName = "Element", Type = typeof(ElementModel))]
        [XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
        [XmlArrayItem(ElementName = "Player", Type = typeof(PlayerModel))]
        [XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitboxModel))]
        [XmlArrayItem(ElementName = "GUIRoot", Type = typeof(GUIRootModel))]
        [XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplayModel))]
        [XmlArrayItem(ElementName = "ObjectDisplay", Type = typeof(ObjectDisplay_Model))]
        // GUIs
        [XmlArrayItem(ElementName = "Inventory", Type = typeof(Inventory_Model))]
        [XmlArrayItem(ElementName = "Button", Type = typeof(Button_Model))]
        public List<ElementModel> Elements { get; set; }

        [XmlAttribute]
        public string AssetId { get; set; }

        [XmlAttribute]
        [DefaultValue("0,0,0")]
        public string Position { get; set; }

        public ObjectModel()
        {
            Position = "0,0,0";
        }
    }
}
