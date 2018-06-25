using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models.Enemies;
using Scripts.ViewModels.GUIs;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Enemies
{
	/// <summary>
	///     Base class for everything that have Health
	/// </summary>
	public class LivingObject : Object
	{
		public event Action Hit;
		public event Action Death;
		public event Func<Vector3> RequestPositionUpdate;
		public event Action DeathEnd;

		private readonly List<ProjectileBase> _attachedProjectiles = new List<ProjectileBase>();

		private readonly LivingObjectModel _model;
		public readonly AdjustableProperty<float> Health;

		public readonly AdjustableProperty<string> SpecialEffect;
		public Action<ProjectileBase> DoAttach;
		private HealthBar _healthBar;

		protected LivingObject(LivingObjectModel model, Base parent) : base(model, parent)
		{
			_model = model;

			// Validate Model
			if (string.IsNullOrEmpty(_model.Type))
			{
				throw new EngineException(this, string.Format("Type for: {0} is empty", FullId));
			}

			Health = new AdjustableProperty<float>("Health", this);
			CollisionEffectNormal = _model.CollisionEffectNormal;

			SpecialEffect = new AdjustableProperty<string>("SpecialEffect", this, true);
		}

		protected string CollisionEffectNormal { private get; set; }

		public float HealthPercentage
		{
			get { return Health.GetValue() / _model.Health; }
		}

		public bool IsDead
		{
			get { return Health.GetValue() <= 0; }
		}

		protected override void OnActivate()
		{
			// Need to be set first because some conditions rely on this
			Health.SetValue(_model.Health);

			base.OnActivate();
		}

		public override void Show()
		{
			base.Show();

			CreateHealthBar();
		}

		public override void Hide(string reason)
		{
			// Cleanup attached projectiles
			foreach (var projectile in _attachedProjectiles)
			{
				projectile.Hide(reason);
			}
			_attachedProjectiles.Clear();
			
			HideHealthBar(reason);

			base.Hide(reason);
		}
		
		/// <summary>
		///     Reduce health by the amount specified
		///     Health is only reduced if it is above 0
		/// </summary>
		/// <param name="damage">How many health we should reduce</param>
		/// <param name="contactPoint">Impact coordinate for displaying damage</param>
		/// <param name="source">Set if we want to attach the object to the target</param>
		public override bool ApplyDamage(float damage, bool isCrit, Vector3 contactPoint, ProjectileBase source = null)
		{
			if (source != null)
			{
				AttachProjectile(source);
			}

			var currentHealth = Health.GetValue();

			// To avoid killing the enemy more than once
			if (currentHealth > 0)
			{
				currentHealth -= damage;
				if (currentHealth <= 0)
				{
					OnKilled();
				}

				Health.SetValue(currentHealth);

				if (Hit != null)
				{
					Hit();
				}
			}

			// Because Vector3 is a struct and structs can't be null
			if (contactPoint != Vector3.zero)
			{
				SDRoot.DamageDisplay.DisplayDamage(damage, isCrit, contactPoint);

				if (!string.IsNullOrEmpty(CollisionEffectNormal))
				{
					SDRoot.SpecialEffectManager.StartSpecialEffectOn(CollisionEffectNormal, contactPoint);
				}
			}

			return true;
		}

		public void InvokeDeathEnd()
		{
			if (DeathEnd != null)
			{
				DeathEnd();
			}
		}

		protected virtual void CreateHealthBar()
		{
			_healthBar = SDRoot.DamageDisplay.CreateHealthBar();
			_healthBar.Activate(this);
			_healthBar.Show();
		}

		protected virtual void HideHealthBar(string reason)
		{
			_healthBar.Hide(reason);

			_healthBar = null;
		}

		/// <summary>
		///     Called once when damage taken is greater or equal to Health
		/// </summary>
		protected virtual void OnKilled()
		{
			if (Death != null)
			{
				Position = RequestPositionUpdate();
				Death();
			}
		}

		private void AttachProjectile(ProjectileBase source)
		{
			if (_attachedProjectiles.Contains(source))
			{
				throw new EngineException(this, "Duplicate Projectile hit");
			}

			_attachedProjectiles.Add(source);

			while (_attachedProjectiles.Count > _model.ProjectileLimit)
			{
				var projectileToRemove = _attachedProjectiles[0];
				_attachedProjectiles.Remove(projectileToRemove);
				projectileToRemove.Hide("Hiding because we have too many already");
			}

			DoAttach(source);
		}
	}
}