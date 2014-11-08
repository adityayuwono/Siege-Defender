using System;
using System.Collections.Generic;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class LootTable : Base
    {
        private readonly LootTableModel _model;

        public LootTable(LootTableModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        /// <summary>
        /// Will give random items based on loot table, may return null if unlucky
        /// </summary>
        /// <returns>List of items, or null</returns>
        public List<Item> GetLoot()
        {
            var randomizer = new Random();
            var items = new List<Item>();
            for (var i = 0; i < _model.Drops; i++)
            {
                foreach (var lootModel in _model.Loots)
                {
                    var chance = randomizer.Next(100);
                    if (chance < lootModel.Chance)
                    {
                        var itemModel = Root.GetItemModel(lootModel.ItemId);
                        itemModel.Type = itemModel.Id;
                        items.Add(new Item(itemModel, Root));
                        break;
                    }
                }
            }
            return items.Count > 0 ? items : null;
        }
    }
}
