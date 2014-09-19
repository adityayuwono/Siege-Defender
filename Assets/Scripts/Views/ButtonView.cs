using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class ButtonView : ElementView
    {
        private readonly Button _viewModel;

        public ButtonView(Button viewModel, ObjectView parent) : base(viewModel, parent)
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
