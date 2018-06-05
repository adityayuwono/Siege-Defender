using System;
using System.Xml.Serialization;
using Scripts.Models.Enemies;
using Scripts.Models.Weapons;

namespace Scripts.Models
{
	[Serializable]
	public class EnemyModel : LivingObjectModel
	{
		public EnemyModel()
		{
			DeathDelay = 2f;
		}

		[XmlAttribute] public string Target { get; set; }

		[XmlAttribute] public float Speed { get; set; }

		[XmlElement] public ProjectileModel Projectile { get; set; }

		[XmlAttribute] public float Rotation { get; set; }


		[XmlAttribute] public string Damage { get; set; }

		[XmlAttribute] public float AttackSpeed { get; set; }
	}
}