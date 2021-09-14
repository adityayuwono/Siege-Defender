using Scripts.ViewModels.Items;

namespace Scripts.Views.Items
{
    public class EquipmentSlotView : DropableSlotsView
	{
		public EquipmentSlotView(EquipmentSlot viewModel, InventoryView parent)
			: base(viewModel, parent)
		{

		}
	}
}