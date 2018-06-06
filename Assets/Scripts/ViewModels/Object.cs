using System;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels
{
	public class Object : Triggerable
	{
		private readonly ObjectModel _model;

		protected readonly List<Object> Elements = new List<Object>();

		private bool _isDelaysIgnored;
		private Vector3 _position;
		public Action<Object> OnObjectDeactivated;

		public Object(ObjectModel model, Base parent) : base(model, parent)
		{
			_model = model;

			//if (string.IsNullOrEmpty(_model.AssetId))
			//    throw new EngineException(this, "No Asset defined");

			// Instantiate children elements
			if (_model.Elements != null)
				foreach (var elementModel in _model.Elements)
				{
					var element = IoC.IoCContainer.GetInstance<Object>(elementModel.GetType(), new object[] {elementModel, this});
					if (element == null)
						throw new EngineException(this,
							string.Format("Failed to find ViewModel for {0}:{1}", elementModel.GetType(), elementModel.Id));
					Elements.Add(element);
				}

			_position = _model.Position.ParseVector3();
		}

		/// <summary>
		///     All things die eventually, we can only delay the inevitable
		///     This will delay the deactivation to show death animation, unless it is ignored (e.g. for caching)
		/// </summary>
		public float DeathDelay
		{
			get { return _isDelaysIgnored ? 0 : _model.DeathDelay; }
		}

		public RandomPositionManager RandomPositionManager { get; private set; }

		public string Type
		{
			get { return _model.Type; }
		}

		public string AssetId
		{
			get
			{
				if (_model.AssetId.StartsWith("{"))
				{
					return GetParent<IContext>().PropertyLookup.GetProperty<ItemModel>(_model.AssetId).GetValue().BaseItem;
				}
				return _model.AssetId;
			}
		}

		public virtual Vector3 Position
		{
			get { return _position; }
			protected set { _position = value; }
		}

		public event Action OnStartSpecialEvent;

		/// <summary>
		///     Activate and Assign a position manager
		/// </summary>
		/// <param name="manager">Position Manager</param>
		public void Activate(RandomPositionManager manager)
		{
			RandomPositionManager = manager;

			Activate();
		}

		public void StartSpecialEvent()
		{
			if (OnStartSpecialEvent != null) OnStartSpecialEvent();
		}

		public virtual bool ApplyDamage(float damage, bool isCrit, Vector3 contactPoint, ProjectileBase source = null)
		{
			return false;
		}

		public virtual void TriggerIgnoreDelays()
		{
			_isDelaysIgnored = true;
		}

		public override void Show()
		{
			base.Show();

			foreach (var element in Elements) element.Show();
		}

		public override void Hide(string reason)
		{
			base.Hide(reason);

			foreach (var element in Elements) element.Hide(string.Format("Child of {0} was hidden because: {1}", Id, reason));
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			_isDelaysIgnored = false;

			foreach (var element in Elements) element.Activate();
		}

		protected override void OnDeactivate()
		{
			base.OnDeactivate();

			if (OnObjectDeactivated != null) OnObjectDeactivated(this);

			OnObjectDeactivated = null;
		}

		protected override void OnDestroyed()
		{
			foreach (var element in Elements) element.Destroy();

			base.OnDestroyed();
		}
	}
}