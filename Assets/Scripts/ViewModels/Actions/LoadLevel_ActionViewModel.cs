using Scripts.Models;

namespace Scripts.ViewModels.Actions
{
    public class LoadLevel_ActionViewModel : Base_ActionViewModel
    {
        private readonly LoadScene_ActionModel _model;

        public LoadLevel_ActionViewModel(LoadScene_ActionModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Invoke()
        {
        }
    }
}
