﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Items
{
	[Serializable]
	public class WeaponStatsModel
	{
		public WeaponStatsModel()
		{
			DamageSerialized = "1-1";
			RoF = 1f;
			Ammunition = -1;
			ReloadTime = 3f;
			Accuracy = 0.9f;
			CriticalChance = 0f;
			CriticalDamageMultiplier = 1f;
			Deviation = 0f;
			SpeedDeviationSerialized = "400-400";
			Scatters = 0;
		}

		[XmlIgnore]
		public float[] Damage { get; set; }

		[XmlAttribute("Damage")]
		[DefaultValue("1-1")]
		public string DamageSerialized
		{
			get { return string.Format("{0}-{1}", Damage[0], Damage[1]); }
			set
			{
				var damageStrings = value.Split('-');
				var lowerLimit = float.Parse(damageStrings[0]);
				var upperLimit = float.Parse(damageStrings[1]);
				Damage = new[] { lowerLimit, upperLimit };
			}
		}

		[XmlAttribute]
		[DefaultValue(1f)]
		public float RoF { get; set; }

		[XmlAttribute]
		public string AoEId { get; set; }

		[XmlAttribute]
		[DefaultValue(-1)]
		public int Ammunition { get; set; }

		[XmlAttribute]
		[DefaultValue(3f)]
		public float ReloadTime { get; set; }

		[XmlAttribute]
		[DefaultValue(0.9f)]
		public float Accuracy { get; set; }

		[XmlAttribute]
		[DefaultValue(0f)]
		public float CriticalChance { get; set; }

		[XmlAttribute]
		[DefaultValue(1f)]
		public float CriticalDamageMultiplier { get; set; }

		[XmlAttribute]
		[DefaultValue(0f)]
		public float Deviation { get; set; }

		[XmlIgnore]
		public float[] SpeedDeviation { get; set; }

		[XmlAttribute("SpeedDeviation")]
		[DefaultValue("400-400")]
		public string SpeedDeviationSerialized
		{
			get { return string.Format("{0}-{1}", SpeedDeviation[0], SpeedDeviation[1]); }
			set
			{
				var speedStrings = value.Split('-');
				var lowerLimit = float.Parse(speedStrings[0]);
				var upperLimit = float.Parse(speedStrings[1]);
				SpeedDeviation = new[] { lowerLimit, upperLimit };
			}
		}

		[XmlAttribute]
		[DefaultValue(1)]
		public int Scatters { get; set; }
	}
}
