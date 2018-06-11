using System;
using Scripts.Contexts;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Inventory : Element, IContext
	{
		public Action ChildrenChanged;

		public AdjustableProperty<string> SelectedItemBaseName;
		public AdjustableProperty<string> SelectedItemStatNames;
		public AdjustableProperty<string> SelectedItemStatNumbers;

		private readonly InventoryModel _model;
		private PropertyLookup _propertyLookup;
		private Item _selectedItem;

		public Inventory(InventoryModel model, Base parent) : base(model, parent)
		{
			SelectedItemBaseName = new AdjustableProperty<string>("SelectedItemBaseName", this);
			SelectedItemStatNames = new AdjustableProperty<string>("SelectedItemStatNames", this);
			SelectedItemStatNumbers = new AdjustableProperty<string>("SelectedItemStatNumbers", this);

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

		public void SelectItem(Item item)
		{
			if (_selectedItem != null)
			{
				_selectedItem.IsSelected.SetValue(false);
			}
			_selectedItem = item;
			_selectedItem.IsSelected.SetValue(true);

			SelectedItemBaseName.SetValue(item.BaseItem);
			SelectedItemStatNames.SetValue(item.Stats);
			SelectedItemStatNumbers.SetValue(item.Numbers);
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