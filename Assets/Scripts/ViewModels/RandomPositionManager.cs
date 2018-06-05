using Scripts.Models;

namespace Scripts.ViewModels
{
	public class RandomPositionManager : Element
	{
		private readonly RandomPositionManagerModel _model;

		public RandomPositionManager(RandomPositionManagerModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}
	}
}