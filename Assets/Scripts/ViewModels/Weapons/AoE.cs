using Scripts.Models.Weapons;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.ViewModels.Weapons
{
	public class AoE : ProjectileBase
	{
		private readonly AoEModel _model;

		public AoE(AoEModel model, Shooter parent) : base(model, parent)
		{
			_model = model;
		}

		public float Radius
		{
			get { return _model.Radius; }
		}

		public override float HideDelay
		{
			get { return 0.05f; }
		}

		public void Show(Vector3 position)
		{
			if (_model.IsGrounded)
			{
				position.y = 0;
			}

			Position = position;
			base.Show();
			Hide("AoEs are hidden immediately");
		}

		public override void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
		{
			if (targetObject is Boss)
			{
				DamageEnemy(targetObject, collisionPosition);
			}
			else
			{
				DamageEnemy(targetObject, contactPoint);
			}
		}
	}
}