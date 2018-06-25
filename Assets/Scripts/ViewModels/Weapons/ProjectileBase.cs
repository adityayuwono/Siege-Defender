using Scripts.Models.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Weapons
{
	public class ProjectileBase : Object
	{
		private readonly ProjectileModel _model;

		public ProjectileBase(ProjectileModel model, Object parent) : base(model, parent)
		{
			_model = model;
		}

		public virtual float HideDelay
		{
			get { return 0f; }
		}

		public virtual void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
		{
		}

		protected virtual float CalculateDamage(ref bool isCrit)
		{
			var currentDamage = Random.Range(_model.Stats.Damage[0], _model.Stats.Damage[1]);

			isCrit = Random.Range(0f, 1f) <= _model.Stats.CriticalChance;
			if (isCrit)
			{
				currentDamage *= _model.Stats.CriticalDamageMultiplier;
			}

			return currentDamage;
		}

		protected bool DamageEnemy(Object enemy, Vector3 contactPoint, bool attachToEnemy = false)
		{
			var isCrit = false;
			var damage = CalculateDamage(ref isCrit);

			var isDamageUpActive = GetParent<Player>().DamageUpDuration.GetValue() > 0;
			if (isDamageUpActive)
			{
				damage *= 1.25f;
			}

			var isDamageApplied = enemy.ApplyDamage(damage, isCrit, contactPoint, attachToEnemy ? this : null);
			if (isDamageApplied)
			{
				GameEndStatsManager.AddDamage(damage);
			}
			return isDamageApplied;
		}
	}
}