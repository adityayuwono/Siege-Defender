using System;
using Scripts.Core;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class Item : Object
	{
		public AdjustableProperty<String> BaseName;

		public Action<Object> ParentChanged;

		private readonly ItemModel _model;

		public Item(ItemModel model, Base parent)
			: base(model, parent)
		{
			_model = model;

			BaseName = new AdjustableProperty<string>("BaseName", this);
			BaseName.SetValue(_model.BaseItem);
		}

		public string BaseItem
		{
			get { return _model.BaseItem; }
		}

		public ItemModel Model
		{
			get { return _model; }
		}

		public string ItemSlotRoots
		{
			get { return _model.ItemSlotRoots; }
		}

		public int Quantity
		{
			get { return _model.Quantity; }
			set { _model.Quantity = value; }
		}

		public int Price
		{
			get { return _model.Price; }
		}

		public void ChangeParent(Object newParent)
		{
			Parent = newParent;

			if (ParentChanged != null)
			{
				ParentChanged(newParent);
			}
		}
		protected override void OnLoad()
		{
			base.OnLoad();

			UpdateItemStats();
		}

		protected virtual void UpdateItemStats()
		{
			_baseItemModel = DataContext.Instance.GetItemModel(_model.BaseItem);
			BaseName.SetValue(_baseItemModel.BaseItem);
			Price.SetValue(_baseItemModel.Price);
			Description.SetValue(_baseItemModel.Description);
		}
	}
}