using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class Static : Object
	{
		private readonly StaticModel _model;
		public Static(StaticModel model, Base parent)
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
