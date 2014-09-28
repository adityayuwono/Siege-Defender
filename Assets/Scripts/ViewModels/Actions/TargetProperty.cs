using System;
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

        protected Object Target { get; private set; }
        protected Property Property { get; private set; }

        protected override void OnLoad()
        {
            base.OnLoad();

            if (!string.IsNullOrEmpty(_model.Property))
            {
                var target = _model.Target;
                if (_model.Target == "{This}")
                    target = GetParent<Object>().Id;
                else if (_model.Target == "{Monster}")
                    target = GetParent<EnemyBase>().Id;

                Property = Root.GetProperty(target, _model.Property);
            }
        }
    }
}
