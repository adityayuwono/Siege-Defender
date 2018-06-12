using System;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class ProjectileItemModel : ItemModel
	{
		[XmlElement]
		public EnchantmentItemModel Enchantment { get; set; }
	}
}