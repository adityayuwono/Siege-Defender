using System;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyBaseViewModel : ObjectViewModel
    {
        private readonly EnemyBase_Model _model;

        public EnemyBaseViewModel(EnemyBase_Model model, ObjectViewModel parent) : base(model, parent)
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
