using System.Collections.Generic;
using Scripts.Models.Actions;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Enemies
{
    public class Triggered_ViewModel : BaseViewModel
    {
        private readonly Triggered_Model _model;
        public Triggered_ViewModel(Triggered_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

            foreach (var conditionModel in _model.Conditions)
            {
                // Get new instance of ActionVM
                var conditionViewModel = Root.IoCContainer.GetInstance<Base_ConditionViewModel>(conditionModel.GetType(), new object[] { conditionModel, this });
                _conditions.Add(conditionViewModel);
            }

            foreach (var actionModel in _model.Actions)
            {
                // Get new instance of ActionVM
                var actionVM = Root.IoCContainer.GetInstance<Base_ActionViewModel>(actionModel.GetType(), new object[] { actionModel, this });
                _actions.Add(actionVM);
            }
        }

        private readonly List<Base_ConditionViewModel> _conditions = new List<Base_ConditionViewModel>();

        private readonly List<Base_ActionViewModel> _actions = new List<Base_ActionViewModel>();

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
