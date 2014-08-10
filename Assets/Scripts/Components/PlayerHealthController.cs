using UnityEngine;

namespace Scripts.Components
{
    public class PlayerHealthController : RigidbodyController
    {
        private void OnTriggerEnter(Collider target)
        {
            var controller = target.gameObject.GetComponent<ProjectileTargetController>();
            if (controller != null)
            {
                Destroy(target.gameObject);
            }
        }
    }
}
