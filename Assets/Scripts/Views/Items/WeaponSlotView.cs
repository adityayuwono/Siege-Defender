using Scripts.ViewModels.Items;

namespace Scripts.Views.Items
{
	public class WeaponSlotView : EquipmentSlotView
	{
		public WeaponSlotView(EquipmentSlot viewModel, InventoryView parent) : base(viewModel, parent)
		{
		}
	}
}
