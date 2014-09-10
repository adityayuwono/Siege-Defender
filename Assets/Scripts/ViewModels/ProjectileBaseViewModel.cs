using Scripts.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.ViewModels
{
    public class ProjectileBaseViewModel : ObjectViewModel
    {
        private readonly Projectile_Model _model;
        public ProjectileBaseViewModel(Projectile_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        protected virtual float CalculateDamage()
        {
            var splitDamage = _model.Damage.Split('-');
            var currentDamage = float.Parse(splitDamage[0]);

            foreach (var damage in splitDamage)
                currentDamage = Random.Range(currentDamage, float.Parse(damage));

            return currentDamage;
        }

        public virtual void CollideWithTarget(ObjectViewModel targetObject, Vector3 collisionPosition, Vector3 contactPoint) { }

        protected bool DamageEnemy(ObjectViewModel enemy, Vector3 contactPoint, bool attachToEnemy = false)
        {
            var damage = CalculateDamage();
            var isDamageApplied = enemy.ApplyDamage(damage, attachToEnemy ? this : null);
            if (isDamageApplied)
                Root.DamageDisplay.DisplayDamage(damage, contactPoint);
            return isDamageApplied;
        }

        public virtual float HideDelay
        {
            get { return 0f; }
        }
    }
}
