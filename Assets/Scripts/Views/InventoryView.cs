using Scripts.Components.UI;
using Scripts.ViewModels;

namespace Scripts.Views
{
	public class InventoryView : ElementView
	{
		public const string ItemSlotRoots = "ItemSlot";

		private readonly Inventory _viewModel;
		private Table table;

		public InventoryView(Inventory viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;

			_viewModel.ChildrenChanged += Children_OnChanged;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			table = Transform.Find("ItemSlot").GetComponent<Table>();
		}

		protected override void OnDestroy()
		{
			_viewModel.ChildrenChanged -= Children_OnChanged;

			base.OnDestroy();
		}

		private void Children_OnChanged()
		{
			if (table != null)
			{
				table.Reposition();
			}
		}
	}
}