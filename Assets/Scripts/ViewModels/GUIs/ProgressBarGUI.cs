using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class ProgressBarGUI : BaseGUI
    {
        private readonly ProgressBarGUIModel _model;

        public ProgressBarGUI(ProgressBarGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            ProgressProperty = GetParent<IContext>().PropertyLookup.GetProperty<float>(_model.Progress);
            MaxProgressProperty = GetParent<IContext>().PropertyLookup.GetProperty<float>(_model.MaxProgress);
        }

        public Property<float> ProgressProperty;
        public Property<float> MaxProgressProperty;
    }
}
