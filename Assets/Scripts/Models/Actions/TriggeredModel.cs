using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class TriggeredModel : BaseModel
	{
		[XmlAttribute]
		[DefaultValue(true)]
		public bool TriggerOnce;

		public TriggeredModel()
		{
			TriggerOnce = true;
		}

		[XmlElement(ElementName = "Condition", Type = typeof(ValueConditionModel))]
		[XmlElement(ElementName = "RandomCondition", Type = typeof(RandomConditionModel))]
		public List<BaseConditionModel> Conditions { get; set; }

		[XmlElement(ElementName = "LoadScene", Type = typeof(LoadSceneActionModel))]
		[XmlElement(ElementName = "Setter", Type = typeof(SetterActionModel))]
		[XmlElement(ElementName = "MoveAction", Type = typeof(MoveActionModel))]
		[XmlElement(ElementName = "SpecialEvent", Type = typeof(StartSpecialEventModel))]
		[XmlElement(ElementName = "SpecialEffect", Type = typeof(SpecialEffectActionModel))]
		public List<BaseActionModel> Actions { get; set; }
	}
}