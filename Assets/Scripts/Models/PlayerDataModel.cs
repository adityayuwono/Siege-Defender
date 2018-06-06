﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	[XmlRoot("PlayerData")]
	public class PlayerDataModel : BaseModel
	{
		[XmlElement(ElementName = "Inventory", Type = typeof(InventoryModel))]
		public List<InventoryModel> Inventories { get; set; }
	}
}