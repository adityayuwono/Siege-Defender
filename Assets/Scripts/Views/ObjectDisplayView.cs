using Scripts.ViewModels;

namespace Scripts.Views
{
	public class ObjectDisplayView : IntervalView
	{
		public ObjectDisplayView(ObjectDisplay viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			viewModel.CurrentObject.OnChange += CurrentProjectile_OnChange;
		}

		private void CurrentProjectile_OnChange()
		{
		}
	}
}