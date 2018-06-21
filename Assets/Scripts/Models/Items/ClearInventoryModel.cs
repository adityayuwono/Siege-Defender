using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Items
{
	public class ClearInventoryModel : BaseActionModel
	{
		[XmlAttribute]
		public bool SellItems { get; set; }
	}
}
