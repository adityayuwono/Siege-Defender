using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class EquipmentSlot_View : ElementView
    {
        private readonly EquipmentSlot_ViewModel _viewModel;
        public EquipmentSlot_View(EquipmentSlot_ViewModel viewModel, Inventory_View parent) : base(viewModel, parent)
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
