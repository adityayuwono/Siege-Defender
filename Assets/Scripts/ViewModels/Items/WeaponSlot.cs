using Scripts.Contexts;
using Scripts.Core;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class WeaponSlot : EquipmentSlot
	{
		public readonly AdjustableProperty<ProjectileItem> ProjectileItem;

		public WeaponSlot(WeaponSlotModel model, Inventory parent) : base(model, parent)
		{
			ProjectileItem = new AdjustableProperty<ProjectileItem>("ProjectileItem", this, true);
			ProjectileItem.SetValue(CurrentItem as ProjectileItem);
		}

		private Item CurrentEnchantment
		{
			set
			{
				HandleItemUpdate(this, ref _currentEnchantment, value);
				DataContext.Instance.Save();
			}
		}
		private Item _currentEnchantment;

		public override void Show()
		{
			base.Show();
			
			if (_currentEnchantment != null)
			{
				_currentEnchantment.Show();
			}
		}

		public override void Hide(string reason)
		{
			if (_currentEnchantment != null)
			{
				_currentEnchantment.Hide(reason);
			}

			base.Hide(reason);
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			if (_currentEnchantment != null)
			{
				_currentEnchantment.Activate();
			}
		}

		protected override void OnDestroyed()
		{
			if (_currentEnchantment != null)
			{
				_currentEnchantment.Destroy();
			}

			base.OnDestroyed();
		}

		public override void HandleObjectDropped(Object droppedObject)
		{
			var droppedEnchantment = droppedObject as EnchantmentItem;

			if (droppedEnchantment == null)
			{
				base.HandleObjectDropped(droppedObject);
			}
			else
			{
				HandleEnchantmentDropped(droppedEnchantment);
			}
		}

		protected override void LoadItem(EquipmentSlotModel equipmentSlotModel)
		{
			var model = equipmentSlotModel.Item as ProjectileItemModel;
			if (model.Enchantment != null)
			{
				_currentEnchantment = IoC.IoCContainer.GetInstance<Item>(model.Enchantment.GetType(), new object[] { model.Enchantment, this });
			}
			base.LoadItem(equipmentSlotModel);
		}

		protected override void UpdateOldItem(Inventory inventory, Item oldItem)
		{
			base.UpdateOldItem(inventory, oldItem);

			var oldProjectileItem = oldItem as ProjectileItem;
			if (oldProjectileItem != null)
			{
				oldProjectileItem.DetachEnchantment();
			}
		}

		protected override void HandleItemUpdate(ProjectileItem currentItem)
		{
			var currentProjectileItem = currentItem;
			Item.SetValue(currentProjectileItem.UpdateModel(_currentEnchantment != null
				? _currentEnchantment.Model as EnchantmentItemModel
				: null));

			if (ProjectileItem != null)
			{
				ProjectileItem.SetValue(currentItem);
			}
		}

		private void HandleEnchantmentDropped(EnchantmentItem droppedEnchantment)
		{
			if (_currentEnchantment == droppedEnchantment)
			{
				CurrentEnchantment = null;
			}
			else
			{
				CurrentEnchantment = droppedEnchantment;
			}
		}
	}
}
