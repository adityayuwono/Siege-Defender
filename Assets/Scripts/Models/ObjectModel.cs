using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.GUIs;
using Scripts.Models.Items;

namespace Scripts.Models
{
	[Serializable]
	public class ObjectModel : TriggerableModel
	{
		/// <summary>
		///     Grouping Identifier
		///     Not XML Attribute, this is auto-set from Id when we spawn it
		/// </summary>
		[XmlIgnore]
		public string Type;

		public ObjectModel()
		{
			DeathDelay = 0;
			Position = "0,0,0";
		}

		[XmlArray(ElementName = "Elements")]
		// 3D Objects
		[XmlArrayItem(ElementName = "Element", Type = typeof(ElementModel))]
		[XmlArrayItem(ElementName = "Player", Type = typeof(PlayerModel))]
		[XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitboxModel))]
		[XmlArrayItem(ElementName = "GUIRoot", Type = typeof(GUIs.RootModel))]
		[XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplayModel))]
		[XmlArrayItem(ElementName = "ObjectDisplay", Type = typeof(ObjectDisplayModel))]
		// GUIs
		[XmlArrayItem(ElementName = "Inventory", Type = typeof(InventoryModel))]
		[XmlArrayItem(ElementName = "Button", Type = typeof(ButtonModel))]
		[XmlArrayItem(ElementName = "ProgressBar", Type = typeof(ProgressBarModel))]
		[XmlArrayItem(ElementName = "CooldownGUI", Type = typeof(CooldownModel))]
		[XmlArrayItem(ElementName = "ShooterGUI", Type = typeof(GUIs.ShooterModel))]
		[XmlArrayItem(ElementName = "Label", Type = typeof(LabelModel))]
		[XmlArrayItem(ElementName = "StatsLabel", Type = typeof(StatsLabelModel))]
		[XmlArrayItem(ElementName = "GameEndStats", Type = typeof(GameEndStatsModel))]
		[XmlArrayItem(ElementName = "BloodOverlay", Type = typeof(BloodOverlayModel))]
		// Intervals
		[XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
		[XmlArrayItem(ElementName = "ObjectSpawn", Type = typeof(ObjectSpawnModel))]
		[XmlArrayItem(ElementName = "SpecialEffectManager", Type = typeof(SpecialEffectManagerModel))]
		public List<ObjectModel> ElementsSerialized
		{
			get { return Elements != null ? (Elements.Count > 0 ? Elements : null) : null; }
			set { Elements = value; }
		}

		[XmlIgnore]
		public List<ObjectModel> Elements { get; private set; }

		[XmlAttribute]
		public string AssetId { get; set; }

		[XmlAttribute]
		[DefaultValue("0,0,0")]
		public string Position { get; set; }

		[XmlAttribute]
		[DefaultValue(0)]
		public float DeathDelay { get; set; }
	}
}