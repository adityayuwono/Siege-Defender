using System;
using Scripts.Models.GUIs;
using Scripts.ViewModels.GUIs;

namespace Scripts.ViewModels
{
    public class ButtonGUI : BaseGUI
    {
        private readonly ButtonGUIModel _model;

        public ButtonGUI(ButtonGUIModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public event Action OnClick;

        public void OnClicked()
        {
            // Invoke all actions related to this button
            if (OnClick != null) OnClick();
        }
    }
}
