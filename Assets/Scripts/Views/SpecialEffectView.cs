using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class SpecialEffectView : ObjectView
    {
        private readonly SpecialEffect _viewModel;
        public SpecialEffectView(SpecialEffect viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        private ParticleSystem _particleSystem;
        protected override void OnLoad()
        {
            base.OnLoad();

            _particleSystem = GameObject.GetComponent<ParticleSystem>();
            if (_particleSystem == null)
                throw new EngineException(this, string.Format("Failed to find ParticleSystem component from {0}", _viewModel.AssetId));

            var particleDuration = 0f;
            foreach (var particleSystem in GameObject.GetComponentsInChildren<ParticleSystem>())
                if (particleDuration < particleSystem.startLifetime)
                    particleDuration = particleSystem.startLifetime + particleSystem.duration;

            _viewModel.SetDeathDelay(particleDuration);
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
