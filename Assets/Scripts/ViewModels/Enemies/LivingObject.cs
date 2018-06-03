using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Enemies
{
    /// <summary>
    /// Base class for everything that have Health
    /// </summary>
    public class LivingObject : Object
    {
	    public event Action Death;
	    public Action<ProjectileBase> DoAttach;

	    public readonly AdjustableProperty<string> SpecialEffect;
	    public readonly AdjustableProperty<float> Health;
	    
        private readonly LivingObjectModel _model;
	    private readonly List<ProjectileBase> _attachedProjectiles = new List<ProjectileBase>();

        protected LivingObject(LivingObjectModel model, Base parent) : base(model, parent)
        {
            _model = model;

            // Validate Model
            if (string.IsNullOrEmpty(_model.Type))
                throw new EngineException(this, string.Format("Type for: {0} is empty", FullId));

            Health = new AdjustableProperty<float>("Health", this);
            CollisionEffectNormal = _model.CollisionEffectNormal;

            SpecialEffect = new AdjustableProperty<string>("SpecialEffect", this, true);
        }

	    protected string CollisionEffectNormal { private get; set; }
		
	    public bool IsDead
	    {
		    get { return Health.GetValue() <= 0; }
	    }

        protected override void OnActivate()
        {
            // Need to be set first because some conditions rely on this
            Health.SetValue(_model.Health);

            base.OnActivate();
        }
        
        public override void Hide(string reason)
        {
            // Cleanup attached projectiles
	        foreach (var projectile in _attachedProjectiles)
	        {
		        projectile.Hide(reason);
	        }
            _attachedProjectiles.Clear();

            base.Hide(reason);
        }

        /// <summary>
        /// Reduce health by the amount specified
        /// Health is only reduced if it is above 0
        /// </summary>
        /// <param name="damage">How many health we should reduce</param>
        /// <param name="contactPoint">Impact coordinate for displaying damage</param>
        /// <param name="source">Set if we want to attach the object to the target</param>
        public override bool ApplyDamage(float damage, Vector3 contactPoint, ProjectileBase source = null)
        {
	        if (source != null)
	        {
		        AttachProjectile(source);
	        }

            var currentHealth = Health.GetValue();

            // To avoid killing the enemy more than once
            if (currentHealth > 0)
            {
                currentHealth -= damage;
	            if (currentHealth <= 0)
	            {
		            OnKilled();
	            }

                Health.SetValue(currentHealth);
            }

            // Because Vector3 is a struct and structs can't be null
            if (contactPoint != Vector3.zero)
            {
                Root.DamageDisplay.DisplayDamage(damage, contactPoint);

	            if (!string.IsNullOrEmpty(CollisionEffectNormal))
	            {
		            Root.SpecialEffectManager.DisplaySpecialEffect(CollisionEffectNormal, contactPoint);
	            }
            }

            return true;
        }
		
        /// <summary>
        /// Called once when damage taken is greater or equal to Health
        /// </summary>
        protected virtual void OnKilled()
        {
            Root.LogEvent("Enemies", "Killed", _model.Type, 1);

            if (Death != null)
                Death();
        }

        private void AttachProjectile(ProjectileBase source)
        {
            if (_attachedProjectiles.Contains(source))
                throw new EngineException(this, "Duplicate Projectile hit");

            _attachedProjectiles.Add(source);

            while (_attachedProjectiles.Count > _model.ProjectileLimit)
            {
                var projectileToRemove = _attachedProjectiles[0];
                _attachedProjectiles.Remove(projectileToRemove);
                projectileToRemove.Hide("Hiding because we have too many already");
            }

            DoAttach(source);
        }
    }
}
