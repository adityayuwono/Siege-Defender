using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class PlayerHitboxView : ElementView
    {
        private readonly PlayerHitboxViewModel _viewModel;

        public PlayerHitboxView(PlayerHitboxViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnShow()
        {
            base.OnShow();

            AttachController<PlayerHealthController>();
        }
    }
}
