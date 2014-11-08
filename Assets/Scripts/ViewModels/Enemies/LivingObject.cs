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

            SpecialEffect = new AdjustableProperty<string>("SpecialEffect", this, true);
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
                projectile.Hide(reason);
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
            if (contactPoint != Vector3.zero)
            {
                Root.DamageDisplay.DisplayDamage(damage, contactPoint);

                if (!string.IsNullOrEmpty(CollisionEffectNormal))
                    Root.SpecialEffectManager.DisplaySpecialEffect(CollisionEffectNormal, contactPoint);
            }

            return true;
        }

        protected virtual void OnKilled()
        {
            if (!string.IsNullOrEmpty(_model.LootTableId))
            {
                var items = Root.GetLoot(_model.LootTableId);
                if (items != null)
                {
                    SpecialEffect.SetValue("ItemDrop");
                }
            }
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

        public readonly AdjustableProperty<string> SpecialEffect;
        public Action<ProjectileBase> DoAttach;
        private readonly List<ProjectileBase> _attachedProjectiles = new List<ProjectileBase>();
        public readonly AdjustableProperty<float> Health;
        protected string CollisionEffectNormal { private get; set; }
    }
}
