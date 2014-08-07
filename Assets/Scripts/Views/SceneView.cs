using Scripts.ViewModels;

namespace Scripts.Views
{
    public class SceneView : ObjectView
    {
        private readonly SceneViewModel _viewModel;

        public SceneView(SceneViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }
}
