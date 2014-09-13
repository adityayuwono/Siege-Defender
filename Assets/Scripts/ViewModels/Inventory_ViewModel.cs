using System;
using System.Collections.Generic;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Inventory_ViewModel : ElementViewModel
    {
        private readonly Inventory_Model _model;
        public Inventory_ViewModel(Inventory_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;

            foreach (var itemModel in _model.Items)
                Items.Add(new Item_ViewModel(itemModel, this));

            foreach (var equipmentSlotModel in _model.EquipmentSlots)
                EquipmentSlots.Add(new EquipmentSlot_ViewModel(equipmentSlotModel, this));
        }

        public Action OnChildrenChanged;

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var itemViewModel in Items)
                itemViewModel.Activate();

            foreach (var equipmentSlotViewModel in EquipmentSlots)
                equipmentSlotViewModel.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var itemViewModel in Items)
                itemViewModel.Show();

            foreach (var equipmentSlotViewModel in EquipmentSlots)
                equipmentSlotViewModel.Show();

            InvokeChildrenChanged();
        }

        public override void Hide(string reason)
        {
            foreach (var itemViewModel in Items)
                itemViewModel.Hide(reason);

            foreach (var equipmentSlotViewModel in EquipmentSlots)
                equipmentSlotViewModel.Hide(reason);

            base.Hide(reason);
        }

        public readonly List<Item_ViewModel> Items = new List<Item_ViewModel>();
        public readonly List<EquipmentSlot_ViewModel> EquipmentSlots =  new List<EquipmentSlot_ViewModel>(); 


        public void AddItem(Item_ViewModel itemViewModel)
        {
            // Item was removed from inventory

            Items.Add(itemViewModel);
            _model.Items.Add(itemViewModel.Model);
            itemViewModel.ChangeParent(this);

            InvokeChildrenChanged();
        }
        public void ReleaseItem(Item_ViewModel itemViewModel)
        {
            // Item was added to inventory

            _model.Items.Remove(itemViewModel.Model);
            Items.Remove(itemViewModel);

            InvokeChildrenChanged();
        }

        private void InvokeChildrenChanged()
        {
            if (OnChildrenChanged != null)
                OnChildrenChanged();
        }
    }

    

    public class Item_ViewModel : ObjectViewModel
    {
        private readonly Item_Model _model;
        public Item_Model Model{get { return _model; }}
        public Item_ViewModel(Item_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
        public string Base
        {
            get { return _model.Base; }
        }



        public Action<ObjectViewModel> OnParentChanged;
        public void ChangeParent(ObjectViewModel newParent)
        {
            Parent = newParent;

            if (OnParentChanged!=null)
                OnParentChanged(newParent);
        }
    }
}
