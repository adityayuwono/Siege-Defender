using Scripts.Models;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class GUIRoot : ElementViewModel
    {
        private Root_GUIModel _model;
        public GUIRoot(Root_GUIModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
