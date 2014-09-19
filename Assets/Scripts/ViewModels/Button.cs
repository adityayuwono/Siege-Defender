using System.Collections.Generic;
using Scripts.Models.GUIs;
using Scripts.ViewModels.Actions;
using Scripts.ViewModels.GUIs;

namespace Scripts.ViewModels
{
    public class Button : BaseGUI
    {
        private readonly ButtonGUIModel _model;

        public Button(ButtonGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;

            foreach (var actionModel in _model.Trigger.Actions)
            {
                // Get new instance of ActionVM
                var actionVM = Root.IoCContainer.GetInstance<BaseAction>(actionModel.GetType(), new object[] { actionModel, this });
                _actions.Add(actionVM);
            }
        }

        private readonly List<BaseAction> _actions = new List<BaseAction>();

        public void OnClicked()
        {
            // Invoke all actions related to this button
            foreach (var action in _actions)
            {
                action.Invoke();
            }
        }
    }
}
