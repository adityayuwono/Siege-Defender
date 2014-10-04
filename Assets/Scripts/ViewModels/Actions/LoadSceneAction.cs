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
        }

        public override void Invoke()
        {
            base.Invoke();

            Root.ChangeScene(_model.Target, _model.LevelId);
        }
    }
}
