using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Components
{
    public class ProjectileController : DamageEnemyController
    {
        public Action OnDeath;
        public float AoE;

        public void Shoot(Transform target)
        {
            transform.localPosition = Vector3.zero;
            transform.parent = null;

            transform.LookAt(target);
            var tempRot = transform.eulerAngles;
            transform.eulerAngles = tempRot;

            Debug.DrawLine(transform.position, target.position);

            var direction = new Vector3(Random.Range(-0.04f, 0.04f), Random.Range(-0.02f, 0.02f), 1);
            var distanceToTarget = Vector3.Distance(target.position, transform.position);
            Rigidbody.AddRelativeForce(direction * 5000f, ForceMode.Acceleration);
            Rigidbody.AddRelativeTorque(Vector3.right * (50f - distanceToTarget) * 0.05f, ForceMode.Impulse);
        }


        private bool _isHit;
        private void OnCollisionEnter(Collision collisionInfo)
        {
            if (_isHit || IsDead) 
                return;

            var controller = collisionInfo.gameObject.GetComponent<ProjectileTargetController>();
            if (controller != null)
            {
                if (AoE > 1)
                {
                    var aoE = Instantiate(Resources.Load("Projectiles/AoE")) as GameObject;
                    aoE.layer = gameObject.layer;
                    var currentPosition = transform.position;
                    currentPosition.y = 0;
                    aoE.transform.position = currentPosition;
                    aoE.transform.localScale = Vector3.one*AoE/2f;
                    var rb = aoE.AddComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    
                    var aoEController = aoE.AddComponent<AoEController>();
                    aoEController.Setup(ViewModel);
                    aoEController.OnHit = OnHit;

                    aoE.AddComponent<SphereCollider>();
                }

                _isHit = true;

                rigidbody.isKinematic = true;

                // If we actually hit an enemy
                var enemy = controller as EnemyBaseController;
                if (enemy != null)
                {
                    enemy.AttachProjectile(transform);
                    OnHit(enemy.Id);
                }
                else
                    OnDeath();
            }
        }

        


        protected override void OnKilled()
        {
            StartCoroutine(DelayedDeath(1f));
        }

        

        public override void ClearEvents()
        {
            base.ClearEvents();

            OnHit = null;
            OnDeath = null;
        }
    }
}
