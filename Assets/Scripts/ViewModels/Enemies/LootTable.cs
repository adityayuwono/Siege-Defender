using System;
using System.Collections.Generic;
using Scripts.Contexts;
using Scripts.Interfaces;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Items;

namespace Scripts.ViewModels.Enemies
{
	public class LootTable : Base
	{
		private readonly List<Loot> _loots = new List<Loot>();
		private readonly LootTableModel _model;
		private readonly Random _randomizer;

		public LootTable(LootTableModel model, Base parent) : base(model, parent)
		{
			_model = model;
			_randomizer = new Random();

			foreach (var lootModel in _model.Loots)
			{
				_loots.Add(new Loot(lootModel));
			}
		}

		/// <summary>
		///     Will give random items based on loot table
		/// </summary>
		/// <returns>List of items</returns>
		public List<Item> GetLoot(Inventory inventory)
		{
			foreach (var loot in _loots) loot.Reset();

			var items = new List<Item>();
			for (var i = 0; i < _model.Drops; i++)
				foreach (var loot in _loots)
				{
					var chance = _randomizer.Next(100);
					var item = loot.GetItemModel(inventory, chance);
					if (item != null)
					{
						items.Add(item);
						break;
					}
				}

			return items;
		}

		private class Loot
		{
			private readonly LootModel _model;
			private int _max;

			public Loot(LootModel model)
			{
				_model = model;
				_max = _model.Max;
			}

			public Item GetItemModel(Base parent, float chance)
			{
				if (_max > 0 && chance <= _model.Chance)
				{
					var itemModel = DataContext.Instance.GetItemModel(_model.ItemId);
					itemModel.Type = itemModel.Id;
					_max--;
					var item = new Item(itemModel, parent);
					return item;
				}

				return null;
			}

			public void Reset()
			{
				_max = _model.Max;
			}
		}
	}
}