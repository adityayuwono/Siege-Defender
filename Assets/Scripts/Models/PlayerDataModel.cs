using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Items;

namespace Scripts.Models
{
	[Serializable]
	[XmlRoot("PlayerData")]
	public class PlayerDataModel : BaseModel
	{
		[XmlAttribute]
		public int Money { get; set; }

		[XmlElement(ElementName = "Inventory", Type = typeof(InventoryModel))]
		public List<InventoryModel> Inventories { get; set; }
	}
}