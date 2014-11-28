using System;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class Object : Triggerable
    {
        private readonly ObjectModel _model;

        public Object(ObjectModel model, Base parent) : base(model, parent)
        {
            _model = model;

            if (string.IsNullOrEmpty(_model.AssetId))
                throw new EngineException(this, "No Asset defined");

            // Instantiate children elements
            if (_model.Elements != null)
            {
                foreach (var elementModel in _model.Elements)
                {
                    var elementVM = Root.IoCContainer.GetInstance<Object>(elementModel.GetType(), new object[] {elementModel, this});
                    if (elementVM == null)
                        throw new EngineException(this, string.Format("Failed to find ViewModel for {0}:{1}", elementModel.GetType(), elementModel.Id));
                    Elements.Add(elementVM);
                }
            }

            _position = _model.Position.ParseVector3();
        }

        public RandomPositionManager RandomPositionManager { get; private set; }
        /// <summary>
        /// Activate and Assign a position manager
        /// </summary>
        /// <param name="manager">Position Manager</param>
        public void Activate(RandomPositionManager manager)
        {
            RandomPositionManager = manager;

            Activate();
        }

        protected readonly List<Object> Elements = new List<Object>(); 

        protected override void OnActivate()
        {
            base.OnActivate();

            _isDelaysIgnored = false;
            
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

        public event Action OnStartSpecialEvent;
        public void StartSpecialEvent()
        {
            if (OnStartSpecialEvent != null)
                OnStartSpecialEvent();
        }

        #region Death

        public virtual bool ApplyDamage(float damage, Vector3 contactPoint, ProjectileBase source = null)
        {
            return false;
        }

        /// <summary>
        /// All things die eventually, we can only delay the inevitable
        /// This will delay the deactivation to show death animation, unless it is ignored (e.g. for caching)
        /// </summary>
        public float DeathDelay
        {
            get { return _isDelaysIgnored ? 0 : _model.DeathDelay; }
        }

        public Action<Object> OnObjectDeactivated;
        
        protected override void OnDeactivate()
        {
            base.OnDeactivate();

            if (OnObjectDeactivated != null)
                OnObjectDeactivated(this);

            OnObjectDeactivated = null;
        }
        #endregion

        protected override void OnDestroyed()
        {
            foreach (var element in Elements)
                element.Destroy();

            base.OnDestroyed();
        }

        #region Model Properties
        public string Type
        {
            get { return _model.Type; }
        }

        public string AssetId
        {
            get
            {
                if (_model.AssetId.StartsWith("{"))
                    return GetParent<IContext>().PropertyLookup.GetProperty<string>(_model.AssetId).GetValue();
                return _model.AssetId;
            }
        }

        private Vector3 _position;
        public virtual Vector3 Position
        {
            get { return _position; }
            protected set { _position = value; }
        }
        #endregion

        private bool _isDelaysIgnored;
        public virtual void TriggerIgnoreDelays()
        {
            _isDelaysIgnored = true;
        }
    }
}
