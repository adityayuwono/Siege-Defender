using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
	[Serializable]
	public class TargetPropertyModel : BaseModel
	{
		public TargetPropertyModel()
		{
			Target = "{This}";
		}

		[XmlAttribute]
		[DefaultValue("{This}")]
		public string Target { get; set; }

		[XmlAttribute] public string Value { get; set; }
	}
}