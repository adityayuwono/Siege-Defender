using System;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Views;
using Random = UnityEngine.Random;

namespace Scripts.ViewModels
{
    public class ProjectileViewModel : ProjectileBaseViewModel
    {
        private readonly ProjectileModel _model;

        public ProjectileViewModel(ProjectileModel model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public Action<ObjectView, ObjectView> DoShooting;
        public readonly Property<bool> IsKinematic = new Property<bool>(); 

        public void Shoot(ObjectView source, ObjectView target)
        {
            if (DoShooting != null)
                DoShooting(source, target);
        }

        public override float DeathDelay
        {
            get { return 1f; }
        }

        protected override float CalculateDamage()
        {
            var splitDamage = _model.Damage.Split('-');
            var currentDamage = float.Parse(splitDamage[0]);

            foreach (var damage in splitDamage)
                currentDamage = Random.Range(currentDamage, float.Parse(damage));

            return currentDamage;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            IsKinematic.SetValue(false);
        }

        public override void CollideWithTarget(ObjectViewModel obj)
        {
            IsKinematic.SetValue(true);

            if (_model.AoE != null)
            {
                var aoeVM = new AoEViewModel(_model.AoE, this);
                aoeVM.Activate();
                aoeVM.Show();
            }

            var enemyViewModel = obj as EnemyBaseViewModel;
            if (enemyViewModel != null)
            {
                enemyViewModel.ApplyDamage(CalculateDamage(), this);
            }
            else
            {
                Hide();
            }
        }
    }
}
