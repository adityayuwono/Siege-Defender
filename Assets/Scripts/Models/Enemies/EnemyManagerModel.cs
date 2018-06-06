using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	public class EnemyManagerModel : IntervalModel
	{
		[XmlAttribute]
		public string LevelId { get; set; }
	}
}