using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class PlayerHitbox : Element
    {
        private readonly PlayerHitboxModel _model;

        public PlayerHitbox(PlayerHitboxModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        public void CollideWithTarget(Object obj)
        {
            var enemyViewModel = obj as Enemy;
	        if (enemyViewModel != null)
	        {
		        enemyViewModel.ApplyDamage(float.PositiveInfinity, Vector3.zero);
	        }
        }
    }
}
