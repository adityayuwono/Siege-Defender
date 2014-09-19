using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Scene : Object
    {
        private readonly SceneModel _model;

        public Scene(SceneModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
