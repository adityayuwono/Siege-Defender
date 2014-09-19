using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [Serializable]
    public class Object_Model : Base_Model
    {
        // Not XML Property, this is set when we spawn it
        public string Type;

        [XmlArray]
        [DefaultValue(default(List<Element_Model>))]
        // 3D Objects
        [XmlArrayItem(ElementName = "Element", Type = typeof(Element_Model))]
        [XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManager_Model))]
        [XmlArrayItem(ElementName = "Player", Type = typeof(Player_Model))]
        [XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitbox_Model))]
        [XmlArrayItem(ElementName = "GUIRoot", Type = typeof(Root_GUIModel))]
        [XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplay_GUIModel))]
        [XmlArrayItem(ElementName = "ObjectDisplay", Type = typeof(ObjectDisplay_Model))]
        // GUIs
        [XmlArrayItem(ElementName = "Inventory", Type = typeof(Inventory_Model))]
        [XmlArrayItem(ElementName = "Button", Type = typeof(Button_GUIModel))]
        public List<Element_Model> Elements { get; set; }

        [XmlAttribute]
        public string AssetId { get; set; }

        [XmlAttribute]
        [DefaultValue("0,0,0")]
        public string Position { get; set; }

        public Object_Model()
        {
            Position = "0,0,0";
        }
    }
}
