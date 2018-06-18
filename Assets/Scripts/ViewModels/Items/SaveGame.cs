using Scripts.Contexts;
using Scripts.Models.Actions;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Items
{
	public class SaveGame : BaseAction
	{
		public SaveGame(BaseActionModel model, Base parent) : base(model, parent)
		{
		}

		public override void Invoke()
		{
			base.Invoke();

			DataContext.Instance.Save();
		}
	}
}
