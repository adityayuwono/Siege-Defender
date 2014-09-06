using Scripts.Models;

namespace Scripts.ViewModels
{
    public class SceneViewModel : ObjectViewModel
    {
        private readonly SceneModel _model;

        public SceneViewModel(SceneModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
