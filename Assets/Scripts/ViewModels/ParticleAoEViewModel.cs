using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ParticleAoEViewModel : AoEViewModel
    {
        private readonly ParticleAoEModel _model;
        public ParticleAoEViewModel(ParticleAoEModel model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
