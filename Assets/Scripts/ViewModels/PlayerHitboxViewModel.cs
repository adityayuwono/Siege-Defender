using Scripts.Models;

namespace Scripts.ViewModels
{
    public class PlayerHitboxViewModel : ElementViewModel
    {
        private readonly PlayerHitbox_Model _model;

        public PlayerHitboxViewModel(PlayerHitbox_Model model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        public void CollideWithTarget(Object obj)
        {
            var enemyViewModel = obj as EnemyBase;
            if (enemyViewModel != null)
                enemyViewModel.ApplyDamage(float.PositiveInfinity, null);
        }
    }
}
