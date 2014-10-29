using System;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class BaseCondition : TargetProperty
    {
        private readonly BaseConditionModel _model;
        protected BaseCondition(BaseConditionModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }
        
        protected override void OnActivate()
        {
            base.OnActivate();

            Property.OnChange += Property_OnChange;
            Property_OnChange();
        }

        protected override void OnDeactivate()
        {
            Property.OnChange -= Property_OnChange;

            base.OnDeactivate();
        }

        protected virtual void Property_OnChange()
        {
            throw new NotImplementedException();
        }

        public readonly Property<bool> IsMatch = new Property<bool>();
    }
}
