using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class ItemsSlotModel : DropableSlotsModel
    {
		public ItemsSlotModel()
		{
			Slots = 36;
			Items = new List<ItemModel>();
		}

		[XmlAttribute]
		public int Slots { get; set; }

		[XmlElement(ElementName = "Item", Type = typeof(ItemModel))]
		[XmlElement(ElementName = "Projectile", Type = typeof(ProjectileItemModel))]
		[XmlElement(ElementName = "Enchantment", Type = typeof(EnchantmentItemModel))]
		public List<ItemModel> Items { get; set; }
	}
}
