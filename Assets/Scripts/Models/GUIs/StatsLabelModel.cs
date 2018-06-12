using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	public class StatsLabelModel : StaticGUIModel
	{
		[XmlAttribute]
		public string Source { get; set; }
	}
}
