using System;
using Scripts.Helpers;
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

        public override void Hide(string reason)
        {
            base.Hide(reason);

            Elements.Clear();
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

        private float Health { get; set; }

        /// <summary>
        /// Reduce health by the amount specified
        /// </summary>
        /// <param name="damage">How many health we should reduce</param>
        /// <param name="source">Set if we want to attach the object to the target</param>
        public void ApplyDamage(float damage, ProjectileBaseViewModel source = null)
        {
            if (source != null)
                AttachProjectile(source);

            if (Health > 0)
            {
                Health -= damage;
                OnDamaged(damage);
                if (Health <= 0)
                    Hide("Killed");
            }
        }

        private void AttachProjectile(ProjectileBaseViewModel source)
        {
            if (Elements.Contains(source))
                throw new EngineException(this, "Duplicate Projectile hit");

            Elements.Add(source);

            while (Elements.Count > 3)
            {
                var elementToRemove = Elements[0];
                Elements.Remove(elementToRemove);
                elementToRemove.Hide("Hiding because we have too many already");
            }

            DoAttach(source);
        }

        #endregion

        #region Model Properties

        public float Speed
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
