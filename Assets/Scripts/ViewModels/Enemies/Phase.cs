using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Phase : TriggerElement_ViewModel
    {
        private readonly Phase_Model _model;

        public Phase(Phase_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

        }
    }
}
