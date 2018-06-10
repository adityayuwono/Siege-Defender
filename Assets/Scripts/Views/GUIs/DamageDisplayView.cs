using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class DamageDisplayView : IntervalView
	{
		public DamageDisplayView(DamageDisplayManager viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}
	}
}