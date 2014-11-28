using System;
using Scripts.Models.Enemies;
using UnityEngine;

namespace Scripts.ViewModels.Enemies
{
    /// <summary>
    /// Body part of a Boss
    /// Break when killed, will invoke OnDrop when the Boss is killed if it's Break
    /// </summary>
    public class Limb : LivingObject
    {
        private readonly LimbModel _model;
        private readonly EnemyBase _parent;

        public Limb(LimbModel model, EnemyBase parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;

            _model.DeathDelay = parent.DeathDelay;
        }

        public override bool ApplyDamage(float damage, Vector3 contactPoint, ProjectileBase source = null)
        {
            var damageMultiplied = damage*_model.DamageMultiplier;

            base.ApplyDamage(damageMultiplied, contactPoint, source);
            return _parent.ApplyDamage(damageMultiplied, Vector3.zero);
        }

        private bool _isBroken;
        public event Action OnBreak;
        protected override void OnKilled()
        {
            if (OnBreak != null)
                OnBreak();

            _isBroken = true;

            if (!string.IsNullOrEmpty(_model.CollisionEffectBroken))
                CollisionEffectNormal = _model.CollisionEffectBroken;
        }

        public void Kill()
        {
            if (_isBroken)
                base.OnKilled();
        }
    }
}
