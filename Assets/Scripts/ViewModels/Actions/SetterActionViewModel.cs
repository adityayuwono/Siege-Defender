using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class SetterActionViewModel : BaseActionViewModel
    {
        private readonly Setter_ActionModel _model;
        public SetterActionViewModel(Setter_ActionModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public override void Invoke()
        {
            base.Invoke();

            var property = Property;

            if (property is Property<string>)
            {
                var stringProperty = property as AdjustableProperty<string>;
                stringProperty.SetValue(_model.Value);
            }
            else if (property is Property<bool>)
            {
                var boolProperty = property as AdjustableProperty<bool>;
                boolProperty.SetValue(bool.Parse(_model.Value));
            }
        }
    }
}
