using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
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

			GameObject.AddComponent<DragDropContainerController>().OnDropped += _viewModel.Object_OnDropped;
		}

		protected override void OnDestroy()
		{
			GameObject.GetComponent<DragDropContainerController>().OnDropped -= _viewModel.Object_OnDropped;

			base.OnDestroy();
		}
	}
}