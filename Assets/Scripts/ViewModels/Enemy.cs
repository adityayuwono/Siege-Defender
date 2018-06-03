using System;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
    public class Enemy : LivingObject, IContext
    {
        private readonly EnemyModel _model;

        public Enemy(EnemyModel model, Base parent) : base(model, parent)
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
            base.OnKilled();

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
        private PropertyLookup _propertyLookup;

        public float AttackSpeed
        {
            get { return _model.AttackSpeed; }
        }

        #region Events

        public event Action Spawn;
        public void OnSpawn()
        {
            if (Spawn != null) 
                Spawn();
        }

        public event Action Walk;
        public void OnWalk()
        {
            if (Walk != null) 
                Walk();
        }
        
        public event Action Attack;
        public void OnAttack()
        {
            if (Attack != null) 
                Attack();
        }

        #endregion
    }
}
