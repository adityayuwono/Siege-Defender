using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class SpawnActionModel : BaseActionModel
	{
		[XmlAttribute]
		public string EnemyId { get; set; }
	}
}
