using Scripts.Helpers;
using Scripts.Models;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels
{
    public class LoadSceneAction : BaseAction
    {
        private readonly LoadSceneActionModel _model;
        public LoadSceneAction(LoadSceneActionModel model, Base parent) : base(model, parent)
        {
            _model = model;

            if (!string.IsNullOrEmpty(_model.Target))
                throw new EngineException(this, "Doesn't need Target, specify the scene id in the Value");
        }

        public override void Invoke()
        {
            base.Invoke();

            Root.ChangeScene(_model.Value, _model.LevelId);
        }
    }
}
