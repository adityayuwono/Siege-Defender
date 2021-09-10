using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.Enemies;
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

		// 3D Objects
		[XmlArrayItem(ElementName = "Element", Type = typeof(ElementModel))]
		[XmlArrayItem(ElementName = "Player", Type = typeof(PlayerModel))]
		[XmlArrayItem(ElementName = "PlayerHitbox", Type = typeof(PlayerHitboxModel))]
		[XmlArrayItem(ElementName = "GUIRoot", Type = typeof(GUIs.GUIRootModel))]
		[XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplayModel))]
		// GUIs
		[XmlArrayItem(ElementName = "Inventory", Type = typeof(InventoryModel))]
		[XmlArrayItem(ElementName = "InventoryReference", Type = typeof(InventoryReferenceModel))]
		[XmlArrayItem(ElementName = "Button", Type = typeof(ButtonModel))]
		[XmlArrayItem(ElementName = "ProgressBar", Type = typeof(ProgressBarModel))]
		[XmlArrayItem(ElementName = "Cooldown", Type = typeof(CooldownModel))]
		[XmlArrayItem(ElementName = "ShooterGUI", Type = typeof(GUIs.GUIShooterModel))]
		[XmlArrayItem(ElementName = "Label", Type = typeof(LabelModel))]
		[XmlArrayItem(ElementName = "GameEndStats", Type = typeof(GameEndStatsModel))]
		[XmlArrayItem(ElementName = "BloodOverlay", Type = typeof(BloodOverlayModel))]
		// Intervals
		[XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
		[XmlArrayItem(ElementName = "ObjectSpawn", Type = typeof(ObjectSpawnModel))]
		[XmlArrayItem(ElementName = "SpecialEffectManager", Type = typeof(SpecialEffectManagerModel))]
		public List<ObjectModel> Elements { get; set; }

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