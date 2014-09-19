using Scripts.Models;

namespace Scripts.ViewModels
{
    public class PlayerHitbox : Element
    {
        private readonly PlayerHitboxModel _model;

        public PlayerHitbox(PlayerHitboxModel model, Object parent) : base(model, parent)
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
