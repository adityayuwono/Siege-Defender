using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	[Serializable]
	public class LabelGUIModel : ObjectModel
	{
		public LabelGUIModel()
		{
			Text = string.Empty;
		}

		[XmlAttribute]
		public string Text { get; set; }

		[XmlAttribute]
		public bool IsStatic { get; set; }
	}
}