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

        protected Property Target { get; private set; }

        protected override void OnLoad()
        {
            base.OnLoad();

            Target = FindTarget();
        }

        protected virtual Property FindTarget()
        {
            var parentContext = GetParent<IContext>();
            if (parentContext == null)
                throw new EngineException(this, "Failed to find parent Context");
            
            return parentContext.PropertyLookup.GetProperty(_model.Target.Replace("This", parentContext.Id));
        }
    }
}
