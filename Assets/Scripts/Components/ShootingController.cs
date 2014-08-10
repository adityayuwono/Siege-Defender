using Scripts.ViewModels;

namespace Scripts.Components
{
    public class ShootingController : IntervalController
    {
        private ShooterViewModel _viewModel;

        protected override void OnSetup()
        {
            base.OnSetup();

            _viewModel = ViewModel as ShooterViewModel;
            _viewModel.IsShooting.OnChange += OnShootingChanged;
        }

        private void OnShootingChanged()
        {
            if (_viewModel.IsShooting.GetValue())
                StartInterval();
            else
                StopInterval();
        }
    }
}
