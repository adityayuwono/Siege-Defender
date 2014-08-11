using System;
using Scripts.Interfaces;

namespace Scripts.Components
{
    public class DamageEnemyController : RigidbodyController
    {
        protected Action<string> OnHit;

        protected override void OnSetup()
        {
            base.OnSetup();

            var iDamageEnemies = ViewModel as IDamageEnemies;
            if (iDamageEnemies != null)
                OnHit = iDamageEnemies.DamageEnemy;
        }
    }
}
