﻿using Scripts.Contexts;
using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class EquipmentSlot : Element
	{
		public readonly AdjustableProperty<ObjectModel> Item;
		public readonly AdjustableProperty<string> ItemId;

		private readonly EquipmentSlotModel _model;
		private Item _currentItem;

		public EquipmentSlot(EquipmentSlotModel model, Inventory parent) : base(model, parent)
		{
			_model = model;

			ItemId = new AdjustableProperty<string>("ItemId", this);
			Item = new AdjustableProperty<ObjectModel>("Item", this);

			CurrentItem = IoC.IoCContainer.GetInstance<Item>(_model.Item.GetType(), new object[] {_model.Item, this});
		}

		private Item CurrentItem
		{
			set { OnItemUpdate(value); }
		}

		public void Object_OnDropped(Object objectVM)
		{
			var droppedItem = objectVM as Item;
			
			if (droppedItem.GetParent<EquipmentSlot>() == null)
			{
				CurrentItem = droppedItem;
			}
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			_currentItem.Activate();
		}

		public override void Show()
		{
			base.Show();

			_currentItem.Show();
		}

		public override void Hide(string reason)
		{
			_currentItem.Hide(reason);

			base.Hide(reason);
		}

		protected override void OnDestroyed()
		{
			_currentItem.Destroy();

			base.OnDestroyed();
		}

		private void OnItemUpdate(Item item)
		{
			var inventoryParent = GetParent<Inventory>();
			// Remove it from the inventory, do this first to make sure there's a spot left in the inventory
			inventoryParent.ReleaseItem(item);

			var oldItem = _currentItem;
			_currentItem = item; // Swap the current item
			_currentItem.ChangeParent(this);

			if (oldItem != null)
			{
				inventoryParent.AddItem(oldItem); // Send the current item back to inventory
			}

			_model.Item = _currentItem.Model; // Save the change to model
			ItemId.SetValue(_currentItem.BaseItem); // Update projectile used

			var currentProjectileItem = _currentItem as ProjectileItem;
			Item.SetValue(currentProjectileItem == null
				? null
				: currentProjectileItem.GetProjectileModel()); // Update actual object model

			DataContext.Save();
		}
	}
}