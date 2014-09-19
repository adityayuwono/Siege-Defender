using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class BaseAction : TargetProperty
    {
        private readonly BaseActionModel _model;
        public BaseAction(BaseActionModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public virtual void Invoke()
        {
            Activate();
        }
    }
}
