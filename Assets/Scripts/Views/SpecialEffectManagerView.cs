using Scripts.ViewModels;

namespace Scripts.Views
{
    public class SpecialEffectManagerView : IntervalView
    {
        private readonly SpecialEffectManager _viewModel;
        public SpecialEffectManagerView(SpecialEffectManager viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }
}
