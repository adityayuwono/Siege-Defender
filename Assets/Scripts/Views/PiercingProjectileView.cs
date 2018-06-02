using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class PiercingProjectileView : ProjectileView
    {
        private readonly PiercingProjectile _viewModel;
        public PiercingProjectileView(PiercingProjectile viewModel, ShooterView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void AddRelativeForce(float strength, ForceMode forceMode = ForceMode.Impulse)
        {
			base.AddRelativeForce(strength * 10f, ForceMode.Acceleration);
        }
    }
}
