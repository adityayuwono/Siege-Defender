﻿using System;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
    public class EnemyBase : LivingObject, IContext
    {
        private readonly EnemyBaseModel _model;

        public EnemyBase(EnemyBaseModel model, Object parent) : base(model, parent)
        {
            _model = model;

            AnimationId = new AdjustableProperty<string>("AnimationId", this);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            if (!string.IsNullOrEmpty(_model.Target))
                Target = Root.GetViewModelAsType<Object>(_model.Target);
        }

        public Object Target { get; private set; }

        protected override void OnKilled()
        {
            Hide("Killed");// Start the hiding process when the enemy is killed
        }

        public readonly AdjustableProperty<string> AnimationId;
        protected override void OnDeactivate()
        {
            AnimationId.SetValue("");

            base.OnDeactivate();
        }

        #region Model Properties

        public virtual float Speed
        {
            get { return _model.Speed; }
        }
        public float Rotation
        {
            get { return _model.Rotation/2f; }
        }

        #endregion

        public PropertyLookup PropertyLookup
        {
            get
            {
                if (_propertyLookup == null)
                    _propertyLookup = new PropertyLookup(Root, this);

                return _propertyLookup;
            }
        }

        public float AttackSpeed
        {
            get { return _model.AttackSpeed; }
        }

        private PropertyLookup _propertyLookup;

        #region Events

        public event Action OnSpawn;
        public void InvokeOnSpawn()
        {
            if (OnSpawn != null) 
                OnSpawn();
        }

        public event Action OnWalk;
        public void InvokeOnWalk()
        {
            if (OnWalk != null) 
                OnWalk();
        }
        
        public event Action OnAttack;
        public void InvokeOnAttack()
        {
            if (OnAttack != null) 
                OnAttack();
        }

        #endregion
    }
}
