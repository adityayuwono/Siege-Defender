using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class ButtonView : ElementView
    {
        private readonly ButtonGUI _viewModel;

        public ButtonView(ButtonGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GameObject.AddComponent<ButtonController>().OnClicked = _viewModel.OnClicked;
        }
    }
}
