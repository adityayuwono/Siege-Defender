﻿using System;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Inventory_ViewModel : ElementViewModel
    {
        private readonly Inventory_Model _model;
        public Inventory_ViewModel(Inventory_Model model, Object parent) : base(model, parent)
        {
            // Grab reference to Player's Inventory loaded from XML
            _model = Root.InventoryModel;

            foreach (var itemModel in _model.Items)
                Elements.Add(new Item_ViewModel(itemModel, this));

            foreach (var equipmentSlotModel in _model.EquipmentSlots)
                Elements.Add(new EquipmentSlot_ViewModel(equipmentSlotModel, this));
        }

        public Action OnChildrenChanged;

        public override void Show()
        {
            base.Show();

            InvokeChildrenChanged();
        }

        public void AddItem(Item_ViewModel itemViewModel)
        {
            // Item was removed from inventory

            Elements.Add(itemViewModel);
            _model.Items.Add(itemViewModel.Model);
            itemViewModel.ChangeParent(this);

            InvokeChildrenChanged();
        }
        public void ReleaseItem(Item_ViewModel itemViewModel)
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

    

    public class Item_ViewModel : Object
    {
        private readonly Item_Model _model;
        public Item_Model Model{get { return _model; }}
        public Item_ViewModel(Item_Model model, Object parent) : base(model, parent)
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
