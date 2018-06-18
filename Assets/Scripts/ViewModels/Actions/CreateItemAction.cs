using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	public class CreateItemAction : BaseInventoryAction
	{
		private readonly CreateItemActionModel _model;

		public CreateItemAction(CreateItemActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();

			var items = SDRoot.GetLoot(_model.Value, TargetInventory);
			foreach (var item in items)
			{
				TargetInventory.AddItem(item);
			}
		}
	}
}