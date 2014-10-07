﻿using Scripts.Core;
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


        protected override void OnKilled()
        {
            Hide("Killed");// Start the hiding process when the enemy is killed
        }

        public readonly AdjustableProperty<string> AnimationId;

        public EnemyManager EnemyManager { get; private set; }
        public void Activate(EnemyManager manager)
        {
            EnemyManager = manager;

            Activate();
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
        private PropertyLookup _propertyLookup;
    }
}
