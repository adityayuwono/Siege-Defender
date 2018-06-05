using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Weapons
{
	[Serializable]
	public class PiercingProjectileModel : ProjectileModel
	{
		public PiercingProjectileModel()
		{
			DamageReduction = 0.75f;
		}

		[XmlAttribute] [DefaultValue(0.75f)] public float DamageReduction { get; set; }
	}
}