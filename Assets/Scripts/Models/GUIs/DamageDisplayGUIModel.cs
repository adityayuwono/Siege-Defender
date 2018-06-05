using System.Xml.Serialization;

namespace Scripts.Models
{
	public class DamageDisplayGUIModel : IntervalModel
	{
		[XmlAttribute] public string DamageGUI { get; set; }
	}
}