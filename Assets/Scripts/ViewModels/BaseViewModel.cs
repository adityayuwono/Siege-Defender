using System;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class BaseViewModel : IBase
    {
        private readonly BaseModel _model;
        public BaseViewModel Parent { get; protected set; }
        protected BaseViewModel(BaseModel model, BaseViewModel parent)
        {
            _model = model;
            Parent = parent;

            if (string.IsNullOrEmpty(_model.Id))
                _model.Id = Guid.NewGuid().ToString();
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
                throw new EngineException(this, "Failed to Activate");

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

        public void Deactivate(string reason)
        {
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
