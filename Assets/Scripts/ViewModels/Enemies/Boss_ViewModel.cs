using System.Collections.Generic;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Boss_ViewModel : EnemyBaseViewModel
    {
        private readonly Boss_Model _model;
        public Boss_ViewModel(Boss_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;

            foreach (var phaseModel in _model.Phases)
                _phases.Add(new Phase_ViewModel(phaseModel, this));
        }

        private readonly List<Phase_ViewModel> _phases = new List<Phase_ViewModel>();

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
