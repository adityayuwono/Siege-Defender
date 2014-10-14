﻿using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels.Enemies
{
    public class LivingObject : Object
    {
        private readonly LivingObjectModel _model;
        public LivingObject(LivingObjectModel model, Base parent) : base(model, parent)
        {
            _model = model;

            Health = new AdjustableProperty<float>("Health", this);
            CollisionEffectNormal = _model.CollisionEffectNormal;

            if (_model.Trigger != null)
                _trigger = new Triggered(_model.Trigger, this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Health.SetValue(_model.Health);

            if (_trigger != null)
                _trigger.Activate();
        }

        protected override void OnDeactivate()
        {
            if (_trigger != null)
                _trigger.Deactivate(string.Format("{0} is deactivated", GetType()));

            base.OnDeactivate();
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
        }

        public override void Hide(string reason)
        {
            foreach (var projectile in _projectiles)
                projectile.Hide(reason);
            _projectiles.Clear();

            base.Hide(reason);
        }

        private readonly Triggered _trigger;

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
                AttachProjectile(source);

            var currentHealth = Health.GetValue();

            // To avoid killing the enemy more than once
            if (currentHealth > 0)
            {
                currentHealth -= damage;
                if (currentHealth <= 0)
                    OnKilled();

                Health.SetValue(currentHealth);
            }

            // Because Vector3 is a struct and structs can't be null
            if (Vector3.zero != contactPoint)
            {
                Root.DamageDisplay.DisplayDamage(damage, contactPoint);

                if (!string.IsNullOrEmpty(CollisionEffectNormal))
                    Root.SpecialEffectManager.DisplaySpecialEffect(CollisionEffectNormal, contactPoint);
            }


            return true;
        }

        protected virtual void OnKilled()
        {
            
        }

        private void AttachProjectile(ProjectileBase source)
        {
            if (_projectiles.Contains(source))
                throw new EngineException(this, "Duplicate Projectile hit");

            _projectiles.Add(source);

            while (_projectiles.Count > _model.ProjectileLimit)
            {
                var projectileToRemove = _projectiles[0];
                _projectiles.Remove(projectileToRemove);
                projectileToRemove.Hide("Hiding because we have too many already");
            }

            DoAttach(source);
        }

        public Action<ProjectileBase> DoAttach;
        private readonly List<ProjectileBase> _projectiles = new List<ProjectileBase>();
        public readonly AdjustableProperty<float> Health;
        protected string CollisionEffectNormal { private get; set; }
    }
}
