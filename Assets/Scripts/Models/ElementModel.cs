using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	public class ElementModel : ObjectModel
	{
		[XmlAttribute] public string IsVisible { get; set; }
	}
}