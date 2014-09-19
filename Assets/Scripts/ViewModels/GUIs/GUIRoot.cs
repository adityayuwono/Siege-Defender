using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class GUIRoot : Element
    {
        private RootGUIModel _model;
        public GUIRoot(RootGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
