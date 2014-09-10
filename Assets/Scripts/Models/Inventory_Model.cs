using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Inventory_Model : ElementModel
    {
        [XmlElement(ElementName = "Item", Type = typeof(Item_Model))]
        public List<Item_Model> Items { get; set; }

        [XmlElement(ElementName = "EquipmentSlot", Type = typeof(EquipmentSlot_Model))]
        public List<EquipmentSlot_Model> EquipmentSlots { get; set; } 

        public Inventory_Model()
        {
            Items = new List<Item_Model>();
            EquipmentSlots = new List<EquipmentSlot_Model>();
        }
    }

    [Serializable]
    public class Item_Model : ObjectModel
    {
        [XmlAttribute]
        public string Base { get; set; }

        [XmlAttribute]
        public int Level { get; set; }
    }
}
