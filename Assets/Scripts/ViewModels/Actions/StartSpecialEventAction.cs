using Scripts.Helpers;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class StartSpecialEventAction : BaseAction
    {
        private readonly StartSpecialEventModel _model;

        public StartSpecialEventAction(StartSpecialEventModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Invoke()
        {
            base.Invoke();

            var targetObject = Target as Object;

            if (targetObject != null)
                targetObject.StartSpecialEvent();
            else
                throw new EngineException(this, string.Format("Target: {0} was not an object", _model.Target));
        }
    }
}
