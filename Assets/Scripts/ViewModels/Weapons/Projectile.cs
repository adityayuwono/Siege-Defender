using System;
using Scripts.Core;
using Scripts.Models.Weapons;
using Scripts.Views;
using UnityEngine;

namespace Scripts.ViewModels.Weapons
{
	public class Projectile : ProjectileBase
	{
		public event Action Hit;
		public event Action<ObjectView, float> DoShooting;
		public float[] SpeedDeviations;
		public readonly Property<bool> IsKinematic = new Property<bool>();

		private readonly ProjectileModel _model;

		private bool _hasCollided;

		public Projectile(ProjectileModel model, Shooter parent) : base(model, parent)
		{
			_model = model;

			CalculateSpeed();
		}

		public override Vector3 Position
		{
			get { return Vector3.zero; }
		}

		public float Accuracy
		{
			get { return 1 - _model.Stats.Accuracy; }
		}

		private void CalculateSpeed()
		{
			SpeedDeviations = _model.Stats.SpeedDeviation;
		}

		public void Shoot(ObjectView target, float accuracy)
		{
			if (DoShooting != null)
			{
				DoShooting(target, accuracy);
			}
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			_hasCollided = false;
			IsKinematic.SetValue(false);
		}

		public override void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
		{
			// BUG: Need checking here because sometimes two collisions can happen very quickly
			if (_hasCollided)
			{
				return;
			}
			_hasCollided = true;

			IsKinematic.SetValue(true);

			// Spawn AoE if there are any Id defined
			if (!string.IsNullOrEmpty(_model.Stats.AoEId))
			{
				GetParent<Shooter>().SpawnAoE(_model.Stats.AoEId, collisionPosition);
			}

			OnHit();

			if (!DamageEnemy(targetObject, collisionPosition, true))
			{
				Hide("Hit Nothing"); // If we don't hit an enemy, hide the projectile
			}
			else
			{
				GameEndStats.AddOneHit();
			}
		}

		private void OnHit()
		{
			if (Hit != null)
			{
				Hit();
			}
		}
	}
}