using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
    public class TriggerElement_ViewModel : BaseViewModel
    {
        private readonly TriggerElement_Model _model;
        protected TriggerElement_ViewModel(TriggerElement_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

            _triggered = new Triggered_ViewModel(_model.Trigger, this);
        }

        private readonly Triggered_ViewModel _triggered;

        protected override void OnActivate()
        {
            base.OnActivate();

            _triggered.Activate();
        }

        protected override void OnDeactivate()
        {
            _triggered.Deactivate("Phase is deactivated");

            base.OnDeactivate();
        }
    }
}
