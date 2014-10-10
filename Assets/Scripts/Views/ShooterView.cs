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
            for (var i = 0; i < _viewModel.Scatters; i++)
            {
                var projectile = _viewModel.SpawnProjectile();

                if (projectile != null)
                    projectile.Shoot(_source, _target, _viewModel.Accuracy);
            }
        }

        protected override void OnDestroy()
        {
            _target = null;
            _source = null;

            _viewModel.IsShooting.OnChange -= OnShootingChanged;

            base.OnDestroy();
        }

        public void SetupController(UITexture uiSprite)
        {
            var shootingUI = GameObject.AddComponent<ShootingGUI>();
            shootingUI.MainTexture = uiSprite;
            shootingUI.Setup(_viewModel);
        }
    }
}
