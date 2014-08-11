using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyBaseViewModel : ObjectViewModel
    {
        private readonly EnemyBaseModel _model;

        public EnemyBaseViewModel(EnemyBaseModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Health = _model.Health;
        }

        #region Health
        public float Health { get; private set; }

        public void ApplyDamage(float damage)
        {
            if (Health > 0)
            {
                Health -= damage;
                if (Health <= 0)
                    Destroy();
            }
        }
        #endregion

        public float Speed { get { return _model.Speed; } }
    }
}
