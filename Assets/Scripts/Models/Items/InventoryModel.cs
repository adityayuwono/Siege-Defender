using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[XmlRoot("Inventory")]
	[Serializable]
	public class InventoryModel : ItemsSlotModel
	{
		public InventoryModel()
		{
			EquipmentSlots = new List<EquipmentSlotModel>();
		}

		[XmlAttribute]
		public string Source { get; set; }

		[XmlElement(ElementName = "EquipmentSlot", Type = typeof(EquipmentSlotModel))]
		[XmlElement(ElementName = "WeaponSlot", Type = typeof(WeaponSlotModel))]
		public List<EquipmentSlotModel> EquipmentSlots { get; set; }

		[XmlAttribute]
		[DefaultValue(false)]
		public bool AlwaysTransferFromSlots { get; set; }
	}
}