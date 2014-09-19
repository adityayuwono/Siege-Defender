using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyBase : Object
    {
        private readonly EnemyBaseModel _model;

        public EnemyBase(EnemyBaseModel model, Object parent) : base(model, parent)
        {
            _model = model;

            Health = new AdjustableProperty<float>("Health", this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Health.SetValue(_model.Health);
        }

        public override void Hide(string reason)
        {
            foreach (var projectile in _projectiles)
                projectile.Hide(reason);
            _projectiles.Clear();

            base.Hide(reason);
        }

        public override float DeathDelay
        {
            get { return 2f; }
        }


        #region Actions

        public Action<ProjectileBaseViewModel> DoAttach;
        public Action<float> OnDamaged;

        #endregion

        #region Health

        public AdjustableProperty<float> Health;

        /// <summary>
        /// Reduce health by the amount specified
        /// </summary>
        /// <param name="damage">How many health we should reduce</param>
        /// <param name="source">Set if we want to attach the object to the target</param>
        public override bool ApplyDamage(float damage, ProjectileBaseViewModel source = null)
        {
            if (source != null)
                AttachProjectile(source);

            var currentHealth = Health.GetValue();

            if (currentHealth > 0)
            {
                currentHealth -= damage;
                if (currentHealth <= 0)
                    Hide("Killed");

                Health.SetValue(currentHealth);
            }

            return true;
        }

        private readonly List<ProjectileBaseViewModel> _projectiles = new List<ProjectileBaseViewModel>();

        private void AttachProjectile(ProjectileBaseViewModel source)
        {
            if (_projectiles.Contains(source))
                throw new EngineException(this, "Duplicate Projectile hit");

            _projectiles.Add(source);

            while (_projectiles.Count > 3)
            {
                var projectileToRemove = _projectiles[0];
                _projectiles.Remove(projectileToRemove);
                projectileToRemove.Hide("Hiding because we have too many already");
            }

            DoAttach(source);
        }

        #endregion

        #region Model Properties

        public virtual float Speed
        {
            get { return _model.Speed; }
        }
        public float Rotation
        {
            get { return _model.Rotation/2f; }
        }

        #endregion
    }
}
