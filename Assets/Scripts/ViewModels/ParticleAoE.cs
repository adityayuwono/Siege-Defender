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

        public void SetDeathDelay(float delay)
        {
            _model.DeathDelay = delay;
        }
    }
}
