using System.Linq;
using Scripts.Helpers;
using Scripts.Models.Items;
using Scripts.ViewModels.Items;

namespace Scripts.ViewModels.Actions
{
	public class TransferInventoryItems : BaseInventoryAction
	{
		private readonly TransferInventoryItemsModel _model;

		public TransferInventoryItems(TransferInventoryItemsModel model, Base parent)
			: base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();

			var sourceInventory = FindTarget(_model.Value) as Inventory;

			if (sourceInventory == null)
			{
				throw new EngineException(this, string.Format("Failed to find Inventory: {0}", _model.Value));
			}

			var items = sourceInventory.Elements.Where(e => e.GetType() == typeof(Item)).ToArray();
			foreach (var item in items.Cast<Item>())
			{
				TargetInventory.AddItem(item);
				sourceInventory.ReleaseItem(item);
			}

			if (_model.CombineItems)
			{
				TargetInventory.CombineItemsToStack();
			}
		}
	}
}
