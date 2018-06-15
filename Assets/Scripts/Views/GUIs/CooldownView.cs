using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class CooldownView : PercentageView
	{
		public CooldownView(Cooldown viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected override void UpdateValueDisplay(float value, float maxValue)
		{
			Image.fillAmount = value / maxValue;
		}
	}
}