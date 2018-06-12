using System;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class EnchantmentItemModel : ItemModel
	{
		public EnchantmentItemModel()
		{
			ItemSlotRoots = "EnchantmentSlot";

			Stats = new WeaponStatsModel
			{
				DamageSerialized = "0-0",
				RoF = 0f,
				Ammunition = 0,
				ReloadTime = 0f,
				Accuracy = 0f,
				CriticalChance = 0f,
				CriticalDamageMultiplier = 0f,
				Deviation = 0f,
				SpeedDeviationSerialized = "0-0",
				Scatters = 0,
			};
		}

		[XmlElement]
		public WeaponStatsModel Stats { get; set; }
	}
}
