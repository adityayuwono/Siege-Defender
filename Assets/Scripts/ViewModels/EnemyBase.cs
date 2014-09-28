using Scripts.Core;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
    public class EnemyBase : LivingObject
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
    }
}
