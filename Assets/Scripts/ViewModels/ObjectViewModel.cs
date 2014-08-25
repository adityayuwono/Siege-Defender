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

        public override void Hide()
        {
            base.Hide();

            foreach (var child in Children)
                child.Hide();
        }

        protected override void OnDeactivate()
        {
            foreach (var child in Children)
                child.Deactivate();

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
        
        public void InvokeOnObjectDeath()
        {
            if (OnObjectDeath != null)
                OnObjectDeath(this);

            OnObjectDeath = null;

            Deactivate();
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

        public Vector3 Position
        {
            get { return UnityExtension.ParseVector3(_model.Position); }
        }
        #endregion
    }
}
