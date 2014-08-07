using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class PlayerView : ElementView
    {
        private readonly PlayerViewModel _viewModel;

        public PlayerView(PlayerViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }



        protected override void OnShow()
        {
            base.OnShow();

            AttachController<AccelerometerController>();
        }
    }
}
