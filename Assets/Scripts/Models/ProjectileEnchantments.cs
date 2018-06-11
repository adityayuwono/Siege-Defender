using System;
using Scripts.Models.Weapons;

namespace Scripts.Models
{
	[Serializable]
	public class ProjectileEnchantments : ProjectileModel
	{
		public ProjectileEnchantments()
		{
			DeathDelay = 0f;

			DamageSerialized = "0-0";
			RoF = 0f;
			Ammunition = 0;
			ReloadTime = 0f;
			Accuracy = 0f;
			CriticalChance = 0f;
			CriticalDamageMultiplier = 0f;
			Deviation = 0f;
			SpeedDeviationSerialized = "0-0";
			Scatters = 0;
		}
	}
}