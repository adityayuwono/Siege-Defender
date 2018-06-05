using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class RandomConditionModel : ValueConditionModel
	{
		[XmlAttribute] [DefaultValue(1)] public float Frequency { get; set; }
	}
}