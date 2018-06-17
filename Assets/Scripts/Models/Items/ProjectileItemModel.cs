using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class ProjectileItemModel : ItemModel
	{
		public ProjectileItemModel()
		{
			Level = 1;
		}

		[XmlAttribute]
		[DefaultValue(1)]
		public int Level { get; set; }

		[XmlElement]
		public EnchantmentItemModel Enchantment { get; set; }
	}
}