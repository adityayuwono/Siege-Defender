using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class BaseGUIView : ElementView
	{
		public BaseGUIView(BaseGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}
	}
}