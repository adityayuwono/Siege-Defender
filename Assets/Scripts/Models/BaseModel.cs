using Scripts.Interfaces;
using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	public class BaseModel : IHaveId
	{
		[XmlAttribute]
		public string Id { get; set; }
	}
}