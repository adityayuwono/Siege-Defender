using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Views
{
	public class SpecialEffectView : ObjectView
	{
		private readonly SpecialEffect _viewModel;
		private ParticleSystem _particleSystem;

		public SpecialEffectView(SpecialEffect viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		public void StopImmediatelly()
		{
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(OnDeath);

			_particleSystem.Stop();
			_particleSystem.Clear(true);
		}

		protected override void OnLoad()
		{
			_viewModel.UpdateParent += UpdateParent;
			_viewModel.OnStopImmediatelly += StopImmediatelly;

			base.OnLoad();

			_particleSystem = GameObject.GetComponent<ParticleSystem>();
			if (_particleSystem == null)
			{
				throw new EngineException(this,
					string.Format("Failed to find ParticleSystem component from {0}", _viewModel.AssetId));
			}

			var particleDuration = 0f;
			foreach (var particleSystem in GameObject.GetComponentsInChildren<ParticleSystem>())
			{
				var startLifetime = particleSystem.main.startLifetime.constant;
				if (particleDuration < startLifetime)
				{
					particleDuration = startLifetime + particleSystem.main.duration;
				}
			}

			_viewModel.SetDeathDelay(particleDuration);
		}

		private void UpdateParent(Object o)
		{
			var view = _viewModel.Root.GetView<ObjectView>(o);
			if (view != null)
			{
				Transform.parent = view.Transform;
				Transform.localPosition = Vector3.zero;
			}
		}

		protected override void OnShow()
		{
			base.OnShow();

			_particleSystem.Play(true);
		}

		protected override void OnDeath(string reason)
		{
			base.OnDeath(reason);

			_particleSystem.Clear(true);
		}
	}
}