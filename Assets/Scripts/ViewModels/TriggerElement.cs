using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
    public class TriggerElement : Base
    {
        private readonly TriggerElementModel _model;
        protected TriggerElement(TriggerElementModel model, Base parent) : base(model, parent)
        {
            _model = model;

            _triggered = new Triggered(_model.Trigger, this);
        }

        private readonly Triggered _triggered;

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
