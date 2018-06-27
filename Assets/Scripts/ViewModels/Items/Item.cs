using System;
using Scripts.Contexts;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class Item : Object
	{
		public AdjustableProperty<string> BaseName;
		public AdjustableProperty<int> Price;
		public AdjustableProperty<string> Description;

		public Action<Object> ParentChanged;

		private readonly ItemModel _model;
		private ItemModel _baseItemModel;

		public Item(ItemModel model, Base parent)
			: base(model, parent)
		{
			_model = model;

			BaseName = new AdjustableProperty<string>("BaseName", this);
			Price = new AdjustableProperty<int>("Price", this);
			Description = new AdjustableProperty<string>("Description", this);
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

		public void ChangeParent(Object newParent)
		{
			Parent = newParent;

			if (ParentChanged != null)
			{
				ParentChanged(newParent);
			}
		}

		public void Select()
		{
			GetParent<Inventory>().SelectItem(this);
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