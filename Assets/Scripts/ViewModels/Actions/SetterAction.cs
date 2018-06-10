using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	public class SetterAction : BaseAction
	{
		private readonly SetterActionModel _model;

		public SetterAction(SetterActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();

			var property = Target;

			if (property is Property<string>)
			{
				var stringProperty = (AdjustableProperty<string>) property;
				stringProperty.SetValue(_model.Value);
			}
			else if (property is Property<bool>)
			{
				var boolProperty = (AdjustableProperty<bool>) property;
				boolProperty.SetValue(bool.Parse(_model.Value));
			}
			else if (property is Property<float>)
			{
				var floatProperty = (AdjustableProperty<float>) property;
				var propertyValue = floatProperty.GetValue();
				var newValue = 0f;
				if (_model.Value.StartsWith("["))
				{
					if (_model.Value.StartsWith("[+]"))
					{
						newValue = propertyValue + float.Parse(_model.Value.Replace("[+]", ""));
					}
					else if (_model.Value.StartsWith("[-]"))
					{
						newValue = propertyValue - float.Parse(_model.Value.Replace("[-]", ""));
					}
				}
				else
				{
					newValue = float.Parse(_model.Value);
				}

				floatProperty.SetValue(newValue);
			}
		}
	}
}