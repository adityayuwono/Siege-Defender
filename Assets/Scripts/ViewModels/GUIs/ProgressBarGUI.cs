using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class ProgressBarGUI : ValueDisplayGUI
    {
        private readonly ProgressBarGUIModel _model;

        public ProgressBarGUI(ProgressBarGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
