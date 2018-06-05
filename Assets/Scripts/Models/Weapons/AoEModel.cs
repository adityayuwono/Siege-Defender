using System;
using System.Xml.Serialization;

namespace Scripts.Models.Weapons
{
	[Serializable]
	public class AoEModel : ProjectileModel
	{
		public AoEModel()
		{
			DeathDelay = 1f;
		}

		[XmlAttribute] public float Radius { get; set; }

		[XmlAttribute] public bool IsGrounded { get; set; }
	}
}