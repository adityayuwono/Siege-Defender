using Scripts.Helpers;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class CreateItemAction : BaseAction
    {
        private readonly CreateItemActionModel _model;

        public CreateItemAction(CreateItemActionModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Invoke()
        {
            base.Invoke();

            Root.LogEvent("Item", "Dropped", _model.Value, 1);

            var targetInventory = Target as Inventory;

            if (targetInventory == null)
                throw new EngineException(this, string.Format("Failed to find Inventory: {0}", _model.Target));

            var items = Root.GetLoot(_model.Value);
            foreach (var item in items)
                targetInventory.AddItem(item);
        }
    }
}
