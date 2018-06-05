using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
	[Serializable]
	public class LoadSceneActionModel : BaseActionModel
	{
		public LoadSceneActionModel()
		{
			Target = "";
		}

		[XmlAttribute] public string LevelId { get; set; }
	}
}