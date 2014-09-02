using Scripts.Models;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class GUIRoot : ElementViewModel
    {
        private GUIRootModel _model;
        public GUIRoot(GUIRootModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
