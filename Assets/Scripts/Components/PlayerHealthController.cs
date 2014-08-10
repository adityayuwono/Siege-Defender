using UnityEngine;

namespace Scripts.Components
{
    public class PlayerHealthController : DamageEnemyController
    {
        private void OnTriggerEnter(Collider target)
        {
            var enemy = target.gameObject.GetComponent<EnemyBaseController>();
            if (enemy != null)
            {
                OnHit(enemy.Id);
            }
        }
    }
}
