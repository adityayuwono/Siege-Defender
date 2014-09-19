using Scripts.Models;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels
{
    public class LoadSceneActionViewModel : BaseActionViewModel
    {
        private readonly LoadScene_ActionModel _model;
        public LoadSceneActionViewModel(LoadScene_ActionModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Invoke()
        {
            Root.ChangeScene(_model.Target);
        }
    }
}
