using Scripts.Contexts;
using Scripts.Interfaces;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class InventoryReference : Object
	{
		private readonly Inventory _inventory;

		public InventoryReference(InventoryReferenceModel model, IHaveRoot parent)
			: base(model, parent)
		{
			_inventory = DataContext.Instance.GetInventorySource(model.Source);
			_inventory.OverrideModelForShowing(parent, model.AssetId);
		}

		public override void Show()
		{
			base.Show();

			_inventory.Show();
		}

		public override void Hide(string reason)
		{
			_inventory.Hide(reason);

			base.Hide(reason);
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			_inventory.Activate();
		}

		protected override void OnDeactivate()
		{
			_inventory.Hide("Deactivating");
			_inventory.Deactivate("Deactivating");

			base.OnDeactivate();
		}

		protected override void OnDestroyed()
		{
			_inventory.Destroy();

			base.OnDestroyed();
		}
	}
}
