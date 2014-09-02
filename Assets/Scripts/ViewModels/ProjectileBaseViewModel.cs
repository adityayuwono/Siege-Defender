using Scripts.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.ViewModels
{
    public class ProjectileBaseViewModel : ObjectViewModel
    {
        private readonly ProjectileModel _model;
        public ProjectileBaseViewModel(ProjectileModel model, ObjectViewModel parent) : base(model, parent)
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

        protected void DamageEnemy(EnemyBaseViewModel enemy, Vector3 contactPoint, bool attachToEnemy = false)
        {
            var damage = CalculateDamage();
            Root.DamageDisplay.DisplayDamage(damage, contactPoint);
            enemy.ApplyDamage(damage, attachToEnemy ? this : null);
        }
    }
}
