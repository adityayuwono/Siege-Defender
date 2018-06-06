using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class EventTriggeredModel : TriggeredModel
	{
		[XmlAttribute]
		public Event Event { get; set; }
	}
}