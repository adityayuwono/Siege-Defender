using System;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.ViewModels
{
    public class Projectile : ProjectileBase
    {
        private readonly ProjectileModel _model;

        public Projectile(ProjectileModel model, Shooter parent) : base(model, parent)
        {
            _model = model;

            CalculateSpeed();
        }

        private void CalculateSpeed()
        {
            var splitSpeed = _model.SpeedDeviation.Split('-');
            var speed0 = float.Parse(splitSpeed[0]);
            var speed1 = float.Parse(splitSpeed[1]);

            SpeedDeviations = new[] {speed0, speed1};
        }

        public Action<ObjectView, ObjectView, float> DoShooting;
        public readonly Property<bool> IsKinematic = new Property<bool>(); 

        public void Shoot(ObjectView source, ObjectView target, float accuracy)
        {
            if (DoShooting != null)
                DoShooting(source, target, accuracy);
        }

        protected override float CalculateDamage()
        {
            var splitDamage = _model.Damage.Split(Values.DAMAGE_DELIMITER);
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
        public override void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            // BUG: Need checking here because sometimes two collisions can happen very quickly
            if (_hasCollided) return;
            _hasCollided = true;

            IsKinematic.SetValue(true);
            
            // Spawn AoE if there are any Id defined
            if (!string.IsNullOrEmpty(_model.AoEId))
                GetParent<Shooter>().SpawnAoE(_model.AoEId, collisionPosition);

            if (!DamageEnemy(targetObject, collisionPosition, true))
            {
                Hide("Hit Nothing");// If we don't hit an enemy, hide the projectile
            }
        }

        public float Accuracy
        {
            get { return 1-_model.Accuracy; }
        }

        public bool IsRotationRandomized
        {
            get { return _model.IsRotationRandomized; }
        }

        public float[] SpeedDeviations;
    }
}
