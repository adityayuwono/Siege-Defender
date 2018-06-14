using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	[Serializable]
	public class LabelModel : StaticModel
	{
		public LabelModel()
		{
			Text = string.Empty;
		}

		[XmlAttribute]
		public string Text { get; set; }
	}
}