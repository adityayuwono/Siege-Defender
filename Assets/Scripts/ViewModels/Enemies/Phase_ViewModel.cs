using Scripts.Models;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Phase_ViewModel : TriggerElement_ViewModel
    {
        private readonly Phase_Model _model;

        public Phase_ViewModel(Phase_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

        }
    }
}
