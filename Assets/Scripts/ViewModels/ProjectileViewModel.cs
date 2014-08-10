using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Views;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class ProjectileViewModel : ObjectViewModel, IDamageEnemies
    {
        private ProjectileModel _model;

        public ProjectileViewModel(ProjectileModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public System.Action<ObjectView, ObjectView> OnShootAction;

        public void Shoot(ObjectView source, ObjectView target)
        {
            if (OnShootAction != null)
                OnShootAction(source, target);
        }

        public void DamageEnemy(string enemyId)
        {
            var damage = CalculateDamage();
            
            Root.DamageEnemy(enemyId, damage);
        }

        private float CalculateDamage()
        {
            var splitDamage = _model.Damage.Split('-');
            var currentDamage = float.Parse(splitDamage[0]);

            foreach (var damage in splitDamage)
                currentDamage = Random.Range(currentDamage, float.Parse(damage));

            return currentDamage;
        }



        public float AoE
        {
            get { return _model.AoE; }
        }
    }
}
