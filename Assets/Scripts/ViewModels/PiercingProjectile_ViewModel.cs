using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class PiercingProjectile_ViewModel : ProjectileViewModel
    {
        private readonly PiercingProjectile_Model _model;
        private readonly ShooterViewModel _parent;
        public PiercingProjectile_ViewModel(PiercingProjectile_Model model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;
        }

        public override void Show()
        {
            base.Show();
            _damageMultiplier = 1;
            Hide("Hide from show :D");
        }

        protected override float CalculateDamage()
        {
            return base.CalculateDamage()*(_damageMultiplier);
        }

        private float _damageMultiplier;
        public override void CollideWithTarget(ObjectViewModel targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            // Spawn AoE if there are any Id defined
            if (!string.IsNullOrEmpty(_model.AoEId))
                _parent.SpawnAoE(_model.AoEId, collisionPosition);

            if (DamageEnemy(targetObject, contactPoint, false))
                _damageMultiplier *= 0.75f;
            else
                _damageMultiplier = 0.25f;
        }

        public override float HideDelay
        {
            get { return 2f; }
        }
    }
}
