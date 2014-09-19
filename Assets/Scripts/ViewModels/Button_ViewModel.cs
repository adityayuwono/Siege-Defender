using System.Collections.Generic;
using Scripts.Models.GUIs;
using Scripts.ViewModels.Actions;
using Scripts.ViewModels.GUIs;

namespace Scripts.ViewModels
{
    public class Button_ViewModel : BaseGUI
    {
        private readonly Button_GUIModel _model;

        public Button_ViewModel(Button_GUIModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;

            foreach (var actionModel in _model.Trigger.Actions)
            {
                // Get new instance of ActionVM
                var actionVM = Root.IoCContainer.GetInstance<BaseActionViewModel>(actionModel.GetType(), new object[] { actionModel, this });
                _actions.Add(actionVM);
            }
        }

        private readonly List<BaseActionViewModel> _actions = new List<BaseActionViewModel>();

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
