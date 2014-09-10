using Scripts.Models;

namespace Scripts.ViewModels
{
    public class SceneViewModel : ObjectViewModel
    {
        private readonly Scene_Model _model;

        public SceneViewModel(Scene_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
