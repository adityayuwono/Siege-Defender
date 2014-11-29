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
                var triggered = Root.IoCContainer.GetInstance<Triggered>(triggeredModel.GetType(), new object[] {triggeredModel, this});
                _triggers.Add(triggered);
            }
        }

        private readonly List<Triggered> _triggers = new List<Triggered>();

        public override void Show()
        {
            base.Show();

            foreach (var triggered in _triggers)
                triggered.Activate();
        }

        public override void Hide(string reason)
        {
            foreach (var triggered in _triggers)
                triggered.Deactivate("Triggerable is deactivated");

            base.Hide(reason);
        }
    }
}
