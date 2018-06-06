using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Scripts.Models.GUIs;

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
		[XmlArrayItem(ElementName = "GUIRoot", Type = typeof(RootGUIModel))]
		[XmlArrayItem(ElementName = "DamageDisplay", Type = typeof(DamageDisplayGUIModel))]
		[XmlArrayItem(ElementName = "ObjectDisplay", Type = typeof(ObjectDisplayModel))]
		// GUIs
		[XmlArrayItem(ElementName = "Inventory", Type = typeof(InventoryModel))]
		[XmlArrayItem(ElementName = "Button", Type = typeof(ButtonGUIModel))]
		[XmlArrayItem(ElementName = "ProgressBar", Type = typeof(ProgressBarGUIModel))]
		[XmlArrayItem(ElementName = "CooldownGUI", Type = typeof(CooldownGUIModel))]
		[XmlArrayItem(ElementName = "ShooterGUI", Type = typeof(ShooterGUIModel))]
		// Intervals
		[XmlArrayItem(ElementName = "EnemySpawn", Type = typeof(EnemyManagerModel))]
		[XmlArrayItem(ElementName = "ObjectSpawn", Type = typeof(ObjectSpawnModel))]
		[XmlArrayItem(ElementName = "SpecialEffectManager", Type = typeof(SpecialEffectManagerModel))]
		public List<ElementModel> ElementsSerialized
		{
			get { return Elements != null ? (Elements.Count > 0 ? Elements : null) : null; }
			set { Elements = value; }
		}

		[XmlIgnore]
		public List<ElementModel> Elements { get; private set; }

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