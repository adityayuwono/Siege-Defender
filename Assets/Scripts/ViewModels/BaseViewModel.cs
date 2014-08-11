using System;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class BaseViewModel : IBase
    {
        public Action OnShow;
        public Action OnHide;

        private readonly BaseModel _model;
        private readonly BaseViewModel _parent;
        protected BaseViewModel(BaseModel model, BaseViewModel parent)
        {
            _model = model;
            _parent = parent;

            if (string.IsNullOrEmpty(_model.Id))
                _model.Id = Guid.NewGuid().ToString();
        }



        #region Activation
        private bool _isActive;
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
            _view = Root.IoCContainer.GetInstance<Views.BaseView>(GetType(), new object[] {this, _parent != null ? _parent._view : null});
            Root.RegisterView(this, _view);
        }

        protected virtual void OnActivate() { }
        #endregion

        #region Deactivation

        public void Deactivate()
        {
            if (!_isActive)
                throw new EngineException(this, "Failed to Deactivate");

            _isActive = false;

            OnDeactivate();
        }

        public virtual void Hide()
        {
            if (OnHide != null)
                OnHide();
        }

        protected virtual void OnDeactivate() { }
        #endregion



        public virtual EngineBase Root { get { return _parent.Root; } }

        public string Id
        {
            get { return _model.Id; }
        }
    }
}
