using Scripts.Helpers;
using Scripts.Models.Actions;
using Scripts.ViewModels.Items;

namespace Scripts.ViewModels.Actions
{
	public class BaseInventoryAction : BaseAction
	{
		private readonly BaseActionModel _model;

		public BaseInventoryAction(BaseActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		protected Inventory TargetInventory { get; private set; }

		public override void Invoke()
		{
			base.Invoke();

			TargetInventory = Target as Inventory;
			if (TargetInventory == null)
			{
				throw new EngineException(this, string.Format("Failed to find Inventory: {0}", _model.Target));
			}
		}
	}
}
