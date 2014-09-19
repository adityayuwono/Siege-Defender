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

        protected override void OnLoad()
        {
            base.OnLoad();

            AttachController<FollowMouse>().Setup(_viewModel);
        }
    }
}
