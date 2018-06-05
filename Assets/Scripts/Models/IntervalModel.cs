using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
	[Serializable]
	public class IntervalModel : RandomPositionManagerModel
	{
		[XmlAttribute] public float Interval { get; set; }
	}
}