using System;
using System.Collections.Generic;
using Scripts.Models;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels
{
    public class Button_ViewModel : ElementViewModel
    {
        private readonly Button_Model _model;

        public Button_ViewModel(Button_Model model, SceneViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            foreach (var actionModel in _model.Actions)
            {
                var actionVM = Root.IoCContainer.GetInstance<Action_ViewModel>(actionModel.GetType(), new object[] {actionModel, this});
                _actions.Add(actionVM);
            }
        }

        private readonly List<Action_ViewModel> _actions = new List<Action_ViewModel>(); 
        public void OnClicked()
        {
            foreach (var action in _actions)
            {
                action.Invoke();
            }
        }
    }
}
