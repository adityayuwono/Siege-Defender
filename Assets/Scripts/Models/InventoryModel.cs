using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Weapons;

namespace Scripts.Models
{
	[XmlRoot("Inventory")]
	[Serializable]
	public class InventoryModel : ElementModel
	{
		public InventoryModel()
		{
			Slots = 36;
			Items = new List<ItemModel>();
			EquipmentSlots = new List<EquipmentSlotModel>();
		}

		[XmlAttribute] public string Source { get; set; }

		[XmlAttribute] public int Slots { get; set; }

		[XmlElement(ElementName = "Item", Type = typeof(ItemModel))]
		[XmlElement(ElementName = "Projectile", Type = typeof(ProjectileItemModel))]
		public List<ItemModel> Items { get; set; }

		[XmlElement(ElementName = "EquipmentSlot", Type = typeof(EquipmentSlotModel))]
		public List<EquipmentSlotModel> EquipmentSlots { get; set; }
	}

	[Serializable]
	public class ItemModel : ObjectModel
	{
		public ItemModel()
		{
			Level = 1;
		}

		[XmlAttribute] public string BaseItem { get; set; }

		[XmlAttribute]
		[DefaultValue(1)]
		public int Level { get; set; }
	}

	[Serializable]
	public class ProjectileItemModel : ItemModel
	{
		[XmlElement] public ProjectileEnchantments Enchantments { get; set; }
	}

	[Serializable]
	public class ProjectileEnchantments : ProjectileModel
	{
	}
}