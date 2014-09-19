using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class TargetProperty_ViewModel : BaseViewModel
    {
        private TargetProperty_Model _model;

        protected TargetProperty_ViewModel(TargetProperty_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        protected Property Property { get; private set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            var target = _model.Target;
            if (_model.Target == "{This}")
                target = GetParent<Object>().Id;

            Property = Root.GetProperty(target, _model.Property);
        }
    }
}
