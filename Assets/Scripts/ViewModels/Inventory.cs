using System;
using Scripts.Contexts;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Inventory : Element, IContext
	{
		private readonly InventoryModel _model;
		private PropertyLookup _propertyLookup;
		public Action ChildrenChanged;

		public Inventory(InventoryModel model, Base parent) : base(model, parent)
		{
			// Grab reference to Player's Inventory loaded from XML
			foreach (var inventoryModel in DataContext.PlayerDataModel.Inventories)
			{
				if (inventoryModel.Id == model.Source)
				{
					_model = inventoryModel;
				}
			}

			foreach (var itemModel in _model.Items)
			{
				Elements.Add(IoC.IoCContainer.GetInstance<Item>(itemModel.GetType(), new object[] {itemModel, this}));
			}

			foreach (var equipmentSlotModel in _model.EquipmentSlots)
			{
				Elements.Add(new EquipmentSlot(equipmentSlotModel, this));
			}
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
			item.ChangeParent(this);

			InvokeChildrenChanged();
		}

		public void ReleaseItem(Item itemViewModel)
		{
			// Item was added to inventory

			_model.Items.Remove(itemViewModel.Model);
			Elements.Remove(itemViewModel);

			InvokeChildrenChanged();
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