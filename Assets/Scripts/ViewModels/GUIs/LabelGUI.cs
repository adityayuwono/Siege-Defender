using Scripts.Core;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class LabelGUI : ObjectViewModel
    {
        private Label_GUIModel _model;
        public LabelGUI(Label_GUIModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public readonly Property<string> Text = new Property<string>();

        public string Font
        {
            get { return _model.Font; }
        }
    }
}
