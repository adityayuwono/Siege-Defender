using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Items;

namespace Scripts.Models.Weapons
{
	[Serializable]
	public class ProjectileModel : ObjectModel
	{
		public ProjectileModel()
		{
			DeathDelay = 1f;

			Stats = new WeaponStatsModel
			{
				DamageSerialized = "1-1",
				RoF = 1f,
				Ammunition = -1,
				ReloadTime = 3f,
				Accuracy = 0.9f,
				CriticalChance = 0f,
				CriticalDamageMultiplier = 1f,
				Deviation = 0f,
				SpeedDeviationSerialized = "400-400",
				Scatters = 0,
			};
		}

		[XmlAttribute]
		public string BulletAssetId { get; set; }

		[XmlElement]
		public WeaponStatsModel Stats { get; set; }

	}
}