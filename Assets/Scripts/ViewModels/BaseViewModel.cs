using System;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class BaseViewModel : IBase
    {
        private readonly Base_Model _model;
        public BaseViewModel Parent { get; protected set; }
        protected BaseViewModel(Base_Model model, BaseViewModel parent)
        {
            _model = model;
            Parent = parent;

            // Generate a unique Id if there's none
            if (string.IsNullOrEmpty(_model.Id))
                _model.Id = Guid.NewGuid().ToString();
            
            // Immediately register this to the main Engine
            Root.RegisterViewModel(this);
        }

        #region Actions
        public Action OnShow;
        public Action<string> OnHide;
        #endregion

        #region Activation

        protected bool _isActive { get; private set; }
        private bool _isLoaded;
        public void Activate()
        {
            if (_isActive)
                throw new EngineException(this, "Failed to Activate.\n" + _lastDeactivationReason);

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
            if (OnShow != null)
                OnShow();
        }

        private Views.BaseView _view;
        protected virtual void OnLoad()
        {
            _view = Root.IoCContainer.GetInstance<Views.BaseView>(GetType(), new object[] {this, Parent != null ? Parent._view : null});
            Root.RegisterView(this, _view);
        }

        protected virtual void OnActivate() { }
        #endregion

        #region Deactivation

        private string _lastDeactivationReason;
        public void Deactivate(string reason)
        {
            _lastDeactivationReason = reason;

            if (!_isActive)
                throw new EngineException(this, "Failed to Deactivate, Reason for deactivation "+reason);

            _isActive = false;

            OnDeactivate();
        }
        
        public virtual void Hide(string reason)
        {
            if (OnHide != null)
                OnHide(reason);
        }

        protected virtual void OnDeactivate() { }
        #endregion


        public T GetParent<T>() where T : BaseViewModel
        {
            var parent = Parent as T;
            return parent ?? Parent.GetParent<T>();
        }

        public virtual EngineBase Root { get { return Parent.Root; } }

        public string Id
        {
            get { return _model.Id; }
        }
    }
}
