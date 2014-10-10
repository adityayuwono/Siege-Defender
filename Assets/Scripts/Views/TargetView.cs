using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class TargetView : ObjectView
    {
        private readonly Target _viewModel;

        public TargetView(Target viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        public void SetupController(UITexture uiSprite)
        {
            var followMouseController = GameObject.AddComponent<FollowMouse>();
            followMouseController.MainTexture = uiSprite;
            followMouseController.Setup(_viewModel);
        }
    }
}
