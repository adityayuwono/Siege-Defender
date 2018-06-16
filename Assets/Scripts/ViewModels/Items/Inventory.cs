using System;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class Inventory : Element, IContext
	{
		public Action ChildrenChanged;

		private readonly InventoryModel _model;
		private PropertyLookup _propertyLookup;

		public Inventory(InventoryModel model, IHaveRoot parent)
			: base(model, parent)
		{
			_model = model;

			foreach (var itemModel in _model.Items)
			{
				Elements.Add(IoC.IoCContainer.GetInstance<Item>(itemModel.GetType(), new object[] {itemModel, this}));
			}

			foreach (var equipmentSlotModel in _model.EquipmentSlots)
			{
				Elements.Add(IoC.IoCContainer.GetInstance<EquipmentSlot>(equipmentSlotModel.GetType(), new object[] { equipmentSlotModel, this }));
			}
		}

		public PropertyLookup PropertyLookup
		{
			get
			{
				if (_propertyLookup == null)
				{
					_propertyLookup = new PropertyLookup(Root, this);
				}

				return _propertyLookup;
			}
		}

		public void OverrideModelForShowing(IHaveRoot parent, string modelAssetId)
		{
			Parent = parent;
			_model.AssetId = modelAssetId;
		}

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
			if (ChildrenChanged != null)
			{
				ChildrenChanged();
			}
		}
	}
}