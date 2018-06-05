using Scripts.ViewModels;

namespace Scripts.Views
{
	public class ObjectDisplayView : IntervalView
	{
		private readonly ObjectDisplay _viewModel;

		public ObjectDisplayView(ObjectDisplay viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;

			_viewModel.CurrentObject.OnChange += CurrentProjectile_OnChange;
		}

		private void CurrentProjectile_OnChange()
		{
		}
	}
}