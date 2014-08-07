using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class TargetView : ObjectView
    {
        private readonly TargetViewModel _viewModel;

        public TargetView(TargetViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnShow()
        {
            base.OnShow();

            AttachController<FollowMouse>().Setup(_viewModel);
        }
    }
}
