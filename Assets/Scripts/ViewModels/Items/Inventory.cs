using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class Inventory : Element, IContext
	{
		public Action ChildrenChanged;

		private readonly InventoryModel _model;
		private PropertyLookup _propertyLookup;

		public Inventory(InventoryModel model, IHaveRoot parent)
			: base(model, parent)
		{
			_model = model;

			foreach (var itemModel in _model.Items)
			{
				Elements.Add(IoC.IoCContainer.GetInstance<Item>(itemModel.GetType(), new object[] {itemModel, this}));
			}

			foreach (var equipmentSlotModel in _model.EquipmentSlots)
			{
				var equipmentSlot =
					IoC.IoCContainer.GetInstance<EquipmentSlot>(equipmentSlotModel.GetType(), new object[] {equipmentSlotModel, this});
				Elements.Add(equipmentSlot);

				if (_model.AlwaysTransferFromSlots)
				{
					equipmentSlot.OnItemUpdate += GetItemFromSlot;
				} 
			}

			// Need this to trigger registration of context in parent
			var propertyLookup = PropertyLookup;
		}

		public PropertyLookup PropertyLookup
		{
			get
			{
				if (_propertyLookup == null)
				{
					_propertyLookup = new PropertyLookup(Root, this);
				}

				return _propertyLookup;
			}
		}

		public void OverrideModelForShowing(IHaveRoot parent, string modelAssetId)
		{
			Parent = parent;
			_model.AssetId = modelAssetId;
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

		private static void GetItemFromSlot(Item item)
		{
			var equipmentSlot = item.GetParent<EquipmentSlot>();
			if (equipmentSlot != null)
			{
				equipmentSlot.ReleaseItem();
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