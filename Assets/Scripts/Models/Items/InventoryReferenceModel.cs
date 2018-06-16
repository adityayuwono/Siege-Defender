using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	public class InventoryReferenceModel : ObjectModel
	{
		[XmlAttribute]
		public string Source { get; set; }
	}
}
