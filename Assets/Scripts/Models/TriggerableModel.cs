using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
	[Serializable]
	public class TriggerableModel : BaseModel
	{
		[XmlArray(ElementName = "Triggers")]
		[XmlArrayItem(Type = typeof(TriggeredModel), ElementName = "Trigger")]
		[XmlArrayItem(Type = typeof(EventTriggeredModel), ElementName = "EventTrigger")]
		public List<TriggeredModel> TriggersSerialized
		{
			get { return Triggers != null ? (Triggers.Count > 0 ? Triggers : null) : null; }
			set { Triggers = value; }
		}

		[XmlIgnore] public List<TriggeredModel> Triggers { get; private set; }
	}
}