using System;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class ObjectViewModel : BaseViewModel
    {
        private readonly ObjectModel _model;

        public ObjectViewModel(ObjectModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

            if (string.IsNullOrEmpty(_model.AssetId))
                throw new EngineException(this, "No Asset defined");
        }



        protected readonly List<ObjectViewModel> Children = new List<ObjectViewModel>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var child in Children)
                child.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var child in Children)
                child.Show();
        }

        public override void Hide(string reason)
        {
            base.Hide(reason);

            foreach (var child in Children)
                child.Hide(string.Format("Child of {0} was hidden because: {1}", Id, reason));
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }


        #region Death
        /// <summary>
        /// All things die eventually, we can only delay the inevitable
        /// </summary>
        public virtual float DeathDelay
        {
            get { return 0f; }
        }
        
        public Action<ObjectViewModel> OnObjectDeath;
        
        public void InvokeOnObjectDeath(string reason)
        {
            if (OnObjectDeath != null)
                OnObjectDeath(this);

            OnObjectDeath = null;

            Deactivate(reason);
        }
        #endregion


        #region Model Properties
        public string Type
        {
            get { return _model.Type; }
        }

        public string AssetId
        {
            get { return _model.AssetId; }
        }

        public virtual Vector3 Position
        {
            get { return UnityExtension.ParseVector3(_model.Position); }
        }
        #endregion
    }
}
