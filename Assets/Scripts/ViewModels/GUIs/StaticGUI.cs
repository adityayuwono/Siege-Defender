using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class StaticGUI : Object
	{
		private readonly StaticGUIModel _model;
		public StaticGUI(StaticGUIModel model, Base parent)
			: base(model, parent)
		{
			_model = model;
		}

		public bool IsStatic
		{
			get { return _model.IsStatic; }
		}
	}
}
