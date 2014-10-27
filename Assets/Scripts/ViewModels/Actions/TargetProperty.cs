using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class TargetProperty : Base
    {
        private readonly TargetPropertyModel _model;

        protected TargetProperty(TargetPropertyModel model, Base parent) : base(model, parent)
        {
            _model = model;

            if (string.IsNullOrEmpty(_model.Target))
                throw new EngineException(this, string.Format("{0} does not have a Target defined", FullId));
        }

        protected Property Property { get; private set; }

        protected override void OnLoad()
        {
            base.OnLoad();

            Property = FindProperty();
        }

        protected virtual Property FindProperty()
        {
            if (string.IsNullOrEmpty(_model.Property)) return null;

            var target = _model.Target;
            if (_model.Target == "{This}")
                target = GetParent<Object>().Id;
            else if (_model.Target == "{Monster}")
                target = GetParent<EnemyBase>().Id;

            var parentContext = GetParent<IContext>();
            if (parentContext == null)
                throw new EngineException(this, "Failed to find parent Context");
            
            return parentContext.PropertyLookup.GetProperty(target, _model.Property);
        }
    }
}
