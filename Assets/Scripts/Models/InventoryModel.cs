﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [XmlRoot("Inventory")]
    [Serializable]
    public class InventoryModel : ElementModel
    {
        [XmlAttribute]
        public int Slots { get; set; }

        [XmlElement(ElementName = "Item", Type = typeof(ItemModel))]
        public List<ItemModel> Items { get; set; }

        [XmlElement(ElementName = "EquipmentSlot", Type = typeof(EquipmentSlotModel))]
        public List<EquipmentSlotModel> EquipmentSlots { get; set; } 

        public InventoryModel()
        {
            Slots = 36;
            Items = new List<ItemModel>();
            EquipmentSlots = new List<EquipmentSlotModel>();
        }
    }

    [Serializable]
    public class ItemModel : ObjectModel
    {
        [XmlAttribute]
        public string Base { get; set; }

        [XmlAttribute]
        public int Level { get; set; }
    }
}