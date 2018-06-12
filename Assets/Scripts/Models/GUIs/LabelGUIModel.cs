using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	[Serializable]
	public class LabelGUIModel : StaticGUIModel
	{
		public LabelGUIModel()
		{
			Text = string.Empty;
		}

		[XmlAttribute]
		public string Text { get; set; }
	}
}