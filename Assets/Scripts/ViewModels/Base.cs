using System;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Views;

namespace Scripts.ViewModels
{
	public class Base : IBase
	{
		private readonly BaseModel _model;
		private bool _isActive;
		private bool _isLoaded;
		private string _lastDeactivationReason;
		private BaseView _view;
		public Action OnDestroy;
		public Action<string> OnHide;
		public Action OnShow;

		protected Base(BaseModel model, Base parent)
		{
			_model = model;
			Parent = parent;

			// Generate a unique Id if there's none
			if (string.IsNullOrEmpty(_model.Id))
			{
				_model.Id = Guid.NewGuid().ToString();
			}
		}

		public bool IsShown { get; private set; }

		public Base Parent { get; protected set; }

		public virtual RootBase Root
		{
			get { return Parent.Root; }
		}

		public virtual GameRoot SDRoot
		{
			get { return Parent.SDRoot; }
		}

		public virtual string Id
		{
			get { return _model.Id; }
		}

		public virtual string FullId
		{
			get { return Parent != null ? Parent.FullId + "/" + Id : Id; }
		}

		public T GetParent<T>() where T : class, IBase
		{
			var parent = Parent as T;
			return parent ?? Parent.GetParent<T>();
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", GetType(), Id);
		}

		#region Activation

		public void Activate()
		{
			if (_isActive) throw new EngineException(this, "Failed to Activate.\n" + _lastDeactivationReason);

			_isActive = true;

			if (!_isLoaded)
			{
				_isLoaded = true;
				OnLoad();
			}

			OnActivate();
		}

		public virtual void Show()
		{
			if (IsShown) throw new EngineException(this, "trying to show twice");

			IsShown = true;

			if (OnShow != null) OnShow();
		}

		protected virtual void OnLoad()
		{
			Root.RegisterToLookup(this);

			_view = IoC.IoCContainer.GetInstance<BaseView>(GetType(), new object[] { this, Parent != null ? Parent._view : null });
			Root.RegisterView(this, _view);
		}

		protected virtual void OnActivate()
		{
		}

		#endregion

		#region Deactivation

		public void Deactivate(string reason)
		{
			//UnityEngine.Debug.Log(FullId+":"+reason);
			var lastDeactivationReason = _lastDeactivationReason;
			_lastDeactivationReason = reason;

			if (!_isActive)
				throw new EngineException(this,
					string.Format("Failed to Deactivate\n" +
								  "Reason for deactivation: {0}\n" +
								  "Last Deactivation reason was: {1}", reason, lastDeactivationReason));

			_isActive = false;

			OnDeactivate();
		}

		public virtual void Hide(string reason)
		{
			if (!IsShown) throw new EngineException(this, "Trying to hide twice");

			IsShown = false;

			if (OnHide != null) OnHide(reason);
		}

		protected virtual void OnDeactivate()
		{
		}

		#endregion

		#region Destruction

		public void Destroy()
		{
			if (_isActive) Deactivate("Destroyed");

			OnDestroyed();

			if (OnDestroy != null) OnDestroy();

			_isLoaded = false; // Finally we will reload a destroyed object
		}

		protected virtual void OnDestroyed()
		{
			_view = null;
			Root.UnregisterView(this);
			Root.UnregisterFromLookup(this);
		}

		#endregion
	}
}