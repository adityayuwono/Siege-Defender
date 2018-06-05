using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Enemies
{
	public class LootModel : BaseModel
	{
		public LootModel()
		{
			Max = int.MaxValue;
		}

		[XmlAttribute] public float Chance { get; set; }

		[XmlAttribute] public string ItemId { get; set; }

		[XmlAttribute]
		[DefaultValue(int.MaxValue)]
		public int Max { get; set; }
	}
}