using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class PiercingProjectile_View : ProjectileView
    {
        private readonly PiercingProjectile_ViewModel _viewModel;
        public PiercingProjectile_View(PiercingProjectile_ViewModel viewModel, ShooterView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void AddRelativeForce(Vector3 direction, ForceMode forceMode = ForceMode.Impulse)
        {
            base.AddRelativeForce(direction*10f, ForceMode.Acceleration);
        }
    }
}
