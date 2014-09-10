using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class BaseGUI : ElementViewModel
    {
        private Base_GUIModel _model;
        public BaseGUI(Base_GUIModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
