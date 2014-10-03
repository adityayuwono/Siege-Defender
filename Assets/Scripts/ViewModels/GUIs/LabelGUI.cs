using Scripts.Core;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class LabelGUI : Object
    {
        private readonly LabelGUIModel _model;
        public LabelGUI(LabelGUIModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public readonly Property<string> Text = new Property<string>();
    }
}
