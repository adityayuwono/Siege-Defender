using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
    public class Inventory : ItemsSlot, IContext
	{
		public AdjustableProperty<Item> SelectedItem;

		private readonly InventoryModel _model;
		private PropertyLookup _propertyLookup;

		public Inventory(InventoryModel model, IHaveRoot parent)
			: base(model, parent)
		{
			_model = model;

			SelectedItem = new AdjustableProperty<Item>("SelectedItem", this, true);

			foreach (var equipmentSlotModel in _model.EquipmentSlots)
			{
				var equipmentSlot =
					IoC.IoCContainer.GetInstance<EquipmentSlot>(equipmentSlotModel.GetType(), new object[] {equipmentSlotModel, this});
				Elements.Add(equipmentSlot);

				if (_model.AlwaysTransferFromSlots)
				{
					equipmentSlot.OnItemUpdate += GetItemFromSlot;
				} 
			}

			// Need this to trigger registration of context in parent
			var propertyLookup = PropertyLookup;
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

		private static void GetItemFromSlot(Item item)
		{
			var equipmentSlot = item.GetParent<EquipmentSlot>();
			if (equipmentSlot != null)
			{
				equipmentSlot.ReleaseItem();
			}
		}

		public void OverrideModelForShowing(IHaveRoot parent, string modelAssetId)
		{
			Parent = parent;
			_model.AssetId = modelAssetId;
		}

		public override void Hide(string reason)
		{
			SelectedItem.SetValue(null);

			base.Hide(reason);
		}

		public void SelectItem(Item item)
		{
			SelectedItem.SetValue(item);
		}
	}
}