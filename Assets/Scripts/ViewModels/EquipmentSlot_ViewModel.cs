using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EquipmentSlot_ViewModel : ElementViewModel
    {
        private readonly EquipmentSlot_Model _model;
        private readonly Inventory_ViewModel _parent;
        public EquipmentSlot_ViewModel(EquipmentSlot_Model model, Inventory_ViewModel parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;

            ProjectileId = new AdjustableProperty<string>("ProjectileId", this);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            CurrentItem = new Item_ViewModel(_model.Item, this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            _currentItem.Activate();
        }

        public override void Show()
        {
            base.Show();

            _currentItem.Show();
        }

        public override void Hide(string reason)
        {
            _currentItem.Hide(reason);

            base.Hide(reason);
        }


        public readonly AdjustableProperty<string> ProjectileId;

        private Item_ViewModel _currentItem;
        private Item_ViewModel CurrentItem
        {
            set { OnItemUpdate(value); }
        }

        private void OnItemUpdate(Item_ViewModel itemViewModel)
        {
            _parent.ReleaseItem(itemViewModel);// Remove it from the inventory, do this first to make sure there's a spot left in the inventory

            if (_currentItem != null)
                _parent.AddItem(_currentItem);// Send the current item back to inventory

            _currentItem = itemViewModel;// Swap the current item
            _currentItem.ChangeParent(this);
            _model.Item = _currentItem.Model;// Save the change to model
            ProjectileId.SetValue(_currentItem.Base);// Update projectile used
        }



        public void Object_OnDropped(ObjectViewModel objectViewModel)
        {
            CurrentItem = objectViewModel as Item_ViewModel;
        }
    }
}
