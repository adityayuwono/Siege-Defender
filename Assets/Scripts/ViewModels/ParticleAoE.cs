using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ParticleAoE : AoE
    {
        private readonly ParticleAoEModel _model;
        public ParticleAoE(ParticleAoEModel model, Shooter parent) : base(model, parent)
        {
            _model = model;
        }


        private float _deathDelay;
        public override float DeathDelay
        {
            get { return _deathDelay; }
        }
        public void SetDeathDelay(float delay)
        {
            _deathDelay = delay;
        }
    }
}
