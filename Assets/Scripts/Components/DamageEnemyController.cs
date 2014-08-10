using System;
using Scripts.Interfaces;

namespace Scripts.Components
{
    public class DamageEnemyController : RigidbodyController
    {
        public Action<string> OnHit;

        protected override void OnSetup()
        {
            base.OnSetup();

            var iDamageEnemies = View as IDamageEnemies;
            if (iDamageEnemies != null)
                OnHit = iDamageEnemies.DamageEnemy;
        }
    }
}
