using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	public class BaseModel
	{
		[XmlAttribute]
		public string Id { get; set; }
	}
}