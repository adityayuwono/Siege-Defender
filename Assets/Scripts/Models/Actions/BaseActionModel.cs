using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class BaseActionModel : TargetPropertyModel
	{
		public BaseActionModel()
		{
			Wait = 0;
		}

		[XmlAttribute] public float Wait { get; set; }

		[XmlAttribute] [DefaultValue(false)] public bool IsInterruptable { get; set; }
	}
}