using Scripts.Models.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Weapons
{
	public class PiercingProjectile : Projectile
	{
		private readonly PiercingProjectileModel _model;

		private float _damageMultiplier;

		public PiercingProjectile(PiercingProjectileModel model, Shooter parent) 
			: base(model, parent)
		{
			_model = model;
		}

		public override float HideDelay
		{
			get { return 2f; }
		}

		public override void Show()
		{
			base.Show();

			_damageMultiplier = 1;
			Hide("Hide since show, this is weird, :D:D:D");
		}

		public override void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
		{
			// Spawn AoE if there are any Id defined
			if (!string.IsNullOrEmpty(_model.AoEId))
			{
				GetParent<Shooter>().SpawnAoE(_model.AoEId, collisionPosition);
			}

			if (DamageEnemy(targetObject, contactPoint, false))
			{
				// Every enemy hit by piercing will reduce it's effectiveness
				_damageMultiplier *= _model.DamageReduction;
			}
		}

		protected override float CalculateDamage(ref bool isCrit)
		{
			return base.CalculateDamage(ref isCrit) * _damageMultiplier;
		}
	}
}