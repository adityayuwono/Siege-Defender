using System;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Inventory : Element
    {
        private readonly InventoryModel _model;
        public Inventory(InventoryModel model, Object parent) : base(model, parent)
        {
            // Grab reference to Player's Inventory loaded from XML
            foreach (var inventoryModel in Root.PlayerSettingsModel.Inventories)
            {
                if (inventoryModel.Id == model.Source)
                    _model = inventoryModel;
            }

            foreach (var itemModel in _model.Items)
                Elements.Add(new Item(itemModel, this));

            foreach (var equipmentSlotModel in _model.EquipmentSlots)
                Elements.Add(new EquipmentSlot(equipmentSlotModel, this));
        }

        public Action OnChildrenChanged;

        public override void Show()
        {
            base.Show();

            InvokeChildrenChanged();
        }

        public void AddItem(Item item)
        {
            // Item was removed from inventory

            Elements.Add(item);
            _model.Items.Add(item.Model);
            item.ChangeParent(this);

            InvokeChildrenChanged();
        }
        public void ReleaseItem(Item itemViewModel)
        {
            // Item was added to inventory

            _model.Items.Remove(itemViewModel.Model);
            Elements.Remove(itemViewModel);

            InvokeChildrenChanged();
        }

        private void InvokeChildrenChanged()
        {
            if (OnChildrenChanged != null)
                OnChildrenChanged();
        }
    }

    

    public class Item : Object
    {
        private readonly ItemModel _model;
        public ItemModel Model{get { return _model; }}
        public Item(ItemModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }
        public string Base
        {
            get { return _model.Base; }
        }



        public Action<Object> OnParentChanged;
        public void ChangeParent(Object newParent)
        {
            Parent = newParent;

            if (OnParentChanged!=null)
                OnParentChanged(newParent);
        }
    }
}
