using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class BaseGUI : ObjectViewModel
    {
        private BaseGUIModel _model;
        public BaseGUI(BaseGUIModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
