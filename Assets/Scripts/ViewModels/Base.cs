using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Views;
using System;
using System.Collections.Generic;

namespace Scripts.ViewModels
{
    public class Base : HaveRoot, IHaveView
    {
        public Action OnDestroy;
        public Action<string> OnHide;
        public Action OnShow;

        public List<Property> Properties = new List<Property>();

        private readonly BaseModel _model;
        private bool _isActive;
        private bool _isLoaded;
        private string _lastDeactivationReason;

        protected Base(BaseModel model, IHaveRoot parent) :
            base(model, parent)
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

        public BaseView View { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", GetType(), Id);
        }

        #region Activation

        public void Activate()
        {
            if (_isActive)
            {
                throw new EngineException(this, "Failed to Activate.\n" + _lastDeactivationReason);
            }

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
            if (IsShown)
            {
                throw new EngineException(this, "trying to show twice");
            }

            IsShown = true;

            if (OnShow != null)
            {
                OnShow();
            }
        }

        protected virtual void OnLoad()
        {
            Root.RegisterToLookup(this);

            var parentHaveView = Parent as IHaveView;
            View = IoC.IoCContainer.GetInstance<BaseView>(GetType(),
                new object[] { this, parentHaveView != null ? parentHaveView.View : null });
            Root.RegisterView(this, View);
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
            {
                throw new EngineException(this,
                    string.Format("Failed to Deactivate\n" +
                                  "Reason for deactivation: {0}\n" +
                                  "Last Deactivation reason was: {1}", reason, lastDeactivationReason));
            }

            _isActive = false;

            OnDeactivate();
        }

        public virtual void Hide(string reason)
        {
            if (!IsShown)
            {
                throw new EngineException(this, "Trying to hide twice");
            }

            IsShown = false;

            if (OnHide != null)
            {
                OnHide(reason);
            }
        }

        protected virtual void OnDeactivate()
        {
        }

        #endregion

        #region Destruction

        public void Destroy()
        {
            if (_isActive)
            {
                Deactivate("Destroyed");
            }

            OnDestroyed();

            if (OnDestroy != null)
            {
                OnDestroy();
            }

            _isLoaded = false; // Finally we will reload a destroyed object
        }

        protected virtual void OnDestroyed()
        {
            View = null;
            Root.UnregisterView(this);
            Root.UnregisterFromLookup(this);
        }

        #endregion
    }
}