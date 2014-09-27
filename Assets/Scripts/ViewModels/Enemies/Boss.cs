using System.Collections.Generic;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Boss : EnemyBase
    {
        private readonly BossModel _model;
        public Boss(BossModel model, Object parent) : base(model, parent)
        {
            _model = model;

            foreach (var limbModel in _model.Limbs)
                _limbs.Add(new Limb(limbModel, this));

            foreach (var phaseModel in _model.Phases)
                _phases.Add(new Phase(phaseModel, this));
        }

        private readonly List<Limb> _limbs = new List<Limb>();
        private readonly List<Phase> _phases = new List<Phase>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var limb in _limbs)
                limb.Activate();

            foreach (var phase in _phases)
                phase.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var limb in _limbs)
                limb.Show();
        }


        public override float Speed
        {
            get { return 0; }
        }
    }
}
