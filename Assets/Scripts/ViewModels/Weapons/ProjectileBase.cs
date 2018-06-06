﻿using Scripts.Models.Weapons;
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

		protected virtual float CalculateDamage(ref bool isCrit)
		{
			var currentDamage = Random.Range(_model.Damage[0], _model.Damage[1]);

			isCrit = Random.Range(0f, 1f) <= _model.CriticalChance;
			if (isCrit)
			{
				currentDamage *= _model.CriticalDamageMultiplier;
			}

			return currentDamage;
		}

		public virtual void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
		{
		}

		protected bool DamageEnemy(Object enemy, Vector3 contactPoint, bool attachToEnemy = false)
		{
			var isCrit = false;
			var damage = CalculateDamage(ref isCrit);
			var isDamageApplied = enemy.ApplyDamage(damage, isCrit, contactPoint, attachToEnemy ? this : null);
			return isDamageApplied;
		}
	}
}