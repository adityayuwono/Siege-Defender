using Scripts.Contexts;
using Scripts.Core;
using Scripts.Models;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class EquipmentSlot : Element
	{
		public readonly AdjustableProperty<ObjectModel> Item;
		public readonly AdjustableProperty<string> ItemId;

		private readonly EquipmentSlotModel _model;

		public EquipmentSlot(EquipmentSlotModel model, Inventory parent) : base(model, parent)
		{
			_model = model;

			ItemId = new AdjustableProperty<string>("ItemId", this);
			Item = new AdjustableProperty<ObjectModel>("Item", this);

			LoadItem(model);
		}

		protected Item CurrentItem
		{
			get { return _currentItem;}
			set
			{
				HandleItemUpdate(this, ref _currentItem, value);
				SaveItemChange(_currentItem);
			}
		}
		private Item _currentItem;

		public virtual void HandleObjectDropped(Object droppedObject)
		{
			var isItemValid = CheckIfItemValid(droppedObject);
			if (isItemValid)
			{
				var droppedItem = (Item)droppedObject;
				if (droppedItem.GetParent<EquipmentSlot>() == null)
				{
					CurrentItem = droppedItem;
				}
			}
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

		protected static void HandleItemUpdate(EquipmentSlot parent, ref Item currentItem, Item newItem)
		{
			var inventoryParent = parent.GetParent<Inventory>();
			// Remove it from the inventory, do this first to make sure there's a spot left in the inventory
			if (newItem != null)
			{
				inventoryParent.ReleaseItem(newItem);
			}

			var oldItem = currentItem;
			currentItem = newItem; // Swap the current item
			if (currentItem != null)
			{
				currentItem.ChangeParent(parent);
			}

			parent.UpdateOldItem(inventoryParent, oldItem);

			parent.UpdateItem();
		}

		protected virtual void LoadItem(EquipmentSlotModel model)
		{
			CurrentItem = IoC.IoCContainer.GetInstance<Item>(model.Item.GetType(), new object[] { model.Item, this });
		}

		protected virtual void UpdateOldItem(Inventory inventoryParent, Item oldItem)
		{
			if (oldItem != null)
			{
				inventoryParent.AddItem(oldItem); // Send the current item back to inventory
			}
		}

		protected virtual void HandleItemUpdate(ProjectileItem currentItem)
		{
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			_currentItem.Activate();
		}

		protected override void OnDestroyed()
		{
			_currentItem.Destroy();

			base.OnDestroyed();
		}

		private bool CheckIfItemValid(Object droppedObject)
		{
			var droppedItem = droppedObject as Item;
			return droppedItem != null;
		}

		private void UpdateItem()
		{
			HandleItemUpdate(_currentItem as ProjectileItem);
		}

		private void SaveItemChange(Item currentItem)
		{
			_model.Item = currentItem.Model; // Save the change to model
			ItemId.SetValue(currentItem.BaseItem); // Update projectile used

			DataContext.Save();
		}
	}
}