using System;
using Scripts.Core;
using Scripts.Models;
using Scripts.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.ViewModels
{
    public class ProjectileViewModel : ProjectileBaseViewModel
    {
        private readonly ProjectileModel _model;
        private readonly ShooterViewModel _parent;

        public ProjectileViewModel(ProjectileModel model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;
        }

        public Action<ObjectView, ObjectView, float> DoShooting;
        public readonly Property<bool> IsKinematic = new Property<bool>(); 

        public void Shoot(ObjectView source, ObjectView target, float accuracy)
        {
            if (DoShooting != null)
                DoShooting(source, target, accuracy);
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

            _hasCollided = false;
            IsKinematic.SetValue(false);
        }

        public override Vector3 Position
        {
            get { return Vector3.zero; }
        }

        private bool _hasCollided;
        public override void CollideWithTarget(ObjectViewModel targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            // Need checking here because sometimes two collisions can happen very quickly
            if (_hasCollided) return;
            _hasCollided = true;

            IsKinematic.SetValue(true);
            
            // Spawn AoE if there are any Id defined
            if (!string.IsNullOrEmpty(_model.AoEId))
                _parent.SpawnAoE(_model.AoEId, collisionPosition);
            
            if (!DamageEnemy(targetObject, contactPoint, true))
            {
                Hide("Hit Nothing");// If we don't hit an enemy, hide the projectile
            }
        }

        public float Accuracy
        {
            get { return 1-_model.Accuracy; }
        }
    }
}
