using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class BaseGUI : ElementViewModel
    {
        private BaseGUIModel _model;
        public BaseGUI(BaseGUIModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
