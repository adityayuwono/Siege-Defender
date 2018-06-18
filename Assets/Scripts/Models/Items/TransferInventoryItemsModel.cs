using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Items
{
	public class TransferInventoryItemsModel : BaseActionModel
	{
		[XmlAttribute]
		public bool CombineItems { get; set; }
	}
}
