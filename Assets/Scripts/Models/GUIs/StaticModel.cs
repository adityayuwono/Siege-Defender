using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
	[Serializable]
	public class StaticModel : ObjectModel
	{
		[XmlAttribute]
		public bool IsStatic { get; set; }
	}
}
