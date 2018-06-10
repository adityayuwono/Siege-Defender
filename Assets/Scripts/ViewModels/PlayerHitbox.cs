using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
	public class PlayerHitbox : Element
	{
		public PlayerHitbox(PlayerHitboxModel model, Object parent) : base(model, parent)
		{
		}

		public void CollideWithTarget(Object obj)
		{
			var enemyViewModel = obj as Enemy;
			if (enemyViewModel != null)
			{
				enemyViewModel.ApplyDamage(float.PositiveInfinity, false, Vector3.zero);
			}
		}
	}
}