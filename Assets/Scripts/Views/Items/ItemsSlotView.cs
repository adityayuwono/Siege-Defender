using Scripts.ViewModels.Items;

namespace Scripts.Views.Items
{
    public class ItemsSlotView : DropableSlotsView
    {
        public ItemsSlotView(ItemsSlot viewModel, ObjectView parent) : 
            base(viewModel, parent)
        {
        }
    }
}
