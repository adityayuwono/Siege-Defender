using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	public class DamageDisplayModel : IntervalModel
	{
		[XmlAttribute]
		public string DamageGUI { get; set; }

		[XmlAttribute]
		public string HealthBarGUI { get; set; }
	}
}