using System;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Enemies
{
	/// <summary>
	///     Body part of a Boss
	///     Break when killed, will invoke OnDrop when the Boss is killed if it's Break
	/// </summary>
	public class Limb : LivingObject
	{
		public event Action OnBreak;

		private readonly LimbModel _model;
		private readonly Enemy _parent;

		private bool _isBroken;

		public Limb(LimbModel model, Enemy parent) : base(model, parent)
		{
			_model = model;
			_parent = parent;

			_model.DeathDelay = parent.DeathDelay;
		}

		public override bool ApplyDamage(float damage, bool isCrit, Vector3 contactPoint, ProjectileBase source = null)
		{
			var damageMultiplied = damage * (_isBroken ? _model.BrokenDamageMultiplier : _model.DamageMultiplier);

			base.ApplyDamage(damageMultiplied, isCrit, contactPoint, source);
			return _parent.ApplyDamage(damageMultiplied, isCrit, Vector3.zero);
		}

		protected override void CreateHealthBar()
		{
		}

		protected override void HideHealthBar(string reason)
		{
		}

		protected override void OnKilled()
		{
			if (OnBreak != null)
			{
				OnBreak();
			}

			_isBroken = true;

			if (!string.IsNullOrEmpty(_model.CollisionEffectBroken))
			{
				CollisionEffectNormal = _model.CollisionEffectBroken;
			}
		}

		public void Kill()
		{
			if (_isBroken)
			{
				base.OnKilled();
			}
		}
	}
}