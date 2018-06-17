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
		}

		[XmlAttribute]
		public string BaseItem { get; set; }

		[XmlAttribute]
		[DefaultValue(1)]
		public int Quantity { get; set; }

		[XmlIgnore]
		public string ItemSlotRoots { get; set; }
	}
}