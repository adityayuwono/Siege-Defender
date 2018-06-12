using Scripts.Components;
using Scripts.ViewModels;
using Scripts.ViewModels.Items;

namespace Scripts.Views.Items
{
	public class EquipmentSlotView : ElementView
	{
		private readonly EquipmentSlot _viewModel;

		public EquipmentSlotView(EquipmentSlot viewModel, InventoryView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			GameObject.AddComponent<DragDropContainerController>().OnDropped += _viewModel.HandleObjectDropped;
		}

		protected override void OnDestroy()
		{
			GameObject.GetComponent<DragDropContainerController>().OnDropped -= _viewModel.HandleObjectDropped;

			base.OnDestroy();
		}
	}
}