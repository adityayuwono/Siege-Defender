using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class PlayerHitboxViewModel : ElementViewModel, IDamageEnemies
    {
        private readonly PlayerHitboxModel _model;

        public PlayerHitboxViewModel(PlayerHitboxModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public void DamageEnemy(string enemyId)
        {
            Root.DamageEnemy(enemyId, float.PositiveInfinity);
        }
    }
}
