using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class BaseActionViewModel : TargetProperty_ViewModel
    {
        private readonly Base_ActionModel _model;
        public BaseActionViewModel(Base_ActionModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public virtual void Invoke()
        {
            Activate();
        }
    }
}
