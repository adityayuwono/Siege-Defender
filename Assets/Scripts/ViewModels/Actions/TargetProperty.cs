using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class TargetProperty : Base
    {
        private TargetPropertyModel _model;

        protected TargetProperty(TargetPropertyModel model, Base parent) : base(model, parent)
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
