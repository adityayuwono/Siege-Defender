using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	[Serializable]
	public class ShooterModel : BaseGUIModel
	{
		[XmlAttribute]
		public string ShooterTarget { get; set; }

		[XmlAttribute]
		public string AimingAssetId { get; set; }
	}
}