using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class ShooterView : IntervalView
    {
        private readonly Shooter _viewModel;

        public ShooterView(Shooter viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            _target = _viewModel.Root.GetView<ObjectView>(_viewModel.Target);
            _source = _viewModel.Root.GetView<ObjectView>(_viewModel.Source);

            _viewModel.IsShooting.OnChange += OnShootingChanged;

            var shootingUI = AttachController<ShootingGUI>();
            shootingUI.Setup(_viewModel);
        }

        private void OnShootingChanged()
        {
            if (_viewModel.IsShooting.GetValue())
                StartInterval();
            else
                StopInterval();
        }

        private ObjectView _target;
        private ObjectView _source;

        protected override void IntervalInvoked()
        {
            var projectile = _viewModel.SpawnProjectile();
            
            if (projectile!=null)
                projectile.Shoot(_source, _target, _viewModel.Accuracy);
        }

        protected override void OnDestroy()
        {
            _target = null;
            _source = null;

            _viewModel.IsShooting.OnChange -= OnShootingChanged;

            base.OnDestroy();
        }
    }
}
