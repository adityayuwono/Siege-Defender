using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class BaseGUI : Element
	{
		private BaseGUIModel _model;

		public BaseGUI(BaseGUIModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}
	}
}