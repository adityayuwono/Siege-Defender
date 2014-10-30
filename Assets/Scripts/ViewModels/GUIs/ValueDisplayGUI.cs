using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class ValueDisplayGUI : BaseGUI
    {
        private readonly ValueDisplayGUIModel _model;

        public ValueDisplayGUI(ValueDisplayGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Value = GetParent<IContext>().PropertyLookup.GetProperty<float>(_model.Value);
            MaxValue = GetParent<IContext>().PropertyLookup.GetProperty<float>(_model.MaxValue);
        }

        public Property<float> Value;
        public Property<float> MaxValue;
    }
}
