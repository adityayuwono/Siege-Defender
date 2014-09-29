using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class TargetProperty : Base
    {
        private readonly TargetPropertyModel _model;

        protected TargetProperty(TargetPropertyModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        protected Property Property { get; private set; }

        protected override void OnLoad()
        {
            base.OnLoad();

            Property = FindProperty();
        }

        protected virtual Property FindProperty()
        {
            var target = _model.Target;
            if (_model.Target == "{This}")
                target = GetParent<Object>().Id;
            else if (_model.Target == "{Monster}")
                target = GetParent<EnemyBase>().Id;

            return Root.GetProperty(target, _model.Property);
        }
    }
}
