using System;
using System.Collections.Generic;
using System.Globalization;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Inventory : Element
    {
        private readonly InventoryModel _model;
        public Inventory(InventoryModel model, Base parent) : base(model, parent)
        {
            // Grab reference to Player's Inventory loaded from XML
            foreach (var inventoryModel in Root.PlayerSettingsModel.Inventories)
                if (inventoryModel.Id == model.Source)
                    _model = inventoryModel;

            foreach (var itemModel in _model.Items)
                Elements.Add(Root.IoCContainer.GetInstance<Item>(itemModel.GetType(), new object[] {itemModel, this}));

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
        public Item(ItemModel model, Base parent) : base(model, parent)
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

    public class ProjectileItem : Item
    {
        private readonly ProjectileItemModel _model;
        public ProjectileItem(ProjectileItemModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        private ProjectileModel _projectileModel;

        public string Stats { get; private set; }

        public ProjectileModel GetProjectileModel()
        {
            if (_projectileModel != null)
                return _projectileModel;

            var baseProjectileModel = Root.GetObjectModel(_model.Base) as ProjectileModel;
            if (baseProjectileModel == null)
                throw new EngineException(this, string.Format("Failed to Find a projectile model with id: {0}", _model.Base));

            var newProjectileModel = Copier.CopyAs<ProjectileModel>(baseProjectileModel);
            newProjectileModel.Id = baseProjectileModel.Id + "_" + Guid.NewGuid();

            var overriderModel = _model.Overrides;
            if (overriderModel != null)
            {
                #region Calculate for damage increase

                var originalSplitDamages = baseProjectileModel.Damage.Split('-');
                var overrideSplitDamage = overriderModel.Damage.Split('-');
                var augmentedDamages = new List<string>();

                for (var i = 0; i < originalSplitDamages.Length; i++)
                {
                    var originalSplitDamage = float.Parse(originalSplitDamages[i]) + float.Parse(overrideSplitDamage[i]);
                    augmentedDamages.Add(originalSplitDamage.ToString(CultureInfo.InvariantCulture));
                }

                #endregion

                newProjectileModel.Damage = string.Join("-", augmentedDamages.ToArray());
                newProjectileModel.Scatters += overriderModel.Scatters;
                newProjectileModel.Ammunition += overriderModel.Ammunition;
            }

            Stats = string.Format("Damage: {0}\n\nRate of Fire: {1}\nAmmunition: {2}", 
                newProjectileModel.Damage, 
                newProjectileModel.RoF, 
                newProjectileModel.Ammunition);

            // Register the new Model, to make sure it's available for duplication later
            Root.AddNewObjectModel(newProjectileModel);
            _projectileModel = new ProjectileModel();

            return newProjectileModel;
        }
    }
}
