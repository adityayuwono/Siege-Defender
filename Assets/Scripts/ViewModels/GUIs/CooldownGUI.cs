using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class CooldownGUI : ValueDisplayGUI
    {
        private readonly CooldownGUIModel _model;

        public CooldownGUI(CooldownGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
