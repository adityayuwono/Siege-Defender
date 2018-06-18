using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class ItemModel : ObjectModel
	{
		public ItemModel()
		{
			ItemSlotRoots = "ItemSlot";
			Quantity = 1;
		}

		[XmlAttribute]
		public string BaseItem { get; set; }

		[XmlAttribute]
		[DefaultValue(1)]
		public int Quantity { get; set; }

		[XmlAttribute]
		public int Price { get; set; }

		[XmlIgnore]
		public string ItemSlotRoots { get; set; }
	}
}