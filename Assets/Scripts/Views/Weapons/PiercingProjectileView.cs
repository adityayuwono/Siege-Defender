using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.Views.Weapons
{
	public class PiercingProjectileView : ProjectileView
	{
		public PiercingProjectileView(PiercingProjectile viewModel, ShooterView parent)
			: base(viewModel, parent)
		{
		}

		protected override void AddRelativeForce(float strength, ForceMode forceMode = ForceMode.Impulse)
		{
			base.AddRelativeForce(strength * 10f, ForceMode.Acceleration);
		}
	}
}