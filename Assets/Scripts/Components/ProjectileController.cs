using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Components
{
    public class ProjectileController : DamageEnemyController
    {
        public Action OnDeath;
        public float AoE;

        private GameObject aoeGameObject;
        protected override void OnSetup()
        {
            base.OnSetup();

            var aoe = transform.FindChild("AoE");
            if (aoe != null)
                aoeGameObject = aoe.gameObject;
        }

        public void Shoot(Transform target)
        {
            transform.localPosition = Vector3.zero;
            transform.parent = null;

            transform.LookAt(target);
            var tempRot = transform.eulerAngles;
            transform.eulerAngles = tempRot;

            Debug.DrawLine(transform.position, target.position);

            var direction = new Vector3(Random.Range(-0.04f, 0.04f), Random.Range(-0.02f, 0.02f), 1);
            Rigidbody.AddRelativeForce(direction * 5000f, ForceMode.Acceleration);
        }

        private void Update()
        {
            if (_isHit || IsDead)
                return;

            if (transform.position.magnitude > 1000f)
                OnDeath();
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
                    var aoE = Instantiate(aoeGameObject) as GameObject;
                    aoE.layer = gameObject.layer;
                    var currentPosition = transform.position;
                    currentPosition.y = 0;
                    aoE.transform.position = currentPosition;
                    aoE.transform.localScale = Vector3.one*AoE/2f;
                    var rb = aoE.AddComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    aoE.AddComponent<SphereCollider>();

                    var aoEController = aoE.AddComponent<AoEController>();
                    aoEController.OnHit = OnHit;
                    aoEController.Setup(ViewModel);
                }

                _isHit = true;

                rigidbody.isKinematic = true;

                // If we actually hit an enemy
                var enemy = controller as EnemyBaseController;
                if (enemy != null)
                {
                    enemy.AttachProjectile(this);
                    OnHit(enemy.Id);
                }
                else
                    OnDeath();
            }
        }



        protected override void OnKilled()
        {
            StartCoroutine(DelayedDeath(0.5f));
        }

        

        public override void ClearEvents()
        {
            base.ClearEvents();

            OnHit = null;
            OnDeath = null;
        }
    }
}
