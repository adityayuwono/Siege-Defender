using System.Collections.Generic;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Boss : EnemyBaseViewModel
    {
        private readonly BossModel _model;
        public Boss(BossModel model, Object parent) : base(model, parent)
        {
            _model = model;

            foreach (var phaseModel in _model.Phases)
                _phases.Add(new Phase(phaseModel, this));
        }

        private readonly List<Phase> _phases = new List<Phase>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var phase in _phases)
                phase.Activate();
        }


        public override float Speed
        {
            get { return 0; }
        }
    }
}
