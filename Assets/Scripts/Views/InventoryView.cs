using Scripts.ViewModels;

namespace Scripts.Views
{
	public class InventoryView : ElementView
	{
		private readonly Inventory _viewModel;
		private UITable _uiTable;

		public InventoryView(Inventory viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;

			_viewModel.OnChildrenChanged += Children_OnChanged;
		}

		private void Children_OnChanged()
		{
			if (_uiTable != null) _uiTable.Reposition();
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_uiTable = Transform.Find("ItemSlot").GetComponent<UITable>();
		}

		protected override void OnDestroy()
		{
			_viewModel.OnChildrenChanged -= Children_OnChanged;

			base.OnDestroy();
		}
	}
}