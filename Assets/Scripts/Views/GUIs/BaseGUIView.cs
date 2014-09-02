using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
    public class BaseGUIView : ObjectView
    {
        private BaseGUI _viewModel;
        public BaseGUIView(BaseGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }
}
