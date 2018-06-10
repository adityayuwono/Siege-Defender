using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	public class SpecialEffectAction : SetterAction
	{
		private readonly SpecialEffectActionModel _model;

		public SpecialEffectAction(SpecialEffectActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		protected override void OnDeactivate()
		{
			base.OnDeactivate();

			var property = Target;
			var stringProperty = (AdjustableProperty<string>) property;
			if (stringProperty.GetValue() == _model.Value)
			{
				stringProperty.SetValue("");
			}
		}
	}
}