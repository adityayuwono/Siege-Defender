using System.Xml.Serialization;

namespace Scripts.Models.Levels
{
	public class SpawnIntervalModel : SpawnModel
	{
		[XmlAttribute]
		public float Delay { get; set; }

		[XmlAttribute]
		public float Interval { get; set; }

		[XmlAttribute]
		public int LoopCount { get; set; }
	}
}
