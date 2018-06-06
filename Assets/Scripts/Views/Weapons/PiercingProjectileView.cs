using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.Views.Weapons
{
	public class PiercingProjectileView : ProjectileView
	{
		private readonly PiercingProjectile _viewModel;

		public PiercingProjectileView(PiercingProjectile viewModel, ShooterView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void AddRelativeForce(float strength, ForceMode forceMode = ForceMode.Impulse)
		{
			base.AddRelativeForce(strength * 10f, ForceMode.Acceleration);
		}
	}
}