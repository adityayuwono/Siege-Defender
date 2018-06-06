using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	[Serializable]
	public class ValueDisplayGUIModel : BaseGUIModel
	{
		[XmlAttribute]
		public string Value { get; set; }

		[XmlAttribute]
		public string MaxValue { get; set; }
	}
}