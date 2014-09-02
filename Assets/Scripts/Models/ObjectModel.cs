﻿using System;
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
        [XmlArrayItem(ElementName = "Element", Type = typeof(ElementModel))]
        [XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
        [XmlArrayItem(ElementName = "Player", Type = typeof(PlayerModel))]
        [XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitboxModel))]
        [XmlArrayItem(ElementName = "GUIRoot", Type = typeof(GUIRootModel))]
        [XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplayModel))]
        public List<ElementModel> Elements = new List<ElementModel>();

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
