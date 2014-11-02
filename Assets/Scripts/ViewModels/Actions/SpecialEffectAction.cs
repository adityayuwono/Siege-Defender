using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class SpecialEffectAction : SetterAction
    {
        private readonly SpecialEffectActionModel _model;

        public SpecialEffectAction(SpecialEffectActionModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
