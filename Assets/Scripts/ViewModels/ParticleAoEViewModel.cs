using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ParticleAoEViewModel : AoEViewModel
    {
        private readonly ParticleAoE_Model _model;
        public ParticleAoEViewModel(ParticleAoE_Model model, ShooterViewModel parent) : base(model, parent)
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
