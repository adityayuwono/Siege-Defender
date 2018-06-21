using System.Linq;
using Scripts.Contexts;
using Scripts.Models.Items;
using Scripts.ViewModels.Items;

namespace Scripts.ViewModels.Actions
{
	public class ClearInventory : BaseInventoryAction
	{
		private readonly ClearInventoryModel _model;
		public ClearInventory(ClearInventoryModel model, Base parent)
			: base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();

			var items = TargetInventory.Elements.Where(e => e.GetType() == typeof(Item)).Cast<Item>().ToArray();
			var totalItemPrice = items.Sum(i => i.Price);
			foreach (var item in items)
			{
				TargetInventory.ReleaseItem(item);
			}

			if (_model.SellItems)
			{
				DataContext.Instance.AddMoney(totalItemPrice);
			}
		}
	}
}
