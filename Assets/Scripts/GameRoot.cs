using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using Scripts.ViewModels.GUIs;

namespace Scripts
{
    public class GameRoot : MenuRoot
    {
        private readonly EngineModel _model;

		public GameRoot(EngineModel model, BaseContext parent)
			: base(model, parent)
        {
            _model = model;
        }

        public override IIntervalRunner IntervalRunner
        {
			get { return Context.IntervalRunner; }
        }

		public override RootBase Root
		{
			get { return this; }
		}

	    public override GameRoot SDRoot
	    {
		    get { return this; }
	    }

		public DamageDisplayManager DamageDisplay { get; set; }
		public SpecialEffectManager SpecialEffectManager { get; set; }

	    public override void StartCoroutine(IEnumerator coroutine)
	    {
		    Context.StartCoroutine(coroutine);
	    }

		protected override void OnLoad()
		{
			base.OnLoad();

			// Register all Loot Tables
			if (_model.LootTables != null)
			{
				foreach (var lootTableModel in _model.LootTables)
				{
					var id = lootTableModel.Id;

					if (string.IsNullOrEmpty(id))
						throw new EngineException(this, "Failed to register <LootTable>, <LootTable> need id");

					if (LootTables.ContainsKey(id))
						throw new EngineException(this, string.Format("Duplicate <LootTable> id: {0}", id));

					LootTables.Add(id, new LootTable(lootTableModel, this));
				}
			}
		}

		public LevelModel GetLevel(string levelId)
		{
			foreach (var levelModel in _model.Levels.Where(levelModel => levelModel.Id == levelId))
			{
				return levelModel;
			}
			throw new EngineException(this, string.Format("Level not found: {0}", levelId));
		}

		#region Loot Table
		protected readonly Dictionary<string, LootTable> LootTables = new Dictionary<string, LootTable>();
		public List<Item> GetLoot(string lootTableId)
		{
			if (LootTables.ContainsKey(lootTableId))
				return LootTables[lootTableId].GetLoot();

			throw new EngineException(this, string.Format("There's no loot table with id: {0}", lootTableId));
		}
		#endregion

        public void ThrowError(string message)
        {
	        Context.ThrowError(message);
        }
    }
}
