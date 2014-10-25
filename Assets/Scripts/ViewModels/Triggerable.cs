using System.Collections.Generic;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
    public class Triggerable : Base
    {
        private readonly TriggerableModel _model;

        protected Triggerable(TriggerableModel model, Base parent) : base(model, parent)
        {
            _model = model;

            foreach (var triggeredModel in _model.Triggers)
            {
                var triggered = Root.IoCContainer.GetInstance<Triggered>(triggeredModel.GetType(), new object[] { triggeredModel, this });
                _triggers.Add(triggered);
            }
        }

        private readonly List<Triggered> _triggers = new List<Triggered>();

        protected override void OnActivate()
        {
            base.OnActivate();
            
            foreach (var triggered in _triggers)
                triggered.Activate();
        }

        protected override void OnDeactivate()
        {
            foreach (var triggered in _triggers)
                triggered.Deactivate("Triggerable is deactivated");

            base.OnDeactivate();
        }
    }
}
