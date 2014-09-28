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

            _trigger = new Triggered(_model.Trigger, this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            _trigger.Activate();
        }

        protected override void OnDeactivate()
        {
            _trigger.Deactivate("Limb is deactivated");

            base.OnDeactivate();
        }

        private readonly Triggered _trigger;

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
