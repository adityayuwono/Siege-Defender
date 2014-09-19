using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EquipmentSlot : ElementViewModel
    {
        private readonly EquipmentSlot_Model _model;
        public EquipmentSlot(EquipmentSlot_Model model, Inventory parent) : base(model, parent)
        {
            _model = model;

            ProjectileId = new AdjustableProperty<string>("ProjectileId", this);
            CurrentItem = new Item(_model.Item, this);
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

        protected override void OnDestroyed()
        {
            _currentItem.Destroy();

            base.OnDestroyed();
        }

        public readonly AdjustableProperty<string> ProjectileId;

        private Item _currentItem;
        private Item CurrentItem
        {
            set { OnItemUpdate(value); }
        }

        private void OnItemUpdate(Item itemViewModel)
        {
            var inventoryParent = GetParent<Inventory>();
            inventoryParent.ReleaseItem(itemViewModel);// Remove it from the inventory, do this first to make sure there's a spot left in the inventory

            if (_currentItem != null)
                inventoryParent.AddItem(_currentItem);// Send the current item back to inventory

            _currentItem = itemViewModel;// Swap the current item
            _currentItem.ChangeParent(this);
            _model.Item = _currentItem.Model;// Save the change to model
            ProjectileId.SetValue(_currentItem.Base);// Update projectile used

            Root.Save();
        }

        public void Object_OnDropped(Object objectViewModel)
        {
            CurrentItem = objectViewModel as Item;
        }
    }
}
