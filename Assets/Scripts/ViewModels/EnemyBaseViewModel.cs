using System;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyBaseViewModel : ObjectViewModel
    {
        private readonly EnemyBaseModel _model;

        public EnemyBaseViewModel(EnemyBaseModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Health = _model.Health;
        }

        public override void Hide()
        {
            base.Hide();

            Children.Clear();
        }

        public override float DeathDelay
        {
            get { return 2f; }
        }


        #region Actions
        public Action<ProjectileBaseViewModel> DoAttach; 
        #endregion

        #region Health

        private float Health { get; set; }
        public void ApplyDamage(float damage, ProjectileBaseViewModel source)
        {
            if (source != null)
                AttachProjectile(source);

            if (Health > 0)
            {
                Health -= damage;
                if (Health <= 0)
                    Hide();
            }
        }

        private void AttachProjectile(ProjectileBaseViewModel source)
        {
            Children.Add(source);
            DoAttach(source);
        }

        #endregion

        #region Model Properties
        public float Speed { get { return _model.Speed; } }
        #endregion
    }
}
