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

            foreach (var elementModel in _model.Elements)
            {
                var elementVM = Root.IoCContainer.GetInstance<ObjectViewModel>(elementModel.GetType(), new object[] { elementModel, this });
                if (elementVM == null)
                    throw new EngineException(this, string.Format("Failed to find ViewModel for {0}:{1}", elementModel.GetType(), elementModel.Id));
                Elements.Add(elementVM);
            }
        }

        protected readonly List<ObjectViewModel> Elements = new List<ObjectViewModel>(); 

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var element in Elements)
                element.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var element in Elements)
                element.Show();
        }

        public override void Hide(string reason)
        {
            base.Hide(reason);

            foreach (var element in Elements)
                element.Hide(string.Format("Child of {0} was hidden because: {1}", Id, reason));
        }

        


        #region Death

        public virtual bool ApplyDamage(float damage, ProjectileBaseViewModel source = null)
        {
            return false;
        }
        
        /// <summary>
        /// All things die eventually, we can only delay the inevitable
        /// </summary>
        public virtual float DeathDelay
        {
            get { return 0f; }
        }
        
        public Action<ObjectViewModel> OnObjectDeactivated;
        
        protected override void OnDeactivate()
        {
            base.OnDeactivate();

            if (OnObjectDeactivated != null)
                OnObjectDeactivated(this);

            OnObjectDeactivated = null;
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
