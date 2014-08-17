using System;
using System.Collections.Generic;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyBaseViewModel : ObjectViewModel
    {
        private readonly EnemyBaseModel _model;

        public EnemyBaseViewModel(EnemyBaseModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Health = _model.Health;
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
                    Destroy();
            }
        }

        private readonly List<ProjectileBaseViewModel> _projectiles = new List<ProjectileBaseViewModel>();

        private void AttachProjectile(ProjectileBaseViewModel source)
        {
            _projectiles.Add(source);
            DoAttach(source);
        }

        #endregion

        public float Speed { get { return _model.Speed; } }
    }
}
