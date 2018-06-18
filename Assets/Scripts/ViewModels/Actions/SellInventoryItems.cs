using System.Linq;
using Scripts.Contexts;
using Scripts.Models.Items;
using Scripts.ViewModels.Items;

namespace Scripts.ViewModels.Actions
{
	public class SellInventoryItems : BaseInventoryAction
	{
		private readonly SellInventoryItemsModel _model;
		public SellInventoryItems(SellInventoryItemsModel model, Base parent)
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

			DataContext.Instance.AddMoney(totalItemPrice);
		}
	}
}
