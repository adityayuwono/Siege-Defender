using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class Button_View : ElementView
    {
        private readonly Button_ViewModel _viewModel;

        public Button_View(Button_ViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnShow()
        {
            base.OnShow();

            GameObject.AddComponent<ButtonController>().OnClicked = _viewModel.OnClicked;
        }
    }
}
