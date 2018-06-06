using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	public class ProjectileItemModel : ItemModel
	{
		[XmlElement]
		public ProjectileEnchantments Enchantments { get; set; }
	}
}