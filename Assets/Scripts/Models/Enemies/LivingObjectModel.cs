using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
	[Serializable]
	public class LivingObjectModel : ObjectModel
	{
		public LivingObjectModel()
		{
			Health = 1;
			ProjectileLimit = 3;
		}

		[XmlAttribute]
		[DefaultValue(1)]
		public int Health { get; set; }

		[XmlElement]
		public TriggeredModel Trigger { get; set; }

		[XmlAttribute]
		[DefaultValue("")]
		public string CollisionEffectNormal { get; set; }

		public int ProjectileLimit { get; protected set; }
	}
}