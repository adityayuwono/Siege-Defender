﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Weapons
{
	[Serializable]
	public class ProjectileModel : ObjectModel
	{
		public ProjectileModel()
		{
			DeathDelay = 1f;

			Ammunition = -1;
			Reload = 3f;
			Accuracy = 0.9f;
			CriticalChance = 0f;
			CriticalDamageMultiplier = 1f;
			Scatters = 0;
			Deviation = 0f;
			SpeedDeviation = "400-400";
			Damage = "1-1";
			RoF = 1f;

			IsRotationRandomized = false;
		}

		[XmlAttribute] public string BulletAssetId { get; set; }

		[XmlAttribute] [DefaultValue("1-1")] public string Damage { get; set; }

		[XmlAttribute] [DefaultValue(1f)] public float RoF { get; set; }

		[XmlAttribute] public string AoEId { get; set; }

		[XmlAttribute] [DefaultValue(-1)] public int Ammunition { get; set; }

		[XmlAttribute] [DefaultValue(3f)] public float Reload { get; set; }

		[XmlAttribute] [DefaultValue(0.9f)] public float Accuracy { get; set; }

		[XmlAttribute] [DefaultValue(0f)] public float CriticalChance { get; set; }

		[XmlAttribute] [DefaultValue(1f)] public float CriticalDamageMultiplier { get; set; }

		[XmlAttribute] [DefaultValue(0f)] public float Deviation { get; set; }

		[XmlAttribute]
		[DefaultValue("400-400")]
		public string SpeedDeviation { get; set; }

		[XmlAttribute] [DefaultValue(false)] public bool IsRotationRandomized { get; set; }

		[XmlAttribute] [DefaultValue(1)] public int Scatters { get; set; }
	}
}