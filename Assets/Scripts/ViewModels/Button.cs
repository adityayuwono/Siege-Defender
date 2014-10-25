using System;
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

            _actions = new ActionCollection(_model.Trigger.Actions, this);
        }

        private readonly ActionCollection _actions;

        public event Action OnClick;

        public void OnClicked()
        {
            // Invoke all actions related to this button
            _actions.Activate();
        }
    }
}
