using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class PiercingProjectile_ViewModel : ProjectileViewModel
    {
        private readonly PiercingProjectile_Model _model;
        public PiercingProjectile_ViewModel(PiercingProjectile_Model model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Show()
        {
            base.Show();

            _damageMultiplier = 1;
            Hide("Hide since show, this is weird, :D:D:D");
        }

        protected override float CalculateDamage()
        {
            return base.CalculateDamage()*_damageMultiplier;
        }

        private float _damageMultiplier;
        public override void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            // Spawn AoE if there are any Id defined
            if (!string.IsNullOrEmpty(_model.AoEId))
                GetParent<ShooterViewModel>().SpawnAoE(_model.AoEId, collisionPosition);

            if (DamageEnemy(targetObject, contactPoint, false))
                _damageMultiplier *= _model.DamageReduction;// Every enemy hit by piercing will reduce it's effectiveness
        }

        public override float HideDelay
        {
            get { return 2f; }
        }
    }
}
