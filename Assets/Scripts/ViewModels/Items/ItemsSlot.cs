using Scripts.Interfaces;
using Scripts.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.ViewModels.Items
{
    public class ItemsSlot : DropableSlots
    {
		public Action ChildrenChanged;

		private ItemsSlotModel _model;

        public ItemsSlot(ItemsSlotModel model, IHaveRoot parent) : base(model, parent)
        {
			_model = model;

			foreach (var itemModel in _model.Items)
            {
                Elements.Add(IoC.IoCContainer.GetInstance<Item>(itemModel.GetType(), new object[] { itemModel, this }));
            }
        }

		public override void Show()
		{
			base.Show();

			InvokeChildrenChanged();
		}

		public void AddItem(Item item)
		{
			// Item was removed from inventory

			Elements.Add(item);
			_model.Items.Add(item.Model);

			if (IsShown)
			{
				item.ChangeParent(this);
			}

			InvokeChildrenChanged();
		}

		public void ReleaseItem(Item itemViewModel)
		{
			// Item was added to inventory

			_model.Items.Remove(itemViewModel.Model);
			Elements.Remove(itemViewModel);

			InvokeChildrenChanged();
		}

		public void CombineItemsToStack()
		{
			var objects = Elements.Where(e => e.GetType() == typeof(Item)).ToArray();
			var combinedItems = new List<Item>();
			foreach (var o in objects)
			{
				var item = o as Item;
				var existingCombinedItem = combinedItems.FirstOrDefault(i => i.BaseItem == item.BaseItem);
				if (existingCombinedItem != null)
				{
					existingCombinedItem.Quantity += item.Quantity;
					ReleaseItem(item);
				}
				else
				{
					combinedItems.Add(item);
				}
			}
		}

		private void InvokeChildrenChanged()
		{
			if (ChildrenChanged != null)
			{
				ChildrenChanged();
			}
		}
	}
}
