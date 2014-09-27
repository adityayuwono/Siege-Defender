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
        }

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
