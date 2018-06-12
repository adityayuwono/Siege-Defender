using System;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class EquipmentSlotModel : ElementModel
	{
		[XmlElement(ElementName = "Item", Type = typeof(ItemModel))]
		[XmlElement(ElementName = "Projectile", Type = typeof(ProjectileItemModel))]
		public ItemModel Item { get; set; }
	}
}