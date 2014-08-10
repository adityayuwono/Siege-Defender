using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Components
{
    public class AoEController : BaseController
    {
        public Action<string> OnHit;

        protected override void OnSetup()
        {
            base.OnSetup();

            renderer.enabled = true;
        }

        private void OnCollisionEnter(Collision collisionInfo)
        {
            StartCoroutine(DelayedDeath(0.5f));

            var controller = collisionInfo.gameObject.GetComponent<ProjectileTargetController>();
            if (controller != null)
            {
                rigidbody.isKinematic = true;

                // If we actually hit an enemy
                var enemy = controller as EnemyBaseController;
                if (enemy != null)
                {
                    OnHit(enemy.Id);
                }
            }
        }
    }
}
