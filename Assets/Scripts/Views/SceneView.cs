using Scripts.ViewModels;

namespace Scripts.Views
{
    public class SceneView : ObjectView
    {
        private readonly Scene _viewModel;

        public SceneView(Scene viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }
}
