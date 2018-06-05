using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class GUIRootView : ElementView
	{
		private GUIRoot _viewModel;

		public GUIRootView(GUIRoot viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}
	}
}