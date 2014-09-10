using Scripts.Models;

namespace Scripts.ViewModels
{
    public class PlayerHitboxViewModel : ElementViewModel
    {
        private readonly PlayerHitbox_Model _model;

        public PlayerHitboxViewModel(PlayerHitbox_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public void CollideWithTarget(ObjectViewModel obj)
        {
            var enemyViewModel = obj as EnemyBaseViewModel;
            if (enemyViewModel != null)
                enemyViewModel.ApplyDamage(float.PositiveInfinity, null);
        }
    }
}
