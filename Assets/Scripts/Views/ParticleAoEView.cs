using System.Collections.Generic;
using System.Linq;
using Scripts.Helpers;
using Scripts.ViewModels.Weapons;
using Scripts.Views.Weapons;
using UnityEngine;

namespace Scripts.Views
{
	public class ParticleAoEView : AoEView
	{
		private readonly ParticleAoE _viewModel;

		private List<ParticleSystem> _particleSystem;

		public ParticleAoEView(ParticleAoE viewModel, ShooterView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_particleSystem = GameObject.GetComponentsInChildren<ParticleSystem>().ToList();
			if (_particleSystem == null)
			{
				throw new EngineException(this,
					string.Format("Failed to find ParticleSystem component from {0}", _viewModel.AssetId));
			}

			var particleDuration = 0f;
			foreach (var particleSystem in _particleSystem)
			{
				if (particleDuration < particleSystem.startLifetime)
				{
					particleDuration = particleSystem.startLifetime + particleSystem.duration;
				}

				particleSystem.startSize *= _viewModel.Radius / 2f;
				particleSystem.Play(true);
			}

			_viewModel.SetDeathDelay(particleDuration);
		}

		protected override void OnDeath(string reason)
		{
			base.OnDeath(reason);
			foreach (var particleSystem in _particleSystem)
			{
				particleSystem.Clear(true);
			}
		}
	}
}