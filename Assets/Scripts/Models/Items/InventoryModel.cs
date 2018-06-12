using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Items
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

		[XmlAttribute]
		public string Source { get; set; }

		[XmlAttribute]
		public int Slots { get; set; }

		[XmlElement(ElementName = "Item", Type = typeof(ItemModel))]
		[XmlElement(ElementName = "Projectile", Type = typeof(ProjectileItemModel))]
		[XmlElement(ElementName = "Enchantment", Type = typeof(EnchantmentItemModel))]
		public List<ItemModel> Items { get; set; }

		[XmlElement(ElementName = "EquipmentSlot", Type = typeof(EquipmentSlotModel))]
		[XmlElement(ElementName = "WeaponSlot", Type = typeof(WeaponSlotModel))]
		public List<EquipmentSlotModel> EquipmentSlots { get; set; }
	}
}