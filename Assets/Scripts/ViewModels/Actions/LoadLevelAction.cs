using Scripts.Models;

namespace Scripts.ViewModels.Actions
{
    public class LoadLevelAction : BaseAction
    {
        private readonly LoadSceneActionModel _model;

        public LoadLevelAction(LoadSceneActionModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Invoke()
        {
        }
    }
}
