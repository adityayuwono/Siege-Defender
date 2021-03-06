using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	[XmlRoot("Shooter")]
	public class ShooterModel : IntervalModel
	{
		[XmlAttribute]
		public string ProjectileId { get; set; }

		[XmlElement]
		public TargetModel Target { get; set; }

		[XmlAttribute]
		public string Index { get; set; }
	}
}