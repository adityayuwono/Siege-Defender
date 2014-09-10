using System.Collections.Generic;
using Scripts.Models;
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
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            foreach (var actionModel in _model.Actions)
            {
                // Get new instance of ActionVM
                var actionVM = Root.IoCContainer.GetInstance<Base_ActionViewModel>(actionModel.GetType(), new object[] {actionModel, this});
                _actions.Add(actionVM);
            }
        }

        private readonly List<Base_ActionViewModel> _actions = new List<Base_ActionViewModel>(); 
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
