using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Enemies;
using Scripts.Models.GUIs;
using Scripts.Models.Weapons;

namespace Scripts.Models
{
    [XmlRoot("Engine")]
    public class EngineModel : MenuRootModel
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "StaticEnemy", Type = typeof(StaticEnemyModel))]
        [XmlArrayItem(ElementName = "Enemy", Type = typeof(EnemyModel))]
        [XmlArrayItem(ElementName = "Boss", Type = typeof(BossModel))]

        [XmlArrayItem(ElementName = "Object", Type = typeof(ObjectModel))]
        [XmlArrayItem(ElementName = "SpecialEffect", Type = typeof(SpecialEffectModel))]
        [XmlArrayItem(ElementName = "Projectile", Type = typeof(ProjectileModel))]
        [XmlArrayItem(ElementName = "Piercing", Type = typeof(PiercingProjectileModel))]
        [XmlArrayItem(ElementName = "AoE", Type = typeof(AoEModel))]
        [XmlArrayItem(ElementName = "ParticleAoE", Type = typeof(ParticleAoEModel))]
        [XmlArrayItem(ElementName = "DamageGUI", Type = typeof(DamageGUIModel))]
        public List<ObjectModel> Objects { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Level", Type = typeof(LevelModel))]
        [XmlArrayItem(ElementName = "SubLevel", Type = typeof(LevelModel))]
        public List<LevelModel> Levels { get; set; }

	    [XmlArray]
	    [XmlArrayItem(ElementName = "Item", Type = typeof(ItemModel))]
	    public List<ItemModel> Items { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "LootTable", Type = typeof(LootTableModel))]
        public List<LootTableModel> LootTables { get; set; }

	    [XmlArray]
	    [XmlArrayItem(ElementName = "Inventory", Type = typeof(InventoryModel))]
		public List<InventoryModel> Inventories { get; set; }

		[XmlArray]
		[XmlArrayItem(ElementName = "Scene", Type = typeof(SceneModel))]
		public List<SceneModel> Scenes { get; set; }

        public EngineModel()
        {
            Objects = new List<ObjectModel>();
            LootTables = new List<LootTableModel>();
	        Inventories = new List<InventoryModel>();
			Scenes = new List<SceneModel>();
        }
    }
}
