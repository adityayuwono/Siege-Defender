using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class SpecialEffectActionModel : SetterActionModel
	{
		public SpecialEffectActionModel()
		{
			Target = "{This.SpecialEffect}";
		}

		[XmlAttribute]
		public string SpecialEffectId { get; set; }
	}
}