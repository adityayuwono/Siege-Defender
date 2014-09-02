using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
    public class DamageDisplayView : IntervalView
    {
        private DamageDisplayManager _viewModel;
        public DamageDisplayView(DamageDisplayManager viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }
}
