using System;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Limb : LivingObject
    {
        private readonly LimbModel _model;
        private readonly EnemyBase _parent;

        public Limb(LimbModel model, EnemyBase parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;

            Trigger = new Triggered(_model.Trigger, this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Trigger.Activate();
        }

        protected override void OnDeactivate()
        {
            Trigger.Deactivate("Limb is deactivated");

            base.OnDeactivate();
        }

        private Triggered Trigger;

        public override bool ApplyDamage(float damage, ProjectileBase source = null)
        {
            base.ApplyDamage(damage, source);
            return _parent.ApplyDamage(damage, null);
        }

        public Action DoBreakParts;
        protected override void OnKilled()
        {
            if (DoBreakParts != null)
                DoBreakParts();
        }
    }
}
