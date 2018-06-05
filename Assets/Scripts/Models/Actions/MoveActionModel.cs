using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class MoveActionModel : BaseActionModel
	{
		public MoveActionModel()
		{
			SpeedMultiplier = 1;
		}

		[XmlAttribute] public string MoveTarget { get; set; }

		[XmlAttribute] [DefaultValue(1)] public float SpeedMultiplier { get; set; }
	}
}