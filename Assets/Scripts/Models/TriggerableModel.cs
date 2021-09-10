using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
	[Serializable]
	public class TriggerableModel : BaseModel
	{
		[XmlArrayItem(Type = typeof(TriggeredModel), ElementName = "Trigger")]
		[XmlArrayItem(Type = typeof(EventTriggeredModel), ElementName = "EventTrigger")]
		public List<TriggeredModel> Triggers { get; set; }
	}
}