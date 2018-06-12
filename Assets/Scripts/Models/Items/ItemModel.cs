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
			Level = 1;
			ItemSlotRoots = "ItemSlot";
		}

		[XmlAttribute]
		public string BaseItem { get; set; }

		[XmlAttribute]
		[DefaultValue(1)]
		public int Level { get; set; }

		[XmlIgnore]
		public string ItemSlotRoots { get; set; }
	}
}