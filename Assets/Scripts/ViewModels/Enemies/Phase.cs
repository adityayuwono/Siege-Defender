using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Phase : TriggerElement
    {
        private readonly PhaseModel _model;

        public Phase(PhaseModel model, Base parent) : base(model, parent)
        {
            _model = model;

        }
    }
}
