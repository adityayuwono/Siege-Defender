using Scripts.Components;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
	public class ShooterView : IntervalView
	{
		private readonly Shooter _viewModel;

		private float _lastInvocationTime;

		private ObjectView _target;

		public ShooterView(Shooter viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_target = _viewModel.Root.GetView<ObjectView>(_viewModel.Target);

			_viewModel.IsShooting.OnChange += OnShootingChanged;
		}

		private void OnShootingChanged()
		{
			if (_viewModel.IsShooting.GetValue())
			{
				if (Time.time - _lastInvocationTime > _viewModel.Interval.GetValue())
				{
					_lastInvocationTime = Time.time;
					StartInterval();
				}
			}
			else
			{
				StopInterval();
			}
		}

		protected override void IntervalInvoked()
		{
			// Shoot once for each scatter value
			for (var i = 0; i < _viewModel.Scatters; i++)
			{
				var projectile = _viewModel.SpawnProjectile();

				if (projectile != null)
					projectile.Shoot(_target, _viewModel.Accuracy);
			}
		}

		protected override void OnDestroy()
		{
			_target = null;

			_viewModel.IsShooting.OnChange -= OnShootingChanged;

			base.OnDestroy();
		}

		public void SetupController(UITexture uiSprite)
		{
			var shootingUI = GameObject.AddComponent<ShootingController>();
			shootingUI.MainTexture = uiSprite;
			shootingUI.Setup(_viewModel);
		}
	}
}