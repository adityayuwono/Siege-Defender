using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.Enemies
{
	public class LootTableModel : BaseModel
	{
		public LootTableModel()
		{
			Drops = 1;
			Loots = new List<LootModel>();
		}

		[XmlAttribute]
		public int Drops { get; set; }

		[XmlElement(ElementName = "Loot", Type = typeof(LootModel))]
		public List<LootModel> Loots { get; set; }
	}
}