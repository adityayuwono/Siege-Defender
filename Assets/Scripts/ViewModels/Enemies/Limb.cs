using System;
using Scripts.Models.Enemies;
using UnityEngine;

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

        public override bool ApplyDamage(float damage, Vector3 contactPoint, ProjectileBase source = null)
        {
            var damageMultiplied = damage*_model.DamageMultiplier;

            base.ApplyDamage(damageMultiplied, contactPoint, source);
            return _parent.ApplyDamage(damageMultiplied, contactPoint, null);
        }

        public Action DoBreakParts;
        protected override void OnKilled()
        {
            if (DoBreakParts != null)
                DoBreakParts();
        }
    }
}
