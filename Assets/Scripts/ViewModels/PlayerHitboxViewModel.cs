using Scripts.Models;

namespace Scripts.ViewModels
{
    public class PlayerHitboxViewModel : ElementViewModel
    {
        private readonly PlayerHitboxModel _model;

        public PlayerHitboxViewModel(PlayerHitboxModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
