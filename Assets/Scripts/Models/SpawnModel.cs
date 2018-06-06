using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
	public class SpawnModel : BaseModel
	{
		public SpawnModel()
		{
			Count = 1;
			SpawnIndexOverride = -1;
		}

		[XmlAttribute]
		[DefaultValue(1)]
		public int Count { get; set; }

		[XmlAttribute]
		public string EnemyId { get; set; }

		[XmlAttribute]
		[DefaultValue(-1)]
		public int SpawnIndexOverride { get; set; }
	}
}