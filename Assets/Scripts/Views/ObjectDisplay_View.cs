using Scripts.ViewModels;

namespace Scripts.Views
{
    public class ObjectDisplay_View : IntervalView
    {
        private readonly ObjectDisplay_ViewModel _viewModel;
        public ObjectDisplay_View(ObjectDisplay_ViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;

            _viewModel.CurrentObject.OnChange += CurrentProjectile_OnChange;
        }

        private void CurrentProjectile_OnChange()
        {

        }
    }
}
