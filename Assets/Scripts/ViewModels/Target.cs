using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Target : Object
	{
		private readonly TargetModel _model;

		public Target(TargetModel model, Object parent) : base(model, parent)
		{
			_model = model;
		}
	}
}