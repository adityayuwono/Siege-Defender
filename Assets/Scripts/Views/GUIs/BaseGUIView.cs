using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
    public class BaseGUIView : ElementView
    {
        private readonly BaseGUI _viewModel;

        public BaseGUIView(BaseGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }
}
