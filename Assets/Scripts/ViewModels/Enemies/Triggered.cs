using System.Collections.Generic;
using Scripts.Models.Actions;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Enemies
{
    public class Triggered : Base
    {
        private readonly TriggeredModel _model;
        public Triggered(TriggeredModel model, Base parent) : base(model, parent)
        {
            _model = model;

            foreach (var conditionModel in _model.Conditions)
            {
                // Get new instance of ActionVM
                var conditionViewModel = Root.IoCContainer.GetInstance<BaseCondition>(conditionModel.GetType(), new object[] { conditionModel, this });
                _conditions.Add(conditionViewModel);
            }

            foreach (var actionModel in _model.Actions)
            {
                // Get new instance of ActionVM
                var actionVM = Root.IoCContainer.GetInstance<BaseAction>(actionModel.GetType(), new object[] { actionModel, this });
                _actions.Add(actionVM);
            }
        }

        private readonly List<BaseCondition> _conditions = new List<BaseCondition>();

        private readonly List<BaseAction> _actions = new List<BaseAction>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var condition in _conditions)
            {
                condition.Activate();
                condition.IsMatch.OnChange += Conditions_OnChange;
            }
            Conditions_OnChange();
        }

        protected override void OnDeactivate()
        {
            foreach (var condition in _conditions)
            {
                condition.IsMatch.OnChange -= Conditions_OnChange;
                condition.Deactivate("Triggered is deactivated");
            }

            base.OnDeactivate();
        }

        private void Conditions_OnChange()
        {
            var isMatch = true;
            foreach (var condition in _conditions)
                isMatch &= condition.IsMatch.GetValue();

            if (isMatch)
                foreach (var action in _actions)
                    action.Invoke();
        }
    }
}
