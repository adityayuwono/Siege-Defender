using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
	[Serializable]
	public class SkillModel : TriggerableModel
	{
		[XmlAttribute] [DefaultValue(false)] public bool IsQueuedable { get; set; }

		[XmlAttribute] [DefaultValue(false)] public bool IsInterrupt { get; set; }

		[XmlAttribute] [DefaultValue(0)] public float InterruptThreshold { get; set; }

		[XmlElement(ElementName = "Setter", Type = typeof(SetterActionModel))]
		[XmlElement(ElementName = "MoveAction", Type = typeof(MoveActionModel))]
		[XmlElement(ElementName = "SpecialEvent", Type = typeof(StartSpecialEventModel))]
		[XmlElement(ElementName = "SpecialEffect", Type = typeof(SpecialEffectActionModel))]
		public List<BaseActionModel> Actions { get; set; }
	}
}