using Scripts.Models;

namespace Scripts.ViewModels
{
    public class AoEViewModel : ProjectileBaseViewModel
    {
        private readonly AoEModel _model;
        public AoEViewModel(AoEModel model, ProjectileViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public float Radius
        {
            get { return _model.Radius; }
        }

        public override float DeathDelay
        {
            get { return 0.5f; }
        }

        public override void Show()
        {
            base.Show();

            Destroy();
        }

        public override void CollideWithTarget(ObjectViewModel obj)
        {
            var enemyViewModel = obj as EnemyBaseViewModel;
            if (enemyViewModel != null)
            {
                enemyViewModel.ApplyDamage(CalculateDamage(), this);
            }
        }
    }
}
