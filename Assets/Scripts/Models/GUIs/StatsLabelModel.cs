using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	public class StatsLabelModel : StaticModel
	{
		[XmlAttribute]
		public string Source { get; set; }
	}
}
