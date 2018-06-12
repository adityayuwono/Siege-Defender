using System;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
	public class Enemy : LivingObject, IContext
	{
		public event Action Spawn;
		public event Action Walk;
		public event Action Attack;
		
		private readonly EnemyModel _model;
		public readonly AdjustableProperty<string> AnimationId;
		private PropertyLookup _propertyLookup;

		public Enemy(EnemyModel model, Base parent) : base(model, parent)
		{
			_model = model;

			AnimationId = new AdjustableProperty<string>("AnimationId", this);
		}

		public Object Target { get; private set; }

		public virtual float Speed
		{
			get { return _model.Speed; }
		}

		public float Rotation
		{
			get { return _model.Rotation / 2f; }
		}

		public float AttackSpeed
		{
			get { return _model.AttackSpeed; }
		}

		public PropertyLookup PropertyLookup
		{
			get
			{
				if (_propertyLookup == null)
				{
					_propertyLookup = new PropertyLookup(Root, this);
				}

				return _propertyLookup;
			}
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			if (!string.IsNullOrEmpty(_model.Target))
			{
				Target = Root.GetViewModelAsType<Object>(_model.Target);
			}
		}

		protected override void OnKilled()
		{
			base.OnKilled();
			GameEndStatsManager.AddOneEnemyKilled();
			Hide("Killed"); // Start the hiding process when the enemy is killed
		}

		protected override void OnDeactivate()
		{
			AnimationId.SetValue("");

			base.OnDeactivate();
		}

		#region Events

		public void OnSpawn()
		{
			if (Spawn != null)
			{
				Spawn();
			}
		}

		public void OnWalk()
		{
			if (Walk != null)
			{
				Walk();
			}
		}

		public void OnAttack()
		{
			if (Attack != null)
			{
				Attack();
			}
		}

		#endregion
	}
}