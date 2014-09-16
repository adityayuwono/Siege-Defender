using Scripts.Models;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class Base_ActionViewModel : TargetProperty_ViewModel
    {
        private readonly Base_ActionModel _model;
        public Base_ActionViewModel(Base_ActionModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public virtual void Invoke()
        {
            Activate();
        }
    }
}
