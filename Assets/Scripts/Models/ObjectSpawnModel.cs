using System.Xml.Serialization;

namespace Scripts.Models
{
	public class ObjectSpawnModel : IntervalModel
	{
		[XmlAttribute] public string LevelId { get; set; }
	}
}